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
        public ActionResult Query()
        {
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
        public ActionResult TradeQuery()
        {
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
        public ActionResult SettlementQuery()
        {
            return View();
        }

    }
}
