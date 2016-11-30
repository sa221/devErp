using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.ReportUI
{
    public partial class ProductionReceiveReport : System.Web.UI.Page
    {
        DevERPDBDataContext db=new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadProductDdl();
            }
        }

        protected void showReportButton_Click(object sender, EventArgs e)
        {
            if (empId.Value != "" && productDropDownList.SelectedValue != "0" && fromDate.Value != "" &&
                toDate.Value != "")
            {
                var chk =
                    db.tbl_ProductionInfos.FirstOrDefault(
                        c => c.EmpId == empId.Value && c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) && c.ProductionDate >= Convert.ToDateTime(fromDate.Value) && c.ProductionDate <= Convert.ToDateTime(toDate.Value));
                DateTime f = Convert.ToDateTime(chk.ProductionDate);
                
            }
        }

        protected void LoadProductDdl()
        {
            var getData = from c in db.tbl_ProductSizes
                orderby c.FullProductName ascending
                select new {c.Id, c.FullProductName};
            productDropDownList.DataSource = getData;
            productDropDownList.DataValueField = "Id";
            productDropDownList.DataTextField = "FullProductName";
            productDropDownList.DataBind();
            productDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}