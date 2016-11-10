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
            string preQuery = "where t.transactionDate between '" + transactionView.FromDate + "' and '" + transactionView.ToDate + "' ";
            string partyQuery = "and t.partyId= " + transactionView.PartyId+" ";
            string catagoryQuery = "and t.transactionCatagory='" + transactionView.TransactionCatagory + "' ";
            string typeQuery = "and t.transactionType='" + transactionView.TransactionType + "' ";
            string query = "";
            if (transactionView.PartyId.Equals(0) && transactionView.TransactionCatagory.Equals("all")&& transactionView.TransactionType.Equals("all"))
            {
                query = "";
            }
            else if (!transactionView.PartyId.Equals(0) && transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = partyQuery;
            }
            else if (transactionView.PartyId.Equals(0) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = catagoryQuery;
            }
            else if (transactionView.PartyId.Equals(0) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = typeQuery;
            }
            else if (!transactionView.PartyId.Equals(0) && !transactionView.TransactionCatagory.Equals("all") && transactionView.TransactionType.Equals("all"))
            {
                query = partyQuery + catagoryQuery;
            }
            else if (!transactionView.PartyId.Equals(0) && transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = partyQuery + typeQuery;
            }
            else if (transactionView.PartyId.Equals(0) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = catagoryQuery + typeQuery;
            }
            else if (!transactionView.PartyId.Equals(0) && !transactionView.TransactionCatagory.Equals("all") && !transactionView.TransactionType.Equals("all"))
            {
                query = partyQuery + catagoryQuery + typeQuery;
            }

            balance = _transactionViewGatway.GetBalance(preQuery+query, out isSuccess);
            return _transactionViewGatway.GetAllTransaction(preQuery+query);
        }
    }
}