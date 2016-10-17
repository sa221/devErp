using System.Collections.Generic;
using DevERP.DAL;
using DevERP.Models;

namespace DevERP.BLL
{
    public class SubItemManager
    {
        readonly SubItemGatway _subItemGatway = new SubItemGatway();

        public bool InsertSubItem(SubItem subItem)
        {
            return _subItemGatway.InsertSubItem(subItem);
        }

        public bool UpdateSubItem(SubItem subItem)
        {
            return _subItemGatway.UpdateSubItem(subItem);
        }
        public bool DeleteSubItem(int subItemId)
        {
            return _subItemGatway.DeleteSubItem(subItemId);
        }

        public List<SubItem> GetAllSubItem(int itemId)
        {
            return _subItemGatway.GetAllSubItem(itemId);
        }
    }
}