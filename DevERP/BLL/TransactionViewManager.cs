using System;
using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class TransactionViewManager
    {
        readonly TransactionViewGatway _transactionViewGatway = new TransactionViewGatway();


        public List<Transaction> GetAllTransaction(TransactionViewModel transactionView, out Boolean isSuccess, out decimal balance)
        {
            string query = "";
            if (transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all")&& transactionView.TransactionType.Equals("all"))
            {
                query = "";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionDate between '"+transactionView.FromDate+"' and '"+transactionView.ToDate+"'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionCatagory='"+transactionView.TransactionCatagory+"'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionType='"+transactionView.TransactionType+"'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and t.transactionCatagory='" + transactionView.TransactionCatagory + "'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and t.transactionType='" + transactionView.TransactionType + "'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionCatagory='" + transactionView.TransactionCatagory + "' and t.transactionType='" + transactionView.TransactionType + "'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where t.transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and t.transactionCatagory='" + transactionView.TransactionCatagory + "' and t.transactionType='" + transactionView.TransactionType + "'";
            }

            balance = _transactionViewGatway.GetBalance(query, out isSuccess);
            return _transactionViewGatway.GetAllTransaction(query);
        }
    }
}