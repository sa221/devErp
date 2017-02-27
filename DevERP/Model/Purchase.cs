using System;

namespace DevERP.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public string PurchaseInvNo { get; set; }
        public string PurchaseInvNoText { get; set; }
        
        public string ChalanNo { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime TentativeDate { get; set; }

        public string SupplierId { get; set; }

        public string OrganizationName { get; set; }        
       
        public double TotalAmount { get; set; }

        public string Remarks { get; set; }

        public double   Amount { get; set; }        

        public string EntryBy { get; set; }

        public DateTime EntryDate { get; set; }

        public string Approve { get; set; }

        public string ApproveBy { get; set; }

        public DateTime ApproveDate { get; set; }

        public string Department { get; set; }

        public string CompanyName { get; set; }

    }
}