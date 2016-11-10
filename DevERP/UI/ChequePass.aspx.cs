using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class ChequePass : System.Web.UI.Page
    {
        readonly ChequePassManager _chequePassManager = new ChequePassManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void StatusDropDown_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            successMessage.InnerHtml = string.Empty;
            BindGridView();
        }

        protected void PassButton_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int transactionId = Convert.ToInt32(lnkRemove.CommandArgument);
            successMessage.InnerHtml = _chequePassManager.UpdateChequeStatus(transactionId, "Pass")
                ? Provider.GetSuccessMassage("Successfully Pass the Cheque")
                : Provider.GetSuccessMassage("Successfully Pass the Cheque");
            BindGridView();
        }

        protected void CancelButton_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            int transactionId = Convert.ToInt32(lnkRemove.CommandArgument);
            successMessage.InnerHtml = _chequePassManager.UpdateChequeStatus(transactionId, "Cancel")
                ? Provider.GetSuccessMassage("Successfully Cancel the Cheque")
                : Provider.GetSuccessMassage("Successfully Cancel the Cheque");
            BindGridView();
        }
        private void BindGridView()
        {
            ChequePassModel chequePassModel = GetChequePassModel();
            if (chequePassModel.ChequeStatus.Equals("All"))
            {
                BindOnlyGrid(_chequePassManager.GetAllChequeTransactions(chequePassModel));
            }
            else
            {
                List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions(chequePassModel).FindAll(x => x.ChequeStatus.Equals(chequePassModel.ChequeStatus));
                BindOnlyGrid(transactions);
            }
            BindBalance();
            ShowHideButton();
            ChangeStatusColor();
        }

        private void BindBalance()
        {
            ChequePassModel chequePassModel = GetChequePassModel();
            bool isSuccess;
            decimal balance = _chequePassManager.GetBalance(chequePassModel, out isSuccess);
            ((Label) TransactionGridView.FooterRow.FindControl("balance")).Text = isSuccess ? balance.ToString(CultureInfo.CurrentCulture) : "Error";
        }
        private ChequePassModel GetChequePassModel()
        {
            ChequePassModel chequePassModel = new ChequePassModel();
            if (string.IsNullOrEmpty(fromDate.Value))
            {
                chequePassModel.FromDate = Provider.GetMinDate();
            }
            else
            {
                chequePassModel.FromDate = Provider.StringToDateTime(fromDate.Value);
            }
            if (string.IsNullOrEmpty(toDate.Value))
            {
                chequePassModel.ToDate = DateTime.Today;
            }
            else
            {
                chequePassModel.ToDate = Provider.StringToDateTime(toDate.Value);
            }
            chequePassModel.ChequeStatus = StatusDropDown.SelectedValue;
            return chequePassModel;
        }
        private void BindOnlyGrid(List<Transaction> transactions)
        {
            TransactionGridView.DataSource = transactions;
            TransactionGridView.DataBind();
        }

        private void ShowHideButton()
        {
            foreach (GridViewRow gridViewRow in TransactionGridView.Rows)
            {
                string status = ((Label)gridViewRow.FindControl("status")).Text;
                if (!status.Equals("Pending"))
                {
                    ((LinkButton)gridViewRow.FindControl("PassButton")).Enabled = false;
                    ((LinkButton)gridViewRow.FindControl("CancelButton")).Enabled = false;
                    ((LinkButton)gridViewRow.FindControl("PassButton")).ForeColor = Color.DarkGray;
                    ((LinkButton)gridViewRow.FindControl("CancelButton")).ForeColor = Color.DarkGray;
                }
            }
        }
        private void ChangeStatusColor()
        {
            foreach (GridViewRow gridViewRow in TransactionGridView.Rows)
            {
                string status = ((Label)gridViewRow.FindControl("status")).Text;
                
                if (status.Equals("Pending"))
                {
                    ((Label) gridViewRow.FindControl("status")).ForeColor = Color.BlueViolet;
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

        protected void TransactionGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            TransactionGridView.PageIndex = e.NewPageIndex;
            TransactionGridView.DataBind();
            BindGridView();
        }

        protected void SearchTransaction_OnClickTransaction_OnClick(object sender, EventArgs e)
        {
            successMessage.InnerHtml = string.Empty;
            BindGridView();
        }
    }
}