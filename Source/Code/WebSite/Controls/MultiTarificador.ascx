<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiTarificador.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.MultiTarificador" %>
<%@ Register Src="~/Controls/BicicletasPlan.ascx" TagPrefix="app" TagName="BicicletasPlan" %>
<%@ Register Src="~/Controls/QuotationResults.ascx" TagPrefix="app" TagName="QuotationResults" %>
<%@ Register Src="~/Controls/BicicletasSendToPopupControl.ascx" TagPrefix="app" TagName="BicicletasSendToPopupControl" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            3</div>
        <span class="title">COBERTURAS</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <ajax:updatepanel id="MultitarificadorUpdatePanel" runat="server" updatemode="Conditional">
            <ContentTemplate>
                <div style="position: relative">
                   
                    <!-- Block Div Product -->
                    <div runat="server" id="mBlockDivProduct" class="blockProdutoOverlay" visible="false">
                    </div>
                                
                    <!-- Coberturas -->
                    <app:bicicletasplan id="mBicicletasPlan" runat="server" />

                    <!-- Resultados -->
                    <app:quotationresults id="mQuotationResults" runat="server" />
                </div>

                                 

                <!-- Botões -->  
                <table class="Buttons"> 
                    <tr runat="server" id="conProtocolo" visible="true">
                        <td>
                            <!-- Protocolo -->
                             <table class="Protocolos Layout">
                                 <tr>
                                    <td class="TitleBold">Protocolo</td>
                                    <td style="visibility:hidden" class="TitleBold">Nº associado AMP</td>
                                </tr>
                                 <tr>
                                    <td><asp:DropDownList runat="server" AutoPostBack="true" ID="mDDLProtocolo" OnSelectedIndexChanged="mDDLProtocolo_SelectedIndexChanged"></asp:DropDownList></td>
                                    <td style="visibility:hidden"><asp:TextBox runat="server" ID="mNumAssociado" Enabled="false"></asp:TextBox></td>
                                </tr>
                            </table>  
                        </td>
                    </tr>                   
                    <tr>                            
                        
                        <td class="conSDD" id="comSDDrow" runat="server" >
                            <span>Pagamento por débito em conta?</span>
                            <asp:CheckBox ID="chkSDD" AutoPostBack="true" runat="server" OnCheckedChanged="chkSDD_CheckedChanged"/>
                        </td>
                        
                        <td style="float:right;">                             
                            <!-- Botão Plano-->
                            <asp:Button ID="mBackToGenericPlanButton" runat="server" Text="Voltar ao Plano" CssClass="ButtonSimular" OnClientClick="return validaBackToPlan();" OnClick="mBackToGenericPlanButton_Click" CausesValidation="false"/>                                                                                   
                            <!-- Botão Imprimir Cotação-->
                            <asp:Button ID="mPrintButton" runat="server" Text="Imprimir" CssClass="_btn ButtonPropostas" OnClick="mPrintButton_Click" Enabled="false" />
                            <!-- Botão Proposta-->
                            <asp:Button ID="mProposalButton" runat="server" Text="Proposta" CssClass="_btn ButtonPropostas" OnClick="mProposalButton_Click" Enabled="false"/>
                            <!-- Botão Enviar Cotação pelo email-->
                            <asp:Button ID="mEnviar" runat="server" Text="Enviar" CssClass="_btn ButtonPropostas" OnClick="mEnviar_Click" Enabled="false" />                                                                           
                            <!-- Botão Simular-->
                            <asp:Button ID="mSimulationButton" runat="Server" Text="Simular" CssClass="_btn ButtonSimular"  OnClick="mSimulationButton_Click" />                            
                             
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="disclaimer">
                            O Lusitania Bicicletas está disponível para velocípedes com duas rodas, acionado pelo esforço do próprio condutor, por meio de pedais.<br/>
                            Não são considerados os veículos equipados com qualquer tipo de motor.<br/>
                            A cobertura  de morte ou invalidez permanente só produz efeitos quando a pessoa segura tenha idade igual ou superior a 14 anos.<br/>
                            A idade limite de adesão ao módulo Super é até aos 65 anos de idade.
                        </td>
                    </tr>
                </table>
                           
                <asp:Panel ID="mCommercialDiscountPanel" runat="Server" Visible="false">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td valign="middle" colspan="2">
                                        <asp:Label ID="Label5" runat="Server" Text="Desconto Comercial:" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="middle">
                                        <asp:TextBox ID="mCommercialDiscountBox" runat="server" SkinID="Cash" MaxLength="8"
                                            Text="0">
                                        </asp:TextBox>                                        
                                    </td>
                                    <td valign="middle">
                                        <asp:Label ID="Label6" runat="server" Text="%"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>   
                        
                        
                        
                <div id="InfoProduto_1" style="display: none;">
                    <table class="InfoProduto">
                        <tr>
                            <th >
                                Fracionamento  
                            </th>
                            <th>
                                Prémio Comercial
                            </th>
                            <th>
                                Prémio Total
                            </th>
                            <th>
                                Valor 1º Recibo
                            </th>
                            <th style="width:200px;">
                                Período 1º Recibo
                            </th>
                            <th>
                                Recibos Seguintes
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Anual
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_comercial" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_total" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_recibo" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_periodo" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_seguinte" class="enable"></asp:Label>
                            </td>
                        </tr>
                        <!--
                       <tr class="trPremioEmPlano">
                            <td colspan="2">
                                 Lusitania Plano E +                                   
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_1_plano" class="enable"></asp:Label>
                            </td>
                            <td colspan="3"  class="LastChild">
                               Com o Lusitania Plano E+, além de descontos, tem muitas mais vantagens.
                            </td>
                        </tr>
                        -->
                    </table>
                </div>
                <div id="InfoProduto_2" style="display: none;">
                    <table class="InfoProduto">
                        <tr>
                            <th >
                                Fracionamento  
                            </th>
                            <th>
                                Prémio Comercial
                            </th>
                            <th>
                                Prémio Total
                            </th>
                            <th>
                                Valor 1º Recibo
                            </th>
                            <th style="width:200px;">
                                Período 1º Recibo
                            </th>
                            <th>
                                Recibos Seguintes
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Anual
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_comercial" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_total" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_recibo" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_periodo" class="enable"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_seguinte" class="enable"></asp:Label>
                            </td>
                        </tr>

                        <!-- 
                        <tr class="trPremioEmPlano">
                            <td colspan="2">
                                Lusitania Plano E +                                
                            </td>
                            <td>
                                <asp:Label runat="server" ID="mProduto_2_plano" class="enable"></asp:Label>
                            </td>
                            <td colspan="3" class="LastChild">
                               Com o Lusitania Plano E+, além de descontos, tem muitas mais vantagens.
                            </td>
                        </tr>
                        -->
                    </table>
                </div>         

             </ContentTemplate>
        </ajax:updatepanel>
    </div>
