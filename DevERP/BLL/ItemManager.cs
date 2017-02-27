using System;
using System.Collections.Generic;
using DevERP.Base;
using DevERP.DAL;
using DevERP.Model;

namespace DevERP.BLL
{
    public class ItemManager
    {

        ItemGateway aItemGateway=new ItemGateway();
        public string Message { get; set; }
        CustomMethod custom=new CustomMethod();
        //public void  SaveItem(Item aItem)
        //{
            
        //    var allItemList = aItemGateway.GetAll();
        //    try
        //    {
        //        var checkItemCode = allItemList.Find(f=> f.ItemCode==aItem.ItemCode);
        //        if (checkItemCode == null)
        //        {
        //            //var checkItemName = allItemList.Find(f => f.ItemName == aItem.ItemName);
        //            //if (checkItemName==null)
        //            //{
        //                int rowAffected = aItemGateway.SaveItem(aItem);
        //                if (rowAffected > 0)
        //                {
        //                    Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
        //                    Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
        //                    Message += "Record saved successfully</div>";
        //                }
        //                else
        //                {
        //                    var message = "This " + aItem.ItemName + " is already exist.Please choose another.";
        //                    Message = custom.GetMessage(message, "danger");
        //                }
        //            //}                   
                    
                    
        //        }
        //        else
        //        {
        //            var checkName =
        //                allItemList.Find(f=> f.ItemName == aItem.ItemName);
        //            if (checkName == null)
        //            {
                       
        //                int rowAffect = aItemGateway.UpdateItem(aItem);
        //                if (rowAffect > 0)
        //                {
        //                    Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
        //                    Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
        //                    Message += "Record Updated successfully</div>";
        //                }
        //            }
        //            else
        //            {
        //                var message = "This " + aItem.ItemName + " is already exist.Please choose another.";
        //                Message = custom.GetMessage(message, "danger");
                       
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Message = "<div class='alert alert-danger alert-dismissible' role='alert'>";
        //        Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
        //        Message += "Something wrong.Please try again" + ex.Message + "</div>";
        //    }


        //}

        //public List<Item> GetAllItemGroupName()
        //{
        //    return aItemGateway.GetAllItemGroupName();
        //}

        //public List<Item> GetAllItemCategoryName()
        //{
        //    return aItemGateway.GetAllItemCategoryName();
        //}

        //public List<Item> GetAllItemCode()
        //{
        //    return aItemGateway.GetAllItemCode();
        //}

        //public List<Item> GetAllItemName()
        //{
        //    return aItemGateway.GetAllItemName();
        //}

        //public List<Item> GetItemCategoryName(string group)
        //{
        //    return aItemGateway.GetItemCategoryName(group);
        //}
        //public List<Item> GetAll()
        //{
        //    return aItemGateway.GetAll();
        //}

        public int DeleteItem(int iId)
        {
            int rowAffected = aItemGateway.DeleteItem(iId);
            if (rowAffected > 0)
            {
                Message = "<div class='alert alert-success alert-dismissible' role='alert'>";
                Message += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                Message += "Record Deleted successfully</div>";
            }
            return rowAffected;
        }

        //public List<Item> GetGroupNameByInvNo(string purcInv)
        //{
        //  List<Item> items=  aItemGateway.GetGroupNameByInvNo(purcInv);
        //    return items;

        //}

        public List<Item> GetAll()
        {
            return aItemGateway.GetAll();
        }
    }
}