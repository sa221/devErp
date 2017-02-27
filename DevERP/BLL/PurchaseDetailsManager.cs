using System.Collections.Generic;

using DevERP.DAL;
using DevERP.Model;

namespace DevERP.BLL
{
    public class PurchaseDetailsManager
    {
        PurchaseDetailsGateway aPurchaseDetailsGateway=new PurchaseDetailsGateway();
        //public List<PurchesDetails> GetAll()
        //{
        //    return aPurchaseDetailsGateway.GetAll();
        //}
        //public PurchesDetails GetQtyBySupplierName(string supName)
        //{
        //    return aPurchaseDetailsGateway.GetQtyBySupplierName(supName);
        //}

        //public double GetQtyByItemCode(string iCode,string invNo)
        //{
        //    return aPurchaseDetailsGateway.GetQtyByItemCode(iCode, invNo);
        //}

        //public List<PurchesItem> GetAllPurchaseDetails()
        //{
        //    return aPurchaseDetailsGateway.GetAllPurchaseDetails();
        //}

        public int DeletePurchseItem(int gId)
        {
            
           return aPurchaseDetailsGateway.DeletePurchseItem(gId);
         
        }

        public void SaveItem(PurchaseDetails purchesItem)
        {

            if (purchesItem.PDId != 0)
                aPurchaseDetailsGateway.UpdateItem(purchesItem);
            else
                aPurchaseDetailsGateway.SaveItem(purchesItem);
            aPurchaseDetailsGateway.UpdateStock(purchesItem);

        }

        public List<PurchaseDetails> GetAll()
        {
            return aPurchaseDetailsGateway.GetAll();
        }

        public List<PurchaseDetails> GetAllPurchaseDetails( )
        {
            return aPurchaseDetailsGateway.GetAllPurchaseDetails();
        }

        public double GetPurchseDetailQtyByItemCode(string iCode, string invoiceNo)
        {
            return aPurchaseDetailsGateway.GetPurchseDetailQtyByItemCode(iCode, invoiceNo);
        }

        public List<PurchaseDetails> GetAllPurchesDetails()
        {
           return aPurchaseDetailsGateway.GetAllPurchaseDetails();
        }

        public string DeleteItem(int pdId)
        {
            string message = "";
            int rowAffected = aPurchaseDetailsGateway.DeleteItem(pdId);
            if (rowAffected > 0)
            {
                message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                message += "Record saved successfully</div>";
            }
            return message;
        }
    }
}