using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using SBBusMS.DAL;

namespace DevERP.BLL
{
    public class ReturnDetailsManager
    {
        ReturnDetailsGateway aReturnDetailsGateway=new ReturnDetailsGateway();
        StockGateway aStockGateway=new StockGateway();
        public void SaveItem(PurchaseDetails purchesDetails)
        {
            int rowAffectd;
            if (purchesDetails.PDId != 0)
               rowAffectd= aReturnDetailsGateway.UpdateItem(purchesDetails);
            else
              rowAffectd=  aReturnDetailsGateway.SaveItem(purchesDetails);
            if (rowAffectd>0)
            {
                aStockGateway.UpdateStock(purchesDetails.PartsCode,purchesDetails.Department,purchesDetails.CompanyName);
            }

        }

        public int UpdateItem(PurchaseDetails purchaseDetails)
        {
            return aReturnDetailsGateway.UpdateItem(purchaseDetails);
        }

        public List<PurchaseDetails> GetAllReturnDetails(string invoiceNo)
        {
            return aReturnDetailsGateway.GetAllReturnDetails(invoiceNo);
        }

        public string DeleteReturnItem(int pdId)
        {
            string message = "";
            int rowAffected = aReturnDetailsGateway.DeleteReturnItem(pdId);
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