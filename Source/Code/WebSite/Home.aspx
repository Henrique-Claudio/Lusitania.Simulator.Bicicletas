<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Home.aspx.cs" Inherits="Lusitania.Simuladores.WebSite.Home"
MasterPageFile="~/Pages/Simulators.Master" %>
<%@ Register Src="~/Controls/DebugLoggedUserControl.ascx" TagPrefix="app" TagName="DebugLoggedUserControl" %>
<asp:Content runat="server" ID="HomeContent" ContentPlaceHolderID="BodyContent">
 <ajax:UpdatePanel ID="hDebugLoggedUserControlUpdatePanel" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="hDebugLoggedUserControlPanel" runat="server" SkinID="TopRightOverlay">
            <app:DebugLoggedUserControl ID="hDebugLoggedUserControl" runat="server" />
        </asp:Panel>
    </ContentTemplate>
</ajax:UpdatePanel>
</asp:Content>