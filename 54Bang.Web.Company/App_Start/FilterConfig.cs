using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Company
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}