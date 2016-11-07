using System;

namespace DevERP.Models
{
    public class TransactionViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string TransactionCatagory { get; set; }
        public string TransactionType { get; set; }
    }
}