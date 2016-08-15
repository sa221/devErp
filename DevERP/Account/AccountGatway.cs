using System;
using System.Data;
using DevERP.Others;

namespace DevERP.Account

{
    public class AccountGatway : ConnectionGateway
    {
        public int GetLoginId(LoginInfo loginInfo)
        {
            Query = "select loginId from Login where userName=@userName and password=@password and status = 'v'";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@userName", loginInfo.UserName);
            Command.Parameters.AddWithValue("@password", loginInfo.Password);
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    return Reader["loginId"]!=DBNull.Value? Convert.ToInt32(Reader["loginId"].ToString()):0;
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                CloseAllConnection();
            }
            
        }

        public UserInfo GetUserInfo(int loginId)
        {
            Query = "UserAllInformation";
            PrepareCommand(CommandType.StoredProcedure);
            Command.Parameters.AddWithValue("@loginId", loginId);
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                UserInfo userInfo = new UserInfo();
                while (Reader.Read())
                {
                    userInfo.UserInfoId = Convert.ToInt32(Reader["userinfoId"].ToString());
                    userInfo.LoginId = Convert.ToInt32(Reader["loginId"].ToString());
                    userInfo.UserName = Reader["userName"] != DBNull.Value ? Reader["userName"].ToString() : string.Empty;
                    userInfo.Password = Reader["password"] != DBNull.Value ? Reader["password"].ToString() : string.Empty;
                    userInfo.Name = Reader["name"] != DBNull.Value ? Reader["name"].ToString() : string.Empty;
                    userInfo.Image = Reader["image"] != DBNull.Value ? Reader["image"].ToString() : string.Empty;
                    userInfo.Email = Reader["email"] != DBNull.Value ? Reader["email"].ToString() : string.Empty;
                    userInfo.Phone = Reader["phone"] != DBNull.Value ? Reader["phone"].ToString() : string.Empty;
                    userInfo.Designation = Reader["designation"] != DBNull.Value ? Reader["designation"].ToString() : string.Empty;
                }
                return userInfo;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseAllConnection();
            }
            
        }
        //registration

        public int InsertUserInformation(UserInfo userInfo, int loginId)
        {
            Query = "INSERT INTO UserInfo (name,email,loginId) VALUES (@name,@email,@loginId)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@name", userInfo.Name);
            Command.Parameters.AddWithValue("@email", userInfo.Email);
            Command.Parameters.AddWithValue("@loginId", loginId);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                CloseAllConnection();
            }
        }

        public int InsertLoginInformation(UserInfo userInfo)
        {
            Query = "INSERT INTO Login(userName,password)  VALUES (@userName,@password); select SCOPE_IDENTITY() as ID;";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@userName", userInfo.UserName);
            Command.Parameters.AddWithValue("@password", userInfo.Password);
            Connection.Open();
            try
            {
                return Convert.ToInt32(Command.ExecuteScalar().ToString());
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public int DeleteLoginInformation(int loginId)
        {
            Query = "delete from Login where loginId=@loginId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@loginId", loginId);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                CloseAllConnection();
            }
            
        }

        public bool IsExistEmail(string email)
        {
            Query = "select * from UserInfo where email=@email";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@email", email);
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                return Reader.Read();
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
              CloseAllConnection();  
            }
        }

        public bool IsExistUserName(string userName)
        {
            Query = "select * from Login where userName=@userName";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@userName", userName);
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                return Reader.Read();
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                CloseAllConnection();
            }
        }
    }
}