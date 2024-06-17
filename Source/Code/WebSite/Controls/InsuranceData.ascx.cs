using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer;
using System.Configuration;
using Lusitania.Simuladores.WebSite.Common;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class InsuranceData : System.Web.UI.UserControl
    {
        private const string SIMULACAO = "Simulação Bicicletas";

        #region Properties
        public decimal? GenericPlanID
        {
            get { return ViewState["GenericPlanID"] as decimal?; }
            set { ViewState["GenericPlanID"] = value; }
        }
        public decimal? FamilyPlanID
        {
            get { return ViewState["FamilyPlanID"] as decimal?; }
            set { ViewState["FamilyPlanID"] = value; }
        }
        public decimal? SimulationID
        {
            get { return ViewState["SimulationID"] as decimal?; }
            set { ViewState["SimulationID"] = value; }
        }
        public DateTime? DataInicio
        {
            get { return TryConvert.ToDateTime(tbDateBegin.Text); }
            set { ViewState["Mediador"] = value; }
        }
        public string Vencimento
        {
            get { return DataInicio.HasValue ? string.Format("{0:00}{1:00}", DataInicio.Value.Month, DataInicio.Value.Day) : string.Empty; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                mDataVencimentoDayBox.Enabled=
                mDataVencimentoMonthddl.Enabled = false;

                LoadDefaultValues();
                CarregaDataVencimento();
            }

        }

        #region Events
        protected void mDataVencimentoMonthddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetSimulation();
        }
        protected void mDataVencimentoDayBox_TextChanged(object sender, EventArgs e)
        {
            ResetSimulation();
        }

        protected void tbDateBegin_OnTextChanged(object sender, EventArgs e)
        {
            ResetSimulation();

            if (!string.IsNullOrEmpty(tbDateBegin.Text))
            {
                DateTime? startDate = TryConvert.ToDateTime(tbDateBegin.Text);

                if (startDate.HasValue)
                {
                    if (GenericPlanID.HasValue)
                    {
                        CarregaDataVencimento();
                    }
                    else
                    {
                        if (startDate.Value <= DateTime.Now)
                        {
                            //tbDateBegin.Text = DateTime.Now.AddDays(1).ToShortDateString();
                            tbDateBegin.Text = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy");
                            ScriptHelper.ShowMessageBox("A data início  não pode ser anterior a data do dia +1.", SIMULACAO, ScriptHelper.AlertType.ALERT);
                        }
                        else
                        {
                            LoadDueDate(string.Format("{0:00}{1:00}", startDate.Value.Month, startDate.Value.Day));
                        }
                    }
                }
                else
                {
                    ScriptHelper.ShowMessageBox("Data Início é inválida.", SIMULACAO, ScriptHelper.AlertType.ALERT);
                    tbDateBegin.Text = string.Empty;
                }

            }


        }
        #endregion

        #region Helper Metheds
        /// <summary>
        /// Loads the day and month of the due date ("Data Vencimento")
        /// </summary>
        /// <param name="sMonthDay">2 digit month plus 2 digit day ex: 0102 (Month 1, Day 2)</param>
        private void LoadDueDate(String sMonthDay)
        {
            mDataVencimentoDayBox.Text = sMonthDay.Substring(2, 2);
            mDataVencimentoMonthddl.SelectedValue = sMonthDay.Substring(0, 2);
        }

        private void LoadDefaultValues()
        {
            //this.tbDateBegin.Text = DateTime.Today.AddDays(1).ToShortDateString();
            this.tbDateBegin.Text = DateTime.Today.AddDays(1).ToString("dd-MM-yyyy");
        }

        private void CarregaDataVencimento()
        {
            string dataVencimento, planCode;
            DateTime startDate;

            planCode = string.Empty;
            dataVencimento = string.Empty;

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            mDataVencimentoMonthddl.DataSource = WsHelper.GetMonthList(
                ConfigurationManager.AppSettings["NetworkCredential_user"],
                ConfigurationManager.AppSettings["NetworkCredential_pass"]);

            mDataVencimentoMonthddl.DataTextField = "Descritivo";
            mDataVencimentoMonthddl.DataValueField = "Codigo";
            mDataVencimentoMonthddl.DataBind();

            if (GenericPlanID.HasValue)
            {
                startDate = Convert.ToDateTime(tbDateBegin.Text);

                dataVencimento = WsHelper.GetDueDate(
                    planCode
                    , startDate
                    , ConfigurationManager.AppSettings["NetworkCredential_user"]
                    , ConfigurationManager.AppSettings["NetworkCredential_pass"]);
            }

            if ((GenericPlanID.HasValue || SimulationID.HasValue) && !string.IsNullOrEmpty(dataVencimento))
            {
                LoadDueDate(dataVencimento);
                
            }
            else
            {
                //set data vencimento diferenciado
                ResetDateField();
            }
        }

        private void ResetDateField()
        {
            DateTime data = Convert.ToDateTime(tbDateBegin.Text);

            string mes = Convert.ToString(data.Month);
            string day = Convert.ToString(data.Day);

            if (mes.Length == 1)
            {
                mes = "0" + Convert.ToString(data.Month);
            }

            if (day.Length == 1)
            {
                day = "0" + Convert.ToString(data.Day);
            }

            LoadDueDate(mes + day);
        }

        public void LoadSimulacao(Simulacao sim)
        {
            tbDateBegin.Text = string.Format("dd-MM-yyyy", sim.Contrato.DataInicio);          
        }

        #endregion


        #region Validation

        public void ValidarDados(ref string msgErro)
        {
            string _msgErro = string.Empty;

            //data inicio vazia
            if (string.IsNullOrEmpty(tbDateBegin.Text))
            {
                _msgErro += "- Data Início deve ser preenchida.";
            }

            //data inicio é inválida
            DateTime? dt = TryConvert.ToDateTime(tbDateBegin.Text);
            if (string.IsNullOrEmpty(_msgErro) && !dt.HasValue)
            {
                _msgErro += "- Data Início é inválida.";
            }

            //data inicio < dia de amanha
            if (string.IsNullOrEmpty(_msgErro) && dt.Value <= DateTime.Now)
            {
                _msgErro += "- Data Início é inválida.";
            }

            //dia do vencimento vazio
            if (string.IsNullOrEmpty(_msgErro) && string.IsNullOrEmpty(mDataVencimentoDayBox.Text))
            {
                _msgErro += "- O Dia da Data de Vencimento deve ser preenchido.";
            }

            //mes do vencimento >= mes do data de inicio
            //if (string.IsNullOrEmpty(_msgErro) && TryConvert.ToInt32(mDataVencimentoMonthddl.SelectedValue) < dt.Value.Month)
            //{
            //    _msgErro += "- Mes de Vencimento é inválido.";
            //}

            //dia do vencimento tem caracteros invalidos
            int? _dia = TryConvert.ToInt32(mDataVencimentoDayBox.Text);
            if (string.IsNullOrEmpty(_msgErro) && !_dia.HasValue)
            {
                _msgErro += "- Dia de Vencimento é inválido.";
            }

            //dia do vencimento >= dia do data de inicio do mesmo mes
            //if (string.IsNullOrEmpty(_msgErro) && TryConvert.ToInt32(mDataVencimentoMonthddl.SelectedValue).Value == dt.Value.Month &&
            //    _dia.Value < dt.Value.Day)
            //{
            //    _msgErro += "- Dia de Vencimento é inválido.";
            //}

            msgErro += _msgErro;
        }

        private void ResetSimulation()
        {
            this.Page.GetType().InvokeMember("ResetSimulation", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
        }


        #endregion
    }
}