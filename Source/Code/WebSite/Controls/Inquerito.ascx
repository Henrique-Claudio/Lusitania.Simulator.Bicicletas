<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inquerito.ascx.cs" Inherits="Lusitania.Simuladores.WebSite.Controls.Inquerito" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            5</div>
        <span class="title">Respostas Obrigatórias</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
     <ajax:updatepanel id="UpdatePanel" runat="server" updatemode="Always">					
		    <ContentTemplate>
                <table class="Layout" style="width:100%;">
                    <colgroup>
                        <col style="width: 65%;" />
                        <col style="text-align:right;" />
                    </colgroup>
                    <tr>
                        <td class="TitleBold">
                            Existem débitos por falta de pagamento relativamente a outros seguros sobre o veículo
                            ?
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="mFaltaPagamento" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:ListItem Text="Sim" Value="S" />
                                <asp:ListItem Text="Não" Value="N" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="space">
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="TitleBold">
                            Quanto sinistros teve nas últimas 2 anuidades?
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="mNrSinistros" AutoPostBack="true">
                                <asp:ListItem Text="      " Value="" />
                                <asp:ListItem Text="Nenhum" Value="0" />
                                <asp:ListItem Text="Um" Value="1"/>
                                <asp:ListItem Text="Dois" Value="2"/>
                                <asp:ListItem Text="Três" Value="3"/>
                                <asp:ListItem Text="Mais de Três" Value="4"/>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="EmailPanel" visible="false" runat="server">

                                                                                                                                                                                <table cellpadding="0" cellspacing="0" border="0" id="ReceberEmailDiv" runat="server"
                    width="90%">
                    <tr>
                        <td align="left" width="62%">
                            <asp:Label ID="Label8" runat="server" SkinID="LabelBold" Text="Pretende receber a documentação inicial de contratação deste seguro por email?"></asp:Label>
                        </td>
                        <td align="left" style="padding-top: 5px; position: relative;" width="10%">
                            <span id="infoAtividades" class="popupInfo" href="#" style="" onclick="$('.ui-infoBox-Wrapper').toggle();">
                            </span>
                            <div class="ui-infoBox-Wrapper" style="position: absolute; width: 600px; top: -65px;
                                left: -250px; display: none">
                                <div class="ui-infoBox-Header">
                                    <a class="ui-infoBox-close" tabindex="-1" onclick="$('.ui-infoBox-Wrapper').toggle();"
                                        title="Fechar Janela">X</a></div>
                                <div class="ui-infoBox-Body">
                                    Ao concordar, autoriza que a entrega da referida documentação seja efetuada por
                                    meio de suporte eletrónico duradouro. Autoriza, ainda, que todas as comunicações
                                    ou notificações do Segurador, ao abrigo de contratos de apólices de seguro, lhe
                                    sejam preferencialmente dirigidas para o endereço eletrónico indicado. Esta autorização
                                    não invalida que, por opção do Segurador, as mesmas comunicações ou notificações
                                    possam também ser efetuadas para a morada constante na presente proposta de seguro.</div>
                            </div>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="mNaoReceberEmailBox" runat="server" Text="Não" GroupName="ReceberEmail" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="mReceberEmailBox" runat="server" Text="Sim" Checked="true" GroupName="ReceberEmail" />
                                    </td>
                                </tr>
                            </table>
                            <app:GroupedRadioButtonRequiredFieldValidator ID="mReceberEmailBoxValidator" runat="server"
                                GroupName="ReceberEmail" Display="None" SetFocusOnError="false" ErrorMessage="Indique se pretende receber a documentação por email.">
                            </app:GroupedRadioButtonRequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                    <ajax:CollapsiblePanelExtender ID="mReceberEmailPanelExtender" runat="server" TargetControlID="mReceberEmailPanel"
                        CollapseControlID="mNaoReceberEmailBox" ExpandControlID="mReceberEmailBox" Collapsed="false">
                    </ajax:CollapsiblePanelExtender>
                    <asp:Panel ID="mReceberEmailPanel" runat="server">
                                                                                                            <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" SkinID="LabelBold" Text="Email:" Width="50"></asp:Label>
                            </td>
                            <td>
                                <ajax:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
                                    <ContentTemplate>
                                        <asp:TextBox ID="mEmailEnvio" runat="server" OnTextChanged="ValidaEmailBox" AutoPostBack="True"
                                            MaxLength="100" Width="310" SkinID="CaixaInput"></asp:TextBox>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                                <%--<asp:RequiredFieldValidator ID="mEmailEnvioValidator" runat="server" ControlToValidate="mEmailEnvio"
                                    ErrorMessage="Indique o Email a enviar a documentação." Display="None" SetFocusOnError="false"></asp:RequiredFieldValidator>
                                <app:ValidatorEnablerExtender ID="mEmailEnvioValidatorExtender" runat="server" TargetControlID="mEmailEnvioValidator"
                                    RadioButtonControlID="mReceberEmailBox">
                                </app:ValidatorEnablerExtender>--%>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>      
        </ajax:updatepanel>
    </div>
</div>
