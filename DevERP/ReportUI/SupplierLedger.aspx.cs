using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.ReportUI
{
    public partial class SupplierLedger : System.Web.UI.Page
    {
        private static DevERPDBDataContext db = new DevERPDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllSupplier();

            }
        }
        private void GetAllSupplier()
        {
            var getAll = from x in db.tblSuppliers
                         select new { x.SupplierId, x.OrganizationName };
            supplierDropdownlist.DataSource = getAll.AsEnumerable();
            supplierDropdownlist.DataValueField = "SupplierId";
            supplierDropdownlist.DataTextField = "OrganizationName";
            supplierDropdownlist.DataBind();

        }
        [WebMethod]
        public static object GetSupplierLedgerReport(string fromDate, string toDate, string supplierId)
        {
            object tempDataF = null;
            db.ExecuteCommand("truncate table TempLedger");
            db.ExecuteCommand("truncate table TempLedgerFinal");
            db.Dispose();
            db = new DevERPDBDataContext();
            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate) && !string.IsNullOrEmpty(supplierId))
            {
                DateTime fDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime tDate = DateTime.ParseExact(toDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime opDate = Convert.ToDateTime(fDate.AddDays(-1).ToString("yyyy-MM-dd"));
                DateTime clDate = Convert.ToDateTime(tDate.AddDays(+1).ToString("yyyy-MM-dd"));
                var supId = supplierId;
                var opBalance = db.tblSuppliers.FirstOrDefault(x => x.SupplierId == supId);
                if (opBalance != null)
                {
                    var opBlancePurchase = from pu in db.tblPurchases
                                           where (pu.PurchaseDate) <= opDate && Convert.ToString(pu.SupplierId) == supId && pu.Approve == "Y"
                                           select new { pu.Amount };
                    var opBlancePayment = from pu in db.tblPayments
                                          where (pu.PayDate) <= opDate && Convert.ToString(pu.SupplierId) == supId && pu.Approve == "Y"
                                          select new { pu.PayAmount };
                    var opBlancePurchaseReturn = from pu in db.tblReturns
                                                 where (pu.PurchaseDate) <= opDate && Convert.ToString(pu.SupplierId) == supId && pu.Approve == "Y"
                                                 select new { pu.Amount };
                    //1
                    if (opBlancePurchase.FirstOrDefault() != null && opBlancePayment.FirstOrDefault() != null && opBlancePurchaseReturn.FirstOrDefault() != null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance + opBlancePurchase.Sum(x => x.Amount) - (opBlancePayment.Sum(x => x.PayAmount) + opBlancePurchaseReturn.Sum(x => x.Amount)));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //2
                    else if (opBlancePurchase.FirstOrDefault() != null && opBlancePayment.FirstOrDefault() != null && opBlancePurchaseReturn.FirstOrDefault() == null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance + opBlancePurchase.Sum(x => x.Amount) - opBlancePayment.Sum(x => x.PayAmount));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //3
                    else if (opBlancePurchase.FirstOrDefault() != null && opBlancePayment.FirstOrDefault() == null && opBlancePurchaseReturn.FirstOrDefault() != null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance + opBlancePurchase.Sum(x => x.Amount) - opBlancePurchaseReturn.Sum(x => x.Amount));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //4
                    else if (opBlancePurchase.FirstOrDefault() == null && opBlancePayment.FirstOrDefault() != null && opBlancePurchaseReturn.FirstOrDefault() != null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance - (opBlancePayment.Sum(x => x.PayAmount) + opBlancePurchaseReturn.Sum(x => x.Amount)));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //5
                    else if (opBlancePurchase.FirstOrDefault() != null && opBlancePayment.FirstOrDefault() == null && opBlancePurchaseReturn.FirstOrDefault() == null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance + opBlancePurchase.Sum(x => x.Amount));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //6
                    else if (opBlancePurchase.FirstOrDefault() == null && opBlancePayment.FirstOrDefault() != null && opBlancePurchaseReturn.FirstOrDefault() == null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance - opBlancePayment.Sum(x => x.PayAmount));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //7
                    else if (opBlancePurchase.FirstOrDefault() == null && opBlancePayment.FirstOrDefault() == null && opBlancePurchaseReturn.FirstOrDefault() != null)
                    {
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        tempLedger.Balance = (decimal?)(opBalance.OpeningBalance - opBlancePurchaseReturn.Sum(x => x.Amount));
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                    //8
                    else
                    {
                        var getOpBalance =
                            db.tblSuppliers.FirstOrDefault(x => x.SupplierId == supId);
                        TempLedger tempLedger = new TempLedger();
                        tempLedger.DatDate = opDate;
                        tempLedger.VoucherNo = "Opening";
                        if (getOpBalance != null) tempLedger.Balance = (decimal?)getOpBalance.OpeningBalance;
                        db.TempLedgers.InsertOnSubmit(tempLedger);
                        db.SubmitChanges();
                    }
                }

                var getPurchase = from p in db.tblPurchases
                                  join s in db.tblSuppliers on p.SupplierId equals s.SupplierId into sGroup
                                  from s in sGroup.DefaultIfEmpty()
                                  where p.PurchaseDate >= fDate && p.PurchaseDate <= tDate && s.SupplierId == supId && p.Approve == "Y"
                                  select new { p.PurchaseDate, p.PurchaseInvNo, p.Remarks, p.Amount };
                foreach (var purchase in getPurchase)
                {
                    TempLedger tempLedger = new TempLedger();
                    tempLedger.DatDate = purchase.PurchaseDate;
                    tempLedger.VoucherNo = purchase.PurchaseInvNo;
                    tempLedger.HeadsName = "Purchase";
                    tempLedger.Description = purchase.Remarks;
                    tempLedger.Credit = (decimal?)purchase.Amount;
                    tempLedger.Debit = 0;
                    tempLedger.Balance = 0;
                    db.TempLedgers.InsertOnSubmit(tempLedger);
                    db.SubmitChanges();
                }
                var getPayment = from p in db.tblPayments
                                 join s in db.tblSuppliers on p.SupplierId equals s.SupplierId into sGroup
                                 from s in sGroup.DefaultIfEmpty()
                                 where p.PayDate >= fDate && p.PayDate <= tDate && s.SupplierId == supId && p.Approve == "Y"
                                 select new { p.PayDate, p.PayInvoiceNo, p.Remarks, p.PayAmount };

                foreach (var purchase in getPayment)
                {
                    TempLedger tempLedger = new TempLedger();
                    tempLedger.DatDate = purchase.PayDate;
                    tempLedger.VoucherNo = purchase.PayInvoiceNo;
                    tempLedger.HeadsName = "Payment";
                    tempLedger.Description = purchase.Remarks;
                    tempLedger.Debit = (decimal?)purchase.PayAmount;
                    tempLedger.Credit = 0;
                    tempLedger.Balance = 0;
                    db.TempLedgers.InsertOnSubmit(tempLedger);
                    db.SubmitChanges();
                }
                var getPurchaseReturn = from p in db.tblReturns
                                        join s in db.tblSuppliers on p.SupplierId equals s.SupplierId into sGroup
                                        from s in sGroup.DefaultIfEmpty()
                                        where p.PurchaseDate >= fDate && p.PurchaseDate <= tDate && s.SupplierId == supId && p.Approve == "Y"
                                        select new { p.PurchaseDate, p.PurchaseInvNo, p.Remarks, p.Amount };

                foreach (var purchase in getPurchaseReturn)
                {
                    TempLedger tempLedger = new TempLedger();
                    tempLedger.DatDate = purchase.PurchaseDate;
                    tempLedger.VoucherNo = purchase.PurchaseInvNo;
                    tempLedger.HeadsName = "Purchase Return";
                    tempLedger.Description = purchase.Remarks;
                    tempLedger.Debit = (decimal?)purchase.Amount;
                    tempLedger.Credit = 0;
                    tempLedger.Balance = 0;
                    db.TempLedgers.InsertOnSubmit(tempLedger);
                    db.SubmitChanges();
                }
                decimal baln = 0;
                var tempData = from c in db.TempLedgers
                               orderby c.DatDate, c.VoucherNo
                               select new { c.DatDate, c.HeadsName, c.VoucherNo, c.Description, c.Debit, c.Credit, c.Balance };
                foreach (var insertNewTemp in tempData)
                {
                    var s = insertNewTemp.Balance;

                    if (s == 0)
                    {
                        s = baln;
                    }
                    TempLedgerFinal tempLedger = new TempLedgerFinal();
                    tempLedger.FDatDate = insertNewTemp.DatDate;
                    tempLedger.FHeadsName = insertNewTemp.HeadsName;
                    tempLedger.FVoucherNo = insertNewTemp.VoucherNo;
                    tempLedger.FDescription = insertNewTemp.Description;
                    tempLedger.FDebit = insertNewTemp.Debit;
                    tempLedger.FCredit = insertNewTemp.Credit;
                    tempLedger.FBalance = s + Convert.ToDecimal(insertNewTemp.Credit - insertNewTemp.Debit);
                    db.TempLedgerFinals.InsertOnSubmit(tempLedger);
                    db.SubmitChanges();
                    baln = (decimal)(s + Convert.ToDecimal(insertNewTemp.Credit - insertNewTemp.Debit));
                }
                TempLedgerFinal tempLedgerFinal = new TempLedgerFinal();
                tempLedgerFinal.FDatDate = clDate;
                tempLedgerFinal.FHeadsName = "";
                tempLedgerFinal.FVoucherNo = "Closing";
                tempLedgerFinal.FDescription = "";
                tempLedgerFinal.FBalance = baln;
                db.TempLedgerFinals.InsertOnSubmit(tempLedgerFinal);
                db.SubmitChanges();
                tempDataF = from c in db.TempLedgerFinals
                            select new
                            {
                                FDatDate = string.Format("{0:MM/dd/yyyy}", c.FDatDate),
                                c.FHeadsName,
                                c.FVoucherNo,
                                c.FDescription,
                                c.FDebit,
                                c.FCredit,
                                c.FBalance
                            };

            }
            return tempDataF;
        }
    }
}