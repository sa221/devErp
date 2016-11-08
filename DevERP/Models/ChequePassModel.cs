using System;

namespace DevERP.Models
{
    public class ChequePassModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ChequeStatus { get; set; }
    }
}