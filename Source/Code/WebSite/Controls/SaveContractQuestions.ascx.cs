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
using System.ComponentModel;
using System.Text;
using Lusitania.Simuladores.DataLayer;

public partial class Controls_SaveContractQuestions : MyUserControl
{
    #region Properties

    public bool? CanPrintDocument
    {
        get
        {
            return ViewState["CanPrintDocument"] as bool?;
        }
        set
        {
            ViewState["CanPrintDocument"] = value;
        }
    }

    public bool CanPayLater
    {
        get
        {
            return GetFromViewState("CanPayLater", false);
        }
        set
        {
            ViewState["CanPayLater"] = value;
        }
    }

    public bool? WillPayNow
    {
        get { return ViewState["WillPayNow"] as bool?; }
        set { ViewState["WillPayNow"] = value; }
    }

    public string popupID
    {
        get { return mSaveContractQuestionsPanelPopup.ClientID; }
    }

    #endregion

    #region Page Events

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            mHideBtn_ShowContractDialog.Attributes.Add("open-popup", mSaveContractQuestionsPanelPopup.ClientID);

        }
        base.OnLoad(e);
    }

    #endregion

    #region Events

    public event EventHandler<CancelEventArgs> SavingOfProposalNeeded;
    protected virtual void OnSavingOfProposalNeeded(CancelEventArgs e)
    {
        if (SavingOfProposalNeeded != null)
        {
            SavingOfProposalNeeded(this, e);
        }
    }

    public event EventHandler<CancelEventArgs> SavingOfContractNeeded;
    protected virtual void OnSavingOfContractNeeded(CancelEventArgs e)
    {
        if (SavingOfContractNeeded != null)
        {
            SavingOfContractNeeded(this, e);
        }
    }

    public event EventHandler SavedContract;
    protected virtual void OnSavedContract()
    {
        if (SavedContract != null)
        {
            SavedContract(this, EventArgs.Empty);
        }
    }

    #endregion

    #region Methods

    public bool TrySaveProposal()
    {
        CancelEventArgs e = new CancelEventArgs(false);
        OnSavingOfProposalNeeded(e);
        return e.Cancel == false;
    }

    public bool TrySaveContract()
    {
        CancelEventArgs e = new CancelEventArgs(false);
        OnSavingOfContractNeeded(e);
        return e.Cancel == false;
    }

    public void Finish_Mensal_SDD()
    {
        WillPayNow = true;
        Finish();
    }

    public void Show()
    {
        //projecto de poder de cobrança
        int contractPermissions = BicicletasInterface.GetContractPermissionLevel(UserHelper.UserName);

        if (contractPermissions == 2 || contractPermissions == 1)
            mCancelQuestion2Button_Click(this, null);
        else
        {
            if (CanPrintDocument.GetValueOrDefault(false))
            {
                if (CanPayLater)
                {
                    mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mClientWilPayNowView);
                }
                else
                {
                    mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mContractWillBeStartedView);
                }
            }
            else
            {
                mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mConfirmSaveView);
            }
        }
        //mSaveContractQuestionsPanelPopup.Show();
        string script = "document.getElementById('" + mHideBtn_ShowContractDialog.ClientID + "').click();";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "click_hideBtn", script, true);
    }

    #endregion

    protected void mConfirmQuestion1Button_Click(object sender, EventArgs e)
    {

        if (WillPayNow.GetValueOrDefault(false))
        {
            mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mContractWillBeStartedView);
        }
        else
        {
            WillPayNow = false;
            Finish();
        }
    }

    protected void mCancelQuestion1Button_Click(object sender, EventArgs e)
    {
        mSaveContractQuestionsPanelPopup.Hide();
    }

    protected void mConfirmQuestion2Button_Click(object sender, EventArgs e)
    {
        WillPayNow = true;
        mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mContractWillBeStartedView);
    }

    protected void mCancelQuestion2Button_Click(object sender, EventArgs e)
    {
        WillPayNow = false;
        mSaveContractQuestionsMultiView.ActiveViewIndex = mSaveContractQuestionsMultiView.Views.IndexOf(mConfirmSaveView);
    }

    protected void mMessagePanel3ConfirmButton_Click(object sender, EventArgs e)
    {
        Finish();
    }

    private void Finish()
    {
        if (TrySaveProposal())
        {
            if (TrySaveContract())
            {
                OnSavedContract();
            }
        }

        mSaveContractQuestionsPanelPopup.Hide();
    }

    protected void mMessagePanel3CancelButton_Click(object sender, EventArgs e)
    {
        mSaveContractQuestionsPanelPopup.Hide();
    }

}
