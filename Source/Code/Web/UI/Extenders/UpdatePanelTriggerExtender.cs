using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
    [Designer(typeof(UpdatePanelTriggerDesigner))]
    [ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior", "Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.js")]
    [TargetControlType(typeof(Control))]
    public class UpdatePanelTriggerExtender : ExtenderControlBase
    {
        // TODO: Add your property accessors here.
        //
        [ExtenderControlProperty]
        [DefaultValue("")]
        public string UpdatePanelControlID
        {
            get
            {
                return GetPropertyValue("UpdatePanelControlID", "");
            }
            set
            {
                SetPropertyValue("UpdatePanelControlID", value);
            }
        }

        [ExtenderControlProperty]
        [DefaultValue("")]
        protected string UpdatePanelControlClientID
        {
            get
            {
                return GetPropertyValue("UpdatePanelControlClientID", "");
            }
            set
            {
                SetPropertyValue("UpdatePanelControlClientID", value);
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Control xUpdatePanel = FindControl(UpdatePanelControlID);
            if (xUpdatePanel != null)
            {
                UpdatePanelControlID = xUpdatePanel.ClientID;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
    }
}
