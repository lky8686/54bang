using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    public class CompanySettlementModel
    {
        //序号	订单号	交易额	类型	师傅账户	订单时间
        public string OrderId { get; set; }

        public decimal TradeTotal { get; set; }

        public string ServiceType { get; set; }

        public string EmpAccount { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
