using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Recognizers_Text_Visualization.DAL;

namespace Recognizers_Text_Visualization.Controllers
{
    public class HomeController : Controller
    {
        private TextContext db = new TextContext();
        public ActionResult Index()
        {
            var texts = db.Texts.ToList();
            ViewBag.Texts = texts;
            ViewBag.TextJson = JsonConvert.SerializeObject(texts);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}