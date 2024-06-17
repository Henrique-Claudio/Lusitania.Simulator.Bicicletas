using System.ComponentModel;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    internal class CheckBoxListConverter : StringConverter
    {
        /// <summary>
        /// Checks the page context to see if standard values are supported.
        /// </summary>
        /// <param name="context">The web page's context object.</param>
        /// <returns>A boolean true/false value.</returns>
        /// <remarks></remarks>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Parses the page context and finds all CheckBoxList IDs.
        /// </summary>
        /// <param name="context">The web page's context object.</param>
        /// <returns>A StandardValuesCollection of CheckBoxList IDs.</returns>
        /// <remarks></remarks>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList list = new ArrayList();
            foreach (Control control in context.Container.Components)
            {
                if (control is CheckBoxList)
                {
                    if (control.ID.StartsWith("_ctl"))
                    {
                        list.Add(control.ID.Remove(1, control.ID.ToString().IndexOf('_', 1) + 1));
                    }
                    else
                    {
                        list.Add(control.ID);
                    }
                }
            }
            return new StandardValuesCollection(list);
        }
    }
}