using System;
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
            var datep = Convert.ToDateTime(date.Value);
            report.Load(Server.MapPath("~/Reports/DailyProductionStatement.rpt"));
            // Create the connection object
            ConnectionInfo connectionInfo = ReportConection.CreateConnection(".", 1433, "DevERP", "sa", "arafat7218");

            // Set the connection info on each table in the report
            ReportConection.SetDbLogonForReport(connectionInfo, report);
            ReportConection.SetDbLogonForSubreports(connectionInfo, report);
            if (date.Value!="")
            {
                dailyStatementViewer.ReportSource = report;
                dailyStatementViewer.SelectionFormula = "{tbl_DailyStock.RecordDate}='" + datep.ToString("yyyy-MM-dd") + "'";
                dailyStatementViewer.RefreshReport();
            }
        }
    }
}