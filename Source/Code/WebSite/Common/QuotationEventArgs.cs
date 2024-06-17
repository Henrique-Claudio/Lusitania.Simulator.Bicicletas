using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Lusitania.Simuladores.WebSite.Common
{
    public class QuotationEventArgs : EventArgs
    {
        private string _quotationId;

        public string QuotationId
        {
            get { return _quotationId; }
            set { _quotationId = value; }
        }

        public QuotationEventArgs(string quotationId)
        {
            this._quotationId = quotationId;
        }
    }
}
