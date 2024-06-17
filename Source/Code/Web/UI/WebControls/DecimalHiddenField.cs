using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class DecimalHiddenField : HiddenField
    {
        public new decimal? Value
        {
            get
            {
                if (String.IsNullOrEmpty(base.Value))
                {
                    return null;
                }
                else
                {
                    return Decimal.Parse(base.Value, NumberStyles.Number, CultureInfo.InvariantCulture);
                }
            }
            set
            {
                base.Value = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : String.Empty;
            }
        }
    }
}
