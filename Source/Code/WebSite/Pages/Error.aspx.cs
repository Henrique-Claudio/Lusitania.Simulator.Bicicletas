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

public partial class Pages_Error : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {
        mErrorMessageLabel.Text = MySession.LastException != null ? MySession.LastException.Message : String.Empty;

        base.OnLoad(e);
    }
}