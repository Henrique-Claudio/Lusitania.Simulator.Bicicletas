using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class Notes : UserControl
    {
        #region Properties

        public string NotesText
        {
            get { return mNotesBox.Text; }
            set { mNotesBox.Text = value; }
        }

        #endregion

        #region >>> Helper Methods <<<

        public void GetData(ref Proposta proposta)
        {
            proposta.OBSERVACOES = mNotesBox.Text;
        }

        #endregion
    }
}