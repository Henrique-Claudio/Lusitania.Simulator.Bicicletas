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
using System.Threading;
using Resources;

namespace Lusitania.Simuladores.WebSite.Pages
{
    public partial class Simulators : System.Web.UI.MasterPage
    {
        protected override void OnPreRender(EventArgs e)
        {
            UpdateMenu();

            base.OnPreRender(e);
        }

        private void UpdateMenu()
        {
            //mMainMenu.Visible = true;
            mDebugLoggedUserControlPanel.Visible = Settings.DEV || Settings.QUAL;

            int EscondeHeader;

            try
            {
                EscondeHeader = int.Parse(Request.Params.Get("EscondeHeader"));
            }
            catch (Exception)
            {
                EscondeHeader = 0;
            }

            if (EscondeHeader == 1)
            {
                mMenuContent.Visible = false;
                divBR.Visible = false;
                //mMainMenu.Visible = false;
                //mDebugLoggedUserControlPanel.Visible = false;
            }

            /*if (!IsPostBack)
            {
                mMainMenu.DataBind();
                mMainMenu.Items.Add(new MenuItem(Strings.Menu_ClearCache, "ClearCache"));
            }*/
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
                    ScriptHelper.ShowMessageBox(Strings.Menu_TheCacheWasCleared);
                    break;
            }
        }
}
}
