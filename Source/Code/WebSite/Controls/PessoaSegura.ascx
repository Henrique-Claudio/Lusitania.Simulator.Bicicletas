<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PessoaSegura.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.PessoaSegura" %>
<%@ Register TagPrefix="app" TagName="Pessoa" Src="~/Controls/Person.ascx" %>
<%@ Register TagPrefix="app" TagName="Beneficiarios" Src="~/Controls/Beneficiarios.ascx" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            3</div>
        <span class="title">PESSOA SEGURA</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <ajax:updatepanel id="PSDetailPanel" runat="server" updatemode="Conditional">					
		    <ContentTemplate>

                <div class="row">
                    <table class="Layout">
                        <tr class="TitleBold">
                            <td>
                                Pessoa Segura é o Tomador?
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList runat="server" ID="mPS_Is_Tomador" RepeatDirection="Horizontal" OnSelectedIndexChanged="mPS_Is_Tomador_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Sim" Value="S" />
                                    <asp:ListItem Text="Não" Value="N" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>                
                </div>

                <!-- Dados Pessoa Segura -->
                <div class="row" runat="server" id="conPessoaSegura" visible="false">
                    <app:Pessoa id="mPessoaSegura" runat="server"></app:Pessoa>
                    <table class="Layout" style="width:100%; margin-top:20px;">
                        <tr class="TitleBold">
                            <td>
                                Parentesco 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:dropdownlist runat="server" ID="mParentesco">
                                </asp:dropdownlist>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="row" style="margin-top:20px;border-top:dotted 2px #FCB53F;">
                 <table class="Layout">
                        <tr class="TitleBold">
                            <td>
                                Pretende a confidencialidade da Pessoa Segura?
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList runat="server" ID="mDadosConfideciais" RepeatDirection="Horizontal" OnSelectedIndexChanged="mPS_Is_Tomador_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Sim" Value="S" />
                                    <asp:ListItem Text="Não" Value="N" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                  </table>
                  </div>

                  <!-- Dados Beneficiários  -->
                  <div class="row" style="margin-top:20px;border-top:dotted 2px #FCB53F;" id="conBeneficiarios" runat="server">
                    <app:Beneficiarios id="mBeneficiarios" runat="server"></app:Beneficiarios>
                  </div>
                  



            </ContentTemplate>
        </ajax:updatepanel>
    </div>
</div>
