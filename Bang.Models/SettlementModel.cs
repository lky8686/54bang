using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    public class SettlementModel
    {
        //Settlement序号	结算单号	对方编号	交易对方	结算生成日	收款人	收款人银行账号	收款银行	交易金额	补贴金额	提现金额	操作是否打出成功
        public string SettlementNumber { get; set; }
        /// <summary>
        /// 对方编号
        /// </summary>
        public string ReceiverNumber { get; set; }
        /// <summary>
        /// 交易对方
        /// </summary>
        public string ReceiverAccount { get; set; }
        public DateTime SettlementDate { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverBankAccount { get; set; }
        public decimal ReceiverTotal { get; set; }
        /// <summary>
        /// 1:申请提现;-2:已打款;
        /// </summary>
        public string SettlementStatus { get; set; }

    }
}
