using System;
using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class TransactionViewManager
    {
        readonly TransactionViewGatway _transactionViewGatway = new TransactionViewGatway();

        
        public List<Transaction> GetAllTransaction(TransactionViewModel transactionView)
        {
            string query = "";
            if (transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all")&& transactionView.TransactionType.Equals("all"))
            {
                query = "";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionDate between '"+transactionView.FromDate+"' and '"+transactionView.ToDate+"'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionCatagory='"+transactionView.TransactionCatagory+"'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionType='"+transactionView.TransactionType+"'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and transactionCatagory='" + transactionView.TransactionCatagory + "'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and transactionType='" + transactionView.TransactionType + "'";
            }
            else if (transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionCatagory='" + transactionView.TransactionCatagory + "' and transactionType='" + transactionView.TransactionType + "'";
            }
            else if (!transactionView.FromDate.Equals(DateTime.MaxValue) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = "where transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' and transactionCatagory='" + transactionView.TransactionCatagory + "' and transactionType='" + transactionView.TransactionType + "'";
            }
            
            return _transactionViewGatway.GetAllTransaction(query);
        }
    }
}