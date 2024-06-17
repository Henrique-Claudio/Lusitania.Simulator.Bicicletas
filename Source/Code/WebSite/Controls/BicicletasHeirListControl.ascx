<%@ Control Language="C#" AutoEventWireup="true" Inherits="BicicletasHeirListControl"
    CodeBehind="BicicletasHeirListControl.ascx.cs" %>
<%@ Register Src="~/Controls/Person.ascx" TagPrefix="app" TagName="Person" %>
<ajax:updatepanel id="ItemsGridViewUpdatePanel" runat="server" updatemode="Always">        
        <ContentTemplate>
            <asp:GridView ID="ItemsGridView" runat="server" AutoGenerateColumns="false" OnRowCommand="ItemsGridView_RowCommand" DataKeyNames="Ordem" CssClass="Herdeiros" >
                <Columns>                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="mUserImage" runat="Server" SkinID="User" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField  DataField="ORDEM" headertext="Ordem" ControlStyle-Width="80" />
                     <asp:BoundField  DataField="PNOME" headertext="Nome" ControlStyle-Width="300" />
                    <asp:TemplateField HeaderText="Parentesco">
                        <ItemTemplate>
                            <asp:DropDownList ID="mParentesco" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="mParentesco_SelectedIndexChanged" SelectedValue='<%# Eval("PARENTESCO") %>' 
                                DataTextField="Descritivo" DataValueField="Codigo" DataSource='<%# ListaParentesco() %>'  >
                            </asp:DropDownList>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:ImageButton ID="RemoveButton" runat="server" ToolTip="Remover" CommandName="RemoveItem" 
                                CausesValidation="false" CommandArgument='<%# Eval("Ordem") %>' SkinID="RemoveItem" /></ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="row" id="conAddButtons" runat="server">  
                <div style="width: 100px;text-align: center;border: solid 1px #00A380;height: 30px;line-height: 35px;background-color: rgba(0,163,128,0.1);">
                    <asp:Image ID="mImage2387" runat="server" SkinID="User" />
                    <%--asp:ImageButton ID="mAddButton" runat="Server" SkinID="AddItem" ToolTip="Adicionar"  OnClick="AddHeirButton_Click" CausesValidation="false" /--%>
                    <asp:LinkButton  ID="mAddButton" runat="server" ToolTip="Adicionar"  OnClick="AddHeirButton_Click" CausesValidation="false">
                        <asp:Image ID="mImgAddButton" runat="server" SkinID="AddItem" />
                    </asp:LinkButton>
                </div>
            </div>
            
            <div class="row" id="conPerson" runat="server" visible="false">  
                <app:Person id="mPersonPH" runat="server"/>
                
                <table class="Layout" style="margin-top:20px;">
                    <tr>
                        <td>
                            <asp:Button ID="SaveHeirButton" runat="server" Text="Adicionar"  CausesValidation="false" OnClick="SaveHeirButton_Click" SkinID="ButtonSimular"/>
                        </td>
                        <td>
                            <asp:Button ID="CancelHeirButton" runat="server" Text="Cancelar" CausesValidation="false"  OnClick="CancelHeirButton_Click" SkinID="ButtonPropostas"/>
                        </td>
                    </tr>
                </table>
                
            </div>    

        </ContentTemplate>
    </ajax:updatepanel>
