<%@ Control Language="C#" AutoEventWireup="true" Inherits="CommercialAccount" CodeBehind="CommercialAccount.ascx.cs" %>


<asp:ValidationSummary ID="mMediatorValidationSummary" runat="server" ValidationGroup="SearchMediator"
    DisplayMode="SingleParagraph" ShowMessageBox="true" ShowSummary="false"   EnableClientScript="false"/>

<asp:ValidationSummary ID="mCollectorValidationSummary" runat="server" ValidationGroup="SearchCollector"
    DisplayMode="List" ShowMessageBox="true" ShowSummary="false"  EnableClientScript="false"/>


<div class="CollapsiblePanel" runat="server" id="conCommercialAccount">
    <div class="PanelHeader clearfix">
        <div class="order">
            0</div>
        <span class="title">CONTA COMERCIAL</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix" id="mContentPanel" runat="server">
        <table class="Layout">
            <tr>
                <td colspan="2">
                    <asp:Label ID="mMediatorHeader" runat="server" Text="Mediador" SkinID="Heavy"></asp:Label>
                </td>
                <td style="width: 30px" rowspan="3">
                </td>
                <td colspan="2">
                    <asp:Label ID="mCollectorHeader" runat="Server" Text="Cobrador" SkinID="Heavy"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="mMediatorNumberHeader" runat="Server" Text="Número de Conta" SkinID="LabelBold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="mMediatorNameHeader" runat="server" Text="Descritivo" SkinID="LabelBold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="mCollectorNumberHeader" runat="server" Text="Número de Conta" SkinID="LabelBold"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="mCollectorNameHeader" runat="server" Text="Descritivo" SkinID="LabelBold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <ajax:updatepanel id="mMediatorNumberBoxUpdatePanel" runat="Server">                        
                        <ContentTemplate>
                            <asp:TextBox ID="mMediatorNumberBox" runat="server" MaxLength="5" Width="100" format="number" AutoPostBack="true" OnTextChanged="mMediatorNumberBox_TextChanged"></asp:TextBox>                    
                        </ContentTemplate>
                    </ajax:updatepanel>
                </td>
                <td>
                    <ajax:updatepanel id="mMediatorNameBoxUpdatePanel" runat="Server">
                        <Triggers>
                            <ajax:AsyncPostBackTrigger ControlID="mMediatorNumberBox" EventName="TextChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="mMediatorNameBox" runat="server" Width="270" SkinID="CaixaInput" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </ContentTemplate>
                    </ajax:updatepanel>
                </td>
                <td>
                    <ajax:updatepanel id="mCollectorNumberBoxUpdatePanel" runat="Server">                        
                        <ContentTemplate>
                            <asp:TextBox ID="mCollectorNumberBox" runat="server" Width="100" MaxLength="5" format="number"
                        AutoPostBack="true" OnTextChanged="mCollectorNumberBox_TextChanged"></asp:TextBox>        
                        </ContentTemplate>
                    </ajax:updatepanel>
                </td>
                <td>
                    <ajax:updatepanel id="mCollectorNameBoxUpdatePanel" runat="server" updatemode="Conditional">
                        <Triggers>
                            <ajax:AsyncPostBackTrigger ControlID="mCollectorNumberBox" EventName="TextChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:TextBox ID="mCollectorNameBox" runat="server" Width="270" SkinID="CaixaInput" ReadOnly="true" Enabled="False"></asp:TextBox>
                        </ContentTemplate>
                    </ajax:updatepanel>
                </td>
            </tr>
        </table>
    </div>
</div>
