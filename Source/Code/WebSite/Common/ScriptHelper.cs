using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Provides helper methods for working with scripts.
/// </summary>
public static class ScriptHelper
{
    /// <summary>
    /// Places javascript on the page
    /// Warning: The javascript will only run after page preRender, PostBack maybe required
    /// </summary>        
    /// <param name="javascriptCode">String containing Javascript ex: "alert('TESTE')"</param>
    /// /// <param name="iPage">The Page on which to run the javascript</param>
    public static void RunClientJavascript(string javascriptCode)
    {
        Page iPage = (Page)(HttpContext.Current.Handler);
        // remove '
        javascriptCode = javascriptCode.Replace(@"'", String.Empty);

        // remove ""
        javascriptCode = javascriptCode.Replace("\"", String.Empty);

        // remove carriage returns
        javascriptCode = javascriptCode.Replace("\r", @"\r");

        // remove line feed
        javascriptCode = javascriptCode.Replace("\n", @"<br />");

        // register the message
        ScriptManager.RegisterStartupScript(
            iPage, iPage.GetType(),
            Guid.NewGuid().ToString(),
            @"
                Sys.Application.add_load(RunOnce);
                function RunOnce() {
                  // This will only happen once per GET request to the page.
                  ___SCRIPT___  
                  Sys.Application.remove_load(RunOnce);
                }"
                .Replace("___SCRIPT___", javascriptCode),
            true
        );
    }

    public static void RegisterScriptBlock(string script)
    {
        // register the script
        ScriptManager.RegisterStartupScript(
            (Page)(HttpContext.Current.Handler), ((Page)(HttpContext.Current.Handler)).GetType(),
            Guid.NewGuid().ToString(),
            @"
                Sys.Application.add_load(RunOnceScript);
                function RunOnceScript() {
                  // This will only happen once per GET request to the page.
                  __SCRIPT__  
                  Sys.Application.remove_load(RunOnceScript);
                }".Replace("__SCRIPT__", script),
            true
        );
    }

    public enum AlertType
    {
        INFO = 1,
        ERROR = 2,
        ALERT = 3
    }

    /// <summary>
    /// Shows an Alert on Page at the next Async Rendering.
    /// </summary>
    /// <param name="iMessage">The Message to be shown.</param>
    /// <param name="iPage">The Page on which to show the Message. This is needed by the ScriptManager.</param>
    public static void ShowMessageBox(string iMessage, Page iPage)
    {
        // remove '
        iMessage = iMessage.Replace(@"'", String.Empty);

        // remove ""
        iMessage = iMessage.Replace("\"", String.Empty);

        // remove carriage returns
        iMessage = iMessage.Replace("\r", @"\r");

        // remove line feed
        iMessage = iMessage.Replace("\n", @"\n");

        // register the message
        ScriptManager.RegisterStartupScript(
            iPage, iPage.GetType(),
            Guid.NewGuid().ToString(),
            @"setTimeout(function() { alert(""___MESSAGE___""); }, 0);"
                .Replace("___MESSAGE___", iMessage),
            true
        );
    }
    public static void ShowMessageBox(string iMessage, string iTitle, AlertType type, Page iPage)
    {
        // remove '
        iMessage = iMessage.Replace(@"'", String.Empty);

        // remove ""
        iMessage = iMessage.Replace("\"", String.Empty);

        // remove carriage returns
        iMessage = iMessage.Replace("\r", @"\r");

        // remove line feed
        iMessage = iMessage.Replace("\n", @"<br />");

        // register the message
        ScriptManager.RegisterStartupScript(
            iPage, iPage.GetType(),
            Guid.NewGuid().ToString(),
            @"
                Sys.Application.add_load(RunOnce);
                function RunOnce() {
                  // This will only happen once per GET request to the page.
                  $.AlertBox(""___MESSAGE___"", ""__TITLE__"", ""__TIPOALERT__"");  
                  Sys.Application.remove_load(RunOnce);
                }"
                .Replace("___MESSAGE___", iMessage).Replace("__TITLE__", iTitle).Replace("__TIPOALERT__", Convert.ToInt32(type).ToString()),
            true
        );
    }

