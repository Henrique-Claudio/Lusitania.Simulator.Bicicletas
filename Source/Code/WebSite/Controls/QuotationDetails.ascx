<%@ Control Language="C#" AutoEventWireup="true" Inherits="QuotationDetails" CodeBehind="QuotationDetails.ascx.cs" %>
<div>
    <script language="javascript">
        function SaberNIF() {
            var oNif = document.getElementById("<%=SimulationNifClientID %>");
            return oNif.value
        }
        function ErrorNIF() {
            var oNif = document.getElementById("<%=ErrorNIFClientID %>");
            return oNif.value
        }
        function HasAlertSet() {
            var oAlert = document.getElementById("<%=HasAlertSetClientID %>");
            return oAlert.value
        }
        function ClearError() {
            document.getElementById("<%=ErrorNIFClientID %>").value = "";
        }
        function ClearAlert() {
            document.getElementById("<%=HasAlertSetClientID %>").value = "";
        }

        //Réplica do que existe na BD
        function IsNIFValid(nif) {
            var v_acum;
            var v_resto;
            var v_check_digit;
            var v_dig_nif;

            /* Lista de alguns NIFs inválidos mas que o checkdigit bate certo */
            if (nif == "000000000" || nif == "111111110" || nif == "123456789" || nif == "188888888" || nif == "500000000" || nif == "999999990")
                return false;

            /*	Se o identificador tem um comprimento diferente de nove caracteres dá erro */
            if (nif.length != 9)
                return false;

            /* Se o identificador contém caracteres que não são numericos dá erro */
            try {
                v_acum = parseInt(nif);
            }
            catch (ex) {
                return false;
            }

            /* Verifica a integridade do NIF por cálculo do checkdigito se invalido dá erro */
            var i = 0;
            var j = 9;
            v_acum = 0;
            while (i < 8) {
                v_dig_nif = nif.substr(i, 1);
                v_acum += parseInt(v_dig_nif) * j;
                i = i + 1;
                j = j - 1;
            }
            v_resto = v_acum % 11;
            if (v_resto < 2) {
                v_resto = 0;
                v_check_digit = 0;
            }
            else
                v_check_digit = 11 - v_resto;

            if (v_check_digit == parseInt(nif.substr(8, 1))) {
                return true;
            }
            else {
                return false;
            }

        }
    </script>
    <ajax:scriptmanagerproxy id="mScriptManagerProxy" runat="server">
        <Scripts>
            <ajax:ScriptReference Path="QuotationDetails.js" />
        </Scripts>
    </ajax:scriptmanagerproxy>
    <div class="CollapsiblePanel">
        <div class="PanelHeader clearfix">
            <div class="order">
                4</div>
            <span class="title">Dados Pessoais</span>
            <img src="Images/hide.gif" title="Ocultar">
        </div>
        <div class="PanelBody clearfix">
            <ajax:updatepanel id="mUpdatePanel" runat="server" updatemode="Conditional">
                    <ContentTemplate>
                        <table class="Layout">
                            <tr>
                                
                                <td  class="TitleBold">
                                    <asp:Label ID="mNameLabel" runat="server" Text="Nome"></asp:Label></td>
                                <td  class="TitleBold">
                                    <asp:Label ID="mPhoneNumberLabel" runat="server" Text="Telefone" ></asp:Label></td>
                                <td  class="TitleBold">
                                    <asp:Label ID="mTaxNumberLabel" runat="server" Text="NIF"></asp:Label></td>
                                <td  class="TitleBold" id="EmailCollumnTitle" runat="server">
                                    <asp:Label ID="mEmailLabel" runat="server" Text="E-Mail" ></asp:Label></td>
                            </tr>
                            <tr>
                                
                                <td >
                                    <asp:TextBox ID="mNameBox" runat="server" Width="390" MaxLength="100">
                                    </asp:TextBox></td>
                                <td >
                                    <asp:TextBox ID="mPhoneNumberBox" runat="server" Width="100" MaxLength="<%$ AppSettings:TextBox.MaxLength.PhoneNumber %>"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="mPhoneNumberBoxExtender1" runat="server" TargetControlID="mPhoneNumberBox"
                                        FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                                    </ajax:FilteredTextBoxExtender>
                                </td>
                                <td >
                                    <asp:TextBox ID="mTaxNumberBox" ValidationGroup="Proposal" CausesValidation="false" runat="server" Width="100"  MaxLength="<%$ AppSettings:TextBox.MaxLength.Nif %>"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="mTaxNumberBoxExtender1" runat="server" TargetControlID="mTaxNumberBox"
                                        FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                                    </ajax:FilteredTextBoxExtender>
                                    <asp:CustomValidator ID="mTaxNumberBoxValidator" ValidationGroup="Proposal" runat="server" TargetControlID="mTaxNumberBox" Display="None" ClientValidationFunction="CheckFields"></asp:CustomValidator>
                                </td>
                                <td id="EmailCollumnField" runat="server">
                                    <asp:TextBox ID="mEmailBox" runat="server" Width="160" MaxLength="100" AutoPostBack="True"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="valRegEx" runat="server" ControlToValidate="mEmailBox"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Deve inserir um endereço de email válido."
                                        Display="none">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>                       
                        </table>                        
                        <asp:HiddenField ID="mErrorNIFBox" runat="server" />
                        <asp:HiddenField ID="mHasAlertSet" runat="server" />
                    </ContentTemplate>
                </ajax:updatepanel>
        </div>
    </div>
