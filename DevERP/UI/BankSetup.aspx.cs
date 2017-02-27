using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class BankSetup : System.Web.UI.Page
    {
        DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBankInfoGrid();
            }
            
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (bankId.Value != "" && bankName.Value != "" &&
                contactNameText.Value != "" && contactNumber.Value != "" &&
                cardCommisionText.Value != "")
            {
                var checkBankInfo =
                    db.BankInformation_tbls.FirstOrDefault(
                        x => x.VarBankid == bankId.Value);

                if (checkBankInfo == null && saveButton.Text == "Save")
                {
                    SaveData();
                    bankInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Bank Info Added Successfully";
                    ClearText();
                    LoadBankInfoGrid();
                }
                else if (checkBankInfo != null && saveButton.Text == "Update")
                {
                    UpdateData(checkBankInfo.VarBankid);
                    bankInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Bank Info Updated Successfully";
                    saveButton.Text = "Save";
                    ClearText();
                    LoadBankInfoGrid();
                    //groupNameDropDownList.Enabled = true;
                    //productNameDropDownList.Enabled = true;
                    //brandNameDropDownList.Enabled = true;
                    //categoryNameDropDownList.Enabled = true;
                    bankId.Attributes.Remove("readonly");
                }
                else if (checkBankInfo != null && saveButton.Text == "Save")
                {
                    bankInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Bank Info Already Exist";
                }
                else if (checkBankInfo != null && saveButton.Text == "Update")
                {
                    bankInfoLiteral.Text = "<span style='color:#3C763D;background-color: #DFF0D8'>Bank Info Not Found";
                }
            }
            else
            {
                bankInfoLiteral.Text =
                    "<span style='color:#A94464;background-color: #F2DEDE'>Please Fill All Required Field.";
            }
        }
        private void SaveData()
        {
            BankInformation_tbl bankInfo = new BankInformation_tbl();

            bankInfo.VarBankid = bankId.Value;
            bankInfo.VarBankName = bankName.Value;
            bankInfo.VarContractpersons = contactNameText.Value;
            bankInfo.VarBankAddress = addressText.Value;
            bankInfo.VarBankphoneno = contactNumber.Value;
            bankInfo.VarEmailAddress = emailAddressText.Value;
            bankInfo.CardCom = Convert.ToDouble(cardCommisionText.Value);
            db.BankInformation_tbls.InsertOnSubmit(bankInfo);
            db.SubmitChanges();
        }

        private void UpdateData(string b)
        {
            var checkBankInfo =
                    db.BankInformation_tbls.FirstOrDefault(
                        x => x.VarBankid == b);
            if (checkBankInfo != null)
            {
                checkBankInfo.VarBankName = bankName.Value;
                checkBankInfo.VarContractpersons = contactNameText.Value;
                checkBankInfo.VarBankAddress = addressText.Value;
                checkBankInfo.VarBankphoneno = contactNumber.Value;
                checkBankInfo.VarEmailAddress = emailAddressText.Value;
                checkBankInfo.CardCom = Convert.ToDouble(cardCommisionText.Value);
                db.SubmitChanges();
            }
        }
        private void LoadBankInfoGrid()
        {
            var getAllBankInfo = from x in db.BankInformation_tbls
                                 orderby x.VarBankid ascending
                                 select new { x.VarBankid, x.VarBankName, x.VarContractpersons };
            bankGridView.DataSource = getAllBankInfo;
            bankGridView.DataBind();
        }
        protected void BankOnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(bankGridView, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        //Grid row command retrive value to TextBox
        protected void bankGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClearText();
            bankInfoLiteral.Text = "_";
            int index = bankGridView.SelectedRow.RowIndex;
            GridViewRow gvRow = bankGridView.Rows[index];
            string bankIdLvl = ((Label)gvRow.FindControl("idLabel")).Text;
            string bankNameLvl = ((Label)gvRow.FindControl("bankNameLabel")).Text;
            string contactNameLvl = ((Label)gvRow.FindControl("contactPersonLabel")).Text;
            var getBankInfo = db.BankInformation_tbls.FirstOrDefault(x => x.VarBankid == bankIdLvl);
            if (getBankInfo != null)
            {
                bankId.Value = getBankInfo.VarBankid;
                bankName.Value = getBankInfo.VarBankName;
                contactNameText.Value = getBankInfo.VarContractpersons;
                addressText.Value = getBankInfo.VarBankAddress;
                contactNumber.Value = getBankInfo.VarBankphoneno;
                emailAddressText.Value = getBankInfo.VarEmailAddress;
                cardCommisionText.Value = getBankInfo.CardCom.ToString();
            }

            bankId.Attributes.Add("readonly", "readonly");
            saveButton.Text = "Update";
            // groupId.Attributes.Add("readonly", "readonly");
        }
        private void ClearText()
        {
            bankId.Value = "";
            bankName.Value = "";
            contactNameText.Value = "";
            addressText.Value = "";
            contactNumber.Value = "";
            emailAddressText.Value = "";
            cardCommisionText.Value = "";
        }
    }
}