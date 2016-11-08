using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DevERP.Others
{
    public class Provider
    {

        private static Control[] FlattenHierachy(Control root)
        {
            List<Control> list = new List<Control>();
            list.Add(root);
            if (root.HasControls())
            {
                foreach (Control control in root.Controls)
                {
                    list.AddRange(FlattenHierachy(control));
                }
            }
            return list.ToArray();
        }

        public static void ClearTextBoxes(Control root)
        {
            Control[] allControls = FlattenHierachy(root);
            foreach (Control control in allControls)
            {
                HtmlInputText textBox = control as HtmlInputText;
                if (textBox != null)
                {
                    textBox.Value = "";
                }
                DropDownList dropDownList = control as DropDownList;
                if (dropDownList != null)
                {
                    dropDownList.SelectedIndex = -1;
                }
            }
        }

        public static DateTime StringToDateTime(string date)
        {
            try
            {
                return DateTime.Parse(date, new CultureInfo("en-US"));
            }
            catch (Exception)
            {
                return DateTime.MaxValue;
            }
        }
        public static string DateTimeToSting(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static DateTime GetMinDate()
        {
            return new DateTime(2000,01,01);
        }
        public static DateTime GetMaxDate()
        {
            return new DateTime(2100, 12, 31);
        }
        public enum Status
        {
            Success,
            Failed,
            ExistUser,
            ExistEmail,
            Others
        }
        public enum ChequeStatus
        {
            Pending,
            Pass,
            Cancel
        }

        public static string GetSuccessMassage(string msg)
        {
            return "<b><p style=color:green>" + msg + "</p></b>";
        }
        public static string GetErrorMassage(string msg)
        {
            return "<b><p style=color:red>" + msg + "</p></b>";
        }

        public static string GetMd5HashData(string data)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();
            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();
            //loop for each byte and add it to StringBuilder
            foreach (byte t in hashData)
            {
                returnValue.Append(t.ToString());
            }
            // return hexadecimal string
            return returnValue.ToString();

        }
        public static bool ValidateMd5HashData(string inputData, string storedHashData)
        {
            string getHashInputData = GetMd5HashData(inputData);
            if (String.CompareOrdinal(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            return false;
        }
        public static string GenarateSerial(int lenght)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[lenght];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new String(stringChars);
        }

        public static string ConvertEngilshToBangla(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string languagePair = "en|bn";
                string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                //string url1 = String.Format("https://translate.google.com/#en/bn/{0}", input);
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                string result;
                try
                {
                    result = webClient.DownloadString(url);
                    result = result.Substring(result.IndexOf("<span title=\"", StringComparison.Ordinal) + "<span title=\"".Length);
                    result = result.Substring(result.IndexOf(">", StringComparison.Ordinal) + 1);
                    result = result.Substring(0, result.IndexOf("</span>", StringComparison.Ordinal));
                }
                catch (Exception)
                {
                    return string.Empty;
                }

                return HttpUtility.HtmlDecode(result.Trim());
            }
            return string.Empty;
        }

        private static string ConvertEnglishToBanglaAll(string number)
        {
            return string.Concat(number.Select(c => (char)('\u09E6' + c - '0'))); // "১২৩৪৫৬৭৮৯০"
        }
        public static string ConvertEnglishDigitToBangla(string number)
        {
            string output = "";
            foreach (char c in number)
            {
                if (c >= 48 && c <= 57)
                {
                    output += ConvertEnglishToBanglaAll(c.ToString());
                }
                else
                {
                    output += c;
                }
            }
            return output;
        }

        public static string GetRandomString(int lenght)
        {
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < lenght; i++)
                s.Append(combination[random.Next(combination.Length)]);
            return s.ToString();
        }

    }
}