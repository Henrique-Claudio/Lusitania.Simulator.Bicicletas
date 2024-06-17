<%@ Control Language="C#" AutoEventWireup="true" Inherits="Lusitania.Simuladores.WebSite.Controls.CreatedBy"
    CodeBehind="CreatedBy.ascx.cs" %>
<%@ Register TagPrefix="app" TagName="UserSuggestionsControl" Src="~/Controls/UserSuggestionsControl.ascx" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            4</div>
        <span class="title">Elaborado Por</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <table class="Layout">
            <tr>
                                    
                <td  class="TitleBold">
                    <asp:Label ID="mNameLabel" runat="server" Text="Nome"></asp:Label>
                </td>
                <td  class="TitleBold">
                    <asp:Label ID="mEMailLabel" runat="server" Text="E-Mail" SkinID="LabelBold"></asp:Label>
                </td>
                <td  SkinID="LabelBold">
                    <asp:Label ID="mPhoneNumberLabel" runat="server" Text="Telefone" ></asp:Label>
                </td>
            </tr>
            <tr>                                    
                <td>
                    <asp:TextBox ID="mNameBox" runat="server" Width="390" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="mEMailBox" runat="server" Width="225" ReadOnly="true"></asp:TextBox>
                </td>
                <td >
                    <asp:TextBox ID="mPhoneNumberBox" runat="server" Width="160"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</div>
