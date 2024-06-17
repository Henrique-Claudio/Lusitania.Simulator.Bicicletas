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

using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.Common;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;

public partial class CommercialAccount : UserControl
{

    #region Properties

    /// <summary>
    /// Gets or sets the Header Panel's SkinID.
    /// </summary>

    public bool isVizible {
        get { return conCommercialAccount.Visible; }
    }
    public string MediatorNumber
    {
        get { return mMediatorNumberBox.Text; }
        set
        {
            if (mMediatorNumberBox.Text != value)
            {
                mMediatorNumberBox.Text = value;
                UpdateMediator();
            }
        }
    }

    public string CollectorNumber
    {
        get { return mCollectorNumberBox.Text; }
        set
        {
            if (mCollectorNumberBox.Text != value)
            {
                mCollectorNumberBox.Text = value;
                UpdateCollector();
            }
        }
    }

    public decimal? GenericPlanID
    {
        get { return ViewState["GenericPlanID"] as decimal?; }
        set { ViewState["GenericPlanID"] = value; }
    }
    
    public decimal? SimulationID
    {
        get { return ViewState["SimulationID"] as decimal?; }
        set { ViewState["SimulationID"] = value; }
    }

    public bool IsProposalPage
    {
        get { return ViewState["IsProposalPage"] != null ? (bool)ViewState["IsProposalPage"] : false; }
        set { ViewState["IsProposalPage"] = value; }
    }

    #endregion

    #region Page events

    protected override void OnLoad(EventArgs e)
    {


        if (!IsPostBack)
        {
            string username = string.Empty;

            // Para o caso de querer personificar outro user (e.g.: Sim Viagem)
            if (Session["SuperLogin"] != null && Session["SuperLogin"].ToString() != String.Empty)
            {
                username = Session["SuperLogin"].ToString();
            }
            else
            {
                username = UserHelper.UserName;
            }


            if (GenericPlanID.HasValue)
            {
                //obter mediator e collector
                string mediator = string.Empty;
                string collector = string.Empty;
                DataHelper.PreSelectCommercialAccount(GenericPlanID, ref mediator, ref collector);

                MediatorNumber = mediator;
                CollectorNumber = collector;
            }


            conCommercialAccount.Visible = !String.IsNullOrEmpty(UserHelper.UserName) && DataHelper.CanLogAsAgent(UserHelper.UserName)
                                        && !GenericPlanID.HasValue;

            if (!conCommercialAccount.Visible )
            {
                MediatorNumber = CollectorNumber = string.Empty;    
            }
            


            if (Functions.IsProfileDefined(username, Profiles.GestorRede))
            {
                mCollectorNumberBox.Text = DataHelper.getDependencyInternalAccount(username);
                UpdateCollector();

                mCollectorNumberBox.ReadOnly = true;
            }
            else if (Functions.IsProfileDefined(username, Profiles.ContactCenter))
            {
                if (!GlobalInterface.ObtemCentroCusto(username).Equals("0165"))
                {
                    mCollectorNumberBox.Text = "09500";
                    mMediatorNumberBox.Text = "09500";

                    UpdateCollector();
                    UpdateMediator();

                    mCollectorNumberBox.ReadOnly = true;
                    mMediatorNumberBox.ReadOnly = true;
                }
            }
            else if (Functions.IsProfileDefined(username, Profiles.CanalDirecto))
            {
                mCollectorNumberBox.Text = "05900";
                mMediatorNumberBox.Text = "05900";

                UpdateCollector();
                UpdateMediator();

                mCollectorNumberBox.ReadOnly = true;
                mMediatorNumberBox.ReadOnly = true;
            }

        }

        base.OnLoad(e);
    }

    #endregion

    #region Events

    protected void mMediatorDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            string xName = TryConvert.ToString(e.OutputParameters["A_NOME_MEDIADOR"]);
            string xErrorMessage = TryConvert.ToString(e.OutputParameters["A_MSG_ERRO"]);

