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
        public static List<CompanyEmployeeModel> GetEmpListBy(string companyCode, string empAccount, string status, string serviceType, int pageIndex, int pageSize, out int recordCount)
        {
            return CompanyDataAccess.GetEmpListBy(companyCode, empAccount, status, serviceType, pageIndex, pageSize, out recordCount);
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
