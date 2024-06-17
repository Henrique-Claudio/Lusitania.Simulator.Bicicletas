<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_UserSuggestionsControl" Codebehind="UserSuggestionsControl.ascx.cs" %>
<asp:Panel ID="mMainPanel" runat="server">
    <ajax:UpdatePanel ID="mMainUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:LinkButton ID="mPopupButton" runat="server" Text="[Dúvidas ou Sugestões]" CausesValidation="false" style="position:absolute; top:-15px; left:0; display:none;" />
            <ajax:ModalPopupExtender ID="mPopupExtender" runat="server" TargetControlID="mPopupButton" PopupControlID="mPopupPanel" DropShadow="true" CancelControlID="mCloseButton">
            </ajax:ModalPopupExtender>
            <asp:Panel ID="mPopupPanel" runat="server" DefaultButton="mSendButton" SkinID="Popup">
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="mTopicLabel" runat="server" Width="300" SkinID="Heavy"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="mNamePanel" runat="server" GroupingText="Nome:">
                                <asp:TextBox ID="mNameBox" runat="Server" MaxLength="100" Width="300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mNameBoxRFV" runat="server" ControlToValidate="mNameBox" Display="None" SetFocusOnError="true" ErrorMessage="Preencha o seu Nome."></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="mEmailPanel" runat="server" GroupingText="E-Mail:">
                                <asp:TextBox ID="mEmailBox" runat="Server" MaxLength="100" Width="300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mEmailRFV" runat="server" ControlToValidate="mEmailBox" Display="None" SetFocusOnError="true" ErrorMessage="Preencha o seu E-Mail."></asp:RequiredFieldValidator>
                                <app:EmailValidator ID="mEmailBoxEV" runat="server" ControlToValidate="mEmailBox" Display="None" SetFocusOnError="true" ErrorMessage="O seu E-Mail não é válido."></app:EmailValidator>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="mSubjectPanel" runat="Server" GroupingText="Assunto:">
                                <asp:TextBox ID="mSubjectBox" runat="Server" TextMode="MultiLine" MaxLength="1000" Width="300" Rows="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="mSubjectBoxRFV" runat="server" ControlToValidate="mSubjectBox" Display="None" SetFocusOnError="true" ErrorMessage="Preencha o Assunto."></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="mSendButton" runat="server" Text="Enviar" OnClick="mSendButton_Click" />
                            <asp:Button ID="mCloseButton" runat="server" Text="Fechar" CausesValidation="false" />
                            <asp:ValidationSummary ID="mValidationSummary" runat="server" ShowMessageBox="true" ShowSummary="false" EnableClientScript="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Panel>
