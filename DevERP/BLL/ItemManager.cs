using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class ItemManager
    {
        readonly ItemGatway _itemGatway = new ItemGatway();

        public bool InsertItem(string itemName)
        {
            return _itemGatway.InsertItem(itemName);
        }

        public bool UpdateItem(Item item)
        {
            return _itemGatway.UpdateItem(item);
        }
        public bool DeleteItem(int itemId)
        {
            return _itemGatway.DeleteItem(itemId);
        }

        public List<Item> GetAllItem()
        {
            return _itemGatway.GetAllitem();
        }
    }
}