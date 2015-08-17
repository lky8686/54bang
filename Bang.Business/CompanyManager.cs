using Bang.DataAccess;
using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Business
{
    public class CompanyManager
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            return CompanyDataAccess.VerifyUserLogin(userName, password);
        }

        /// <summary>
        /// 获取师傅列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static List<CompanyEmployeeModel> GetEmpListBy(string empAccount, string status, string serviceType, int pageIndex)
        {
            return CompanyDataAccess.GetEmpListBy(empAccount, status, serviceType, pageIndex);
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public static List<object> GetCustomerList(string companyId)
        {
            return CompanyDataAccess.GetCustomerList(companyId);
        }
    }
}
