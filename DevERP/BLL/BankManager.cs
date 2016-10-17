using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class BankManager
    {
        readonly BankGatway _bankGatway = new BankGatway();

        public bool InsertBank(string bankName)
        {
            return _bankGatway.InsertBank(bankName);
        }

        public bool UpdateBank(Bank bank)
        {
            return _bankGatway.UpdateBank(bank);
        }
        public bool DeleteBank(int bankId)
        {
            return _bankGatway.DeleteBank(bankId);
        }

        public List<Bank> GetAllBank()
        {
            return _bankGatway.GetAllBank();
        }
    }
}