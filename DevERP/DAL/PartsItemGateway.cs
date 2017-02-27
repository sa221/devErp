using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.DAL
{
    public class PartsItemGateway : ConnectionGateway
    {

        public int UpdatePartsInfo(tblPartsInfo tblPartsInfo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    string query = "UPDATE tblPartsInfo SET GroupId=@GroupId,CategoryId=@CategoryId,PartsName=@PartsName,Unit=@Unit,TenRate=@TenRate," +
                                   "UsesLoc=@UsesLoc,LifeCycle=@LifeCycle WHERE PartsCode=@PartsCode";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@GroupId", tblPartsInfo.GroupId);
                    cmd.Parameters.AddWithValue("@CategoryId", tblPartsInfo.CategoryId);
                    cmd.Parameters.AddWithValue("@PartsName", tblPartsInfo.PartsName);
                    cmd.Parameters.AddWithValue("@Unit", tblPartsInfo.Unit);
                    cmd.Parameters.AddWithValue("@TenRate", tblPartsInfo.TenRate);
                    cmd.Parameters.AddWithValue("@UsesLoc", tblPartsInfo.UsesLoc);
                    cmd.Parameters.AddWithValue("@LifeCycle", tblPartsInfo.LifeCycle);
                    cmd.Parameters.AddWithValue("@PartsCode", tblPartsInfo.PartsCode);

                    con.Open();

                    int rowEffected = cmd.ExecuteNonQuery();

                    if (rowEffected > 0)
                    {
                        return 2;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public   string  GetAllPartsItem()
        {
            string query = "select PartsCode,PartsName,Unit,TenRate,UsesLoc,LifeCycle From tblPartsInfo ";
            SqlCommand cmd = new SqlCommand(query);
            string constr = ConfigurationManager.ConnectionStrings["SBBusMSDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);
                        return ds.GetXml();
                    }
                }
            }
        }
    }
}