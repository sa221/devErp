
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DevERP.DAL;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.DAL
{
    public class ItemGateway : ConnectionGateway
    {


        public int DeleteItem(int iId)
        {
            Query = "DELETE FROM Item WHERE ItemId=@iId";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("@iId", SqlDbType.Int).Value = iId;
            Connection.Open();
            int rowAfected = Command.ExecuteNonQuery();

            Connection.Close();
            return rowAfected;

        }



        public List<Item> GetAll()
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<Item> items = new List<Item>();
            Query = "SELECT * FROM tblPartsInfo ";
            //Where Department=@Department and CompanyName=@CompanyName
            Command.CommandText = Query;
            //Command.Parameters.Clear();
            //Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = "";
            //Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = "";
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Close();
                Connection.Open();
            }
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Item aItem = new Item();
                aItem.PartsCode = Reader["PartsCode"].ToString();
                aItem.GroupId = (int)Reader["GroupId"];
                aItem.CategoryId = (int)Reader["CategoryId"];
                aItem.PartsName = Reader["PartsName"].ToString();
                
                aItem.Unit = Reader["Unit"].ToString();
                aItem.UsesLoc = Reader["UsesLoc"].ToString();
                aItem.PartsType = Reader["PartsType"].ToString();
                aItem.UsesPurpose = Reader["UsesPurpose"].ToString();
                if (Reader["ReOrderLevel"]!=DBNull.Value)
                {
                    aItem.ReOrderLevel = Convert.ToDouble(Reader["ReOrderLevel"]);    
                }                     
                aItem.Department = Reader["Department"].ToString();
                aItem.CompanyName = Reader["CompanyName"].ToString();
                items.Add(aItem);
            }
            Reader.Close();
            Connection.Close();
            return items;
        }
    }
}