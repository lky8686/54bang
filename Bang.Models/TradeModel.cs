using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    /// <summary>
    /// 平台-交易管理
    /// </summary>
    public class TradeModel
    {
        //序号	订单号	银行流水号	订单时间	清算机构	订单金额	手续费	交易状态
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 银行流水号
        /// </summary>
        public string BankSerialNumber { get; set; }

        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 清算机构
        /// </summary>
        public string TradeOrg { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Poundage { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        public string TradeStatus { get; set; }

    }
}
