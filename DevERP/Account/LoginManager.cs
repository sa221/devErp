using AbcoItAccountingApplication.DAL.Login;
using AbcoItAccountingApplication.Model.Register;

namespace AbcoItAccountingApplication.BLL.Login
{
    public class LoginManager
    {
        private readonly LoginGatway _loginGatway= new LoginGatway();
        private UserInfo _userInfo;
        private int loginId = 0;
        public string CheckLogin(Model.Login.Login login)
        {
            string message = "";
            loginId = _loginGatway.CheckLogin(login);
            if (loginId > 0)
            {
                _userInfo = _loginGatway.GetUserInfo(loginId);
            }
            else
            {
                message = "Invalid User Name Or Password";
            }
            return message;
        }

        public int GetLoginId()
        {
            return loginId;
        }
        public UserInfo GetUserInfo(int loginId)
        {
            _userInfo = _loginGatway.GetUserInfo(loginId);
            return _userInfo;
        }
    }
}