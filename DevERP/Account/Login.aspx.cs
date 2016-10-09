using System;
using System.Web.UI;
using DevERP.Others;

namespace DevERP.Account
{
    public partial class Login : Page
    {
        readonly AccountManager _accountManager = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void OnClick(object sender, EventArgs e)
        {
            LoginInfo loginInfo = GetLoginInfo();
            if (loginInfo == null) return;
            if (_accountManager.CheckLogin(loginInfo).Equals(Provider.Status.Success))
            {
                int loginId=_accountManager.GetLoginId(loginInfo);
                UserInfo userInfo = _accountManager.GetUserInfo(loginId);
                if (userInfo!=null)
                {
                    Session["userInfo"] = userInfo;
                }
                Response.Redirect("~/");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Invalid UserName Or Password");
            }
        }

        private LoginInfo GetLoginInfo()
        {
            LoginInfo loginInfo = new LoginInfo();
            if (!string.IsNullOrWhiteSpace(userNameTextBox.Value))
            {
                loginInfo.UserName = userNameTextBox.Value;
                if (!string.IsNullOrWhiteSpace(passwordTextBox.Value))
                {
                    loginInfo.Password = passwordTextBox.Value;
                    return loginInfo;
                }
                passwordTextBoxError.InnerHtml = Provider.GetErrorMassage("Password cannot be blanked");
                return null;
            }
            userNameTextBoxError.InnerHtml = Provider.GetErrorMassage("UserName cannot be blanked");
            return null;
        }
    }
}