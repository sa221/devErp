using System;
using System.Web.UI;
using NipaRMGManagement.Others;

namespace DevERP.Account
{
    public partial class Register : Page
    {
        readonly AccountManager _accountManager = new AccountManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCapctha();
            }
            
        }
        protected void OnClick(object sender, EventArgs e)
        {
            UserInfo userInfo = CreateNewUser();
            if (userInfo!=null)
            {
                switch (_accountManager.Registration(userInfo))
                {
                    case Provider.Status.Failed:
                        successMessage.InnerHtml = Provider.GetErrorMassage("Registration Failed");
                        break;
                    case Provider.Status.Success:
                        successMessage.InnerHtml = Provider.GetSuccessMassage("sucessfully inserted");
                        ClearAllErrorMessage();
                        Provider.ClearTextBoxes(this);
                        break;
                    case Provider.Status.ExistEmail:
                        successMessage.InnerHtml = Provider.GetErrorMassage("This Email Already Registered");
                        break;
                    case Provider.Status.ExistUser:
                        successMessage.InnerHtml = Provider.GetErrorMassage("This User Name Already Registered");
                        break;
                    case Provider.Status.Others:
                        successMessage.InnerHtml = Provider.GetErrorMassage("Unknown Error Occured");
                        break;
                    default:
                        successMessage.InnerHtml = Provider.GetErrorMassage("Unknown Error Occured");
                        break;
                }
            }
        }

        private UserInfo CreateNewUser()
        {
            UserInfo userInfo = new UserInfo();
            if (!string.IsNullOrWhiteSpace(nameTextBox.Value))
            {
                userInfo.Name = nameTextBox.Value;
                if (!string.IsNullOrWhiteSpace(emailTextBox.Value))
                {
                    userInfo.Email = emailTextBox.Value;
                    if (!string.IsNullOrWhiteSpace(userNameTextBox.Value))
                    {
                        userInfo.UserName = userNameTextBox.Value;
                        if (!string.IsNullOrWhiteSpace(passwordTextBox.Value))
                        {
                            if (passwordTextBox.Value.Equals(confirmPasswordTextBox.Value))
                            {
                                userInfo.Password = passwordTextBox.Value;
                                if (ValidateCaptcha())
                                {
                                    return userInfo;
                                }
                                captchaTextBoxError.InnerHtml = Provider.GetErrorMassage("Captcha donot match");
                                return null;
                            }
                            confirmPasswordTextBoxError.InnerHtml = Provider.GetErrorMassage("password donot match");
                            return null;
                        }
                        passwordTextBoxError.InnerHtml = Provider.GetErrorMassage("Password cannot be blanked");
                        return null;
                    }
                    userNameTextBoxError.InnerHtml = Provider.GetErrorMassage("UserName cannot be blanked");
                    return null;
                }
                emailTextBoxError.InnerHtml = Provider.GetErrorMassage("Email cannot be blanked");
                return null;
            }
            nameTextBoxError.InnerHtml = Provider.GetErrorMassage("Name cannot be blanked");
            return null;
        }

        protected void btnRefresh_OnClick(object sender, EventArgs e)
        {
            ShowCapctha();
            captchaTextBoxError.InnerHtml = string.Empty;
        }
        private void ShowCapctha()
        {
            Session["captcha"] = Provider.GetRandomString(6);
            imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks;
        }

        private bool ValidateCaptcha()
        {
            try
            {
                return Session["captcha"].ToString() == captchaTextBox.Value;
            }
            finally
            {
                ShowCapctha();
            }
        }

        private void ClearAllErrorMessage()
        {
            nameTextBoxError.InnerHtml = string.Empty;
            emailTextBoxError.InnerHtml = string.Empty;
            userNameTextBoxError.InnerHtml = string.Empty;
            passwordTextBoxError.InnerHtml = string.Empty;
            confirmPasswordTextBoxError.InnerHtml = string.Empty;
            captchaTextBoxError.InnerHtml = string.Empty;

        }
    }
}