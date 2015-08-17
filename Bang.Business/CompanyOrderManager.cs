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
        public static List<OrderModel> Query(string startDate, string endDate, string empAccount, string serviceType, string status, int pageIndex)
        {
            return CompanyOrderDataAccess.Query(startDate, endDate, empAccount, serviceType, status, pageIndex);
        }
    }
}
