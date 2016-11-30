using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

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
            if (date.Value!="")
            {
                dailyStatementViewer.ReportSource = report;
                dailyStatementViewer.SelectionFormula = "{tbl_DailyStock.RecordDate}='" + datep.ToString("yyyy-MM-dd") + "'";
                dailyStatementViewer.RefreshReport();
            }
        }
    }
}