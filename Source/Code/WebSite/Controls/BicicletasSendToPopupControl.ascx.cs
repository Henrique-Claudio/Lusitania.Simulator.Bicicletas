using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Resources;
using System.Net.Mail;
using Lusitania.Simuladores.WebSite.Classes;

public partial class BicicletasSendToPopupControl : System.Web.UI.UserControl
{



    #region Properties

    public string SimulationID
    {
        get { return ViewState["SimulationID"].ToString(); }
        set { ViewState["SimulationID"] = value; }
    }

    public bool HasSDD
    {
        get { return  ViewState["HasSDD"] == null ? false : (bool)ViewState["HasSDD"]; }
        set { ViewState["HasSDD"] = value; }
    }

    #endregion

    #region Methods
    private void mSendToPopupControl_Confirmed(object sender, EventArgs e)
    {
        List<string> parametrosO = new List<string>();
        parametrosO.Add(SimulationID.ToString());
        if (HasSDD)
            parametrosO.Add("S");
        else
            parametrosO.Add("N");

        string recordSet = Strings.RecordSetQuotation;
        mSendToPopupControl.Attachment = //new Attachment(WebUtility.GetResponseStream(WebUtility.BuildUrl(Request, ResolveUrl(PageLinks.BicicletasQuotation), String.Format("templateName={0}&recordName={1}&ID={2}&docType={3}", "cotacao_rc.xdp", "BIKEINSURANCEQUOTATION", SimulationID, "7"))), Strings.BicicletasSimulator_QuotationAttachmentNameForPdf);
                                         new Attachment(OutputsManagement.GetPDFStream(recordSet, parametrosO, Strings.XDPQuotation, ConfigurationManager.AppSettings["OutputES.OutputServiceDir"]), Strings.BicicletasSimulator_QuotationAttachmentNameForPdf);
        mSendToPopupControl.Send();

        if (!string.IsNullOrEmpty(Strings.QuotationSentSuccessfully))
        {
            ScriptHelper.ShowMessageBox(Strings.QuotationSentSuccessfully, "Quotação da Simulação", ScriptHelper.AlertType.INFO);
        }



    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        mSendToPopupControl.Confirmed += new EventHandler(mSendToPopupControl_Confirmed);
    }

    public void Show()
    {
        mSendToPopupControl.Show();
    }

    #endregion
}
