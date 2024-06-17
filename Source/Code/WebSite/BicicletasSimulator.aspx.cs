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
using System.Collections.Specialized;
using System.Xml.XPath;
using System.Text;
using System.Xml;
using Resources;

// New
using Lusitania.LusnetBase;
using System.Net;
using System.Collections.Generic;
using Lusitania.Simuladores.WebSite.Common;
using Lusitania.Simuladores.WebSite.WsPlanos;
using System.Threading;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Simulation;
using Lusitania.Simulador.WebCommon.Common;
using OSBConnector.Extensions;


public partial class BicicletasSimulator : MyPage
{
    #region Constants
    
    private const string PARAM_PLANID = "PlanID";
    private const string PARAM_SIMULATIONID = "SimulationID";

    private const string RMS = "130100";
    private readonly string RAMO = RMS.Substring(0, 2);
    private readonly string MODALIDADE = RMS.Substring(2, 2);
    private readonly string SUBMODALIDADE = RMS.Substring(4, 2);

    private const string SIMULACAO = "Simulação Bicicletas";


    #endregion

    #region Properties


    protected decimal? GenericPlanID
    {
        get { return ViewState[PARAM_PLANID] as decimal?; }
        set { ViewState[PARAM_PLANID] = value; }
    }

    protected decimal? SimulationID
    {
        get { return ViewState[PARAM_SIMULATIONID] as decimal?; }
        set { ViewState[PARAM_SIMULATIONID] = value; }
    }

    protected String SelectedQuotationID
    {
        get { return ViewState["SelectedQuoteId"] as string; }
        set { ViewState["SelectedQuoteId"] = value; }
    }

    protected string outputXML { get; set; }

    #endregion

    #region Page LiveCycle


    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();

            //Load Params
            
            
            mInsuranceData.GenericPlanID =
            mClientData.GenericPlanID = 
            mMultiTarificador.GenericPlanID =
            GenericPlanID = TryConvert.ToDecimal(Request.Params.Get(PARAM_PLANID));
            mMultiTarificador.UrlPlan = ServidorDestino;

            mMultiTarificador.SimulationID =
            mInsuranceData.SimulationID =
            SimulationID = TryConvert.ToDecimal(Request.Params.Get(PARAM_SIMULATIONID));


            if (SimulationID.HasValue)
            {
                LoadSimulacao();
            }
            //When using PlanID with SimulationID
            if (GenericPlanID.HasValue)
            {
                
            }

