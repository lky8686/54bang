using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    [Serializable]
    public class OrderModel
    {
        //序号	订单号	交易时间	订单状态	服务分类	交易额	手续费	补贴费	师傅账户	用户账户
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public string TradeDate { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 服务分类
        /// </summary>
        public string ServiceType { get; set; }
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
        /// 师傅账户
        /// </summary>
        public string CompanyEmpAccount { get; set; }
        /// <summary>
        /// 用户账户
        /// </summary>
        public string Customer { get; set; }
    }
}
