<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Tests.WebForm2" Title="Untitled Page" %>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="XPTO" Checked="true" />
    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="XPTO" Checked="false" />
    <app:UpdatePanelTriggerExtender ID="RadioButton1E" runat="server" TargetControlID="RadioButton1" UpdatePanelControlID="UpdatePanel1"></app:UpdatePanelTriggerExtender>
    <app:UpdatePanelTriggerExtender ID="RadioButton2E" runat="server" TargetControlID="RadioButton2" UpdatePanelControlID="UpdatePanel1"></app:UpdatePanelTriggerExtender>
    <ajax:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:LinkButton ID="XPTO" runat="server" Text="XPTO"></asp:LinkButton>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>