            ShowHideBackToPlanButton();
        }
    }

    #endregion

    #region Events

    /*** BOTOES ***/

    protected void mBackToGenericPlanButton_Click(object sender, EventArgs e)
    {
        mMultiTarificador.mBackToGenericPlanButton_Click(sender, e);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Shows ChildrenProtectionProposal.aspx inside an iframe
    /// </summary>
    /// <param name="nif">User Nif</param>
    public void ShowProposalIFrame()
    {
        if (!string.IsNullOrEmpty(mClientData.NIF))
        {
            if (BusinessHelper.IsValidNif(mClientData.NIF))
            {
                StringBuilder iFrameSrc;

                bool isNIF_ENT = BusinessHelper.IsEnterpriseNif(mClientData.NIF);
                string _ProtocoloTipoCliente;
                _ProtocoloTipoCliente = (new ProtocoloDS()).getTipoProtocolo(Convert.ToInt32(mMultiTarificador.ProtocoloID));

                if ((_ProtocoloTipoCliente == "E" && !BusinessHelper.IsEnterpriseNif(mClientData.NIF)) ||
                             (_ProtocoloTipoCliente == "P" && BusinessHelper.IsEnterpriseNif(mClientData.NIF)))
                {
                    ScriptHelper.ShowMessageBox("- O NIF colocado não é Válido para o tipo de protocolo selecionado.", SIMULACAO, ScriptHelper.AlertType.ERROR);
                }
                else
                {

                    //Url        
                    iFrameSrc = new StringBuilder("BicicletasProposal.aspx?EscondeHeader=1");
                    iFrameSrc.Append("&").Append(PARAM_SIMULATIONID).Append("=").Append(mMultiTarificador.SelectedQuoteId);
                    iFrameSrc.Append("&").Append("NIF").Append("=").Append(mClientData.NIF);

                    //Container
                    divSimulacao.Style["display"] = "none";

                    //Iframe
                    iFrameProposta.Attributes["src"] = iFrameSrc.ToString();
                    iFrameProposta.Attributes["height"] = "3500px";
                    mBackToSimulationButton.Visible = iFrameProposta.Visible = true;
                }
            }
            else
            {
                ScriptHelper.ShowMessageBox("O Número de Contribuinte é inválida", SIMULACAO, ScriptHelper.AlertType.ERROR);
            }
        }
        else
        {
            ScriptHelper.ShowMessageBox("Para iniciar a proposta, deve indicar o NIF do Tomador.", SIMULACAO, ScriptHelper.AlertType.ALERT);
        }



    }

    protected void mBackToSimulationButton_Click(object sender, EventArgs e)
    {
        //Container
        divSimulacao.Style["display"] = "";

        //Iframe            
        mBackToSimulationButton.Visible = iFrameProposta.Visible = false;

    }

    private void ShowHideBackToPlanButton()
    {
        mBackToFamilyPlanButton.Visible = GenericPlanID.HasValue;
    }

    private void DisableBackToPlanButton()
    {
        mBackToFamilyPlanButton.Enabled = false;
    }

    // Os users internos com perfil 'Gestor de Rede', 'Centros de Produção, 'Canal directo' ou Contact Center'
    // Podem simular, fazer propostas e contratar com data de início até 15 dias antes da data actual

    private bool HasPrivileges(string username)
    {
        if (ViewState["HasPrivileges"] == null)
        {
            string hasPrivileges = string.Empty;

            try
            {
                ViewState["HasPrivileges"] = hasPrivileges;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return TryConvert.ToBoolean(hasPrivileges).Value;
        }
        else
            return TryConvert.ToBoolean(ViewState["HasPrivileges"]).Value;
    }

   
    public void LoadSimulacao()
    {
        string _msgErro = string.Empty;

        Simulacao sim = SimulacaoDS.ObterSimulacao(SimulationID.Value, ref _msgErro);

        if (!string.IsNullOrEmpty(_msgErro))
        {
            ScriptHelper.ShowMessageBox(_msgErro, SIMULACAO, ScriptHelper.AlertType.ALERT);
        }
        else
        {
            mCommercialAccount.LoadSimulacao(sim);
            mInsuranceData.LoadSimulacao(sim);
            mClientData.LoadSimulacao(sim);
            mMultiTarificador.LoadSimulacao(sim);
        }
    }

    public void ResetSimulation()
    {
        mMultiTarificador.ResetData();
    }

    public void BlockProduct(bool block)
    {
        mMultiTarificador.BlockProduct(block);
    }
        
    public void SelectProduct(string ProductID)
    {
        mMultiTarificador.SelectedProductID = ProductID;
        SimID.Value = mMultiTarificador.SelectedQuoteId;
    }


    #endregion

    #region Validacoes

    public bool ValidarSimulacao()
    {

        string _msgErro = string.Empty;

        mInsuranceData.ValidarDados(ref _msgErro);

        mClientData.ValidarDados(ref _msgErro);

        if (!string.IsNullOrEmpty(mClientData.NIF))
        {
            if (BusinessHelper.IsValidNif(mClientData.NIF))
            {
                bool isNIF_ENT = BusinessHelper.IsEnterpriseNif(mClientData.NIF);
                string _ProtocoloTipoCliente;
                _ProtocoloTipoCliente = (new ProtocoloDS()).getTipoProtocolo(Convert.ToInt32(mMultiTarificador.ProtocoloID));

                if ((_ProtocoloTipoCliente == "E" && !BusinessHelper.IsEnterpriseNif(mClientData.NIF)) ||
                             (_ProtocoloTipoCliente == "P" && BusinessHelper.IsEnterpriseNif(mClientData.NIF)))
                {
                    _msgErro += "- O NIF colocado não é Válido para o tipo de protocolo selecionado.";
                }

            }
        }

        if (string.IsNullOrEmpty(_msgErro))
        {
            mMultiTarificador.Simulacao = new Simulacao()
            {
                Elaborador = mCreatedBy.Elaborador,
                Cobrador = mCommercialAccount.CollectorNumber,
                Mediador = mCommercialAccount.MediatorNumber,
                Contrato = new Contrato()
                {
                    DataInicio = mInsuranceData.DataInicio.HasValue ? mInsuranceData.DataInicio.Value.ToString("dd-MM-yyyy") : "",
                    Vencimento = mInsuranceData.Vencimento,
                    Tomador = mClientData.Tomador,
                    PessoaSeguraDataNasc = mClientData.PessoaSegura_DataNascimento.Value.ToString("dd-MM-yyyy"),
                    PlanID = GenericPlanID.HasValue ? GenericPlanID.Value.ToString() : null
                }
            };
        }
        else
        {
            ScriptHelper.ShowMessageBox(_msgErro, SIMULACAO, ScriptHelper.AlertType.ALERT);
        }

        return !string.IsNullOrEmpty(_msgErro);
    }

    #endregion

}



