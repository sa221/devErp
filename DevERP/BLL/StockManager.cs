using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using SBBusMS.DAL;

namespace DevERP.BLL
{
    public class StockManager
    {
        StockGateway aStockGateway=new StockGateway();

        public decimal GetTotalStock(string partsCode, string department, string companyName)
        {
            return aStockGateway.GetTotalStock(partsCode, department, companyName);
        }

        public void UpdateStock(string partsCode, string department, string companyName)
        {
            aStockGateway.UpdateStock(partsCode,department,companyName);
        }
        public List<StockData> GetAll()
        {
            return aStockGateway.GetAll();
        }


    }
}