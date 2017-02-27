using System;

namespace DevERP.Model
{
    [Serializable]
    public class PurchesItem
    {
        public int PDId { get; set; }
        public string PurchaseInvNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OrganizationName { get; set; }
        public string ChalanNo{ get; set; } 
        public int  GroupId { get; set; }
        public int  CategoryId { get; set; }
        public string PartsCode { get; set; }

        public string PartsName { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }

        public double Rate { get; set; }
        public double Total { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }

    }
}