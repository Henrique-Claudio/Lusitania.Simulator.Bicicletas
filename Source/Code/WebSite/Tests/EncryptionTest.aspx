<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EncryptionTest.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Tests.EncryptionTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td><asp:Label ID="mPasswordLabel" runat="server" Text="Password:"></asp:Label></td>
                <td><asp:TextBox ID="mPasswordBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="mClearTextLabel" runat="server" Text="Input:"></asp:Label></td>
                <td><asp:TextBox ID="mClearTextBox" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="mEncryptButton" runat="server" Text="Encrypt" OnClick="mEncryptButton_Click" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="mEncryptedTextLabel" runat="server" Text="Encrypted:"></asp:Label></td>
                <td><asp:TextBox ID="mEncryptedTextBox" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="mDecryptButton" runat="server" Text="Decrypt" OnClick="mDecryptButton_Click" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="mDecryptedTextLabel" runat="server" Text="Decrypted:"></asp:Label></td>
                <td><asp:TextBox ID="mDecryptedTextBox" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
