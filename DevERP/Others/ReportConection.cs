using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace DevERP.Others
{
    public class ReportConection
    {
        public static ConnectionInfo CreateConnection(string server,
                                                       int port,
                                                       string databaseName,
                                                       string username,
                                                       string password)
        {

            ConnectionInfo connectionInfo = new ConnectionInfo();

            string connString = "DRIVER=InterSystems ODBC;SERVER=" + server
                                                      //+ ";PORT=" + port
                                                      + ";DATABASE=" + databaseName
                                                      + ";UID=" + username
                                                      + ";PWD=" + password;

            connectionInfo.IntegratedSecurity = false;
            connectionInfo.UserID = username;
            connectionInfo.Password = password;
            //In examples that I have seen, this is the actual connection string, not just the server name


            connectionInfo.ServerName = connString;
            connectionInfo.DatabaseName = databaseName;
            connectionInfo.Type = ConnectionInfoType.CRQE;

            return connectionInfo;

        }

        public static void SetDbLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in reportDocument.Database.Tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        public static void SetDbLogonForSubreports(ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            foreach (Section section in reportDocument.ReportDefinition.Sections)
            {
                foreach (ReportObject reportObject in section.ReportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subreportObject = (SubreportObject)reportObject;
                        ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                        SetDbLogonForReport(connectionInfo, subReportDocument);
                    }
                }
            }
        }
    }
}