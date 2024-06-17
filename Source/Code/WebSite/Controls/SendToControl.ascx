<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Lusitania.Simuladores.WebSite.Controls.SendToControl" Codebehind="SendToControl.ascx.cs" %>
<asp:Panel ID="mMainPanel" runat="Server">
    <table cellpadding="0" cellspacing="0" width="735">
        <tr>
            <td>
                <asp:Panel ID="mHeaderPanel" runat="server" HorizontalAlign="Center">
                    <table width="735" height="35" cellpadding="0" cellspacing="0" bgcolor="#CCE3DF">
                        <tr>
                            <td align="left" style="padding-left:15px;">
                                <asp:Label ID="mHeaderLabel" runat="server" SkinID="PanelHeaderSendEmail" Text="Enviar Cotação Por E-Mail"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <table width="735" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td>
                            <asp:Panel ID="Panel1" runat="server" GroupingText="" style="border:none;">
                                <table>
                                    <tr>
                                        <td class="TitleGreen" style="text-transform:none; padding-top:10px;">
                                            Os seus dados
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding-top:5px;">
                                            <asp:Label ID="Label3" runat="server" Text="Nome" SkinID="LabelBold"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="FromNameBox" runat="server" MaxLength="100" Width="300" SkinID="CaixaInput" AutoCompleteType="DisplayName"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="FromNameBoxV1" runat="Server" ControlToValidate="FromNameBox"
                                                ErrorMessage="Indique o seu Nome." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding-top:5px;">
                                            <asp:Label ID="Label5" runat="Server" Text="E-Mail" SkinID="LabelBold"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="FromEMailBox" runat="Server" MaxLength="100" Width="300" SkinID="CaixaInput" AutoCompleteType="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="FromEMailBoxV1" runat="server" ControlToValidate="FromEMailBox"
                                                ErrorMessage="Indique o seu E-Mail." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <app:EmailValidator ID="FromEMailBoxV2" runat="server" ControlToValidate="FromEMailBox" ErrorMessage="O E-Mail do Remetente não é válido." Display="None" SetFocusOnError="true"></app:EmailValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Panel ID="Panel2" runat="server" GroupingText="" style="border:none;">
                                <table>
                                    <tr>
                                        <td class="TitleGreen" style="text-transform:none; padding-top:10px;">
                                            Os dados do Destinatário
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding-top:5px;">
                                            <asp:Label ID="Label4" runat="server" Text="Nome" SkinID="LabelBold"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="ToNameBox" runat="Server" MaxLength="100" Width="300" SkinID="CaixaInput"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ToNameBoxV1" runat="server" ControlToValidate="ToNameBox"
                                                ErrorMessage="Indique o Nome do Destinatário." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding-top:5px;">
                                            <asp:Label ID="Label6" runat="Server" Text="E-Mail" SkinID="LabelBold"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="ToEMailBox" runat="server" MaxLength="100" Width="300" SkinID="CaixaInput"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ToEMailBoxV1" runat="server" ControlToValidate="ToEMailBox"
                                                ErrorMessage="Indique o E-Mail do Destinatário." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <app:EmailValidator ID="ToEMailBoxV2" runat="server" ControlToValidate="ToEMailBox" ErrorMessage="O E-Mail do Destinatário não é válido." Display="None" SetFocusOnError="true"></app:EmailValidator>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="TitleBold" style="text-transform:none; padding:15px 0 3px 3px;">
                            Mensagem
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding:0 0 3px 3px;">
                            <asp:Panel ID="Panel3" runat="server" GroupingText="" style="border:none;">
                                <asp:TextBox ID="MessageBox" runat="server" MaxLength="1000" SkinID="CaixaTexto" Width="720" Height="100" TextMode="MultiLine"></asp:TextBox>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowMessageBox="True"
                    ShowSummary="False" EnableClientScript="true"/>
            </td>
        </tr>
    </table>
</asp:Panel>
