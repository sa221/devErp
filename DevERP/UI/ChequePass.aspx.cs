using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;

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
            
        }

        protected void CancelButton_OnClick(object sender, EventArgs e)
        {
            
        }
        private void BindGridView()
        {
            //if (StatusDropDown.SelectedValue == "Pending")
            //{
            //    List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions().FindAll(x => x.ChequeStatus.Equals("Pending"));
            //    BindOnlyGrid(transactions);
            //}
            //else if (StatusDropDown.SelectedValue == "Pass")
            //{
            //    List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions().FindAll(x => x.ChequeStatus.Equals("Pass"));
            //    BindOnlyGrid(transactions);
            //}
            //else if (StatusDropDown.SelectedValue == "Cancel")
            //{
            //    List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions().FindAll(x => x.ChequeStatus.Equals("Cancel"));
            //    BindOnlyGrid(transactions);
            //}
            //else
            //{
            //    List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions();
            //    BindOnlyGrid(transactions);
            //}
            if (StatusDropDown.SelectedValue.Equals("All"))
            {
                BindOnlyGrid(_chequePassManager.GetAllChequeTransactions());
            }
            else
            {
                List<Transaction> transactions = _chequePassManager.GetAllChequeTransactions().FindAll(x => x.ChequeStatus.Equals(StatusDropDown.SelectedValue));
                BindOnlyGrid(transactions);
            }
            ShowHideButton();
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
                    ((LinkButton)gridViewRow.FindControl("PassButton")).Visible = false;
                    ((LinkButton)gridViewRow.FindControl("CancelButton")).Visible = false;
                }
            }
        }
    }
}