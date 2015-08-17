using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    [Serializable]
    public class CompanyEmployeeModel
    {
        //师傅账户	姓名	账户状态	信用等级	技能	注册时间	在线时长	订单数量	师傅详情
        public string Id { get; set; }
        public string CompanyId { get; set; }
        /// <summary>
        /// 师傅账户
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账户状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        public string CreditRating { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegDate { get; set; }    
    }
}
