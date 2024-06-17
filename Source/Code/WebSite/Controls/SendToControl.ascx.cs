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
using System.Net.Mail;
using Resources;
using System.Text;
using System.IO;
using Lusitania.Simulador.WebCommon.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class SendToControl : UserControl
    {
        #region Constants

        private const string cValidationGroupVSK = "ValidationGroup";

        #endregion

        #region Properties

        public string HeaderPanelSkinID
        {
            get { return mHeaderPanel.SkinID; }
            set { mHeaderPanel.SkinID = value; }
        }

        public string ValidationGroup
        {
            get
            {
                return (string)ViewState[cValidationGroupVSK];
            }
            set
            {
                ViewState[cValidationGroupVSK] = value;
            }
        }

        private Attachment mAttachment;
        public Attachment Attachment
        {
            get { return mAttachment; }
            set { mAttachment = value; }
        }

        public string MessageText
        {
            get { return MessageBox.Text; }
            set { MessageBox.Text = value; }
        }

        public string Subject
        {
            get { return ViewState["Subject"] as string; }
            set { ViewState["Subject"] = value; }
        }

        #endregion

        #region LifeCycle

        protected override void OnPreRender(EventArgs e)
        {
            // set the correct validation group
            WebUtility.SetValidationGroups(mMainPanel, ValidationGroup);

            base.OnPreRender(e);
        }

        #endregion

        #region Methods

        public void Send()
        {

            MailMessage xMessage = new MailMessage();

            xMessage.From = new MailAddress("noreply@lusitania.pt", "Lusitania Seguros");

            xMessage.To.Add(new MailAddress(ToEMailBox.Text, ToNameBox.Text));

            if (!String.IsNullOrEmpty(UserHelper.UserName))
                xMessage.CC.Add(new MailAddress(UserHelper.UserEMail, UserHelper.UserDisplayName));
            else
                xMessage.CC.Add(new MailAddress(FromEMailBox.Text, FromNameBox.Text));

            xMessage.Subject = Subject;
            xMessage.Body = Strings.SendQuotation_Body_FormatString
                .Replace("###TO###", ToNameBox.Text)
                .Replace("###FROM-NAME###", FromNameBox.Text)
                .Replace("###FROM-EMAIL###", FromEMailBox.Text)
                .Replace("###MESSAGE###", MessageBox.Text);

            if (mAttachment != null)
            {
                xMessage.Attachments.Add(mAttachment);
            }

            SmtpClient xClient = new SmtpClient(ClComum.obterSmtpServer());
            xClient.Send(xMessage);
        }

        #endregion
    }
}