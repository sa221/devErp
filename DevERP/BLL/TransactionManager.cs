using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class TransactionManager
    {
        readonly TransactionGatway _transactionGatway = new TransactionGatway();

        public bool SaveTransaction(Transaction transaction)
        {
            return _transactionGatway.InsertTransaction(transaction);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            return _transactionGatway.UpdateTransaction(transaction);
        }
        public bool DeleteTransaction(int transactionId)
        {
            return _transactionGatway.DeleteTransaction(transactionId);
        }

        public List<Transaction> GetAllTransaction()
        {
            return _transactionGatway.GetAllTransaction();
        }

        public decimal GetBalance(out bool isSuccss)
        {
            return _transactionGatway.GetBalance(out isSuccss);
        }
    }
}