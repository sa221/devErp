using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.UI
{
    public partial class ProductionEntry : System.Web.UI.Page
    {
        DevERPDBDataContext db=new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductDdl();
                productionDate.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
            }
        }

        private void LoadProductDdl()
        {
            var b = from p in db.tbl_ProductSizes
                    select new { p.Id, p.FullProductName };

            productDropDownList.DataSource = b;
            productDropDownList.DataValueField = "Id";
            productDropDownList.DataTextField = "FullProductName";
            productDropDownList.DataBind();


            productDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }

        protected void searchEmpButton_Click(object sender, EventArgs e)
        {
            empName.InnerText = "";
            messageLabel.Text = "";
            if (empId.Value != "")
            {
                tbl_EmployeeEntry getEmployee = db.tbl_EmployeeEntries.FirstOrDefault(c => c.EmployeeId == empId.Value);
                if (getEmployee != null)
                {
                    empName.InnerText = getEmployee.FirstName + "" + getEmployee.LastName;
                    
                }
            }
            else
            {
                messageLabel.Text = "Please Insert Employee ID.";
            }
        }

        protected void productDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            productRate.Value = "";
            tbl_ProductSize getRate = db.tbl_ProductSizes.FirstOrDefault(c => c.Id == Convert.ToInt32(productDropDownList.SelectedValue));
            if (getRate != null)
            {
                productRate.Value = getRate.Rate.ToString();
            }
        }
    }
}