using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevERP.Base;
using DevERP.BLL;
using DevERP.Model;
using DevERP.Others;

namespace DevERP.UI
{
    public partial class PartsEntryUI : System.Web.UI.Page
    {
        static ItemManager aItemManager = new ItemManager();
        Item aItem = new Item();
        static CustomMethod customMethod = new CustomMethod();
        static PartsItemManager partsItemManager = new PartsItemManager();


        MyImage myImage = new MyImage();

        static DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadItemGridView();
                LoadGroupDropdown();
                LoadCategoryDropdown();

            }
        }
        private void LoadItemGridView()
        {
            var group = (from gro in db.tblPartsInfos
                         select new { gro.PartsName, gro.PartsCode, gro.Unit, gro.TenRate, gro.LifeCycle, gro.UsesLoc }
                        );

            ItemGridView.DataSource = group.AsEnumerable();
            ItemGridView.DataBind();

        }


        private static void LoadItemGridView2()
        {
            PartsEntryUI inEntryUi = new PartsEntryUI();
            inEntryUi.LoadItemGridView();
        }

        [WebMethod]
        public static string DeletePartsItemById(string partsCode)
        {
            string message = "";
            int partsId = 0;
            partsId = Convert.ToInt32(partsCode);
            tblPartsInfo tblPartsInfo = db.tblPartsInfos.FirstOrDefault(x => x.PartsCode == partsId);
            if (tblPartsInfo != null)
            {
                db.tblPartsInfos.DeleteOnSubmit(tblPartsInfo);
                db.SubmitChanges();
                message = customMethod.GetMessage("Parts Deleted ", "danger");


            }
            return message;

        }

        [WebMethod]
        public static MyDataReturnClass GetGroupAndCategoryByPartsId(string partsCode)
        {
            MyDataReturnClass myDataReturn = new MyDataReturnClass();
            if (partsCode != "")
            {
                DevERPDBDataContext db = new DevERPDBDataContext();
                var group = (from gro in db.tblPartsInfos
                             where gro.PartsCode == int.Parse(partsCode)
                             select new { gro.GroupId, gro.CategoryId }
                           );

                foreach (var tt in group)
                {
                    myDataReturn.GroupId = (int)tt.GroupId;
                    myDataReturn.CategoryId = (int)tt.CategoryId;
                }
            }
            return myDataReturn;

        }


        [WebMethod]
        public static string SaveParts(string groupId, string categoryId, string partsId, string partsName, string unit, string tenRate, string usesLocation, string lifeCycle)
        {
            DevERPDBDataContext db = new DevERPDBDataContext();
            string message = "";
            tblPartsInfo isPartsIdExist = null;
            if (partsId != "")
            {
                isPartsIdExist = db.tblPartsInfos.FirstOrDefault(x => x.PartsCode == Convert.ToInt32(partsId));
            }

            var isPartsNameExist = db.tblPartsInfos.FirstOrDefault(x => x.PartsName == partsName);


            if (isPartsIdExist == null)
            {
                if (isPartsNameExist != null)
                {
                    //"Parts Name Already Exists. Please Try With New One"
                    message = "-1";
                }
                else
                {

                    tblPartsInfo tblParts = new tblPartsInfo();
                    tblParts.GroupId = Convert.ToInt32(groupId);
                    tblParts.CategoryId = Convert.ToInt32(categoryId);
                    tblParts.PartsName = partsName;
                    tblParts.Unit = unit;
                    tblParts.TenRate = Double.Parse(tenRate);
                    tblParts.UsesLoc = usesLocation;
                    tblParts.LifeCycle = lifeCycle;
                    db.tblPartsInfos.InsertOnSubmit(tblParts);
                    db.SubmitChanges();

                    message = customMethod.GetMessage("Parts Save Successfully", "success");
                }
            }
            else
            {
                tblPartsInfo nameAndId =
                    db.tblPartsInfos.FirstOrDefault(
                        x => x.PartsName == partsName && x.PartsCode != Convert.ToInt32(partsId));
                if (nameAndId != null)
                {
                    //"The Parts Name Alerady Exist With Another ID"
                    message = customMethod.GetMessage("Parts Already Exists", "info");
                }
                else
                {
                    tblPartsInfo tblParts = db.tblPartsInfos.FirstOrDefault(x => x.PartsCode == Convert.ToInt32(partsId));
                    if (tblParts != null)
                    {
                        tblParts.PartsCode = Convert.ToInt32(partsId);
                        tblParts.GroupId = Convert.ToInt32(groupId);
                        tblParts.CategoryId = Convert.ToInt32(categoryId);
                        tblParts.PartsName = partsName;
                        tblParts.Unit = unit;
                        tblParts.TenRate = Double.Parse(tenRate);
                        tblParts.UsesLoc = usesLocation;
                        tblParts.LifeCycle = lifeCycle;
                        db.SubmitChanges();
                        message = customMethod.GetMessage("Parts Update Successfully", "success");
                    }
                    else
                    {
                        //"Error Unknown Or Update Faild"
                        message = customMethod.GetMessage("Update Failed", "danger");
                    }
                }
            }

            return message;
        }

        //[WebMethod]
        //public static string SavePartsData(Item item)
        //{
        //    string message = "";

        //    aItemManager.SavePartsData(item);

        //    return message;

        //}

        #region DummyDataForAjaxGridLoad
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("PartsCode");
            dummy.Columns.Add("PartsName");
            dummy.Columns.Add("Unit");
            dummy.Columns.Add("TenRate");
            dummy.Columns.Add("LifeCycle");
            dummy.Columns.Add("UsesLoc");
            dummy.Rows.Add();
            ItemGridView.DataSource = dummy;
            ItemGridView.DataBind();
        }
        #endregion






        private void LoadGroupDropdown()
        {
            var group = (from gro in db.tblGroups
                         select gro
                ).ToList();
            if (!(@group == null))
            {
                groupnameNoDropDownList.DataSource = group;
                groupnameNoDropDownList.DataTextField = "GroupName";
                groupnameNoDropDownList.DataValueField = "Id";
                groupnameNoDropDownList.DataBind();
                groupnameNoDropDownList.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        private void LoadCategoryDropdown()
        {
            var category = (from gro in db.tblCategories
                            select gro
                ).ToList();
            if (!(@category == null))
            {
                categorynameDropDownList.DataSource = category;
                categorynameDropDownList.DataTextField = "CategoryName";
                categorynameDropDownList.DataValueField = "Id";
                categorynameDropDownList.DataBind();
                categorynameDropDownList.Items.Insert(0, new ListItem("Select", "0"));
            }
        }








        protected void ItemGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ItemGridView.PageIndex = e.NewPageIndex;
            //LoadItemGridView();
            DataBind();
        }

        protected void reportButton_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/UI/ReportsUI/GSOItemsReportUI.aspx");

        }


    }

    public class MyDataReturnClass
    {
        public string Seats { get; set; }
        public int GroupId { get; set; }
        public int CategoryId { get; set; }
    }
    
}