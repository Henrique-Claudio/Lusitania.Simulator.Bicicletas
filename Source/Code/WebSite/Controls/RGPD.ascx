<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RGPD.ascx.cs" Inherits="Lusitania.Simuladores.WebSite.Controls.RGPD" %>
<div class="CollapsiblePanel" id="div_RGPD" runat="server">
    <div class="PanelHeader clearfix">
        <div class="order">
            6</div>
        <span class="title">Tratamento de Dados Pessoais</span>
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
                            Autoriza a Lusitania, Companhia de Seguros, S.A. a utilizar os dados pessoais agora recolhidos para a finalidade de comunicação e Marketing, nomeadamente por correio, SMS, email e telefone, em ações de marketing direto, informações sobre campanhas e oferta de produtos e serviços acessórios relacionados, ainda que indiretamente, com a atividade da Seguradora?
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="mRGPD" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:ListItem Text="Não" Value="N" />
                                <asp:ListItem Text="Sim" Value="A" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="space">
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>      
        </ajax:updatepanel>
    </div>
</div>
