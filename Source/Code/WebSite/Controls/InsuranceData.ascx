<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsuranceData.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.InsuranceData" %>
<%@ Register Src="~/Controls/CommercialAccount.ascx" TagPrefix="app" TagName="CommercialAccount" %>


<script type="text/javascript">
    function ValidarDataVencimento(sender, args) {
        var dia = document.getElementById("<%= mDataVencimentoDayBox.ClientID %>").value;
        var mes = document.getElementById("<%= mDataVencimentoMonthddl.ClientID %>").value;
        if (ValidarDiaDoMes(mes, dia) == false) args.IsValid = false;
    }

    function ValidarDiaDoMes(mes, dia) {

        var _dia = parseInt(dia, 10);
        var _mes = parseInt(mes, 10);

        switch (_mes) {
            // Meses 31 dias.               
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                if (_dia < 1 || _dia > 31 || isNaN(_dia)) {
                    return false;
                }
                break;
            // Meses 30 dias.                
            case 4:
            case 6:
            case 9:
            case 11:
                if (_dia < 1 || _dia > 30 || isNaN(_dia)) {
                    return false;
                }
                break;
            // Meses 28/29 dias.              
            case 2:
                if (_dia < 1 || _dia > 29 || isNaN(_dia)) {
                    return false;
                }
                else {
                    if (_dia == 29 && !(_dia == parseInt(document.getElementById('<%=tbDateBegin.ClientID%>').value.substring(0, 2), 10) && _mes == parseInt(document.getElementById('<%=tbDateBegin.ClientID%>').value.substring(3, 5), 10))) {
                        return false;
                    }
                }
                break;
            default:
                return false;
                break;
        }
        return true;
    }

</script>
<asp:Panel ID="mInsuranceData" runat="server">

    <div class="CollapsiblePanel">
        <div class="PanelHeader clearfix">
            <div class="order">
                1</div>
            <span class="title">Dados do Seguro</span>
            <img src="Images/hide.gif" title="Ocultar">
        </div>
        <div class="PanelBody clearfix">
            <asp:Panel ID="mContentPanel" runat="server">
                <ajax:updatepanel id="mOptionsUpdatePanel" runat="server" updatemode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr class="TitleBold" >
                                    <td >                                        
                                        <asp:Literal ID="ltlDateBegin" runat="server" Text="<%$ Resources:Bicicletas, ltlDateBegin %>"  />
                                        
                                    </td> 
                                    <td style="width:50px" rowspan="2"></td>                                   
                                    <td>                                        
                                        <asp:Label ID="Label16" Text="Data de Vencimento" runat="server" Visible="true" />
                                    </td>
                                </tr>                                
                                <tr valign="baseline">                                
                                    <td>                                        
                                        <asp:TextBox ID="tbDateBegin" runat="server"  placeholder="dd-mm-aaaa" MaxLength="10" format="date"  Width="100" AutoPostBack="True" OnTextChanged="tbDateBegin_OnTextChanged" ValidationGroup="Simulator_Validations" TabIndex="1"/>                                        
                                        
                                    </td>                                    
                                    <td>
                                        <div>
                                            <div style="width: auto; padding-top:5px; font-size: 13px;">                                                
                                                <asp:TextBox ID="mDataVencimentoDayBox" runat="server" Columns="2" MaxLenght="2" format="number"
                                                    OnTextChanged="mDataVencimentoDayBox_TextChanged" AutoPostBack="true" SkinID="CaixaInput" ValidationGroup="Simulator_Validations" TabIndex="2"></asp:TextBox>                                                
                                                <asp:Label ID="Label17" runat="server" Text="/"></asp:Label>
                                                <asp:DropDownList ID="mDataVencimentoMonthddl" SkinID="CaixaInput" style="height:21px" runat="server" AutoPostBack="true" ValidationGroup="Simulator_Validations" OnSelectedIndexChanged="mDataVencimentoMonthddl_SelectedIndexChanged" TabIndex="3">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>                                   
                                </tr>
                            </table>
                                             
                        </ContentTemplate>                        
                    </ajax:updatepanel>
            </asp:Panel>
        </div>
    </div>

</asp:Panel>
