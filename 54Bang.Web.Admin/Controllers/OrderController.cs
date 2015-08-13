using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Admin.Controllers
{
    public class OrderController : Controller
    {
        //
        // GET: /Order/
        /// <summary>
        /// 业务管理==订单管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Query(string city, string startDate, string endDate, string empAccount, string orderStatus, string serviceType, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            //todo 

            ViewBag.RecordCount = 93;
            ViewBag.PageSize = 20;
            ViewBag.CurrentIndex = pageIndex;
            return View();
        }

        /// <summary>
        /// 交易
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Trade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TradeQuery(string city, string startDate, string endDate, string orderNum, string bankSerialNumber, string tradeStatus, string sel_TradeOrg, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;

            //todo 

            ViewBag.RecordCount = 93;
            ViewBag.PageSize = 20;
            ViewBag.CurrentIndex = pageIndex;
            return View();
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settlement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SettlementQuery(string city, string company, string companyEmp, string settlementNum, string startDate, string endDate, string settlementStatus, int pageIndex)
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
