<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BicicletasPlan.ascx.cs"
    Inherits="BicicletasPlan" %>
<asp:GridView ID="mGridCoberturas" runat="server" AutoGenerateColumns="false" CssClass="Coberturas">
    <Columns>
        <asp:TemplateField HeaderText="Coberturas">
            <ItemTemplate>
                
                <asp:Label ID="Cobertura" runat="Server" Text='<%# Eval("Cobertura") %>' />
                <span class="Icon-InfoBox" infoBoxTitle="Assistência em Viagem a Pessoas" infoBox="InfoAssistEmViagem" runat="server" visible='<%# ((string)Eval("CodCobertura") == "005") %>' title="ver detalhe"></span>                        

            </ItemTemplate>
        </asp:TemplateField>        
        <asp:BoundField DataField="00006" HeaderText="Light" />
        <asp:BoundField DataField="00007" HeaderText="Super" />
    </Columns>
</asp:GridView>
