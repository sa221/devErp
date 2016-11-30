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
        DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProductDdl();
                LoadProductionGrid();
                LoadDailyStockFromStock(DateTime.Now.Date);
                productionDate.Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
                saveButton.Text = "Save";
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
            successStatusLavel.InnerText = "";
            failStatusLavel.InnerText = "";
            if (empId.Value != "")
            {
                tbl_EmployeeEntry getEmployee = db.tbl_EmployeeEntries.FirstOrDefault(c => c.EmployeeId == empId.Value);
                if (getEmployee != null)
                {
                    empName.InnerText = getEmployee.FirstName + "" + getEmployee.LastName;
                    LoadProductionGrid();
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
            productQuantity.Value = "";
            totalTaka.Value = "";
            tbl_ProductSize getRate = db.tbl_ProductSizes.FirstOrDefault(c => c.Id == Convert.ToInt32(productDropDownList.SelectedValue));
            if (getRate != null)
            {
                productRate.Value = getRate.Rate.ToString();
            }
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            EnableFeild();
            successStatusLavel.InnerText = "";
            failStatusLavel.InnerText = "";
            if (empId.Value != "" && productionDate.Value != "" && productDropDownList.SelectedValue != "0" && productRate.Value != "" && productQuantity.Value != "" && totalTaka.Value != "")
            {
                int productId = Convert.ToInt32(productDropDownList.SelectedValue);
                Double quantity = Convert.ToDouble(productQuantity.Value);
                DateTime dateTime = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                if (previousQtyTextBox.Text != "")
                {
                    double newQuantity = (Convert.ToDouble(productQuantity.Value) -
                                         Convert.ToDouble(previousQtyTextBox.Text));
                    UpdateDailyStockFromStockInEdit(dateTime, newQuantity,
                        Convert.ToInt32(productDropDownList.SelectedValue));
                }
               
                    InsertIntoProductionInfo();
                    InsertIntoDailyStock();
                    InsertIntoStock(productId, quantity);
               
                
                

                //tbl_ProductionInfo productionInfo = new tbl_ProductionInfo();
                //tbl_DailyStock dailyStock = new tbl_DailyStock();

                //if (productionIdTextBox.Text == "" && empId.Value != "")
                //{
                //    //Insert
                //    var checkEmpId = db.tbl_EmployeeEntries.FirstOrDefault(c => c.EmployeeId == empId.Value);
                //    if (checkEmpId != null)
                //    {
                //        var checkProductOnDate = db.tbl_ProductionInfos.FirstOrDefault(c => c.EmpId == empId.Value && c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) && c.ProductionDate == DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null));
                //        if (checkProductOnDate != null)
                //        {
                //            checkProductOnDate.Quantity = checkProductOnDate.Quantity + Convert.ToDouble(productQuantity.Value);

                //            checkProductOnDate.TotalRate = checkProductOnDate.TotalRate + Convert.ToDecimal(totalTaka.Value);

                //            db.SubmitChanges();
                //            successStatusLavel.InnerText = "Product Added Successfully";
                //            LoadProductionGrid();
                //            //empId.Value = String.Empty;
                //            productDropDownList.SelectedValue = "0";
                //            productQuantity.Value = String.Empty;
                //            productRate.Value = String.Empty;
                //            totalTaka.Value = String.Empty;
                //        }
                //        else
                //        {
                //            if (!String.IsNullOrWhiteSpace(productionDate.Value))
                //            {
                //                DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                //                productionInfo.ProductionDate = date;
                //            }
                //            productionInfo.EmpId = empId.Value;
                //            productionInfo.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                //            productionInfo.Quantity = Convert.ToDouble(productQuantity.Value);
                //            productionInfo.Rate = Convert.ToDecimal(productRate.Value);
                //            productionInfo.TotalRate = Convert.ToDecimal(totalTaka.Value);
                //            productionInfo.InputDate = DateTime.Now.Date;
                //            productionInfo.InputBy = "session";
                //            db.tbl_ProductionInfos.InsertOnSubmit(productionInfo);
                //            db.SubmitChanges();
                //            successStatusLavel.InnerText = "Product Received Successfully";
                //            LoadProductionGrid();
                //            //empId.Value = String.Empty;

                //            //Insert Into Stock
                //            InsertIntoStock(Convert.ToInt32(productDropDownList.SelectedValue), Convert.ToDouble(productQuantity.Value));
                //            //Insert Into Daily Stock
                //            var checkDailyStock =
                //                db.tbl_DailyStocks.FirstOrDefault(
                //                    c =>
                //                        c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) &&
                //                        c.RecordDate == DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null));
                //            if (checkDailyStock != null)
                //            {
                //                if (checkDailyStock.DailyProduction==null)
                //                {
                //                    checkDailyStock.DailyProduction = Convert.ToDouble(productQuantity.Value);                            
                //                }
                //                else
                //                {
                //                    checkDailyStock.DailyProduction = checkDailyStock.DailyProduction +
                //                                                  Convert.ToDouble(productQuantity.Value);
                //                }
                //                db.SubmitChanges();
                //            }
                //            else
                //            {
                //                if (!String.IsNullOrWhiteSpace(productionDate.Value))
                //                {
                //                    DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                //                    dailyStock.RecordDate = date;
                //                }
                //                dailyStock.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                //                dailyStock.DailyProduction = Convert.ToDouble(productQuantity.Value);
                //                db.tbl_DailyStocks.InsertOnSubmit(dailyStock);
                //                db.SubmitChanges();
                //            }

                //            productDropDownList.SelectedValue = "0";
                //            productQuantity.Value = String.Empty;
                //            productRate.Value = String.Empty;
                //            totalTaka.Value = String.Empty;
                //        }

                //    }
                //    else
                //    {
                //        failStatusLavel.InnerText = "Invalid Employee ID";
                //    }
                //}
                //else if (productionIdTextBox.Text != "" && empId.Value != "")
                //{
                //    var checkProductionId =
                //        db.tbl_ProductionInfos.FirstOrDefault(c => c.Id == Convert.ToInt32(productionIdTextBox.Text) && c.EmpId == empId.Value);
                //    if (checkProductionId != null)
                //    {
                //        if (!String.IsNullOrWhiteSpace(productionDate.Value))
                //        {
                //            DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                //            checkProductionId.ProductionDate = date;
                //        }
                //        checkProductionId.EmpId = empId.Value;
                //        checkProductionId.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                //        checkProductionId.Quantity = Convert.ToDouble(productQuantity.Value);
                //        checkProductionId.Rate = Convert.ToDecimal(productRate.Value);
                //        checkProductionId.TotalRate = Convert.ToDecimal(totalTaka.Value);
                //        checkProductionId.UpdateDate = DateTime.Now.Date;
                //        checkProductionId.UpdateBy = "session";
                //        db.SubmitChanges();
                //        successStatusLavel.InnerText = "Product Info Updated Successfully";
                //        LoadProductionGrid();
                //        //empId.Value = String.Empty;

                //    }
                //    double newQuantity = Convert.ToDouble(productQuantity.Value) - Convert.ToDouble(previousQtyTextBox.Text);
                //    var checkInStock =
                //        db.tbl_Stocks.FirstOrDefault(
                //            c => c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue));
                //    if (checkInStock!=null)
                //    {
                //        checkInStock.Quantity = checkInStock.Quantity + newQuantity;
                //    }
                //    var checkInDailyStock =
                //        db.tbl_DailyStocks.FirstOrDefault(
                //            c =>
                //                c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) &&
                //                c.RecordDate == DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null));
                //    if (checkInDailyStock!=null)
                //    {
                //        checkInDailyStock.DailyProduction = checkInDailyStock.DailyProduction + newQuantity;
                //    }
                //    DateTime dateTime = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                //    UpdateDailyStockFromStockInEdit(dateTime, newQuantity,
                //        Convert.ToInt32(productDropDownList.SelectedValue));

                //    db.SubmitChanges();
                //    productDropDownList.SelectedValue = "0";
                //    productQuantity.Value = String.Empty;
                //    productRate.Value = String.Empty;
                //    totalTaka.Value = String.Empty;
                //    saveButton.Text = "Save";
                //}
                productDropDownList.SelectedValue = "0";
                productionIdTextBox.Text = "";
                productQuantity.Value = String.Empty;
                productRate.Value = String.Empty;
                totalTaka.Value = String.Empty;
                saveButton.Text = "Save";
            }
            else
            {
                failStatusLavel.InnerText = "Please Fill All Required Field!";
            }
        }

        private void LoadProductionGrid()
        {
            if (!String.IsNullOrWhiteSpace(productionDate.Value) && empId.Value != "")
            {
                DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                var getAllProduction = from c in db.tbl_ProductionInfos
                                       join s in db.tbl_ProductSizes on c.ProductId equals s.Id into uGroup
                                       from s in uGroup.DefaultIfEmpty()
                                       where c.EmpId == empId.Value && c.ProductionDate == date
                                       select new { c.ProductionDate, s.FullProductName, c.ProductId, c.Id, c.Rate, c.Quantity, c.TotalRate, c.EmpId };
                productionGridView.DataSource = getAllProduction.AsEnumerable();
                productionGridView.DataBind();
            }
            else if (productionDate.Value == "" && empId.Value != "")
            {
                var getAllProduction = from c in db.tbl_ProductionInfos
                                       join s in db.tbl_ProductSizes on c.ProductId equals s.Id into uGroup
                                       from s in uGroup.DefaultIfEmpty()
                                       where c.EmpId == empId.Value
                                       select new { c.ProductionDate, s.FullProductName, c.ProductId, c.Id, c.Rate, c.Quantity, c.TotalRate, c.EmpId };
                productionGridView.DataSource = getAllProduction.AsEnumerable();
                productionGridView.DataBind();
            }

        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productionGridView.PageIndex = e.NewPageIndex;
            this.LoadProductionGrid();
        }
        protected void productionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditButton")
            {
                DisableFeild();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = productionGridView.Rows[index];
                string employeeId = ((Label)gvRow.FindControl("empIdLabel")).Text;
                string date = ((Label)gvRow.FindControl("Label1")).Text;
                string productId = ((Label)gvRow.FindControl("Label2")).Text;
                string rate = ((Label)gvRow.FindControl("Label3")).Text;
                string quantity = ((Label)gvRow.FindControl("Label4")).Text;
                string amount = ((Label)gvRow.FindControl("Label5")).Text;
                string productionId = ((Label)gvRow.FindControl("Label7")).Text;

                empId.Value = employeeId;
                productionDate.Value = date;
                productDropDownList.SelectedValue = productId;
                productRate.Value = rate;
                productQuantity.Value = quantity;
                previousQtyTextBox.Text = quantity;
                totalTaka.Value = amount;
                productionIdTextBox.Text = productionId;
                saveButton.Text = "Update";
            }
            else if (e.CommandName == "DeleteButton")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = productionGridView.Rows[index];
                string employeeId = ((Label)gvRow.FindControl("empIdLabel")).Text;
                string rate = ((Label)gvRow.FindControl("Label3")).Text;
                string amount = ((Label)gvRow.FindControl("Label5")).Text;
                string productionId = ((Label)gvRow.FindControl("Label7")).Text;
                string date = ((Label)gvRow.FindControl("Label1")).Text;
                string productId = ((Label)gvRow.FindControl("Label2")).Text;
                string quantity = ((Label)gvRow.FindControl("Label4")).Text;

                tbl_ProductionInfo productionInfo = db.tbl_ProductionInfos.FirstOrDefault(c => c.Id == Convert.ToInt32(productionId));
                if (productionInfo != null)
                    db.tbl_ProductionInfos.DeleteOnSubmit(productionInfo);
                var checkInDailyStock =
                    db.tbl_DailyStocks.FirstOrDefault(
                        c => c.ProductId == Convert.ToInt32(productId) && c.RecordDate == DateTime.ParseExact(date, "dd/MM/yyyy", null));
                if (checkInDailyStock != null)
                {
                    if (checkInDailyStock.DailyProduction != null)
                    {
                        checkInDailyStock.DailyProduction = checkInDailyStock.DailyProduction - Convert.ToDouble(quantity);
                    }
                }
                UpdateDailyStockFromStock(Convert.ToDateTime(date), Convert.ToDouble(quantity), Convert.ToInt32(productId));

                var checkStock = db.tbl_Stocks.FirstOrDefault(c => c.ProductId == Convert.ToInt32(productId));
                if (checkStock != null) checkStock.Quantity = checkStock.Quantity - Convert.ToDouble(quantity);
                tbl_ProductionDeleteHistory deleteHistory = new tbl_ProductionDeleteHistory();
                deleteHistory.EmpId = employeeId;
                deleteHistory.ProductId = Convert.ToInt32(productId);
                deleteHistory.ProductionDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
                deleteHistory.Rate = Convert.ToDecimal(rate);
                deleteHistory.Qty = Convert.ToDouble(quantity);
                deleteHistory.Taka = Convert.ToDecimal(amount);
                deleteHistory.DeleteBy = "session";
                deleteHistory.DeleteDate = DateTime.Now.Date;
                db.tbl_ProductionDeleteHistories.InsertOnSubmit(deleteHistory);
                db.SubmitChanges();
                LoadProductionGrid();
            }
        }

        private void LoadDailyStockFromStock(DateTime date)
        {
            var checkRecord = db.tbl_DailyStocks.FirstOrDefault(c => c.RecordDate == date);
            if (checkRecord == null)
            {
                var getAllFromStock = from c in db.tbl_Stocks
                                      select new { c.ProductId, c.Quantity };
                foreach (var getData in getAllFromStock)
                {
                    tbl_DailyStock dailyStock = new tbl_DailyStock();
                    dailyStock.RecordDate = date;
                    dailyStock.ProductId = getData.ProductId;
                    dailyStock.PreviousQuantity = getData.Quantity;
                    db.tbl_DailyStocks.InsertOnSubmit(dailyStock);
                }
                db.SubmitChanges();
            }
        }
        private void UpdateDailyStockFromStock(DateTime date, double qty, int productId)
        {
            var checkRecord = db.tbl_DailyStocks.FirstOrDefault(c => c.RecordDate >= date && c.ProductId == productId);
            if (checkRecord != null)
            {
                var getAllFromStock = from c in db.tbl_DailyStocks
                                      where c.RecordDate >= date && c.ProductId == productId
                                      select new { c.ProductId, c.PreviousQuantity, c.RecordDate };
                foreach (var getData in getAllFromStock)
                {
                    var getP =
                        db.tbl_DailyStocks.FirstOrDefault(
                            c => c.ProductId == getData.ProductId && c.RecordDate == getData.RecordDate);
                    if (getP != null) getP.PreviousQuantity = getData.PreviousQuantity - qty;
                }
                db.SubmitChanges();
            }
        }
        private void UpdateDailyStockFromStockInEdit(DateTime date, double qty, int productId)
        {
            var checkRecord = db.tbl_DailyStocks.FirstOrDefault(c => c.RecordDate >= date && c.ProductId == productId);
            if (checkRecord != null)
            {
                var getAllFromStock = from c in db.tbl_DailyStocks
                                      where c.RecordDate >= date && c.ProductId == productId
                                      select new { c.ProductId, c.PreviousQuantity, c.RecordDate,c.DailyProduction};
                foreach (var getData in getAllFromStock)
                {
                    var getP =
                        db.tbl_DailyStocks.FirstOrDefault(
                            c => c.ProductId == getData.ProductId && c.RecordDate == getData.RecordDate);
                    if (date != DateTime.Now.Date)
                    {
                        if (getP != null) getP.PreviousQuantity = getData.PreviousQuantity + qty;
                    }
                    else if (date == DateTime.Now.Date)
                    {
                        if (getP != null) getP.DailyProduction = getData.DailyProduction + qty; 
                    }
                    
                }
                db.SubmitChanges();
            }
        }
        private void InsertIntoStock(int productId, double quantity)
        {
            if (empId.Value != "" && productionIdTextBox.Text == "")
            {
                var checkProduct = db.tbl_Stocks.FirstOrDefault(c => c.ProductId == productId);
                if (checkProduct != null)
                {
                    checkProduct.Quantity = checkProduct.Quantity + quantity;
                }
                else
                {
                    tbl_Stock stock = new tbl_Stock();

                    stock.ProductId = productId;
                    stock.Quantity = quantity;
                    db.tbl_Stocks.InsertOnSubmit(stock);
                }
                db.SubmitChanges();
            }
            else if (empId.Value != "" && productionIdTextBox.Text != "")
            {
                double newQuantity = Convert.ToDouble(productQuantity.Value) - Convert.ToDouble(previousQtyTextBox.Text);
                var checkInStock =
                    db.tbl_Stocks.FirstOrDefault(
                        c => c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue));
                if (checkInStock != null)
                {
                    checkInStock.Quantity = checkInStock.Quantity + newQuantity;
                    db.SubmitChanges();
                }
            }
            
        }

        private void InsertIntoProductionInfo()
        {
            if (empId.Value != "" && productionIdTextBox.Text=="")
            {
                //Insert
                tbl_ProductionInfo productionInfo = new tbl_ProductionInfo();
                var checkEmpId = db.tbl_EmployeeEntries.FirstOrDefault(c => c.EmployeeId == empId.Value);
                if (checkEmpId != null)
                {
                    var checkProductOnDate =
                        db.tbl_ProductionInfos.FirstOrDefault(
                            c =>
                                c.EmpId == empId.Value &&
                                c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) &&
                                c.ProductionDate == DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null));
                    if (checkProductOnDate != null)
                    {
                        checkProductOnDate.Quantity = checkProductOnDate.Quantity +
                                                      Convert.ToDouble(productQuantity.Value);

                        checkProductOnDate.TotalRate = checkProductOnDate.TotalRate + Convert.ToDecimal(totalTaka.Value);

                        db.SubmitChanges();
                        successStatusLavel.InnerText = "Product Added Successfully";
                        LoadProductionGrid();
                        //empId.Value = String.Empty;
                        //productDropDownList.SelectedValue = "0";
                        //productQuantity.Value = String.Empty;
                        //productRate.Value = String.Empty;
                        //totalTaka.Value = String.Empty;
                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(productionDate.Value))
                        {
                            DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                            productionInfo.ProductionDate = date;
                        }
                        productionInfo.EmpId = empId.Value;
                        productionInfo.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                        productionInfo.Quantity = Convert.ToDouble(productQuantity.Value);
                        productionInfo.Rate = Convert.ToDecimal(productRate.Value);
                        productionInfo.TotalRate = Convert.ToDecimal(totalTaka.Value);
                        productionInfo.InputDate = DateTime.Now.Date;
                        productionInfo.InputBy = "session";
                        db.tbl_ProductionInfos.InsertOnSubmit(productionInfo);
                        db.SubmitChanges();
                        successStatusLavel.InnerText = "Product Received Successfully";
                        LoadProductionGrid();
                    }
                }
            }
            else if (empId.Value != "" && productionIdTextBox.Text!="")
            {
                //failStatusLavel.InnerText = "Please Enter Employee ID.";
                var checkProductionId =
                    db.tbl_ProductionInfos.FirstOrDefault(
                        c => c.Id == Convert.ToInt32(productionIdTextBox.Text) && c.EmpId == empId.Value);
                if (checkProductionId != null)
                {
                    if (!String.IsNullOrWhiteSpace(productionDate.Value))
                    {
                        DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                        checkProductionId.ProductionDate = date;
                    }
                    checkProductionId.EmpId = empId.Value;
                    checkProductionId.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                    checkProductionId.Quantity = Convert.ToDouble(productQuantity.Value);
                    checkProductionId.Rate = Convert.ToDecimal(productRate.Value);
                    checkProductionId.TotalRate = Convert.ToDecimal(productQuantity.Value) * Convert.ToDecimal(productRate.Value);
                    checkProductionId.UpdateDate = DateTime.Now.Date;
                    checkProductionId.UpdateBy = "session";
                    db.SubmitChanges();
                    successStatusLavel.InnerText = "Product Info Updated Successfully";
                    LoadProductionGrid();
                    //empId.Value = String.Empty;

                }
            }
        }

        private void InsertIntoDailyStock()
        {
            if (empId.Value != "" && productionIdTextBox.Text == "")
            {
                tbl_DailyStock dailyStock = new tbl_DailyStock();

                var checkDailyStock =
                                    db.tbl_DailyStocks.FirstOrDefault(
                                        c =>
                                            c.ProductId == Convert.ToInt32(productDropDownList.SelectedValue) &&
                                            c.RecordDate == DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null));
                if (checkDailyStock != null)
                {
                    if (checkDailyStock.DailyProduction == null)
                    {
                        checkDailyStock.DailyProduction = Convert.ToDouble(productQuantity.Value);
                    }
                    else
                    {
                        checkDailyStock.DailyProduction = checkDailyStock.DailyProduction +
                                                      Convert.ToDouble(productQuantity.Value);
                    }
                    db.SubmitChanges();
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(productionDate.Value))
                    {
                        DateTime date = DateTime.ParseExact(productionDate.Value, "dd/MM/yyyy", null);
                        dailyStock.RecordDate = date;
                    }
                    dailyStock.ProductId = Convert.ToInt32(productDropDownList.SelectedValue);
                    dailyStock.DailyProduction = Convert.ToDouble(productQuantity.Value);
                    db.tbl_DailyStocks.InsertOnSubmit(dailyStock);
                    db.SubmitChanges();
                }
            }
            

            //productDropDownList.SelectedValue = "0";
            //productQuantity.Value = String.Empty;
            //productRate.Value = String.Empty;
            //totalTaka.Value = String.Empty;

        }
        private void EnableFeild()
        {
            empId.Disabled = false;
            productionDate.Disabled = false;
            productDropDownList.Enabled = true;
            totalTaka.Disabled = false;
        }
        private void DisableFeild()
        {
            empId.Disabled = true;
            productionDate.Disabled = true;
            productDropDownList.Enabled = false;
            totalTaka.Disabled = true;
        }
    }
}