using System;
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

        protected void SearchTransaction_OnClick(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            TransactionViewModel transactionView = GetTransactionViewModel();
            TransactionGridView.DataSource = _transactionViewManager.GetAllTransaction(transactionView);
            TransactionGridView.DataBind();
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
    }
}