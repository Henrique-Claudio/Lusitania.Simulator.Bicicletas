<%@ Control Language="C#" AutoEventWireup="true" Inherits="Lusitania.Simuladores.WebSite.Controls.ProposalUserDetails"
    CodeBehind="ProposalUserDetails.ascx.cs" %>
<%@ Register Src="~/Controls/Person.ascx" TagPrefix="app" TagName="Person" %>


<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            1</div>
        <span class="title">Tomador do Seguro</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <app:Person id="mTomador" runat="server" IsSearchPanelVisible="false"></app:Person>
    </div>
</div>
