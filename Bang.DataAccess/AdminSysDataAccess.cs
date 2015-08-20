using Bang.Models;
using System;
using System.Collections.Generic;

namespace Bang.DataAccess
{
    public class AdminSysDataAccess
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            #region
            return new LoginInfoModel
            {
                UserName = userName,
                Name = "运行平台",
                Status = 1
            };
            #endregion
        }

        public static List<CompanyEmployeeModel> GetCompanyEmpList(string city, string serviceType, string startDate, string endDate, string companyCode, string empAccount, int pageIndex, int pageSize, out int recordCount)
        {
            #region
            recordCount = 0;
            pageIndex = pageIndex - 1;
            recordCount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;

            var result = new List<CompanyEmployeeModel>();

            var sqlString = "select rownum as rn, r.shifu_phone,d.sf_real_name,r.login_status,d.shifu_level,r.shifu_reg_date from shifu_reg r,shifu_details d where r.shifu_code=d.shifu_code ";
            var sqlPageString = "select * from ({table})m where rn >=" + startIndex + " and rn <=" + endIndex;
            var sqlCountString = "select count(*) rcount from shifu_reg r,shifu_details d where r.shifu_code=d.shifu_code ";
            //  and r.company_code='' and r.shifu_phone='' and r.city_code='' and  r.shifu_code in (select shifu_code from shifu_category_price where category_code='')
            if (string.IsNullOrEmpty(city) == false)
            {
                sqlString += " and r.city_code='" + city + "' ";
                sqlCountString += " and r.city_code='" + city + "' ";
            }
            if (string.IsNullOrEmpty(serviceType) == false)
            {
                sqlString += "and  r.shifu_code in (select shifu_code from shifu_category_price where category_code='" + serviceType + "') ";
                sqlCountString += "and  r.shifu_code in (select shifu_code from shifu_category_price where category_code='" + serviceType + "') ";
            }
            if (string.IsNullOrEmpty(startDate) == false)
            {
                sqlString += " and r.shifu_reg_date >= to_date('" + startDate + "','yyyy-mm-dd') ";
                sqlCountString += " and r.shifu_reg_date >= to_date('" + startDate + "','yyyy-mm-dd') ";
            }
            if (string.IsNullOrEmpty(endDate) == false)
            {
                sqlString += " and r.shifu_reg_date <= to_date('" + endDate + "','yyyy-mm-dd') ";
                sqlCountString += " and r.shifu_reg_date <= to_date('" + endDate + "','yyyy-mm-dd') ";
            }
            if (string.IsNullOrEmpty(companyCode) == false)
            {
                sqlString += "  and r.company_code='" + companyCode + "' ";
                sqlCountString += "  and r.company_code='" + companyCode + "' ";
            }
            if (string.IsNullOrEmpty(empAccount) == false)
            {
                sqlString += " and r.shifu_phone='" + empAccount + "' ";
                sqlCountString += " and r.shifu_phone='" + empAccount + "' ";
            }

            sqlPageString = sqlPageString.Replace("{table}", sqlString);

            return result;

            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlPageString);
            var count = OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString);
            recordCount = int.Parse(count.ToString());

            while (reader.Read())
            {
                var emp = new CompanyEmployeeModel
                {
                    CompanyId = reader["company_code"].ToString(),
                    AccountId = reader["shifu_phone"].ToString(),
                    Name = reader["sf_real_name"].ToString(),
                    Status = reader["login_status"].ToString(),
                    CreditRating = reader["shifu_level"].ToString(),
                    RegDate = DateTime.Parse(reader["shifu_reg_date"].ToString())
                };
                result.Add(emp);
            }
            return result;
            #endregion
        }

        public static bool SetCompanyEmpAccountStatus(string empAccount, string status)
        {
            var sqlString = "update shifu_reg set login_status='" + (status == "1" ? "1" : "0") + "' where shifu_phone in('" + empAccount + "')";
            return OracleHelper.ExecuteNonQuery(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlString) > 0 ? true : false;
        }

        public static List<CustomerModel> GetCustomerList(string city, string startDate, string endDate, string mobile, int pageIndex, int pageSize, out int recordCount)
        {
            #region
            recordCount = 0;
            pageIndex = pageIndex - 1;
            recordCount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;

            var result = new List<CustomerModel>();

            var sqlString = "select rownum as rn, c.phone_no,i.user_name,c.create_date from client_reg c left join client_info i on i.client_code=c.client_code where 1=1 ";
            var sqlPageString = "select * from ({table})m where rn >=" + startIndex + " and rn <=" + endIndex;
            var sqlCountString = "select count(*) rCount from client_reg c left join client_info i on i.client_code=c.client_code where 1=1 ";
            //c.city_code='' and c.phone_no='' and c.create_date between to_date('','yyyy-mm-dd') and to_date('','yyyy-mm-dd')
            if (string.IsNullOrEmpty(city) == false)
            {
                sqlString += " and c.city_code='" + city + "'";
                sqlCountString += " and c.city_code='" + city + "'";
            }

            if (string.IsNullOrEmpty(startDate) == false)
            {
                sqlString += "  and c.create_date >= to_date('" + startDate + "','yyyy-mm-dd')";
                sqlCountString += "  and c.create_date >= to_date('" + startDate + "','yyyy-mm-dd')";
            }
            if (string.IsNullOrEmpty(endDate) == false)
            {
                sqlString += "  and c.create_date <= to_date('" + endDate + "','yyyy-mm-dd')";
                sqlCountString += "  and c.create_date <= to_date('" + endDate + "','yyyy-mm-dd')";
            }

            if (string.IsNullOrEmpty(mobile) == false)
            {
                sqlString += "  and c.phone_no='" + mobile + "'";
                sqlCountString += "  and c.phone_no='" + mobile + "'";
            }
            sqlPageString = sqlPageString.Replace("{table}", sqlString);
            return result;

            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlPageString);
            var count = OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString);
            recordCount = int.Parse(count.ToString());

            while (reader.Read())
            {
                var emp = new CustomerModel
                {
                    AccountId = reader["phone_no"].ToString(),
                    NickName = reader["user_name"].ToString(),
                    RegDate = DateTime.Parse(reader["create_date"].ToString())
                };
                result.Add(emp);
            }

            return result;
            #endregion
        }
    }
}
