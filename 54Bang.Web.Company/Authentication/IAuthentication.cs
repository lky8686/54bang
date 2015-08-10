using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _54Bang.Web.Company.Authentication
{
    interface IAuthentication
    {
        void SignIn(LoginInfoModel user, bool createPersistentCookie = false);
        void SignOut();
        LoginInfoModel GetAuthenticatedUser();
    }
}
