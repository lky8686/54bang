using Bang.Common;
using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _54Bang.Web.Company.Authentication
{
    public class UserContext
    {
        private const string CurrentUserContextCacheKey = "UserContext";

        private UserContext(LoginInfoModel user)
        {
            this.UserId = user.UserId;
            this.Name = user.Name;
            this.UserName = user.UserName;
            this.CompanyId = user.CompanyId;
        }

        public static UserContext Current
        {
            get
            {
                if (HttpContext.Current.Items[CurrentUserContextCacheKey] == null)
                {
                    var user = LoginAuthentication.Instance.GetAuthenticatedUser();
                    if (user == null)
                    {
                        return null;
                    }
                    var context = new UserContext(user);
                    HttpContext.Current.Items[CurrentUserContextCacheKey] = context;
                    return context;
                }
                return HttpContext.Current.Items[CurrentUserContextCacheKey] as UserContext;
            }
        }
        public string Area { get; private set; }

        public string UserId { get; private set; }

        public string Name { get; private set; }

        public string UserName { get; private set; }

        public string CompanyId { get; private set; }

        public static LoginInfoModel GetBy(string cookieValue)
        {
            var json = BangSecurity.DESDecrypt(HttpUtility.UrlDecode(cookieValue), BangSecurity.DESKEY);
            var user = JsonHelper.JsonToObject<LoginInfoModel>(json);
            return user;
        }
    }
}