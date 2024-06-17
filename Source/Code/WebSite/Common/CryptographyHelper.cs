using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Lusitania.Simuladores.WebSite.Common
{
    public static class CryptographyHelper
    {
        public static string EncryptToBase64(string iInput, string iPassword)
        {
            // define the vector and the key
            byte[] xKey = Encoding.ASCII.GetBytes(iPassword.PadRight(16, '0'));

            // create the encryptor
            ICryptoTransform xEncryptor = TripleDES.Create().CreateEncryptor(xKey, xKey);

            // create the target stream
            MemoryStream xMemoryStream = new MemoryStream();

            // create the encryptor stream
            CryptoStream xCryptoStream = new CryptoStream(xMemoryStream, xEncryptor, CryptoStreamMode.Write);

            // get the clear string as a buffer
            byte[] xInputArray = Encoding.Unicode.GetBytes(iInput);

            // encrypt the whole query string
            xCryptoStream.Write(xInputArray, 0, xInputArray.Length);

            // finish the block
            xCryptoStream.FlushFinalBlock();

            // done
            return Convert.ToBase64String(xMemoryStream.ToArray());
        }

        public static string DecryptFromBase64(string iInput, string iPassword)
        {
            // define the vector and the key
            byte[] xKey = Encoding.ASCII.GetBytes(iPassword.PadRight(16, '0'));

            // create the decryptor stream
            return (new StreamReader(
                new CryptoStream(
                    new MemoryStream(Convert.FromBase64String(iInput)),
                    TripleDES.Create().CreateDecryptor(xKey, xKey),
                    CryptoStreamMode.Read),
                    Encoding.Unicode)).ReadToEnd();
        }

        public static Uri EncryptQueryString(Uri iUrl, string iNewParamaterName, string iPassword)
        {
            UriBuilder xUrl = new UriBuilder(iUrl);

            xUrl.Query = String.Format("{0}={1}", iNewParamaterName, HttpUtility.UrlEncode(EncryptToBase64(xUrl.Query, iPassword)));

            return xUrl.Uri;
        }

        public static Uri DecryptQueryString(Uri iUrl, string iEncryptedParamaterName, string iPassword)
        {
            UriBuilder xUrl = new UriBuilder(iUrl);

            string[] xParams = xUrl.Query.TrimStart('?').Split('&');
            foreach (string xParam in xParams)
            {
                string[] xParamPair = xParam.Split('=');
                string xParamName = xParamPair[0];
                if (xParamName == iEncryptedParamaterName)
                {
                    string xParamValue = HttpUtility.UrlDecode(xParamPair[1]);

                    xUrl.Query = DecryptFromBase64(xParamValue, iPassword);
                }
            }

            return xUrl.Uri;
        }
    }
}