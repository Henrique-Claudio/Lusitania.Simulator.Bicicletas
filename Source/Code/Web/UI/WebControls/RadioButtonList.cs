using System;
using System.Collections.Generic;
using System.Text;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList
    {
        protected override void PerformDataBinding(System.Collections.IEnumerable dataSource)
        {
            try
            {
                base.PerformDataBinding(dataSource);
            }
            catch
            {
                
            }
        }

        public override string SelectedValue
        {
            get
            {
                return base.SelectedValue;
            }
            set
            {
                try
                {
                    base.SelectedValue = value;
                }
                catch
                {
                }
            }
        }
    }
}
