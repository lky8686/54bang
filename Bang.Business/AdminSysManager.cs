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

        public static List<CompanyEmployeeRecommendModel> GetEmployeeRecommendList(string city, string year, string month, string companyCode, string empAccount, int pageIndex, int pageSize, out int recordCount)
        {
            return AdminSysDataAccess.GetEmployeeRecommendList(city, year, month, companyCode, empAccount, pageIndex, pageSize, out recordCount);
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

        public static List<CustomerModel> GetCustomerList(string city, string startDate, string endDate, string mobile, int pageIndex, int pageSize, out int recordCount)
        {
            return AdminSysDataAccess.GetCustomerList(city, startDate, endDate, mobile, pageIndex, pageSize, out recordCount);
        }

        public static List<OrderModel> OrderQuery(string city, string startDate, string endDate, string empAccount, string orderStatus, string serviceType, int pageIndex, int pageSize, out int recordCount)
        {
            return AdminSysDataAccess.OrderQuery(city, startDate, endDate, empAccount, orderStatus, serviceType, pageIndex, pageSize, out recordCount);
        }

        public static List<TradeModel> OrderTradeQuery(string city, string startDate, string endDate, string orderNum, string bankSerialNumber, string tradeStatus, string tradeOrg, int pageIndex, int pageSize, out int recordCount, out decimal total, out int amount)
        {
            return AdminSysDataAccess.OrderTradeQuery(city, startDate, endDate, orderNum, bankSerialNumber, tradeStatus, tradeOrg, pageIndex, pageSize, out recordCount, out total, out amount);
        }

        public static List<CompanySettlementModel> CompanySettlementQuery(string companyCode, string year, string month, out decimal total)
        {
            return AdminSysDataAccess.CompanySettlementQuery(companyCode, year, month, out total);
        }

        public static List<SettlementModel> SettlementQuery(string city, string company, string companyEmp, string settlementNum, string startDate, string endDate, string settlementStatus, int pageIndex, int pageSize, out int recordCount, out decimal total, out int amount)
        {
            return AdminSysDataAccess.SettlementQuery(city, company, companyEmp, settlementNum, startDate, endDate, settlementStatus, pageIndex, pageSize, out recordCount, out total, out amount);
        }

        public static bool SetSettlementStatus(string number)
        {
            return AdminSysDataAccess.SetSettlementStatus(number);
        }

    }
}
