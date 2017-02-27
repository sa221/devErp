using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using SBBusMS.DAL;

namespace DevERP.BLL
{
    public class PaymentManager
    {
        PaymentGateway aPaymentGateway=new PaymentGateway();
        public string Message { get; set; }
        public List<Payment> GetAll()
        {
            return aPaymentGateway.GetAll();
        }

        public int SavePayment(Payment payment)
        {
            int error = 0;
            var existsPayment = GetAll();
            var existInvId = existsPayment.Find(f => f.PayInvoiceNo == payment.PayInvoiceNo);
            try
            {
                if (existInvId !=null)
                {
                    payment.PayInvoiceNo = existInvId.PayInvoiceNo;
                    bool isUpdated = aPaymentGateway.UpdatePayment(payment);
                    if (isUpdated)
                    {
                        Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                        Message += "Record Updated Successfully</div>";
                    }
                }
                else
                {
                    bool isSaved = aPaymentGateway.SavePayment(payment);
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

        public List<Payment> GetAllPaymentInvoice()
        {
           return aPaymentGateway.GetAllPaymentInvoice();
        }

        //public List<Payment> GetAllBank()
        //{
        //    return aPaymentGateway.GetAllBank();
        //}

        public List<Payment> InvPrefixList(string purYear)
        {
            return aPaymentGateway.InvPrefixList(purYear);
        }
    }
}