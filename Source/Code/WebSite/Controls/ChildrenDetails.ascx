<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChildrenDetails.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.ChildrenDetails" %>
<%@ Register TagPrefix="app" TagName="ProposalUserDetails" Src="~/Controls/ProposalUserDetails.ascx" %>
<asp:Panel ID="PersonalInfoPanel" runat="server" GroupingText="">
    <span class="TitleGreen">Dados Pessoais</span>
    <app:ProposalUserDetails ID="uctDetalhesPessoaSegura" runat="Server" RequireDriverLicense="false" HeaderPanelSkinID="ChildrenProtectionPanelHeader" RequireMinimumAge="true" />
    <br />
    <table>
		<tr>
			<td>
				<table cellpadding="0" cellspacing="0" border="0"  style="margin-bottom:15px;"> 
					<tr>
						<td style="padding:5px 0 2px 0;">
							<asp:Label ID="ParentLabel" runat="server" Text="Parentesco c/ Tomador" SkinID="LabelBold" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:DropDownList ID="ParentDrop" runat="server" DataSourceID="ObjectDataSource1"
								DataTextField="DESCRITIVO" DataValueField="CODIGO" AutoPostBack="true">
							</asp:DropDownList>
							<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
								SelectMethod="GetData" TypeName="Lusitania.Simuladores.DataLayer.ChildrenProtectionDSTableAdapters.GETPARENTESCOTableAdapter">
								<SelectParameters>
									<asp:Parameter Direction="Output" Name="A_CURSOR" Type="Object" />
								</SelectParameters>
							</asp:ObjectDataSource>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br />
</asp:Panel>