</div>
<!-- Tabela de informação adicional dos limites de despesas para cobertura Assistencia Em Viagem -->
<div id="InfoAssistEmViagem" style="display: none;">
    <img src="Images/InfoAssistEmViagem.png" border="0">
    <%--asp:GridView ID="mGridCE_Portugal" runat="server" AutoGenerateColumns="false" CssClass="InfoTable">
        <Columns>
            <asp:TemplateField HeaderText=" GARANTIAS DE ASSISTÊNCIA A PESSOAS EM PORTUGAL" HeaderStyle-CssClass="Descricao">
                <ItemTemplate>
                    <div id="Nome" runat="Server" class='<%# ("nivel"+ Eval("Nivel")) %>'>
                        <%# Eval("Nome") %></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Limit Max" HeaderText=" LIMITE MÁXIMO" HeaderStyle-CssClass="LimitMax"/>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="mGridCE_Espanha" runat="server" AutoGenerateColumns="false" CssClass="InfoTable" >
        <Columns>
            <asp:TemplateField HeaderText=" GARANTIAS DE ASSISTÊNCIA A PESSOAS EM ESPANHA" HeaderStyle-CssClass="Descricao">
                <ItemTemplate>
                    <div id="Nome" runat="Server" class='<%# ("nivel"+ Eval("Nivel")) %>'>
                        <%# Eval("Nome") %></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Limit Max" HeaderText=" LIMITE MÁXIMO" HeaderStyle-CssClass="LimitMax"/>
        </Columns>
    </asp:GridView--%>
</div>


<app:BicicletasSendToPopupControl runat="server" id="mSendPopupQuotation" />
