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
            var result = new Dictionary<int, string>();
            result.Add(1, "上门洗车");
            result.Add(2, "清洗油烟机");            

            return result;
        }
    }
}
