<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Controls_SendToPopupControl" Codebehind="SendToPopupControl.ascx.cs" %>
<%@ Register TagPrefix="app" TagName="SendToControl" Src="~/controls/sendtocontrol.ascx" %>
<asp:HyperLink ID="mSendToTarget" runat="Server" Style="display: none"></asp:HyperLink>
<asp:Panel ID="mSendToPanel" runat="server" SkinID="Popupv6" DefaultButton="mOKButton" Style="display: none">
    <table>
        <tr>
            <td>
                <app:SendToControl ID="mSendToControl" runat="server" ValidationGroup="SendMail" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="mOKButton" runat="server" Text="Enviar" SkinID="ButtonSimular" ValidationGroup="SendMail" OnClick="mOKButton_Click" /></td>
                        <td>
                            <asp:Button ID="mCancelButton" runat="Server" Text="Fechar" SkinID="ButtonSimular" CausesValidation="False" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
<ajax:ModalPopupExtender ID="mSendToPopup" runat="server" TargetControlID="mSendToTarget"
    PopupControlID="mSendToPanel" CancelControlID="mCancelButton">
</ajax:ModalPopupExtender>
