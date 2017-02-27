namespace DevERP.Model
{
    public class PurchaseDetails
    {
        public int PDId { get; set; }   
        public string PurchaseInvNo { get; set; }
        public string ModeEditOrSave { get; set; }
        public string PartsCode { get; set; }
        public string PartsName { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }



    }
}