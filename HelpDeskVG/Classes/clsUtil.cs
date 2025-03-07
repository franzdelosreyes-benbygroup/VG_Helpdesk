using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;

namespace HelpDeskVG.Classes
{
    static class Globals
    {

        public static string FILE_JPG = "255216";
        public static string FILE_BMP = "6677";
        public static string FILE_PNG = "13780";
        public static string FILE_PDF = "3780";
    }
    public class clsUtil
    {
        /// Escapes quotes in string to prevent error in SQL Statements.
        /// </summary>
        /// <param name="tmpStr"></param>
        /// <returns>formatted string</returns>
        public static string replaceQuote(string tmpStr)
        {
            tmpStr = tmpStr.Replace("'", "''");
            return tmpStr;
        }
        /// <summary>
        /// Escapes quotes in string to prevent error in SQL Statements and returns Uppercase version of string.
        /// </summary>
        /// <param name="tmpStr"></param>
        /// <returns>formatted string</returns>
        //public static string replaceQuoteU(string tmpStr)
        //{
        //    tmpStr = tmpStr.Replace("'", "''");
        //    return tmpStr.ToUpper();
        //}
        /// <summary>
        /// Checks whether the object is null
        /// </summary>
        /// <param name="str"></param>
        /// <returns>boolean</returns>
        public static bool isNull(object str)
        {
            if (str == DBNull.Value || str == null)
                return true;
            else
                return false;
        }
        public static object convertEmptyStringToNull(string str)
        {
            if (str == "")
                return Convert.DBNull;
            else
                return str;
        }
        public static string convertNullToEmptyString(object str)
        {
            if (str == DBNull.Value || str == null)
                return "";
            else
                return str.ToString().Trim();
        }
        /// <summary>
        /// Convert String to Date Format yyyy-MM-dd
        /// </summary>
        /// <param name="str"></param>
        /// <returns>formatted string</returns>
        public static string convertToDateFormat(string str)
        {
            try
            {
                return Convert.ToDateTime(str).ToString("yyyy-MM-dd");
            }
            catch (Exception x)
            {
                return "";
            }
        }
        /// <summary>
        /// Convert String to Date Format MM/dd/yyyy
        /// </summary>
        /// <param name="str"></param>
        /// <returns>formatted string</returns>
        public static string convertToDateFormat2(string str)
        {
            try
            {
                return Convert.ToDateTime(str).ToString("MM/dd/yyyy");
            }
            catch (Exception x)
            {
                return "";
            }
        }
        /// <summary>
        /// Convert String to Decimal Format 0.00
        /// </summary>
        /// <param name="dec"></param>
        /// <returns>formatted string</returns>
        public static string convertToDecimal(string dec)
        {
            return Convert.ToDecimal(dec).ToString("#,##0.00");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Msg">Your Message</param>
        /// <param name="MsgType">Type of Message </param>
        /// <param name="Child">this</param>
        public static void toast(string Msg, string MsgType, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "sweetAlert", "toastMsg('" + Msg + "','" + MsgType + "'); ", true);
        }
        /// <summary>
        /// Format String to Decimal Format #,##0.00
        /// </summary>
        /// <param name="str"></param>
        /// <returns>formatted string</returns>
        public static string formatToDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(str.Replace(",", "")).ToString("#,##0.00");
            }
            catch (Exception x)
            {
                return "0.00";
            }
        }
        public static void alert(string Msg, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "alert('" + Msg + "'); ", true);
        }
        public static void alert2(string Msg, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "alert(`" + Msg + "`); ", true);
        }

        public static void ShowToastr(Page page, string message, string type)
        {
            string script = $"toastr.{type}('{message}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "toastr", script, true);
        }
        public static void CallLoadScreen(Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "$('#waitscreen').modal({ backdrop: 'static' });", true);

        }
        public static void CloseLoadScreen(string modal_id, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "$('#" + modal_id + "').modal({ backdrop: 'static' });", true);

        }

        public static string GetUniqueToken(int length, string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890")
        {
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];

                // If chars.Length isn't a power of 2 then there is a bias if we simply use the modulus operator. The first characters of chars will be more probable than the last ones.
                // buffer used if we encounter an unusable random byte. We will regenerate it in this buffer
                byte[] buffer = null;

                // Maximum random number that can be used without introducing a bias
                int maxRandom = byte.MaxValue - ((byte.MaxValue + 1) % chars.Length);

                crypto.GetBytes(data);

                char[] result = new char[length];

                for (int i = 0; i < length; i++)
                {
                    byte value = data[i];

                    while (value > maxRandom)
                    {
                        if (buffer == null)
                        {
                            buffer = new byte[1];
                        }

                        crypto.GetBytes(buffer);
                        value = buffer[0];
                    }

                    result[i] = chars[value % chars.Length];
                }

                return new string(result);
            }
        }
    }

    public static class ToastType
    {
        public const string success = "success";
        public const string error = "error";
        public const string info = "info";
        public const string warning = "warning";
    }
}