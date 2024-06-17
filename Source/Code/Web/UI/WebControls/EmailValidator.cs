using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class EmailValidator : RegularExpressionValidator
    {
        private const string cExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public EmailValidator()
        {
            ValidationExpression = cExpression;
        }
    }
}
