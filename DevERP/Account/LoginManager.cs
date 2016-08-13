namespace DevERP.Account
{
    public class LoginManager
    {
        private readonly LoginGatway _loginGatway= new LoginGatway();
        private UserInfo _userInfo;
        private int _loginId;
        public string CheckLogin(LoginInfo login)
        {
            string message = "";
            _loginId = _loginGatway.CheckLogin(login);
            if (_loginId > 0)
            {
                _userInfo = _loginGatway.GetUserInfo(_loginId);
            }
            else
            {
                message = "Invalid User Name Or Password";
            }
            return message;
        }

        public int GetLoginId()
        {
            return _loginId;
        }
        public UserInfo GetUserInfo(int loginId)
        {
            _userInfo = _loginGatway.GetUserInfo(loginId);
            return _userInfo;
        }
    }
}