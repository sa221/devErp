using System;
using DevERP.Account;

namespace DevERP.UI
{
    public partial class Profile : System.Web.UI.Page
    {
        UserInfo _userInfo = new UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userInfo"]!=null)
            {
                _userInfo = (UserInfo) Session["userInfo"];
                nameSpan.InnerText = _userInfo.Name;
                emailSpan.InnerText = _userInfo.Email;
                
            }
        }

        protected void nameSaveButton_OnClick(object sender, EventArgs e)
        {
            
        }

        protected void emailSaveButton_OnClick(object sender, EventArgs e)
        {
            
        }
    }
}