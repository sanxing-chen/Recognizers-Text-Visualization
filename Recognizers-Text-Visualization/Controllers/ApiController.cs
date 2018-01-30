using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.DateTime;
using Microsoft.Recognizers.Text.NumberWithUnit;
using Newtonsoft.Json;
using Recognizers_Text_Visualization.DAL;
using Recognizers_Text_Visualization.Models;

namespace Recognizers_Text_Visualization.Controllers
{
    public class ApiController : Controller
    {
        private readonly TextContext db = new TextContext();

        // GET: Api
        public ActionResult Index()
        {
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Recognize(int id, string checkedModelString = "[]")
        {
            var text = db.Texts.Find(id);
            if (text == null) return HttpNotFound();

            var checkedModels =
                JsonConvert.DeserializeObject<int[]>(checkedModelString);

            string culture;
            switch (text.Language)
            {
                case Lang.Chinese:
                    culture = Culture.Chinese;
                    break;
                case Lang.English:
                    culture = Culture.English;
                    break;
                case Lang.French:
                    culture = Culture.French;
                    break;
                case Lang.Spanish:
                    culture = Culture.Spanish;
                    break;
                case Lang.Portuguese:
                    culture = Culture.Portuguese;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var results = new List<ModelResult>();
            if (checkedModels.Contains(1))
            {
                var model = NumberRecognizer.Instance.GetNumberModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(2))
            {
                var model = NumberRecognizer.Instance.GetOrdinalModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(3))
            {
                var model =
                    NumberRecognizer.Instance.GetPercentageModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(4))
            {
                var model =
                    NumberWithUnitRecognizer.Instance.GetAgeModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(5))
            {
                var model =
                    NumberWithUnitRecognizer.Instance.GetCurrencyModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(6))
            {
                var model =
                    NumberWithUnitRecognizer.Instance
                        .GetDimensionModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(7))
            {
                var model =
                    NumberWithUnitRecognizer.Instance.GetTemperatureModel(
                        culture);
                results.AddRange(model.Parse(text.Content));
            }

            if (checkedModels.Contains(8))
            {
                var model = DateTimeRecognizer.GetInstance()
                    .GetDateTimeModel(culture);
                results.AddRange(model.Parse(text.Content));
            }

            return Json(results);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,Language")]
            Text text)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home");
            db.Texts.Add(text);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}