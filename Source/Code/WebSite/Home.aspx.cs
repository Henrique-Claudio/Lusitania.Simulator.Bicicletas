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
using System.Xml;
using System.Xml.XPath;

namespace Lusitania.Simuladores.WebSite
{
    public partial class Home : System.Web.UI.Page
    {

        protected override void OnPreRender(EventArgs e)
        {       
            //mMainMenu.DataBind();
            hDebugLoggedUserControlPanel.Visible = Settings.DEV || Settings.QUAL;
            //mMainMenu.Visible = true;
            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void mMainMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "ClearCache":
                    IDictionaryEnumerator xCacheEnumerator = Cache.GetEnumerator();
                    while (xCacheEnumerator.MoveNext())
                    {
                        Cache.Remove(xCacheEnumerator.Key.ToString());
                    }
                    //ScriptHelper.ShowMessageBox(Strings.Menu_TheCacheWasCleared);
                    break;
            }
        }
    }
}
