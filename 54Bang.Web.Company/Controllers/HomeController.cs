using _54Bang.Web.Company.Authentication;
using Bang.Business;
using Bang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _54Bang.Web.Company.Controllers
{
    [CompanyAuthorize]
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
            #region
            var user = CompanyManager.VerifyUserLogin(loginUserName, loginPassword);

            if (user != null)
            {                
                if (user.Status == 1)
                {
                    LoginAuthentication.Instance.SignIn(user);
                }
                else
                {
                    //return Json("true");
                    ViewBag.Flag = user.Status;
                    ViewBag.LoginName = loginUserName;
                    return View();
                }
            }
            else
            {
                ViewBag.Flag = "-1";
                ViewBag.LoginName = loginUserName;
                //return Json("false");
                return View();
            }
            //如果登录成功，跳转到 ReturnUrl 制定的地址
            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) == false)
            {
                return Redirect(Request.QueryString["ReturnUrl"]);
            }
            else
            {
                return RedirectToAction("Index");
            }
            #endregion
        }

        public ActionResult Logout()
        {
            LoginAuthentication.Instance.SignOut();
            return RedirectToAction("Login");
        }
    }
}
