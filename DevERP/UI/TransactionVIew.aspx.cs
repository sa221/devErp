using System;
using System.Drawing;
using System.Globalization;
using System.Web.UI.WebControls;
using DevERP.BLL;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class TransactionVIew : System.Web.UI.Page
    {
        private readonly TransactionViewManager _transactionViewManager = new TransactionViewManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        //private List<Transaction> GetTransactions()
        //{
        //    List<Transaction> transactions;
        //    if (ViewState["transactions"] == null)
        //    {
        //        TransactionViewModel transactionView = GetTransactionViewModel();
        //        bool isSuccess;
        //        decimal balance;
        //        transactions = _transactionViewManager.GetAllTransaction(transactionView, out isSuccess, out balance);
        //        ViewState["transactions"] = transactions;
        //    }
        //    else
        //    {
        //        transactions = (List<Transaction>)ViewState["transactions"];
        //    }

        //    return transactions;
        //}
        protected void SearchTransaction_OnClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            TransactionViewModel transactionView = GetTransactionViewModel();
            bool isSuccess;
            decimal balance;
            TransactionGridView.DataSource = _transactionViewManager.GetAllTransaction(transactionView, out isSuccess, out balance);
            TransactionGridView.DataBind();
            if (isSuccess)
            {
                ((Label)TransactionGridView.FooterRow.FindControl("balance")).Text = balance.ToString(CultureInfo.CurrentCulture);
            }
            else
            {
                ((Label)TransactionGridView.FooterRow.FindControl("balance")).Text = "Error";
            }
            ChangeStatusColor();
        }

        private TransactionViewModel GetTransactionViewModel()
        {
            TransactionViewModel transactionView = new TransactionViewModel();
            if (string.IsNullOrEmpty(fromDate.Value))
            {
                transactionView.FromDate = Provider.GetMinDate();
            }
            else
            {
                transactionView.FromDate = Provider.StringToDateTime(fromDate.Value);
            }
            if (string.IsNullOrEmpty(toDate.Value))
            {
                transactionView.ToDate = DateTime.Today;
            }
            else
            {
                transactionView.ToDate = Provider.StringToDateTime(toDate.Value);
            }

            transactionView.TransactionCatagory = CatagoryDropDown.SelectedValue;
            transactionView.TransactionType = TypeDropDown.SelectedValue;
            return transactionView;
        }

        protected void TransactionGridView_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {   
            TransactionGridView.PageIndex = e.NewPageIndex;
            TransactionGridView.DataBind();
            BindGridView();
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
    }
}