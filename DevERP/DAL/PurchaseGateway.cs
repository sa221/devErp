using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DevERP.Model;
using DevERP.Others;

namespace SBBusMS.DAL
{
    public class PurchaseGateway : ConnectionGateway
    {
        public string DeletePurches(string purchaseInvNo)
        {
            Query = "DELETE FROM tblPurchase WHERE PurchaseInvNo='" + purchaseInvNo + "'";


            int rowAfected = 0;
            try
            {
                Connection.Open();
                Command.CommandText = Query;
                rowAfected = Command.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                return exception.Message;

            }
            finally
            {
                Connection.Close();
            }

            if (rowAfected > 0)
            {
                return "Data Deleted";
            }
            return "No Data to be Delete";
        }
        // method for getting Purchase Invoice Substring Value
        public string PurchaseInvId(string purchaseInvNo)
        {
            Query ="SELECT  MAX(SUBSTRING(PurchaseInvNo, 10, 16)) As Id FROM tblPurchase WHERE SUBSTRING(PurchaseInvNo, 1, 8) =@PInvId";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PInvId", purchaseInvNo);
            Reader = Command.ExecuteReader();
            string invNo = "";
            if (Reader.Read())
            {

                invNo = Reader["Id "].ToString();

            }
            Reader.Close();
            Connection.Close();
            return invNo;
        }

        public List<Purchase> GetAllPurchaseInvNo()
        {
            List<Purchase>  purInvNoList=new List<Purchase>();
            Query = "SELECT PurchaseInvNo FROM TblPurchase";
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
               Purchase aPurchase=new Purchase();
                aPurchase.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                purInvNoList.Add(aPurchase);
            }
            Reader.Close();
            Connection.Close();
            return purInvNoList;
        }

        public List<Purchase> InvPrefixList(string purYear)
        {
            List<Purchase> prList = new List<Purchase>();
            Query =
                "SELECT PurchaseInvNo FROM TblPurchase WHERE SUBSTRING(PurchaseInvNo, 1, 8)=@Prefix";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Prefix",purYear);
            Reader = Command.ExecuteReader();
            Purchase pre = new Purchase();
            while (Reader.Read())
            {
                pre.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                
                prList.Add(pre);
            }
            Reader.Close();
            Connection.Close();
            return prList;
        } 



        public bool UpdatePurchase(Purchase purches)
        {
            Query =
                "UPDATE  tblPurchase SET PurchaseInvNo=@PurchaseInvNo,PurchaseDate=@PurchaseDate,SupplierId=@SupplierId,ChalanNo=@ChalanNo,Remarks=@Remarks,Amount=@Amount,Approve=@Approve,Department=@Department,CompanyName=@CompanyName WHERE PurchaseInvNo=@PurchaseInvNo";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            //Command.Parameters.Add("@Id", SqlDbType.Int).Value = aPurches.Id;
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purches.PurchaseInvNo;
            Command.Parameters.Add("@Approve", SqlDbType.VarChar).Value = "N";
            Command.Parameters.Add("@PurchaseDate", SqlDbType.VarChar).Value = purches.PurchaseDate;
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value = purches.SupplierId;
            Command.Parameters.Add("@ChalanNo", SqlDbType.VarChar).Value = purches.ChalanNo;
            Command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = purches.Remarks;
            Command.Parameters.Add("@Amount", SqlDbType.Float).Value = purches.Amount;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purches.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purches.CompanyName;

            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected > 0;
        }

        public bool SavePurchase(Purchase purches)
        {
            Query =
                "INSERT INTO tblPurchase (PurchaseInvNo,PurchaseDate,SupplierId,ChalanNo,Remarks,Amount,Department,CompanyName) VALUES(@PurchaseInvNo,@PurchaseDate,@SupplierId,@ChalanNo,@Remarks,@Amount,@Department,@CompanyName)";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purches.PurchaseInvNo;
            Command.Parameters.Add("@PurchaseDate", SqlDbType.Date).Value = purches.PurchaseDate;
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value = purches.SupplierId;
            Command.Parameters.Add("@ChalanNo", SqlDbType.VarChar).Value = purches.ChalanNo;
            Command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = purches.Remarks;
            Command.Parameters.Add("@Amount", SqlDbType.Float).Value = purches.Amount;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purches.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purches.CompanyName;


            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected > 0;
        }



       

