<%@ Control Language="C#" AutoEventWireup="true" Inherits="FamilyRentHeirPersonalInfo" Codebehind="FamilyRentHeirPersonalInfo.ascx.cs" %>
<%@ Register Src="~/Controls/ProposalUserDetails.ascx" TagPrefix="app" TagName="ProposalUserDetails" %>
<div>
    <asp:HiddenField ID="mSimulationIDField" runat="Server" Visible="false" />
    <app:ProposalUserDetails ID="mProposalUserDetails" runat="server" ShowHeader="false" ShowContact="true" ShowExtraContact="false" RequireDriverLicense="false" AlowChangeOfNif="true" />
</div>