using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Lusitania.Simuladores.WebSite.Common;
using Lusitania.Simuladores.WebSite.WsPlanos;
using System.Text;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Simulation;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simulador.WebCommon.Common;
using Resources;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using System.Threading;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public delegate Simulacao ObjectAndStringByRefHandler(Simulacao arg1, ref string arg2);

    public partial class MultiTarificador : System.Web.UI.UserControl
    {
        SimulacaoDS _db = new SimulacaoDS();
        private const string SIMULACAO = "Simulação Bicicletas";


        #region Properties
        public decimal? GenericPlanID
        {
            get { return ViewState["GenericPlanID"] as decimal?; }
            set { ViewState["GenericPlanID"] = value; }
        }

        public decimal? SimulationID
        {
            get { return ViewState["SimulationID"] as decimal?; }
            set
            {
                ViewState["SimulationID"] = value;
                mQuotationResults.SimulatorID = SimulationID;
            }
        }

        public string NumeroContribuinte
        {
            get { return ViewState["NumeroContribuinte"] as string; }
            set { ViewState["NumeroContribuinte"] = value; }
        }


        public decimal ProtocoloID
        {
            get
            {
                return TryConvert.ToInt32(mDDLProtocolo.SelectedValue) ?? 0;
            }
        }


        public Pagamento PaymentMethod
        {
            get { return (ViewState["PaymentMethod"] != null) ? (Pagamento)ViewState["PaymentMethod"] : null; }
            set { ViewState["PaymentMethod"] = value; }
        }

        public bool CanSeeProcutLAZER
        {
            get { return ViewState["CanSeeProcutLAZER"] == null ? false : (bool)ViewState["CanSeeProcutLAZER"]; }
            set { ViewState["CanSeeProcutLAZER"] = value; }
        }

        public string SelectedQuoteId
        {
            get { return mQuotationResults.getSelectedQuotationID(SelectedProductID); }
        }

        public String SelectedProductID
        {
            get { return ViewState["SelectedProductID"] as string; }
            set
            {
                ViewState["SelectedProductID"] = value;
                if (!string.IsNullOrEmpty((string)ViewState["SelectedProductID"]))
                {
                    EnableButtons(true);
                    if (string.IsNullOrEmpty(this.SelectedQuoteId))
                    {
                        SaveSimulacao();
                    }

                    MultitarificadorUpdatePanel.Update();

                }
            }
        }

        public string UrlPlan{
            get { return ViewState["UrlPlan"] as string; }
            set { ViewState["UrlPlan"] = value; }
        }

        public Simulacao Simulacao { get; set; }


        #endregion

        #region Page LiveCycle

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Condição de visibilidade em plano e sem plano
                mProposalButton.Visible =
                mEnviar.Visible =
                mPrintButton.Visible =
                chkSDD.Enabled = !GenericPlanID.HasValue && !String.IsNullOrEmpty(UserHelper.UserName);
                
                //Condição do campo SDD               
                if (GenericPlanID.HasValue)
                {
                    LoadPaymentMethod();
                    chkSDD.Checked = PaymentMethod != null && PaymentMethod.MetodoPagamento.Equals("SDD");
                    mQuotationResults.ShowPremiumEmPlano = false;
                    comSDDrow.Visible = false;
                }
                else {
                    mQuotationResults.ShowPremiumEmPlano = true;
                }

                mBackToGenericPlanButton.Visible = GenericPlanID.HasValue && !String.IsNullOrEmpty(UserHelper.UserName);

                //Bind
                CarregarProtocolos();
                //CarregarCE();

                if (!SimulationID.HasValue)
                {
                    CanSeeProcutLAZER = true;
                }
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            //Activete or desactivate btns
            if (!IsPostBack)
            {
                if (!SimulationID.HasValue)
                {
                    EnableButtons(!string.IsNullOrEmpty(SelectedProductID));
                }
            }

            base.OnPreRender(e);
        }

        #endregion

        #region Bind Section

        private void CarregarProtocolos()
        {
            if (!string.IsNullOrEmpty(NumeroContribuinte))
            {
                bool isNIF_ENT = BusinessHelper.IsEnterpriseNif(NumeroContribuinte);
            }
            //if (!string.IsNullOrEmpty(mClientData.NIF))
            mDDLProtocolo.Items.Clear();
            mDDLProtocolo.Items.Add(new ListItem("--- Escolhe ---", ""));
            mDDLProtocolo.AppendDataBoundItems = true;
            mDDLProtocolo.DataTextField = "Descritivo";
            mDDLProtocolo.DataValueField = "Codigo";
            mDDLProtocolo.DataSource = (new ProtocoloDS()).Load("13", "01", "00", UserHelper.UserName, (UserHelper.UserName + string.Empty).Length > 5 ? UserHelper.UserName.Substring(1, 5) : UserHelper.UserName);
            mDDLProtocolo.DataBind();

        }
        /*private void CarregarCE()
        {
            mGridCE_Portugal.DataSource = (new CONDICAO_ESPECIAL_030()).dt_Portugal;
            mGridCE_Portugal.DataBind();

            mGridCE_Espanha.DataSource = (new CONDICAO_ESPECIAL_030()).dt_Espanha;
            mGridCE_Espanha.DataBind();
        }
        */
        private void LoadPaymentMethod()
        {
            //Determine the payment method                
            PaymentMethod = WsHelper.GetPlanPaymentMethod(GenericPlanID,
                ConfigurationManager.AppSettings["NetworkCredential_user"],
                ConfigurationManager.AppSettings["NetworkCredential_pass"]);
        }

        private Simulacao BindSimulation()
        {
            Simulacao.Login = UserHelper.UserName;
            Simulacao.Contrato.Protocolo = mDDLProtocolo.SelectedValue;
            Simulacao.Contrato.NumAssociado = mNumAssociado.Text;
            Simulacao.Contrato.SDD = chkSDD.Checked ? "S" : "N";
            Simulacao.Gravar = !string.IsNullOrEmpty(SelectedProductID) ? "S" : "N";


            //fazer o pedido para dois produtos 00006 e 00007
            Simulacao.Outputs = new Outputs()
            {
                Produtos = new List<Produto>()
            };

            if (string.IsNullOrEmpty(SelectedProductID) || (!string.IsNullOrEmpty(SelectedProductID) && SelectedProductID == UtilityG.GetValue(Products.LIGHT)))
            {
                Simulacao.Outputs.Produtos.Add(new Produto()
                {
                    ProductID = UtilityG.GetValue(Products.LIGHT)
                });
            }


            if (CanSeeProcutLAZER && (string.IsNullOrEmpty(SelectedProductID) || (!string.IsNullOrEmpty(SelectedProductID) && SelectedProductID == UtilityG.GetValue(Products.SUPER))))
            {
                Simulacao.Outputs.Produtos.Add(new Produto()
                {
                    ProductID = UtilityG.GetValue(Products.SUPER)
                });
            }


            return this.Simulacao;
        }

        private void BindPremios(Simulacao sim)
        {
            mQuotationResults.Produtos = sim.Outputs.Produtos;
            mQuotationResults.BindPremios();


            foreach (Produto item in sim.Outputs.Produtos)
            {
                if (item.ProductID == UtilityG.GetValue(Products.LIGHT))
                {
                    Premio _premioAnual = item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    if (_premioAnual != null)
                    {
                        mProduto_1_comercial.Text = UtilityG.GetFormatDoubleValue(_premioAnual.PremioComercial);
                        mProduto_1_total.Text = UtilityG.GetFormatDoubleValue(_premioAnual.PremioTotal);
                        mProduto_1_recibo.Text = UtilityG.GetFormatDoubleValue(_premioAnual.ValorRecibo);
                        mProduto_1_periodo.Text = _premioAnual.PeriodoRecibo_1;
                        mProduto_1_seguinte.Text = "N/A";//Utility.GetFormatDoubleValue(_premioAnual.ValorReciboSeguinte);
                        mProduto_1_plano.Text =  UtilityG.GetFormatDoubleValue(_premioAnual.PremioPlano);
                    }
                }

                if (item.ProductID == UtilityG.GetValue(Products.SUPER))
                {
                    Premio _premioAnual = item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    if (_premioAnual != null)
                    {
                        mProduto_2_comercial.Text = UtilityG.GetFormatDoubleValue(_premioAnual.PremioComercial);
                        mProduto_2_total.Text = UtilityG.GetFormatDoubleValue(_premioAnual.PremioTotal);
                        mProduto_2_recibo.Text = UtilityG.GetFormatDoubleValue(_premioAnual.ValorRecibo);
                        mProduto_2_periodo.Text = _premioAnual.PeriodoRecibo_1;
                        mProduto_2_seguinte.Text = "N/A";// Utility.GetFormatDoubleValue(_premioAnual.ValorReciboSeguinte);
                        mProduto_2_plano.Text = UtilityG.GetFormatDoubleValue(_premioAnual.PremioPlano);
                    }
                }
            }

        }

        public void LoadSimulacao(Simulacao sim)
        {
            mQuotationResults.LoadSimulacao(sim);

            BindPremios(sim);

            //Protocolo
            mDDLProtocolo.SelectedValue = sim.Contrato.Protocolo;
            if (!string.IsNullOrEmpty(sim.Contrato.Protocolo))
            {
                mNumAssociado.Enabled = true;
                mNumAssociado.Text = sim.Contrato.NumAssociado;

            }
            //Sdd
            chkSDD.Checked = sim.Contrato.SDD == "S";

            EnableButtons(true);

        }

        #endregion

        #region Events

        /*** ELEMENTOS ***/

        protected void chkSDD_CheckedChanged(object sender, EventArgs e)
        {
            ResetData();
        }


        /*** BOTOES ***/

        protected void mSimulationButton_Click(object sender, EventArgs e)
        {
            ResetData();
            SaveSimulacao();
        }

        protected void mProposalButton_Click(object sender, EventArgs e)
        {
           ShowProposalIFrame();              
        }

        protected void mPrintButton_Click(object sender, EventArgs e)
        {
            ScriptHelper.ShowWindow(ResolveUrl(PageLinks.BicicletasQuotation), "templateName={0}&recordName={1}&ID={2}&docType={3}", Strings.XDPQuotation, Strings.RecordSetQuotation, this.SelectedQuoteId, "7");             
        }

        protected void mEnviar_Click(object sender, EventArgs e)
        {
            mSendPopupQuotation.SimulationID = this.SelectedQuoteId;
            mSendPopupQuotation.HasSDD = chkSDD.Checked;
            mSendPopupQuotation.Show();
        }

        public void mBackToGenericPlanButton_Click(object sender, EventArgs e)
        {
            string /*urlPlan,*/ msg_erro, selectedSimulationId;
            try
            {
                msg_erro = string.Empty;
                selectedSimulationId = SelectedQuoteId;

                if (!string.IsNullOrEmpty(selectedSimulationId) && GenericPlanID.HasValue)
                {
                    (new SimulacaoDS()).ADD_BIKE(GenericPlanID.Value, selectedSimulationId, ref msg_erro);
                }

                if (!String.IsNullOrEmpty(msg_erro))
                    ScriptHelper.ShowMessageBox("Erro na inserção da Simulação no Plano <br />" + msg_erro, SIMULACAO, ScriptHelper.AlertType.ERROR);

                /*if (GenericPlanID.HasValue)
                {
                    urlPlan = WsHelper.GetGenericPlanUrl(
                        GenericPlanID,
                        ConfigurationManager.AppSettings["NetworkCredential_user"],
                        ConfigurationManager.AppSettings["NetworkCredential_pass"]);


                    Response.Redirect(urlPlan);
                }*/
                Response.Redirect(UrlPlan);
            }
            catch (ThreadAbortException)
            {
                //Ignora esta excepção em particular.
            }
            catch (Exception ex)
            {
                ScriptHelper.ShowMessageBox("Não foi possível efectuar a operação", SIMULACAO, ScriptHelper.AlertType.ERROR);
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }


        /*** PROTOCOLO ***/

        protected void mDDLProtocolo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ResetData();
            mNumAssociado.Enabled = !string.IsNullOrEmpty(mDDLProtocolo.SelectedValue);
        }

        protected void mNumAssociado_OnChangedText(object sender, EventArgs e)
        {
            ResetData();
        }

        #endregion

        #region Validation
        private bool IsValidPage()
        {
            return (bool)this.Page.GetType().InvokeMember("ValidarSimulacao", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
        }


        public void ValidarDados(ref string msgErro)
        {
            string _msgErro = string.Empty;

            //validar protocolo
            if (!string.IsNullOrEmpty(mDDLProtocolo.SelectedValue) && string.IsNullOrEmpty(mNumAssociado.Text))
            {
                _msgErro += "Indique o Número Associado.";
            }

            msgErro += (!string.IsNullOrEmpty(msgErro) ? "<br />" : "") + _msgErro;
        }
        #endregion

        #region Helper

        public void ResetData()
        {
            mQuotationResults.Clear();
            EnableButtons(false);
            ClearTableInfo();

            SelectedProductID = string.Empty;

            MultitarificadorUpdatePanel.Update();
        }

        public void BlockProduct(bool block)
        {
            CanSeeProcutLAZER = !block;
            mBlockDivProduct.Visible = block;
            ResetData();

            MultitarificadorUpdatePanel.Update();
        }

        private void EnableButtons(bool enable)
        {
            mProposalButton.Enabled =
                mEnviar.Enabled =
                mPrintButton.Enabled = enable;

            AddRemoveDisableClass(ref mProposalButton, !enable);
            AddRemoveDisableClass(ref mEnviar, !enable);
            AddRemoveDisableClass(ref mPrintButton, !enable);

        }

        private void AddRemoveDisableClass(ref Button bnt, bool add)
        {
            if (add)
            {
                if (bnt.CssClass.IndexOf("disabled") == -1)
                {
                    bnt.CssClass += " disabled";
                }

            }
            else
            {
                bnt.CssClass = bnt.CssClass.Replace(" disabled", "");
            }
        }

        private void ClearTableInfo()
        {
            mProduto_1_comercial.Text =
            mProduto_1_total.Text =
            mProduto_1_recibo.Text =
            mProduto_1_periodo.Text =
            mProduto_1_seguinte.Text =
            mProduto_1_plano.Text =
            mProduto_2_comercial.Text =
            mProduto_2_total.Text =
            mProduto_2_recibo.Text =
            mProduto_2_periodo.Text =
            mProduto_2_seguinte.Text =
            mProduto_2_plano.Text = string.Empty;
        }


        protected void mDDLProtocolo_SelectedIndexChanged(object sender, EventArgs e)
        {

            ResetData();
        }

        private void SaveSimulacao()
        {
            if (!IsValidPage())
            {
                string _msgErro = string.Empty;
                Simulacao _Simulacao = _db.Simular(BindSimulation(), ref _msgErro);

                if (!string.IsNullOrEmpty(_msgErro))
                {
                    ScriptHelper.ShowMessageBox(_msgErro, SIMULACAO, ScriptHelper.AlertType.ERROR);
                    mQuotationResults.simRadiosEnable(false);
                }
                else if (_Simulacao != null)
                {
                    if (_Simulacao.Outputs.Erros.Count() > 0)
                    {
                        string _msg = string.Empty;

                        foreach (Erro erro in _Simulacao.Outputs.Erros)
                        {
                            BusinessHelper.AddErroAtList(ref _msg , erro.MsgErro); 
                        }

                        ScriptHelper.ShowMessageBox(_msg, SIMULACAO, ScriptHelper.AlertType.ALERT);
                        mQuotationResults.simRadiosEnable(false);
                    }
                    else
                    {
                        BindPremios(_Simulacao);
                        mQuotationResults.simRadiosEnable(true);
                    }
                }
            }
        }

        private void ShowProposalIFrame()
        {
            this.Page.GetType().InvokeMember("ShowProposalIFrame", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] {  });
        }

        #endregion

    }
}