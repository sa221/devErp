using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class SubItemGatway :ConnectionGateway
    {
        public bool InsertSubItem(SubItem subItem)
        {
            Query = "Insert into SubItem (subItemName,itemId) values (@subItemName,@itemId)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@subItemName", subItem.SubItemName);
            Command.Parameters.AddWithValue("@itemId", subItem.ItemId);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery()>0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public bool UpdateSubItem(SubItem subItem)
        {
            Query = "Update SubItem set subItemName=@subItemName where subItemId = @subItemId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@subItemId", subItem.SubItemId);
            Command.Parameters.AddWithValue("@subItemName", subItem.SubItemName);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public bool DeleteSubItem(int subItemId)
        {
            Query = "delete from SubItem where SubItemId = @subItemId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@subItemId", subItemId);
            Connection.Open();
            try
            {
                return Command.ExecuteNonQuery() > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                CloseAllConnection();
            }
        }
        public List<SubItem> GetAllSubItem(int itemId)
        {
            Query = "Select * from SubItem where itemId= @itemId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@itemId", itemId);
            List<SubItem> subItems = new List<SubItem>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    SubItem subItem = new SubItem();
                    subItem.SubItemId = Convert.ToInt32(Reader["subItemId"].ToString());
                    subItem.SubItemName = Reader["subItemName"].ToString();
                    subItem.ItemId = Convert.ToInt32(Reader["itemId"].ToString());
                    subItems.Add(subItem);
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CloseAllConnection();
                
            }
            return subItems;
        }
    }
}