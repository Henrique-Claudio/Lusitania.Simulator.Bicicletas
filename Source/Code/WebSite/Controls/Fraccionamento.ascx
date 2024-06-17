<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Fraccionamento.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.Fraccionamento" %>
<%@ Register Src="~/Controls/BancSearch.ascx" TagPrefix="app" TagName="BancSearch" %>


<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            2</div>
        <span class="title">Fraccionamento</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <ajax:updatepanel id="UserFracionamentoPanel" runat="server" updatemode="Always">					
		    <ContentTemplate>
                <asp:RadioButtonList runat="server" ID="mFraccionamentos"  RepeatDirection="Horizontal" AutoPostBack="true" CssClass="TitleBold">                        
                            </asp:RadioButtonList>
                <table class="Layout" style="width:100%; margin-top:10px;" runat="server" id="conIBAN_BIC">
                  <colgroup >
                    <col style="width:50%"/>
                </colgroup>  
                    <tr>
                        <td class="TitleBold">
                            IBAN <asp:textbox runat="server" ID=mIBAN Width="300" MaxLength="34" OnTextChanged="ValidaIBAN_ObtemBIC" AutoPostBack="true"/>
                        </td> 
                        <td rowspan="3" style="vertical-align: middle;text-align:center;">
                            <span class="InfoBtn Icon-InfoBox" infoboxtitle="Alterções Ao SDD" infobox="popup_text_IBAN">
                                CONHEÇA AS ALTERAÇÕES AO SDD
                            </span>                            
                        </td>
                    </tr>
                    <tr><td colspan="2" style="height:10px"></td></tr>
                    <tr>
                        <td  class="TitleBold">
                           BIC/SWIFT CODE <asp:textbox runat="server" ID="mBIC" Width="234" MaxLength="11"/> 
                           <asp:ImageButton ID="mSearchBancButton" runat="server" SkinID="Zoom" CausesValidation="false" OnClick="mSearchBIC_SWC_Click"/>

                           <asp:Panel ID="mBancSearchPopupPanel" runat="Server" SkinID="Popupv6">
                                <asp:Panel ID="mBancSearchPanel" runat="Server" Visible="false">
	                            <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <app:BancSearch ID="mBancSearch" runat="server" OnBancChosen="mBancSearch_BancChosen" OnBancLocalSearch="mBancSearch_BancLocalSearch" OnBancPageIndexChanging="mBancSearch_BancLocalSearch"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-top:10px;">
                                            <asp:Button ID="mCloseBancSearchButton" runat="server" Text="Fechar" SkinID="SimulateX4" CausesValidation="false" /></td>
                                    </tr>
                                </table>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:HyperLink ID="mBancSearchDummyButton" runat="Server"></asp:HyperLink>
                            <ajax:ModalPopupExtender ID="mBancSearchPopup" runat="server" TargetControlID="mBancSearchDummyButton"  PopupControlID="mBancSearchPopupPanel" BehaviorID="pop_up_search_profissao" Y="300">
                            </ajax:ModalPopupExtender>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </ajax:updatepanel>
    </div>
</div>
<%-- Informação sobre alteração ao SDD --%>
<div id="popup_text_IBAN" style="display: none;">
    <table>
        <tr>
            <td style="font-style: normal; font-size: 10pt; font-weight: bold;">
                INFORMAÇÃO IMPORTANTE
            </td>
        </tr>
        <tr style="height: 15px;">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <p style="font-style: normal; font-size: 8pt;">
                    <b>IBAN - Número de conta bancária internacional</b>
                    <br />
                    O código IBAN das contas bancárias portuguesas são formados pela adição do código
                    PT50 ao seu atual NIB:
                    <br />
                    Ex: PT50XXXXXXXXXXXXXXXXXXXXX
                </p>
                <p style="font-style: normal; font-size: 8pt; margin-top: 10px;">
                    <b>BIC / SWIFT CODE (código internacional de identificação de Banco) : </b>
                    <br />
                    Permite identificar o seu Banco, País e Agência Bancária, de modo a que as transferências
                    internacionais se possa realizar
                    <br />
                    Ex: “MPIOPTPL” – Corresponde ao BIC / SWIFT CODE do Montepio Geral
                </p>
                <p style="font-style: normal; font-size: 8pt; margin-top: 10px;">
                    <b>ONDE CONSULTAR?</b>
                    <br />
                    Pode encontrar o IBAN da sua conta, na sua caderneta bancária, no extrato de conta
                    à ordem, no serviço de multibanco ou ainda através do seu gestor de conta bancária.
                    <br />
                    Para obter o BIC/ SWIF CODE da sua conta deve contactar o seu banco.
                </p>
            </td>
        </tr>
    </table>
</div>



