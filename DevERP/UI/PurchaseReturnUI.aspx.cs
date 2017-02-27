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

namespace DevERP
{
    public partial class PurchaseReturnUI : System.Web.UI.Page
    {
        static ReturnManager aReturnManager = new ReturnManager();
        static ReturnDetailsManager aReturnDetailsManager = new ReturnDetailsManager();
        SupplierManager aSupplierManager = new SupplierManager();
        ItemManager iManager = new ItemManager();
        PurchaseManager aPurchaseManager = new PurchaseManager();
        static CustomMethod customMethod = new CustomMethod();
        static StockManager asStockManager = new StockManager();

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
            partsNameDropDownList.Items.Insert(0, new ListItem("", "0"));
        }

        private void LoadSupplierNameDropDownList()
        {
            supplierNameDropDownList.DataSource = aPurchaseManager.GetAllSupplier();
            supplierNameDropDownList.DataTextField = "OrganizationName";
            supplierNameDropDownList.DataValueField = "SupplierId";
            supplierNameDropDownList.DataBind();
            supplierNameDropDownList.Items.Insert(0, new ListItem("", "0"));
        }

        private void LoadSupplierIdDropDownList()
        {
            supplierIdDropDownList.DataSource = aPurchaseManager.GetAllSupplier();
            supplierIdDropDownList.DataTextField = "SupplierId";
            supplierIdDropDownList.DataValueField = "SupplierId";
            supplierIdDropDownList.DataBind();
            supplierIdDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void LoadPurchaseNoDropDownList()
        {
            puchaseinvoiceNoDropDownList.DataSource = aReturnManager.GetAll();
            puchaseinvoiceNoDropDownList.DataTextField = "PurchaseInvNo";
            puchaseinvoiceNoDropDownList.DataValueField = "PurchaseInvNo";
            puchaseinvoiceNoDropDownList.DataBind();
            puchaseinvoiceNoDropDownList.Items.Insert(0, new ListItem("", "0"));
        }
        [WebMethod]
        public static object SaveReturn(Purchase purchase, PurchaseDetails purchaseDetails, string purchaseDate)
        {
            ReturnToClient returnToClient = new ReturnToClient();

            string deptName = "Office";//value will be initialize from session value
            string compName = "SB Super Deluxe";//value will be initialize from session value
            purchase.Department = deptName;
            purchase.CompanyName = compName;
            purchase.PurchaseDate = DateTime.ParseExact(purchaseDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            purchaseDetails.Department = deptName;
            purchaseDetails.CompanyName = compName;

            decimal stockAvilableQty = asStockManager.GetTotalStock(purchaseDetails.PartsCode,
                                      purchaseDetails.Department, purchaseDetails.CompanyName);

            var id = purchase.PurchaseInvNo;
            string purYear = "Rtn-" + DateTime.Now.Year + "";
            if (id == "0")
            {
                if (aReturnManager.InvPrefixList(purYear).Count == 0)
                    purchase.PurchaseInvNo = "Rtn-" + DateTime.Now.Year + "-0000001";
                else
                {
                    var lastPurchaseId = aReturnManager.InvPrefixList(purYear).Max();
                    purchase.PurchaseInvNo = customMethod.GenerateInvNo("Rtn", lastPurchaseId.PurchaseInvNo);
                }
            }

            //if (stockAvilableQty> (decimal)purchaseDetails.Quantity && purchaseDetails.ModeEditOrSave=="Save")
            if (purchaseDetails.ModeEditOrSave == "Save")
            {
                if (stockAvilableQty > (decimal)purchaseDetails.Quantity)
                {
                    int errorCount = aReturnManager.SaveReturn(purchase);
                    if (errorCount > 0)
                    {
                        //message = aReturnManager.Message;
                        returnToClient.Message = aReturnManager.Message;
                        returnToClient.InvoiceNo = "";
                    }
                    else
                    {
                        //purchaseDetails.PurchaseInvNo = purchase.PurchaseInvNo;
                        //aReturnDetailsManager.SaveItem(purchaseDetails);
                        //message = aReturnManager.Message;

                        purchaseDetails.PurchaseInvNo = purchase.PurchaseInvNo;
                        aReturnDetailsManager.SaveItem(purchaseDetails);
                        returnToClient.Message = aReturnManager.Message;
                        returnToClient.InvoiceNo = purchaseDetails.PurchaseInvNo;
                    }
                }
                else
                {
                    returnToClient.InvoiceNo = purchase.PurchaseInvNo;
                    returnToClient.Message = "<div class='alert alert-danger alert-dismissible' role='alert'>";

                    returnToClient.Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                    returnToClient.Message += "Stock Not Available</div>";
                }
            }
            else
            {
                purchaseDetails.PurchaseInvNo = purchase.PurchaseInvNo;
                if (stockAvilableQty > (decimal)purchaseDetails.Quantity)
                {
                    if (aReturnDetailsManager.UpdateItem(purchaseDetails) > 0)
                    {
                        asStockManager.UpdateStock(purchaseDetails.PartsCode, purchaseDetails.Department,
                            purchaseDetails.CompanyName);
                        returnToClient.InvoiceNo = purchase.PurchaseInvNo;
                        returnToClient.Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        returnToClient.Message +=
                            "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        returnToClient.Message += "Data Updated Successfully</div>";
                    }
                }
                else
                {
                    returnToClient.InvoiceNo = purchase.PurchaseInvNo;
                    returnToClient.Message = "<div class='alert alert-danger alert-dismissible' role='alert'>";
                    returnToClient.Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                    returnToClient.Message += "Stock Not Available</div>";
                }


            }
            return returnToClient;


        }

        public static List<Purchase> GetAll()
        {
            return aReturnManager.GetAll();
        }

        [WebMethod]
        public static List<PurchesItem> GetAllReturnItem()
        {
            return aReturnManager.GetAllReturnItem();
        }

        [WebMethod]
        public static object GetReturnDetailsByInvoc(string invoiceNo)
        {
            //tblReturn tblReturn = db.tblReturns.FirstOrDefault(x => x.PurchaseInvNo == invoiceNo);

            var tbReturn = (from ret in db.tblReturns
                            where ret.PurchaseInvNo == invoiceNo
                            select new
                            {
                                PurchaseDate = string.Format("{0:dd-MM-yyyy}", ret.PurchaseDate),
                                ret.SupplierId,
                                ret.ChalanNo,
                                ret.Remarks,
                                ret.Amount
                            }).ToList();

            if (tbReturn.Any())
            {
                return tbReturn.Last();
            }
            return tbReturn;
        }
        [WebMethod]
        public static List<PurchaseDetails> GetAllReturnDetails(string invoiceNo)
        {
            return aReturnDetailsManager.GetAllReturnDetails(invoiceNo);
        }

        [WebMethod]
        public static string DeleteReturnItem(int PdId)
        {
            return aReturnDetailsManager.DeleteReturnItem(PdId);
        }
    }
    public class ReturnToClient
    {
        public string InvoiceNo { get; set; }
        public string Message { get; set; }
    }
}