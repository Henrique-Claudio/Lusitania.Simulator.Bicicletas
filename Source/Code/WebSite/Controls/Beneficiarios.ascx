<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Beneficiarios.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.Beneficiarios" %>
<%@ Register Src="~/Controls/BicicletasHeirListControl.ascx" TagPrefix="app" TagName="BicicletasHeirList" %>
<ajax:updatepanel id="mHolderHeirsUpdatePanel" runat="server" updatemode="Always">
    <ContentTemplate>
            <!-- Tem Beneficiário -->
            <h4> Beneficiários por Morte</h4>
            <div class="row">
                <table class="Layout">
				    <tr>
					    <td class="TitleBold">
						    Herdeiros Legais do Titular
                        </td>
					    <td>
                            <asp:radiobuttonlist runat="server" ID="mHasLegalHeirsBox" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:listitem text="Sim" Value="S" />
                                <asp:listitem text="Não" Value="N"/>
                            </asp:radiobuttonlist>						
					    </td>
				    </tr>
			    </table>
            </div>

             <!-- TIPO -->
            <div class="row" ID="conDiscriminatePH" runat="server" visible="false">
                <table class="Layout">
				    <tr>
					    <td class="TitleBold">
						    Tipo Beneficiário
                        </td>
					    <td>
                            <asp:radiobuttonlist runat="server" ID="mHasDiscriminate" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:listitem text="A Discriminar" Value="1" />
                                <asp:listitem text="Cláusula Genérica" Value="2"/>
                            </asp:radiobuttonlist>						
					    </td>
				    </tr>
			    </table>
            </div>
         
         <!-- Herdeiros -->				
		<div ID="conHeirdeiros" runat="server" visible="false" class="row">
			<app:BicicletasHeirList id="mBicicletasHeirList" runat="server"></app:BicicletasHeirList>
		</div>   
			
        <!-- Clausula -->				
		<div ID="conClausulaPH" runat="server" visible="false" class="row">
			<table>
				<tr>
					<td style="vertical-align:middle;padding-right:20px;">
						Cláusula Genérica
					</td>
					<td>
						<asp:textbox id="mClausulaGenerica" width="540" runat="server" maxlength="500" textmode="MultiLine" rows="7" height="90px" Style="border:1px solid #9A9A9A; margin-left: auto; margin-right: auto;" CssClass="Box"></asp:textbox>
					</td>
				</tr>
			</table>
		</div>
			
		
	</ContentTemplate>
</ajax:updatepanel>
