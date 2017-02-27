using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevERP.DAL;
using SBBusMS.DAL;

namespace DevERP.BLL
{
    public class PartsItemManager
    {
        PartsItemGateway partsItemGateway=new PartsItemGateway();


        public int UpdatePartsInfo(tblPartsInfo tblPartsInfo)
        {
            return partsItemGateway.UpdatePartsInfo(tblPartsInfo);
        }

        public string GetAllPartsItem()
        {
            return partsItemGateway.GetAllPartsItem();
        }
    }
}