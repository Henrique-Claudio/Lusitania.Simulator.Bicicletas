using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;
using Lusitania.Simuladores.DataLayer;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    [ToolboxData("<{0}:NifValidator runat=server></{0}:NifValidator>")]
    public class NifValidator : BaseValidator
    {
        protected override bool EvaluateIsValid()
        {
            return Functions.IsNifValid(GetControlValidationValue(ControlToValidate));
        }
    }
}
