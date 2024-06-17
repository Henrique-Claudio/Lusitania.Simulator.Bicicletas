using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        protected override void PerformDataBinding(IEnumerable dataSource)
        {
            try
            {
                base.PerformDataBinding(dataSource);
            }
            catch (ArgumentOutOfRangeException)
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
                catch(Exception)
                {
                }
            }
        }
    }
}
