using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Business
{
    public class BaseDataManager
    {
        public static Dictionary<int, string> GetCityList()
        {
            var result = new Dictionary<int, string>();
            result.Add(1, "北京");
            result.Add(2, "上海");
            result.Add(3, "东莞");

            return result;
        }

        public static Dictionary<int, string> ServiceTypeList()
        {
            #region
            var result = new Dictionary<int, string>();
            result.Add(1, "管道维修");
            result.Add(2, "上门洗车");
            result.Add(3, "装修改造");
            result.Add(4, "家具");
            result.Add(5, "家用电器");
            result.Add(6, "手机电脑");
            result.Add(7, "跑腿代办");
            result.Add(8, "开锁换锁");
            result.Add(9, "保洁家政");
            result.Add(10, "其他");
            return result;
            #endregion
        }

        public static Dictionary<int, string> OrderStatusList()
        {
            var result = new Dictionary<int, string>();
            result.Add(1, "服务中");
            result.Add(2, "等待支付");
            result.Add(3, "服务已完成");
            result.Add(4, "已取消");
            result.Add(5, "待退款");
            return result;
        }

        public static Dictionary<int, string> TradeStatusList()
        {
            var result = new Dictionary<int, string>();
            result.Add(1, "成功交易");
            result.Add(2, "失败交易");
            result.Add(3, "退款交易");
            result.Add(4, "进行中交易");
            return result;
        }
    }
}
