namespace DevERP.Model
{
    public class Item
    {
        public string PartsCode { get; set; }
        public int GroupId { get; set; }
        public int CategoryId { get; set; }       
        public string PartsName { get; set; }
        public string Unit { get; set; }
        public string UsesLoc { get; set; }
        public string PartsType { get; set; }
        public string UsesPurpose { get; set; }
        public double ReOrderLevel { get; set; }
        public double TenRate { get; set; }
        public string LifeCycle { get; set; }
        public string Department { get; set; }
        public string CompanyName { get; set; }
    }
}