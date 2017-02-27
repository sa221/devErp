using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class PartySetup : System.Web.UI.Page
    {
        DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPartyInfoGrid();
            }
        }
        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (organizationNameText.Value != "" && addressText.Value != "" && contactPersonNameText.Value != "" && contactNumber.Value != "" && addressText.Value != "")
            {
                var checkParty =
                    db.tblSuppliers.FirstOrDefault(x => x.OrganizationName == organizationNameText.Value.Trim());

                if (checkParty == null && saveButton.Text == "Save")
                {
                    SaveParty();
                    partyInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Party Added Successfully";
                    ClearText();
                    LoadPartyInfoGrid();
                }
                else if (checkParty != null && saveButton.Text == "Update")
                {
                    UpdateParty(checkParty.OrganizationName);
                    partyInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Party Updated Successfully";
                    saveButton.Text = "Save";
                    ClearText();
                    LoadPartyInfoGrid();
                    organizationNameText.Attributes.Remove("readonly");
                }
                else if (checkParty != null && saveButton.Text == "Save")
                {
                    partyInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Party Already Exist";
                }
                else if (checkParty != null && saveButton.Text == "Update")
                {
                    partyInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Party Not Found";
                }
            }
            else
            {
                partyInfoLiteral.Text =
                    "<span style='color:#A94464;background-color: #F2DEDE'>Please Fill All Required Field.";
            }
        }

        public void SaveParty()
        {
            int maxPartyId = 1;
            tblSupplier party = new tblSupplier();
            var p = from u in db.tblSuppliers
                    select new { u.SupplierId };
            if (p.FirstOrDefault() != null)
            {
                maxPartyId = Convert.ToInt32(p.Max(x => x.SupplierId)) + maxPartyId;
            }
            party.SupplierId = maxPartyId.ToString();
            party.Type = supplierCustomerDropDownList.SelectedValue;
            party.OrganizationName = organizationNameText.Value;
            party.ContactPerson = contactPersonNameText.Value;
            party.Address = addressText.Value;
            party.ContactNo = contactNumber.Value;
            party.Email = emailAddressText.Value;
            party.OpeningBalance = Convert.ToDouble(openingBalanceText.Value);
            db.tblSuppliers.InsertOnSubmit(party);
            db.SubmitChanges();
        }
        private void UpdateParty(string orgName)
        {
            var checkParty =
                    db.tblSuppliers.FirstOrDefault(x => x.OrganizationName == orgName);
            if (checkParty != null)
            {
                checkParty.Type = supplierCustomerDropDownList.SelectedValue;
                checkParty.Address = addressText.Value;
                checkParty.ContactPerson = contactPersonNameText.Value;
                checkParty.ContactNo = contactNumber.Value;
                checkParty.Email = emailAddressText.Value;
                openingBalanceText.Value = openingBalanceText.Value;
                checkParty.OpeningBalance = Convert.ToDouble(openingBalanceText.Value);
                db.SubmitChanges();
            }
        }
        private void LoadPartyInfoGrid()
        {
            var getAllPartyInfo = from x in db.tblSuppliers
                                  orderby x.OrganizationName ascending
                                  select new { x.SupplierId, x.OrganizationName, x.ContactPerson };
            partyGridView.DataSource = getAllPartyInfo;
            partyGridView.DataBind();
        }
        protected void PartyOnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(partyGridView, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        //Grid row command retrive value to TextBox
        protected void partyGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearText();
            partyInfoLiteral.Text = "_";
            int index = partyGridView.SelectedRow.RowIndex;
            GridViewRow gvRow = partyGridView.Rows[index];
            string partyIdLvl = ((Label)gvRow.FindControl("idLabel")).Text;
            string orgNameLvl = ((Label)gvRow.FindControl("orgNameLabel")).Text;
            //string contactNameLvl = ((Label)gvRow.FindControl("contactPersonLabel")).Text;
            var getPartyInfo = db.tblSuppliers.FirstOrDefault(x => x.SupplierId == partyIdLvl && x.OrganizationName == orgNameLvl);
            if (getPartyInfo != null)
            {
                supplierCustomerDropDownList.SelectedValue = getPartyInfo.Type;
                organizationNameText.Value = getPartyInfo.OrganizationName;
                contactPersonNameText.Value = getPartyInfo.ContactPerson;
                addressText.Value = getPartyInfo.Address;
                contactNumber.Value = getPartyInfo.ContactNo;
                emailAddressText.Value = getPartyInfo.Email;
                openingBalanceText.Value = getPartyInfo.OpeningBalance.ToString();
            }

            organizationNameText.Attributes.Add("readonly", "readonly");
            saveButton.Text = "Update";
        }

        private void ClearText()
        {
            supplierCustomerDropDownList.SelectedValue = "1";
            organizationNameText.Value = "";
            contactPersonNameText.Value = "";
            contactNumber.Value = "";
            addressText.Value = "";
            emailAddressText.Value = "";
            openingBalanceText.Value = "";
        }
    }
}