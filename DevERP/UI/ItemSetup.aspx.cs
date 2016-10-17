using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class ItemSetup : System.Web.UI.Page
    {
        readonly ItemManager _itemManager = new ItemManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItem();
            }
        }
        private void BindItem()
        {
            List<Item> items = _itemManager.GetAllItem();
            ItemGridView.DataSource = items;
            ItemGridView.DataBind();
        }
        protected void SaveItem_OnClick(object sender, EventArgs e)
        {
            if (_itemManager.InsertItem(itemName.Value))
            {
                BindItem();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Inserted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Insert Failed");
            }
        }

        protected void ItemGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            ItemGridView.EditIndex = e.NewEditIndex;
            BindItem();
        }

        protected void ItemGridView_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Item item = new Item();
            item.ItemId = Convert.ToInt32(((Label)ItemGridView.Rows[e.RowIndex].FindControl("id")).Text);
            item.ItemName = ((TextBox)ItemGridView.Rows[e.RowIndex].FindControl("itemNameTextBox")).Text;
            if (_itemManager.UpdateItem(item))
            {
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Updated");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Update failed");
            }

            ItemGridView.EditIndex = -1;
            BindItem();
        }

        protected void ItemGridView_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ItemGridView.EditIndex = -1;
            BindItem();
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int itemId = Convert.ToInt32(lnkRemove.CommandArgument);
            if (_itemManager.DeleteItem(itemId))
            {
                BindItem();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Deleted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("This Item already in used");
            }
        }
    }
}