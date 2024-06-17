<%@ Control Language="C#" AutoEventWireup="true" Inherits="Lusitania.Simuladores.WebSite.Controls.Notes"
    CodeBehind="Notes.ascx.cs" %>
<div class="CollapsiblePanel">
    <div class="PanelHeader clearfix">
        <div class="order">
            6</div>
        <span class="title">Observações</span>
        <img src="Images/hide.gif" title="Ocultar">
    </div>
    <div class="PanelBody clearfix">
        <ajax:updatepanel id="UpdatePanel" runat="server" updatemode="Always">					
		    <ContentTemplate>
                <asp:TextBox ID="mNotesBox" runat="server" TextMode="MultiLine" Width="840" Style="margin-left: auto;
                    margin-right: auto;" Height="50" onKeyUp="LimitLengthKeyUp(this, 256)" onblur="LimitLengthKeyUp(this, 256)"
                    AutoPostBack="true">
                </asp:TextBox>
            </ContentTemplate>
        </ajax:updatepanel>
    </div>
</div>
<script type="text/javascript">
    function LimitLengthKeyUp(field, maxlimit) {
        if (field.value.length > maxlimit) {
            field.value = field.value.substring(0, maxlimit);
        }
    }

</script>
