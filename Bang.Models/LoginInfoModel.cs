using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    [Serializable]
    public class LoginInfoModel
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// 用户状态（1启用，0停用）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Int16 Status { get; set; }
    }
}
