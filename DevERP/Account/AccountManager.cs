using DevERP.Others;

namespace DevERP.Account
{
    public class AccountManager
    {
        private readonly AccountGatway _accountGatway= new AccountGatway();
        private int _loginId;
        public Provider.Status CheckLogin(LoginInfo loginInfo)
        {
            _loginId = _accountGatway.GetLoginId(loginInfo);
            if (_loginId > 0)
            {
                return Provider.Status.Success;
            }
            return Provider.Status.Failed;
        }

        public int GetLoginId(LoginInfo loginInfo)
        {
            return _accountGatway.GetLoginId(loginInfo);
        }
        public UserInfo GetUserInfo(int loginId)
        {
            return _accountGatway.GetUserInfo(loginId);
        }
        public Provider.Status Registration(UserInfo userInfo)
        {
            if (!_accountGatway.IsExistEmail(userInfo.Email))
            {
                if (!_accountGatway.IsExistUserName(userInfo.UserName))
                {
                    int loginId = _accountGatway.InsertLoginInformation(userInfo);
                    if (loginId > 0)
                    {
                        int rowAffected = _accountGatway.InsertUserInformation(userInfo, loginId);
                        if (rowAffected > 0)
                        {
                            return Provider.Status.Success;
                        }
                        rowAffected =_accountGatway.DeleteLoginInformation(loginId);
                        if (rowAffected>0)
                        {
                            return Provider.Status.Failed;
                        }
                        return Provider.Status.Others;
                    }
                    return Provider.Status.Failed;
                }
                return Provider.Status.ExistUser;
            }
            return Provider.Status.ExistEmail;
        }
    }
}