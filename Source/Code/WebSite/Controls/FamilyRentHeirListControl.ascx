<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="FamilyRentHeirListControl" Codebehind="FamilyRentHeirListControl.ascx.cs" %>
<%@ Register Src="~/Controls/FamilyRentHeirPersonalInfo.ascx" TagPrefix="app" TagName="HeirPersonalInfo" %>
<div>
    <ajax:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <ajax:ScriptReference Path="~/Script/Common.js" />
        </Scripts>
    </ajax:ScriptManagerProxy>
    <asp:HiddenField ID="mUserNameField" runat="server" Visible="False" />
    <asp:HiddenField ID="mValidationGroupField" runat="Server" Visible="false" />
    <asp:ValidationSummary ID="HeirValidationSummary" runat="Server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="HeirPersonalInfo" />
    <ajax:UpdatePanel ID="ItemsGridViewUpdatePanel" runat="server" UpdateMode="Conditional">
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="SaveHeirButton" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:GridView ID="ItemsGridView" runat="server" ShowFooter="true" AutoGenerateColumns="false"
                SkinID="Clean" OnRowCommand="ItemsGridView_RowCommand" DataKeyNames="ID">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="mUserImage" runat="Server" SkinID="User" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="N&#186;">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="Server" Text='<%# Eval("Number") %>'></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nome">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parentesco">
                        <ItemTemplate>
                            <asp:DropDownList ID="KinshipBox" runat="server" Width="200px" SelectedValue='<%# Eval("KinshipID") %>'
                                DataTextField="DESCRITIVO" DataValueField="CODIGO" DataSource='<%# QueryKinshipList() %>'>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="KinshipBoxV1" runat="server" ControlToValidate="KinshipBox"
                                ErrorMessage='<%# Eval("Number", "Indique o Parentesco do Herdeiro Nº {0}.") %>'
                                Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="RemoveButton" runat="server" ToolTip="Remover" CommandName="RemoveItem"
                                CausesValidation="false" CommandArgument='<%# Eval("Number") %>' SkinID="RemoveItem" /></ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </ajax:UpdatePanel>
    <asp:Image ID="mImage2387" runat="server" SkinID="User" />
    <asp:ImageButton ID="mAddButton" runat="Server" SkinID="AddItem" ToolTip="Adicionar" CausesValidation="false" />
    <asp:Panel ID="HeirPersonalInfoPanel" runat="server" SkinID="Popup">
        <table>
            <tr>
                <td>
                    <app:HeirPersonalInfo ID="HeirPersonalInfo" runat="server" ValidationGroup="HeirPersonalInfo" />
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <ajax:UpdatePanel ID="SaveHeirButtonUpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="SaveHeirButton" runat="server" Text="Adicionar" ValidationGroup="HeirPersonalInfo"
                                            OnClick="SaveHeirButton_Click" />
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="CancelHeirButton" runat="server" Text="Cancelar" CausesValidation="false" /></td>
                            <td>
                                <ajax:UpdateProgress ID="SaveHeirButtonUpdateProgress" runat="server" AssociatedUpdatePanelID="SaveHeirButtonUpdatePanel">
                                    <ProgressTemplate>
                                        <asp:Image ID="Image56" runat="server" SkinID="Wait" />
                                    </ProgressTemplate>
                                </ajax:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajax:ModalPopupExtender ID="HeirPersonalInfoPopup" runat="server" TargetControlID="mAddButton"
        PopupControlID="HeirPersonalInfoPanel" CancelControlID="CancelHeirButton">
    </ajax:ModalPopupExtender>
</div>
