using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.DAL
{
    public class PaymentGateway:ConnectionGateway
    {
        public List<Payment> GetAll()
        {        
            List<Payment> paymentList=new List<Payment>();
            Query = @"Select p.PayInvoiceNo,p.PayDate,s.SupplierId,s.OrganizationName,p.Remarks,p.PayType,p.BankName,p.AccountNo,p.ChequeNo,p.ChequeDate,
            p.PayAmount,p.Department,p.CompanyName
            from tblPayment p,tblSuppliers s where  s.SupplierId=p.SupplierId";
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Payment aPayment=new Payment();
                aPayment.PayInvoiceNo = Reader["PayInvoiceNo"].ToString();
                aPayment.PayDate = Convert.ToDateTime(Reader["PayDate"]);
                aPayment.SupplierId = Reader["SupplierId"].ToString();
                aPayment.OrganizationName = Reader["OrganizationName"].ToString();
                aPayment.Remarks = Reader["Remarks"].ToString();
                aPayment.PayType = Reader["PayType"].ToString();
                aPayment.BankName = Reader["BankName"].ToString();     
                aPayment.AccountNo = Reader["AccountNo"].ToString();
                aPayment.ChequeNo = Reader["ChequeNo"].ToString();
                aPayment.ChequeDate = Convert.ToDateTime(Reader["ChequeDate"]);               
                aPayment.PayAmount = Convert.ToDouble(Reader["PayAmount"]);
                aPayment.Department = Reader["Department"].ToString();
                aPayment.CompanyName = Reader["CompanyName"].ToString();
                    paymentList.Add(aPayment);
            }
            Reader.Close();
            Connection.Close();
            return paymentList;

        }

        public bool UpdatePayment(Payment payment)
        {
            Query = "Update tblPayment Set PayDate=@PayDate,SupplierId=@SupplierId,Remarks=@Remarks," +
                    "PayType=@PayType,BankName=@BankName,AccountNo=@AccountNo,ChequeDate=@ChequeDate,PayAmount=@PayAmount WHERE PayInvoiceNo=@PayInvoiceNo";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PayInvoiceNo", payment.PayInvoiceNo);
            Command.Parameters.AddWithValue("@PayDate", payment.PayDate);
            Command.Parameters.AddWithValue("@SupplierId", payment.SupplierId);
            Command.Parameters.AddWithValue("@Remarks", payment.Remarks);
            Command.Parameters.AddWithValue("@PayType", payment.PayType);
            Command.Parameters.AddWithValue("@BankName", payment.BankName);
            Command.Parameters.AddWithValue("@AccountNo", payment.AccountNo);
            Command.Parameters.AddWithValue("@ChequeDate", payment.ChequeDate);
            Command.Parameters.AddWithValue("@PayAmount", payment.PayAmount);
            Connection.Open();
            int rowCount = Command.ExecuteNonQuery();
            Connection.Close();
            return rowCount > 0;

        }

        public bool SavePayment(Payment payment)
        {
            int rowCount = 0;
            Query = @"INSERT INTO tblPayment" +
                    "(PayInvoiceNo,PayDate,SupplierId,Remarks,PayType,BankName,AccountNo,ChequeNo,ChequeDate,PayAmount,EntryBy,EntryDate,EntryIp,Department,CompanyName) " +
                    "VALUES(@PayInvoiceNo,@PayDate,@SupplierId,@Remarks,@PayType,@BankName,@AccountNo,@ChequeNo,@ChequeDate,@PayAmount,@EntryBy,@EntryDate,@EntryIp,@Department,@CompanyName)";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PayInvoiceNo", payment.PayInvoiceNo);
            Command.Parameters.AddWithValue("@PayDate", payment.PayDate);
            Command.Parameters.AddWithValue("@SupplierId", payment.SupplierId);
            Command.Parameters.AddWithValue("@Remarks", payment.Remarks);
            Command.Parameters.AddWithValue("@PayType", payment.PayType);
            Command.Parameters.AddWithValue("@BankName", payment.BankName);
            Command.Parameters.AddWithValue("@AccountNo", payment.AccountNo);
            Command.Parameters.AddWithValue("@ChequeNo", payment.ChequeNo);
            Command.Parameters.AddWithValue("@ChequeDate", payment.ChequeDate);
            Command.Parameters.AddWithValue("@PayAmount", payment.PayAmount);
            Command.Parameters.AddWithValue("@EntryBy", payment.EntryBy);
            Command.Parameters.AddWithValue("@EntryDate", payment.EntryDate);
            Command.Parameters.AddWithValue("@EntryIp", payment.EntryIp);
            Command.Parameters.AddWithValue("@Department", payment.Department);
            Command.Parameters.AddWithValue("@CompanyName", payment.CompanyName);
            Connection.Open();
             rowCount = Command.ExecuteNonQuery();
            Connection.Close();
            return rowCount > 0;

        }

        public List<Payment> GetAllPaymentInvoice()
        {
            List<Payment> paymentList=new List<Payment>();
            Query = "select * from tblPayment";
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Payment aPayment = new Payment();
                aPayment.PayInvoiceNo = Reader["PayInvoiceNo"].ToString();                
                paymentList.Add(aPayment);
            }
            Reader.Close();
            Connection.Close();
            return paymentList;   
        }

        //public List<Payment> GetAllBank()
        //{
        //   List<Payment> bankList=new List<Payment>();
        //   Query = "select p.BankName,ahs.VarHeadName from tblPayment p, AccHeadSetup ahs where p.BankName=ahs.NumHeadID";
        //   Command.CommandText = Query;
        //   Connection.Open();
        //   Reader = Command.ExecuteReader();
        //   while (Reader.Read())
        //   {
        //       Payment aPayment = new Payment();
        //       aPayment.BankName = Reader["BankName"].ToString();
        //       aPayment.VarHeadName = Reader["VarHeadName"].ToString();
        //       bankList.Add(aPayment);
        //   }
        //   Reader.Close();
        //   Connection.Close();
        //    return bankList;
        //}

        public List<Payment> InvPrefixList(string purYear)
        {
            List<Payment> prList = new List<Payment>();
            Query =
                "SELECT PayInvoiceNo FROM TblPayment WHERE SUBSTRING(PayInvoiceNo, 1, 8)=@Prefix";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Prefix", purYear);
            Reader = Command.ExecuteReader();
            Payment pre = new Payment();
            while (Reader.Read())
            {
                pre.PayInvoiceNo = Reader["PayInvoiceNo"].ToString();

                prList.Add(pre);
            }
            Reader.Close();
            Connection.Close();
            return prList;
        } 
    }
}