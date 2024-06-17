using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Xml;
using Resources;
using Lusitania.Simuladores.DataLayer.FamilyRentDSTableAdapters;
using Lusitania.Simuladores.DataLayer;
using System.Collections.Generic;

using Lusitania.Simulador.WebCommon.Common;

// New
using Lusitania.LusnetBase;

namespace Lusitania.Simuladores.WebSite.Pages
{
    public partial class FamilyRentProposal : MyPage
    {
        #region Fields

        private bool? mCanSeeSaveProposalButton;
        private bool? mCanSeePrintProposalButton;
        private bool? mCanSeeSaveContractButton;
        private bool? mCanSeePrintContractButton;
        private bool? mCanSeePrintReceiptButton;
        private bool? mCanSeePrintAcceptanceSheetButton;

        public string divSim = "";
        public string divPro = "";

        #endregion

        #region Properties

        protected bool IsDirty
        {
            get { return (ViewState["IsDirty"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsDirty"] = value; }
        }

        protected string Nif
        {
            get { return ViewState["Nif"] as string; }
            set { ViewState["Nif"] = value; }
        }

        protected decimal? SimulationID
        {
            get { return ViewState["SimulationID"] as decimal?; }
            set { ViewState["SimulationID"] = value; }
        }

        protected bool CanSeeSaveProposalButton
        {
            get
            {
                if (!mCanSeeSaveProposalButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_GUARDAR_PROPOSTA(SimulationID, UserHelper.UserName, out result);
                    mCanSeeSaveProposalButton = result.HasValue && result.Value == 1;
                }
                return mCanSeeSaveProposalButton.Value;
            }
        }

        protected bool CanSeePrintProposalButton
        {
            get
            {
                if (!mCanSeePrintProposalButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_IMP_PROPOSTA(SimulationID, UserHelper.UserName, out result);
                    mCanSeePrintProposalButton = result.HasValue && result.Value == 1;
                }
                return mCanSeePrintProposalButton.Value;
            }
        }

        protected bool CanSeeSaveContractButton
        {
            get
            {
                if (!mCanSeeSaveContractButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_CONTRATO(SimulationID, UserHelper.UserName, out result);
                    mCanSeeSaveContractButton = result.HasValue && result.Value == 1;
                }
                return mCanSeeSaveContractButton.Value;
            }
        }

        protected bool CanSeePrintContractButton
        {
            get
            {
                if (!mCanSeePrintContractButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_IMP_CONTRATO(SimulationID, UserHelper.UserName, out result);
                    mCanSeePrintContractButton = result.HasValue && result.Value == 1;
                }
                return mCanSeePrintContractButton.Value;
            }
        }

        protected bool CanSeePrintReceiptButton
        {
            get
            {
                if (!mCanSeePrintReceiptButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_IMP_RECIBO(SimulationID, UserHelper.UserName, out result);
                    mCanSeePrintReceiptButton = result.HasValue && result.Value == 1;
                }
                return mCanSeePrintReceiptButton.Value;
            }
        }

        protected bool CanSeePrintAcceptanceSheetButton
        {
            get
            {
                if (!mCanSeePrintAcceptanceSheetButton.HasValue)
                {
                    decimal? result;
                    (new QueriesTableAdapterFix()).MOSTRA_BOTAO_IMP_FOLHA_ACEITA(SimulationID, UserHelper.UserName, out result);
                    mCanSeePrintAcceptanceSheetButton = result.HasValue && result.Value == 1;
                }
                return mCanSeePrintAcceptanceSheetButton.Value;
            }
        }

        protected bool IsProposalDone
        {
            get { return (ViewState["IsProposalDone"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsProposalDone"] = value; }
        }

        protected decimal? FamilyPlanID
        {
            get { return ViewState["FamilyPlanID"] as decimal?; }
            set { ViewState["FamilyPlanID"] = value; }
        }

        protected bool IsProposalPrinted
        {
            get { return (ViewState["IsProposalPrinted"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsProposalPrinted"] = value; }
        }

        protected bool IsContractDone
        {
            get { return (ViewState["IsContractDone"] as bool?).GetValueOrDefault(false); }
            set { ViewState["IsContractDone"] = value; }
        }

        #endregion

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            WebUtility.HookChangeEventOnAllControls(Page, new EventHandler(delegate(object pSender, EventArgs pArgs)
            {
                IsDirty = true;
            }));
        }

        protected override void OnInit(EventArgs e)
        {
            mSaveContractQuestions.Success += new EventHandler(mSaveContractQuestions_Success);

            base.OnInit(e);

            try
            {
                divSim = Request.Params.Get("divSim");
                divPro = Request.Params.Get("divPro");
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            int EscondeHeader;

            try
            {
                EscondeHeader = int.Parse(Request.Params.Get("EscondeHeader"));
            }
            catch (Exception)
            {
                EscondeHeader = 0;
            }

            if (EscondeHeader == 1)
            {
                Image1.Visible = false;
            }
        }

        private decimal? getFamilyPlanId()
        {
            return (string.IsNullOrEmpty(Request.Params.Get("FamilyPlanID")))
                    ? BusinessHelper.CheckSimPlanIntegration(SimulationID)
                    : TryConvert.ToDecimal(Request.Params.Get("FamilyPlanID"));
        }

        protected override void OnLoad(EventArgs e)
        {
            if(!IsPostBack)
            {
                // load parameters
                SimulationID = TryConvert.ToDecimal(Request.Params.Get("SimulationID"));
                Nif = Request.Params.Get("NIF");
                FamilyPlanID = getFamilyPlanId();

                // set the start nif
                if (FamilyPlanID.HasValue)
                {
                    mProposalUserDetails.pIdPlano = ClValoresObjectos.getDecimal(FamilyPlanID);
                }
                else
                {
                    mProposalUserDetails.StartNif = Nif;
                }
                mProposalUserDetails.Visible = !FamilyPlanID.HasValue;

                // configure the spouse info
                mSpousePersonalInfo.SimulationID = SimulationID;

                // configure the heir lists
                mHolderHeirListControl.SimulationID = SimulationID;
                mSpouseHeirListControl.SimulationID = SimulationID;

                // only show the spouse panels if needed
                bool xCanSeeSpouseBlocks = BusinessHelper.CanSeeFamilyRentSpouseBlock(UserHelper.UserName, SimulationID);
                mSpousePanel.Visible = xCanSeeSpouseBlocks;
                mSpouseBeneficiaryPanel.Visible = xCanSeeSpouseBlocks;

                // configure the commercial account
                mProposalCommercialAccount.Visible = QueryCanSeeCommercialAccount();

                BindFractionBox();
            }

            base.OnLoad(e);
        }

        private void UpdateCommands()
        {
            mBackToSimulationButton.Visible = !FamilyPlanID.HasValue &&
                                                !String.IsNullOrEmpty(divSim); //If the user doesn't come from the simulation page, hide the back to simulation button
            mBackToPlanButton.Visible = FamilyPlanID.HasValue;
            mSaveProposalButton.Visible = CanSeeSaveProposalButton;
            mPrintProposalButton.Visible = !IsDirty && !FamilyPlanID.HasValue && CanSeePrintProposalButton;
            mSaveContractButton.Visible = !IsDirty && !FamilyPlanID.HasValue && CanSeeSaveContractButton;
            mPrintContractButton.Visible = !IsDirty && !FamilyPlanID.HasValue && CanSeePrintContractButton;
            mPrintReceiptButton.Visible = !IsDirty && !FamilyPlanID.HasValue && CanSeePrintReceiptButton;
            mPrintAcceptanceSheetButton.Visible = !IsDirty && !FamilyPlanID.HasValue && CanSeePrintAcceptanceSheetButton;
        }

        /// <summary>
        /// Binds the Fraction Control.
        /// </summary>
        private void BindFractionBox()
        {
            FamilyRentDS.LISTA_FRACCIONAMENTODataTable xTable = (new LISTA_FRACCIONAMENTOTableAdapter()).GetData(SimulationID, out Dummy);
            mFractionBox.DataSource = xTable;
            mFractionBox.DataTextField = xTable.DESCRITIVOColumn.ColumnName;
            mFractionBox.DataValueField = xTable.CODIGOColumn.ColumnName;
            mFractionBox.DataBind();
        }

        private bool QueryCanSeeCommercialAccount()
        {
            decimal? result;
            (new QueriesTableAdapterFix()).MOSTRA_SECCAO_CCOMERCIAL(SimulationID, UserHelper.UserName, out result);
            return result.HasValue && result.Value == 1;
        }

        protected override void OnPreRender(EventArgs e)
        {
            UpdateVisualImpairmentBoxes();
            UpdateEaringImpairmentBoxes();
            UpdateSpouseVisualImpairmentBoxes();
            UpdateSpouseEaringImpairmentBoxes();
            UpdateCommands();

            mSpouseQuestionsPanel.Visible = mSpousePersonIDField.Value.HasValue;

            // show the holder heir list control as needed
            mHolderHeirListPanel.Visible = mHolderHasNoLegalHeirsBox.Checked;

            // if the holder has legal heirs, remove all declared heirs
            if (mHolderHasLegalHeirsBox.Checked)
            {
                mHolderHeirListControl.RemoveAllHeirs();
            }

            // show the spouse heir list control as needed
            mSpouseHeirListPanel.Visible = mSpouseHasNoLegalHeirsBox.Checked;

            // if the spouse has legal heirs, remove all declared heirs
            if (mSpouseHasLegalHeirsBox.Checked)
            {
                mSpouseHeirListControl.RemoveAllHeirs();
            }

            base.OnPreRender(e);
        }

        private void SaveProposal()
        {
            List<string> xErrorList = new List<string>();

            // see if the beneficiary nif is the same as the holder's
            if (mSpousePersonalInfo.Nif == mProposalUserDetails.Nif)
            {
                xErrorList.Add(Strings.FamilyRent_SpouseNifIsEqualToHolderNif);
            }

            if (mHolderBeneficiaryPanel.Visible && !(mHolderHasLegalHeirsBox.Checked || mHolderHasNoLegalHeirsBox.Checked))
            {
                xErrorList.Add(Strings.FamilyRent_SelectIfHolderHasLegalHeirs);
            }

            if (mSpouseBeneficiaryPanel.Visible && !(mSpouseHasLegalHeirsBox.Checked || mSpouseHasNoLegalHeirsBox.Checked))
            {
                xErrorList.Add(Strings.FamilyRent_SelectIfSpouseHasLegalHeirs);
            }

            if (xErrorList.Count > 0)
            {
                ScriptHelper.ShowMessageBox(String.Join(Environment.NewLine, xErrorList.ToArray()));
                return;
            }


            string HolderHeirListXml;
            string SpouseHeirListXml;

            HolderHeirListXml = CreateHolderHeirListXml();
            SpouseHeirListXml = CreateSpouseHeirListXml();



            if (mHolderHasNoLegalHeirsBox.Checked && mHolderHeirListControl.Items.Count == 0)
            {
                ScriptHelper.ShowMessageBox("Indique quais os herdeiros legais do titular.");
                return;
            }

            if (mSpouseHasNoLegalHeirsBox.Checked && mSpouseHeirListControl.Items.Count == 0)
            {
                ScriptHelper.ShowMessageBox("Indique quais os herdeiros legais do cônjuge.");
                return;
            }



            string xPolicyID;
            string xErrorMessage;

            (new QueriesTableAdapterFix()).GRAVAR_PROPOSTA(
                SimulationID,
                mProposalUserDetails.ClientNumber,
                mProposalUserDetails.PersonTitleID,
                mProposalUserDetails.PersonName,
                mProposalUserDetails.AddressTypeID,
                mProposalUserDetails.AddressStreetName,
                mProposalUserDetails.AddressStreetNumber,
                mProposalUserDetails.AddressFloorNumber,
                mProposalUserDetails.Locality,
                mProposalUserDetails.ZipCode4,
                mProposalUserDetails.ZipCode3,
                mProposalUserDetails.CountryID,
                mProposalUserDetails.GenderID,
                mProposalUserDetails.DateOfBirth,
                mProposalUserDetails.MaritalStatusID,
                mProposalUserDetails.Nif,
                mProposalUserDetails.IdCardNumber,
                mProposalUserDetails.NationalityID,
                mProposalUserDetails.ProfessionID,
                mProposalUserDetails.PhoneNumber,
                mProposalUserDetails.FaxNumber,
                mProposalUserDetails.EMail,
                mProposalUserDetails.DriverLicense,
                mProposalUserDetails.DriverLicenseDate,
                mProposalUserDetails.AttentionOf,
                mProposalUserDetails.ExtraAddressTypeID,
                mProposalUserDetails.ExtraAddressStreetName,
                mProposalUserDetails.ExtraAddressStreetNumber,
                mProposalUserDetails.ExtraAddressFloorNumber,
                mProposalUserDetails.ExtraLocality,
                mProposalUserDetails.ExtraZipCode4,
                mProposalUserDetails.ExtraZipCode3,
                mProposalUserDetails.ExtraCountryID,
                mProposalUserDetails.ExtraPhoneNumber,
                mProposalUserDetails.ExtraFaxNumber,
                mMortgageCreditor.CreditorID,
                mProposalCommercialAccount.MediatorNumber,
                mProposalCommercialAccount.CollectorNumber,
                mFractionBox.SelectedValue,
                UserHelper.UserName,
                mSaveContractQuestions.EmissionCode.ToString(),
                TryConvert.ToDecimal(mIsLeftHandedBox.SelectedValue),
                TryConvert.ToDecimal(mHasVisualImpairmentBox.SelectedValue),
                mVisualImpairmentDegreeBox.Text,
                TryConvert.ToDecimal(mHasEaringImpairmentBox.SelectedValue),
                mEaringImpairmentDegreeBox.Text,
                TryConvert.ToDecimal(mHasReceivedIndemnationBox.SelectedValue),
                TryConvert.ToDecimal(mSuffersFromDiseaseBox.SelectedValue),
                TryConvert.ToDecimal(mSpouseIsLeftHandedBox.SelectedValue),
                TryConvert.ToDecimal(mSpouseHasVisualImpairmentBox.SelectedValue),
                mSpouseVisualImpairmentDegreeBox.Text,
                TryConvert.ToDecimal(mSpouseHasEaringImpairmentBox.SelectedValue),
                mSpouseEaringImpairmentDegreeBox.Text,
                TryConvert.ToDecimal(mSpouseHasReceivedIndemnationBox.SelectedValue),
                TryConvert.ToDecimal(mSpouseSuffersFromDiseaseBox.SelectedValue),
                CreateHolderHeirListXml(),
                CreateSpouseHeirListXml(),
                mWasInsuredBox.Checked ? 1 : 0,
                mWasInsuredInCompanyBox.Text,
                TryConvert.ToDecimal(mOldInsuranceIsUnpaidBox.SelectedValue),
                mNotes.NotesText,
                out xPolicyID,
                out xErrorMessage
            );

            if (String.IsNullOrEmpty(xErrorMessage))
            {
                IsDirty = false;
                IsProposalDone = true;

                if (FamilyPlanID.HasValue)
                {
                    (new Lusitania.Simuladores.DataLayer.FamilyPlanDSTableAdapters.QueriesTableAdapterFix()).LIMPA_IMPRIMIU_PROP(FamilyPlanID, SimulationID, out xErrorMessage);
                    ScriptHelper.ShowMessageBoxIfNotEmpty(xErrorMessage);
                }

                ScriptHelper.ShowMessageBox(Strings.Proposal_ProposalCreatedOK);
            }
            else
            {
                IsProposalDone = false;
                ScriptHelper.ShowMessageBox(xErrorMessage);
            }
        }

        private string CreateHolderHeirListXml()
        {
            mHolderHeirListControl.EnsureItemsUpdated();

            if (mHolderHasLegalHeirsBox.Checked == false)
            {
                XmlDocument xDocument = new XmlDocument();
                XmlNode xRootNode = xDocument.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "herdeiros", null));
                foreach (FamilyRentHeirListControl.Item xItem in mHolderHeirListControl.Items)
                {
                    XmlNode xHeirNode = xRootNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "herdeiro", null));
                    xHeirNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "pcodigo", null)).InnerText = xItem.PersonID;
                    xHeirNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "parentesco", null)).InnerText = xItem.KinshipID;
                }
                return xDocument.InnerXml;
            }
            else
            {
                //Se a radio «Sim - Herdeiros Legais» estiver selecionada vamos apagar herdeiros que tenham sido definidos

                return null;
            }
        }

        private string CreateSpouseHeirListXml()
        {

            mSpouseHeirListControl.EnsureItemsUpdated();

            if (mSpouseHasLegalHeirsBox.Checked == false)
            {
                XmlDocument xDocument = new XmlDocument();
                XmlNode xRootNode = xDocument.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "herdeiros", null));
                foreach (FamilyRentHeirListControl.Item xItem in mSpouseHeirListControl.Items)
                {
                    XmlNode xHeirNode = xRootNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "herdeiro", null));
                    xHeirNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "pcodigo", null)).InnerText = xItem.PersonID;
                    xHeirNode.AppendChild(xDocument.CreateNode(XmlNodeType.Element, "parentesco", null)).InnerText = xItem.KinshipID;
                }
                return xDocument.InnerXml;
            }
            else
            {
                return null;
            }
        }



        private void UpdateEaringImpairmentBoxes()
        {
            bool xHasEaringImpairment = (mHasEaringImpairmentBox.SelectedIndex == 1);

            mEaringImpairmentDegreeBlock1.Visible = xHasEaringImpairment;
            mEaringImpairmentDegreeBlock2.Visible = xHasEaringImpairment;

            if (!xHasEaringImpairment)
            {
                mEaringImpairmentDegreeBox.Text = String.Empty;
            }
        }

        private void UpdateSpouseEaringImpairmentBoxes()
        {
            bool xHasEaringImpairment = (mSpouseHasEaringImpairmentBox.SelectedIndex == 1);

            mSpouseEaringImpairmentDegreeBlock1.Visible = xHasEaringImpairment;
            mSpouseEaringImpairmentDegreeBlock2.Visible = xHasEaringImpairment;

            if (!xHasEaringImpairment)
            {
                mSpouseEaringImpairmentDegreeBox.Text = String.Empty;
            }
        }

        private void UpdateVisualImpairmentBoxes()
        {
            bool xHasVisualImpairment = (mHasVisualImpairmentBox.SelectedIndex == 1);

            mVisualImpairmentDegreeBlock1.Visible = xHasVisualImpairment;
            mVisualImpairmentDegreeBlock2.Visible = xHasVisualImpairment;

            if (!xHasVisualImpairment)
            {
                mVisualImpairmentDegreeBox.Text = String.Empty;
            }
        }

        private void UpdateSpouseVisualImpairmentBoxes()
        {
            bool xHasVisualImpairment = (mSpouseHasVisualImpairmentBox.SelectedIndex == 1);

            mSpouseVisualImpairmentDegreeBlock1.Visible = xHasVisualImpairment;
            mSpouseVisualImpairmentDegreeBlock2.Visible = xHasVisualImpairment;

            if (!xHasVisualImpairment)
            {
                mSpouseVisualImpairmentDegreeBox.Text = String.Empty;
            }
        }

        protected void mProposalButton_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                return;
            }

            ShowProposal();
        }

        private void ShowProposal()
        {
            ScriptHelper.ShowWindow(PageLinks.FamilyRentProposalDocument, "SimulationID={0}", SimulationID);
            IsProposalPrinted = true;

            (new QueriesTableAdapterFix()).IMPRIMIU_PROP(SimulationID);
        }

        protected void mContractButton_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                return;
            }

            ShowSaveContractDialog();
        }

        private void ShowSaveContractDialog()
        {
            mSaveContractQuestions.SimulationID = SimulationID;
            mSaveContractQuestions.Show();
        }

        protected void mSaveSpouseInfoButton_Click(object sender, EventArgs e)
        {
            SaveSpouse();
        }

        private void SaveSpouse()
        {
            decimal? xPersonID;
            string xErrorMessage;
            string xPersonName;

            if (mSpousePersonalInfo.TrySave(out xPersonID, out xErrorMessage, out xPersonName))
            {
                // update the current spouse id
                mSpousePersonIDField.Value = xPersonID;

                // update the spouse name label
                mSpouseNameShowLabel.Text = mSpousePersonalInfo.PersonName;
            }
            else
            {
                ScriptHelper.ShowMessageBox(xErrorMessage);
            }
        }
        protected void mFractionBox_DataBound(object sender, EventArgs e)
        {
            try
            {
                mFractionBox.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
        protected void mSaveButton_Click(object sender, EventArgs e)
        {
            SaveProposal();
        }
        /*protected void mBackToSimulationButton_Click(object sender, EventArgs e)
        {
            RedirectToSimulation();
        }

        private void RedirectToSimulation()
        {
            Response.Redirect(String.Format("{0}?SimulationID={1}", PageLinks.FamilyRentSimulator, SimulationID));
        }*/
        protected void mBackToPlanButton_Click(object sender, EventArgs e)
        {
            //if (IsDirty)
            //{
            //    ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
            //    return;
            //}

            Response.Redirect(String.Format("{0}?FamilyPlanID={1}", ResolveUrl(PageLinks.FamilyPlanSimulator), FamilyPlanID));
        }
        protected void mPrintContractButton_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                return;
            }

            ScriptHelper.ShowWindow(PageLinks.FamilyRentContractReport, "SimulationID={0}", SimulationID);
        }
        protected void mPrintReceiptButton_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                return;
            }

            ScriptHelper.ShowWindow(PageLinks.FamilyRentReceiptReport, "SimulationID={0}", SimulationID);
        }

        private void mSaveContractQuestions_Success(object sender, EventArgs e)
        {
            IsContractDone = true;
        }

        protected void mPrintAcceptanceSheetButton_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ScriptHelper.ShowMessageBox(Strings.ProposalIsDirty);
                return;
            }

            ScriptHelper.ShowWindow(PageLinks.FamilyRentAcceptanceSheetReport, "SimulationID={0}", SimulationID);
        }
    }
}
