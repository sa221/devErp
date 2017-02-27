using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DevERP.Others;
using DevERP.Model;

namespace DevERP.DAL
{
    public class SupplierGateway : ConnectionGateway
    {
        public int SaveSupplier(Supplier aSupplier)
        {
            if (aSupplier.SupplierPic==null)
            {
                Query = @"INSERT INTO tblSuppliers(SupplierId,OrganizationName,ContactPerson,Address,ContactNo,MobileNo,Email,OpeningBalance,Department,CompanyName) VALUES" +
                    "(@SupplierId,@OrganizationName,@ContactPerson,@Address,@ContactNo,@MobileNo,@Email,@OpeningBalance,@Department,@CompanyName)"; 
            }
            else
            {
                Query = @"INSERT INTO tblSuppliers(SupplierId,OrganizationName,ContactPerson,Address,ContactNo,MobileNo,Email,OpeningBalance,SupplierPic,Department,CompanyName) VALUES" +
                   "(@SupplierId,@OrganizationName,@ContactPerson,@Address,@ContactNo,@MobileNo,@Email,@OpeningBalance@SupplierPic,@Department,@CompanyName)";
            }         
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value = aSupplier.SupplierId;
            Command.Parameters.Add("@OrganizationName", SqlDbType.VarChar).Value = aSupplier.OrganizationName;
            Command.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = aSupplier.ContactPerson;
            Command.Parameters.Add("@Address", SqlDbType.VarChar).Value = aSupplier.Address;

            Command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = aSupplier.ContactNo;
            Command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = aSupplier.MobileNo;
            Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = aSupplier.Email;
            Command.Parameters.Add("@OpeningBalance", SqlDbType.Decimal).Value = aSupplier.OpeningBalance;

            if (aSupplier.SupplierPic !=null)
            {
                Command.Parameters.Add("@SupplierPic", SqlDbType.VarBinary).Value = aSupplier.SupplierPic;  
            }            
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = aSupplier.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = aSupplier.CompanyName;
            //Command.Parameters.Add("@Status", SqlDbType.VarChar).Value = aSupplier.Status;
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;


        }

        //public bool GetSuppId(string supplierId)
        //{
        //    Query = "SELECT SupplierId From Suppliers WHERE SupplierId='" + supplierId + "'";
        //    Command.CommandText = Query;
        //    Connection.Open();
        //    try
        //    {
        //        Reader = Command.ExecuteReader();
        //        return Reader.HasRows;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //    finally
        //    {
        //        Reader.Close();
        //        Connection.Close();
        //    }
        //}

        //public bool GetSuppName(string supplierName)
        //{
        //    Query = "SELECT SupplierName FROM Suppliers WHERE SupplierName='" + supplierName + "'";
        //    Command.CommandText = Query;
        //    Connection.Open();
        //    try
        //    {
        //        Reader = Command.ExecuteReader();
        //        return Reader.HasRows;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //    finally
        //    {
        //        Reader.Close();
        //        Connection.Close();
        //    }
        //}

