<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotationResults.ascx.cs"
    Inherits="QuotationResults" %>
<asp:Panel ID="PremiumPanel" runat="server" Visible="true">
    <table class="Fraccionamento">
        <tr>
            <th>
                Fracionamento
            </th>
            <th>
                Prémio Total
            </th>
            <th>
                Prémio Total
            </th>
        </tr>
        <tr>
            <!-- Por agora fica estatico a representação dos Premios, se no futuro começar a ter 
                 mais fracionamentos e aqui que se deve acrescentar o grid dinamico -->
            <td>
                Anual
            </td>
            <td>
                <asp:Label runat="server" ID="mPremio_Produto_1" class="enable"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="mPremio_Produto_2" class="enable"></asp:Label>
            </td>
        </tr>
        <tr class="RefSIm">
            <td>
                Ref. simulação:<asp:Label runat="server" ID="mRefSim" class="enable">1234565</asp:Label>
            </td>
            <td>
                <ajax:updatepanel id="updateVerDetalhe_1" runat="server" updatemode="Always">
                    <ContentTemplate>
                        <asp:RadioButton GroupName="VerDetalhe" runat="server" ID="mVerDetalhe_1" Enabled="false" AutoPostBack="true" OnCheckedChanged="verDetalhe_SelectedIndexChanged" />
                        <span runat="server" id="mLabelDisable_1">Ver Detalhe</span>
                        <span runat="server" id="mLabelEnable_1" visible="false" class="enable Icon-InfoBox" infoBoxTitle="" infoBox="InfoProduto_1">Ver Detalhe</span>
                        <asp:image imageurl="~/Images/info_descount.png" runat="server" id="mDescontoComrec_1" class="DetailTooltip" visible="false"/>
                    </ContentTemplate>                                        
                </ajax:updatepanel>
            </td>
            <td>
                <ajax:updatepanel id="updateVerDetalhe_2" runat="server" updatemode="Always">
                    <ContentTemplate>
                        <asp:RadioButton GroupName="VerDetalhe" runat="server" ID="mVerDetalhe_2" Enabled="false" AutoPostBack="true" OnCheckedChanged="verDetalhe_SelectedIndexChanged" />
                        <span runat="server" id="mLabelDisable_2">Ver Detalhe</span>
                        <span runat="server" id="mLabelEnable_2" visible="false" class="enable Icon-InfoBox" infoBoxTitle="" infoBox="InfoProduto_2">Ver Detalhe</span>
                        <asp:image imageurl="~/Images/info_descount.png" runat="server" id="mDescontoComrec_2" class="DetailTooltip"  visible="false"/>
                    </ContentTemplate>                     
                 </ajax:updatepanel>
            </td>
        </tr>
       
        <%--<tr class="PremioEmPlano" id="trPlanPremium" runat="server">
            <td>
                Plano E+ Lusitania
            </td>
            <td>
                <asp:Label runat="server" ID="mPremio_Plano_Produto_1" class="enable"></asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="mPremio_Plano_Produto_2" class="enable"></asp:Label>
            </td>
        </tr>
      --%>
    </table>
    <asp:Panel ID="QuotationResultsPanel" runat="server">
        <asp:PlaceHolder ID="mTablePlaceHolder" runat="server" />
    </asp:Panel>
</asp:Panel>
