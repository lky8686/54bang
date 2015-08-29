using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Business
{
    public class BaseDataManager
    {
        public static Dictionary<string, string> GetCityList()
        {
            var result = new Dictionary<string, string>();
            result.Add("131000", "廊坊");
            result.Add("130100", "石家庄");
            result.Add("370200", "青岛市");
            result.Add("441900", "东莞市");
            result.Add("340300", "蚌埠市");
            result.Add("110000", "北京市");

            return result;
        }

        public static Dictionary<string, string> ServiceTypeList()
        {
            #region
            var result = new Dictionary<string, string>();
            result.Add("01", "管道维修");
            result.Add("02", "上门洗车");
            result.Add("03", "装修改造");
            result.Add("04", "家具");
            result.Add("05", "家用电器");
            result.Add("06", "手机电脑");
            result.Add("07", "跑腿代办");
            result.Add("08", "开锁换锁");
            result.Add("09", "保洁家政");
            result.Add("99", "其他");
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

        public static Dictionary<int, string> PaymentStatusList()
        {
            var result = new Dictionary<int, string>();
            result.Add(0, "支付成功;");
            result.Add(5, "支付失败");
            result.Add(7, "关闭订单失败");
            result.Add(8, "订单已关闭");
            return result;
        }
    }
}