            if (String.IsNullOrEmpty(xErrorMessage))
            {
                mMediatorNameBox.Text = xName;
            }
            else
            {
                ScriptHelper.ShowMessageBox(xErrorMessage, "Simulação", ScriptHelper.AlertType.ERROR);
            }
        }
    }

    protected void mCollectorDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            string xName = TryConvert.ToString(e.OutputParameters["A_NOME_COBRADOR"]);
            string xErrorMessage = TryConvert.ToString(e.OutputParameters["A_MSG_ERRO"]);

            if (String.IsNullOrEmpty(xErrorMessage))
            {
                mCollectorNameBox.Text = xName;
            }
            else
            {
                ScriptHelper.ShowMessageBox(xErrorMessage, "Simulação", ScriptHelper.AlertType.ERROR);
            }
        }
    }

    protected void mMediatorNumberBox_TextChanged(object sender, EventArgs e)
    {
        UpdateMediator();
        if (!IsProposalPage)
        {
            ResetSimulation();
        }
        else
        {
            ResetProposta();
        } 
    }

    protected void mCollectorNumberBox_TextChanged(object sender, EventArgs e)
    {
        UpdateCollector();
        if (!IsProposalPage)
        {
            ResetSimulation();
        }
        else
        {
            ResetProposta();
        } 
        
    }

    #endregion

    #region Helper Methods
    
    private void UpdateMediator()
    {
        if (String.IsNullOrEmpty(mMediatorNumberBox.Text))
        {
            mMediatorNameBox.Text = String.Empty;
        }
        else
        {
            string xErrorMessage;
            mMediatorNameBox.Text = DataHelper.GetMediatorName(mMediatorNumberBox.Text, out xErrorMessage);


            if (!String.IsNullOrEmpty(xErrorMessage))
            {
                ScriptHelper.ShowMessageBox(xErrorMessage, !IsProposalPage ? "Simulação Bicletas" : "Proposta Bicletas", ScriptHelper.AlertType.ERROR);
                mMediatorNumberBox.Text = String.Empty;
            }
        }
    }
    
    private void UpdateCollector()
    {
        if (String.IsNullOrEmpty(mCollectorNumberBox.Text))
        {
            mCollectorNameBox.Text = String.Empty;
        }
        else
        {
            string xErrorMessage;
            mCollectorNameBox.Text = DataHelper.GetCollectorName(mCollectorNumberBox.Text, UserHelper.UserName, out xErrorMessage);


            if (!String.IsNullOrEmpty(xErrorMessage))
            {
                ScriptHelper.ShowMessageBox(xErrorMessage, !IsProposalPage ? "Simulação Bicletas" : "Proposta Bicletas", ScriptHelper.AlertType.ERROR);
                mCollectorNumberBox.Text = String.Empty;
            }
        }
    }

    public void LoadSimulacao(Simulacao sim)
    {

        if (!string.IsNullOrEmpty(sim.Cobrador))
        {
            mCollectorNumberBox.Text = sim.Cobrador;
            UpdateCollector();
        }


        if (!string.IsNullOrEmpty(sim.Cobrador))
        {
            mMediatorNumberBox.Text = sim.Mediador;
            UpdateMediator();
        }
    }

    private void ResetSimulation()
    {
        this.Page.GetType().InvokeMember("ResetSimulation", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
    }

    private void ResetProposta() {
        this.Page.GetType().InvokeMember("ResetProposta", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
    }

    public void ValidarDados(ref string msgErro)
    {
        /*if (string.IsNullOrEmpty(CollectorNumber))
        {
            BusinessHelper.AddErroAtList(ref msgErro, "Indique o Número de Conta do Cobrador."); 
        }
        if (string.IsNullOrEmpty(MediatorNumber))
        {
           BusinessHelper.AddErroAtList(ref msgErro, "Indique o Número de Conta do Mediador.");
        }*/

    }
    #endregion


}