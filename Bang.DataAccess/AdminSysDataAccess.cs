using Bang.Models;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace Bang.DataAccess
{
    public class AdminSysDataAccess
    {
        public static LoginInfoModel VerifyUserLogin(string userName, string password)
        {
            #region
            #region
            var sqlString = "select * from CSR where CSRYGID=:UserName and CSRPWD=:password and islock='1'";//
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
                    Name = reader.GetString(reader.GetOrdinal("CSRNAME")),
                    UserName = reader.GetString(reader.GetOrdinal("CSRYGID")),
                    Status = Int16.Parse(reader["CSRSTATUS"].ToString())
                };
            }
            else
            {
                return null;
            }
            #endregion
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
            if (city != "-1")
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

            //return result;

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

        public static List<CompanyEmployeeRecommendModel> GetEmployeeRecommendList(string city, string year, string month, string companyCode, string empAccount, int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            pageIndex = pageIndex - 1;
            recordCount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;
            var startDate = new DateTime(int.Parse(year), int.Parse(month), 1);
            var endDate = startDate.AddMonths(1);

            var result = new List<CompanyEmployeeRecommendModel>();
            var sqlString = "select rownum as rn, shifu_phone, shifu_name, recommed_count from recommend_summary where 1=1 ";
            var sqlPageString = "select * from ({table})m where rn >=" + startIndex + " and rn <=" + endIndex;
            var sqlCountString = "select count(*) rcount from recommend_summary where 1=1 ";

            sqlString += " and create_date between to_date('" + startDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and to_date('" + endDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') ";
            sqlCountString += " and create_date between to_date('" + startDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') and to_date('" + endDate.ToString("yyyy-MM-dd") + "','yyyy-mm-dd') ";
            if (string.IsNullOrEmpty(companyCode) == false)
            {
                sqlString += " and shifu_code in (select shifu_code from shifu_details where company_code='" + companyCode + "')";
                sqlCountString += " and shifu_code in (select shifu_code from shifu_details where company_code='" + companyCode + "')";
            }

            if (string.IsNullOrEmpty(empAccount) == false)
            {
                sqlString += "  and shifu_code ='" + empAccount + "'";
                sqlCountString += "  and shifu_code ='" + empAccount + "'";
            }

            sqlPageString = sqlPageString.Replace("{table}", sqlString);

            //return result;

            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlPageString);
            var count = OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString);
            recordCount = int.Parse(count.ToString());

            while (reader.Read())
            {
                var emp = new CompanyEmployeeRecommendModel
                {
                    AccountId = reader["shifu_phone"].ToString(),
                    Name = reader["shifu_name"].ToString(),
                    Amount = int.Parse(reader["recommed_count"].ToString())
                };
                result.Add(emp);
            }
            return result;
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
            if (city != "-1")
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
            //return result;

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

        public static List<OrderModel> OrderQuery(string city, string startDate, string endDate, string empAccount, string orderStatus, string serviceType, int pageIndex, int pageSize, out int recordCount)
        {
            #region
            recordCount = 0;
            pageIndex = pageIndex - 1;
            recordCount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;

            var result = new List<OrderModel>();
            var sqlString = "select rownum as rn, o.order_number,o.order_time,o.order_status,o.release_status,o.money_status,c.category_value,o.pay_total,r.shifu_phone,o.c_phone from order_info o , order_rob_shifu r , dic_category c where  c.category_code=o.category_code and r.order_number=o.order_number and r.rob_status in ('80','90') ";
            var sqlPageString = "select * from ({table})m where rn >=" + startIndex + " and rn <=" + endIndex;
            var sqlCountString = "select count(*) rCount from order_info o , order_rob_shifu r , dic_category c where  c.category_code=o.category_code and r.order_number=o.order_number and r.rob_status in ('80','90') ";
            // 
            //and r.shifu_phone='18611102971'
            //and o.category_code=''
            //and o.city_code=''
            //and o.order_time between and 

            if (city != "-1")
            {
                sqlString += " and o.city_code='" + city + "' ";
                sqlCountString += " and o.city_code='" + city + "' ";
            }

            if (string.IsNullOrEmpty(startDate) == false)
            {
                sqlString += " and o.order_time>= to_date('" + startDate + "','yyyy-mm-dd')";
                sqlCountString += " and o.order_time>= to_date('" + startDate + "','yyyy-mm-dd')";
            }
            if (string.IsNullOrEmpty(endDate) == false)
            {
                sqlString += " and o.order_time<= to_date('" + endDate + "','yyyy-mm-dd')";
                sqlCountString += " and o.order_time<= to_date('" + endDate + "','yyyy-mm-dd')";
            }
            if (string.IsNullOrEmpty(empAccount) == false)
            {
                sqlString += " and r.shifu_phone='" + empAccount + "' ";
                sqlCountString += " and r.shifu_phone='" + empAccount + "' ";
            }
            if (orderStatus != "-1")
            {//order_status release_status money_status
                /*
                   order_status
                       15：等待支付
                       20，25：服务中
                       90:服务已完成
                   release_status
                       5:已取消
                   money_status
                       -10,-15:待退款
                */
                if (orderStatus == "1" || orderStatus == "2" || orderStatus == "3")
                {
                    sqlString += " and o.order_status='" + orderStatus + "'";
                    sqlCountString += " and o.order_status='" + orderStatus + "'";
                }
                else if (orderStatus == "4")
                {
                    sqlString += " and o.release_status='" + orderStatus + "'";
                    sqlCountString += " and o.release_status='" + orderStatus + "'";
                }
                else
                {//5
                    sqlString += " and o.money_status='" + orderStatus + "'";
                    sqlCountString += " and o.money_status='" + orderStatus + "'";
                }

            }
            if (serviceType != "-1")
            {
                sqlString += " and o.category_code='" + serviceType + "' ";
                sqlCountString += " and o.category_code='" + serviceType + "' ";
            }

            sqlPageString = sqlPageString.Replace("{table}", sqlString);
            //return result;

            recordCount = Convert.ToInt32(OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString));
            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlPageString);
            while (reader.Read())
            {
                var emp = new OrderModel
                {
                    OrderId = reader["order_number"].ToString(),
                    TradeDate = reader["order_time"].ToString(),
                    Status = reader["order_status"].ToString(),
                    ReleaseStatus = reader["release_status"].ToString(),
                    MoneyStatus = reader["money_status"].ToString(),
                    ServiceType = reader["category_value"].ToString(),
                    Total = decimal.Parse(reader["pay_total"].ToString()),
                    CompanyEmpAccount = reader["shifu_phone"].ToString(),
                    Customer = reader["c_phone"].ToString()
                };
                result.Add(emp);
            }

            return result;
            #endregion
        }

        public static List<TradeModel> OrderTradeQuery(string city, string startDate, string endDate, string orderNum, string bankSerialNumber, string tradeStatus, string tradeOrg, int pageIndex, int pageSize, out int recordCount, out decimal total, out int amount)
        {
            #region
            recordCount = 0;
            pageIndex = pageIndex - 1;
            recordCount = 0;
            total = 0;
            amount = 0;
            var startIndex = pageIndex * pageSize + 1;
            var endIndex = startIndex + pageSize - 1;
            var result = new List<TradeModel>();

            var sqlString = "select rownum as rn, p.ORDER_NUMBER,TRANSACTION_ID,o.order_time,PAYMENT_TYPE,PAY_MONEY,PAYMENT_STATUS from payment_info p left join order_info o on o.order_number=p.order_number where 1=1 ";
            var sqlPageString = "select * from ({table})m where rn >=" + startIndex + " and rn <=" + endIndex;
            var sqlCountString = "select count(*) from payment_info p left join order_info o on o.order_number=p.order_number where 1=1 ";
            var sqlTotalString = "select sum(PAY_MONEY) as total,count(*) as amount  from payment_info p left join order_info o on o.order_number=p.order_number where 1=1 ";

            if (string.IsNullOrEmpty(startDate) == false)
            {
                sqlString += " and END_TIME >= to_date('" + startDate + "','yyyy-mm-dd')";
                sqlCountString += " and END_TIME >= to_date('" + startDate + "','yyyy-mm-dd')";
                sqlTotalString += " and END_TIME >= to_date('" + startDate + "','yyyy-mm-dd')";
            }
            if (string.IsNullOrEmpty(endDate) == false)
            {
                sqlString += " and END_TIME <= to_date('" + endDate + "','yyyy-mm-dd')";
                sqlCountString += " and END_TIME <= to_date('" + endDate + "','yyyy-mm-dd')";
                sqlTotalString += " and END_TIME <= to_date('" + endDate + "','yyyy-mm-dd')";
            }

            if (string.IsNullOrEmpty(orderNum) == false)
            {
                sqlString += " and ORDER_NUMBER='" + orderNum + "'";
                sqlCountString += " and ORDER_NUMBER='" + orderNum + "'";
                sqlTotalString += " and ORDER_NUMBER='" + orderNum + "'";
            }

            if (string.IsNullOrEmpty(bankSerialNumber) == false)
            {
                sqlString += " and TRANSACTION_ID='" + bankSerialNumber + "'";
                sqlCountString += " and TRANSACTION_ID='" + bankSerialNumber + "'";
                sqlTotalString += " and TRANSACTION_ID='" + bankSerialNumber + "'";
            }

            if (tradeOrg != "-1")
            {
                sqlString += " and PAYMENT_TYPE='" + tradeOrg + "'";
                sqlCountString += " and PAYMENT_TYPE='" + tradeOrg + "'";
                sqlTotalString += " and PAYMENT_TYPE='" + tradeOrg + "'";
            }

            if (tradeStatus != "-1")
            {
                sqlString += " and PAYMENT_STATUS='" + tradeStatus + "'";
                sqlCountString += " and PAYMENT_STATUS='" + tradeStatus + "'";
                sqlTotalString += " and PAYMENT_STATUS='" + tradeStatus + "'";
            }
            sqlPageString = sqlPageString.Replace("{table}", sqlString);

            recordCount = Convert.ToInt32(OracleHelper.ExecuteScalar(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlCountString));
            var totalReader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlTotalString);
            while (totalReader.Read())
            {
                total = decimal.Parse(totalReader["total"].ToString());
                amount = int.Parse(totalReader["amount"].ToString());
            }
            var reader = OracleHelper.ExecuteReader(OracleHelper.OracleConnString, System.Data.CommandType.Text, sqlPageString);
            while (reader.Read())
            {
                var emp = new TradeModel
                {
                    OrderId = reader["order_number"].ToString(),
                    BankSerialNumber = reader["TRANSACTION_ID"].ToString(),
                    PaymentStatus = reader["PAYMENT_STATUS"].ToString(),
                    OrderDate = string.IsNullOrEmpty(reader["order_time"].ToString()) ? DateTime.MinValue : DateTime.Parse(reader["order_time"].ToString()),
                    OrderTotal = decimal.Parse(reader["PAY_MONEY"].ToString()),
                    Poundage = 0,
                    TradeOrg = reader["PAYMENT_TYPE"].ToString()
                };
                result.Add(emp);
            }


            return result;
            #endregion
        }
    }
}
