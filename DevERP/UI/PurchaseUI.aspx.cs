using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevERP.Base;
using DevERP.BLL;
using DevERP.Model;

namespace DevERP.UI
{
    public partial class PurchaseUI : System.Web.UI.Page
    {
        static  PurchaseManager aPurchesManager =new PurchaseManager();
        static PurchaseDetailsManager aPurchaseDetailsManager=new PurchaseDetailsManager();
        SupplierManager aSupplierManager=new SupplierManager();
        ItemManager iManager=new ItemManager();
        static  CustomMethod customMethod=new CustomMethod();

        static DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPurchaseNoDropDownList();
                LoadSupplierIdDropDownList();
                LoadSupplierNameDropDownList();
                LoadPartsNameDropDownList();
            }
        }
         private void LoadPartsNameDropDownList()
        {
            partsNameDropDownList.DataSource = iManager.GetAll();
            partsNameDropDownList.DataTextField = "PartsName";
            partsNameDropDownList.DataValueField = "PartsCode";
            partsNameDropDownList.DataBind();
            partsNameDropDownList.Items.Insert(0,new ListItem("","0"));
        }

        private void LoadSupplierNameDropDownList()
        {
           
            supplierNameDropDownList.DataSource = aSupplierManager.GetAll();
            supplierNameDropDownList.DataTextField = "OrganizationName";
            supplierNameDropDownList.DataValueField = "SupplierId";
            supplierNameDropDownList.DataBind();
            supplierNameDropDownList.Items.Insert(0,new ListItem("Select","0"));
        }

        private void LoadSupplierIdDropDownList()
        {
            supplierIdDropDownList.DataSource = aSupplierManager.GetAll();
            supplierIdDropDownList.DataTextField = "SupplierId";
            supplierIdDropDownList.DataValueField = "SupplierId";
            supplierIdDropDownList.DataBind();
            supplierIdDropDownList.Items.Insert(0,new ListItem("Select","0"));
        }

        private void LoadPurchaseNoDropDownList()
        {
            DevERPDBDataContext myContext = new DevERPDBDataContext();
            var data = from purchase in myContext.tblPurchases
                       select new { purchase.PurchaseInvNo };
            puchaseinvoiceNoDropDownList.DataSource = data;
            puchaseinvoiceNoDropDownList.DataTextField = "PurchaseInvNo";
            puchaseinvoiceNoDropDownList.DataValueField = "PurchaseInvNo";
            puchaseinvoiceNoDropDownList.DataBind();

            puchaseinvoiceNoDropDownList.Items.Insert(0,new ListItem("","0"));
        }

        [WebMethod]
        public static object GetPartsDetailsById(int partsCode)
        {
            var partsInfo = db.tblPartsInfos.FirstOrDefault(x => x.PartsCode == partsCode);
            return partsInfo;
        }



       [WebMethod]
        public static object SavePurchase(Purchase purches, PurchaseDetails purchesDetails, string purchaseDate)
       {
           string dept, compName;
           dept = "Office";//value will be initialize from session value
           compName = "SB Super Deluxe";//value will be initialize from session value
           purches.Department = dept;
           purches.CompanyName = compName;

           purchesDetails.Department = dept;
           purchesDetails.CompanyName = compName;

           purches.PurchaseDate = DateTime.ParseExact(purchaseDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

           ReturnToClient returnToClient=new ReturnToClient();
           string message = "";
           var id = purches.PurchaseInvNo;
           string purYear = "Pur-" + DateTime.Now.Year + "";
           if (id =="0")
           {
               if (aPurchesManager.InvPrefixList(purYear).Count == 0)
                   purches.PurchaseInvNo = "Pur-" + DateTime.Now.Year  + "-0000001";
               else
               {
                   var lastPurchaseId = aPurchesManager.InvPrefixList(purYear).Max();
                   purches.PurchaseInvNo = customMethod.GenerateInvNo("Pur", lastPurchaseId.PurchaseInvNo);
               }
           }
              
           int errorCount = aPurchesManager.SavePurchase(purches);
           if (errorCount>0)
           {
               returnToClient.Message = aPurchesManager.Message;
               returnToClient.InvoiceNo = "";
           }
           else
           {
               purchesDetails.PurchaseInvNo = purches.PurchaseInvNo;
               aPurchaseDetailsManager.SaveItem(purchesDetails);
               returnToClient.Message = aPurchesManager.Message;
               returnToClient.InvoiceNo = purchesDetails.PurchaseInvNo;

           }
           return returnToClient;

           
       }

        public static List<Purchase> GetAll()
        {
           return aPurchesManager.GetAll();
        }

        [WebMethod]
        public static List<PurchesItem> GetAllPurchaseItem(string invoiceNo)
        {
            List<PurchesItem> purchesItem= aPurchesManager.GetAllPurchaseItem().FindAll(x=>x.PurchaseInvNo==invoiceNo);
            return purchesItem;
        }

        [WebMethod]
        public static object GetPurchaseDetailsByInvoc(string invoiceNo)
        {
            //tblPurchase tblPurchase = db.tblPurchases.FirstOrDefault(x => x.PurchaseInvNo == invoiceNo);

            //return tblPurchase;

            var tbPurchase = (from ret in db.tblPurchases
                              where ret.PurchaseInvNo == invoiceNo
                              select new
                              {
                                  PurchaseDate = string.Format("{0:dd-MM-yyyy}", ret.PurchaseDate),
                                  ret.SupplierId,
                                  ret.ChalanNo,
                                  ret.Remarks,
                                 

                              }).ToList();

           
            if (tbPurchase.Any())
            {
                return tbPurchase.Last();
            }
            return tbPurchase;
        }

        [WebMethod]
        public static List<PurchaseDetails> GetAllPurchesDetails()
        {
            return aPurchaseDetailsManager.GetAllPurchesDetails();
        }

        [WebMethod]
        public static string DeleteItem(int pdId)
        {
            return aPurchaseDetailsManager.DeleteItem(pdId);
        }
    }

    public class ReturnToClient
    {
        public string InvoiceNo { get; set; }
        public string Message { get; set; }
    }
    
}