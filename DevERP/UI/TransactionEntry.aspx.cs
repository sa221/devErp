using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class TransactionEntry : System.Web.UI.Page
    {
        readonly ItemManager _itemManager = new ItemManager();
        readonly SubItemManager _subItemManager = new SubItemManager();
        readonly PartyManager _partyManager = new PartyManager();
        readonly BankManager _bankManager = new BankManager();
        readonly TransactionManager _transactionManager = new TransactionManager();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllDropdown();
                BindGridView();
            }
        }

        private void LoadAllDropdown()
        {
            BindItem();
            LoadSubitem();
            BindParty();
            LoadBank();

        }
        
        private void BindGridView()
        {
            List<Transaction> transactions = _transactionManager.GetAllTransaction();
            TransactionGridView.DataSource = transactions;
            TransactionGridView.DataBind();
            if (transactions.Count>0)
            {
                BindBalance();
                ChangeStatusColor();
            }
            
        }

        private void BindBalance()
        {
            bool isSuccess;
            string balance = _transactionManager.GetBalance(out isSuccess).ToString(CultureInfo.CurrentCulture);
            if (isSuccess)
            {
                ((Label) TransactionGridView.FooterRow.FindControl("balance")).Text = balance;
            }
            else
            {
                ((Label) TransactionGridView.FooterRow.FindControl("balance")).Text = "Error";
            }
        }

        private void BindItem()
        {
            itemNameDropDown.DataSource = _itemManager.GetAllItem();
            itemNameDropDown.DataTextField = "ItemName";
            itemNameDropDown.DataValueField = "ItemId";
            itemNameDropDown.DataBind();
            itemNameDropDown.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void BindSubItem(int itemId)
        {
            subItemNameDropDown.DataSource = _subItemManager.GetAllSubItem(itemId);
            subItemNameDropDown.DataTextField = "SubItemName";
            subItemNameDropDown.DataValueField = "SubItemId";
            subItemNameDropDown.DataBind();
            subItemNameDropDown.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void LoadSubitem()
        {
            int itemId = Convert.ToInt32(itemNameDropDown.SelectedValue);
            if (itemId > 0)
            {
                BindSubItem(itemId);
            }
            else
            {
                subItemNameDropDown.Items.Clear();
                subItemNameDropDown.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        private void BindParty()
        {
            partyDropDown.DataSource = _partyManager.GetAllParty();
            partyDropDown.DataTextField = "PartyName";
            partyDropDown.DataValueField = "PartyId";
            partyDropDown.DataBind();
            partyDropDown.Items.Insert(0, new ListItem("Select", "0"));
        }
        private void BindBank()
        {
            bankDropDown.DataSource = _bankManager.GetAllBank();
            bankDropDown.DataTextField = "BankName";
            bankDropDown.DataValueField = "BankId";
            bankDropDown.DataBind();
            bankDropDown.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void LoadBank()
        {
            if (TypeDropDown.SelectedValue.Equals("cheque"))
            {
                BindBank();
            }
            else
            {
                bankDropDown.Items.Clear();
                bankDropDown.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        protected void itemNameDropDown_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubitem();
        }

        protected void TypeDropDown_OnTextChanged(object sender, EventArgs e)
        {
            LoadBank();
        }

        protected void SaveTransaction_OnClick(object sender, EventArgs e)
        {
            Transaction transaction = GetTransactionModel();
            if (transaction.TransactionId>0)
            {
                successMessage.InnerHtml = _transactionManager.UpdateTransaction(transaction)
                    ? Provider.GetSuccessMassage("Sucsessfully Update")
                    : Provider.GetErrorMassage("Update failed");
            }
            else
            {
                successMessage.InnerHtml = _transactionManager.SaveTransaction(transaction)
                    ? Provider.GetSuccessMassage("Sucsessfully Insert")
                    : Provider.GetErrorMassage("Insert failed");
            }
            BindGridView();
            ChangeButtonText();
        }

        private void ChangeButtonText()
        {
            SaveTransaction.Text = "Save";
            transactionHidden.Value = string.Empty;
        }

        private Transaction GetTransactionModel()
        {
            Transaction transactionModel = new Transaction();
            if (!string.IsNullOrEmpty(transactionHidden.Value))
            {
                transactionModel.TransactionId = Convert.ToInt32(transactionHidden.Value);
            }
            transactionModel.TransactionDate = Provider.StringToDateTime(transactionDate.Value);
            transactionModel.ItemId = Convert.ToInt32(itemNameDropDown.SelectedValue);
            transactionModel.SubItemId = Convert.ToInt32(subItemNameDropDown.SelectedValue);
            transactionModel.Amount = Convert.ToDecimal(amount.Value);
            transactionModel.TransactionCatagory = CatagoryDropDown.SelectedValue;
            transactionModel.PartyId = Convert.ToInt32(partyDropDown.SelectedValue);
            transactionModel.TransactionType = TypeDropDown.SelectedValue;
            transactionModel.BankId = Convert.ToInt32(bankDropDown.SelectedValue);
            transactionModel.Remarks = remarks.Value;
            return transactionModel;

        }
        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int transactionId = Convert.ToInt32(lnkRemove.CommandArgument);
            transactionHidden.Value = transactionId.ToString();
            Transaction transaction = _transactionManager.GetAllTransaction().FirstOrDefault(x => x.TransactionId.Equals(transactionId));
            if (transaction != null)
            {
                transactionDate.Value = Provider.DateTimeToSting(transaction.TransactionDate);
                itemNameDropDown.SelectedValue = transaction.ItemId.ToString();
                LoadSubitem();
                subItemNameDropDown.SelectedValue = transaction.SubItemId.ToString();
                amount.Value = transaction.Amount.ToString(CultureInfo.CurrentCulture);
                CatagoryDropDown.SelectedValue = transaction.TransactionCatagory;
                partyDropDown.SelectedValue = transaction.PartyId.ToString();
                TypeDropDown.SelectedValue = transaction.TransactionType;
                LoadBank();
                bankDropDown.SelectedValue = transaction.BankId.ToString();
                remarks.Value = transaction.Remarks;
                SaveTransaction.Text = "Update";
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Something is Error");
            }
        }
        private void ChangeStatusColor()
        {
            foreach (GridViewRow gridViewRow in TransactionGridView.Rows)
            {
                string status = ((Label)gridViewRow.FindControl("status")).Text;
                if (status.Equals("Pending"))
                {
                    ((Label)gridViewRow.FindControl("status")).ForeColor = Color.BlueViolet;
                }
                else if (status.Equals("Pass"))
                {
                    ((Label)gridViewRow.FindControl("status")).ForeColor = Color.Green;
                }
                else if (status.Equals("Cancel"))
                {
                    ((Label)gridViewRow.FindControl("status")).ForeColor = Color.Red;
                }
                string catagory = ((Label)gridViewRow.FindControl("transactionCatagory")).Text;
                ((Label)gridViewRow.FindControl("transactionCatagory")).ForeColor = catagory.Equals("expence") ? Color.Red : Color.Green;
            }
        }
        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int transactionId = Convert.ToInt32(lnkRemove.CommandArgument);
            successMessage.InnerHtml = _transactionManager.DeleteTransaction(transactionId) ? Provider.GetSuccessMassage("Sucsessfully Deleted") : Provider.GetErrorMassage("delete failed");
            BindGridView();
        }


        protected void TransactionGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TransactionGridView.PageIndex = e.NewPageIndex;
            TransactionGridView.DataBind();
            BindGridView();
        }

        protected void CancelTransaction_OnClick(object sender, EventArgs e)
        {
            Provider.ClearTextBoxes(this);
            transactionHidden.Value = string.Empty;
            SaveTransaction.Text = "Save";
        }
    }
}