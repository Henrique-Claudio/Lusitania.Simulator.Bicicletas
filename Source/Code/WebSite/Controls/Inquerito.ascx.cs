using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer;
using System.Configuration;


namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class Inquerito : System.Web.UI.UserControl
    {
        #region >>>> Properties <<<<

        public bool ExistDebitoPorFalta { get { return mFaltaPagamento.SelectedValue == "S"; } }

        public string NrSinistros { get; set; }


        #endregion

        #region >>>> Page events <<<<
        
        protected void Page_Load(object sender, EventArgs e)
        {
            EmailPanel.Visible = true;
        }

        #endregion

        #region >>> Validação <<<

        public void validar(ref string msgErro)
        {

            if (string.IsNullOrEmpty(mFaltaPagamento.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Seleccione se tem débitos por falta.");
            }

            if (string.IsNullOrEmpty(mNrSinistros.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Seleccione Quanto sinistros teve nas últimas 2 anuidades.");
            }



        }

        public void ValidaEmailBox(object sender, EventArgs e)
        {
            bool flgErrorEmail = false;
            string email = mEmailEnvio.Text;

            if (email.IndexOf("@") > 0)
            {
                if (email.IndexOf(".") > 0)
                {
                    if (email.IndexOf("/") > 0)
                    {
                        flgErrorEmail = true;
                    }
                }
                else
                {
                    flgErrorEmail = true;
                }
            }
            else
            {
                flgErrorEmail = true;
            }

            if (flgErrorEmail)
            {
                string msg_erro;

                msg_erro = "O email introduzido é inválido.";
                ScriptHelper.ShowMessageBox(msg_erro, "Proposta Bicicletas", ScriptHelper.AlertType.ALERT);
               
                mEmailEnvio.Text = string.Empty;
            }
            else
            {
                mEmailEnvio.Text = email;
            }

        }

        public void UpdateEmailEnvio(string strpCodigo)
        {

            string email = "";
            ProposalUserDetails mProposalUserDetails = (ProposalUserDetails)this.Parent.FindControl("mUserDetails");

            if (strpCodigo != null)
            {
                try
                {
                    email = BicicletasInterface.GetEmailApolice(strpCodigo);
                }
                catch (Exception ex)
                {
                    email = "";
                }
            }

            if (email == "" || email == "null")
            {
                if (mProposalUserDetails != null)
                {
                if (mProposalUserDetails.Email != null)
                    email = mProposalUserDetails.Email;
                }
            }

            if (mEmailEnvio.Text.Length == 0)
            {
                mEmailEnvio.Text = email;
            }
        }
        #endregion

        #region >>> Helper Methods <<<

        public void GetData(ref Proposta proposta)
        {
            proposta.DEBITO_FALTA_PAG = mFaltaPagamento.SelectedValue;
            proposta.QTD_SINISTROS = mNrSinistros.SelectedValue;
            proposta.DOCS_POR_EMAIL = mReceberEmailBox.Checked ? "1" : "0";
            proposta.EMAIL_PARA_DOCS = mEmailEnvio.Text;
        }
        
        #endregion

    }
}