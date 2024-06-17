<%@ Page Language="C#" AutoEventWireup="true" Codebehind="WebForm1.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Tests.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ajax:ScriptManager ID="mScriptManager" runat="server"></ajax:ScriptManager>
            <asp:TextBox ID="mTextBox1" runat="server"></asp:TextBox>
            <asp:TextBox ID="mTextBox2" runat="server"></asp:TextBox>
            <app:TextBoxFocusChangerExtender ID="mExtender" runat="server" TargetControlID="mTextBox1" NextControlID="mTextBox2" MaxLength="3"></app:TextBoxFocusChangerExtender>
        </div>
    </form>
</body>
</html>
