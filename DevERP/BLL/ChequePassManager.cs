using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class ChequePassManager
    {
        readonly ChequePassGatway _chequePassGatway = new ChequePassGatway();


        public List<Transaction> GetAllChequeTransactions(ChequePassModel chequePassModel)
        {
            return _chequePassGatway.GetAllChequeTransactions(chequePassModel);
        }

        public bool UpdateChequeStatus(int transactionId, string chequeStatus)
        {
            return _chequePassGatway.UpdateChequeStatus(transactionId, chequeStatus);
        }
    }
}