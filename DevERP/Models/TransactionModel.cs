using System;

namespace DevERP.Models
{
    public class TransactionModel
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ItemId { get; set; }
        public int SubItemId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionCatagory { get; set; }
        public int PartyId { get; set; }
        public string TransactionType { get; set; }
        public int BankId { get; set; }
        public string Remarks { get; set; }
        public DateTime LastModify { get; set; }
    }
}