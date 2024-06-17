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
using Resources;
using System.Net.Mail;
using Lusitania.Simulador.WebCommon.Common;

public partial class Controls_UserSuggestionsControl : UserControl
{
    #region Properties

    public string SimulatorName
    {
        get { return ViewState["SimulatorName"] as string; }
        set { ViewState["SimulatorName"] = value; }
    }

    public string ValidationGroup
    {
        get { return ViewState["ValidationGroup"] as string; }
        set { ViewState["ValidationGroup"] = value; }
    }

    #endregion

    #region Methods

    protected override void OnPreRender(EventArgs e)
    {
        Visible = !String.IsNullOrEmpty(UserHelper.UserName);

        UpdateTitleText();
        UpdateValidationGroups();
        UpdateDefaultEmail();

        base.OnPreRender(e);
    }

    private void UpdateDefaultEmail()
    {
        if (!IsPostBack)
        {
            mNameBox.Text = UserHelper.UserDisplayName;
            mEmailBox.Text = UserHelper.UserEMail;
        }
    }

    private void UpdateValidationGroups()
    {
        WebUtility.SetValidationGroups(this, ValidationGroup);
    }

    private void UpdateTitleText()
    {
        mTopicLabel.Text = String.Format(Strings.SendSuggestion_Title_FormatString, SimulatorName);
    }

    private void SendEmail()
    {
        Page.Validate(ValidationGroup);
        if (Page.IsValid)
        {
            SmtpClient xClient = new SmtpClient(); //ClComum.obterSmtpServer());

            MailMessage xMessage = new MailMessage(
                new MailAddress(mEmailBox.Text, mNameBox.Text),
                new MailAddress(Settings.UserSuggestions_DestinationEmail)
            );
            xMessage.Subject = String.Format(Strings.SendSuggestion_Subject_FormatString, SimulatorName);
            xMessage.Body = mSubjectBox.Text;

            xClient.Send(xMessage);

            ScriptHelper.ShowMessageBox(Strings.SendSuggestion_Success);
        }
    }

    #endregion

    protected void mSendButton_Click(object sender, EventArgs e)
    {
        SendEmail();
    }
}
