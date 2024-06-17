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
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Simulation;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class CreatedBy : MyUserControl
    {
        private SimulacaoDS _db = new SimulacaoDS();

        #region Properties



        public string UserName
        {
            get { return UserHelper.UserName; }
        }

        public Elaborador Elaborador
        {

            get { return ViewState["Elaborador"] != null ? (Elaborador)ViewState["Elaborador"] : new Elaborador(); }
            set { ViewState["Elaborador"] = value; }
        }

        #endregion

        #region Page LiveCycle

        protected override void OnLoad(EventArgs e)
        {

            //bind
            if (!IsPostBack)
            {
                this.Elaborador = _db.GetElaborador(UserHelper.UserName);
                mEMailBox.Text = this.Elaborador.Email;
                mNameBox.Text = this.Elaborador.Nome;
                mPhoneNumberBox.Text = !DataHelper.IsInternUser(UserHelper.UserName) ? this.Elaborador.Telefon : string.Empty;
                mPhoneNumberBox.Enabled = !DataHelper.IsInternUser(UserHelper.UserName);
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            Visible = !String.IsNullOrEmpty(UserHelper.UserName);

            base.OnPreRender(e);
        }

        #endregion


        #region Methods

        #endregion
    }
}