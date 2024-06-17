<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_SaveContractQuestions"
    CodeBehind="SaveContractQuestions.ascx.cs" %>

<div>
    
    <asp:Button ID="mHideBtn_ShowContractDialog" runat="server"  CausesValidation="false" style="display:none;" />    
    <ajax:ModalPopupExtender ID="mSaveContractQuestionsPanelPopup" runat="server" TargetControlID="mHideBtn_ShowContractDialog" PopupControlID="mSaveContractQuestionsPanel">
	</ajax:ModalPopupExtender>

    <asp:Panel ID="mSaveContractQuestionsPanel" runat="server" SkinID="Popup" Style="display: none;">
       <ajax:updatepanel id="mSaveContractQuestionsMultiViewUpdatePanel" runat="server"  updatemode="Always">
            <ContentTemplate>                                    
       
                <div class="ui-ShowBox-Alert ui-ShowBox-Custom">

		            <div class="ui-infoBox-alert-header">
			            <div class="alertHeader">
				            <span>!</span>
			            </div>
			            <h3 class="alertHeader titleHeader">Contrato</h3>
			            <div style="clear:both;"></div>
		            </div>              
		        
			        <asp:MultiView ID="mSaveContractQuestionsMultiView" runat="server">
                            <asp:View ID="mConfirmSaveView" runat="server" >                                                                                                                                                                                                            
                                <div class="ui-infoBox-alert-body"> 
                                    Atenção, esta proposta será gravada informaticamente e deverá imprimir o respectivo aviso de pagamento. Quer continuar?                            
                                </div>
                                <div class="ui-infoBox-alert-footer">                                    
                                    <asp:Button ID="mCancelQuestion1Button" runat="server" Text="Não" CausesValidation="false" CssClass="ui-infoBox-button" Width="100" OnClick="mCancelQuestion1Button_Click" />
                                    <div style="float:right">&nbsp;&nbsp</div>
                                    <asp:Button ID="mConfirmQuestion1Button" runat="server" Text="Sim" CausesValidation="false" CssClass="ui-infoBox-button" Width="100" OnClick="mConfirmQuestion1Button_Click" />
                                </div>
                            </asp:View>
                            <asp:View ID="mClientWilPayNowView" runat="server">                                                                                                                                                                                                           
                                <div class="ui-infoBox-alert-body"> 
                                    O pagamento do seguro vai ser efectuado imediatamente?
                                </div>
                                 <div class="ui-infoBox-alert-footer">                                    
                                    <asp:Button ID="mCancelQuestion2Button" runat="server" Text="Não" CausesValidation="false"  CssClass="ui-infoBox-button" Width="100" OnClick="mCancelQuestion2Button_Click" />
                                    <div style="float:right">&nbsp;&nbsp</div>
                                    <asp:Button ID="mConfirmQuestion2Button" runat="server" Text="Sim" CausesValidation="false"  CssClass="ui-infoBox-button" Width="100" OnClick="mConfirmQuestion2Button_Click"  /> 
                                </div>
                            </asp:View>
                            <asp:View ID="mContractWillBeStartedView" runat="server">
                                <div class="ui-infoBox-alert-body"> 
                                    Atenção, vai colocar o Contrato em vigor. O recibo será considerado pago. Quer continuar?"
                                </div>
                                 <div class="ui-infoBox-alert-footer">
                                    <asp:Button ID="mMessagePanel3CancelButton" runat="server" Text="Não" CausesValidation="false" CssClass="ui-infoBox-button" Width="100" OnClick="mMessagePanel3CancelButton_Click" />
                                    <div style="float:right">&nbsp;&nbsp</div>
                                    <asp:Button ID="mMessagePanel3ConfirmButton" runat="server" Text="Sim" CausesValidation="false" CssClass="ui-infoBox-button" Width="100" OnClick="mMessagePanel3ConfirmButton_Click" />
                                </div>
                            </asp:View>
                    </asp:MultiView>                    	              
	            </div>  
            </ContentTemplate>
        </ajax:updatepanel>
    </asp:Panel>
</div>

