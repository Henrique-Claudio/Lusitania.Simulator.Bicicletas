<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Pages/Simulators.Master"
    Title="Proposta de Seguro Acidentes Pessoais" CodeBehind="BicicletasProposal.aspx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Pages.BicicletasProposal" %>

<%@ Register TagPrefix="app" TagName="CommercialAccount" Src="~/Controls/CommercialAccount.ascx" %>
<%@ Register TagPrefix="app" TagName="ProposalTomador" Src="~/Controls/ProposalUserDetails.ascx" %>
<%@ Register TagPrefix="app" TagName="Fraccionamento" Src="~/Controls/Fraccionamento.ascx" %>
<%@ Register TagPrefix="app" TagName="PessoaSegura" Src="~/Controls/PessoaSegura.ascx" %>
<%@ Register TagPrefix="app" TagName="ObjetoSeguro" Src="~/Controls/ObjetoSeguro.ascx" %>
<%@ Register TagPrefix="app" TagName="Inquerito" Src="~/Controls/Inquerito.ascx" %>
<%@ Register TagPrefix="app" TagName="RGPD" Src="~/Controls/RGPD.ascx" %>
<%@ Register TagPrefix="app" TagName="Notes" Src="~/Controls/Notes.ascx" %>
<%@ Register TagPrefix="app" TagName="BicicletasSaveContractQuestions" Src="~/Controls/BicicletasSaveContractQuestions.ascx" %>

<%@ Register TagPrefix="app" TagName="Beneficiarios" Src="~/Controls/Beneficiarios.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div id="ChildrenProtectionHeaderDiv" runat="server" class="header clearfix">
        <div class="logo_lusitania">
            <a href="">
                <img src="Images/logo_lusitania_original.png" border="0" alt="Lusitania" title="Lusitania"></a>            
        </div>
        <div class="logo_simulador">
            <asp:Image ID="Image1" runat="server" SkinID="BicicletasHeader" ImageAlign="middle" />
        </div>      
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" class="BodyColumnProposal"
    runat="server">
        

        <!-- 0 Conta Comercial -->
        <app:CommercialAccount id="mCommercialAccount" runat="server" />

        <!-- 1 Tomador de Seguro -->
        <app:proposaltomador id="mTomador" runat="Server" IsSearchPanelVisible="false" ></app:proposaltomador>

        <!-- 2 Fraccionamento -->
        <app:fraccionamento id="mFraccionamento" runat="server"></app:fraccionamento>

        <!-- 3 Pessoa Segura -->
        <app:pessoasegura id="mpessoasegura" runat="server"></app:pessoasegura>
       
        <!-- 4 Objeto Seguro bicicleta -->
        <app:ObjetoSeguro id="mObjetoSeguro" runat="server"></app:ObjetoSeguro>       
 
        <!-- 5 Resposta Obrigatoria -->
        <app:Inquerito id="mInquerito" runat="server"></app:Inquerito>

        <!-- 6 RGPD -->
        <app:RGPD id="mRGPD" runat="server"></app:RGPD>
        
        <!-- 7 Observasoes -->
        <%--app:notes id="mNotes" runat="server" headerpanelskinid="ChildrenProtectionPanelHeader" /--%>

        <app:BicicletasSaveContractQuestions id="mBicicletasSaveContractQuestions" runat="server" OnSuccess="mSaveContractQuestions_Success"></app:BicicletasSaveContractQuestions>


        <!-- Botoes -->
        <ajax:updatepanel id="mCommandUpdatePanel" runat="server" updatemode="Always">
            <ContentTemplate>
                <table style="margin-left: auto; margin-right: auto; margin-top: 10px;" cellpadding="0" cellspacing="0" border="0" >
                    <tr>
                        <td valign="top">
                            <asp:Button ID="mSaveProposalButton" runat="server" Text="Gravar" SkinID="ButtonSimular"
                                OnClick="mSaveButton_Click" />
                            <asp:Button ID="mPrintProposalButton" runat="Server" Text="Imprimir Proposta" SkinID="ButtonSimular"
                                OnClick="mPrintProposalButton_Click" Visible="false" />
                            <asp:Button ID="mSaveContractButton" runat="server" Text="Contrato" SkinID="ButtonSimular"
                                OnClick="mContractButton_Click" />
                            <asp:Button ID="mPrintContractButton" runat="server" Text="Imprimir Contrato" SkinID="ButtonSimular"
                                OnClick="mPrintContractButton_Click" Visible="false" />
                            <asp:Button ID="mGestaoUnicaRecibos" SkinID="ButtonSimular" runat="server" Text="Gestão Única de Recibos"
                                OnClick="mGestaoUnicaRecibos_Click" />
                            <asp:Button ID="mPrintReceiptButton" runat="server" Text="Imprimir Recibo" SkinID="ButtonSimular"
                                OnClick="mPrintReceiptButton_Click" Visible="false" />                                                        
                        </td>
                    </tr>                   
                    <tr>
                        <td align="center">                            
                            <asp:Button ID="mBackToGenericPlanButton" runat="server" Text="Voltar ao Plano" SkinID="ButtonSimular"
                                OnClick="mBackToGenericPlanButton_Click" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <ajax:UpdateProgress ID="mCommandUpdateProgress" runat="server" AssociatedUpdatePanelID="mCommandUpdatePanel">
                                <ProgressTemplate>
                                    <asp:Image ID="mWaitImage" runat="server" SkinID="Wait" />
                                </ProgressTemplate>
                            </ajax:UpdateProgress>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>           
        </ajax:updatepanel>
       
</asp:Content>
