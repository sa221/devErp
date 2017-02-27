using System;
using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Model;
using SBBusMS.DAL;
namespace DevERP.BLL
{
    public class PurchaseManager
    {
        public string Message { get; set; }

        PurchaseGateway aPurchesGateway = new PurchaseGateway();
        PurchaseGateway aPurchaseGateway=new PurchaseGateway();
        PurchaseDetailsGateway aPurchaseDetailsGateway=new PurchaseDetailsGateway();

        //public List<Purchase> GetAllInvoiceNo()
        //{
        //    return aPurchesGateway.GetAllInvoiceNo();
        //}

        public int SavePurchase(Purchase purches)
        {

            int error = 0;
            var existingPurchase = GetAll();
            var existInvNo = existingPurchase.Find(f => f.PurchaseInvNo == purches.PurchaseInvNo);
            try
            {
                if (existInvNo != null)
                {
                    purches.Id = existInvNo.Id;
                    bool isUpdated = aPurchesGateway.UpdatePurchase(purches);
                    if (isUpdated)
                    {
                        Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        Message += "Record Updated Successfully</div>";
                    }
                }
                else
                {
                    bool isSaved = aPurchesGateway.SavePurchase(purches);
                    if (isSaved)
                    {
                        Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        Message += "Record Saved Successfully</div>";
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


        public string DeletePurches(string purchaseInvNo)
        {
            return aPurchesGateway.DeletePurches(purchaseInvNo);
        }

        
        //public List<Purches> GetPurchaseInfo()
        //{
        //    return aPurchesGateway.GetPurchaseInfo();
        //}

        public List<Purchase> GetAll()
        {
            return aPurchaseGateway.GetAll();
        }

        public List<Purchase> GetAllPurchaseInfo()
        {
            return aPurchesGateway.GetAllPurchaseInfo();
        }

        public int DeletePrchaseInv(string invNo)
        {
            int error = 0;
            int rowAffected = aPurchesGateway.DeletePrchaseInv(invNo);
            if (rowAffected >0)
            {
                int deletePurchaseItem = aPurchaseDetailsGateway.DeletePurchseItem(0,invNo);
                if (deletePurchaseItem <= 0)
                    return ++error;
                return error;
            }
            return ++error;
        }

        public bool UpdatePurchase(Purchase aPurches)
        {
            return aPurchesGateway.UpdatePurchase(aPurches);
        }

        public List<PurchesItem> GetAllPurchaseItem()
        {
            return aPurchesGateway.GetAllPurchaseItem();
          
        }

       
        public string DeleteItem(int pdId)
        {
            
            string message = "";

            int rowAffected = aPurchesGateway.DeleteItem(pdId);
            if (rowAffected>0)
            {
                message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                message += "Record Deleted successfully</div>";
            }
            return message;
        }

        public List<Purchase> GetAllSupplier()
        {
            return aPurchesGateway.GetAllSupplier();
        }

        public List<Purchase> GetAllPurchaseInvNo()
        {
            return aPurchaseGateway.GetAllPurchaseInvNo();
        }

        public List<Purchase> InvPrefixList(string purYear)
        {
            return aPurchaseGateway.InvPrefixList(purYear);

        }
    }
}