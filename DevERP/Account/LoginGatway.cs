using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DevERP.Account

{
    public class LoginGatway
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        public int CheckLogin(LoginInfo login)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string query = "select loginId from Login_tbl where userName='" + login.UserName + "' and password= '" + login.Password + "' and status = 'v'";
            SqlCommand command = new SqlCommand(query, connection);
            int loginId = 0;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                loginId = Convert.ToInt32(reader["loginId"].ToString());
            }
            reader.Close();
            connection.Close();
            return loginId;
        }

        public UserInfo GetUserInfo(int loginId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            UserInfo userInfo = new UserInfo();

            //string query = "select * from Login_tbl as l join UserInfo_tbl as u on l.loginId=u.loginId where l.loginId =" + loginId;
            SqlCommand command = new SqlCommand("UserAllInformation", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@loginId", loginId);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                userInfo.UserInfoId = Convert.ToInt32(reader["userinfoId"].ToString());
                userInfo.LoginId = Convert.ToInt32(reader["loginId"].ToString());
                userInfo.UserName = reader["userName"].ToString();
                userInfo.Password = reader["password"].ToString();
                userInfo.FirstName = reader["fName"].ToString();
                userInfo.LastName = reader["lName"].ToString();
                userInfo.Image = reader["image"].ToString();
                userInfo.Email = reader["email"].ToString();
                userInfo.Phone = reader["phone"].ToString();
                userInfo.Designation = reader["designation"].ToString();
            }
            connection.Close();
            return userInfo;
        }
    }
}