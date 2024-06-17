using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
    [Designer(typeof(NumberFormatDesigner))]
    [ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior", "Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class NumberFormatExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [DefaultValue("")]
        public string FormatString
        {
            get
            {
                return GetPropertyValue("FormatString", "");
            }
            set
            {
                SetPropertyValue("FormatString", value);
            }
        }
    }
}
