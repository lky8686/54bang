using Bang.DataAccess;
using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Business
{
    public class CompanyOrderManager
    {
        public static List<OrderModel> Query(string companyCode, string startDate, string endDate, string empAccount, string serviceType, string status, int pageIndex, int pageSize, out int recordCount)
        {
            return CompanyOrderDataAccess.Query(companyCode, startDate, endDate, empAccount, serviceType, status, pageIndex, pageSize, out recordCount);
        }

        public static List<CompanySettlementModel> SettlementQuery(string companyCode, string year, string month)
        {
            return CompanyOrderDataAccess.SettlementQuery(companyCode, year, month);
        }
    }
}
