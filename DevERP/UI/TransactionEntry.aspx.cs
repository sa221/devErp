using System;
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
                BindTransactionGrid();
            }
        }

        private void LoadAllDropdown()
        {
            BindItem();
            LoadSubitem();
            BindParty();
            LoadBank();

        }

        private void BindTransactionGrid()
        {
            transactionGridView.DataSource = _transactionManager.GetAllTransaction();
            transactionGridView.DataBind();

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
            if (_transactionManager.InsertTransaction(transaction))
            {
                successMessage.InnerHtml = Provider.GetSuccessMassage("Sucsessfully Insert");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Insert failed");
            }

        }

        private Transaction GetTransactionModel()
        {
            Transaction transactionModel = new Transaction();
            transactionModel.TransactionDate = Provider.DateTimeConverter(transactionDate.Value);
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
        protected void TransactionGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void lnkEdit_OnClick(object sender, EventArgs e)
        {

        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {

        }
    }
}