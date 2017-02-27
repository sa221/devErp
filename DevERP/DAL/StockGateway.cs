using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.DAL
{
    public class StockGateway:ConnectionGateway
    {

        public List<StockData> GetAll()
        {
            List<StockData> stockList = new List<StockData>();
            Query = "select p.PartsName,s.ItemCode,s.ItemQty,s.Rate,s.Unit,s.Department,s.CompanyName,Total=(s.ItemQty*s.Rate) from Stock s, tblPartsInfo p where s.ItemCode=p.PartsCode";
            Command.CommandText = Query;
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                StockData aStock = new StockData();
                aStock.PartsCode = Convert.ToDouble(Reader["ItemCode"]);
                aStock.PartsName = Reader["PartsName"].ToString();
                aStock.PartsQty = Convert.ToDouble(Reader["ItemQty"]);
                aStock.Unit = Reader["Unit"].ToString();
                aStock.Rate = Convert.ToDouble(Reader["Rate"]);
                aStock.Total = Convert.ToDouble(Reader["Total"]);
                aStock.Department = Reader["Department"].ToString();
                aStock.CompanyName = Reader["CompanyName"].ToString();
                stockList.Add(aStock);
            }
            Reader.Close();
            Connection.Close();
            return stockList;
        }

        public void UpdateStock(string partsCode, string department, string companyName)
        {
            decimal totalPartsQty = GetPartsTotalQtyByPartsCode(partsCode, department, companyName);
            decimal totalReturn = GetTotalReturnQty(partsCode, department, companyName);
            decimal totalIssue = GetTotalIssueQty(partsCode, department, companyName);
           

            decimal realStockQty = (totalPartsQty - (totalReturn + totalIssue));

            //int rowAffected;
        
            Query ="UPDATE Stock SET ItemQty=@ItemQty  WHERE ItemCode=@ItemCode AND Department=@Department AND CompanyName=@CompanyName";
            Command.CommandText = Query;

            Command.Parameters.Clear();


            Command.Parameters.Add("@ItemQty", SqlDbType.Decimal).Value = realStockQty;
            Command.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = partsCode;
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyName;
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

        public decimal GetTotalStock(string partsCode, string department, string companyName)
        {
            decimal qty = 0;
            //Query = "SELECT sum(Quantity) As Quantity  FROM tblPurchaseDetails  Where PartsCode=@PartsCode AND Department=@Department AND CompanyName=@CompanyName";
            Query = "SELECT ISNULL(SUM(s.ItemQty),0) As Quantity  FROM Stock s Where s.ItemCode=@ItemCode And s.Department=@Department And s.CompanyName=@CompanyName";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@ItemCode", partsCode);
            Command.Parameters.AddWithValue("@Department", department);
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

        public decimal GetTotalIssueQty(string partsCode, string department, string companyName)
        {
            decimal qty = 0;
            //Query = "SELECT sum(Quantity) As Quantity  FROM tblPurchaseDetails  Where PartsCode=@PartsCode AND Department=@Department AND CompanyName=@CompanyName";
            Query = "SELECT ISNULL( sum(m.Qty),0) As Quantity  FROM MaintenanceDetails m Where m.VarItemCode=@VarItemCode AND m.Department=@Department AND m.Company=@Company";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@VarItemCode", partsCode);
            Command.Parameters.AddWithValue("@Department", department);
            Command.Parameters.AddWithValue("@Company", companyName);
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

        public decimal GetTotalReturnQty(string partsCode, string department, string companyName)
        {
            decimal qty = 0;
            Query = "SELECT ISNULL(sum(Quantity),0) As Quantity  FROM tblReturnDetails  Where PartsCode=@PartsCode AND Department=@Department AND CompanyName=@CompanyName";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("@PartsCode", partsCode);
            Command.Parameters.AddWithValue("@Department", department);
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

        public decimal GetPartsTotalQtyByPartsCode(string partsCode, string deptName, string companyName)
        {
            decimal qty = 0;
            Query = "SELECT sum(Quantity) As Quantity  FROM tblPurchaseDetails  Where PartsCode=@PartsCode AND Department=@Department AND CompanyName=@CompanyName";
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