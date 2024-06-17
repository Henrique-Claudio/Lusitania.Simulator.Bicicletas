using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    [NonVisualControl]
    [DefaultProperty("Value")]
    [ControlValueProperty("Value")]
    public class PagePropertyField : Control
    {
        [DefaultValue(null)]
        public string PagePropertyName
        {
            get { return ViewState["PagePropertyName"] as string; }
            set { ViewState["PagePropertyName"] = value; }
        }

        [Bindable(true)]
        public object Value
        {
            get
            {
                return DataBinder.GetPropertyValue(Page, PagePropertyName);
            }
        }
    }
}