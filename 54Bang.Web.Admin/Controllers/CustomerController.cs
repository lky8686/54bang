using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Admin.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Query(string city,string startDate,string endDate,string customerAccount,int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            //todo 

            ViewBag.RecordCount = 93;
            ViewBag.PageSize = 20;
            ViewBag.CurrentIndex = pageIndex;
            return View();
        }

    }
}
