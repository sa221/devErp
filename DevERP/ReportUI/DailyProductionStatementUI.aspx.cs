using System;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevERP.Others;

namespace DevERP.ReportUI
{
    public partial class DailyProductionStatementUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void showReportButton_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            ReportDocument report=new ReportDocument();
            //DateTime datep = Convert.ToDateTime(Convert.ToDateTime(date.Value.ToString()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
            //DateTime fromDate = Convert.ToDateTime(fromDateTextBox.Text);
            var pDate = DateTime.ParseExact(date.Value, "dd/MM/yyyy", null);
            //var pDate = Convert.ToDateTime(date.Value.ToString())
            //        .ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            report.Load(Server.MapPath("~/Reports/DailyProductionStatement.rpt"));
            // Create the connection object
            //ConnectionInfo connectionInfo = ReportConection.CreateConnection(".", 1433, "DevERP", "sa", "arafat7218");

            //// Set the connection info on each table in the report
            //ReportConection.SetDbLogonForReport(connectionInfo, report);
            //ReportConection.SetDbLogonForSubreports(connectionInfo, report);pDate.ToString("yyyy-MM-dd")
            var p = "2016-11-30";
            if (date.Value!="")
            {
                dailyStatementViewer.ReportSource = report;
                dailyStatementViewer.SelectionFormula = "{tbl_DailyStock.RecordDate}='" + pDate.ToString("yyyy-MM-dd") + "'";
                dailyStatementViewer.RefreshReport();
            }
        }
    }
}