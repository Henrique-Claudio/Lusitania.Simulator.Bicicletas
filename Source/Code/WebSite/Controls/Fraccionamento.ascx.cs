using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simuladores.WebSite.Common;
using System.Configuration;
using Lusitania.Simuladores.DataLayer;
using Resources;

using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.WebSite.WsPlanos;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class Fraccionamento : System.Web.UI.UserControl
    {
        #region >>> Properties <<<

        protected Pagamento PaymentMethod
        {
            get { return (ViewState["PaymentMethod"] != null) ? (Pagamento)ViewState["PaymentMethod"] : null; }
            set { ViewState["PaymentMethod"] = value; }
        }


        public decimal? GenericPlanID
        {
            get { return ViewState["GenericPlanID"] as decimal?; }
            set { ViewState["GenericPlanID"] = value; }
        }

        public bool HasSDD
        {
            get
            {
                return ViewState["HasSDD"] != null ? (bool)ViewState["HasSDD"] : false;                
            }
            set
            {
                ViewState["HasSDD"] = value;
            }

        }

        public string SimulationProduct
        {
            get
            {
                return ViewState["SimulationProduct"] != null ? (string)ViewState["SimulationProduct"]: "";                
            }
            set
            {
                ViewState["SimulationProduct"] = value;
            }

        }
                
        public string pNIB {
            get {
                return mIBAN.Text;
            }
            set {
                mIBAN.Text = value;
            }
        }

        public string pBIC
        {
            get
            {
                return mBIC.Text;
            }
            set
            {
                mBIC.Text = value;
            }
        }

        #endregion

        #region >>> Page Events <<<
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UtilityG.BindList(mFraccionamentos, this.SimulationProduct , UtilityG.GetValue(GenericLists.FRACCIONAMENTO), false);

                
                mIBAN.Enabled =
                mBIC.Enabled = !GenericPlanID.HasValue;

                //Determine the payment method                
                LoadPaymentMethod();

                if (PaymentMethod != null && PaymentMethod.MetodoPagamento.Equals("SDD"))
                {
                    HasSDD = true;
                    pNIB = PaymentMethod.NIB;
                    pBIC = PaymentMethod.BIC;
                }

                //mFraccionamentos.SelectedValue = "1";
                mFraccionamentos.Enabled = true;
                conIBAN_BIC.Visible = HasSDD;

                
            }
        }
        #endregion

        #region >>> Elements Events <<<

        public void ValidaIBAN_ObtemBIC(object sender, EventArgs e)
        {
            decimal? valido = 1;
            string bic_aux = mBIC.Text;
            WsHelper.ValidaIBAN_ObtemBIC(mIBAN.Text, ref bic_aux, ref valido
                , ConfigurationManager.AppSettings["NetworkCredential_user"],
                ConfigurationManager.AppSettings["NetworkCredential_pass"]);

            if (valido != 1 && mIBAN.Text.Length > 3 && mIBAN.Text.ToUpper().Substring(0, 4).Equals("PT50"))
            {
                string msg_erro;
                if (valido == 2)
                {
                    msg_erro = "O IBAN introduzido é inválido, banco não aderente ao sistema de débitos directos.";
                }
                else
                {
                    msg_erro = "O IBAN introduzido é inválido.";
                }
                ScriptHelper.ShowMessageBox(msg_erro, "Fraccionamento", ScriptHelper.AlertType.ERROR);
            }
            else
            {
                mBIC.Text = bic_aux;
            }

        }

        protected void mSearchBIC_SWC_Click(object sender, ImageClickEventArgs e)
        {  
            mBancSearchPanel.Visible = true;
            mBancSearchPopup.Show();
        }
        
        public void ObterBIC(object sender, EventArgs e)
        {
            mBIC.Text = GlobalInterface.OBTEM_BIC(mIBAN.Text);
            if (string.IsNullOrEmpty(mBIC.Text))
            {
                ScriptHelper.ShowMessageBox(Strings.ErroPesquisaBIC, "Fraccionamento", ScriptHelper.AlertType.ERROR);
            }
        }

        protected void mBancSearch_BancChosen(object sender, EventArgs e)
        {
            mBIC.Text = GlobalInterface.saber_Bancos_ROWID(mBancSearch.SelectedRowID);
            mBancSearchPanel.Visible = false;
            mBancSearchPopup.Hide();
        }

        protected void mBancSearch_BancLocalSearch(object sender, EventArgs e)
        {
            mBancSearchPopup.Show();
        }
        
        #endregion

        #region >>> Helper Medhods <<<

        private void LoadPaymentMethod()
        {
            //Determine the payment method                
            PaymentMethod = WsHelper.GetPlanPaymentMethod(GenericPlanID,
                ConfigurationManager.AppSettings["NetworkCredential_user"],
                ConfigurationManager.AppSettings["NetworkCredential_pass"]);
        }

        public void GetData(ref Proposta proposta)
        {
            proposta.FRACCIONAMENTO = mFraccionamentos.SelectedValue;
            proposta.IBAN = mIBAN.Text;
            proposta.BIC = mBIC.Text;
        }

        #endregion

        #region >>> Validação <<<

        public void validar(ref string msgErro) {
            
            if (string.IsNullOrEmpty(mFraccionamentos.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Indique o fracionamento do seguro pretendido.");
            }

            if (HasSDD)
            {
                decimal? result = Functions.Validar_IBAN_BIC(mIBAN.Text, mBIC.Text);

                if (!result.HasValue || (result.HasValue && result.Value == 0))
                {
                    BusinessHelper.AddErroAtList(ref msgErro, "O IBAN é inválido");
                }
                
                if (result.HasValue && result.Value == 2)
                {
                    BusinessHelper.AddErroAtList(ref msgErro, "Banco não aderente ao sistema de Débitos Directos");
                }
            }

        }
        
        #endregion
    }
}