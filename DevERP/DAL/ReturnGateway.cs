using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using DevERP.Others;
using DevERP.Model;

namespace DevERP.DAL
{
    public class ReturnGateway:ConnectionGateway
    {
        public bool UpdateReturn(Purchase purchase)
        {
            Query = "UPDATE  tblReturn SET PurchaseInvNo=@PurchaseInvNo,PurchaseDate=@PurchaseDate,SupplierId=@SupplierId,ChalanNo=@ChalanNo,Remarks=@Remarks,Amount=@Amount,Approve=@Approve,Department=@Department,CompanyName=@CompanyName WHERE PurchaseInvNo=@PurchaseInvNo";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            //Command.Parameters.Add("@Id", SqlDbType.Int).Value = aPurches.Id;
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchase.PurchaseInvNo;
            Command.Parameters.Add("@Approve", SqlDbType.VarChar).Value = "N";
            Command.Parameters.Add("@PurchaseDate", SqlDbType.VarChar).Value = purchase.PurchaseDate;
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value = purchase.SupplierId;
            Command.Parameters.Add("@ChalanNo", SqlDbType.VarChar).Value = purchase.ChalanNo;
            Command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = purchase.Remarks;
            Command.Parameters.Add("@Amount", SqlDbType.Float).Value = purchase.Amount;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchase.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchase.CompanyName;
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected > 0;
        }

        public List<Purchase> InvPrefixList(string purYear)
        {
            List<Purchase> prList = new List<Purchase>();
            Query =
                "SELECT PurchaseInvNo FROM TblReturn WHERE SUBSTRING(PurchaseInvNo, 1, 8)=@Prefix";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@Prefix", purYear);
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

        public bool SaveReturn(Purchase purchase)
        {
            Query ="INSERT INTO tblReturn (PurchaseInvNo,PurchaseDate,SupplierId,ChalanNo,Remarks,Amount,Department,CompanyName) VALUES(@PurchaseInvNo,@PurchaseDate,@SupplierId,@ChalanNo,@Remarks,@Amount,@Department,@CompanyName)";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchase.PurchaseInvNo;
            Command.Parameters.Add("@PurchaseDate", SqlDbType.VarChar).Value = purchase.PurchaseDate;
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value = purchase.SupplierId;
            Command.Parameters.Add("@ChalanNo", SqlDbType.VarChar).Value = purchase.ChalanNo;
            Command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = purchase.Remarks;
            Command.Parameters.Add("@Amount", SqlDbType.Float).Value = purchase.Amount;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchase.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchase.CompanyName;


            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected > 0; 
        }

        public List<Purchase> GetAll()
        {
            List<Purchase> purcheses = new List<Purchase>();
            Query = "SELECT * FROM tblReturn";
            Command.Parameters.Clear();   
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Purchase aPurches = new Purchase();
                aPurches.Id = Convert.ToInt32(Reader["Id"]);
                aPurches.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurches.PurchaseDate =Convert.ToDateTime( Reader["PurchaseDate"]);
                aPurches.SupplierId = Reader["SupplierId"].ToString();
                aPurches.ChalanNo = Reader["ChalanNo"].ToString();
                aPurches.Remarks = Reader["Remarks"].ToString();
                aPurches.Amount = Convert.ToDouble(Reader["Amount"]);
                aPurches.Department = Reader["Department"].ToString();
                aPurches.CompanyName = Reader["CompanyName"].ToString();
                purcheses.Add(aPurches);
            }
            Reader.Close();
            Connection.Close();
            return purcheses;
        }
        
        public List<PurchesItem> GetAllReturnItem()
        {
            Query = @"select p.PurchaseInvNo,p.PurchaseDate,p.ChalanNo, s.OrganizationName,i.PartsName,pd.Quantity,pd.Unit,pd.Rate,pd.Total from tblReturn p, tblReturnDetails pd,tblPartsInfo i, tblSuppliers s  where p.PurchaseInvNo=pd.PurchaseInvNo and p.SupplierId=s.SupplierId and i.PartsCode=pd.PartsCode";
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
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Quantity"]);
                aPurchesDetails.Rate = Convert.ToDouble(Reader["Rate"]);
                aPurchesDetails.Unit = Reader["Unit"].ToString();
                aPurchesDetails.Total = Convert.ToDouble(Reader["Total"]);
                InvList.Add(aPurchesDetails);

            }
            Reader.Close();
            Connection.Close();
            return InvList;
        }
    }
}