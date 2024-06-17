using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    [NonVisualControl]
    [DefaultProperty("Value")]
    [ControlValueProperty("Value")]
    public class DynamicValueField : WebControl
    {
        public event EventHandler<DynamicValueFieldValueNeededEventArgs> ValueNeeded;
        protected virtual void OnValueNeeded(out object value)
        {
            if (ValueNeeded != null)
            {
                DynamicValueFieldValueNeededEventArgs args = new DynamicValueFieldValueNeededEventArgs();
                ValueNeeded(this, args);
                value = args.Value;
            }
            else
            {
                value = null;
            }
        }

        [Bindable(true)]
        public object Value
        {
            get
            {
                object value;
                OnValueNeeded(out value);
                return value;
            }
        }
    }

    public class DynamicValueFieldValueNeededEventArgs : EventArgs
    {
        private object mValue;
        public object Value
        {
            get { return mValue; }
            set { mValue = value; }
        }
    }
}
