<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true" Inherits="Lusitania.Simuladores.WebSite.Pages.FamilyRentProposal" Title="Proposta de Seguro de Renda Familiar" Codebehind="FamilyRentProposal.aspx.cs" %>

<%@ Register TagPrefix="app" TagName="ProposalUserDetails" Src="~/Controls/ProposalUserDetails.ascx" %>
<%@ Register TagPrefix="app" TagName="SpousePersonalInfo" Src="~/Controls/SpousePersonalInfo.ascx" %>
<%@ Register TagPrefix="app" TagName="Notes" Src="~/Controls/Notes.ascx" %>
<%@ Register TagPrefix="app" TagName="SaveContractQuestions" Src="~/Controls/FamilyRentSaveContractQuestions.ascx" %>
<%@ Register TagPrefix="app" TagName="MortgageCreditor" Src="~/Controls/MortgageCreditor.ascx" %>
<%@ Register TagPrefix="app" TagName="ProposalCommercialAccount" Src="~/Controls/CommercialAccount.ascx" %>
<%@ Register TagPrefix="app" TagName="FamilyRentHeirListControl" Src="~/Controls/FamilyRentHeirListControl.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <asp:Image ID="Image1" runat="server" SkinID="PersonalAccidentsHeader" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
<div runat="server" id="divProposta" style="background-color: #FFFFFF; border:solid 1px #5B8673; padding: 2px;">
<script type="text/javascript">
function VoltarSimulacao()
{
    window.top.document.getElementById("<%=divSim%>").style.display = "";
    window.top.document.getElementById("<%=divPro%>").style.display = "none";
}
</script>
    <div style="text-align: right;">
        <asp:Button ID="mBackToSimulationButton" runat="server" Text="Voltar à Simulação" OnClientClick="VoltarSimulacao()" CausesValidation="false" />
    </div>
    <asp:ValidationSummary ID="mValidationSummary" runat="server" ShowSummary="false" ShowMessageBox="true" />
    <app:ProposalUserDetails ID="mProposalUserDetails" runat="Server" RequireDriverLicense="false" HeaderPanelSkinID="PersonalAccidents" RequireMinimumAge="true" />
    <br />
    <ajax:CollapsiblePanelExtender ID="mHeaderPanelExtender" runat="server" ImageControlID="Image2" ExpandControlID="Image2" CollapseControlID="Image2" TargetControlID="mContentPanel">
    </ajax:CollapsiblePanelExtender>
    <asp:Panel ID="mHeaderPanel" runat="server" SkinID="PersonalAccidents">
        <asp:Label ID="mHeaderLabel" runat="server" SkinID="PanelHeader" Text="Fraccionamento"></asp:Label>
        <asp:Image ID="Image2" runat="server" SkinID="PanelHeader" ImageUrl="~/Images/CollapsePanel_65x15.gif" />
    </asp:Panel>
    <asp:Panel ID="mContentPanel" runat="server">
        <fieldset>
            <asp:RadioButtonList ID="mFractionBox" runat="server" Style="margin-left: auto; margin-right: auto;" RepeatDirection="Horizontal" OnDataBound="mFractionBox_DataBound">
                <asp:ListItem Text="Dummy 1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Dummy 2"></asp:ListItem>
            </asp:RadioButtonList>
        </fieldset>
    </asp:Panel>
    <br />
    <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" ImageControlID="Image3" ExpandControlID="Image3" CollapseControlID="Image3" TargetControlID="Panel2">
    </ajax:CollapsiblePanelExtender>
    <asp:Panel ID="Panel1" runat="server" SkinID="PersonalAccidents">
        <asp:Label ID="Label1" runat="server" SkinID="PanelHeader" Text="Pessoas Seguras"></asp:Label>
        <asp:Image ID="Image3" runat="server" SkinID="PanelHeader" ImageUrl="~/Images/CollapsePanel_65x15.gif" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <asp:Panel ID="mUserPanel" runat="server" GroupingText="Titular">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="É Canhoto ?"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="mIsLeftHandedBox" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="mIsLeftHandedBoxRFV" runat="server" ControlToValidate="mIsLeftHandedBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Titular é canhoto ou não."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label3" runat="server" Text="Tem diminuição de faculdades:"></asp:Label></td>
                </tr>
                <tr>
                    <td valign="top">
                        <ajax:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="Label43" runat="server" Text="Visuais:"></asp:Label></td>
                                        <td align="left" valign="middle" runat="server" id="mVisualImpairmentDegreeBlock1">
                                            <asp:Label ID="mVisualImpairmentDegreeLabel" runat="server" Text="Grau:"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:RadioButtonList ID="mHasVisualImpairmentBox" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="mHasVisualImpairmentBoxRFV" runat="server" ControlToValidate="mHasVisualImpairmentBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Titular tem diminuição de faculdades visuais ou não."></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" valign="middle" runat="server" id="mVisualImpairmentDegreeBlock2">
                                            <asp:TextBox ID="mVisualImpairmentDegreeBox" runat="server" MaxLength="8" Width="30px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="mVisualImpairmentDegreeBoxV1" runat="server" ControlToValidate="mVisualImpairmentDegreeBox" ErrorMessage="Indique o Grau de Diminuição Visual." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="mVisualImpairmentDegreeBoxV2" runat="Server" ControlToValidate="mVisualImpairmentDegreeBox" Operator="DataTypeCheck" Type="Currency" ErrorMessage="O Grau de Diminuição Visual não é válido." Display="None" SetFocusOnError="true"></asp:CompareValidator>
                                            <ajax:FilteredTextBoxExtender ID="mVisualImpairmentDegreeBoxE1" runat="server" TargetControlID="mVisualImpairmentDegreeBox" SkinID="NumbersCommasAndDots">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:UpdatePanel>
                    </td>
                    <td valign="top">
                        <ajax:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="Label51" runat="server" Text="Auditivas:"></asp:Label></td>
                                        <td align="left" valign="middle" runat="server" id="mEaringImpairmentDegreeBlock1">
                                            <asp:Label ID="mEaringImpairmentDegreeLabel" runat="server" Text="Grau:"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle">
                                            <asp:RadioButtonList ID="mHasEaringImpairmentBox" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="mHasEaringImpairmentBoxRFV" runat="server" ControlToValidate="mHasEaringImpairmentBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Titular tem diminuição de faculdades auditivas ou não."></asp:RequiredFieldValidator>
                                        </td>
                                        <td align="left" valign="middle" runat="server" id="mEaringImpairmentDegreeBlock2">
                                            <asp:TextBox ID="mEaringImpairmentDegreeBox" runat="server" MaxLength="8" Width="30px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="mEaringImpairmentDegreeBoxV1" runat="server" ControlToValidate="mEaringImpairmentDegreeBox" Display="None" ErrorMessage="Indique o Grau da Diminuição Auditiva." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="mEaringImpairmentDegreeBoxV2" runat="server" ControlToValidate="mEaringImpairmentDegreeBox" Display="None" ErrorMessage="O grau de diminuição auditiva não é válido." Operator="DataTypeCheck" SetFocusOnError="True" Type="Currency"></asp:CompareValidator>
                                            <ajax:FilteredTextBoxExtender ID="mEaringImpairmentDegreeBoxE1" runat="server" TargetControlID="mEaringImpairmentDegreeBox" SkinID="NumbersCommasAndDots">
                                            </ajax:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Recebe ou recebeu qualquer pensão ou indemnização a título de incapacidade física?  &nbsp;&nbsp;"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="mHasReceivedIndemnationBox" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="mHasReceivedIndemnationBoxRFV" runat="server" ControlToValidate="mHasReceivedIndemnationBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Titular recebe ou recebeu qualquer pensão ou indemnização a título de incapacidade física."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Sofre de qualquer doença que agrave a ocorrência de acidente?"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="mSuffersFromDiseaseBox" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                            <asp:ListItem Value="1">Sim</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="mSuffersFromDiseaseBoxRFV" runat="server" ControlToValidate="mSuffersFromDiseaseBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Titular sofre de qualquer doença que agrave a ocorrência de acidente."></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="mSpousePanel" runat="server" GroupingText="Cônjuge">
        <div>
            <ajax:UpdatePanel ID="mSpouseUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <app:DecimalHiddenField ID="mSpousePersonIDField" runat="server" Visible="false" />
                    <asp:Image ID="mSpouseImage" runat="server" SkinID="User" />
                    <asp:HyperLink ID="mSpousePersonalInfoButton" runat="server" SkinID="AddItem" ToolTip="Editar Detalhes do Cônjuge" NavigateUrl="javascript:void(0);"></asp:HyperLink>
                    <asp:Panel ID="mSpousePersonalInfoPopup" runat="server" SkinID="Popup">
                        <asp:ValidationSummary ID="mSpousePersonalInfoSummary" runat="server" ShowMessageBox="true" DisplayMode="BulletList" ValidationGroup="SpousePersonalInfo" ShowSummary="false" />
                        <table>
                            <tr>
                                <td>
                                    <ajax:UpdatePanel ID="mSpousePersonalInfoUpdatePanel" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <app:SpousePersonalInfo ID="mSpousePersonalInfo" runat="server" ValidationGroup="SpousePersonalInfo" />
                                        </ContentTemplate>
                                    </ajax:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="mSaveSpouseInfoButton" runat="Server" Text="Guardar" ValidationGroup="SpousePersonalInfo" OnClick="mSaveSpouseInfoButton_Click" /></td>
                                            <td>
                                                <asp:Button ID="mCloseSpouseInfoButton" runat="server" Text="Fechar" CausesValidation="false" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajax:ModalPopupExtender ID="mSpousePersonalInfoPopupExtender" runat="Server" TargetControlID="mSpousePersonalInfoButton" PopupControlID="mSpousePersonalInfoPopup" CancelControlID="mCloseSpouseInfoButton">
                    </ajax:ModalPopupExtender>
                </ContentTemplate>
            </ajax:UpdatePanel>
        </div>
        <ajax:UpdatePanel ID="mSpouseQuestionsUpdatePanel" runat="server" UpdateMode="Conditional">
            <Triggers>
                <ajax:AsyncPostBackTrigger ControlID="mSaveSpouseInfoButton" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="mSpouseQuestionsPanel" runat="Server">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label999" runat="server" Text="Nome:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="mSpouseNameShowLabel" runat="server" SkinID="Heavy"></asp:Label></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="É Canhoto ?"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="mSpouseIsLeftHandedBox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="mSpouseIsLeftHandedBoxRFV" runat="server" ControlToValidate="mSpouseIsLeftHandedBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Cônjuge é canhoto."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="Tem diminuição de faculdades:"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <ajax:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Label ID="Label42" runat="server" Text="Visuais:"></asp:Label></td>
                                                <td align="left" valign="middle" runat="server" id="mSpouseVisualImpairmentDegreeBlock1">
                                                    <asp:Label ID="mSpouseVisualImpairmentDegreeLabel" runat="server" Text="Grau:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:RadioButtonList ID="mSpouseHasVisualImpairmentBox" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="mSpouseHasVisualImpairmentBoxRFV" runat="server" ControlToValidate="mSpouseHasVisualImpairmentBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Cônjuge tem diminuição de faculdades visuais."></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" valign="middle" runat="server" id="mSpouseVisualImpairmentDegreeBlock2">
                                                    <asp:TextBox ID="mSpouseVisualImpairmentDegreeBox" runat="server" MaxLength="8" Width="30px"></asp:TextBox><asp:RequiredFieldValidator ID="mSpouseVisualImpairmentDegreeBoxV1" runat="server" ControlToValidate="mSpouseVisualImpairmentDegreeBox" Display="None" ErrorMessage="Indique o Grau de Diminuição Visual." SetFocusOnError="true"></asp:RequiredFieldValidator><asp:CompareValidator ID="mSpouseVisualImpairmentDegreeBoxV2" runat="Server" ControlToValidate="mSpouseVisualImpairmentDegreeBox" Display="None" ErrorMessage="O Grau de Diminuição Visual não é válido." Operator="DataTypeCheck" SetFocusOnError="true" Type="Currency"></asp:CompareValidator>
                                                    <ajax:FilteredTextBoxExtender ID="mSpouseVisualImpairmentDegreeBoxE1" runat="server" TargetControlID="mSpouseVisualImpairmentDegreeBox" SkinID="NumbersCommasAndDots">
                                                    </ajax:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                            </td>
                            <td valign="top">
                                <ajax:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:Label ID="Label52" runat="server" Text="Auditivas:"></asp:Label></td>
                                                <td align="left" valign="middle" runat="Server" id="mSpouseEaringImpairmentDegreeBlock1">
                                                    <asp:Label ID="mSpouseEaringImpairmentDegreeLabel" runat="server" Text="Grau:"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="middle">
                                                    <asp:RadioButtonList ID="mSpouseHasEaringImpairmentBox" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="mSpouseHasEaringImpairmentBoxRFV" runat="Server" ControlToValidate="mSpouseHasEaringImpairmentBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Cônjuge tem diminuição de faculdades auditivas."></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="left" valign="middle" runat="Server" id="mSpouseEaringImpairmentDegreeBlock2">
                                                    <asp:TextBox ID="mSpouseEaringImpairmentDegreeBox" runat="server" MaxLength="8" Width="30px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="mSpouseEaringImpairmentDegreeBoxV1" runat="server" ControlToValidate="mSpouseEaringImpairmentDegreeBox" Display="None" ErrorMessage="Indique o grau da diminuição auditiva." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="mSpouseEaringImpairmentDegreeBoxV2" runat="server" ControlToValidate="mSpouseEaringImpairmentDegreeBox" Display="None" ErrorMessage="O grau de diminuição auditiva não é válido." Operator="DataTypeCheck" SetFocusOnError="True" Type="Currency"></asp:CompareValidator>
                                                    <ajax:FilteredTextBoxExtender ID="mSpouseEaringImpairmentDegreeBoxE1" runat="server" TargetControlID="mSpouseEaringImpairmentDegreeBox" SkinID="NumbersCommasAndDots">
                                                    </ajax:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Recebe ou recebeu qualquer pensão ou indemnização a título de incapacidade física?  &nbsp;&nbsp;"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="mSpouseHasReceivedIndemnationBox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="mSpouseHasReceivedIndemnationBoxRFV" runat="server" ControlToValidate="mSpouseHasReceivedIndemnationBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Cônjuge recebe ou recebeu qualquer pensão ou indemnização a título de incapacidade física."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Sofre de qualquer doença que agrave a ocorrência de acidente?"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="mSpouseSuffersFromDiseaseBox" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                    <asp:ListItem Value="1">Sim</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="mSpouseSuffersFromDiseaseBoxRFV" runat="server" ControlToValidate="mSpouseSuffersFromDiseaseBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se o Cônjuge sofre de qualquer doença que agrave a ocorrência de acidente."></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </ajax:UpdatePanel>
    </asp:Panel>
    <br runat="server" visible="false" />
    <app:MortgageCreditor ID="mMortgageCreditor" runat="server" HeaderPanelSkinID="PersonalAccidents" Visible="false" />
    <br />
    <app:ProposalCommercialAccount ID="mProposalCommercialAccount" runat="server" HeaderPanelSkinID="PersonalAccidents" />
    <br />
    <asp:Panel ID="mBeneficiaryPanel" runat="server">
        <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" ImageControlID="Image5" ExpandControlID="Image5" CollapseControlID="Image5" TargetControlID="Panel7">
        </ajax:CollapsiblePanelExtender>
        <asp:Panel ID="Panel6" runat="server" SkinID="PersonalAccidents">
            <asp:Label ID="Label12" runat="server" SkinID="PanelHeader" Text="Beneficiários por Morte"></asp:Label>
            <asp:Image ID="Image5" runat="server" SkinID="PanelHeader" ImageUrl="~/Images/CollapsePanel_65x15.gif" />
        </asp:Panel>
        <asp:Panel ID="Panel7" runat="server">
            <asp:Panel ID="mHolderBeneficiaryPanel" runat="server" GroupingText="Titular">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label13" runat="server" Text="Herdeiros Legais do Titular ?"></asp:Label></td>
                        <td>
                            <ajax:UpdatePanel ID="mHolderHasLegalHeirsBoxUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="mHolderHasLegalHeirsBox" runat="server" Text="Sim" GroupName="HolderLegalHeirs" AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:RadioButton ID="mHolderHasNoLegalHeirsBox" runat="server" Text="Não" GroupName="HolderLegalHeirs" AutoPostBack="true" /></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajax:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <ajax:UpdatePanel ID="mHolderHeirListControlUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="mHolderHasLegalHeirsBox" EventName="CheckedChanged" />
                        <ajax:AsyncPostBackTrigger ControlID="mHolderHasNoLegalHeirsBox" EventName="CheckedChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="mHolderHeirListPanel" runat="Server">
                            <app:FamilyRentHeirListControl ID="mHolderHeirListControl" runat="server" HeirTarget="Holder" ValidationGroup="HolderHeirInfo" />
                        </asp:Panel>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </asp:Panel>
            <asp:Panel ID="mSpouseBeneficiaryPanel" runat="server" GroupingText="Cônjuge">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Herdeiros Legais do Cônjuge ?"></asp:Label></td>
                        <td>
                            <ajax:UpdatePanel ID="mSpouseHasLegalHeirsBoxUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="mSpouseHasLegalHeirsBox" runat="Server" Text="Sim" GroupName="SpouseLegalHeirs" AutoPostBack="true" /></td>
                                            <td>
                                                <asp:RadioButton ID="mSpouseHasNoLegalHeirsBox" runat="Server" Text="Não" GroupName="SpouseLegalHeirs" AutoPostBack="true" /></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajax:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <ajax:UpdatePanel ID="mSpouseHeirListUpdatePanel" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <Triggers>
                        <ajax:AsyncPostBackTrigger ControlID="mSpouseHasLegalHeirsBox" EventName="CheckedChanged" />
                        <ajax:AsyncPostBackTrigger ControlID="mSpouseHasNoLegalHeirsBox" EventName="CheckedChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="mSpouseHeirListPanel" runat="Server">
                            <app:FamilyRentHeirListControl ID="mSpouseHeirListControl" runat="server" HeirTarget="Spouse" ValidationGroup="SpouseHeirInfo" />
                        </asp:Panel>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <br />
    <ajax:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" ImageControlID="Image4" ExpandControlID="Image4" CollapseControlID="Image4" TargetControlID="Panel5">
    </ajax:CollapsiblePanelExtender>
    <asp:Panel ID="Panel4" runat="server" SkinID="PersonalAccidents">
        <asp:Label ID="Label5" runat="server" SkinID="PanelHeader" Text="Respostas Obrigatórias"></asp:Label>
        <asp:Image ID="Image4" runat="server" SkinID="PanelHeader" ImageUrl="~/Images/CollapsePanel_65x15.gif" />
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server">
        <fieldset>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Já esteve seguro?"></asp:Label></td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="mWasNotInsuredBox" runat="server" Text="Não" GroupName="WasInsured" /></td>
                                <td>
                                    <asp:RadioButton ID="mWasInsuredBox" runat="server" Text="Sim" GroupName="WasInsured" /></td>
                            </tr>
                        </table>
                        <app:GroupedRadioButtonRequiredFieldValidator ID="mWasInsuredBoxValidator" runat="server" GroupName="WasInsured" Display="None" SetFocusOnError="true" ErrorMessage="Indique se já esteve seguro."></app:GroupedRadioButtonRequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <ajax:CollapsiblePanelExtender ID="mWasInsuredDetailsPanelExtender" runat="server" TargetControlID="mWasInsuredDetailsPanel" CollapseControlID="mWasNotInsuredBox" ExpandControlID="mWasInsuredBox" Collapsed="true">
            </ajax:CollapsiblePanelExtender>
            <asp:Panel ID="mWasInsuredDetailsPanel" runat="server">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="mWasInsuredInCompanyLabel" runat="server" Text="Em que companhia?"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="mWasInsuredInCompanyBox" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="mWasInsuredInCompanyBoxRFV" runat="server" ControlToValidate="mWasInsuredInCompanyBox" ErrorMessage="Indique o nome da companhia em que esteve seguro." Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <app:ValidatorEnablerExtender ID="mWasInsuredInCompanyBoxRFVExtender" runat="server" TargetControlID="mWasInsuredInCompanyBoxRFV" RadioButtonControlID="mWasInsuredBox">
                            </app:ValidatorEnablerExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label100" runat="server" Text="Relativamente ao mesmo seguro existe qualquer débito por falta de pagamento de prémios ou fracção de prémios ?"></asp:Label></td>
                        <td>
                            <asp:RadioButtonList ID="mOldInsuranceIsUnpaidBox" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Value="0">N&#227;o</asp:ListItem>
                                <asp:ListItem Value="1">Sim</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="mOldInsuranceIsUnpaidBoxV1" runat="server" ControlToValidate="mOldInsuranceIsUnpaidBox" Display="None" SetFocusOnError="true" ErrorMessage="Indique se existe débito por falta de pagamento de prémios."></asp:RequiredFieldValidator>
                            <app:ValidatorEnablerExtender ID="mOldInsuranceIsUnpaidBoxV1Extender" runat="server" TargetControlID="mOldInsuranceIsUnpaidBoxV1" RadioButtonControlID="mWasInsuredBox">
                            </app:ValidatorEnablerExtender>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <br />
    <app:Notes ID="mNotes" runat="server" HeaderPanelSkinID="PersonalAccidents" />
    <br />
    <ajax:UpdatePanel ID="mCommandUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="margin-left: auto; margin-right: auto;">
                <tr valign="top">
                    <td>
                        <asp:Button ID="mSaveProposalButton" runat="server" Text="Gravar" OnClick="mSaveButton_Click" />
                        <asp:Button ID="mPrintProposalButton" runat="Server" Text="Imprimir Proposta" OnClick="mProposalButton_Click" />
                        <asp:Button ID="mSaveContractButton" runat="server" Text="Contrato" OnClick="mContractButton_Click" />
                        <asp:Button ID="mPrintContractButton" runat="server" Text="Imprimir Contrato" OnClick="mPrintContractButton_Click" />
                        <asp:Button ID="mPrintReceiptButton" runat="server" Text="Imprimir Recibo" OnClick="mPrintReceiptButton_Click" />
                        <asp:Button ID="mPrintAcceptanceSheetButton" runat="server" Text="Imprimir Folha De Aceitação" OnClick="mPrintAcceptanceSheetButton_Click" />
                        <app:SaveContractQuestions ID="mSaveContractQuestions" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="mBackToPlanButton" runat="server" Text="Voltar ao Plano" OnClick="mBackToPlanButton_Click" CausesValidation="false" />
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
        <Triggers>
            <ajax:AsyncPostBackTrigger ControlID="mSaveContractQuestions" EventName="Success" />
        </Triggers>
    </ajax:UpdatePanel>
    </div>
</asp:Content>
