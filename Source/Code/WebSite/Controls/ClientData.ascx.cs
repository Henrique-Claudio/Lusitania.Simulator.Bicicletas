using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Utilities;
using Lusitania.Simuladores.DataLayer.Simulation;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class ClientData : System.Web.UI.UserControl
    {
        private const string SIMULACAO = "Simulação Bicicletas";

        #region Properties

        public Tomador Tomador
        {
            get
            {
                return new Tomador()
                {
                    Nome = frmTomador_nome.Text,
                    NIF = frmTomador_nrContrib.Text,
                    Telefon = frmTomador_telefone.Text,
                    Email = frmTomador_email.Text
                };
            }

        }

        public DateTime? PessoaSegura_DataNascimento
        {
            get
            {
                return TryConvert.ToDateTime(frmPessoaSegura_dataNasc.Text);
            }
        }

        public string NIF {
            get {
               return frmTomador_nrContrib.Text;
            }
        }

        public decimal? GenericPlanID
        {
            get { return ViewState["GenericPlanID"] as decimal?; }
            set { ViewState["GenericPlanID"] = value; }
        }


        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GenericPlanID.HasValue)
                {
                    LoadQuotationDetailsFromPlan();
                }
            }
        }

        #endregion

        #region Events

        protected void frmDataNascPessoaSegura_OnChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(frmPessoaSegura_dataNasc.Text))
            {
                DateTime? dt = TryConvert.ToDateTime(frmPessoaSegura_dataNasc.Text);
                if (dt.HasValue)
                {
                    BlockProduct(BusinessHelper.getAge(dt.Value) > 65);
                }
                else
                {
                    ScriptHelper.ShowMessageBox("Data Nascimento da pessoa segura é inválida", SIMULACAO, ScriptHelper.AlertType.ALERT);
                    frmPessoaSegura_dataNasc.Text = string.Empty;
                }

            }

            ResetSimulation();
        }

        protected void frm_OnChangedText(object sender, EventArgs e)
        {
            ResetSimulation();
        }

        protected void frmEmail_OnChangedText(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(frmTomador_email.Text) && (!ClEmail.emailValido(frmTomador_email.Text)))
            {                
                ScriptHelper.ShowMessageBox("O Email é inválido", SIMULACAO, ScriptHelper.AlertType.ERROR);
            }

            ResetSimulation();
        }

        protected void frmContrib_OnChangedText(object sender, EventArgs e)
        {
            if (!BusinessHelper.IsValidNif(frmTomador_nrContrib.Text))
            {
                ScriptHelper.ShowMessageBox("O Número de Identificação Fiscal é inválido", SIMULACAO, ScriptHelper.AlertType.ERROR);
            }
        }

        #endregion

        #region Helper Mehods

        private void ResetSimulation()
        {
            this.Page.GetType().InvokeMember("ResetSimulation", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
        }

        public void LoadSimulacao(Simulacao sim)
        {

            frmPessoaSegura_dataNasc.Text = sim.Contrato.PessoaSeguraDataNasc;
            DateTime? dt = TryConvert.ToDateTime(frmPessoaSegura_dataNasc.Text);
            if (dt.HasValue)
            {
                BlockProduct(BusinessHelper.getAge(dt.Value) > 65);
            }
            frmTomador_nome.Text = sim.Contrato.Tomador.Nome;
            frmTomador_telefone.Text = sim.Contrato.Tomador.Telefon;
            frmTomador_nrContrib.Text = sim.Contrato.Tomador.NIF;

        }

        private void BlockProduct(bool isBlock)
        {
            this.Page.GetType().InvokeMember("BlockProduct", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { isBlock });
        }

        private void LoadQuotationDetailsFromPlan()
        {
            Tomador _tomador = (new SimulacaoDS()).Saber_PessoaProv(GenericPlanID.Value);
            if (_tomador != null)
            {
                 frmTomador_nome.Text  = _tomador.Nome;
                 frmTomador_nrContrib.Text =_tomador.NIF;
                 frmTomador_telefone.Text = _tomador.Telemovel;
                 frmTomador_email.Text = _tomador.Email;
            }

            frmTomador_nome.Enabled = 
            frmTomador_nrContrib.Enabled = 
            frmTomador_telefone.Enabled = 
            frmTomador_email.Enabled = false;
        }


        #endregion

        #region Validation

        public void ValidarDados(ref string msgErro)
        {
            string _msgErro = string.Empty;

            //Data Nascimento da pessoa segura vazia
            if (string.IsNullOrEmpty(frmPessoaSegura_dataNasc.Text))
            {
                BusinessHelper.AddErroAtList(ref _msgErro,"Data Nascimento da pessoa segura deve ser preenchida.");
            }

            //data inicio é inválida
            DateTime? dt = TryConvert.ToDateTime(frmPessoaSegura_dataNasc.Text);
            if (string.IsNullOrEmpty(_msgErro) && !dt.HasValue)
            {
                BusinessHelper.AddErroAtList(ref _msgErro ,"Data Nascimento da pessoa segura é inválida.");
            }

            if (string.IsNullOrEmpty(_msgErro) && dt.Value > DateTime.Now)
            {
                BusinessHelper.AddErroAtList(ref _msgErro , "Data Nascimento da pessoa segura é inválida.");
            }

            // NIF
            if (string.IsNullOrEmpty(frmTomador_nrContrib.Text) || !BusinessHelper.IsValidNif(frmTomador_nrContrib.Text))
            {
                BusinessHelper.AddErroAtList(ref _msgErro, "O Número de Identificação Fiscal é inválido.");
            }

            // E-mail
            if (!string.IsNullOrEmpty(frmTomador_email.Text) && (!ClEmail.emailValido(frmTomador_email.Text)))
            {
                BusinessHelper.AddErroAtList(ref _msgErro, "O Email é inválido.");
            }

            msgErro += (!string.IsNullOrEmpty(msgErro) ? "<br />" : "") + _msgErro;

        }

        #endregion
    }
}
