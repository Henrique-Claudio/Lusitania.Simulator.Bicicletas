<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BancSearch.ascx.cs" Inherits="Lusitania.Simuladores.WebSite.Controls.BancSearch" %>
<div>
    <ajax:UpdatePanel ID="mUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin-left: auto; margin-right: auto; text-align: left;">
                <asp:Label ID="HeaderLabel" runat="server" Text="Pesquisa de BIC/SWIFT CODE" SkinID="HeavyBlack"></asp:Label></div>
            <asp:Panel ID="mCriteriaPanel" runat="server" DefaultButton="mRunSearchButton">
                <table class="Layout">                    
                    <tr >
                        <td class="TitleBold">
                            Nome do Banco
                        </td>
                        <td>
                            <asp:TextBox ID="mNomeBanco" runat="server" Width="350px" MaxLength="50" SkinID="CaixaInput"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="mRunSearchButton" runat="server" Text="Pesquisar" SkinID="ButtonSimular" CausesValidation="false" OnClick="mRunSearchButton_Click" /></td>
                        </td>
                        <td>
                            <ajax:UpdateProgress ID="mCriteriaUpdateProgress" runat="server" AssociatedUpdatePanelID="mUpdatePanel">
                                <ProgressTemplate>
                                    <asp:Image ID="mCriteriaUpdateImage" runat="server" ImageUrl="~/Images/StatusAnimation.gif" />
                                </ProgressTemplate>
                            </ajax:UpdateProgress>
                        </td>
                    </tr>
                </table>                
            </asp:Panel>
            <br />
            <asp:HiddenField ID="SearchParamDescritivoBanco" runat="server" Visible="false" />
            <ajax:UpdatePanel ID="mSearchResultsGridViewUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:GridView ID="mSearchResultsGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="mSearchResultsGridView_PageIndexChanging" SkinID="SelectableSearchResult" DataSourceID="mSearchDataSource" AllowSorting="True" DataKeyNames="ROWID" OnRowCommand="mSearchResultsGridView_RowCommand" EmptyDataText="Não foram encontrados bancos que correspondam à sua pesquisa." OnRowDataBound="mSearchResultsGridView_RowDataBound" CssClass="Bancos">
                        <Columns>
                            <asp:BoundField DataField="NOME_BANCO" HeaderText="Nome do Banco" SortExpression="NOME_BANCO">
                                <ItemStyle HorizontalAlign="Left" Width="300px" Font-Names="Arial" />
                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Font-Size="10" Font-Bold="true" Font-Names="Arial" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BIC" HeaderText="BIC/SWIFT CODE" SortExpression="BIC">
                                <ItemStyle HorizontalAlign="Left" Width="100px" Font-Names="Arial" />
                                <HeaderStyle HorizontalAlign="Left" ForeColor="Black" Font-Size="10" Font-Bold="true" Font-Names="Arial" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </ajax:UpdatePanel>
            <asp:ObjectDataSource ID="mSearchDataSource" runat="server" EnableCaching="True" CacheDuration="300" CacheExpirationPolicy="Absolute" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.SABER_BANCOSTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SearchParamDescritivoBanco" Name="P_DESCRITIVO" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Direction="Output" Name="P_CURSOR" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </ajax:UpdatePanel>
</div>
