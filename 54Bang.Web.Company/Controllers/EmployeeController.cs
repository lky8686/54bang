using _54Bang.Web.Company.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Company.Controllers
{
    [CompanyAuthorize]
    /// <summary>
    /// 员工管理:师傅等等
    /// </summary>
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string mobileList)
        {
            return Json(new { success = true, msg = mobileList });
        }

    }
}
