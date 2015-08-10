using Bang.Common;
using Bang.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace _54Bang.Web.Company.Authentication
{
    public class LoginAuthentication : IAuthentication
    {
        private const string AUTHCOOKIENAME = "BangCompany";

        public readonly static LoginAuthentication Instance = new LoginAuthentication();


        public void SignIn(string loginName, string role, object user, bool createPersistentCookie = false)
        {
            //var json = JsonHelper.ObjectToJson(user);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            var encryptedJson = BangSecurity.DESEncrypt(json, BangSecurity.DESKEY);
            if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
            {
                CookieManager.SetCookie(AUTHCOOKIENAME, encryptedJson, DateTime.MinValue, FormsAuthentication.CookieDomain, "/");
            }
            else
            {
                CookieManager.SetCookie(AUTHCOOKIENAME, encryptedJson, DateTime.MinValue);
            }

            var ticket = new FormsAuthenticationTicket(1, loginName, DateTime.Now, DateTime.MaxValue, createPersistentCookie, role);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //cookie.Expires = DateTime.Now.Add(FormsAuthentication.Timeout);//DateTime.MinValue;
            cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        public void SignIn(LoginInfoModel user, bool createPersistentCookie = false)
        {
            SignIn(user.UserName, "100", user, createPersistentCookie);
        }

        public void SignOut()
        {
            CookieManager.DeleteCookie(AUTHCOOKIENAME, "/");

            FormsAuthentication.SignOut();
        }

        public LoginInfoModel GetAuthenticatedUser()
        {
            var encryptedJson = CookieManager.GetCookieValue(AUTHCOOKIENAME);
            if (string.IsNullOrEmpty(encryptedJson))
            {
                return null;
            }
            var json = BangSecurity.DESDecrypt(encryptedJson, BangSecurity.DESKEY);
            var user = JsonConvert.DeserializeObject<LoginInfoModel>(json);//JsonHelper.JsonToObject<LoginInfoModel>(encryptedJson);
            return user;
        }
    }
}