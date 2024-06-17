using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class MyUserControl : UserControl
{
    protected T GetFromViewState<T>(string key, T _default)
    {
        T xValue = (T)ViewState[key];
        if (xValue == null)
        {
            return _default;
        }
        else
        {
            return xValue;
        }
    }
}