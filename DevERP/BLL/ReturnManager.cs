using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using SBBusMS.DAL;

namespace DevERP.BLL
{
    public class ReturnManager
    {
        public string Message { get; set; }

        ReturnGateway aReturnGateway = new ReturnGateway();
        ReturnDetailsGateway aReturnDetailsGateway = new ReturnDetailsGateway();
        public int SaveReturn(Purchase purchase)
        {
            int error = 0;
            var existingPurchase = GetAll();
            var existInvNo = existingPurchase.Find(f => f.PurchaseInvNo == purchase.PurchaseInvNo);
            try
            {
                if (existInvNo != null)
                {
                    purchase.Id = existInvNo.Id;
                    bool isUpdated = aReturnGateway.UpdateReturn(purchase);
                    if (isUpdated)
                    {
                        Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        Message += "Record Updated successfully</div>";
                    }
                }
                else
                {
                    bool isSaved = aReturnGateway.SaveReturn(purchase);
                    if (isSaved)
                    {
                        Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        Message += "Record saved successfully</div>";
                    }
                }

            }
            catch (Exception ex)
            {

                error++;
                Message = "<div class='alert alert-danger alert-dismissible' role='alert'>";
                Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                Message += "Something wrong.Please try again" + ex.Message + "</div>";
            }
            return error;
        }


        public List<PurchesItem> GetAllReturnItem()
        {
            return aReturnGateway.GetAllReturnItem();
        }
        public List<Purchase> GetAll()
        {
            return aReturnGateway.GetAll();
        }


        public List<Purchase> InvPrefixList(string purYear)
        {
            return aReturnGateway.InvPrefixList(purYear);
        }
    }
}