using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    public class CompanyEmpOrderStatModel
    {
        //序号	师傅账户	师傅姓名	订单数量	交易金额
        public string EmpAccount { get; set; }

        public string EmpName { get; set; }

        public int OrderCount { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
