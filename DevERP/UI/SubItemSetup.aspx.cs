using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class SubItemSetup : System.Web.UI.Page
    {
        ItemManager itemManager = new ItemManager();
        SubItemManager subItemManager = new SubItemManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadItems();
                BindSubItem();
            }
        }

        private void LoadItems()
        {
            itemNameDropDown.DataSource = itemManager.GetAllItem();
            itemNameDropDown.DataTextField = "ItemName";
            itemNameDropDown.DataValueField = "ItemId";
            itemNameDropDown.DataBind();
            if (itemManager.GetAllItem().Count>0)
            {
                BindSubItem();
            }
            
        }
        private void BindSubItem()
        {
            int itemId = Convert.ToInt32(itemNameDropDown.SelectedValue);
            List<SubItem> subItems = subItemManager.GetAllSubItem(itemId);
            SubItemGridView.DataSource = subItems;
            SubItemGridView.DataBind();
        }
        protected void SaveSubItem_OnClick(object sender, EventArgs e)
        {
            SubItem subItem = new SubItem();
            subItem.ItemId = Convert.ToInt32(itemNameDropDown.SelectedValue);
            subItem.SubItemName = subItemName.Value;
            if (subItemManager.InsertSubItem(subItem))
            {
                BindSubItem();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Inserted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Insert Failed");
            }
        }

        protected void SubItemGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            SubItemGridView.EditIndex = e.NewEditIndex;
            BindSubItem();
        }

        protected void SubItemGridView_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SubItem subItem = new SubItem();
            subItem.SubItemId = Convert.ToInt32(((Label)SubItemGridView.Rows[e.RowIndex].FindControl("id")).Text);
            subItem.SubItemName = ((TextBox)SubItemGridView.Rows[e.RowIndex].FindControl("subItemNameTextBox")).Text;
            if (subItemManager.UpdateSubItem(subItem))
            {
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Updated");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Update failed");
            }

            SubItemGridView.EditIndex = -1;
            BindSubItem();
        }

        protected void SubItemGridView_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            SubItemGridView.EditIndex = -1;
            BindSubItem();
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int subItemId = Convert.ToInt32(lnkRemove.CommandArgument);
            if (subItemManager.DeleteSubItem(subItemId))
            {
                BindSubItem();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Deleted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("This Sub-Item already in used");
            }
        }

        protected void itemNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSubItem();
        }
    }
}