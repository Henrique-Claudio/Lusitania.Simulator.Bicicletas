<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ModeloColaborativo.ascx.cs"
    Inherits="Lusitania.Simuladores.WebSite.Controls.ModeloColaborativo" %>
<table border="0" runat="server" width="100%" id="Table1">
    <tr>
        <td align="center">
            <asp:Button ID="BtnQueroContratar" Style="display: none" runat="server" Text="Quero subscrever já!&#010;Liguem-me!"
                OnClick="ButtonQueroContratar_Click" Enabled="True" Height="50px" Width="200px"
                BackColor="#FFA000" CssClass="botao" Font-Names="Verdana" Font-Size="Small" />
        </td>
        <td align="center">
            <asp:Button ID="BtnTenhoDuvidas" Style="display: none" runat="server" Text="Gostaria de saber mais!&#010;Liguem-me!"
                OnClick="ButtonTenhoDuvidas_Click" Enabled="True" Height="50px" Width="200px"
                BackColor="#FFA000" CssClass="botao" Font-Names="Verdana" Font-Size="Small" />
        </td>
        <td align="center">
            <asp:Button ID="BtnEnviarMediador" Style="display: none" runat="server" Text="Escolha um Mediador Lusitania&#010;para melhor o acompanhar."
                OnClick="ButtonEnviarMediador_Click" Enabled="True" Height="50px" Width="250px"
                BackColor="#FFA000" CssClass="botao" Font-Names="Verdana" Font-Size="Small" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblInfo" Style="display: none" runat="server"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table border="0" runat="server" width="100%" id="dvCallBack" style="display: none">
    <tr>
        <td>
            <fieldset title="Dados de contacto para Mediador">
                <legend style="color: #ffa000; font-size: 11px; font-weight: bold;">Dados de contacto
                    para Mediador</legend>
                <table border="0" width="100%">
                    <tr>
                        <td class="label" align="center" style="height: 30px; font-size: x-small;">
                            Pelo Agente:
                            <asp:DropDownList ID="cboAgenteCallBack" AutoPostBack="true" runat="server" CssClass="cboText"
                                OnSelectedIndexChanged="PreencherAgenteCallBackDetalhe" />
                            <asp:TextBox ID="txtDataCallBack" runat="server" CssClass="txtText" MaxLength="10"
                                Width="90px" Visible="False"></asp:TextBox>
                            <asp:TextBox ID="txtHoraCallBack" runat="server" CssClass="txtText" MaxLength="5"
                                Width="50px" Visible="False"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label" align="left" style="height: 45px">
                            <fieldset title="Contactos do Mediador" style="font-size: x-small">
                                <legend style="color: #ffa000; font-size: 11px; font-weight: bold;">Contactos do Mediador</legend>
                                Rua &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;Nº
                                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;Andar &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp; Código Postal&nbsp;&nbsp; &nbsp; Localidade&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
                                <asp:TextBox ID="txtMoradaAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="300px" Enabled="false"></asp:TextBox><asp:TextBox ID="txtNumeroMoradaAgente"
                                        runat="server" CssClass="txtText" MaxLength="50" Width="50px" Enabled="false"></asp:TextBox><asp:TextBox
                                            ID="txtAndarMoradaAgente" runat="server" CssClass="txtText" MaxLength="50" Width="50px"
                                            Enabled="false"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtCP4MoradaAgente" runat="server" CssClass="txtText"
                                    MaxLength="50" Width="50px" Enabled="false"></asp:TextBox><asp:TextBox ID="txtCP3MoradaAgente"
                                        runat="server" CssClass="txtText" MaxLength="50" Width="30px" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtLocalidadeMoradaAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="150px" Enabled="false"></asp:TextBox><br />
                                Telefone &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Telefone
                                Trabalho &nbsp;&nbsp; &nbsp; &nbsp;Email&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<br />
                                <asp:TextBox ID="txtTelefoneAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="120px" Enabled="false"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtTelefoneTrabAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="120px" Enabled="false"></asp:TextBox>
                                <asp:TextBox ID="txtEmailAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="300px" Enabled="false"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="txtTelemovelAgente" runat="server" CssClass="txtText" MaxLength="50"
                                    Width="120px" Enabled="false" Visible="False"></asp:TextBox></fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="label" style="height: 45px">
                            <asp:Button ID="BtnSubmitCallback" runat="server" OnClick="ButtonSubmitCallback_Click"
                                Text="Confirmar" OnClientClick="return confirm('A simulação que realizou, bem como os seus dados pessoais\nserão disponibilizados ao Mediador Lusitania seleccionado, e \nque o contactará tão breve quanto possível.\nObrigado por preferir os nossos serviços.');" />
                            <asp:Button ID="BtnCancelCallback" runat="server" OnClick="ButtonCancelCallback_Click"
                                Text="Cancelar" />
                        </td>
                    </tr>
                </table>
                &nbsp;</fieldset>
        </td>
    </tr>
</table>
