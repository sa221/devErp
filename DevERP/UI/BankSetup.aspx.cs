using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class BankSetup : System.Web.UI.Page
    {
        readonly BankManager _bankManager = new BankManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBank();
            }
            
        }

        private void BindBank()
        {
            List<Bank> banks = _bankManager.GetAllBank();
            BankGridView.DataSource = banks;
            BankGridView.DataBind();
        }
        protected void SaveBank_OnClick(object sender, EventArgs e)
        {
            if (_bankManager.InsertBank(bankName.Value))
            {
                BindBank();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Inserted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Insert Failed");
            }
        }

        protected void BankGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            BankGridView.EditIndex = e.NewEditIndex;
            BindBank();
        }

        protected void BankGridView_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Bank bank = new Bank();
            bank.BankId = Convert.ToInt32(((Label)BankGridView.Rows[e.RowIndex].FindControl("id")).Text);
            bank.BankName = ((TextBox)BankGridView.Rows[e.RowIndex].FindControl("bankNameTextBox")).Text;
            if (_bankManager.UpdateBank(bank))
            {
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Updated");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Update failed");
            }
            
            BankGridView.EditIndex = -1;
            BindBank();

        }

        protected void BankGridView_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            BankGridView.EditIndex = -1;
            BindBank();
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int bankId = Convert.ToInt32(lnkRemove.CommandArgument);
            if (_bankManager.DeleteBank(bankId))
            {
                BindBank();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Deleted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("This Bank already in used");
            }
        }
    }
}