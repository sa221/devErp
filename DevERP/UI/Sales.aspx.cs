using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.UI
{
    public partial class Sales : System.Web.UI.Page
    {
        DevERPDBDataContext db = new DevERPDBDataContext();
        DataTable dt1 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProduct();
                Session.Remove("productInSession");
            }
        }
        private void LoadProduct()
        {
            var getAllProduct = from x in db.tbl_ProductSizes
                                where x.ProductType == "Finish Goods"
                                select new { x.Id, x.FullProductName };
            productNameDropDownList.DataSource = getAllProduct;
            productNameDropDownList.DataValueField = "Id";
            productNameDropDownList.DataTextField = "FullProductName";
            productNameDropDownList.DataBind();
            productNameDropDownList.Items.Insert(0, new ListItem("--Select--", ""));

        }
        private void ProductGridViewData()
        {
            dt1.Columns.Add(new DataColumn("ProductId", typeof(string)));
            dt1.Columns.Add(new DataColumn("ProductName", typeof(string)));
            dt1.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt1.Columns.Add(new DataColumn("Price", typeof(string)));
            //dt1.Columns.Add(new DataColumn("Vat", typeof(string)));
            //dt1.Columns.Add(new DataColumn("ItemDiscountPer", typeof(string)));
            //dt1.Columns.Add(new DataColumn("ItemDiscountTk", typeof(string)));
            //dt1.Columns.Add(new DataColumn("OfferDiscountPer", typeof(string)));
            //dt1.Columns.Add(new DataColumn("OfferDiscountTk", typeof(string)));
            dt1.Columns.Add(new DataColumn("Total", typeof(string)));
            //dt1.Columns.Add(new DataColumn("SalesPrice", typeof(string)));

            Session["productInSession"] = dt1;     //Saving Datatable To Session 
        }
        //protected void OnRowDeletingProduct(object sender, GridViewDeleteEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.RowIndex);
        //    dt1 = Session["productInSession"] as DataTable;
        //    if (dt1 != null)
        //    {

        //        //var v=dt1.Rows[index].
        //        dt1.Rows[index].Delete();
        //        dt1.AcceptChanges();
        //        Session["productInSession"] = dt1;
        //        salesGridView.DataSource = dt1;
        //        GetTotalAmount();
        //    }
        //    salesGridView.DataBind();
        //}
       
        private void GetTotalAmount()
        {
            var amount = dt1.AsEnumerable()
                    .Sum(x => Convert.ToDecimal(x["Total"]));
            var qty = dt1.AsEnumerable()
                    .Sum(x => Convert.ToDecimal(x["Qty"]));
            allItemTotalTakaText.Value = amount.ToString();
            totalQtyText.Value = qty.ToString();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (productCodeText.Value != "" && productNameDropDownList.SelectedValue != "" && qtyText.Value != "" &&
                priceText.Value != "" && itemTotalTakaText.Value != "")
            {
                if (Session["productInSession"] != null)
                {
                    dt1 = (DataTable)Session["productInSession"];
                }
                else
                {
                    ProductGridViewData();
                    dt1 = (DataTable)Session["productInSession"];
                }
                var contains = dt1.AsEnumerable().FirstOrDefault(row => productNameDropDownList.SelectedValue == row.Field<String>("ProductId"));
                if (contains == null && itemUpdateText.Value == "")
                {
                    DataRow dr = dt1.NewRow();
                    dr["ProductId"] = productCodeText.Value;
                    dr["ProductName"] = productNameDropDownList.SelectedItem.Text;
                    dr["Qty"] = qtyText.Value;
                    dr["Price"] = priceText.Value;
                    //dr["Vat"] = vatText.Value;
                    //dr["ItemDiscountPer"] = itemDiscountPercentageText.Value;
                    //dr["ItemDiscountTk"] = itemDiscountTakaText.Value;
                    //dr["OfferDiscountPer"] = offerDiscountPercentageText.Value;
                    //dr["OfferDiscountTk"] = offerDiscountTakaText.Value;
                    dr["Total"] = itemTotalTakaText.Value;
                    //dr["SalesPrice"] = salesText.Value;

                    dt1.Rows.Add(dr);
                    Session["productInSession"] = dt1;     //Saving Datatable To Session 
                    salesGridView.DataSource = dt1;
                    salesGridView.DataBind();

                }
                else if (contains != null && itemUpdateText.Value != "")
                {
                    int index = Convert.ToInt32(itemUpdateText.Value);
                    //GridViewRow gvrow = salesGridView.Rows[index];
                    //int index = Convert.ToInt32(e.RowIndex);
                    dt1 = Session["productInSession"] as DataTable;
                    if (dt1 != null)
                    {

                        //var v=dt1.Rows[index].
                        dt1.Rows[index].Delete();
                        dt1.AcceptChanges();
                        Session["productInSession"] = dt1;
                        salesGridView.DataSource = dt1;
                        GetTotalAmount();
                    }
                    salesGridView.DataBind();
                    DataRow dr = dt1.NewRow();
                    dr["ProductId"] = productCodeText.Value;
                    dr["ProductName"] = productNameDropDownList.SelectedItem.Text;
                    dr["Qty"] = qtyText.Value;
                    dr["Price"] = priceText.Value;
                    //dr["Vat"] = vatText.Value;
                    //dr["ItemDiscountPer"] = itemDiscountPercentageText.Value;
                    //dr["ItemDiscountTk"] = itemDiscountTakaText.Value;
                    //dr["OfferDiscountPer"] = offerDiscountPercentageText.Value;
                    //dr["OfferDiscountTk"] = offerDiscountTakaText.Value;
                    dr["Total"] = itemTotalTakaText.Value;
                    //dr["SalesPrice"] = salesText.Value;

                    dt1.Rows.Add(dr);
                    Session["productInSession"] = dt1;     //Saving Datatable To Session 
                    salesGridView.DataSource = dt1;
                    salesGridView.DataBind();
                }
                else
                {
                    int s = Convert.ToInt32(contains.ItemArray[2]) + Convert.ToInt32(qtyText.Value);
                    decimal totalAmount = Convert.ToDecimal(contains.ItemArray[4]) + Convert.ToDecimal(itemTotalTakaText.Value);
                    // dt1.Rows[1][]=
                    contains.SetField("Qty", s);
                    contains.SetField("Total", totalAmount);
                    contains.AcceptChanges();
                    Session["productInSession"] = dt1;
                    salesGridView.DataSource = dt1;
                    salesGridView.DataBind();
                }
                productCodeText.Value = String.Empty;
                productNameDropDownList.SelectedValue = "";
                itemUpdateText.Value = String.Empty;
                qtyText.Value = String.Empty;
                priceText.Value = String.Empty;
                //vatText.Value = String.Empty;
                //itemDiscountPercentageText.Value = String.Empty;
                //itemDiscountTakaText.Value = String.Empty;
                //offerDiscountPercentageText.Value = String.Empty;
                //offerDiscountTakaText.Value = String.Empty;
                itemTotalTakaText.Value = String.Empty;
                GetTotalAmount();
                //var exisiting = dt1.Rows.Find();

            }
        }
        //Grid Row Selected
        //protected void ItemOnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(salesGridView, "Select$" + e.Row.RowIndex);
        //        e.Row.Attributes["style"] = "cursor:pointer";
        //    }
        //}
        //Grid row command retrive value to TextBox
        //protected void salesGridView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ClearText();
        //    //itemLiteral.Text = "_";
        //    int index = salesGridView.SelectedRow.RowIndex;
        //    GridViewRow gvrow = salesGridView.Rows[index];
        //    string itemIdLvl = ((Label)gvrow.FindControl("Label2")).Text;
        //    string productName = gvrow.Cells[2].Text;
        //    string qty = gvrow.Cells[3].Text;
        //    string unitPrice = gvrow.Cells[4].Text;
        //    string total = gvrow.Cells[5].Text;

        //    productCodeText.Value = itemIdLvl;
        //    productNameDropDownList.SelectedValue = itemIdLvl;
        //    itemUpdateText.Value = itemIdLvl;
        //    qtyText.Value = qty;
        //    priceText.Value = unitPrice;
        //    itemTotalTakaText.Value = total;
        //}

        protected void productNameDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            qtyText.Value = "";
            //itemDiscountTakaText.Value = "";
            var getItemDetails = from x in db.tbl_ProductSizes
                                 where x.Id == Convert.ToInt32(productNameDropDownList.SelectedValue)
                                 select new { x.SalesPrice };
            foreach (var itemDetail in getItemDetails)
            {
                priceText.Value = itemDetail.SalesPrice.ToString();
            }
            qtyText.Focus();
        }

        //protected void salesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    int index = salesGridView.SelectedRow.RowIndex;
        //    //int index = Convert.ToInt32(e.NewEditIndex);
        //    GridViewRow gvrow = salesGridView.Rows[index];
        //    string itemIdLvl = ((Label)gvrow.FindControl("Label2")).Text;
        //    string productName = gvrow.Cells[2].Text;
        //    string qty = gvrow.Cells[3].Text;
        //    string unitPrice = gvrow.Cells[4].Text;
        //    string total = gvrow.Cells[5].Text;

        //    productCodeText.Value = itemIdLvl;
        //    productNameDropDownList.SelectedValue = itemIdLvl;
        //    itemUpdateText.Value = itemIdLvl;
        //    qtyText.Value = qty;
        //    priceText.Value = unitPrice;
        //    itemTotalTakaText.Value = total;
        //}
        protected void salesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditButton")
            {
                //DisableFeild();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvrow = salesGridView.Rows[index];
                string itemIdLvl = ((Label)gvrow.FindControl("Label2")).Text;
                string productName = gvrow.Cells[2].Text;
                string qty = gvrow.Cells[3].Text;
                string unitPrice = gvrow.Cells[4].Text;
                string total = gvrow.Cells[5].Text;

                productCodeText.Value = itemIdLvl;
                productNameDropDownList.SelectedValue = itemIdLvl;
                itemUpdateText.Value = index.ToString();
                qtyText.Value = qty;
                priceText.Value = unitPrice;
                itemTotalTakaText.Value = total;
                addButton.Text = "Update";
            }
            else if (e.CommandName == "DeleteButton")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                //int index = Convert.ToInt32(e.RowIndex);
                dt1 = Session["productInSession"] as DataTable;
                if (dt1 != null)
                {

                    //var v=dt1.Rows[index].
                    dt1.Rows[index].Delete();
                    dt1.AcceptChanges();
                    Session["productInSession"] = dt1;
                    salesGridView.DataSource = dt1;
                    GetTotalAmount();
                }
                salesGridView.DataBind();
            }
        }
    }
}