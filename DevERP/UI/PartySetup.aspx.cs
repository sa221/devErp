using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class PartySetup : System.Web.UI.Page
    {
        PartyManager partyManager = new PartyManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindParty();
            }
        }
        private void BindParty()
        {
            List<Party> parties = partyManager.GetAllParty();
            PartyGridView.DataSource = parties;
            PartyGridView.DataBind();
        }
        protected void SaveParty_OnClick(object sender, EventArgs e)
        {
            Party party = new Party();
            party.PartyName = partyName.Value;
            party.PartyMobile = partyMobile.Value;
            party.PartyAddress = partyAddress.Value;
            if (partyManager.InsertParty(party))
            {
                BindParty();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Inserted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Insert Failed");
            }
        }

        protected void PartyGridView_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            PartyGridView.EditIndex = e.NewEditIndex;
            BindParty();
        }

        protected void PartyGridView_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Party party = new Party();
            party.PartyId = Convert.ToInt32(((Label)PartyGridView.Rows[e.RowIndex].FindControl("id")).Text);
            party.PartyName = ((TextBox)PartyGridView.Rows[e.RowIndex].FindControl("partyNameTextBox")).Text;
            party.PartyMobile = ((TextBox)PartyGridView.Rows[e.RowIndex].FindControl("partyMobileTextBox")).Text;
            party.PartyAddress = ((TextBox)PartyGridView.Rows[e.RowIndex].FindControl("partyAddressTextBox")).Text;
            if (partyManager.UpdateParty(party))
            {
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Updated");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("Update failed");
            }

            PartyGridView.EditIndex = -1;
            BindParty();
        }

        protected void PartyGridView_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            PartyGridView.EditIndex = -1;
            BindParty();
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int partyId = Convert.ToInt32(lnkRemove.CommandArgument);
            if (partyManager.DeleteParty(partyId))
            {
                BindParty();
                successMessage.InnerHtml = Provider.GetSuccessMassage("Successfully Deleted");
            }
            else
            {
                successMessage.InnerHtml = Provider.GetErrorMassage("This Sub-Item already in used");
            }
        }
    }
}