        public List<Supplier> GetAll()
        {
            //HttpContext context = HttpContext.Current;
            //string department = context.Session["Department"].ToString();
            //string company = context.Session["Company"].ToString();
            List<Supplier> suppliers = new List<Supplier>();
            

            Query = @"SELECT SupplierId,OrganizationName,ContactPerson,Address,ContactNo,MobileNo,Email,OpeningBalance,
                    Department,CompanyName FROM tblSuppliers";
            Command.CommandText = Query;
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Open();

            }
            else
            {
                Connection.Open();
            }
            
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.SupplierId = Reader["SupplierId"].ToString();
                supplier.OrganizationName = Reader["OrganizationName"].ToString();
                supplier.ContactPerson = Reader["ContactPerson"].ToString();
                supplier.Address = Reader["Address"].ToString();
                supplier.ContactNo = Reader["ContactNo"].ToString();
                supplier.MobileNo = Reader["MobileNo"].ToString();
                supplier.Email = Reader["Email"].ToString();
                supplier.OpeningBalance = Convert.ToDouble(Reader["OpeningBalance"]);               
                supplier.Department = Reader["Department"].ToString();
                supplier.CompanyName = Reader["CompanyName"].ToString();              
                suppliers.Add(supplier);
            }
            Reader.Close();
            Connection.Close();
            return suppliers;
            

           
        }

        public Supplier GetSupplierById(string supplierId)
        {
            Supplier aSupplier = null;
            Query = "SELECT * FROM tblSuppliers WHERE SupplierId='" + supplierId + "' ";
            Command.CommandText = Query;
            try
            {
                Connection.Open();
                Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    aSupplier = new Supplier();
                    aSupplier.SupplierId =Reader["SupplierId"].ToString();
                    aSupplier.OrganizationName = Reader["OrganizationName"].ToString();
                    aSupplier.ContactPerson = Reader["ContactPerson"].ToString();
                    aSupplier.Address = Reader["Address"].ToString();                  
                    aSupplier.ContactNo = Reader["ContactNo"].ToString();
                    aSupplier.MobileNo = Reader["MobileNo"].ToString();
                    aSupplier.Email = Reader["Email"].ToString();
                    aSupplier.OpeningBalance = Convert.ToDouble(Reader["OpeningBalance"]);
                    aSupplier.Department = Reader["Department"].ToString();
                    aSupplier.CompanyName = Reader["CompanyName"].ToString();

                }
            }
            finally
            {

                if (Reader != null)
                {
                    Reader.Close();
                }
                Connection.Close();
            }



            return aSupplier;
        }

        public int UpdateSupplier(Supplier aSupplier)
        {
            if (aSupplier.SupplierPic==null)
            {
                Query = @"UPDATE tblSuppliers SET SupplierId=@SupplierId,OrganizationName=@OrganizationName,ContactPerson=@ContactPerson, 
                    Address=@Address,ContactNo=@ContactNo,MobileNo=@MobileNo,Email=@Email,OpeningBalance=@OpeningBalance,Department=@Department,CompanyName=@CompanyName,Status = @Status WHERE SupplierId=@SupplierId";
            }
            else
            {
                Query = @"UPDATE tblSuppliers SET SupplierId=@SupplierId,OrganizationName=@OrganizationName,ContactPerson=@ContactPerson, 
                    Address=@Address,ContactNo=@ContactNo,MobileNo=@MobileNo,Email=@Email,OpeningBalance=@OpeningBalance,SupplierPic=@SupplierPic,Department=@Department,CompanyName=@CompanyName,Status = @Status WHERE SupplierId=@SupplierId";
            }
            
            Command.CommandText = Query;
            Connection.Open();
            Command.Parameters.Clear();
            Command.Parameters.Add("@SupplierId", SqlDbType.VarChar).Value =aSupplier.SupplierId;
            Command.Parameters.Add("@OrganizationName", SqlDbType.VarChar).Value = aSupplier.OrganizationName;
            Command.Parameters.Add("@ContactPerson", SqlDbType.VarChar).Value = aSupplier.ContactPerson;
            Command.Parameters.Add("@Address", SqlDbType.VarChar).Value = aSupplier.Address;
            Command.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = aSupplier.ContactNo;
            Command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = aSupplier.MobileNo;
            Command.Parameters.Add("@Email", SqlDbType.VarChar).Value = aSupplier.Email;
            Command.Parameters.Add("@OpeningBalance", SqlDbType.Decimal).Value = aSupplier.OpeningBalance;
            if (aSupplier.SupplierPic !=null)
            {
                Command.Parameters.Add("@SupplierPic", SqlDbType.VarBinary).Value = aSupplier.SupplierPic; 
            }  
            Command.Parameters.Add("@Department", SqlDbType.VarChar).Value = aSupplier.Department;
            Command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = aSupplier.CompanyName;
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;




        }

      

        public int DeleteSupplier(string supplierId)
        {
            Query = "DELETE tblSuppliers WHERE SupplierId =@supplierId ";
            Command.CommandText = Query;
            Command.Parameters.Clear();
            Command.Parameters.Add("supplierId", SqlDbType.VarChar).Value = supplierId;
            Connection.Open();           
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }

        public List<Supplier> GetSupplierIdAndStatus()
        {
            List<Supplier> suppliers = new List<Supplier>();
            Command.Parameters.Clear();
            Query = @"SELECT SupplierId FROM tblSuppliers  ";
            Command.CommandText = Query;
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
                Connection.Open();

            }
            else
            {
                Connection.Open();
            }
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.SupplierId = Reader["SupplierId"].ToString();               
                //supplier.Status = Reader["Status"].ToString();
                suppliers.Add(supplier);
            }
            Reader.Close();
            Connection.Close();
            return suppliers; 
        }
    }
}