using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Lusitania.Simuladores.WebSite.Common
{
    public static class ControlHelper
    {
        public static void UpdateRows(GridView target)
        {
            for (int xRowIndex = 0; xRowIndex < target.Rows.Count; ++xRowIndex)
            {
                target.UpdateRow(xRowIndex, false);
            }
        }
    }
}
