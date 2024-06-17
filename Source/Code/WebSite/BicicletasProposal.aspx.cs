using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;
using Resources;
using Lusitania.Simuladores.DataLayer;
using GDS = Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;
using System.Collections.Generic;
using System.Collections.Specialized;
using Lusitania.Simuladores.WebSite.Helpers.ChildrenProtection;
using Lusitania.Simuladores.WebSite.Controls;

using AjaxControlToolkit;
using Lusitania.Simulador.WebCommon.Common;
using _nsGenericPlanDS = Lusitania.Simuladores.DataLayer.GenericPlanDSTableAdapters;


// New
using Lusitania.LusnetBase;
using Lusitania.Web.Simuladores.Base.Classes;
using Lusitania.Model.Simuladores.Negocio;
using Lusitania.Utilities;
using System.Net;
using Lusitania.Simuladores.WebSite.Common;
using Lusitania.Simuladores.WebSite.WsPlanos;
using System.Threading;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Simulation;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simuladores.DataLayer.Proposal;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Lusitania.Simuladores.DataLayer.Contract;
using Lusitania.Simuladores.DataLayer.Common;

namespace Lusitania.Simuladores.WebSite.Pages
{
    //public delegate int ValidaCobraMediadorPropostaHandler(string A_LOGIN, string A_MEDIINTROD, string A_COBRAINTROD, out string A_MEDIADOR, out string A_COBRADOR, out string A_MSG_ERROR);
    public partial class BicicletasProposal : MyPage//Page
    {

        #region ----- Variáveis de instância -----

        private _nsGenericPlanDS.QueriesTableAdapter _qtafGenericPlanDS;


        #endregion

        #region ----- Propriedades -----

        private Simulacao _Simulacao { get; set; }

        private PropostaDS _propostaDS = new PropostaDS();

        #region ----- Variaveis Para o controlo de Anulações -----
        protected string lastNIFHolder
        {
            get { return ViewState[LAST_NIF] as string; }
            set { ViewState[LAST_NIF] = value; }
        }

        protected string validPriorCancMsg
        {
            get { return ViewState[VALID_PREV_CANC_MSG] as string; }
            set { ViewState[VALID_PREV_CANC_MSG] = value; }
        }
        #endregion ----- Variaveis Para o controlo de Anulações -----

        #endregion

        #region Fields

        private bool? mCanSeeSaveContractButton;
        private bool? mCanSeePrintContractButton;
        private bool? mCanSeePrintReceiptButton;

        #endregion

        #region Consts
        private const string PARAM_PLANID = "PlanID";
        private const string PARAM_SIMULATIONID = "SimulationID";

        private const string LAST_NIF = "LastNIF";
        private const string VALID_PREV_CANC_MSG = "PrevCancMsg";

        private const int SimulatorID = 51;

        private const string PROPOSTA = "Proposta Bicicletas";
        private const string CONTRATO = "Contrato Bicicletas";


        #endregion

        #region Properties

        protected DateTime? DataNascimentoPS
        {
            get { return ViewState["DataNascimentoPS"] as DateTime?; }
            set { ViewState["DataNascimentoPS"] = value; }
        }

