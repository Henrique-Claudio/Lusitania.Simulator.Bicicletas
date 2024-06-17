<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Tests.UserDetails" Title="Untitled Page" %>
<%@ Register TagPrefix="app" TagName="UserDetails" Src="~/Controls/ProposalUserDetails.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ValidationSummary ID="mSummary" runat="server" ValidationGroup="XPTO" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"  EnableClientScript="false"/>
    <app:UserDetails ID="mUserDetails" runat="server" ValidationGroup="XPTO" RequireDriverLicense="false" />
    <asp:Button ID="mTestButton" runat="server" Text="Teste" OnClick="mTestButton_Click" ValidationGroup="XPTO" />
</asp:Content>
