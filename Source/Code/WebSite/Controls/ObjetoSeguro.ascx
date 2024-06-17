<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ObjetoSeguro.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.ObjetoSeguro" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
           4</div>
        <span class="title">Objeto Seguro - Bicicleta</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <ajax:updatepanel id="UpdatePanel" runat="server" updatemode="Always">					
		    <ContentTemplate>

                <table class="Layout" style="width:100%;">            
            <%--<tr>
                <td class="TitleBold" colspan="5">
                    Ano da 1ª aquisição                
                    <asp:TextBox runat="server" id="mAnoAquisicao" MaxLength="4" format="number" style="margin-left:20px;width:171px;" AutoPostBack="true"/>
                </td>
                
            </tr>
            <tr class="space"> <td colspan="5"></td></tr>
             <tr>
                <td class="TitleBold">
                    Marca
                </td>
                <td>
                    <asp:TextBox runat="server" ID="mMarca" Width="250" MaxLength="30" AutoPostBack="true"/>
                </td>
                
                <td rowspan="3" style="width:50px;">
                </td>
                
                <td class="TitleBold">
                    Modelo 
                </td>
                <td>
                    <asp:TextBox runat="server" ID="mModelo" Width="250" MaxLength="50" AutoPostBack="true"/>
                </td>
            </tr>
             <tr class="space"> <td colspan="4"></td></tr>
             <tr>
                <td class="TitleBold">
                    Versão
                </td>
                <td>
                    <asp:TextBox runat="server" ID="mVersao" Width="250" MaxLength="50" AutoPostBack="true"/>
                </td>
                

                <td class="TitleBold">
                    Nº de quadro
                </td>
                <td>
                    <asp:TextBox runat="server" ID="mNrQuadro" Width="250" MaxLength="30" AutoPostBack="true"/>
                </td>
            </tr>--%>
            <tr>
                <td class="TitleBold">
                    Estão incluídos como objetos seguros desta apólice todas as bicicletas cuja propriedade é do Tomador do Seguro ou do Segurado, mediante comprovativo de aquisição a apresentar à LUSITANIA sempre que solicitado.
                </td>
            </tr>
        </table>

            </ContentTemplate>
        </ajax:updatepanel>
    </div>
</div>
