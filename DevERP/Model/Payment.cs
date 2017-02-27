using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevERP.Model
{
    public class Payment
    {
        public string PayInvoiceNo { get; set; }
        public DateTime PayDate { get; set; }
        public string DatDate{ get; set; }
        public string SupplierId { get; set; }
        public string OrganizationName{ get; set; }
        public string Remarks { get; set; }
        public string PayType { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public double PayAmount { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryIp { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }

        //public string NumHeadID { get; set; }

        public string VarHeadName { get; set; }
    }
}