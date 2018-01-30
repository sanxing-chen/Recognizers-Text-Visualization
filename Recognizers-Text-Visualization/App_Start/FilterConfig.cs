using System.Web;
using System.Web.Mvc;

namespace Recognizers_Text_Visualization
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
