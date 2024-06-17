<%@ Page Language="C#" MasterPageFile="~/Pages/Simulators.Master" AutoEventWireup="true" CodeBehind="AddressSearch.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Tests.AddressSearch" Title="Untitled Page" %>
<%@ Register TagPrefix="app" TagName="AddressSearch" Src="~/Controls/AddressSearch.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContent" runat="server">
    <app:AddressSearch ID="mAddressSearch" runat="server" />
</asp:Content>
