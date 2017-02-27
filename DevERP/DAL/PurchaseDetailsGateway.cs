using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.DAL
{
    public class PurchaseDetailsGateway : ConnectionGateway
    {
        StockGateway aStockGateway=new StockGateway();

        public PurchaseDetails GetQtyBySupplierName(string supName)
        {
            Query = @"select sum(pd.Qty) qty,p.OrganizationName from tblPurchaseDetails pd,tblPurchase p where pd.PurchaseInvNo = p.PurchaseInvNo 
              and p.SupplierName =@supName group by p.SupplierName";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@supName", SqlDbType.VarChar).Value = supName;
            Connection.Open();
            Reader = Command.ExecuteReader();
            PurchaseDetails aPurchesDetails = null;
            if (Reader.Read())
            {
                 aPurchesDetails= new PurchaseDetails();
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Qty"]);
                aPurchesDetails.PurchaseInvNo = Reader["SupplierId"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return aPurchesDetails;

        }

        public double GetQtyByItemCode(string iCode,string invNo)
        {
            Query = @"select sum( pd.Qty) qty,pd.PartsCode  from tblPurchaseDetails pd where  pd.PurchaseInvNo=@invNo
               and pd.ItemCode=@iCode group by pd.PartsCode";

            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@invNo", SqlDbType.VarChar).Value = invNo;
            Command.Parameters.Add("@iCode", SqlDbType.VarChar).Value = iCode;
            Connection.Open();
            Reader = Command.ExecuteReader();
            double Qty = 0;
            if (Reader.Read())
            {
              Qty = Convert.ToDouble(Reader["Qty"]);
            }
            Reader.Close();
            Connection.Close();
            return Qty;

        }

      

        public int DeletePurchseItem(int gId,string invNo = "")
        {
            Query = "DELETE  FROM tblPurchaseDetails WHERE ";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            if (invNo != "")
            {
                Command.CommandText += "PurchaseInvNo=@PurchaseInvNo";
                Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = invNo;
            }
            else
            {
                Command.CommandText += "PDId=@gId";
                Command.Parameters.Add("@gId", SqlDbType.Int).Value = gId;
            }
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public void UpdateItem(PurchaseDetails purchesItem)
        {

            Query = @"UPDATE tblPurchaseDetails SET PurchaseInvNo=@PurchaseInvNo,PartsCode=@PartsCode,Quantity=@Qty,Unit=@Unit,Rate=@Rate,Total=@Total,Department=@Department,CompanyName=@CompanyName WHERE PDId=@PDId";
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@PDId", SqlDbType.Int).Value = purchesItem.PDId;
            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchesItem.PurchaseInvNo;
            Command.Parameters.Add("@PartsCode", SqlDbType.VarChar).Value = purchesItem.PartsCode;
            Command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = purchesItem.Quantity;
            Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchesItem.Unit;
            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchesItem.Rate;
            Command.Parameters.Add("@Total", SqlDbType.Decimal).Value = purchesItem.Total;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesItem.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesItem.CompanyName;
            Command.ExecuteNonQuery();
            Connection.Close();
            

        }

        public void SaveItem(PurchaseDetails purchesItem)
        {
            

            Query = "INSERT INTO tblPurchaseDetails (PurchaseInvNo,PartsCode,Quantity,Unit,Rate,Total,Department,CompanyName) VALUES (@PurchaseInvNo,@PartsCode,@Quantity,@Unit,@Rate,@Total,@Department,@CompanyName) ";
            Command.CommandText = Query;

            Command.Parameters.Clear();

            Command.Parameters.Add("@PurchaseInvNo", SqlDbType.VarChar).Value = purchesItem.PurchaseInvNo;
            Command.Parameters.Add("@PartsCode", SqlDbType.VarChar).Value = purchesItem.PartsCode;
            Command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = purchesItem.Quantity;
            Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchesItem.Unit;
            Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchesItem.Rate;
            Command.Parameters.Add("@Total", SqlDbType.Decimal).Value = purchesItem.Total;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesItem.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesItem.CompanyName;
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
       

        }

        public List<PurchaseDetails> GetAll()
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<PurchaseDetails> pDetails = new List<PurchaseDetails>();
            //Query = "SELECT * FROM tblPurchaseDetails WHERE Department=@Department and CompanyName=@CompanyName";
            Query = "SELECT * FROM tblPurchaseDetails";
            Command.CommandText = Query;
            Connection.Open();
            //Command.Parameters.Clear();
            //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "";
            //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = "";
            
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                PurchaseDetails aPurchesDetails = new PurchaseDetails();
                aPurchesDetails.PDId = Convert.ToInt32(Reader["PDId"]);
                aPurchesDetails.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchesDetails.PartsCode = Reader["PartsCode"].ToString();
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Quantity"]);
                aPurchesDetails.Unit = Reader["Unit"].ToString();
                aPurchesDetails.Rate = Convert.ToDouble(Reader["Rate"]);
                aPurchesDetails.Total = Convert.ToDouble(Reader["Total"]);
                aPurchesDetails.Department = Reader["Department"].ToString();
                aPurchesDetails.CompanyName = Reader["CompanyName"].ToString();
                pDetails.Add(aPurchesDetails);
            }
            Reader.Close();
            Connection.Close();
            return pDetails;
        }

        public List<PurchesItem> GetPurchesItem()
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<PurchesItem> InvList = new List<PurchesItem>();
            //Query = @"select pd.PDId,pd.PurchaseInvNo,pd.Quantity,pd.Unit,pd.Rate,pd.Total,i.PartsCode,i.PartsName,i.GroupName,i.CategoryName from tblPurchaseDetails pd ,tblPartsInfo i where pd.PartsCode=i.PartsCode and pd.Department=@Department and  pd.CompanyName=@CompanyName";
            Query = @"select pd.PDId,pd.PurchaseInvNo,pd.Quantity,pd.Unit,pd.Rate,pd.Total,i.PartsCode,i.PartsName,i.GroupName,i.CategoryName from tblPurchaseDetails pd ,tblPartsInfo i where pd.PartsCode=i.PartsCode ";
            Command.CommandText = Query;
            Connection.Open();
            //Command.Parameters.Clear();
            //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "";
            //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = "";

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                PurchesItem aPurchesDetails = new PurchesItem();
                aPurchesDetails.PDId = Convert.ToInt32(Reader["PDId"]);
                aPurchesDetails.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchesDetails.GroupId = (int) Reader["GroupId"];
                aPurchesDetails.CategoryId = (int) Reader["CategoryId"];
                aPurchesDetails.PartsCode = Reader["PartsCode"].ToString();
                aPurchesDetails.PartsName = Reader["PartsName"].ToString();
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

        public double GetPurchseDetailQtyByItemCode(string partsCode, string invoiceNo)
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();

            Query = @"select sum( pd.Quantity) qty,pd.PartsCode  from tblPurchaseDetails pd where  pd.PurchaseInvNo=@invoiceNo
               and pd.ItemCode=@partsCode group by pd.PartsCode";

            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@invoiceNo", SqlDbType.VarChar).Value = invoiceNo;
            Command.Parameters.Add("@partsCode", SqlDbType.VarChar).Value = partsCode;
            //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = department;
            //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = company;
            Connection.Open();
            Reader = Command.ExecuteReader();
            double Qty = 0;
            if (Reader.Read())
            {
                Qty = Convert.ToDouble(Reader["Quantity"]);
            }
            Reader.Close();
            Connection.Close();
            return Qty;
        }

        public List<PurchaseDetails> GetAllPurchaseDetails()
        {
            List<PurchaseDetails> invList = new List<PurchaseDetails>();
            Query = @"select pd.PDId,pd.PurchaseInvNo,pd.Quantity,pd.Unit,pd.Rate,pd.Total,i.PartsName from tblPurchaseDetails pd ,tblPartsInfo i where pd.PartsCode=i.PartsCode ";
            Command.CommandText = Query;
            Connection.Open();
            //Command.Parameters.Clear();
            //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "ABC";
            //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = "DEF";

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                PurchaseDetails aPurchesDetails = new PurchaseDetails();
                aPurchesDetails.PDId = Convert.ToInt32(Reader["PDId"]);
                aPurchesDetails.PurchaseInvNo = Reader["PurchaseInvNo"].ToString();
                aPurchesDetails.PartsName = Reader["PartsName"].ToString();
                aPurchesDetails.Rate = Convert.ToDouble(Reader["Rate"]);
                aPurchesDetails.Unit = Reader["Unit"].ToString();
                aPurchesDetails.Quantity = Convert.ToDouble(Reader["Quantity"]);
                aPurchesDetails.Total = Convert.ToDouble(Reader["Total"]);
                aPurchesDetails.Department = Reader["Department"].ToString();
                aPurchesDetails.CompanyName = Reader["CompanyName"].ToString();
                invList.Add(aPurchesDetails);

            }
            Reader.Close();
            Connection.Close();
            return invList;
        }

        public int DeleteItem(int pdId)
        {
            PurchaseDetails purchaseDetails = GePurchaseDetailsByPdId(pdId);
            int rowAffected;
            Query = "Delete From tblPurchaseDetails where PDId=@PDId";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PDId", SqlDbType.Int).Value=pdId;
            Connection.Open();
             rowAffected = Command.ExecuteNonQuery();
             Connection.Close();
            if (rowAffected>0)
            {
                rowAffected = UpdateStockQtyWhenPurchaseDelted(purchaseDetails);

            }
            
            return rowAffected;
        }

        public PurchaseDetails GePurchaseDetailsByPdId(int pdId)
        {
            PurchaseDetails purchaseDetails = new PurchaseDetails();
            Query = "SELECT * FROM tblPurchaseDetails pd Where pd.PDId=@PDId";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PDId", SqlDbType.Int).Value = pdId;
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    purchaseDetails.PartsCode = Reader["PartsCode"].ToString();
                    purchaseDetails.Department = Reader["Department"].ToString();
                    purchaseDetails.CompanyName = Reader["CompanyName"].ToString();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                Reader.Dispose();
                Connection.Close();
            }
            return purchaseDetails;
        }


        public void UpdateStock(PurchaseDetails purchesItem)
        {          
            bool isPartExist = IsPartsExistsInStock(purchesItem);
            
            if (isPartExist)
            {
                //decimal totalPartsQty = GetPartsTotalQtyByPartsCode(purchesItem.PartsCode, purchesItem.Department,
                // purchesItem.CompanyName);
                //decimal totalPartsQty=aStockGateway.UpdateStock()
                //Query =
                //    "UPDATE Stock SET ItemQty=@ItemQty,Unit=@Unit,Rate=@Rate  WHERE ItemCode=@ItemCode AND Department=@Department AND CompanyName=@CompanyName";
                //Command.CommandText = Query;                
                //Command.Parameters.Clear();
                //Command.Parameters.Add("@ItemQty", SqlDbType.Decimal).Value = totalPartsQty;
                //Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchesItem.Unit;
                //Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchesItem.Rate;
                //Command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = purchesItem.PartsCode;
                //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesItem.Department;
                //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesItem.CompanyName;

                aStockGateway.UpdateStock(purchesItem.PartsCode,purchesItem.Department,purchesItem.CompanyName);
            }
            else
            {
                Query = "INSERT INTO Stock(ItemCode,ItemQty,Unit,Rate,Department,CompanyName) VALUES(@ItemCode,@ItemQty,@Unit,@Rate,@Department,@CompanyName)";
                Command.CommandText = Query;

                Command.Parameters.Clear();

                Command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = purchesItem.PartsCode;
                Command.Parameters.Add("@ItemQty", SqlDbType.Decimal).Value = purchesItem.Quantity;
                Command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = purchesItem.Unit;
                Command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = purchesItem.Rate;
                Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesItem.Department;
                Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesItem.CompanyName;

            }

            try
            {
                Connection.Open();
                Command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                Connection.Close();
            }
            
            


        }

        private int UpdateStockQtyWhenPurchaseDelted(PurchaseDetails purchesItem)
        {
            int rowAffected;
            decimal totalPartsQty = GetPartsTotalQtyByPartsCode(purchesItem.PartsCode, purchesItem.Department,
                purchesItem.CompanyName);
            Query =
                "UPDATE Stock SET ItemQty=@ItemQty  WHERE ItemCode=@ItemCode AND Department=@Department AND CompanyName=@CompanyName";
            Command.CommandText = Query;

            Command.Parameters.Clear();


            Command.Parameters.Add("@ItemQty", SqlDbType.Decimal).Value = totalPartsQty;
            Command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = purchesItem.PartsCode;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = purchesItem.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = purchesItem.CompanyName;
            try
            {
                Connection.Open();
              rowAffected=  Command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                Connection.Close();
            }
            return rowAffected;

        }

        private bool IsPartsExistsInStock(PurchaseDetails purchesItem)
        {
            bool isExists;
            Query = "SELECT * FROM Stock s WHERE s.ItemCode=@PartsCode AND s.Department=@Department AND s.CompanyName=@CompanyName";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PartsCode", purchesItem.PartsCode);
            Command.Parameters.AddWithValue("@Department", purchesItem.Department);
            Command.Parameters.AddWithValue("@CompanyName", purchesItem.CompanyName);
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    isExists = true;
                }
                else
                {
                    isExists = false;
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                Reader.Dispose();
                Connection.Close();
            }
            return isExists;
        }

        private decimal GetPartsTotalQtyByPartsCode(string partsCode,string deptName,string companyName)
        {
            decimal qty = 0;
            Query = "SELECT IsNull(sum(Quantity),0) As Quantity  FROM tblPurchaseDetails  Where PartsCode=@PartsCode AND Department=@Department AND CompanyName=@CompanyName";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PartsCode", partsCode);
            Command.Parameters.AddWithValue("@Department", deptName);
            Command.Parameters.AddWithValue("@CompanyName", companyName);
            try
            {
                Connection.Open();

                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    
                    qty = Convert.ToDecimal(Reader["Quantity"].ToString());
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                Reader.Dispose();
                Connection.Close();
            }
            return qty;
        }

       
    }
}