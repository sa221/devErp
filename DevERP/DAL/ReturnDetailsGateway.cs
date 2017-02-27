using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevERP.Model;
using DevERP.Others;

namespace SBBusMS.DAL
{
    public class ReturnDetailsGateway : ConnectionGateway
    {
        public int DeleteReturnItem(int pdId)
        {
            Query = "Delete From tblReturnDetails where PDId=@PDId";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PDId", pdId);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public List<PurchesItem> GetAllPurchaseItem()
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

        public int UpdateItem(PurchaseDetails purchaseDetails)
        {
            Query = @"UPDATE tblReturnDetails SET PurchaseInvNo=@PurchaseInvNo,PartsCode=@PartsCode,Quantity=@Qty,Unit=@Unit,Rate=@Rate,Total=@Total,Department=@Department,CompanyName=@CompanyName WHERE PDId=@PDId";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@PDId", SqlDbType.Int).Value = purchaseDetails.PDId;
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchaseDetails.PurchaseInvNo;
            Command.Parameters.Add("@PartsCode", SqlDbType.VarChar).Value = purchaseDetails.PartsCode;
            Command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = purchaseDetails.Quantity;
            Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchaseDetails.Unit;
            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchaseDetails.Rate;
            Command.Parameters.Add("@Total", SqlDbType.Decimal).Value = purchaseDetails.Total;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchaseDetails.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchaseDetails.CompanyName;
           int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }

        public int SaveItem(PurchaseDetails purchesDetails)
        {
            Query = "INSERT INTO tblReturnDetails (PurchaseInvNo,PartsCode,Quantity,Unit,Rate,Total,Department,CompanyName) VALUES (@PurchaseInvNo,@PartsCode,@Quantity,@Unit,@Rate,@Total,@Department,@CompanyName) ";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchesDetails.PurchaseInvNo;
            Command.Parameters.Add("@PartsCode", SqlDbType.VarChar).Value = purchesDetails.PartsCode;
            Command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = purchesDetails.Quantity;
            Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchesDetails.Unit;
            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchesDetails.Rate;
            Command.Parameters.Add("@Total", SqlDbType.Decimal).Value = purchesDetails.Total;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesDetails.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesDetails.CompanyName;
            Connection.Open();
            int rowAffected= Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<PurchaseDetails> GetAllReturnDetails(string invoiceNo)
        {
            List<PurchaseDetails> InvList = new List<PurchaseDetails>();
            Query = @"select pd.PDId,pd.PurchaseInvNo,pd.Quantity,pd.Unit,pd.Rate,pd.Total,i.PartsName,pd.PartsCode,pd.Department,pd.CompanyName from tblReturnDetails pd ,tblPartsInfo i where pd.PurchaseInvNo=@PurchaseInvNo And pd.PartsCode=i.PartsCode ";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = invoiceNo;
            Connection.Open();
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                PurchaseDetails aPurchesDetails = new PurchaseDetails();
                aPurchesDetails.PDId = Convert.ToInt32(Reader["PDId"]);
                aPurchesDetails.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchesDetails.PartsName = Reader["PartsName"].ToString();
                aPurchesDetails.PartsCode = Reader["PartsCode"].ToString();
                aPurchesDetails.Rate = Convert.ToDouble(Reader["Rate"]);
                aPurchesDetails.Unit = Reader["Unit"].ToString();
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Quantity"]);
                aPurchesDetails.Total = Convert.ToDouble(Reader["Total"]);
                aPurchesDetails.Department = Reader["Department"].ToString();
                aPurchesDetails.CompanyName = Reader["CompanyName"].ToString();
                InvList.Add(aPurchesDetails);

            }
            Reader.Close();
            Connection.Close();
            return InvList;
        }
    }
}