using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lusitania.Utilities;

/// <summary>
/// Holds static functions for various purposes.
/// </summary>
public static class WebUtility
{
    public static T[] FindControls<T>(Control iParent) where T : Control
    {
        List<T> xList = new List<T>();

        FindControlsCore(iParent, xList);

        return xList.ToArray();
    }

    private static void FindControlsCore<T>(Control iParent, IList<T> iList) where T : Control
    {
        foreach (Control xControl in iParent.Controls)
        {
            T xTControl = xControl as T;
            if (xTControl != null)
            {
                iList.Add(xTControl);
            }

            FindControlsCore(xControl, iList);
        }
    }

    /// <summary>
    /// Finds the first control with a given id inside a given control hierarchy.
    /// </summary>
    /// <param name="iID">The ID to search for.</param>
    /// <param name="iParent">The parent from where to start the search.</param>
    /// <returns>The control, if found, or null.</returns>
    public static Control FindControl(string id, Control parent)
    {
        if (parent == null) throw new ArgumentNullException("parent");

        foreach (Control xControl in parent.Controls)
        {
            if (xControl.ID == id)
            {
                return xControl;
            }
            else
            {
                Control xChild = FindControl(id, xControl);
                if (xChild != null)
                {
                    return xChild;
                }
            }
        }

        return null;
    }

    public static T FindControl<T>(string id, Control parent) where T : Control
    {
        if (parent == null) throw new ArgumentNullException("parent");

        foreach (Control xControl in parent.Controls)
        {
            T xTypedControl = xControl as T;
            if (xTypedControl != null && xTypedControl.ID == id)
            {
                if (xTypedControl.ID == id)
                {
                    return xTypedControl;
                }
            }
            else
            {
                T xChild = FindControl<T>(id, xControl);
                if (xChild != null)
                {
                    return xChild;
                }
            }
        }

        return null;
    }

    public static void SetValidatorsEnabled(Control parent, bool enabled)
    {
        foreach (BaseValidator xValidator in FindControls<BaseValidator>(parent))
        {
            xValidator.Enabled = enabled;
        }
    }

    public static void SetValidationGroups(Control parent, string validationGroup)
    {
        foreach (Control xChild in parent.Controls)
        {
            BaseValidator xValidator = xChild as BaseValidator;
            if (xValidator != null)
            {
                xValidator.ValidationGroup = validationGroup;
            }

            CustomValidator xCustom = xChild as CustomValidator;
            if (xCustom != null)
            {
                xCustom.ValidationGroup = validationGroup;
            }

            ValidationSummary xSummary = xChild as ValidationSummary;
            if (xSummary != null)
            {
                xSummary.ValidationGroup = validationGroup;
            }

            IButtonControl xButton = xChild as IButtonControl;
            if (xButton != null)
            {
                xButton.ValidationGroup = validationGroup;
            }

            SetValidationGroups(xChild, validationGroup);
        }
    }

    public static string BuildUrl(HttpRequest request, string tildedUrl, string queryFormatString, params object[] queryParams)
    {
        UriBuilder xUrl = new UriBuilder();
        xUrl.Scheme = request.Url.Scheme;
        xUrl.Host = request.Url.Host;
        xUrl.Port = request.Url.Port;
        xUrl.Path = VirtualPathUtility.ToAbsolute(tildedUrl, request.ApplicationPath);

        if(!String.IsNullOrEmpty(queryFormatString))
            xUrl.Query = String.Format(queryFormatString, queryParams);

        return xUrl.ToString();
    }

    public static string BuildUrl(HttpRequest request, string tildedUrl)
    {
        return BuildUrl(request, tildedUrl, null);
    }

    public static string BuildUrl(string tildedUrl, string queryFormatString, params object[] queryParams)
    {
        return BuildUrl(HttpContext.Current.Request, tildedUrl, queryFormatString, queryParams);
    }

    public static Stream GetResponseStream(string url)
    {
        WebRequest xRequest = WebRequest.Create(url);
        xRequest.Credentials = CredentialCache.DefaultNetworkCredentials;

        return xRequest.GetResponse().GetResponseStream();
    }

    public static void ClearRepeatedCells(GridView iGridView, int iCellIndex)
    {
        if (iGridView.Rows.Count > 1)
        {
            string xLastText = iGridView.Rows[0].Cells[iCellIndex].Text;
            for (int xRowIndex = 1; xRowIndex < iGridView.Rows.Count; ++xRowIndex)
            {
                TableCell xCell = iGridView.Rows[xRowIndex].Cells[iCellIndex];
                if (xCell.Text == xLastText)
                {
                    xCell.Text = String.Empty;
                }
                else
                {
                    xLastText = xCell.Text;
                }
            }
        }
    }

    public static void DownloadToResponse(string iTarget, HttpResponse iResponse, NetworkCredential iCredential)
    {
        DownloadToResponse(new Uri(iTarget), iResponse, iCredential);
    }

    public static void DownloadToResponse(Uri iTarget, HttpResponse iResponse, NetworkCredential iCredential)
    {
        WebRequest xRequest = WebRequest.Create(iTarget);
        WebResponse xResponse = xRequest.GetResponse();
        Stream xResponseStream = xResponse.GetResponseStream();

        byte[] xBuffer = new byte[1024];
        int xBytesRead;
        while ((xBytesRead = xResponseStream.Read(xBuffer, 0, xBuffer.Length)) > 0)
        {
            iResponse.OutputStream.Write(xBuffer, 0, xBytesRead);
        }

        iResponse.End();
    }

