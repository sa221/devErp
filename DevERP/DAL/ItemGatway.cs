using System;
using System.Collections.Generic;
using System.Data;
using DevERP.Models;
using DevERP.Others;

namespace DevERP.DAL
{
    public class ItemGatway :ConnectionGateway
    {
        public bool InsertItem(string itemName)
        {
            Query = "Insert into Item (itemName) values (@itemName)";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@itemName", itemName);
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
        public bool UpdateItem(Item item)
        {
            Query = "Update Item set itemName=@itemName where itemId = @itemId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@itemId", item.ItemId);
            Command.Parameters.AddWithValue("@itemName", item.ItemName);
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
        public bool DeleteItem(int itemId)
        {
            Query = "delete from item where itemId = @itemId";
            PrepareCommand(CommandType.Text);
            Command.Parameters.AddWithValue("@itemId", itemId);
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
        public List<Item> GetAllitem()
        {
            Query = "Select * from item";
            PrepareCommand(CommandType.Text);
            List<Item> items = new List<Item>();
            Connection.Open();
            try
            {
                Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    Item item = new Item();
                    item.ItemId = Convert.ToInt32(Reader["itemId"].ToString());
                    item.ItemName = Reader["itemName"].ToString();
                    items.Add(item);
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
            return items;
        }
    }
}