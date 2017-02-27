namespace DevERP.Model
{
    public class Supplier
    {
        public string SupplierId { get; set; }
        public string OrganizationName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public double OpeningBalance { get; set; }
        public byte[] SupplierPic { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }


    }
}