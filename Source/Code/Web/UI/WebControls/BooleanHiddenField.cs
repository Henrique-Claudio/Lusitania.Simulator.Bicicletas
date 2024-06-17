using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class BooleanHiddenField : HiddenField
    {
        public new bool? Value
        {
            get
            {
                if (String.IsNullOrEmpty(base.Value))
                {
                    return null;
                }
                else
                {
                    return Boolean.Parse(base.Value);
                }
            }
            set
            {
                base.Value = value.ToString();
            }
        }
    }
}
