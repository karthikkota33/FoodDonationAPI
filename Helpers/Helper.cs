using System.Data;
using System.Reflection;
using System.Xml.Linq;

namespace FoodDonationAPI.Helpers
{
    public class Helper
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? string.Empty : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        #region String Helpers

        /// <summary>
        /// Base64 encodes a string.
        /// </summary>
        /// <param name="input">A string</param>
        /// <returns>A base64 encoded string</returns>
        public static string Base64StringEncode(string input)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(encbuff);
        }

        /// <summary>
        /// Base64 decodes a string.
        /// </summary>
        /// <param name="input">A base64 encoded string</param>
        /// <returns>A decoded string</returns>
        public static string Base64StringDecode(string input)
        {
            byte[] decbuff = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        public static string IsExistingElement(XElement element, string elementName)
        {
            string retElementValue = string.Empty;
            try
            {
                //If Elementname is not valid in element it will throw exception
                if (element != null)
                {
                    if (element.Element(elementName) != null)
                    {
                        if (element.Element(elementName).Value.ToString() != string.Empty)
                        {
                            retElementValue = element.Element(elementName).Value.ToString().Trim();
                        }
                        else
                        {
                            retElementValue = string.Empty;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retElementValue;
        }

        //Validate the GUID
        public static bool IsGuid(string candidate)
        {
            System.Text.RegularExpressions.Regex isGuid = new System.Text.RegularExpressions.Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", System.Text.RegularExpressions.RegexOptions.Compiled);
            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidInt(string strInt)
        {
            int val;
            bool isInt = true;
            try
            {
                val = Int32.Parse(strInt);
            }
            catch
            {
                isInt = false;
            }
            return isInt;
        }

        #endregion
    }
}
