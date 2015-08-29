using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Common
{
    public static class SqlFilter
    {
        /// <summary>
        /// SQL注入过滤
        /// </summary>
        /// <param name="InText">要过滤的字符串</param>
        /// <returns>如果参数存在不安全字符，则返回true</returns>
        public static bool Filter(string InText)
        {
            //string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|'|;";            
            string word = "or |or+|or%20|and|exec|insert|select|delete|update|chr|mid|master|truncate|char|declare|join|'|;|--|cmdshell|drop |drop+|drop%20|alter |alter+|alter%20|create |create+|create%20";

            if (InText == null)
                return false;
            foreach (string str_t in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(str_t + " ") > -1) || (InText.ToLower().IndexOf(" " + str_t) > -1) || (InText.ToLower().IndexOf(str_t) > -1))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
