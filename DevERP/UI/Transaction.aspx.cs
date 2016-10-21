using System;
using System.Web.UI.WebControls;
using DevERP.BLL;

namespace DevERP.UI
{
    public partial class Transaction : System.Web.UI.Page
    {
        readonly ItemManager _itemManager = new ItemManager();
        readonly SubItemManager _subItemManager = new SubItemManager();
        readonly PartyManager _partyManager = new PartyManager();
        readonly BankManager _bankManager = new BankManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllDropdown();
            }
        }

        private void LoadAllDropdown()
        {
            BindItem();
            LoadSubitem();
            BindParty();
            LoadBank();

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
            if (itemId>0)
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