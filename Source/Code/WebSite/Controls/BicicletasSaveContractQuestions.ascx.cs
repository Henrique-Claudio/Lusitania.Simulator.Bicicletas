using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

using Resources;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer.Contract;
using Lusitania.Simuladores.DataLayer.Common;

public partial class BicicletasSaveContractQuestions : UserControl
{
    
    #region Properties

    private ContractDS _contratoDS = new ContractDS();

    public decimal? SimulationID
    {
        get { return ViewState["SimulationID"] as decimal?; }
        set { ViewState["SimulationID"] = value; }
    }

    public string ApoliceID
    {
        get { return ViewState["ApoliceID"] as string; }
        set { ViewState["ApoliceID"] = value; }
    }

    public decimal? EmissionCode
    {
        get { return ViewState["EmissionCode"] as decimal?; }
        set { ViewState["EmissionCode"] = value; }
    }

    public string PaymentType
    {
        get { return ViewState["PaymentType"] as string; }
        set { ViewState["PaymentType"] = value; }
    }

    public string Apolice
    {
        get { return ViewState["Apolice"] as string; }
        set { ViewState["Apolice"] = value; }
    }

    public string Recibo
    {
        get { return ViewState["Recibo"] as string; }
        set { ViewState["Recibo"] = value; }
    }

    public string popupID {
        get { return mSaveContractQuestions.popupID; }
    }

    #endregion

    #region Methods

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        mSaveContractQuestions.SavingOfContractNeeded += new EventHandler<CancelEventArgs>(mSaveContractQuestions_SavingOfContractNeeded);
        mSaveContractQuestions.SavedContract += new EventHandler(mSaveContractQuestions_SavedContract);
    }

    public void Show()
    {

        EmissionCode = BusinessHelper.CanEmitDocuments(UserHelper.UserName, UtilityG.SIMULATOR_ID, SimulationID.Value) ? 1 : 0;

        mSaveContractQuestions.CanPrintDocument = EmissionCode == 1;
        mSaveContractQuestions.CanPayLater = BusinessHelper.CanPayLaterAnContract(UtilityG.SIMULATOR_ID, SimulationID.Value, "00000");
        
        mSaveContractQuestions.Show();       
    }

    
    #endregion

    #region Events

    public event EventHandler Success;
    protected virtual void OnSuccess()
    {
        if (Success != null)
        {
            Success(this, EventArgs.Empty);
        }
    }

    #endregion

    private void mSaveContractQuestions_SavingOfContractNeeded(object sender, CancelEventArgs e)
    {
        string _apolice;
        string _recibo;
        string _msgErro;
        int _emitDoc = mSaveContractQuestions.WillPayNow.GetValueOrDefault(true) ? 1 : 0;
        
        _contratoDS.Gravar(SimulationID.Value, UserHelper.UserName, _emitDoc, out _apolice, out _recibo, out _msgErro);

        if (string.IsNullOrEmpty(_msgErro))
        {
            Apolice = _apolice;
            Recibo = _recibo;

            if (_emitDoc == 0)
            {
                //IMPRIMIR AVISO
                ScriptHelper.ShowWindow("Pages/OutputES_PDF.aspx?templateName=avisos_recibo.xdp&recordName=AVISOS_RECIBO&ID=" + SimulationID.Value + "&Ramo="+UtilityG.RMS.Substring(0,2)+"&Apolice=" + Apolice + "&Recibo=" + Recibo + "&docType=130");
            }
        }
        else
        {
            ScriptHelper.ShowMessageBox(_msgErro, "Contrato", ScriptHelper.AlertType.ERROR);
            e.Cancel = true;
        }
        

        
    }

    private void mSaveContractQuestions_SavedContract(object sender, EventArgs e)
    {
		string _msg = "O Contrato foi emitido com sucesso. <br />";
        _msg += "Número de apólice: "+ Apolice + " <br/>";
        _msg += "Número de recibo: " + Recibo + " <br/>"; 


        ScriptHelper.ShowMessageBox(_msg, "Contrato", ScriptHelper.AlertType.INFO);
        OnSuccess();
    }    
}
