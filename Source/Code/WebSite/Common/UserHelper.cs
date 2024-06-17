using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;

using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.Common;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;

/// <summary>
/// Provides User Authentication helpers.
/// </summary>
public static class UserHelper
{
    #region Constants

    private const string cUserInfoKey = "{8B19D07E-7C11-462d-BEE9-FA28E0ED32EE}";

    #endregion

    /// <summary>
    /// Safely returns the current authenticated user's logon name
    /// </summary>
    public static string UserName
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session[SessionKeys.DebugUserName] != null)
                {
                    return DebugUserName;
                }
                //Se o simulador não estiver em produção faz login por defeito com o DebugUserName
                else if ((Settings.DEV || Settings.QUAL) && !string.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("DebugUserName")))
                {
                    return ConfigurationManager.AppSettings.Get("DebugUserName");
                }
                else
                {
                    return MyHelper.LoginNameOnly(HttpContext.Current.User.Identity.Name);
                }
            }
            catch
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session[SessionKeys.DebugUserName] = value;
        }

    }

    public static string DebugUserName
    {
        get
        {
            string xSessionUser = (string)HttpContext.Current.Session[SessionKeys.DebugUserName];
            if (xSessionUser == null)
            {
                HttpContext.Current.Session[SessionKeys.DebugUserName] = xSessionUser = Settings.DebugUserName;
            }
            return xSessionUser;
        }
        set { HttpContext.Current.Session[SessionKeys.DebugUserName] = value; }
    }

    private static GlobalDS.SABER_DADOS_AGENTERow CachedUserInfo
    {
        get
        {
            GlobalDS.SABER_DADOS_AGENTERow info = HttpContext.Current.Items[cUserInfoKey] as GlobalDS.SABER_DADOS_AGENTERow;
            if (info == null)
            {
                object dummy;
                GlobalDS.SABER_DADOS_AGENTEDataTable table = (new SABER_DADOS_AGENTETableAdapter()).GetData(UserName, out dummy);
                if (table.Count > 0)
                {
                    info = table[0];
                }
                else
                {
                    info = table.NewSABER_DADOS_AGENTERow();
                    info.EMAIL = String.Empty;
                    info.NOME = String.Empty;
                    info.TELEFONE = String.Empty;
                }
                HttpContext.Current.Items[cUserInfoKey] = info;
            }
            return info;
        }
    }

    //private static OSBConnector.Models.UA_Person CachedUserInfo
    //{
    //    get
    //    {
    //        var cachedUserAttributes = OSBConnector.Shared.DataServices.GetUserAttributes(UserName, true);
    //        return cachedUserAttributes != null ? cachedUserAttributes.Person : null;
    //    }
    //}

    /// <summary>
    /// Gets the Logged On User's EMail.
    /// </summary>
    public static string UserEMail
    {
        get
        {
            return CachedUserInfo.IsEMAILNull() ? String.Empty : CachedUserInfo.EMAIL;
            //return CachedUserInfo != null ? CachedUserInfo.Email : String.Empty;
        }
    }

    /// <summary>
    /// Gets the Logged On User's Human Name.
    /// </summary>
    public static string UserDisplayName
    {
        get
        {
            return CachedUserInfo.NOME;
            //return CachedUserInfo != null ? CachedUserInfo.FullName : String.Empty;
        }
    }
}