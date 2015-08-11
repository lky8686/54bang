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
    /// 订单管理
    /// </summary>
    public class OrderController : Controller
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

    }
}
