using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Admin.Controllers
{
    public class CompanyEmpController : Controller
    {
        //
        // GET: /CompanyEmp/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 师傅推荐
        /// </summary>
        /// <returns></returns>
        public ActionResult Recommend()
        {
            return View();
        }

    }
}
