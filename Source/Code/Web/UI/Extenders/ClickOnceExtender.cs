using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
    [Designer(typeof(ClickOnceDesigner))]
    [ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior", "Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class ClickOnceExtender : ExtenderControlBase
    {
    }
}
