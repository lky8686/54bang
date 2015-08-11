using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DataAccess
{
    public class AdminSysDataAccess
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            return new LoginInfoModel
            {
                UserName = userName,
                Name = "运行平台",
                Status = 1
            };
        }
    }
}
