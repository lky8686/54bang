using _54Bang.Web.Admin.Authentication;
using Bang.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace _54Bang.Web.Admin.Controllers
{
    [AdminSysAuthorize]
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
            var user = AdminSysManager.VerifyUserLogin(loginUserName, loginPassword);

            if (user != null)
            {
                if (user.Status == 1)
                {
                    Debug.WriteLine("user.Status为1");
                    LoginAuthentication.Instance.SignIn(user);
                }
                else
                {
                    Debug.WriteLine("user.Status不为1");
                    //return Json("true");
                    ViewBag.Flag = user.Status;
                    ViewBag.LoginName = loginUserName;
                    return View();
                }
            }
            else
            {
                Debug.WriteLine("user为null");
                ViewBag.Flag = "-1";
                ViewBag.LoginName = loginUserName;
                //return Json("false");
                return View();
            }

            
            //如果登录成功，跳转到 ReturnUrl 制定的地址
            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]) == false)
            {
                Debug.WriteLine("登陆不成功");
                return Redirect(Request.QueryString["ReturnUrl"]);
            }
            else
            {
                Debug.WriteLine("登陆成功");
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
