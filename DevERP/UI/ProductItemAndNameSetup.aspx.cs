using System;
using System.Linq;
using System.Web.UI.WebControls;

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
            var checkProduct =
                db.tbl_ProductSizes.FirstOrDefault(
                    c => c.ProductNameId == Convert.ToInt32(productNameDropDownList.SelectedValue)
                         && c.ProductTypeId == Convert.ToInt32(pProductTypeDropDownList.SelectedValue) &&
                         c.ProductSize == sizeTextBox.Text);
            if (checkProduct!=null)
            {
                checkProduct.Rate = Convert.ToDecimal(rateTextBox.Text);
                checkProduct.FullProductName = productNameDropDownList.SelectedItem.Text + "" + sizeTextBox.Text.Trim();
                checkProduct.SalesPrice = Convert.ToDecimal(salesPriceTextBox.Text);
                db.SubmitChanges();
                sizeTextBox.Text = String.Empty;
                rateTextBox.Text = String.Empty;
                salesPriceTextBox.Text = String.Empty;
                sizeRateSaveButton.Text = "Save";
                Span2.InnerText = "Updated Successfully.";
            }
            else
            {
                tbl_ProductSize productSize = new tbl_ProductSize();

                productSize.ProductTypeId = Convert.ToInt32(pProductTypeDropDownList.SelectedValue);
                productSize.ProductNameId = Convert.ToInt32(productNameDropDownList.SelectedValue);
                productSize.ProductSize = sizeTextBox.Text.Trim();
                productSize.Rate = Convert.ToDecimal(rateTextBox.Text);
                productSize.FullProductName = productNameDropDownList.SelectedItem.Text + "" + sizeTextBox.Text.Trim();
                productSize.SalesPrice = Convert.ToDecimal(salesPriceTextBox.Text);
                db.tbl_ProductSizes.InsertOnSubmit(productSize);
                db.SubmitChanges();
                sizeTextBox.Text = String.Empty;
                rateTextBox.Text = String.Empty;
                salesPriceTextBox.Text = String.Empty;
                Span2.InnerText = "Inserted Successfully.";
            }
            pProductTypeDropDownList.Enabled = true;
            productNameDropDownList.Enabled = true;
            sizeTextBox.Enabled = true;
            LoadProductSizeGrid();
        }
        protected void ProductDetailsOnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(productWithSizeRateGridView, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        //Grid row command retrive value to TextBox
        protected void productWithSizeRateGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Span2.InnerText = "";
            int index = productWithSizeRateGridView.SelectedRow.RowIndex;
            GridViewRow gvRow = productWithSizeRateGridView.Rows[index];
            string productTyppe = ((Label)gvRow.FindControl("pTypeIdLabel")).Text;
            string productName = ((Label)gvRow.FindControl("pNameLabel")).Text;
            string size = ((Label)gvRow.FindControl("Label3")).Text;
            string rate = ((Label)gvRow.FindControl("Label4")).Text;
            string salesPrice = ((Label)gvRow.FindControl("Label6")).Text;
            pProductTypeDropDownList.SelectedValue = productTyppe;
            productNameDropDownList.SelectedValue = productName;
            sizeTextBox.Text = size;
            rateTextBox.Text = rate;
            salesPriceTextBox.Text = salesPrice;
            sizeRateSaveButton.Text = "Update";
            pProductTypeDropDownList.Enabled = false;
            productNameDropDownList.Enabled = false;
            sizeTextBox.Enabled = false;
            //groupId.Attributes.Add("readonly", "readonly");
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
                          select new { c.ProductSize, s.ProductType,s.ProductId,n.ProdectName,n.Id,c.Rate,c.FullProductName,c.SalesPrice};

            productWithSizeRateGridView.DataSource = getData.AsEnumerable();
            productWithSizeRateGridView.DataBind();
        }
    }
}