    public static void ShowMessageBox(string iMessage)
    {
        ShowMessageBox(iMessage, (Page)(HttpContext.Current.Handler));
    }
    public static void ShowMessageBox(string iMessage, string iTitle, AlertType type)
    {
        ShowMessageBox(iMessage, iTitle, type, (Page)(HttpContext.Current.Handler));
    }

    public static void ShowMessageBoxIfNotEmpty(string iMessage)
    {
        if (!String.IsNullOrEmpty(iMessage))
        {
            ShowMessageBox(iMessage);
        }
    }

    public static void ShowMessageBoxIfNotEmpty(string Message, string Title, AlertType Type)
    {
        if (!String.IsNullOrEmpty(Message))
        {
            ShowMessageBox(Message, Title, Type);
        }
    }


    /// <summary>
    /// Shows OverLayer on Page at the next Async Rendering.
    /// </summary>
    /// <param name="iMessage">The Message to be shown.</param>
    /// <param name="iPage">The Page on which to show the Message. This is needed by the ScriptManager.</param>
    public static void ShowOverLayer()
    {
        Page iPage = (Page)(HttpContext.Current.Handler);
        // register the message
        ScriptManager.RegisterStartupScript(
            iPage, iPage.GetType(),
            Guid.NewGuid().ToString(),
            @"setTimeout(function() { $.fn.LoaderClose(); }, 200);",
            true
        );
    }

    /// <summary>
    /// Opens up an url as a new browser window.
    /// </summary>
    /// <param name="iUrl">The full url to be opened.</param>
    /// <param name="iPage">The page that will trigger the popup.</param>
    public static void ShowWindow(string iUrl, Page iPage)
    {
        ScriptManager.RegisterStartupScript(
            iPage,
            iPage.GetType(),
            Guid.NewGuid().ToString(),
            String.Format("window.open('{0}');", iUrl.Replace("'", @"\'")),
            true);
    }

    public static void ShowWindow(string iUrl)
    {
        ShowWindow(iUrl, (Page)(HttpContext.Current.Handler));
    }

    public static void ShowWindow(string iRelativeTargetPath, string iQueryStringFormatString, params object[] iQueryStringArgs)
    {
        ShowWindow(WebUtility.BuildUrl(HttpContext.Current.Request, ((Page)(HttpContext.Current.Handler)).ResolveUrl(iRelativeTargetPath), String.Format(iQueryStringFormatString, iQueryStringArgs)));
    }

    public static void ShowWindowWithAlert(string alertMsg, string iRelativeTargetPath, string iQueryStringFormatString, params object[] iQueryStringArgs)
    {
        ScriptManager.RegisterStartupScript(
            (Page)(HttpContext.Current.Handler),
            ((Page)(HttpContext.Current.Handler)).GetType(),
            Guid.NewGuid().ToString(),
            String.Format("alert('{0}'); window.open('{1}');", alertMsg,
            WebUtility.BuildUrl(HttpContext.Current.Request, ((Page)(HttpContext.Current.Handler)).ResolveUrl(iRelativeTargetPath), String.Format(iQueryStringFormatString, iQueryStringArgs)).Replace("'", @"\'")),
            true);
    }

    public static void RegisterFormatNumberOnChange(WebControl target, string format)
    {
        ScriptManager.RegisterStartupScript(
            target.Page,
            target.Page.GetType(),
            Guid.NewGuid().ToString(),
            "if($get('__CLIENTID__')) $addHandler($get('__CLIENTID__'), 'change', function() { this.value = String.localeFormat('{0:N2}', Number.parseLocale(this.value)); });"
                .Replace("__CLIENTID__", target.ClientID),
            true
        );
    }

    public static void RegisterClientRedirect(string url, Page page)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "window.location='" + url + "';", true);
    }

    public static void RegisterClientRedirect(string url)
    {
        RegisterClientRedirect(url, (Page)(HttpContext.Current.Handler));
    }

    public static void Focus(Control iControl)
    {
        if (iControl == null) throw new ArgumentNullException("iControl");

        ScriptManager.RegisterStartupScript(
            iControl,
            iControl.GetType(),
            "Focus",
            @"setTimeout(""try { $get('___ID___').focus(); }  catch (ex) {}"", 1);"
                .Replace("___ID___", iControl.ClientID),
            true
        );
    }
}