        protected bool IsDirty
        {
            get { return (ViewState["IsDirty"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsDirty"] = value; }
        }

        protected bool IsProposalDone
        {
            get { return (ViewState["IsProposalSaved"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsProposalSaved"] = value; }
        }
        protected bool IsContractDone
        {
            get { return (ViewState["IsContractDone"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsContractDone"] = value; }
        }

        protected string Nif
        {
            get { return ViewState["Nif"] as string; }
            set { ViewState["Nif"] = value; }
        }

        protected decimal? SimulationID
        {
            get { return ViewState[PARAM_SIMULATIONID] as decimal?; }
            set { ViewState[PARAM_SIMULATIONID] = value; }
        }

        protected bool HasSDD
        {
            get { return (bool)ViewState["HasSDD"]; }
            set { ViewState["HasSDD"] = value; }
        }


        protected bool CanSeeSaveContractButton
        {
            get
            {
                return _propostaDS.MostraBotaoContrato(SimulationID.Value, UserHelper.UserName);
            }
        }

        protected bool CanSeePrintReceiptButton
        {
            get
            {
                return _propostaDS.MostraBotaoImpRecibo(SimulationID.Value, UserHelper.UserName);
            }
        }




        protected decimal? GenericPlanID
        {
            get { return ViewState[PARAM_PLANID] as decimal?; }
            set { ViewState[PARAM_PLANID] = value; }
        }

        protected bool IsProposalPrinted
        {
            get { return (ViewState["IsProposalPrinted"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsProposalPrinted"] = value; }
        }

        #endregion


        #region >>> Page Events <<<

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            WebUtility.HookChangeEventOnAllControls(Page, new EventHandler(delegate(object pSender, EventArgs pArgs)
            {
                if (!IsContractDone)
                {
                    ResetProposta();
                }

            }));
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            int EscondeHeader;

            try
            {
                EscondeHeader = int.Parse(Request.Params.Get("EscondeHeader"));
            }
            catch
            {
                EscondeHeader = 0;
            }

            if (EscondeHeader == 1)
            {
                Image1.Visible = false;
                ChildrenProtectionHeaderDiv.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            NameValueCollection n;

            if (!IsPostBack)
            {
                lastNIFHolder = "-";
                ResetProposta();

                if (String.IsNullOrEmpty(UserHelper.UserName))
                    Response.Redirect(Request.UrlReferrer.ToString());

                n = Request.Params;

                if (n.HasKeys())
                {

                    InitSimulacaoData();

                    GenericPlanID = TryConvert.ToDecimal(n.Get(PARAM_PLANID));

                    string mediator = string.Empty;
                    string collector = string.Empty;
                    DataHelper.PreSelectCommercialAccount(GenericPlanID, ref mediator, ref collector);
                    mCommercialAccount.MediatorNumber = mediator;
                    mCommercialAccount.CollectorNumber = collector;
                    mCommercialAccount.IsProposalPage = true;

                    // set the start nif
                    if (GenericPlanID.HasValue)
                    {
                        mTomador.pIdPlano = GenericPlanID.Value;
                        mFraccionamento.GenericPlanID = GenericPlanID;

                       

                    }

                }

                // configure 
                mTomador.Visible =
                mCommercialAccount.Visible = !GenericPlanID.HasValue;



                //woraround para posicionameto dos popups
                mSaveContractButton.Attributes.Add("open-popup", mBicicletasSaveContractQuestions.popupID);
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {

            UpdateCommands();

            base.OnPreRender(e);
            if (mTomador.NIF.Length>0)
            {
                mInquerito.UpdateEmailEnvio(mTomador.pCodigo.ToString());
            }

        }

        #endregion


        #region >>> Elements Events <<<

        protected void mBackToGenericPlanButton_Click(object sender, EventArgs e)
        {
            string urlPlan;

            try
            {
                if (!IsDirty && IsProposalDone)
                {
                    /*if (GenericPlanID.HasValue)
                    {
                        urlPlan = WsHelper.GetGenericPlanUrl(GenericPlanID,
                            ConfigurationManager.AppSettings["NetworkCredential_user"],
                            ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                        Response.Redirect(urlPlan);
                    }*/
                    Response.Redirect(ServidorDestino);
                }
                else
                {
                    ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                    mSaveProposalButton.Visible = true;
                    UpdateCommands();
                }

            }
            catch (ThreadAbortException)
            {
                //Ignora esta excepção em particular.
            }
            catch (Exception ex)
            {
                ScriptHelper.ShowMessageBox("Não foi possível efectuar a operação");
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        //protected void mBackToFamilyPlanButton_Click(object sender, EventArgs e)
        //{
        //    string urlPlan;

        //    try
        //    {

        //        if (!IsDirty && IsProposalSaved)
        //        {
        //            if (FamilyPlanID.HasValue)
        //            {
        //                urlPlan = WsHelper.GetGenericPlanUrl(
        //                    FamilyPlanID,
        //                    ConfigurationManager.AppSettings["NetworkCredential_user"],
        //                    ConfigurationManager.AppSettings["NetworkCredential_pass"]);
        //                Response.Redirect(urlPlan);
        //            }
        //            else
        //            {
        //                ScriptHelper.ShowMessageBox("Não foi possível efectuar a operação");
        //            }
        //        }
        //        else
        //        {
        //            ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
        //            mSaveProposalButton.Visible = true;
        //            UpdateCommands();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptHelper.ShowMessageBox("Não foi possível efectuar a operação");
        //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        //    }
        //}




        protected void mSaveButton_Click(object sender, EventArgs e)
        {
            string erros = validar();
            if (string.IsNullOrEmpty(erros))
            {
                gravarProposta();
            }
            else
            {
                ScriptHelper.ShowMessageBox(erros, PROPOSTA, ScriptHelper.AlertType.ALERT);
            }
        }
        protected void mPrintProposalButton_Click(object sender, EventArgs e)
        {
            if (IsProposalDone)
            {
                ScriptHelper.ShowWindow(ResolveUrl(PageLinks.BicicletasInfoNote));

                ScriptHelper.ShowWindow(ResolveUrl(PageLinks.BicicletasQuotation), "templateName={0}&recordName={1}&ID={2}&docType={3}", "PROPOSTA_RC.xdp", "PROP_BIKE", string.Concat(SimulationID.Value.ToString(), HasSDD ? "_SDD" : string.Empty), "7");

                _propostaDS.AtualizaImprimiuProposta(SimulationID.Value);

                UpdateCommands();
            }

        }




        protected void mContractButton_Click(object sender, EventArgs e)
        {
            if (!IsDirty && IsProposalDone)
            {
                ShowSaveContractDialog();
            }
        }

        protected void mPrintContractButton_Click(object sender, EventArgs e)
        {
            // Condições Particulares - Tipo Documento: 77
            ScriptHelper.ShowWindow("Pages/OutputES_PDF.aspx?templateName=condicao_particular_rc.xdp&recordName=CONDICOESRC&ID=" + SimulationID.Value + "&docType=77");
        }


        protected void mGestaoUnicaRecibos_Click(object sender, EventArgs e)
        {
            ScriptHelper.ShowWindow(ConfigurationManager.AppSettings.Get("GestaoUnicaoRecibosUrl") + "?Ramo=" + UtilityG.RMS.substring(0, 2) + "&Apolice=" + mBicicletasSaveContractQuestions.Apolice + "&username=" + UserHelper.UserName);
        }

        protected void mPrintReceiptButton_Click(object sender, EventArgs e)
        {
            ContractModel _cm = (new ContractDS()).IMP_RECIBO(HttpContext.Current.Request.Params.Get("SimulationID"));

            if (_cm != null)
            {

                var certificado = Functions.ValidaCertificado(_cm.RAMO, _cm.APOLICE, _cm.RECIBO);
                string faturaReciboAtivo = Functions.VerificaConstante("SIMULADORES_WEB_GERAL", "FATURA_RECIBO");
                if (certificado.Equals("1") && faturaReciboAtivo.Equals("S"))
                {
                    ScriptHelper.ShowWindow("Pages/OutputES_PDF.aspx?templateName=recibos_premio_estorno_fatura.xdp&recordName=FATURA_RECIBO&ID=" + SimulationID + "&docType=338" + "&ramo=" + _cm.RAMO + "&apolice=" + _cm.APOLICE + "&recibo=" + _cm.RECIBO);
                }
                else
                {
                    ScriptHelper.ShowWindow("Pages/OutputES_PDF.aspx?templateName=recibos_premio_estorno.xdp&recordName=RECIBO&ID=" + SimulationID + "&docType=131" + "&ramo=" + _cm.RAMO + "&apolice=" + _cm.APOLICE + "&recibo=" + _cm.RECIBO);
                }

            }
        }

        protected void mSaveContractQuestions_Success(object sender, EventArgs e)
        {
            IsContractDone = true;
        }


        #endregion


        #region >>> Helper Methods <<<

        //private void ShowNibControl()
        //{
        //    HasSDD = true;
        //    /*this.mNib.Visible = true;
        //    this.mNibLabel.Visible = true;
        //    this.chkPlanoPagamentos.Visible = true;
        //    this.LabelPlanoPagamentos.Visible = true;*/
        //    //setPaymentInfoVisible(true);
        //    //enableDisablePaymntFields(true);
        //}

        //private void LoadPaymentMethod()
        //{
        //    //Determine the payment method                
        //    PaymentMethod = WsHelper.GetPlanPaymentMethod(GenericPlanID,
        //        ConfigurationManager.AppSettings["NetworkCredential_user"],
        //        ConfigurationManager.AppSettings["NetworkCredential_pass"]);
        //}

        private void InitSimulacaoData()
        {
            string msgErro = string.Empty;
            SimulationID = TryConvert.ToDecimal(Request.Params.Get(PARAM_SIMULATIONID));

            if (SimulationID.HasValue)
            {
                _Simulacao = SimulacaoDS.ObterSimulacao(SimulationID.Value, ref msgErro);
                if (string.IsNullOrEmpty(msgErro))
                {
                    DataNascimentoPS = TryConvert.ToDateTime(_Simulacao.Contrato.PessoaSeguraDataNasc);

                    //nif Tomador
                    mTomador.StartNif = Nif = Request.Params.Get("NIF");
                    mTomador.Telemovel = _Simulacao.Contrato.Tomador.Telefon;
                    mTomador.Email = _Simulacao.Contrato.Tomador.Email;
                    //Fraccionamento
                    mFraccionamento.SimulationProduct = _Simulacao.Outputs.Produtos.FirstOrDefault().ProductID;
                    HasSDD = mFraccionamento.HasSDD = _Simulacao.Contrato.SDD == "S";

                    //Pessoa Segura
                    mpessoasegura.Product = _Simulacao.Outputs.Produtos.FirstOrDefault().ProductID;
                    mpessoasegura.Age = BusinessHelper.getAge(TryConvert.ToDateTime(_Simulacao.Contrato.PessoaSeguraDataNasc) ?? DateTime.Now);

                }
            }
        }

        private void UpdateCommands()
        {
            ShowHideBackToPlanButton();

            mPrintProposalButton.Visible = !IsDirty && IsProposalDone && !GenericPlanID.HasValue;
            if (IsPostBack)
            {
                mSaveProposalButton.Visible = IsDirty && !IsProposalDone;
            }

            validatePrevCanc();
            mSaveContractButton.Visible = !IsDirty && CanSeeSaveContractButton && !GenericPlanID.HasValue && string.IsNullOrEmpty(validPriorCancMsg);
            mPrintReceiptButton.Visible = !IsDirty && CanSeePrintReceiptButton && !GenericPlanID.HasValue;
            mPrintContractButton.Visible =
            mGestaoUnicaRecibos.Visible = !IsDirty && IsContractDone && !GenericPlanID.HasValue;

            if (mSaveContractButton.Visible)
            {
                int contractPermissions = BicicletasInterface.GetContractPermissionLevel(UserHelper.UserName);
                if (contractPermissions == 1)
                {
                    mSaveContractButton.Visible = false;
                }
            }

        }

        private void ShowHideBackToPlanButton()
        {
            mBackToGenericPlanButton.Visible =
            mBackToGenericPlanButton.Enabled = GenericPlanID.HasValue;
        }

        private int validatePrevCanc()
        {
            int v_erro = 0;
            string v_msg_erro = string.Empty;
            if (!GenericPlanID.HasValue && (!string.IsNullOrEmpty(mTomador.NIF)) && !lastNIFHolder.Equals(mTomador.NIF))
            {
                lastNIFHolder = mTomador.NIF;
                Hashtable xmlValues = new Hashtable();
                xmlValues.Add("SIM_ID", SimulatorID.ToString());
                xmlValues.Add("SIM_NUM", SimulationID.ToString());

                xmlValues.Add("HOLDER_NIF", lastNIFHolder);
                xmlValues.Add("LOGIN", UserHelper.UserName);

                WsHelper.validarAnulacoesRecentes(xmlValues, ref v_erro, ref v_msg_erro
                    , ConfigurationManager.AppSettings["NetworkCredential_user"],
                    ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                validPriorCancMsg = v_msg_erro;

                ScriptHelper.ShowMessageBoxIfNotEmpty(v_msg_erro, PROPOSTA, ScriptHelper.AlertType.ALERT);
            }
            return v_erro;
        }

        private Boolean IsValidInput()
        {
            bool _isValid = true;
            string mediator;
            string collector;
            string err_msg;

            //object[] parameters = new object[] { UserHelper.UserName, mCommercialAccount.MediatorNumber, mCommercialAccount.CollectorNumber, string.Empty, string.Empty, string.Empty };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("VALIDA_WEB_PKG.VALIDA_COBRA_MEDIADOR_PROPOSTA", (ValidaCobraMediadorPropostaHandler)(new GDS.QueriesTableAdapter()).VALIDA_COBRA_MEDIADOR_PROPOSTA, ref parameters);
            //mediator = (string) parameters[3];
            //collector = (string) parameters[4];
            //err_msg = (string) parameters[5];

            (new GDS.QueriesTableAdapter()).VALIDA_COBRA_MEDIADOR_PROPOSTA(UserHelper.UserName,
                mCommercialAccount.MediatorNumber,
                mCommercialAccount.CollectorNumber,
                out mediator,
                out collector,
                out err_msg
                );

            if (!string.IsNullOrEmpty(err_msg))
                _isValid = false;

            return _isValid;
        }

        public void ResetProposta()
        {
            IsProposalDone =
            IsContractDone = false;

            IsDirty = true;
        }

        private void ShowSaveContractDialog()
        {
            mBicicletasSaveContractQuestions.SimulationID = SimulationID;
            mBicicletasSaveContractQuestions.Show();
        }





        #endregion


        #region ----- Validar -----

        private string validar()
        {
            string _msgErro = string.Empty;
            string _tempMsgErro = string.Empty;

            //Conta Comercial
            if (mCommercialAccount.isVizible)
            {
                mCommercialAccount.ValidarDados(ref _tempMsgErro);
                if (!string.IsNullOrEmpty(_tempMsgErro))
                {
                    BusinessHelper.AddErroAtList(ref  _msgErro, "<b>CONTA COMERCIAL </b><br />" + _tempMsgErro, false);
                    _tempMsgErro = string.Empty;
                }
            }

            //Tomador
            mTomador.validar(ref _tempMsgErro);
            if (!string.IsNullOrEmpty(_tempMsgErro))
            {
                BusinessHelper.AddErroAtList(ref  _msgErro, "<b>TOMADOR </b><br />" + _tempMsgErro, false);
                _tempMsgErro = string.Empty;
            }
            else
            {
                if (mpessoasegura.IsPSIgualTomador)
                {

                    if (mTomador.DataNascimento.HasValue && DataNascimentoPS.HasValue
                        && mTomador.DataNascimento.Value != DataNascimentoPS.Value)
                    {
                        BusinessHelper.AddErroAtList(ref  _msgErro, "<b>TOMADOR </b><br /> - A data de nascimento do tomador não corresponde à data introduzida na simulação");
                    }
                }
            }

            //Fracionamento
            mFraccionamento.validar(ref _tempMsgErro);
            if (!string.IsNullOrEmpty(_tempMsgErro))
            {
                BusinessHelper.AddErroAtList(ref  _msgErro, "<b>FRACCIONAMENTO </b><br />" + _tempMsgErro, false);
                _tempMsgErro = string.Empty;
            }

            //Pessoa segura
            mpessoasegura.validar(ref _tempMsgErro);
            if (!string.IsNullOrEmpty(_tempMsgErro))
            {
                BusinessHelper.AddErroAtList(ref  _msgErro, "<b>PESSOA SEGURA </b><br />" + _tempMsgErro, false);
                _tempMsgErro = string.Empty;
            }
            else
            {
                if (!mpessoasegura.IsPSIgualTomador)
                {


                    if (mpessoasegura.DataNascimento.HasValue && DataNascimentoPS.HasValue
                        && mpessoasegura.DataNascimento.Value != DataNascimentoPS.Value)
                    {
                        BusinessHelper.AddErroAtList(ref  _tempMsgErro, "A data de Nascimento da Pessoa segura não corresponde à data introduzida na simulação");
                    }

                    if (mpessoasegura.NIF == mTomador.NIF)
                    {
                        BusinessHelper.AddErroAtList(ref  _tempMsgErro, "O NIF do Tomador não pode ser igual ao da Pessoa Segura");
                    }

                    if (!string.IsNullOrEmpty(_tempMsgErro))
                    {
                        BusinessHelper.AddErroAtList(ref  _msgErro, "<b>PESSOA SEGURA </b><br />" + _tempMsgErro, false);
                        _tempMsgErro = string.Empty;
                    }

                }
            }

            


            //Objeto Seguro
            //mObjetoSeguro.validar(ref _tempMsgErro);
            //if (!string.IsNullOrEmpty(_tempMsgErro))
            //{
            //    BusinessHelper.AddErroAtList(ref  _msgErro, "<b>OBJETO SEGURO - BICICLETA </b><br />" + _tempMsgErro, false);
            //    _tempMsgErro = string.Empty;
            //}

            //Inquerito
            mInquerito.validar(ref _tempMsgErro);
            if (!string.IsNullOrEmpty(_tempMsgErro))
            {
                BusinessHelper.AddErroAtList(ref  _msgErro, "<b>RESPOSTAS OBRIGATÓRIAS </b><br />" + _tempMsgErro, false);
                _tempMsgErro = string.Empty;
            }
            else
            {
                if (mInquerito.ExistDebitoPorFalta)
                {
                    BusinessHelper.AddErroAtList(ref  _msgErro, "<b>RESPOSTAS OBRIGATÓRIAS </b><br /> - Não é possível efectuar o seguro com débitos por liquidar", false);
                }
            }

            try
            {
                //RGPD
                mRGPD.validar(ref _tempMsgErro);
                if (!string.IsNullOrEmpty(_tempMsgErro))
                {
                    BusinessHelper.AddErroAtList(ref  _msgErro, "<b>TRATAMENTO DE DADOS PESSOAIS </b><br />" + _tempMsgErro, false);
                    _tempMsgErro = string.Empty;
                }
            }
            catch (Exception)
            {
            }

            if (!string.IsNullOrEmpty(_msgErro))
            {
                _msgErro = "<b>Faltam preencher os seguintes campos:</b><br /><hr />" + _msgErro;
            }

            return _msgErro;
        }

        #endregion


        #region ----- Gravar Proposta -----

        private string dateParameterToString(DateTime? date)
        {
            string ret = string.Empty;

            if (date != null)
            {
                ret = ((DateTime)date).ToString("dd-MM-yyyy");
            }

            return ret;
        }

        public static string Age(DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age)) age--;

            return age.ToString();
        }

        private void gravarProposta()
        {
            Proposta _proposta = new Proposta();

            _proposta.IDSIMULACAO = SimulationID.Value.ToString();
            _proposta.LOGIN = UserHelper.UserName;
            _proposta.MEDIADOR = mCommercialAccount.MediatorNumber;
            _proposta.COBRADOR = mCommercialAccount.CollectorNumber;

            mTomador.GetData(ref _proposta);
            
            //workaround para o Plano E+
            try
            {
                if (string.IsNullOrEmpty(_proposta.PESSOAS[0].DATA_NASCIMENTO))
                    _proposta.PESSOAS[0].DATA_NASCIMENTO = DataNascimentoPS.Value.ToShortDateString();
            }
            catch
            {}
            
            mFraccionamento.GetData(ref _proposta);
            mpessoasegura.GetData(ref _proposta);
            mObjetoSeguro.GetData(ref _proposta);
            mInquerito.GetData(ref _proposta);
            //mNotes.GetData(ref _proposta);

            if (!string.IsNullOrEmpty(mTomador.DataNascimento.ToString()))
            {
                string strDtNasc = dateParameterToString(mTomador.DataNascimento);
                string strAge = Age(Convert.ToDateTime(strDtNasc));
                if (Convert.ToInt32(strAge) < 18)
                {
                    ScriptHelper.ShowMessageBox("A Data de Nascimento do Tomador não pode ser inferior a 18 anos.", PROPOSTA, ScriptHelper.AlertType.ERROR);
                    return;
                }
            }

            if (_proposta.DOCS_POR_EMAIL == "1")
            {
                if (_proposta.EMAIL_PARA_DOCS.Length == 0)
                {
                    ScriptHelper.ShowMessageBox("Indique o Email a enviar a documentação.", PROPOSTA, ScriptHelper.AlertType.ERROR);
                    return;
                }
                if (_proposta.EMAIL_PARA_DOCS == "null")
                {
                    ScriptHelper.ShowMessageBox("Indique o Email a enviar a documentação.", PROPOSTA, ScriptHelper.AlertType.ERROR);
                    return;
                }

            }

            mBicicletasSaveContractQuestions.PaymentType = HasSDD ? "sdd" : "online";

            _proposta = _propostaDS.Gravar(_proposta);
            if (_proposta == null)
            {
                string _msgErro = "Ocorreu um Erro ao gravar a Proposta.";
                ScriptHelper.ShowMessageBox(_msgErro, PROPOSTA, ScriptHelper.AlertType.ERROR);
            }
            else if (_proposta.Outputs != null && _proposta.Outputs.Erros != null && _proposta.Outputs.Erros.Count > 0)
            {
                string _msgErro = string.Empty;
                foreach (Erro item in _proposta.Outputs.Erros)
                {
                    BusinessHelper.AddErroAtList(ref _msgErro, item.MsgErro);
                }
                ScriptHelper.ShowMessageBox(_msgErro, PROPOSTA, ScriptHelper.AlertType.ERROR);
            }
            else
            {

                try
                {
                    SaveRGPD(GetPessoaID(_proposta), GetNIF(_proposta), mRGPD.isRGPD_VALUE(), UserHelper.UserName);
                }
                catch (Exception)
                {
                }

                IsProposalDone = true;
                IsDirty = false;
                ScriptHelper.ShowMessageBox("Proposta Gravada com Sucesso.", PROPOSTA, ScriptHelper.AlertType.INFO);
            }
        }

        private string GetPessoaID(Proposta a_proposta)
        {

            try
            {
                string PESSOA = "0";

                if (a_proposta.PESSOAS.Count > 0)
                {
                    for (int i = 0; i < a_proposta.PESSOAS.Count; i++)
                    {
                        if (a_proposta.PESSOAS[i].TIPO == "1" && !string.IsNullOrEmpty(a_proposta.PESSOAS[i].PCODIGO))
                            PESSOA = a_proposta.PESSOAS[i].PCODIGO;

                    }
                }
                return PESSOA;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string GetNIF(Proposta a_proposta)
        {

            try
            {
                string NIF = "";

                if (a_proposta.PESSOAS.Count > 0)
                {
                    for (int i = 0; i < a_proposta.PESSOAS.Count; i++)
                    {
                        if (a_proposta.PESSOAS[i].TIPO == "1")
                            NIF = a_proposta.PESSOAS[i].NUMERO_CONTRIBUINTE;
                    }
                }
                return NIF;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void SaveRGPD(string PESSOA, string NIF, string CONSENTIMENTO, string LOGIN)
        {

            string URLAuth = "";
            const string contentType = "application/json";
            Exception oEx = null;

            try
            {
                URLAuth = ConfigurationManager.AppSettings["RGPD_url"] + "InsertPessoaRGPD";
                System.Net.ServicePointManager.Expect100Continue = false;

                HttpWebRequest webRequest = WebRequest.Create(URLAuth) as HttpWebRequest;
                //webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_user"], ConfigurationManager.AppSettings["NetworkCredential_pass"]);
                SetBasicAuthHeader(webRequest, ConfigurationManager.AppSettings["NetworkCredential_user_RGPD"], ConfigurationManager.AppSettings["NetworkCredential_pass_RGPD"]);
                webRequest.Method = "POST";
                webRequest.ContentType = contentType;

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {

                    string json = "{\"PESSOA\":\"" + PESSOA + "\"," +
                                   "\"NIF\":\"" + NIF + "\"," +
                                   "\"CONSENTIMENTO\":\"" + CONSENTIMENTO + "\"," +
                                   "\"ORIGEM\":\"8\"," +
                                   "\"OBS\":\"\"," +
                                   "\"LOGIN\":\"" + LOGIN + "\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                string responseData = responseReader.ReadToEnd();

                responseReader.Close();
                webRequest.GetResponse().Close();

            }
            catch (Exception ex)
            {
                oEx = ex;
            }
        }

        public void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }


        #endregion

    }
}
