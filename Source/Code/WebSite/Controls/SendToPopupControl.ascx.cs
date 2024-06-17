using System;
using System.Web.UI;
using System.Net.Mail;

public partial class Controls_SendToPopupControl : UserControl
{
    #region Events

    public event EventHandler Confirmed;
    protected virtual void OnConfirmed()
    {
        if (Confirmed != null)
        {
            Confirmed(this, EventArgs.Empty);
        }
    }

    #endregion

    #region Properties

    public string Subject
    {
        get { return mSendToControl.Subject; }
        set { mSendToControl.Subject = value; }
    }

    [IDReferenceProperty]
    public string TargetControlID
    {
        get { return mSendToPopup.TargetControlID; }
        set { mSendToPopup.TargetControlID = value; }
    }

    public Attachment Attachment
    {
        get { return mSendToControl.Attachment; }
        set { mSendToControl.Attachment = value; }
    }

    public string HeaderPanelSkinID
    {
        get { return mSendToControl.HeaderPanelSkinID; }
        set { mSendToControl.HeaderPanelSkinID = value; }
    }

    #endregion

    public void Show()
    {
        mSendToPopup.Show();
    }

    public void Send()
    {
        mSendToControl.Send();
    }

    protected void mOKButton_Click(object sender, EventArgs e)
    {
        OnConfirmed();
    }
}
