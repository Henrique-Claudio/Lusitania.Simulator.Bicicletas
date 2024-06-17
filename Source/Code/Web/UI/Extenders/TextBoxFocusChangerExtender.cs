using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
    [Designer(typeof(TextBoxFocusChangerDesigner))]
    [ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior", "Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.js")]
    [TargetControlType(typeof(TextBox))]
    public class TextBoxFocusChangerExtender : ExtenderControlBase
    {        
        [ExtenderControlProperty]
        [DefaultValue("")]
        [IDReferenceProperty(typeof(Control))]
        public string NextControlID
        {
            get
            {
                return GetPropertyValue("NextControlID", "");
            }
            set
            {
                SetPropertyValue("NextControlID", value);
            }
        }

        [ExtenderControlProperty]
        [DefaultValue(0)]
        public int MaxLength
        {
            get
            {
                return GetPropertyValue("MaxLength", 0);
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    SetPropertyValue("MaxLength", value);
                }
            }
        }
    }
}
