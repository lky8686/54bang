using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bang.Common
{
    /// <summary>
    /// Cookie管理器
    /// </summary>
    public class CookieManager
    {
        #region 私有

        /// <summary>
        /// 默认时间:DateTime.MinValue
        /// </summary>
        private static DateTime defaultExpires = DateTime.MinValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CookieManager()
        {
        }

        /// <summary>
        /// 写入Cookie的公用函数
        /// 可以为cookie对象赋于多个key键值
        /// 设键/值如下:
        /// NameValueCollection mycol = new NameValueCollection();
        /// mycol.Add("key1", "value1");
        /// mycol.Add("key2", "value2");
        /// mycol.Add("key3", "value3");
        /// mycol.Add("key1", "value4");
        /// 结果为"key1:value1,value4;key2:value2;key3:value3"
        /// </summary>
        /// <param name="cookiename">Cookie的名称，不能是Empty</param>
        /// <param name="cookievalue">Cookie的值可以Empty</param>
        /// <param name="cookievalues">二维Cookie的Values,可以是null</param>
        /// <param name="expires">过期时间，可以是DateTime或DateTime.MinValue--null</param>
        /// <param name="domain">Cookie的Domain ,可以是Empty</param>
        /// <param name="path">Cookie的Path,可以是Empty</param>
        private static void SetCookie(string cookiename, string cookievalue, NameValueCollection cookievalues, DateTime expires, string domain, string path)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
                cookie = new HttpCookie(cookiename);
            //if (cookievalue.Length > 0)
            cookie.Value = System.Web.HttpUtility.UrlEncode(cookievalue);
            if (cookievalues != null)
            {
                cookie.Values.Clear();
                //foreach (string key in cookievalues.Keys)
                //{
                //    cookie.Values.Add(key, System.Web .HttpUtility .UrlEncode (cookievalues[key]));
                //}
                cookie.Values.Add(cookievalues);
            }
            if (domain.Length > 0)
                cookie.Domain = domain;
            if (path.Length > 0)
                cookie.Path = path;
            if (expires != DateTime.MinValue)
                cookie.Expires = expires;
            else
                cookie.Expires = defaultExpires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 写入一个二维Cookie的一个键值
        /// </summary>
        ///  /// <param name="cookiename">Cookie的名称，不能是Empty</param>
        /// <param name="cookievalue">Cookie的值可以Empty</param>
        /// <param name="subkey">二维Cookie的Key</param>
        /// <param name="subvalue">二维Cookie的值</param>
        /// <param name="expires">过期时间，可以是DateTime或DateTime.MinValue--null</param>
        /// <param name="domain">Cookie的Domain ,可以是Empty</param>
        /// <param name="path">Cookie的Path,可以是Empty</param>
        private void SetCookie(string cookiename, string cookievalue, string subkey, string subvalue, DateTime expires, string domain, string path)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
                cookie = new HttpCookie(cookiename);
            //if (cookievalue.Length > 0)
            cookie.Value = System.Web.HttpUtility.UrlEncode(cookievalue);
            if (subkey.Length > 0)
            {
                cookie.Values.Add(subkey, System.Web.HttpUtility.UrlEncode(subvalue));
            }
            if (domain.Length > 0)
                cookie.Domain = domain;
            if (path.Length > 0)
                cookie.Path = path;
            if (expires != DateTime.MinValue)
                cookie.Expires = expires;
            else
                cookie.Expires = defaultExpires;
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Request.Cookies.Add(cookie);
        }

        #endregion 私有

        /// <summary>
        /// 过期时间的缺省值
        /// </summary>
        public static DateTime DefaultExpires
        {
            set
            {
                defaultExpires = value;
            }

            get
            {
                return defaultExpires;
            }
        }

        #region 获取cookie

        /// <summary>
        /// 得到一个Cookie的Value值
        /// </summary>
        /// <param name="cookiename">Cookie Name</param>
        /// <returns>Cookie的String 的值,如果没有返回string.Empty</returns>
        public static string GetCookieValue(string cookiename)
        {
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
                return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookiename].Value);
            else
                return string.Empty;
        }

        public static T GetCookieValue<T>(string cookiename) where T : class
        {
            T t = default(T);
            string ctr = string.Empty;
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                ctr = System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookiename].Value);
                if (!string.IsNullOrEmpty(ctr))
                {
                    try
                    {
                        t = JsonHelper.JsonToObject<T>(ctr);
                    }
                    catch (Exception ex)
                    {
                       
                    }

                }
            }
            return t;
        }

        /// <summary>
        /// 取得一个二维Cookie的值
        /// </summary>
        /// <param name="cookiename">Cookie的名称</param>
        /// <param name="subkey">二维的Key的名称</param>
        /// <returns>如果有则返回string的值，没有则返回string.Empty</returns>
        public static string GetSubCookieValue(string cookiename, string subkey)
        {
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                string cookieValue = System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookiename].Value);
                string subKeyName = subkey + "=";
                if (cookieValue.IndexOf(subKeyName) == -1)
                    return string.Empty;
                else
                    return System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[cookiename][subkey]);
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 得到Cookie的所有的二维Cookie的Values
        /// </summary>
        /// <param name="cookiename">Cookie 的名称</param>
        /// <returns>返回一个dictionary否则为null</returns>
        public static NameValueCollection GetCookieValues(string cookiename)
        {
            if (HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                NameValueCollection values = HttpContext.Current.Request.Cookies[cookiename].Values;
                if (values != null)
                {
                    int vCount = values.Keys.Count;

                    for (int i = 0; i < vCount; i++)
                    {
                        values[values[i]] = System.Web.HttpUtility.UrlDecode(values[values[i]]);
                    }
                }
                return values;
            }
            else
                return null;
        }



        #endregion 获取cookie

        #region 设置cookie

        /// <summary>
        /// 向客户端写入Cookie
        /// </summary>
        /// <param name="cookiename">Cookie的名称</param>
        /// <param name="cookievalue">Cookie的值</param>
        /// <param name="domain">cookie的Domian</param>
        /// <param name="path">Cookie 的Path</param>
        /// <param name="expires">Cookie的过期时间</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires, string domain, string path)
        {
            SetCookie(cookiename, cookievalue, null, expires, domain, path);
        }

        /// <summary>
        /// 向客户端写入Cookie
        /// </summary>
        /// <param name="cookiename">Cookie 的名称</param>
        /// <param name="cookievalue">Cookie的值</param>
        /// <param name="domain">Cookie的domain</param>
        /// <param name="path">Cookie的path</param>
        public static void SetCookie(string cookiename, string cookievalue, string domain, string path)
        {
            SetCookie(cookiename, cookievalue, null, DateTime.MinValue, domain, path);
        }

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="cookiename">Cookie 的名称</param>
        /// <param name="cookievalue">Cookie的Value</param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, null, DateTime.MinValue, string.Empty, string.Empty);
        }

        /// <summary>
        /// 写入一个Cookie的二维键值的集合
        /// </summary>
        /// <param name="cookiename">Cookie的名称</param>
        /// <param name="cookievalue">Cookie的值</param>
        /// <param name="cookievlues">Cookie的二维键值的集合</param>
        /// <param name="expires">过期时间，可以是DateTime.MinValue</param>
        public static void SetCookie(string cookiename, string cookievalue, NameValueCollection cookievlues, string domain, DateTime expires)
        {
            SetCookie(cookiename, cookievalue, cookievlues, expires, domain, string.Empty);
        }

        /// <summary>
        /// 写入Cookie,用缺省的过期时间，缺省的Path ,ASP.NET写COOKIE的缺省的Path是/,也就是ROOT
        /// </summary>
        /// <param name="cookiename">Cookie的名称</param>
        /// <param name="cookievalue">Cookie的值</param>
        /// <param name="expires">Cookie的过期时间</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];

            if (cookie == null)
            {
                cookie = new System.Web.HttpCookie(cookiename);
            }

            cookie.Value = System.Web.HttpUtility.UrlEncode(cookievalue);
            cookie.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 批量设置Cookie
        /// </summary>
        /// <param name="hash">Cookie hash表对象</param>
        /// <param name="expires">过期时间，可以是DateTime或DateTime.MinValue--null</param>
        public static void SetCookie(Hashtable hash, string domain, DateTime expires)
        {
            try
            {
                if (hash == null || hash.Count == 0)
                    return;

                foreach (DictionaryEntry item in hash)
                {
                    SetCookie(item.Key.ToString(), item.Value.ToString(), expires, domain, "/");
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        #endregion 设置cookie

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="cookiename">Cookie的名称</param>
        public static void DeleteCookie(string cookiename, string domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                if (!string.IsNullOrEmpty(domain)) { cookie.Domain = domain; }
                cookie.Expires = DateTime.Now.AddDays(-1);// DateTime.MinValue;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
