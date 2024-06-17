using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Simuladores.WebSite.Common;
using System.Text;

namespace Lusitania.Simuladores.WebSite.Tests
{
    public partial class EncryptionTest : System.Web.UI.Page
    {
        protected void mEncryptButton_Click(object sender, EventArgs e)
        {
            mEncryptedTextBox.Text = CryptographyHelper.EncryptToBase64(mClearTextBox.Text, mPasswordBox.Text);
        }

        protected void mDecryptButton_Click(object sender, EventArgs e)
        {
            mDecryptedTextBox.Text = CryptographyHelper.DecryptFromBase64(mEncryptedTextBox.Text, mPasswordBox.Text);
        }
    }
}
