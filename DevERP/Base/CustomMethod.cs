using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevERP.Base
{
    public class CustomMethod
    {
        public string GenerateInvNo(string invPrefix, string oldInvNumber)
        {
            string InvoiceNumber = "";
            //int year = DateTime.Now.Year % 100;
            int year = DateTime.Now.Year;

            string invWithDate = invPrefix + "-" + year;
            var subInvNo = oldInvNumber.Substring(invWithDate.Length + 1);
            int invNo = Convert.ToInt32(subInvNo);

            //it will work when new year will come
            //var subYear = oldInvNumber.Substring(invPrefix.Length + 1, 2);
            var subYear = oldInvNumber.Substring(invPrefix.Length + 1, 4);
            int previousYear = Convert.ToInt32(subYear);
            if (year > previousYear)
            {
                return invWithDate + "-0000001";
            }
            if (invNo < 9)
                invWithDate += "-000000" + (invNo + 1);
            else if (invNo < 99)
                invWithDate += "-00000" + (invNo + 1);
            else if (invNo < 999)
                invWithDate += "-0000" + (invNo + 1);
            else if (invNo < 9999)
                invWithDate += "-000" + (invNo + 1);
            else if (invNo < 99999)
                invWithDate = "-00" + (invNo + 1);
            else if (invNo < 999999)
                invWithDate += "-0" + (invNo + 1);
            else
                invWithDate += (invNo + 1).ToString();

            InvoiceNumber = invWithDate;

            return InvoiceNumber;
        }
        public string GenerateId(string prefix,string oldNumber)
        {
            string generateIdNo = "";
            string getOldNumber = oldNumber.Substring(1, 5);
            int getNewNumber = Convert.ToInt32(getOldNumber) + 1;
            generateIdNo = prefix + getNewNumber;
            return generateIdNo;
        }

        public void GetSinglePageReport(GridView gridData,int hideIndex, string caption,string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + fileName + "_" + DateTime.Now + ".xls");
            HttpContext.Current.Response.Charset = "";
            //HttpContext.Current.EnableViewState = false;

            var stringWriter = new StringWriter();
            var htmlTextWriter = new HtmlTextWriter(stringWriter);
            gridData.Caption = caption;
            gridData.CaptionAlign = TableCaptionAlign.Top;
            gridData.BorderStyle = BorderStyle.None;
            if(hideIndex > -1)
                gridData.Columns[hideIndex].Visible = false;

            gridData.RenderControl(htmlTextWriter);

            HttpContext.Current.Response.Write(stringWriter.ToString());
            HttpContext.Current.Response.End();
        }

        public string GetMessage(string meessage, string messageType)
        {
            var messageFormat = "";
            messageFormat = "<div class='alert alert-" + messageType + " alert-dismissible' role='alert'>";
            messageFormat += "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
            messageFormat += meessage + "</div>";
            return messageFormat;
        }
        public string ConvertDateFormDb(string dateValue)
        {
            var month = dateValue.Substring(0, 2);
            var day = dateValue.Substring(3, 2);
            var year = dateValue.Substring(6, 4);
            var date = day + "/" + month + "/" + year;
            return date;
        }
        public string ConvertDateFormClient(string dateValue)
        {
            var day = dateValue.Substring(0, 2);
            var month = dateValue.Substring(3, 2);
            var year = dateValue.Substring(6, 4);
            var date = month + "/" + day + "/" + year;
            return date;
        }
        public  string GetIpAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            return ipAddress.ToString();
        }
        [WebMethod]
        public string SetSessionVariable(string sKey, dynamic sValue)
        {
            if (HttpContext.Current.Session[sKey] == null)
            {
                HttpContext.Current.Session[sKey] = sValue;
                return "session added";
            }
            else
            {
                HttpContext.Current.Session[sKey] = sValue;
                return "session updated";
            }

        }
        [WebMethod]
        public dynamic GetSessionVariable(string sKey)
        {
            if (HttpContext.Current.Session[sKey] != null)
            {
                dynamic myVar = HttpContext.Current.Session[sKey];
                return myVar;
            }
            else
            {
                return null;
            }

        }
     


    }
}