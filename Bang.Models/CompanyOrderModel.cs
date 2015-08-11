using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    [Serializable]
    public class CompanyOrderModel
    {
        //序号	交易流水号	业务类型	交易时间	业务状态	交易额	手续费	补贴费	交易对方	客户
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TransactionSerialNumber { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public string TransactionDate { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 业务状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 交易额
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// 手续费
        /// </summary>
        public decimal Poundage { get; set; }
        /// <summary>
        /// 补贴费
        /// </summary>
        public decimal Bounty { get; set; }
        /// <summary>
        /// 交易对方
        /// </summary>
        public string Traders { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
    }
}
