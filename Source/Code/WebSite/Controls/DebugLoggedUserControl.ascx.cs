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
using Lusitania.Simuladores.WebSite;

public partial class Controls_DebugLoggedUserControl : System.Web.UI.UserControl
{
    protected void mOKButton_Click(object sender, EventArgs e)
    {
        if (mIsLoggedOnRadioButton.Checked)
        {
            UserHelper.DebugUserName = mUserNameBox.Text;
        }
        else
        {
            UserHelper.DebugUserName = String.Empty;
        }

        Response.Redirect(Request.Url.ToString());
    }

    protected override void OnPreRender(EventArgs e)
    {
        ShowLoggedOnUser();

        base.OnPreRender(e);
    }

    private void ShowLoggedOnUser()
    {
        mCurrentUserLabel.Text = UserHelper.UserName;
    }
}
