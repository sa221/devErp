using System;
using System.Collections.Generic;
using DevERP.Base;
using DevERP.DAL;
using DevERP.Model;

namespace DevERP.BLL
{
    public class SupplierManager
    {
        SupplierGateway aSupplierGateway = new SupplierGateway();
        CustomMethod custom = new CustomMethod();
        public string Message { get; set; }

        public int SaveSupplier(Supplier aSupplier)
        {

            if (aSupplierGateway.SaveSupplier(aSupplier) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Supplier GetSupplierById(string supplierId)
        {
            return aSupplierGateway.GetSupplierById(supplierId);
        }


        //public List<Supplier> GetAllSupplierName()
        //{
        //    return aSupplierGateway.GetAllSupplierName();
        //}

        //public List<Supplier> GetAllSupplierCode()
        //{
        //    return aSupplierGateway.GetAllSupplierCode();
        //}

        //public List<Supplier> SupplierName(string code)
        //{
        //    return aSupplierGateway.SupplierName(code);
        //}

        //public List<Supplier> GetAllSupplierCodeByName(string supplierName)
        //{
        //    return aSupplierGateway.GetAllSupplierCodeByName(supplierName);
        //}

        public List<Supplier> GetAll()
        {
            return aSupplierGateway.GetAll();
        }

        public int DeleteSupplier(string supplierId)
        {

            int rowAffected = aSupplierGateway.DeleteSupplier(supplierId);
            if (rowAffected > 0)
            {
                Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                Message += "Record Deleted successfully</div>";
            }
            return rowAffected;
        }

        public List<Supplier> GetSupplierIdAndStatus()
        {
            return aSupplierGateway.GetSupplierIdAndStatus();
        }
    }
}