using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bang.Common
{
    /// <summary>
    ///
    /// </summary>
    public static class JsonHelper
    {
        #region JsonToObject

        public static List<T> JsonFileToList<T>(string jsonDataPath) where T : class
        {
            if (string.IsNullOrEmpty(jsonDataPath))
                throw new ArgumentNullException("jsonString");

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonDataPath));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam> <param name="jsonData"></param> <returns></returns>
        public static List<T> JsonToList<T>(string jsonData) where T : class
        {
            if (string.IsNullOrEmpty(jsonData))
                throw new ArgumentNullException("jsonString");
            //Need to write this？？
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam> <param name="jsonData"></param> <returns></returns>
        public static T JsonToObject<T>(string jsonData) where T : class
        {
            if (string.IsNullOrEmpty(jsonData))
                throw new ArgumentNullException("jsonString");
            T t = default(T);

            try
            {
                t = JsonConvert.DeserializeObject<T>(jsonData); ;
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("Ex: {0} , {1}Json: {2}", ex.ToString(), System.Environment.NewLine, jsonData));
            }

            return t;
        }

        public static object JsonToObject(string jsonData, Type objType)
        {
            if (string.IsNullOrEmpty(jsonData))
                throw new ArgumentNullException("jsonString");
            object t = null;

            try
            {
                t = JsonConvert.DeserializeObject(jsonData, objType);
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("Ex: {0} , {1}Json: {2}", ex.ToString(), System.Environment.NewLine, jsonData));
            }

            return t;
        }

        #endregion JsonToObject

        #region ObjectToJson

        /// <summary>
        /// 将. net对象序列化成JSON格式字符串
        /// </summary>
        /// <typeparam name="T">.net对象类型</typeparam> <param name="o">.net对象实例</param>
        /// <returns>返回JSON格式字符串</returns>
        public static string ObjectToJson(object o)
        {
            return ObjectToJson(o, false, false);
        }

        /// <summary>
        /// 将. net对象序列化成JSON格式字符串
        /// </summary>
        /// <typeparam name="T">.net对象类型</typeparam> <param name="o">.net对象实例</param>
        /// <param name="needIndented">JSON内容是否需要缩进</param> <returns>返回JSON格式字符串</returns>
        public static string ObjectToJson(object o, bool needIndented)
        {
            return ObjectToJson(o, needIndented, false);
        }

        /// <summary>
        /// 将. net对象序列化成JSON格式字符串(可设置是否缩进，可设置是否忽略null值)
        /// </summary>
        /// <typeparam name="T">.net对象类型</typeparam> <param name="o">.net对象实例</param>
        /// <param name="needIndented">JSON内容是否需要缩进</param>
        /// <param name="ignoreNullItem">生成JSON时，NULL值项是否忽略</param> <returns>返回JSON格式字符串</returns>
        public static string ObjectToJson(object o, bool needIndented, bool ignoreNullItem)
        {
            if (o == null)
                throw new Exception("对象不能为空", new ArgumentNullException("o"));
            string strJson;
            if (!ignoreNullItem)
            {
                if (!needIndented)
                    strJson = JsonConvert.SerializeObject(o);
                else
                    strJson = JsonConvert.SerializeObject(o, Newtonsoft.Json.Formatting.Indented);
            }
            else
            {
                Newtonsoft.Json.Formatting format = Newtonsoft.Json.Formatting.None;
                if (needIndented)
                    format = Newtonsoft.Json.Formatting.Indented;
                strJson = JsonConvert.SerializeObject(o, format, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            return strJson;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="obj"></param> <returns></returns>
        //public static string ObjectToJson(object obj)
        //{
        //    //using (var ms = new MemoryStream())
        //    //{
        //    //    var s = new DataContractJsonSerializer(obj.GetType());
        //    //    s.WriteObject(ms, obj);

        //    //    ms.Seek(0, SeekOrigin.Begin);

        //    //    return Encoding.UTF8.GetString(ms.ToArray());
        //    //}

        //    StringBuilder sb = new StringBuilder();
        //    using (StringWriter sw = new StringWriter(sb))
        //    {
        //        JsonSerializer json = new JsonSerializer();
        //        json.Serialize(new JsonTextWriter(sw), obj);
        //        return sb.ToString();
        //    }
        //}

        #endregion ObjectToJson

        #region XmlToJson

        /// <summary>
        /// 将XML文件内容序列化成JSON格式字符串
        /// </summary>
        /// <param name="xml">Xml</param> <returns>返回JSON格式字符串</returns>
        public static string XmlFileToJson(string xml)
        {
            return XmlToJson(xml, false, true);
        }

        /// <summary>
        /// 将XML内容序列化成JSON格式字符串
        /// </summary>
        /// <param name="xml">Xml</param> <returns>返回JSON格式字符串</returns>
        public static string XmlToJson(string xml)
        {
            return XmlToJson(xml, false, false);
        }

        /// <summary>
        /// 将XML/文件内容序列化成JSON格式字符串
        /// </summary>
        /// <param name="xml">Xml</param> <param name="needIndented">JSON是否需要缩进</param>
        /// <param name="file">Is File</param> <returns>返回JSON格式字符串</returns>
        public static string XmlToJson(string xml, bool needIndented, bool file)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xmlFilePath");
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                if (file)
                {
                    if (!File.Exists(xml))
                    {
                        throw new FileNotFoundException();
                    }
                    doc.Load(xml);
                }
                else
                {
                    doc.LoadXml(xml);
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            string jsonString;
            if (needIndented)
                jsonString = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
            else
                jsonString = JsonConvert.SerializeXmlNode(doc);

            return jsonString;
        }

        #endregion XmlToJson

        #region JsonToXml

        /// <summary>
        /// 将JSON内容反序列为XML Document
        /// </summary>
        /// <param name="jsonString">JSON格式字符串</param>
        /// <returns>返回 XML Document</returns>
        public static XmlDocument JsonToXml(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                throw new ArgumentNullException("jsonString");

            return JsonConvert.DeserializeXmlNode(jsonString);
        }

        #endregion JsonToXml

        #region DataTable DataReader To Json

        /// <summary>
        /// 将DataReader转换为Json格式字符串
        /// </summary>
        /// <param name="dataReader">DataReader对象</param> <returns>返回Json格式字符串</returns>
        public static string DataReaderToJson(IDataReader dataReader)
        {
            if (dataReader == null)
            {
                throw new ArgumentNullException("dataReader");
            }

            try
            {
                StringBuilder jsonString = new StringBuilder();
                jsonString.Append("[");
                Type type;
                string strKey = null;
                string strValue = null;
                while (dataReader.Read())
                {
                    jsonString.Append("{");
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        type = dataReader.GetFieldType(i);
                        strKey = dataReader.GetName(i);
                        strValue = dataReader[i].ToString();
                        jsonString.Append("\"" + strKey + "\":");
                        strValue = JsonStringFormat(strValue, type);
                        if (i < dataReader.FieldCount - 1)
                        {
                            jsonString.Append(strValue + ",");
                        }
                        else
                        {
                            jsonString.Append(strValue);
                        }
                    }
                    jsonString.Append("},");
                }

                jsonString.Remove(jsonString.Length - 1, 1);
                jsonString.Append("]");
                if (jsonString.Length == 1)
                {
                    return "[]";
                }
                return jsonString.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Dispose();
                    dataReader = null;
                }
            }
        }

        /// <summary>
        /// 将DataSet转换为Json格式字符串
        /// </summary>
        /// <param name="dataSet">DataSet对象</param> <returns>返回Json格式字符串</returns>
        public static string DataSetToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + DataTableToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }

        /// <summary>
        /// 将DataTable转成Json格式字符串(带有json name)
        /// </summary>
        /// <param name="jsonName">多数为datatable name</param> <param name="dt">DataTable对象</param>
        /// <returns>返回Json格式字符串</returns>
        public static string DataTableoJson(DataTable dt, string jsonName)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }

            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            Type type;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + JsonStringFormat(dt.Rows[i][j] is DBNull ? string.Empty : dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>
        /// 将Datatable转换为Json格式字符串
        /// </summary>
        /// <param name="dt">DataTable对象</param> <returns><para>返回Json格式字符串。</para></returns>
        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null)
            {
                throw new ArgumentNullException("dt");
            }

            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            Type type;
            string strKey = string.Empty;
            string strValue = string.Empty;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strKey = dt.Columns[j].ColumnName;
                    strValue = drc[i][j].ToString();
                    type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = JsonStringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            if (jsonString.Length == 1)
            {
                return "[]";
            }
            return jsonString.ToString();
        }

        /// <summary>
        /// 将Json格式内容转化成DateTable对象
        /// </summary>
        /// <remarks>
        /// JSON数据格式如下:
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}
        /// </remarks>
        /// <param name="jsonString">Json格式字符串</param>
        /// <example>
        /// 本示例说明如何将JSON内容转化成DataTable对象？
        /// <code lang="CS" title="JsonToDatatableManager.JsonToDatatable"> string jsonString =
        /// "[{\"Author\":\"Jim
        /// Bob\",\"Id\":1,\"Price\":13.5,\"Genre\":\"Comedy\"},{\"Author\":\"John
        /// Fox\",\"Id\":2,\"Price\":8.5,\"Genre\":\"Drama\"},{\"Director\":\"Phil
        /// Funk\",\"Id\":1,\"Price\":22.99,\"Genre\":\"Comedy\"},{\"Director\":\"Eddie
        /// Jones\",\"Id\":1,\"Price\":13.4,\"Genre\":\"Action\"}]"; DataTable dt =
        /// JsonToDatatableManager. JsonToDatatable(jsonString); </code>
        /// </example>
        /// <returns>DataTable对象</returns>
        public static DataTable JsonToDatatable(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new ArgumentNullException("jsonString");
            }

            //取出表名
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(jsonString).Value;
            DataTable tb = null;
            //去除表名
            jsonString = jsonString.Substring(jsonString.IndexOf("[") + 1);
            jsonString = jsonString.Substring(0, jsonString.IndexOf("]"));

            //获取数据
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(jsonString);
            string strRow = null;
            string[] strRows = null;
            DataColumn dc = null;
            string[] strCell = null;
            DataRow dr = null;
            for (int i = 0; i < mc.Count; i++)
            {
                strRow = mc[i].Value;
                strRows = strRow.Split(',');

                //创建表
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        dc = new DataColumn();
                        strCell = str.Split(':');
                        dc.ColumnName = strCell[0];
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容
                dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        #endregion DataTable DataReader To Json

        #region JsonUtil

        /// <summary>
        /// 对于JS要求JSON带有callback的处理
        /// </summary>
        /// <param name="callback">js前端的callback名称</param> <param name="jsonString">JSON字符串</param>
        /// <returns><para>给JSON格式字符串加JS callback名称。</para> <para>格式为： callback(jsonString)</para> <para>举例：jsonp7pggos({"callback":"jsonp7pggos","loginstatus":"1","usermasterid":"2691319","usermastername":"","defaultkey":"","paravalue":null})
        /// </para></returns>
        public static string AddJSCallbackJson(string callback, string jsonString)
        {
            if (string.IsNullOrEmpty(callback))
                throw new ArgumentNullException("callback");
            if (string.IsNullOrEmpty(jsonString))
                throw new ArgumentNullException("jsonString");

            return string.Format("{0}({1})", callback, jsonString);
        }

        /// <summary>
        /// 从JSON数组中获取某一个Key的一组值
        /// </summary>
        /// <example>
        /// string jsonData = "{\"addr\":[{\"city\":\"guangzhou\",\"province\":\"guangdong\"},{\"city\":\"guiyang\",\"province\":\"guizhou\"}]}";
        /// IList<string> arrValue = JsonUtil.FindSingleValueFromJsonArray(jsonData, "addr", "city");
        /// </example>
        /// <param name="jsonSring">JSON格式字符串 string jsonData = "{\"addr\":[{\"city\":\"guangzhou\",\"province\":\"guangdong\"},{\"city\":\"guiyang\",\"province\":\"guizhou\"}]}";</param>
        /// <param name="arrayKey">JSON数组名称</param> <param name="key">JSON数组中某一对象的Key名称</param>
        /// <returns>返回IList&lt;string&gt;对象</returns>
        public static IList<string> FindSingleValueFromJsonArray(string jsonSring, string arrayKey, string key)
        {
            try
            {
                JObject obj = JObject.Parse(jsonSring);
                JArray arrObj = JArray.Parse(obj[arrayKey].ToString());
                IList<string> arrRet = new List<string>();
                JObject objItem;
                foreach (JToken item in arrObj)
                {
                    objItem = JObject.Parse(item.ToString());
                    arrRet.Add(objItem[key].ToString());
                }
                return arrRet;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 从JSON对象中获取某个Key的值
        /// </summary>
        /// <example>
        /// <code> string jsonData =
        /// "{\"name\":\"lily\",\"age\":23,\"addr\":{\"city\":\"guangzhou\",\"province\":\"guangdong\"}}";
        /// //获取节点"province"的值 string strValue = JsonUtil.FindSingleValueFromJsonObject(jsonData,
        /// "province"); </code>
        /// </example>
        /// <param name="jsonSring">JSON格式字符串 形式如：string jsonData = "{\"name\":\"lily\",\"age\":23,\"addr\":{\"city\":\"guangzhou\",\"province\":\"guangdong\"}}";</param>
        /// <param name="key">JSON中某个Key名称</param> <returns>返回JSON格式字符串指定Key的值</returns>
        public static string FindSingleValueFromJsonObject(string jsonSring, string key)
        {
            try
            {
                JObject obj = JObject.Parse(jsonSring);
                return GetJsonValue(obj.Children(), key);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 过滤值字符串中的特殊字符
        /// </summary>
        /// <remarks>
        /// <c> switch (c) { case '\"':
        /// sb. Append("\\\""); break; case '\\':
        /// sb. Append("\\\\"); break; case '/':
        /// sb. Append("\\/"); break; case '\b':
        /// sb. Append("\\b"); break; case '\f':
        /// sb. Append("\\f"); break; case '\n':
        /// sb. Append("\\n"); break; case '\r':
        /// sb. Append("\\r"); break; case '\t':
        /// sb. Append("\\t"); break; case '\v':
        /// sb. Append("\\v"); break; case '\0':
        /// sb. Append("\\0"); break;
        /// default:
        /// sb. Append(c); break; }
        /// </c>
        /// </remarks>
        /// <param name="s">值内容</param> <returns>返回过滤后的字符串</returns>
        public static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    case '\v':
                        sb.Append("\\v"); break;
                    case '\0':
                        sb.Append("\\0"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 格式化字符型、日期型、布尔型
        /// </summary>
        /// <param name="str">字符串</param> <param name="type">该字符串内容类型</param>
        /// <returns>返回前后带双引号的值</returns>
        public static string JsonStringFormat(string str, Type type)
        {
            if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type == typeof(byte[]))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(Guid))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        //public static void GetJsonValue(JEnumerable<JToken> jToken)
        //{
        //    IEnumerator er = jToken.GetEnumerator();
        //    while (er.MoveNext())
        //    {
        //        JToken jc = er.Current as JToken;
        //        if (jc is JValue)
        //        {
        //            string sValue = jc.ToString();
        //        }
        //        else if (jc is JObject || ((JProperty)jc).Value is JObject)
        //        {
        //            GetJsonValue(jc.Children());
        //        }
        //        //else if (jc is JContainer || ((JProperty)jc).Value is JContainer)
        //        //{
        //        //    GetJsonValue(jc.Children());
        //        //}
        //        else if (jc is JArray || ((JProperty)jc).Value is JArray)
        //        {
        //        }
        //        else
        //        {
        //            string sValue = ((JProperty)jc).Value.ToString();
        //        }
        //    }
        //}

        private static string GetJsonValue(JEnumerable<JToken> jToken, string key)
        {
            IEnumerator enumerator = jToken.GetEnumerator();
            while (enumerator.MoveNext())
            {
                JToken jc = (JToken)enumerator.Current;

                if (jc is JObject || ((JProperty)jc).Value is JObject)
                {
                    return GetJsonValue(jc.Children(), key);
                }
                else
                {
                    if (((JProperty)jc).Name == key)
                    {
                        return ((JProperty)jc).Value.ToString();
                    }
                }
            }
            return null;
        }

        #endregion JsonUtil
    }
}