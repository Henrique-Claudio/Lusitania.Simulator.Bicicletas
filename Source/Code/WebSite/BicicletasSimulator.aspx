<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true"
    CodeBehind="BicicletasSimulator.aspx.cs" Inherits="BicicletasSimulator" Title="Bicicletas" %>

<%@ Register Src="~/Controls/CommercialAccount.ascx" TagPrefix="app" TagName="CommercialAccount" %>
<%@ Register Src="~/Controls/InsuranceData.ascx" TagPrefix="app" TagName="InsuranceData" %>
<%@ Register Src="~/Controls/ClientData.ascx" TagPrefix="app" TagName="ClientData" %>
<%@ Register Src="~/Controls/MultiTarificador.ascx" TagPrefix="app" TagName="MultiTarificador" %>
<%@ Register Src="~/Controls/CreatedBy.ascx" TagPrefix="app" TagName="CreatedBy" %>
<%@ Register Src="~/Controls/UserSuggestionsControl.ascx" TagPrefix="app" TagName="UserSuggestionsControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <div class="header clearfix">
        <div class="logo_lusitania">
            <a>
                <img src="Images/logo_lusitania_original.png" border="0" alt="Lusitania" title="Lusitania" /></a>            
        </div>
        <div class="logo_simulador">
            <asp:Image ID="Image2" runat="server" SkinID="BicicletasHeader" ImageAlign="middle" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    
    
    <ajax:updatepanel id="UpdatePage" runat="server" updatemode="Always">     
        <ContentTemplate> 

            <div runat="server" id="divSimulacao">
                <asp:ValidationSummary ID="mValidationSummary" runat="server" ShowMessageBox="true"
                    DisplayMode="BulletList" ShowSummary="false" ValidationGroup="Simulator_Validations" EnableClientScript="false"/>
                <ajax:updatepanel id="updSimulacao" runat="server">
                    <ContentTemplate>
                    <app:PagePropertyField ID="mUserNameField" runat="Server" PagePropertyName="UserName" />
                        <asp:Panel ID="mCommandPanel" runat="server">
                            <app:UserSuggestionsControl ID="mUserSuggestionsControl" runat="server" SimulatorName="Bicicletas"
                                ValidationGroup="UserSuggestions" />
                            <asp:Button ID="mBackToFamilyPlanButton" runat="server" Text="Voltar ao Plano" SkinID="ButtonSimular" OnClientClick="return validaBackToPlan();" OnClick="mBackToGenericPlanButton_Click"
                                CausesValidation="false"/>
                        </asp:Panel>
                    </ContentTemplate>
                </ajax:updatepanel>
     
                <!-- Dados da Conta Comercial -->
                <app:commercialaccount runat="server" id="mCommercialAccount" />
                <!-- Dados do Seguro -->
                <app:insurancedata runat="server" id="mInsuranceData" />
                <!-- Clente e Pessoa Segura -->
                <app:clientdata runat="server" id="mClientData" />
                <!-- Coberturas -->
                <app:multitarificador runat="server" id="mMultiTarificador" />
                <!-- ELABORADO POR -->
                <app:createdby id="mCreatedBy" runat="server" />

            </div>
   
            <div style="text-align: right;">
                <asp:Button ID="mBackToSimulationButton" runat="server" Text="Voltar à Simulação"
                    Visible="false" SkinID="ButtonSimular" OnClick="mBackToSimulationButton_Click"
                    CausesValidation="false" />
            </div>
            <iframe runat="server" id="iFrameProposta"  width="100%" frameborder="0" visible="false"></iframe>


        </ContentTemplate>
    </ajax:updatepanel>
    <ajax:updatepanel id="upTripsGrid" runat="server" updatemode="Always">     
        <ContentTemplate> 
            <input id="SimID" type="hidden" runat="server" />
        </ContentTemplate>
    </ajax:updatepanel>
    <script type="text/javascript">
        function validaBackToPlan() {

            var simulationID = document.getElementById('<%=SimID.ClientID%>').value;

            if (simulationID == null || simulationID == "" || simulationID == "0") {
                $.ConfirmBox('A Simulação não foi concluída.\nTem a certeza que pretende voltar ao Plano?', 'Plano Bicicletas', '3', '<%= mBackToFamilyPlanButton.UniqueID %>');
                return false;
            } else {
                $.fn.LoaderShow();
                return true;
            }
        }
        
 
    </script>
</asp:Content>
