using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class ChequePassManager
    {
        readonly ChequePassGatway _chequePassGatway = new ChequePassGatway();


        public List<Transaction> GetAllChequeTransactions()
        {
            return _chequePassGatway.GetAllChequeTransactions();
        }
    }
}