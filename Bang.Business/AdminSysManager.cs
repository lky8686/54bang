using Bang.DataAccess;
using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Business
{
    public class AdminSysManager
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            return AdminSysDataAccess.VerifyUserLogin(userName, password);
        }

        public static List<CompanyEmployeeModel> GetCompanyEmpList(string city, string serviceType, string startDate, string endDate, string companyCode, string empAccount, int pageIndex, int pageSize, out int recordCount)
        {
            return AdminSysDataAccess.GetCompanyEmpList(city, serviceType, startDate, endDate, companyCode, empAccount, pageIndex, pageSize, out recordCount);
        }

        /// <summary>
        /// 等1的时候冻结 等0的时候解冻
        /// </summary>
        /// <param name="empAccount"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool SetCompanyEmpAccountStatus(string empAccount, string status)
        {
            return AdminSysDataAccess.SetCompanyEmpAccountStatus(empAccount, status);
        }
    }
}