        public List<Purchase> GetAll()
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<Purchase> purcheses = new List<Purchase>();
            Query = "select p.Id,p.PurchaseInvNo,p.PurchaseDate,p.SupplierId,s.OrganizationName,p.Remarks,p.Amount," +
                    "p.EntryBy,p.Department,p.CompanyName from tblPurchase p,tblSuppliers s where " +
                    "p.SupplierId=s.SupplierId";
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Purchase aPurchase = new Purchase();
                aPurchase.Id = Convert.ToInt32(Reader["Id"]);
                aPurchase.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchase.PurchaseDate = Convert.ToDateTime(Reader["PurchaseDate"]);
                aPurchase.SupplierId = Reader["SupplierId"].ToString();
                aPurchase.OrganizationName = Reader["OrganizationName"].ToString();
                aPurchase.Remarks = Reader["Remarks"].ToString();
                aPurchase.Amount = Convert.ToDouble(Reader["Amount"]);
                aPurchase.EntryBy = Reader["EntryBy"].ToString();
                aPurchase.Department = Reader["Department"].ToString();
                aPurchase.CompanyName = Reader["CompanyName"].ToString();
                purcheses.Add(aPurchase);
            }
            Reader.Close();
            Connection.Close();
            return purcheses;
        }

        public List<Purchase> GetAllPurchaseInfo()
        {

            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<Purchase> purList = new List<Purchase>();
            Query = @"select p.Id, p.PurchaseInvNo,p.PurchaseDate,p.Remarks,p.Amount,p.Department,p.CompanyName, s.SupplierId,s.OrganizationName  from tblpurchase p,tblSuppliers s where p.SupplierId=s.SupplierId";
            Command.CommandText = Query;
            Connection.Open();
            
            Reader = Command.ExecuteReader();
            Purchase aPurches = new Purchase();
            while (Reader.Read())
            {
                aPurches = new Purchase();
                aPurches.Id = Convert.ToInt32(Reader["Id"]);
                aPurches.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurches.PurchaseDate =Convert.ToDateTime( Reader["PurchaseDate"]);
                aPurches.SupplierId = Reader["SupplierId"].ToString();
                aPurches.OrganizationName = Reader["OrganizationName"].ToString();
                aPurches.ChalanNo = Reader["ChalanNo"].ToString();
                aPurches.Remarks = Reader["Remarks"].ToString();
                aPurches.Amount = Convert.ToDouble(Reader["Amount"]);
                aPurches.Department = Reader["Department"].ToString();
                aPurches.CompanyName = Reader["CompanyName"].ToString();
                purList.Add(aPurches);
            }
            Reader.Close();
            Connection.Close();
            return purList;
        }

        public int DeletePrchaseInv(string invNo)
        {
            Query = "DELETE FROM tblPurchase WHERE PurchaseInvNo = @PurchaseInvNo";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = invNo;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<PurchesItem> GetAllPurchaseItem()
        {
            Query = @"select p.PurchaseInvNo,p.PurchaseDate,p.ChalanNo, s.OrganizationName,i.PartsName,pd.PartsCode,pd.PDId,pd.Quantity,pd.Unit,pd.Rate,pd.Total from tblPurchase p, tblPurchaseDetails pd,tblPartsInfo i, tblSuppliers s  where p.PurchaseInvNo=pd.PurchaseInvNo and p.SupplierId=s.SupplierId and i.PartsCode=pd.PartsCode";
            Command.CommandText = Query;
            Connection.Open();
            List<PurchesItem> InvList = new List<PurchesItem>();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                PurchesItem aPurchesDetails = new PurchesItem();
                aPurchesDetails.PDId = Convert.ToInt32(Reader["PDId"]);
                aPurchesDetails.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchesDetails.PurchaseDate = Convert.ToDateTime(Reader["PurchaseDate"].ToString());
                aPurchesDetails.ChalanNo = Reader["ChalanNo"].ToString();
                aPurchesDetails.OrganizationName = Reader["OrganizationName"].ToString();
                aPurchesDetails.PartsName = Reader["PartsName"].ToString();
                aPurchesDetails.PartsCode = Reader["PartsCode"].ToString();
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Quantity"]);
                aPurchesDetails.Rate = Convert.ToDouble(Reader["Rate"]);
                aPurchesDetails.Unit = Reader["Unit"].ToString();
                aPurchesDetails.Total = Convert.ToDouble(Reader["Total"]);
                //aPurchesDetails.Department = Reader["Department"].ToString();
                //aPurchesDetails.CompanyName = Reader["CompanyName"].ToString();
                InvList.Add(aPurchesDetails);

            }
            Reader.Close();
            Connection.Close();
            return InvList;
        }

        public int DeleteItem(int pdId)
        {
            Query = "Delete From tblPurchaseDetails where PDId=@PDId";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@CustomerId", pdId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public List<Purchase> GetAllSupplier()
        {
            List<Purchase> purcheses = new List<Purchase>();
            Query = "SELECT  s.SupplierId , s.OrganizationName FROM   tblSuppliers s ";

            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Purchase aPurchase = new Purchase();                               
                aPurchase.SupplierId = Reader["SupplierId"].ToString();
                aPurchase.OrganizationName = Reader["OrganizationName"].ToString();                
                purcheses.Add(aPurchase);
            }
            Reader.Close();
            Connection.Close();
            return purcheses;
        }
    }
}