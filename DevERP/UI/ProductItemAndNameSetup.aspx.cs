using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.UI
{
    public partial class ProductItemAndNameSetup : System.Web.UI.Page
    {
        DevERPDBDataContext db=new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                LoadTypeGrid();
                LoadProductNameGrid();
            }
            LoadProductTypeDdl();
        }
        protected void productTypeSaveButton_Click(object sender, EventArgs e)
        {
            tbl_ProductType productType = new tbl_ProductType();

            if (productTypeTextBox.Text != "")
            {
                productType.ProductType = productTypeTextBox.Text.Trim();
                db.tbl_ProductTypes.InsertOnSubmit(productType);
                db.SubmitChanges();
                productTypeTextBox.Text = String.Empty;
                successStatusLabel.InnerText = "Product Type Inserted Successfully";
                LoadTypeGrid();
                LoadProductNameGrid();
                LoadProductTypeDdl();
            }
        }

        private void LoadTypeGrid()
        {
            var getData = from c in db.tbl_ProductTypes
                          select new { c.ProductType };
            productTypeGridview.DataSource = getData.AsEnumerable();
            productTypeGridview.DataBind();
        }
        private void LoadProductNameGrid()
        {
            var getData = from c in db.tbl_ProductNames
                          join s in db.tbl_ProductTypes on c.ProductTypeId equals s.ProductId into uGroup
                          from s in uGroup.DefaultIfEmpty()
                          select new { c.ProdectName, s.ProductType };

            productNameGridView.DataSource = getData.AsEnumerable();
            productNameGridView.DataBind();
        }
        private void LoadProductTypeDdl()
        {
            var b = from p in db.tbl_ProductTypes
                    orderby p.ProductId
                    select new { p.ProductId, p.ProductType };

            productTypeDropDownList.DataSource = b;
            productTypeDropDownList.DataValueField = "ProductId";
            productTypeDropDownList.DataTextField = "ProductType";
            productTypeDropDownList.DataBind();
            //busNoDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }
        protected void productNameSaveButton_Click(object sender, EventArgs e)
        {
            tbl_ProductName productName = new tbl_ProductName();

            if (productNameTextBox.Text != "")
            {
                productName.ProdectName = productNameTextBox.Text.Trim();
                productName.ProductTypeId = Convert.ToInt32(productTypeDropDownList.SelectedValue);
                db.tbl_ProductNames.InsertOnSubmit(productName);
                db.SubmitChanges();
                productNameTextBox.Text = String.Empty;
                successStatusLabelProductName.InnerText = "Product Name Inserted Successfully";
                LoadProductNameGrid();
                LoadTypeGrid();
            }
        }
    }
}