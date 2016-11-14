using System;
using System.Linq;

namespace DevERP.UI
{
    public partial class ProductItemAndNameSetup : System.Web.UI.Page
    {
        readonly DevERPDBDataContext db=new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTypeGrid();
                LoadProductNameGrid();
                LoadProductSizeGrid();
                LoadProductTypeDdl();
                LoadProductNameDdl();
            }
            
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

        protected void sizeRateSaveButton_Click(object sender, EventArgs e)
        {
            tbl_ProductSize productSize=new tbl_ProductSize();

            productSize.ProductTypeId = Convert.ToInt32(pProductTypeDropDownList.SelectedValue);
            productSize.ProductNameId = Convert.ToInt32(productNameDropDownList.SelectedValue);
            productSize.ProductSize = sizeTextBox.Text.Trim();
            productSize.Rate = Convert.ToDecimal(rateTextBox.Text);
            productSize.FullProductName = productNameDropDownList.SelectedItem.Text + "" + sizeTextBox.Text.Trim();

            db.tbl_ProductSizes.InsertOnSubmit(productSize);
            db.SubmitChanges();
            sizeTextBox.Text = String.Empty;
            rateTextBox.Text = String.Empty;
            Span2.InnerText = "Insert Successfully.";
            LoadProductSizeGrid();
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

            pProductTypeDropDownList.DataSource = b;
            pProductTypeDropDownList.DataValueField = "ProductId";
            pProductTypeDropDownList.DataTextField = "ProductType";
            pProductTypeDropDownList.DataBind();
            //busNoDropDownList.Items.Insert(0, new ListItem("Select", "0"));
        }

        private void LoadProductNameDdl()
        {
            var pName = from v in db.tbl_ProductNames
                        select new { v.Id, v.ProdectName };
            productNameDropDownList.DataSource = pName;
            productNameDropDownList.DataValueField = "Id";
            productNameDropDownList.DataTextField = "ProdectName";
            productNameDropDownList.DataBind();
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
        private void LoadProductSizeGrid()
        {
            var getData = from c in db.tbl_ProductSizes
                          join s in db.tbl_ProductTypes on c.ProductTypeId equals s.ProductId into uGroup
                          from s in uGroup.DefaultIfEmpty()
                          join n in db.tbl_ProductNames on c.ProductNameId equals n.Id into nGroup
                          from n in nGroup.DefaultIfEmpty()
                          select new { c.ProductSize, s.ProductType,n.ProdectName,c.Rate,c.FullProductName };

            productWithSizeRateGridView.DataSource = getData.AsEnumerable();
            productWithSizeRateGridView.DataBind();
        }
    }
}