using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    /// <summary>
    /// 用户=客户 信息
    /// </summary>
    public class CustomerModel
    {//用户账户	昵称	注册日期
        /// <summary>
        /// 用户账户
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegDate { get; set; }
    }
}
