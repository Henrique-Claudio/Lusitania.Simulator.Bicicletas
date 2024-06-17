<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientData.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.ClientData" %>

    <div class="CollapsiblePanel">
        <div class="PanelHeader clearfix">
            <div class="order">
                2</div>
            <span class="title">Cliente e Pessoa Segura</span>
            <img src="Images/hide.gif" title="Ocultar">
        </div>
        <div class="PanelBody clearfix">
            
                <ajax:updatepanel id="UpdatePanel" runat="Server" updatemode="Conditional">
                <ContentTemplate>
                                
                                
                <div>
                    <h4>PESSOA SEGURA</h4>                    
                </div>

                <div class="row" runat="server">
                    <table>
                        <tr>
                            <td class="TitleBold">
                                Data de Nascimento
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Calendar ID="Calendar1" Visible="false" runat="server"></asp:Calendar>
                                <asp:TextBox runat="server" ID="frmPessoaSegura_dataNasc" MaxLength="10"  Width="100" placeholder="dd-mm-aaaa" OnTextChanged="frmDataNascPessoaSegura_OnChanged" AutoPostBack="true" format="date" TabIndex="4"></asp:TextBox>
                                
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <h4>
                        Cliente (Tomador do Seguro)</h4>                   
                </div>

                  <div class="row">                    
                    <table class="Layout">
                        <tr>
                             <td class="TitleBold">
                                Contribuinte
                            </td>
                           <td class="TitleBold">
                                Nome
                            </td>
                            <td class="TitleBold">
                                Telemóvel
                            </td> 
                            <td class="TitleBold">
                                Email
                            </td> 
                        </tr>
                        <tr>                            
                             <td>
                                <asp:TextBox runat="server" ID="frmTomador_nrContrib" MaxLength="9"  OnTextChanged="frmContrib_OnChangedText" AutoPostBack="true" format="number" TabIndex="5"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="frmTomador_nome" Style="width: 290px;" OnTextChanged="frm_OnChangedText" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="frmTomador_telefone" MaxLength="9" OnTextChanged="frm_OnChangedText" AutoPostBack="true" format="number" TabIndex="7"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="frmTomador_email"  Style="width: 200px;" OnTextChanged="frmEmail_OnChangedText" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                            </td>
                           
                        </tr>
                    </table>
                </div>

                </ContentTemplate>
            </ajax:updatepanel>
            
        </div>
    </div>
