using Bang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace _54Bang.Web.Company
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void GoSqlErr(string Type, string Data)
        {
            string strMsg = "尝试非法字符注入，IP：" + this.Request.ServerVariables["REMOTE_ADDR"] + " 页面："
                          + this.Request.ServerVariables["URL"].ToLower() + " 方式：" + Type + " 数据：" + Data;
  
            this.Response.Write("<script language='javascript'>window.alert('请不要输入非法字符！');location='" + this.Request.ServerVariables["URL"] + "';</script>");
            this.Response.End();
        }
        

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string strUrl = Request.Url.ToString();

            System.Diagnostics.Debug.WriteLine("请求地址："+ Request.Url.ToString());

            if (!strUrl.Contains("umpay/ReceiveUMPayInfo.aspx"))
            {
                string strMsg = string.Empty;

                //遍历Post参数，隐藏域除外
                foreach (string str_t in this.Request.Form)
                {
                    if (str_t == "__VIEWSTATE" || str_t == "__EVENTVALIDATION") continue;

                    if (strUrl.ToLower().Contains("/gasagreement.aspx") || strUrl.ToLower().Contains("/bitoagreement.aspx"))
                    {
                        if (str_t.ToLower() == "hidcontent") continue;
                    }

                    if (SqlFilter.Filter(this.Request.Form[str_t].ToString()))
                    {
                        GoSqlErr("Post", this.Request.Form[str_t].ToString());
                    }
                }

                //遍历Get参数。
                foreach (string str_t in this.Request.QueryString)
                {
                    if (SqlFilter.Filter(this.Request.QueryString[str_t].ToString()))
                        GoSqlErr("Get", this.Request.QueryString[str_t].ToString());
                }
            }
        }


        
    }
}