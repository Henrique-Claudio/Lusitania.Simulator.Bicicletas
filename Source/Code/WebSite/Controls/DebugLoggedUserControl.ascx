<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_DebugLoggedUserControl" Codebehind="DebugLoggedUserControl.ascx.cs" %>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="mIsNotLoggedOnRadioButton" runat="server" Text="Não Logado" AutoPostBack="True" Checked="False" GroupName="DebugLoggedUser" /></td>
        <td style="width: 3px">
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton ID="mIsLoggedOnRadioButton" runat="server" Text="Logado" AutoPostBack="True" Checked="true" GroupName="DebugLoggedUser" /></td>
        <td style="width: 3px">
            <asp:TextBox ID="mUserNameBox" runat="server" MaxLength="8">a0974700</asp:TextBox></td>
    </tr>
    <tr>
        <td>
        </td>
        <td style="width: 3px; text-align: right;">
            <asp:Button ID="mOKButton" runat="server" OnClick="mOKButton_Click" Text="OK" CausesValidation="False" /></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Utilizador:"></asp:Label></td>
        <td style="width: 3px; text-align: right">
            <asp:Label ID="mCurrentUserLabel" runat="server"></asp:Label></td>
    </tr>
</table>
