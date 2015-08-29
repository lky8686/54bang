using Bang.Business;
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

        [HttpPost]
        public ActionResult SetCompanyEmpStatus(string empAccounts, string status)
        {
            return Json(new { success = AdminSysManager.SetCompanyEmpAccountStatus(empAccounts, status), msg = empAccounts });
        }

        [HttpPost]
        public ActionResult Query(string city, string serviceType, string startDate, string endDate, string company, string empAccount, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var pageSize = 20;
            var recordCount = 0;
            //todo 
            var list = AdminSysManager.GetCompanyEmpList(city, serviceType, startDate, endDate, company, empAccount, pageIndex, pageSize, out recordCount);

            ViewBag.RecordCount = recordCount;
            ViewBag.PageSize = 20;
            ViewBag.CurrentIndex = pageIndex;
            return View(list);
        }

        /// <summary>
        /// 师傅推荐
        /// </summary>
        /// <returns></returns>
        public ActionResult Recommend()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecommendQuery(string city, string year, string month, string company, string empAccount, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var pageSize = 20;
            var recordCount = 0;
            //todo 
            var list = AdminSysManager.GetEmployeeRecommendList(city, year, month, company, empAccount, pageIndex, pageSize, out recordCount);
            ViewBag.RecordCount = recordCount;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentIndex = pageIndex;
            return View(list);
        }

    }
}
