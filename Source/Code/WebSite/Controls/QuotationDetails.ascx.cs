using System;
using System.Web.UI;
using System.ComponentModel;
using Resources;

using Lusitania.Simulador.WebCommon.Common;

public partial class QuotationDetails : MyUserControl
{
    #region Properties

    public bool Enabled
    {
        get
        {
            return mNameBox.Enabled;
        }
        set
        {
            mNameBox.Enabled = value;
            mPhoneNumberBox.Enabled = value;
            mEmailBox.Enabled = value;
            mTaxNumberBox.Enabled = value;
        }
    }

    private bool allowCompanyNif = false;

    public bool AllowCompanyNif
    {
        get
        {
            return allowCompanyNif;
        }
        set
        {
            allowCompanyNif = value;
        }
    }

    public bool IsFilled
    {
        get
        {
            return !String.IsNullOrEmpty(mNameBox.Text) && !String.IsNullOrEmpty(mTaxNumberBox.Text) && !String.IsNullOrEmpty(mPhoneNumberBox.Text);
        }
    }

    protected string LastName
    {
        get
        {
            return GetFromViewState("LastName", String.Empty);
        }
        set
        {
            ViewState["LastName"] = value;
        }
    }

    protected string LastPhoneNumber
    {
        get
        {
            return GetFromViewState("LastPhoneNumber", String.Empty);
        }
        set
        {
            ViewState["LastPhoneNumber"] = value;
        }
    }

    protected string LastTaxNumber
    {
        get
        {
            return GetFromViewState("LastTaxNumber", String.Empty);
        }
        set
        {
            ViewState["LastTaxNumber"] = value;
        }
    }

    protected string LastEmail
    {
        get
        {
            return GetFromViewState("LastEmail", String.Empty);
        }
        set
        {
            ViewState["LastEmail"] = value;
        }
    }

    public bool IsDirty
    {
        get
        {
            return
                mNameBox.Text != LastName ||
                mPhoneNumberBox.Text != LastPhoneNumber ||
                mTaxNumberBox.Text != LastTaxNumber;
        }
    }

    [DefaultValue(false)]
    public bool ShowButtons
    {
        get { return (ViewState["ShowButtons"] as bool?).GetValueOrDefault(false); }
        set { ViewState["ShowButtons"] = value; }
    }

    

    [DefaultValue(null)]
    public string QuotePageUrl
    {
        get { return ViewState["QuotePageUrl"] as string; }
        set { ViewState["QuotePageUrl"] = value; }
    }

    [DefaultValue(null)]
    public string ProposalPageUrl
    {
        get { return ViewState["ProposalPageUrl"] as string; }
        set { ViewState["ProposalPageUrl"] = value; }
    }

    [DefaultValue(true)]
    public bool ShowProposalButton
    {
        get { return (ViewState["ShowProposalButton"] as bool?).GetValueOrDefault(true); }
        set { ViewState["ShowProposalButton"] = value; }
    }

    [DefaultValue(true)]
    public bool ShowSubmitButton
    {
        get { return (ViewState["ShowSubmitButton"] as bool?).GetValueOrDefault(true); }
        set { ViewState["ShowSubmitButton"] = value; }
    }

    [DefaultValue(true)]
    public bool ShowSimulateButton
    {
        get { return (ViewState["ShowSimulateButton"] as bool?).GetValueOrDefault(true); }
        set { ViewState["ShowSimulateButton"] = value; }
    }

    [DefaultValue(true)]
    public bool ShowPrintButton
    {
        get { return (ViewState["ShowPrintButton"] as bool?).GetValueOrDefault(true); }
        set { ViewState["ShowPrintButton"] = value; }
    }

    protected ScriptManager ScriptManager
    {
        get { return ScriptManager.GetCurrent(Page); }
    }

    #endregion

    #region ClientIDs

    public string SimulationNameClientID
    {
        get { return mNameBox.ClientID; }
    }

    public string SimulationPhoneNumberClientID
    {
        get { return mPhoneNumberBox.ClientID; }
    }

    public string SimulationNifClientID
    {
        get { return mTaxNumberBox.ClientID; }
    }

    public string SimulationEmailClientID
    {
        get { return mEmailBox.ClientID; }
    }

    public string ErrorNIFClientID
    {
        get { return mErrorNIFBox.ClientID; }
    }
    public string HasAlertSetClientID
    {
        get { return mHasAlertSet.ClientID; }
    }

    #endregion

    #region Bindable Fields

    public string SimulationName
    {
        get
        {
            return mNameBox.Text;
        }
        set
        {
            mNameBox.Text = value;
            LastName = value;
        }
    }

    public string SimulationPhoneNumber
    {
        get
        {
            return mPhoneNumberBox.Text;
        }
        set
        {
            mPhoneNumberBox.Text = value;
            LastPhoneNumber = value;
        }
    }

    public string SimulationNif
    {
        get
        {
            return mTaxNumberBox.Text;
        }
        set
        {
            mTaxNumberBox.Text = value;
            LastTaxNumber = value;
        }
    }

    public string SimulationEmail
    {
        get
        {
            return mEmailBox.Text;
        }
        set
        {
            mEmailBox.Text = value;
            LastEmail = value;
        }
    }

    #endregion

    #region Events

    private event EventHandler<CancelEventArgs> Printing;

    #endregion

    #region Event Triggers

    /// <summary>
    /// Raises the Printing Event.
    /// </summary>
    /// <param name="iArgs">The arguments to be passed to each listener.</param>
    protected virtual void OnPrinting(CancelEventArgs iArgs)
    {
        if (Printing != null)
        {
            Printing(this, iArgs);
        }
    }



    #endregion

    #region Methods

    public void ClearDirty()
    {
        LastName = mNameBox.Text;
        LastPhoneNumber = mPhoneNumberBox.Text;
        LastTaxNumber = mTaxNumberBox.Text;
    }

    protected override void OnPreRender(EventArgs e)
    {
       /* mRunSimulatorButton.Visible = ShowSimulateButton;

        mPrintButton.Visible = ShowPrintButton;
        mPrintButton.Enabled = !String.IsNullOrEmpty(QuotePageUrl);

        mProposalButton.Visible = ShowProposalButton;
        mProposalButton.Enabled = !String.IsNullOrEmpty(ProposalPageUrl);

        mSubmitButton.Visible = ShowSubmitButton;
        mSubmitButton.Enabled = !String.IsNullOrEmpty(QuotePageUrl);

        mButtonPanel.Visible = ShowButtons;*/

        base.OnPreRender(e);
    }


    #endregion



    protected void mPrintButton_Click(object sender, EventArgs e)
    {
        ScriptHelper.ShowWindow(QuotePageUrl);
    }
    protected void mProposalButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(ProposalPageUrl);
    }

    public bool? HasValidNif()
    {
        bool? isValid = null;

        if (!String.IsNullOrEmpty(mTaxNumberBox.Text))
        {
            if (BusinessHelper.IsValidNif(mTaxNumberBox.Text))
            {
                if (BusinessHelper.IsEnterpriseNif(mTaxNumberBox.Text) && !AllowCompanyNif)
                {
                    isValid = false;
                }
                else
                {
                    isValid = true;
                }
            }
        }

        return isValid;
    }
}
