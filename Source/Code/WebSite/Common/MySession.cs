using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

/// <summary>
/// Facilitates access to session state objects.
/// </summary>
public static class MySession
{
    #region Properties

    private static HttpSessionState State
    {
        get { return HttpContext.Current.Session; }
    }

    public static Exception LastException
    {
        get { return State["LastException"] as Exception; }
        set { State["LastException"] = value; }
    }

    #endregion
}
