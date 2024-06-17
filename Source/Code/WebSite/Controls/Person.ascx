<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Person.ascx.cs" Inherits="Lusitania.Simuladores.WebSite.Controls.Person" %>
<%@ Register Src="~/Controls/AddressSearch.ascx" TagPrefix="app" TagName="AddressSearch" %>
<asp:HiddenField ID="mStartNifField" runat="server" Visible="false" />
<ajax:updatepanel id="UserDetailPanel" runat="server" updatemode="Always">					
	<ContentTemplate>
       
        <!-- Pesquisa -->
        <asp:Panel ID="mSearchPanel" runat="server" GroupingText="Pesquisa" DefaultButton="mSearchClientButton" SkinID="proposal_panel">
            <table class="Layout">
        <tr>
            <td class="TitleBold">
                <asp:Label ID="mClientNumberSearchLabel" runat="server" Text="Cliente:" EnableViewState="false"></asp:Label>
            </td>
            <td class="TitleBold">
                <asp:Label ID="mNifSearchLabel" runat="server" Text="NIF:" EnableViewState="false"></asp:Label>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="mClientNumberSearchBox" runat="server" MaxLength="10" Width="170"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" id="mClientNumberSearchBoxRegVal" ControlToValidate="mClientNumberSearchBox" ErrorMessage="Número inválido." ValidationExpression="([0-9])*" />
            </td>
            <td>
                <asp:TextBox ID="mNifSearchBox" Width="170" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.Nif %>"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" id="mNifSearchBoxRegVal" ControlToValidate="mNifSearchBox" ErrorMessage="Número inválido." ValidationExpression="([0-9])*" />
            </td>
            <td>             
                <ajax:updatepanel id="BtnSearchPanel" runat="server" updatemode="Conditional">					
		            <ContentTemplate>          
					    <asp:Button ID="mSearchClientButton" runat="server" Text="Pesquisar" OnClick="mSearchClientButton_Click" EnableViewState="false" SkinID="SimulateX4" />						
                    </ContentTemplate>
                </ajax:updatepanel>
            </td>
            <td>
                <ajax:updatepanel id="BtnClearPanel" runat="server" updatemode="Conditional">					
		            <ContentTemplate>
                        <asp:Button ID="mClearClientButton" runat="server" Text="Limpar Campos" OnClick="mClearClientButton_Click" CausesValidation="false" EnableViewState="false" SkinID="SimulateX4"/>						
                    </ContentTemplate>
                </ajax:updatepanel>
            </td>
            <td>
                <ajax:updateprogress id="mContentUpdatePanelProgress" runat="server" associatedupdatepanelid="BtnSearchPanel">
						<ProgressTemplate>
							<asp:Image ID="mImage1" runat="server" SkinID="Wait" />
						</ProgressTemplate>
					</ajax:updateprogress>
                <ajax:updateprogress id="mClearClientButtonUpdatePanelProgress" runat="server" associatedupdatepanelid="BtnClearPanel">
						<ProgressTemplate>
							<asp:Image ID="mImage2" runat="server" SkinID="Wait" />
						</ProgressTemplate>
					</ajax:updateprogress>
            </td>
        </tr>
    </table>
        </asp:Panel>

        <br />
        <!-- Dados Pessoais-->
        <asp:Panel ID="pnlDadosPessoais" runat="server">            	        
            <asp:Panel ID="mPersonalInfoPanel" runat="server" GroupingText="Dados Pessoais"  SkinID="proposal_panel">

                <asp:Panel ID="mPersonClientPanel" runat="server" Visible="false" >                

            <table  class="Layout">                        
                <tr >
	                <td class="TitleBold">
		                Cliente
	                </td>
                </tr>
                <tr>
	                <td>		                                            
				        <asp:DropDownList ID="mPersonClientBox" runat="server" AutoPostBack="true" OnSelectedIndexChanged="mPersonClientBox_SelectedIndexChanged" Visible="False">
				        </asp:DropDownList>                                        			                                        
	                </td>
                </tr>
            </table>

        </asp:Panel>

                <asp:Panel ID="mSelectedPersonInfoPanel" runat="server">
                                
            <table class="Layout">								    
			<tr class="TitleBold">
				<td>
					Nº Cliente
                </td>
				<td>
					NIF
                </td>
				<td>
					Nº B.I.
                </td>
				<td>
					Data de Nascimento
                </td>									   
			</tr>
			<tr>
				<td>
					<asp:TextBox ID="mClientNumberBox" runat="server" MaxLength="8" Enabled="False" format="number"></asp:TextBox>										    
				</td>
				<td>										    
					<asp:TextBox ID="mNifBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.Nif %>" Enabled="false" format="number"></asp:TextBox>											    
				</td>
				<td>
					<asp:TextBox ID="mIdCardNumberBox" runat="server" MaxLength="8" format="number"></asp:TextBox>										    
				</td>
				<td>										    
					<asp:TextBox ID="mDateOfBirthBox" runat="server"  MaxLength="10" AutoPostBack="true" placeholder="dd-mm-yyyy" format="date"></asp:TextBox>															    																										    
				</td>
			</tr>						    
			<tr class="TitleBold">
				<td>
					Sexo
                </td>
				<td>
					Estado Civil
                </td>
				<td colspan="2">
					Profissão
                </td>
			</tr>
			<tr>
				<td>
                    <asp:RadioButtonList ID="mGenderBox" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="mGenderBox_SelectedIndexChanged"></asp:RadioButtonList>
				</td>
				<td>
					<asp:DropDownList ID="mMaritalStatusBox" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList>
				</td>
				<td colspan="2">
					<ajax:UpdatePanel ID="mProfessionBoxUpdatePanel" runat="server" UpdateMode="Conditional">												
						<ContentTemplate>
							<asp:Panel ID="mProfessionShowPanel" runat="Server">
								<table cellpadding="0" cellspacing="0" border="0">
									<tr>
										<td>
											<asp:TextBox ID="mProfessionShowBox" runat="Server" Width="300px" Enabled="false"></asp:TextBox></td>
										<td>
											&nbsp;</td>
										<td>
											<asp:ImageButton ID="mSearchProfessionButton" runat="server" SkinID="Zoom" CausesValidation="false" />
                                            <ajax:ModalPopupExtender ID="mProfessionSearchPopup" runat="server" TargetControlID="mSearchProfessionButton" PopupControlID="mProfessionSearchPopupPanel" ></ajax:ModalPopupExtender>
										</td>
									</tr>
								</table>
							</asp:Panel>
							<asp:Panel ID="mProfessionSearchPopupPanel" runat="Server" SkinID="Popupv6">			                                              
								<asp:Panel ID="mProfessionSearchPanel" runat="Server"  DefaultButton="mConfirmSearchProfessionButton">
									<table>
										<tr>
											<td>
												<span style="padding:5px 10px; display:block; background:#CCE3DF;">
													<asp:Label ID="Label11" runat="server" Text="Pesquisa de Profissão:"></asp:Label>
												</span>
											</td>			                                                                
										</tr>
										<tr>
											<td style="padding-top: 20px;">
												<app:ListBox ID="mProfessionBox" runat="server" Width="730px" Height="200px" Rows="20" style="background-image:none;">
												</app:ListBox>
																	   
												<ajax:ListSearchExtender ID="mProfessionBoxExtender1" runat="server" TargetControlID="mProfessionBox" IsSorted="true" PromptPosition="Bottom" PromptText="Tecle para pesquisar..." >
												</ajax:ListSearchExtender>
											</td>
										</tr>
										<tr>
											<td style="text-align:right">
												<asp:Button ID="mConfirmSearchProfessionButton" runat="Server" Text="OK" CausesValidation="false" SkinID="SimulateX4" />
											</td>
										</tr>
										<tr>
											<td>
												<ajax:UpdateProgress ID="mProfessionBoxUpdatePanelProgress" runat="server" AssociatedUpdatePanelID="mProfessionBoxUpdatePanel" DynamicLayout="False">
													<ProgressTemplate>
														<asp:Image ID="mProfessionWaitImage" runat="server" SkinID="Wait" />
													</ProgressTemplate>
												</ajax:UpdateProgress>
											</td>
										</tr>
									</table>
								</asp:Panel>
							</asp:Panel>
							
							
                        </ContentTemplate>
                    </ajax:UpdatePanel>

											    
				</td>
			</tr>							    							    
			<tr class="TitleBold">
				<td>
					Título
                </td>
				<td colspan="2">
					Nome
                </td>
				<td>
					Nacionalidade
                </td>
			</tr>
			<tr>
				<td>
						<asp:DropDownList ID="mTitleBox" runat="server" Width="150px"></asp:DropDownList>											    
				</td>
				<td colspan="2">
					<asp:TextBox ID="mNameBox" runat="server" AutoPostBack="true" MaxLength="50" style="width:300px;vertical-align:middle;"></asp:TextBox>
                </td>
				<td>										    
					<asp:DropDownList ID="mNationalityBox" runat="server" Width="100px" ></asp:DropDownList>
				</td>
			</tr>
		</table>

        </asp:Panel>

	        </asp:Panel>	
        </asp:Panel>
        
        <br />
        <!-- Dados Contacto-->
        <asp:Panel ID="pnlDadosContacto" runat="server">
            
			<asp:Panel ID="mContactPanel" runat="server" GroupingText="Contactos"  SkinID="proposal_panel" >
				<asp:Button ID="mSearchContactButton" runat="server" Text="Pesquisar" CausesValidation="false" SkinID="SimulateX4"/>
				<ajax:ModalPopupExtender ID="mContactSearchPanelExtender" runat="server" TargetControlID="mSearchContactButton" PopupControlID="mContactSearchPanel" DropShadow="true" CancelControlID="mCloseContactSearchButton" SkinID="modal1">
				</ajax:ModalPopupExtender>

				<asp:Panel ID="mContactSearchPanel" runat="server" Style="display: none;" SkinID="Popupv6">
					<table>
						<tr>
							<td>
								<app:AddressSearch ID="mContactAddressSearch" runat="server" OnAddressChosen="mContactAddressSearch_AddressChosen" OnAddressLocalSearch="mContactAddressSearch_AddressSearch" />
							</td>
						</tr>
						<tr>
							<td align="center">
								<asp:Button ID="mCloseContactSearchButton" runat="server" Text="Fechar" CausesValidation="false" SkinID="SimulateX4" /></td>
						</tr>
					</table>
				</asp:Panel>


				<table class="Layout">
					<tr  class="TitleBold">
						<td colspan="3">Morada</td>
						<td>Nº / Lote</td>
						<td>Andar</td>
						<td>Código Postal</td>
						<td>Localidade</td>
						<td>País</td>
					</tr>
					<tr>
						<td>
							<asp:TextBox ID="mAddressTypeBox" runat="server" MaxLength="5" Width="30px" Enabled="False"></asp:TextBox>									
						</td>
						<td>
							<asp:TextBox ID="mAddressTitleBox" runat="server" Width="50px" MaxLength="5" Enabled="False"></asp:TextBox></td>
						<td>									
							<asp:TextBox ID="mAddressStreetNameBox" runat="server" MaxLength="100" Width="150px"></asp:TextBox>									
						</td>
									
						<td>									
							<asp:TextBox ID="mAddressStreetNumberBox" runat="Server" MaxLength="10" Width="50px"></asp:TextBox>									
						</td>
						<td>									
							<asp:TextBox ID="mAddressFloorNumberBox" runat="server" MaxLength="10" Width="50px"></asp:TextBox>
						</td>
						<td>
                            <asp:TextBox ID="mZipCode4Box" runat="server" MaxLength="4" Width="30" format="number"></asp:TextBox>
										-
							<asp:TextBox ID="mZipCode3Box" runat="server" MaxLength="3" Width="20" format="number"></asp:TextBox>									
						</td>
						<td>									
							<asp:TextBox ID="mLocalityBox" runat="server"></asp:TextBox>
                            <asp:TextBox ID="mLocalBox" runat="server" Width="120" SkinID="CaixaInput" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="mCPALFBox" runat="server" Width="120" SkinID="CaixaInput" Visible="false"></asp:TextBox>
						</td>
						<td>
							<asp:DropDownList ID="mCountryBox" runat="Server" AutoPostBack="True" OnSelectedIndexChanged="mCountryBox_SelectedIndexChanged">
							</asp:DropDownList>
						</td>
					</tr>
				</table>
				<table class="Layout" style="margin-top:10px;">							
					<tr>
						<td  class="TitleBold"> Telefone</td>
						<td>
							<asp:TextBox ID="mPhoneNumberBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.PhoneNumber %>" Width="100"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="mPhoneNumberBoxExtender1" runat="server" TargetControlID="mPhoneNumberBox"
                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                            </ajax:FilteredTextBoxExtender>
                        </td>
                        <td  class="TitleBold">Telemóvel</td>
						<td>
							<asp:TextBox ID="mMobilePhoneNumberBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.PhoneNumber %>" Width="100"></asp:TextBox>
                            <ajax:FilteredTextBoxExtender ID="mMobilePhoneNumberExtender1" runat="server" TargetControlID="mMobilePhoneNumberBox"
                                FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                            </ajax:FilteredTextBoxExtender>
                        </td>
						<td  class="TitleBold">E-Mail</td>
						<td>
							<asp:TextBox ID="mEMailBox" runat="server"></asp:TextBox>
                        </td>
						<td  class="TitleBold">Fax</td>
						<td>
							<asp:TextBox ID="mFaxBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.FaxNumber %>"></asp:TextBox>
                        </td>
					</tr>
				</table>
			</asp:Panel>

			<br />

            <asp:Panel ID="mExtraContactPanel" runat="Server">
                
				<table>
					<tr>
						<td>
							<asp:Label ID="mHasExtraContractLabel" runat="server" SkinID="Heavy" Text="Deseja receber a correspondência numa outra morada ?"></asp:Label>
						</td>									
						<td>
                            <asp:radiobuttonlist runat="server" ID="mExtraContactBox" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:listitem text="Não" Value="N"/>
                                <asp:listitem text="Sim" Value="S" />
                            </asp:radiobuttonlist>
							
						</td>
					</tr>
				</table>

				<asp:Panel ID="mExtraContactDetailsPanelHolder" runat="server">
						<asp:Panel ID="Panel1" mProfessionShowPanelID="mExtraContactDetailsPanel" runat="server" GroupingText="Morada Para Correspondência">
							<asp:Panel ID="mSearchExtraContactButtonPanel" runat="Server">
								<asp:Button ID="mSearchExtraContactButton" runat="server" Text="Pesquisar" CausesValidation="false" SkinID="SimulateX4"/>
								<ajax:ModalPopupExtender ID="mExtraContactAddressSearchExtender" runat="server" TargetControlID="mSearchExtraContactButton" PopupControlID="mExtraContactAddressSearchPanel" DropShadow="true" CancelControlID="mCloseExtraContactAddressSearchButton">
								</ajax:ModalPopupExtender>
								<asp:Panel ID="mExtraContactAddressSearchPanel" runat="server" Style="display: none" SkinID="Popupv6">
									<table>
										<tr>
											<td>
												<app:AddressSearch ID="mExtraContactAddressSearch" runat="server" OnAddressChosen="mExtraContactAddressSearch_AddressChosen" OnAddressLocalSearch="mExtraContactAddressSearch_AddressSearch" />
											</td>
										</tr>
										<tr>
											<td align="center">
												<asp:Button ID="mCloseExtraContactAddressSearchButton" runat="server" Text="Fechar" CausesValidation="false" SkinID="SimulateX4" OnClientClick="activePopup ='';"/></td>
										</tr>
									</table>
								</asp:Panel>
							</asp:Panel>
							<table class="Layout">
							    <tr  class="TitleBold">
									<td colspan="8">
										<asp:Label ID="mAttentionOfLabel" runat="server" Text="Ao Cuidado de:" Width="400px"></asp:Label></td>
								</tr>
								<tr>
									<td  colspan="8">
										<asp:TextBox ID="mAttentionOfBox" runat="server" MaxLength="100" style="width:300px;"></asp:TextBox>
									</td>
								</tr>									
								<tr class="TitleBold">
									<td colspan="3">Morada</td>
									<td>Nº / Lote</td>
									<td>Andar</td>
									<td>Código Postal</td>
									<td>Localidade</td>
									<td>País</td>
								</tr>
								<tr>
									<td>												
										<asp:TextBox ID="mExtraAddressTypeBox" runat="server" Width="30px"></asp:TextBox>
									</td>
									<td>
										<asp:TextBox ID="mExtraAddressTitleBox" runat="server" Width="50px"></asp:TextBox>
									</td>
									<td>
										<asp:TextBox ID="mExtraAddressStreetNameBox" runat="server" Width="150px"></asp:TextBox>
									</td>
									<td>
										<asp:TextBox ID="mExtraAddressStreetNumberBox" runat="Server" Width="50px"></asp:TextBox>
									</td>
									<td>
										<asp:TextBox ID="mExtraAddressFloorNumberBox" runat="server" Width="50px"></asp:TextBox>
									</td>
									<td>
										<asp:TextBox ID="mExtraZipCode4Box" runat="server" MaxLength="4" Width="40" format="number"></asp:TextBox>
													-
										<asp:TextBox ID="mExtraZipCode3Box" runat="server" MaxLength="3" Width="30" format="number"></asp:TextBox>
									</td>
									<td>
                                        <asp:TextBox ID="mExtraLocalityBox" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="mExtraLocalBox" runat="server" Width="120" SkinID="CaixaInput" Visible="false"></asp:TextBox>
                                        <asp:TextBox ID="mExtraCPALFBox" runat="server" Width="120" SkinID="CaixaInput" Visible="false"></asp:TextBox>
									</td>
									<td>
										<asp:DropDownList ID="mExtraCountryBox" runat="Server" AppendDataBoundItems="True" AutoPostBack="True"  OnSelectedIndexChanged="mExtraCountryBox_SelectedIndexChanged">
											<asp:ListItem Selected="True"></asp:ListItem>
										</asp:DropDownList>												
									</td>
								</tr>
							</table>
							<table>									
								<tr style="visibility:hidden;">
									<td>
										<asp:Label ID="Label7" runat="server" Text="Telefone:"></asp:Label></td>
									<td>
										<asp:TextBox ID="mExtraPhoneNumberBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.PhoneNumber %>"></asp:TextBox></td>
									<td>
										<asp:Label ID="Label8" runat="server" Text="E-Mail:"></asp:Label></td>
									<td>
										<asp:TextBox ID="mExtraEMailBox" runat="server"></asp:TextBox></td>
									<td>
										<asp:Label ID="Label9" runat="server" Text="Fax:"></asp:Label></td>
									<td>
										<asp:TextBox ID="mExtraFaxBox" runat="server" MaxLength="<%$ AppSettings:TextBox.MaxLength.PhoneNumber %>"></asp:TextBox></td>
								</tr>
							</table>
						</asp:Panel>
					</asp:Panel>
							
                </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</ajax:updatepanel>
