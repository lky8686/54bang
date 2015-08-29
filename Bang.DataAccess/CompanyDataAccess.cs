using Bang.Models;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.DataAccess
{
    public class CompanyDataAccess
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            #region
            var sqlString = "select * from company_info where company_status=1 and company_login=:UserName and company_pwd=:password";//
            var userNameParam = OracleHelper.MakeParam("UserName", userName);
            var userPasswordParam = OracleHelper.MakeParam("Password", password);
            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlString, new OracleParameter[]{
            userNameParam, userPasswordParam
            });
            //var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlString, null);
            if (reader.Read())
            {
                return new LoginInfoModel
                {
                    CompanyId = reader.GetString(reader.GetOrdinal("Company_Code")),
                    CompanyName = reader.GetString(reader.GetOrdinal("Company_Name")),
                    UserName = reader.GetString(reader.GetOrdinal("company_login")),
                    Status = 1
                };
            }
            else
            {
                return null;
            }
            return new LoginInfoModel { Name = "a", UserName = "1", Status = 1, CompanyId = "11-22-33" };
            #endregion
        }

        public static List<CompanyEmployeeModel> GetEmpListBy(string companyCode, string empAccount, string status, string serviceType, int pageIndex, int pageSize, out int recordCount)
        {
            #region
            var result = new List<CompanyEmployeeModel>();
            pageIndex = pageIndex - 1;
            recordCount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;
            var paramList = new List<OracleParameter>();
           
            var sqlCountString = "select count(1) as myCount from shifu_reg where company_code= '" + companyCode + "'";//login_status='0' and shifu_code in (select shifu_code from shifu_category_price where category_code='01') and shifu_phone='18612920767'
            var sqlString = "select rownum as rn,r.company_code,s.shifu_phone,sf_real_name,r.login_status,shifu_level,r.shifu_reg_date from shifu_reg r left join shifu_details s on r.shifu_code=s.shifu_code where r.company_code='" + companyCode + "'";//
            if (string.IsNullOrEmpty(empAccount) == false)
            {
                #region
                //sqlCountString += " and shifu_phone='" + empAccount + "'";
                //sqlString += " and r.shifu_phone='" + empAccount + "'";
                sqlCountString += " and shifu_phone=:empAccount";
                sqlString += " and r.shifu_phone=:empAccount";
                paramList.Add(new OracleParameter { ParameterName = "empAccount", Value = empAccount });
                #endregion
            }

            if (status != "-1")
            {
                #region
                //sqlCountString += " and login_status='" + status + "'";
                //sqlString += " and login_status='" + status + "'";
                sqlCountString += " and login_status=:status";
                sqlString += " and login_status=:status";

                paramList.Add(new OracleParameter { ParameterName = "status", Value = status });
                #endregion
            }

            if (serviceType != "-1")
            {
                #region
                //sqlCountString += " and shifu_code in (select shifu_code from shifu_category_price where category_code='" + serviceType + "')";
                //sqlString += " and r.shifu_code in (select shifu_code from shifu_category_price where category_code='" + serviceType + "')";

                sqlCountString += " and shifu_code in (select shifu_code from shifu_category_price where category_code=:serviceType)";
                sqlString += " and r.shifu_code in (select shifu_code from shifu_category_price where category_code=:serviceType)";

                paramList.Add(new OracleParameter { ParameterName = "serviceType", Value = serviceType });
                #endregion
            }

            sqlString = "select * from (" + sqlString + ")  m where rn >=" + startIndex + " and rn <= " + endIndex;
            //return result;
            var paramArray = new OracleParameter[paramList.Count];
            paramList.CopyTo(paramArray, 0);
            recordCount = Convert.ToInt32(OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString, paramArray));
            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlString, paramArray);
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
            #endregion
            return result;
        }

        public static List<object> GetCustomerList(string companyId)
        {
            return new List<object>();
        }
    }
}
