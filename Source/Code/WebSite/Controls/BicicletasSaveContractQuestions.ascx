<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="BicicletasSaveContractQuestions" Codebehind="BicicletasSaveContractQuestions.ascx.cs" %>
<%@ Register TagPrefix="app" TagName="SaveContractQuestions" Src="~/Controls/SaveContractQuestions.ascx" %>
<div>
    <ajax:UpdatePanel ID="mUpdatePanel" runat="Server" UpdateMode="Conditional">
        <ContentTemplate>
            <app:SaveContractQuestions ID="mSaveContractQuestions" runat="Server" />
        </ContentTemplate>
    </ajax:UpdatePanel>
</div>