    public static void HookChangeEventOnAllControls(Control iParent, EventHandler iHandler)
    {
        foreach (Control xControl in FindControls<Control>(iParent))
        {
            TextBox xTextBox = xControl as TextBox;
            if (xTextBox != null)
            {
                xTextBox.TextChanged += iHandler;
                continue;
            }
            
            CheckBox xCheckBox = xControl as CheckBox;
            if (xCheckBox != null)
            {
                xCheckBox.CheckedChanged += iHandler;
                continue;
            }

            CheckBoxList xCheckBoxList = xControl as CheckBoxList;
            if (xCheckBoxList != null)
            {
                xCheckBoxList.SelectedIndexChanged += iHandler;
                continue;
            }

            RadioButton xRadioButton = xControl as RadioButton;
            if (xRadioButton != null)
            {
                xRadioButton.CheckedChanged += iHandler;
                continue;
            }

            RadioButtonList xRadioButtonList = xControl as RadioButtonList;
            if (xRadioButtonList != null)
            {
                xRadioButtonList.SelectedIndexChanged += iHandler;
                continue;
            }

            DropDownList xDropDown = xControl as DropDownList;
            if (xDropDown != null)
            {
                xDropDown.SelectedIndexChanged += iHandler;
                continue;
            }

            GridView xGridView = xControl as GridView;
            if (xGridView != null)
            {
                xGridView.RowCommand += new GridViewCommandEventHandler(delegate(object pSender, GridViewCommandEventArgs pArgs) { iHandler(pSender, pArgs); });
                continue;
            }
        }
    }


    public static void HookAsyncPostBackTriggerOnAllControls(Control iParent, UpdatePanel iUpdatePanel)
    {
        Control[] xTargets = FindControls<Control>(iParent);
        Control[] xUpdatePanelChildren = FindControls<Control>(iUpdatePanel);

        foreach (Control xControl in xTargets)
        {
            // ignore the update panel's children
            if (Array.IndexOf<Control>(xUpdatePanelChildren, xControl) >= 0)
            {
                continue;
            }

            TextBox xTextBox = xControl as TextBox;
            if (xTextBox != null)
            {
                xTextBox.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xTextBox.UniqueID;
                xTrigger.EventName = "TextChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }

            CheckBoxList xCheckBoxList = xControl as CheckBoxList;
            if (xCheckBoxList != null)
            {
                xCheckBoxList.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xCheckBoxList.UniqueID;
                xTrigger.EventName = "SelectedIndexChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }

            CheckBox xCheckBox = xControl as CheckBox;
            if (xCheckBox != null && !(xCheckBox.Parent is CheckBoxList))
            {
                xCheckBox.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xCheckBox.UniqueID;
                xTrigger.EventName = "CheckedChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }

            RadioButtonList xRadioButtonList = xControl as RadioButtonList;
            if (xRadioButtonList != null)
            {
                xRadioButtonList.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xRadioButtonList.UniqueID;
                xTrigger.EventName = "SelectedIndexChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }

            RadioButton xRadioButton = xControl as RadioButton;
            if (xRadioButton != null && !(xRadioButton.Parent is RadioButtonList))
            {
                //xRadioButton.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xRadioButton.UniqueID;
                xTrigger.EventName = "CheckedChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }

            DropDownList xDropDown = xControl as DropDownList;
            if (xDropDown != null)
            {
                xDropDown.AutoPostBack = true;

                AsyncPostBackTrigger xTrigger = new AsyncPostBackTrigger();
                xTrigger.ControlID = xDropDown.UniqueID;
                xTrigger.EventName = "SelectedIndexChanged";
                iUpdatePanel.Triggers.Add(xTrigger);

                continue;
            }
        }
    }


    public static void InsertUpdatePanelAsParent(Control iTarget, UpdatePanelUpdateMode iUpdateMode, UpdatePanelRenderMode iRenderMode)
    {
        UpdatePanel xNewUpdatePanel = new UpdatePanel();
        xNewUpdatePanel.ID = iTarget.UniqueID + "UpdatePanel";
        xNewUpdatePanel.UpdateMode = iUpdateMode;
        xNewUpdatePanel.RenderMode = iRenderMode;

        iTarget.Parent.Controls.Add(xNewUpdatePanel);
        iTarget.Parent.Controls.Remove(iTarget);
        xNewUpdatePanel.ContentTemplateContainer.Controls.Add(iTarget);
    }

    public static string GetEncodedString(string data)
    {
        if (!string.IsNullOrEmpty(data))
            return System.Web.HttpUtility.HtmlEncode(data);

        return null;
    }

    /// <summary>
    /// Disables the control if its contents arent filled with data    
    /// </summary>
    /// <param name="control">Textbox asp control</param>
    public static void DisableTextBoxIfNotFilled(TextBox control)
    {
        if (string.IsNullOrEmpty(control.Text))
        {
            control.Enabled = true;
        }
        else
        {
            control.Enabled = false;
        }
    }
}
