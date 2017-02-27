using System;
using System.Collections.Generic;
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
    public partial class PaymentUI : System.Web.UI.Page
    {
        SupplierManager aSupplierManager = new SupplierManager();
        static PaymentManager aPaymentManager = new PaymentManager();
        static CustomMethod customMethod = new CustomMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPaymentInvoiceDropDownList();
                LoadSupplierNameDropDownList();
                //LoadBankNameDropDownList();
            }
        }
        //private void LoadBankNameDropDownList()
        //{
        //    bankNameDropDownList.DataSource = aPaymentManager.GetAllBank();
        //    bankNameDropDownList.DataTextField = "VarHeadName";
        //    bankNameDropDownList.DataValueField = "BankName";
        //    bankNameDropDownList.DataBind();
        //    bankNameDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        //}

        private void LoadPaymentInvoiceDropDownList()
        {
            paymentInvoiceNoDropDownList.DataSource = aPaymentManager.GetAllPaymentInvoice();
            paymentInvoiceNoDropDownList.DataTextField = "PayInvoiceNo";
            paymentInvoiceNoDropDownList.DataValueField = "PayInvoiceNo";
            paymentInvoiceNoDropDownList.DataBind();
            paymentInvoiceNoDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void LoadSupplierNameDropDownList()
        {
            supplierNameDropdownList.DataSource = aSupplierManager.GetAll();
            supplierNameDropdownList.DataTextField = "OrganizationName";
            supplierNameDropdownList.DataValueField = "SupplierId";
            supplierNameDropdownList.DataBind();
            supplierNameDropdownList.Items.Insert(0, new ListItem("Select", "0"));

        }

        [WebMethod]
        public static object SavePayment(Payment payment)
        {
            ReturnToClient returnToClient = new ReturnToClient();
            string message = "";
            var id = payment.PayInvoiceNo;
            string purYear = "PAY-" + DateTime.Now.Year + "";
            if (id == "0")
            {
                if (aPaymentManager.InvPrefixList(purYear).Count().Equals(0))
                {
                    payment.PayInvoiceNo = "PAY-" + DateTime.Now.Year + "-0000001";
                }
                else
                {
                    var lastPaymentId = aPaymentManager.InvPrefixList(purYear).Max();
                    payment.PayInvoiceNo = customMethod.GenerateInvNo("PAY", lastPaymentId.PayInvoiceNo);
                }


            }
            if (payment.PayType == "Cash")
            {
                payment.BankName = "N/A";
                payment.AccountNo = "N/A";
                payment.ChequeNo = "N/A";
                payment.ChequeDate = DateTime.Now;
            }
            payment.EntryDate = DateTime.Now;
            payment.EntryBy = "SBOffice";
            payment.EntryIp = customMethod.GetIpAddress();
            payment.Department = "Office";
            payment.CompanyName = "SB Super Deluxe";
            int errorCount = aPaymentManager.SavePayment(payment);
            if (errorCount > 0)
            {
                returnToClient.Message = aPaymentManager.Message;
                returnToClient.PayInvoiceNo = "";
            }
            else
            {
                returnToClient.Message = aPaymentManager.Message;
                returnToClient.PayInvoiceNo = payment.PayInvoiceNo;
            }


            return returnToClient;
        }
        [WebMethod]
        public static Payment LoadPaymentData(String payId)
        {
            Payment pData = aPaymentManager.GetAll().Find(f => f.PayInvoiceNo == payId);
            pData.DatDate = string.Format("{0:MM/dd/yyyy}", pData.PayDate);
            return pData;
        }
        public class ReturnToClient
        {
            public string PayInvoiceNo { get; set; }
            public string Message { get; set; }
        }

    }
}