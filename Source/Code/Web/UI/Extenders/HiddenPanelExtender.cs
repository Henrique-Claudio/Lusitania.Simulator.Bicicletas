using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
    [Designer(typeof(HiddenPanelDesigner))]
    [ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior", "Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class HiddenPanelExtender : ExtenderControlBase
    {
        [ExtenderControlProperty]
        [DefaultValue("")]
        [IDReferenceProperty]
        public string SelectableControlID
        {
            get
            {
                return GetPropertyValue("SelectableControlID", "");
            }
            set
            {
                SetPropertyValue("SelectableControlID", value);
            }
        }
    }
}
