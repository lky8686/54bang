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
            var sqlString = "select * from where company_status=1 and company_login=@UserName and company_pwd=@password";
            var userNameParam = OracleHelper.MakeParam("@UserName", userName);
            var userPasswordParam = OracleHelper.MakeParam("@Password", password);
            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlString, new OracleParameter[]{
            userNameParam, userPasswordParam
            });
            if (reader.NextResult())
            {
                return new LoginInfoModel
                {
                    CompanyId = reader.GetString(reader.GetOrdinal("Company_Code")),
                    CompanyName = reader.GetString(reader.GetOrdinal("Company_Name")),
                    UserName = reader.GetString(reader.GetOrdinal("company_login"))
                };
            }
            else
            {
                return null;
            }
        }

        public static List<object> GetEmpListBy(string companyId)
        {
            return new List<object>();
        }

        public static List<object> GetCustomerList(string companyId)
        {
            return new List<object>();
        }
    }
}
