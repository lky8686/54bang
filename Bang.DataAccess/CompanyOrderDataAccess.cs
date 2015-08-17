using Bang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DataAccess
{
    public class CompanyOrderDataAccess
    {
        public static List<OrderModel> Query(string startDate, string endDate, string empAccount, string serviceType, string status, int pageIndex)
        {
            return new List<OrderModel> { };
        }
    }
}
