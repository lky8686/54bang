using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Company.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string loginUserName, string loginPassword)
        {
            //UsersService usvr = new UsersService();
            //loginPassword = RCSecurity.MD5Hex(loginPassword);
            //var user = usvr.VerifyUserLogin(loginUserName, loginPassword);

            //if (user != null)
            //{
            //    new OperationLogService().Add(user.TenantId, user.UserId, LogType.Login, "登录");
            //    if (user.Status == 1)
            //    {
            //        RCAuthentication.Instance.SignIn(user);
            //    }
            //    else
            //    {
            //        //return Json("true");
            //        ViewBag.Flag = user.Status;
            //        ViewBag.LoginName = loginUserName;
            //        return View();
            //    }
            //}
            //else
            //{
            //    ViewBag.Flag = "-1";
            //    ViewBag.LoginName = loginUserName;
            //    //return Json("false");
            //    return View();
            //}
            //如果登录成功，跳转到 ReturnUrl 制定的地址
            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) == false)
            {
                return Redirect(Request.QueryString["ReturnUrl"]);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout()
        {            
            //RCAuthentication.Instance.SignOut();
            return RedirectToAction("Login");
        }
    }
}
