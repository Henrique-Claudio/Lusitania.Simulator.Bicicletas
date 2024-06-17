<%@ Control Language="C#" AutoEventWireup="true" Inherits="Lusitania.Simuladores.WebSite.Controls.AddressSearch" Codebehind="AddressSearch.ascx.cs" %>
<div>
    <ajax:UpdatePanel ID="mUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="margin-left: auto; margin-right: auto; text-align: center;">
                <asp:Label ID="HeaderLabel" runat="server" Text="Pesquisa de Moradas" SkinID="Heavy"></asp:Label></div>
            <asp:Panel ID="mCriteriaPanel" runat="server" DefaultButton="mRunSearchButton">
                <table>
                    <colgroup>
                        <col />
                        <col style="padding-left: 10px;" />
                        <col style="padding-left: 10px;" />
                        <col style="padding-left: 10px;" />
                    </colgroup>
                    <tr style="padding-top: 20px;">
                        <td style="width:90px;">
                            <asp:Label ID="ZipCodeLabel" runat="server" Text="Cod. Postal:"></asp:Label></td>
                        <td  style="width:220px;">
                            <asp:Label ID="AddressLabel" runat="server" Text="Morada:"></asp:Label></td>
                        <td style="width:150px;">
                            <asp:Label ID="LocalityLabel" runat="server" Text="Localidade:"></asp:Label></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle">
                            <table cellpadding="0" cellspacing="0" border="0">
                                <colgroup>
                                    <col />
                                    <col style="padding-left: 5px; padding-right: 5px;" />
                                    <col />
                                </colgroup>
                                <tr>
                                    <td valign="middle">
                                        <asp:TextBox ID="ZipCode4Box" runat="server" Width="40px" MaxLength="4"></asp:TextBox></td>
                                    <td valign="middle">
                                        -</td>
                                    <td valign="middle">
                                        <asp:TextBox ID="ZipCode3Box" runat="server" Width="20px" MaxLength="3"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:TextBox ID="AddressBox" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="LocalityBox" runat="server" Width="100px" MaxLength="100"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="mRunSearchButton" runat="server" Text="Pesquisar" CausesValidation="false" OnClick="mRunSearchButton_Click" SkinID="SimulateX4" /></td>
                        <td>
                            <ajax:UpdateProgress ID="mCriteriaUpdateProgress" runat="server" AssociatedUpdatePanelID="mUpdatePanel">
                                <ProgressTemplate>
                                    <asp:Image ID="mCriteriaUpdateImage" runat="server" SkinID="Wait" />
                                </ProgressTemplate>
                            </ajax:UpdateProgress>
                        </td>
                    </tr>
                </table>
                <ajax:FilteredTextBoxExtender ID="ZipCode4BoxExtender" runat="server" TargetControlID="ZipCode4Box" FilterType="Numbers">
                </ajax:FilteredTextBoxExtender>
                <ajax:FilteredTextBoxExtender ID="ZipCode3BoxExtender" runat="server" TargetControlID="ZipCode3Box" FilterType="Numbers">
                </ajax:FilteredTextBoxExtender>
            </asp:Panel>
            <br />
            <ajax:UpdatePanel ID="mSearchResultsGridViewUpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="mSearchResultsGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" PagerStyle-ForeColor="#00745E" SkinID="SelectableSearchResult" DataSourceID="mSearchDataSource" AllowSorting="True" DataKeyNames="ROWID" OnRowCommand="mSearchResultsGridView_RowCommand" EmptyDataText="Não foram encontradas moradas que correspondam à sua pesquisa." OnRowDataBound="mSearchResultsGridView_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="TIPO_ART" HeaderText="Tipo" SortExpression="TIPO_ART">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#00745E" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TITULO_ART" HeaderText="Titulo" SortExpression="TITULO_ART">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center"  ForeColor="#00745E"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="ARTERIA" HeaderText="Rua" SortExpression="ARTERIA">
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                                <HeaderStyle HorizontalAlign="Center"  ForeColor="#00745E"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LOCAL" HeaderText="Local" SortExpression="LOCAL">
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                <HeaderStyle HorizontalAlign="Center"  ForeColor="#00745E"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="LUGAR" HeaderText="Lugar" SortExpression="LUGAR">
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#00745E"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="CP4" HeaderText="CP4" SortExpression="CP4">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center"  ForeColor="#00745E"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="CP3" HeaderText="CP3" SortExpression="CP3">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle HorizontalAlign="Center"  ForeColor="#00745E"/>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </ajax:UpdatePanel>
            <asp:HiddenField ID="SearchParamZipCode4" runat="server" Visible="false" />
            <asp:HiddenField ID="SearchParamZipCode3" runat="server" Visible="false" />
            <asp:HiddenField ID="SearchParamAddress" runat="server" Visible="false" />
            <asp:HiddenField ID="SearchParamLocality" runat="server" Visible="false" />
            <asp:ObjectDataSource ID="mSearchDataSource" runat="server" EnableCaching="True" CacheDuration="300" CacheExpirationPolicy="Absolute" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.SABER_ENDERECOS_CTTTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SearchParamZipCode4" Name="A_CP4" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:ControlParameter ControlID="SearchParamZipCode3" Name="A_CP3" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:ControlParameter ControlID="SearchParamAddress" Name="A_ARTERIA" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:ControlParameter ControlID="SearchParamLocality" Name="A_LUGAR" PropertyName="Value" Type="String" ConvertEmptyStringToNull="true" />
                    <asp:Parameter Direction="Output" Name="A_CURSOR" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="mAddressTypeDataSource" runat="server" EnableCaching="True" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.SABER_TIPO_MORADASTableAdapter" OnSelecting="mAddressTypeDataSource_Selecting">
                <SelectParameters>
                    <asp:Parameter Direction="Output" Name="A_CURSOR" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </ajax:UpdatePanel>
</div>
