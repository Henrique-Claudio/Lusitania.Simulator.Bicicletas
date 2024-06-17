using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Lusitania.Simuladores.WebSite.Common;
using System.Configuration;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;
using Lusitania.Utilities;
using Lusitania.Model.Simuladores.Negocio;
using System.Data;
using Lusitania.Simulador.WebCommon.Common;
using Resources;
using Lusitania.Web.Simuladores.Base.Classes;
using Lusitania.LusnetBase;
using Lusitania.Simuladores.DataLayer.Proposal;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using OSBConnector.Models;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class Person : ClControloBase
    {
        #region Constants

        private const string DEFAULT_GENDER_CODE = "1";
        private const string VSK_CLIENT_RESULTS = "CliResults";

        #endregion

        #region ----- Eventos Delegate -----

        // Evento delegate de alterar Igual Tomador
        public delegate void EventoIgualTomadorHandler(Boolean prmIgualTomador);
        private EventoIgualTomadorHandler _evhEventoIgualTomador;

        #endregion

        #region ----- Propriedades -----

        #region ----- pHideProfession -----
        /// <summary>
        /// Indicador de Disable da pesquisa de Profissão
        /// </summary>
        public Boolean pHideProfession
        {
            get;
            set;
        }
        #endregion

        /*   public String pHeader_Text
        {
            get { return mHeaderLabel.Text;}
            set { mHeaderLabel.Text = value; }
        }*/

        //public String pOpsIgualATomador
        //{
        //    get
        //    {
        //        if (rbtIgualTomadorSim.Checked) { return "S"; }
        //        else { return "N"; }
        //    }
        //    set
        //    {
        //        if (value.Equals("S"))
        //        {
        //            rbtIgualTomadorSim.Checked = true;
        //            rbtIgualTomadorNao.Checked = false;
        //        }
        //        else
        //        {
        //            rbtIgualTomadorSim.Checked = false;
        //            rbtIgualTomadorNao.Checked = true;
        //        }
        //    }
        //}

        public string TelemovelTomador
        {
            get { return (ViewState["TelemovelTomador"] != null) ? (string)ViewState["TelemovelTomador"] : string.Empty; }
            set { ViewState["TelemovelTomador"] = value; }
        }

        public string pClientNumberBox
        {
            get { return (ViewState["pClientNumberBox"] != null) ? (string)ViewState["pClientNumberBox"] : string.Empty; }
            set { ViewState["pClientNumberBox"] = value; }
        }

        public string EmailTomador
        {
            get { return (ViewState["EmailTomador"] != null) ? (string)ViewState["EmailTomador"] : string.Empty; }
            set { ViewState["EmailTomador"] = value; }
        }

        public String pPersonType
        {
            get
            {
                return ViewState["pPersonType"] != null ? (string)ViewState["pPersonType"] : string.Empty;
            }
            set
            {
                ViewState["pPersonType"] = value;
            }
        }

        /* public int pParentescoIdx
         {
             get
             {
                 return ParentDrop.SelectedIndex;
             }
         }
         */

        public Boolean pHerdeiro
        {
            get;
            set;
        }

        #region ----- Eventos Delegate ------

        #region ----- pEventoIgualTomador -----

        /// <summary>
        /// Evento delegate de alterar Igual Tomador
        /// </summary>
        public EventoIgualTomadorHandler pEventoIgualTomador
        {
            set
            {
                _evhEventoIgualTomador = value;
            }
        }

        #endregion

        #endregion

        #region ----- pTipoSimulador -----

        /// <summary>
        /// Tipo de Simulador
        /// </summary>
        public ClEnumeration.TipoSimulador pTipoSimulador
        {
            get
            {
                if (ViewState["pTipoSimulador"] == null)
                {
                    return ClEnumeration.TipoSimulador.NaoDefinido;
                }
                return (ClEnumeration.TipoSimulador)ViewState["pTipoSimulador"];
            }
            set
            {
                ViewState["pTipoSimulador"] = value;
            }
        }

        #endregion

        #region ----- pDadosPessoaisObrigatorios -----

        /// <summary>
        /// Dados Pessoais (mínimos) obrigatórios?
        /// </summary>
        public Boolean pDadosPessoaisObrigatorios
        {
            get
            {
                if (ViewState["pDadosPessoaisObrigatorios"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(ViewState["pDadosPessoaisObrigatorios"]);
            }
            set
            {
                ViewState["pDadosPessoaisObrigatorios"] = value;
            }
        }

        #endregion

        #region ----- pTitularDadosInexistentes -----

        /// <summary>
        /// Titular - Dados inexistentes
        /// </summary>
        public List<String> pTitularDadosInexistentes
        {
            get
            {
                if (ViewState["pTitularDadosInexistentes"] == null)
                {
                    return new List<String>();
                }
                return (List<String>)ViewState["pTitularDadosInexistentes"];
            }
            set
            {
                ViewState["pTitularDadosInexistentes"] = value;
            }
        }

        #endregion

        #region ----- pPermitirNIFEmpresa -----

        /// <summary>
        /// Indicar se permite NIF de Empresa
        /// </summary>
        public Boolean pPermitirNIFEmpresa
        {
            get
            {
                if (ViewState["pPermitirNIFEmpresa"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(ViewState["pPermitirNIFEmpresa"]);
            }
            set
            {
                ViewState["pPermitirNIFEmpresa"] = value;
            }
        }

        #endregion

        #region ----- pPermitirContactoSecundario -----

        /// <summary>
        /// Indicar se permite Contacto Secundário
        /// </summary>
        public Boolean pPermitirContactoSecundario
        {
            get
            {
                return mExtraContactPanel.Visible;
            }
            set
            {
                mExtraContactPanel.Visible = value;
            }
        }

        #endregion

        #region ----- pMostrarConfidencialidade -----

        /// <summary>
        /// Indicar se mostra o campo Confidencialidade
        /// </summary>
        /*public Boolean pMostrarConfidencialidade
        {
            get
            {
                return pnlConfidencialidade.Visible;
            }
            set
            {
                pnlConfidencialidade.Visible = value;
            }
        }*/

        #endregion

        #region ----- pConfidencialidade -----

        /// <summary>
        /// Valor de Confidencialidade
        /// </summary>
        /*public String pConfidencialidade
        {
            get
            {
                String _strConfidencialidade;

                // Verificar se mostra Confidencialidade	
                if(pMostrarConfidencialidade)
                {
                    // Opção escolhida
                    _strConfidencialidade = rblConfidencialidade.SelectedValue;
                }
                else
                {
                    _strConfidencialidade = "N";
                }

                // Verificar se mostra Confidencialidade
                return _strConfidencialidade;
            }
            set {
                if (pMostrarConfidencialidade)
                {
                    rblConfidencialidade.SelectedValue = value;
                }
            }
        }
        */
        #endregion

        #region ----- pIdPlano -----

        /// <summary>
        /// Id do Plano
        /// </summary>
        public Decimal pIdPlano
        {
            get
            {
                if (ViewState["pIdPlano"] == null)
                {
                    return 0;
                }
                return Convert.ToDecimal(ViewState["pIdPlano"]);
            }
            set
            {
                ViewState["pIdPlano"] = value;

                // Mostrar Pessoa com base no Id do Plano
                ShowPersonByPlanID();
            }
        }

        public bool IsPlan
        {
            get { return pIdPlano > 0; }
        }


        #endregion

        #region ----- pNumeroPessoaSegura -----

        /// <summary>
        /// N.º da Pessoa Segura
        /// </summary>
        public Int32 pNumeroPessoaSegura
        {
            get
            {
                if (ViewState["pNumeroPessoaSegura"] == null)
                {
                    return 0;
                }
                return Convert.ToInt32(ViewState["pNumeroPessoaSegura"]);
            }
            set
            {
                ViewState["pNumeroPessoaSegura"] = value;
            }
        }

        #endregion

        #region ----- pTituloErro -----

        /// <summary>
        /// Título Erro
        /// </summary>
        public String pTituloErro
        {
            get
            {
                String _strTituloErro;

                // Obter o valor do ViewState
                if (ViewState["pTituloErro"] != null)
                {
                    _strTituloErro = Convert.ToString(ViewState["pTituloErro"]);
                }
                else
                {
                    // Não tem valor

                    // Verificar se tem N.º de Pessoa Segura
                    if (pNumeroPessoaSegura > 0)
                    {
                        // Tem Título
                        _strTituloErro = String.Format("Pessoa Segura {0}", pNumeroPessoaSegura);
                    }
                    else
                    {
                        // Vazio
                        _strTituloErro = String.Empty;
                    }
                }

                return _strTituloErro;
            }
            set
            {
                ViewState["pTituloErro"] = ClErros.formatarErroTitulo(value);
            }

        }

        #endregion

        #region ----- pMostrarTitulo -----

        /// <summary>
        /// Indicar se mostra o Título
        /// </summary>
        public Boolean pMostrarTitulo
        {
            get
            {
                if (ViewState["pMostrarTitulo"] == null)
                {
                    return true;
                }
                return Convert.ToBoolean(ViewState["pMostrarTitulo"]);
            }
            set
            {
                ViewState["pMostrarTitulo"] = value;
            }
        }

        #endregion

        #region ----- pvMostrarTitulo -----

        /// <summary>
        /// Indicar se mostra o Título
        /// (mostra se pede para mostrar Título e não tem n.º de Pessoa Segura)
        /// </summary>
        private Boolean pvMostrarTitulo
        {
            get
            {
                // Mostra se pede para mostrar Título e não tem n.º de Pessoa Segura
                return ((pMostrarTitulo) && (pNumeroPessoaSegura == 0) && !pHerdeiro);
            }
        }

        #endregion

        #region ----- pPerguntarIgualTomador -----

        /// <summary>
        /// Indicar se pergunta se é igual ao Tomador
        /// </summary>
        public Boolean pPerguntarIgualTomador
        {
            get
            {
                if (ViewState["pPerguntarIgualTomador"] == null)
                {
                    return false;
                }
                return Convert.ToBoolean(ViewState["pPerguntarIgualTomador"]);
            }
            set
            {
                ViewState["pPerguntarIgualTomador"] = value;
            }
        }

        #endregion

        #region ----- pvPerguntarIgualTomador -----

        /// <summary>
        /// Indicar se mostra a pergunta se é igual ao Tomador
        /// (mostra se pede para mostrar a pergunta e se é a 1.ª Pessoa Segura)
        /// </summary>
        private Boolean pvPerguntarIgualTomador
        {
            get
            {
                // Mostra se pede para mostrar a pergunta e se é a 1.ª Pessoa Segura
                return ((pPerguntarIgualTomador) && (pNumeroPessoaSegura == 1));
            }
        }

        #endregion

        #region ----- pPessoaSeguraIgualTomador -----

        /// <summary>
        /// Pessoa Segura igual ao Tomador?
        /// </summary>
        //public Boolean pPessoaSeguraIgualTomador
        //{
        //    get
        //    {
        //        return rbtIgualTomadorSim.Checked;
        //    }
        //}

        #endregion

        #region ----- Dados da Pessoa -----

        #region ----- NIF -----

        /// <summary>
        /// NIF
        /// </summary>
        public String pNIF
        {
            get
            {
                return mNifBox.Text;
            }
            set
            {
                mNifBox.Text = value;
            }
        }

        #endregion

        #region ----- Data de Nascimento -----

        /// <summary>
        /// Data de Nascimento
        /// </summary>
        public DateTime pDataNascimento
        {
            get
            {
                return mDateOfBirthBox.Text.getDateTimeData(ClEnumerationBase.FormatoData.DiaMesAno);
            }
            set
            {
                mDateOfBirthBox.Text = value.ToString("dd-MM-yyyy");
            }
        }

        #endregion

        #endregion

        public bool ContactUnChecked
        {
            get
            {
                bool retval;
                if (ViewState["ContactUnChecked"] != null && bool.TryParse(ViewState["ContactUnChecked"].ToString(), out retval))
                {
                    return retval;
                }
                else
                {
                    return true;
                }
            }
            set { ViewState["ContactUnChecked"] = value; }
        }

        #endregion

        #region Constants

        private const string cSearchClientValidationGroupVSK = "SearchClientValidationGroup";
        private const string cEnterpriseResultsVSK = "EnterpriseResults";

        #endregion

        #region Fields

        private object mDummy;
        private string maritalStatusSelValue;
        private bool mShowContact = true;
        private string mValidationGroup = String.Empty;
        private string mSearchClientValidationGroup = Guid.NewGuid().ToString();

        #endregion

        #region Properties

        public string MaritalStatusSelValue
        {
            get { return maritalStatusSelValue; }
            set { maritalStatusSelValue = value; }
        }

        public bool RequireMinimumAge
        {
            get { return (ViewState["RequireMinimumAge"] as bool?).GetValueOrDefault(false); }
            set { ViewState["RequireMinimumAge"] = value; }
        }

        public bool LastSearchSuccessful
        {
            get { return (ViewState["LastSearchSuccessful"] as bool?).GetValueOrDefault(false); }
            set { ViewState["LastSearchSuccessful"] = value; }
        }

        protected bool CanEditExtraAddress
        {
            get { return (ViewState["CanEditExtraAddress"] as bool?).GetValueOrDefault(false); }
            set { ViewState["CanEditExtraAddress"] = value; }
        }

        protected bool CanEditAddress
        {
            get { return (ViewState["CanEditAddress"] as bool?).GetValueOrDefault(false); }
            set { ViewState["CanEditAddress"] = value; }
        }

        public bool RequireDriverLicense
        {
            get { return (ViewState["RequireDriverLicense"] as bool?).GetValueOrDefault(false); }
            set { ViewState["RequireDriverLicense"] = value; }
        }

        protected GlobalDS.SABER_PESSOADataTable EnterpriseResults
        {
            get { return (GlobalDS.SABER_PESSOADataTable)ViewState[cEnterpriseResultsVSK]; }
            set { ViewState[cEnterpriseResultsVSK] = value; }
        }

        //protected string SelectedEnterpriseID
        //{
        //    get { return mEnterpriseClientBox.SelectedValue; }
        //    set { mEnterpriseClientBox.SelectedValue = value; }
        //}

        public bool IsEnterpriseClient
        {
            get { return BusinessHelper.IsEnterpriseNif(mNifSearchBox.Text); }
        }

        public string StartNif
        {
            get { return mStartNifField.Value; }
            set { mStartNifField.Value = value; }
        }

        public string ValidationGroup
        {
            get { return mValidationGroup; }
            set { mValidationGroup = value; }
        }

        public bool ShowContact
        {
            get { return mShowContact; }
            set { mShowContact = value; }
        }

        /* public string DriverLicense
         {
             get
             {
                 return mDriverLicenseBox.Text;
             }
             set {
                 mDriverLicenseBox.Text = value;
             }
         }

         public DateTime? DriverLicenseDate
         {
             get { return TryConvert.ToDateTime(mDriverLicenseDateBox.Text); }
             set { 
                 mDriverLicenseDateBox.Text = value.Value.ToString("dd-MM-yyyy");
             }
         }*/

        /// <summary>
        /// Gets or sets the Header Panel's SkinID.
        /// </summary>
        /*  public string HeaderPanelSkinID
          {
              get { return mHeaderPanel.SkinID; }
              set { mHeaderPanel.SkinID = value; }
          }
          */

        public DateTime? DateOfBirth
        {
            get { return TryConvert.ToDateTime(mDateOfBirthBox.Text); }
        }

        public decimal? ClientNumber
        {
            get { return TryConvert.ToDecimal(mClientNumberBox.Text); }
            set { mClientNumberBox.Text = value.ToString(); }
        }

        public string Nif
        {
            get { return mNifBox.Text; }
        }

        public string pCodigo
        {
            get { return mClientNumberBox.Text; }
        }


        public string IdCardNumber
        {
            get { return mIdCardNumberBox.Text; }
            set { mIdCardNumberBox.Text = value; }
        }

        public string GenderID
        {
            get { return mGenderBox.SelectedValue; }
            set { mGenderBox.SelectedValue = value; }
        }

        public string MaritalStatusID
        {
            get { return mMaritalStatusBox.SelectedValue; }
            set { mMaritalStatusBox.SelectedValue = value; }
        }

        public string ProfessionID
        {
            get { return mProfessionBox.SelectedValue; }
        }

        //public string EnterpriseActivityID
        //{
        //    get { return mEnterpriseActivityBox.SelectedValue; }
        //}

        public string PersonTitleID
        {
            get { return mTitleBox.SelectedValue; }
            set
            {

                mTitleBox.SelectedValue = value;
            }
        }

        public string PersonName
        {
            get { return mNameBox.Text; }
            set
            {

                mNameBox.Text = value;

            }
        }

        public string NationalityID
        {
            get { return mNationalityBox.SelectedValue; }
            set { mNationalityBox.SelectedValue = value; }
        }

        public string AddressTypeID
        {
            get { return mAddressTypeBox.Text == String.Empty ? " " : mAddressTypeBox.Text; }
            set { mAddressTypeBox.Text = value; }
        }

        public string AddressTitle
        {
            get { return mAddressTitleBox.Text; }
        }

        public string AddressStreetName
        {
            get { return mAddressStreetNameBox.Text == String.Empty ? " " : mAddressStreetNameBox.Text; }
            set { mAddressStreetNameBox.Text = value; }
        }

        public string AddressStreetNumber
        {
            get { return mAddressStreetNumberBox.Text; }
            set { mAddressStreetNumberBox.Text = value; }
        }

        public string AddressFloorNumber
        {
            get { return mAddressFloorNumberBox.Text; }
            set { mAddressFloorNumberBox.Text = value; }
        }

        public string ZipCode4
        {
            get { return mZipCode4Box.Text; }
            set { mZipCode4Box.Text = value; }
        }

        public string ZipCode3
        {
            get { return mZipCode3Box.Text; }
            set { mZipCode3Box.Text = value; }
        }

        public string Locality
        {
            get { return mLocalityBox.Text; }
            set { mLocalityBox.Text = value; }
        }

        public string Local
        {
            get { return mLocalBox.Text; }
            set { mLocalBox.Text = value; }
        }

        public string CPALF
        {
            get { return mCPALFBox.Text; }
            set { mCPALFBox.Text = value; }
        }

        public string CountryID
        {
            get { return mCountryBox.SelectedValue; }
            set { mCountryBox.SelectedValue = value; }
        }

        public string PhoneNumber
        {
            get { return mPhoneNumberBox.Text; }
            set { mPhoneNumberBox.Text = value; }
        }

        public string MobilePhoneNumber
        {
            get { return mMobilePhoneNumberBox.Text; }
            set { mMobilePhoneNumberBox.Text = value; }
        }

        public string EMail
        {
            get { return mEMailBox.Text; }
            set { mEMailBox.Text = value; }
        }

        public string FaxNumber
        {
            get { return mFaxBox.Text; }
            set { mFaxBox.Text = value; }
        }

        public bool HasExtraContact
        {
            get { return mExtraContactBox.SelectedValue == "S"; }
            set { mExtraContactBox.SelectedValue = value ? "S" : "N"; }
        }

        public string AttentionOf
        {
            get { return HasExtraContact ? mAttentionOfBox.Text : String.Empty; }
        }

        public string ExtraAddressTypeID
        {
            get { return HasExtraContact ? mExtraAddressTypeBox.Text : String.Empty; }
        }

        public string ExtraAddressTitle
        {
            get { return HasExtraContact ? mExtraAddressTitleBox.Text : String.Empty; }
        }

        public string ExtraAddressStreetName
        {
            get { return HasExtraContact ? mExtraAddressStreetNameBox.Text : String.Empty; }
        }

        public string ExtraAddressStreetNumber
        {
            get { return HasExtraContact ? mExtraAddressStreetNumberBox.Text : String.Empty; }
        }

        public string ExtraAddressFloorNumber
        {
            get { return HasExtraContact ? mExtraAddressFloorNumberBox.Text : String.Empty; }
        }

        public string ExtraZipCode4
        {
            get { return HasExtraContact ? mExtraZipCode4Box.Text : String.Empty; }
        }

        public string ExtraZipCode3
        {
            get { return HasExtraContact ? mExtraZipCode3Box.Text : String.Empty; }
        }

        public string ExtraLocality
        {
            get { return HasExtraContact ? mExtraLocalityBox.Text : String.Empty; }
        }

        public string ExtraLocal
        {
            get { return mExtraLocalBox.Text; }
            set { mExtraLocalBox.Text = value; }
        }

        public string ExtraCPALF
        {
            get { return mExtraCPALFBox.Text; }
            set { mExtraCPALFBox.Text = value; }
        }

        public string ExtraCountryID
        {

            get { return HasExtraContact ? mExtraCountryBox.SelectedValue : String.Empty; }
        }

        public string ExtraPhoneNumber
        {
            get { return HasExtraContact ? mExtraPhoneNumberBox.Text : String.Empty; }
        }

        public string ExtraEMail
        {
            get { return HasExtraContact ? mExtraEMailBox.Text : String.Empty; }
        }

        public string ExtraFaxNumber
        {
            get { return HasExtraContact ? mExtraFaxBox.Text : String.Empty; }
        }

        public string ExtraContractRadioButtonGroupName
        {
            get
            {
                string xValue = (string)ViewState["ExtraContractRadioButtonGroupName"];
                if (String.IsNullOrEmpty(xValue))
                {
                    xValue = Guid.NewGuid().ToString();
                    ViewState["ExtraContractRadioButtonGroupName"] = xValue;
                }
                return xValue;
            }
        }

        private bool HasSearchResult
        {
            get { return (ViewState["HasSearchResult"] as bool?).GetValueOrDefault(false); }
            set { ViewState["HasSearchResult"] = value; }
        }

        public event EventHandler userChanged;

        //protected ChDS.LISTA_VALORES_PARENTDataTable KinResults
        //{
        //    get { return (ChDS.LISTA_VALORES_PARENTDataTable)ViewState["KinList"]; }
        //    set { ViewState["KinList"] = value; }
        //}

        #endregion

        #region ----- Evento Page_Load -----

        /// <summary>
        /// Evento Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
            if (pHideProfession)
            {
                mSearchProfessionButton.Enabled = false;
                // mProfessionLabel.Visible = false;
                //mProfessionBoxUpdatePanel.Visible = false;
                //mProfessionBoxUpdatePanel.EnableViewState = false;
            }
        }

        #endregion

        #region >>>> Page Events <<<<
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLists();

                if (StartNif == "-")
                {
                    StartNif = string.Empty;
                }

                if (!String.IsNullOrEmpty(StartNif))
                {
                    mNifSearchBox.Text = StartNif;
                    RunSearch();
                }
                else if (pIdPlano == 0)
                {
                    ClearAllFields();
                }


                if (!string.IsNullOrEmpty(TelemovelTomador))
                {
                    mMobilePhoneNumberBox.Text = TelemovelTomador;
                }

                // initialize all flags
                UpdateCanEditAddress();
                UpdateCanEditExtraAddress();

            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // configure the contract panel
            mContactPanel.Visible = mShowContact;

            // configure the extra contact panel
            mExtraContactDetailsPanelHolder.Visible = ((pPermitirContactoSecundario) && HasExtraContact);

            // configure the search panel
            WebUtility.SetValidationGroups(mSearchPanel, mSearchClientValidationGroup);

            // configure the nif box validation
            //mNifBox.Enabled = false;// String.IsNullOrEmpty(mNifBox.Text) || verificarPodeAlterarCampo(mNifBox);

            // configure the id card
            mIdCardNumberBox.Enabled = !HasSearchResult || String.IsNullOrEmpty(mIdCardNumberBox.Text);

            // configure the birth date
            mDateOfBirthBox.Enabled = String.IsNullOrEmpty(mDateOfBirthBox.Text) || verificarPodeAlterarCampo(mDateOfBirthBox);

            // configure the gender box
            mGenderBox.Enabled = (mGenderBox.SelectedIndex < 0) || verificarPodeAlterarCampo(mGenderBox);

            // configure the marital status
            mMaritalStatusBox.Enabled = !HasSearchResult || mMaritalStatusBox.SelectedIndex < 0;

            // configure the profession
            mSearchProfessionButton.Enabled = !HasSearchResult || mProfessionBox.SelectedIndex <= 0;

            // configure the title
            mTitleBox.Enabled = !HasSearchResult || mTitleBox.SelectedIndex <= 0;

            // configure the name
            mNameBox.Enabled = String.IsNullOrEmpty(mNameBox.Text) || verificarPodeAlterarCampo(mNameBox);

            //Configure the country
            mCountryBox.Enabled = !HasSearchResult || mCountryBox.SelectedIndex < 0;

            // configure the nationality
            mNationalityBox.Enabled = !HasSearchResult || mNationalityBox.SelectedIndex < 0;


            // keep the profession show box updated
            mProfessionShowBox.Text = mProfessionBox.SelectedIndex > 0 ? mProfessionBox.SelectedItem.Text : String.Empty;

            // set the address controls validation group
            WebUtility.SetValidationGroups(mContactPanel, ValidationGroup);

            // set the address controls enabled or not
            mAddressTypeBox.Visible = !CanEditAddress;
            mAddressTitleBox.Visible = !CanEditAddress;
            mSearchContactButton.Visible = !CanEditAddress && !IsPlan;
            mAddressStreetNumberBox.Enabled = !IsPlan;
            mAddressFloorNumberBox.Enabled = !IsPlan;
            mAddressStreetNameBox.Enabled =                
            mZipCode4Box.Enabled =
            mZipCode3Box.Enabled =
            mLocalityBox.Enabled =
            mLocalBox.Enabled =
            mCPALFBox.Enabled =
            mExtraLocalBox.Enabled =
            mExtraCPALFBox.Enabled = CanEditExtraAddress;

            mPhoneNumberBox.Enabled = !IsPlan;
            mMobilePhoneNumberBox.Enabled = !IsPlan;
            mEMailBox.Enabled = !IsPlan;
            mFaxBox.Enabled = !IsPlan;

            // set the extra address controls validation group
            //WebUtility.SetValidationGroups(mExtraContactPanel, ValidationGroup);

            // set the extra address controls enabled or not
            mExtraAddressTypeBox.Visible = !CanEditExtraAddress;
            mExtraAddressTitleBox.Visible = !CanEditExtraAddress;
            mSearchExtraContactButton.Visible = !CanEditExtraAddress;


            //mExtraAddressStreetNumberBox.Enabled =
            //mExtraAddressFloorNumberBox.Enabled =
            mExtraAddressTypeBox.Enabled =
            mExtraAddressTitleBox.Enabled =
            mExtraAddressStreetNameBox.Enabled =
            mExtraZipCode4Box.Enabled =
            mExtraZipCode3Box.Enabled =
            mExtraLocalityBox.Enabled = CanEditExtraAddress;

            // allow the profession box to be double clicked
            mProfessionBox.Attributes["ondblclick"] =
                Page.ClientScript.GetPostBackClientHyperlink(mConfirmSearchProfessionButton, String.Empty, true);


            //woraround para posicionameto dos popups
            mSearchContactButton.Attributes.Add("open-popup", mContactSearchPanelExtender.ClientID);
            mSearchExtraContactButton.Attributes.Add("open-popup", mExtraContactAddressSearchExtender.ClientID);
            mSearchProfessionButton.Attributes.Add("open-popup", mProfessionSearchPopup.ClientID);


            // done
            base.OnPreRender(e);
        }

        #endregion


        #region ----- Guardar dados Pessoais (mínimos) obrigatórios não preenchidos -----

        /// <summary>
        /// Guardar dados Pessoais (mínimos) obrigatórios não preenchidos
        /// </summary>
        private void guardarDadosPessoaisObrigatorios()
        {
            List<String> _lstTitularDadosInexistentes = new List<String>();

            // Trim aos controlos
            ClControlos.trimTextBoxes(this.Controls);

            // Verificar se está a editar dados de Titular pesquisado
            // e se dados Pessoais (mínimos) são obrigatórios
            if ((HasSearchResult) && (pDadosPessoaisObrigatorios))
            {
                // Verificar se não tem NIF
                if (mNifBox.Text == String.Empty)
                {
                    _lstTitularDadosInexistentes.Add(mNifBox.ClientID);
                }

                // Verificar se não tem Nome
                if (mNameBox.Text == String.Empty)
                {
                    _lstTitularDadosInexistentes.Add(mNameBox.ClientID);
                }

                // Verificar se não tem Data Nascimento
                if (mDateOfBirthBox.Text == String.Empty)
                {
                    _lstTitularDadosInexistentes.Add(mDateOfBirthBox.ClientID);
                }

                // Verificar se não tem Sexo
                if (mGenderBox.SelectedIndex < 0)
                {
                    _lstTitularDadosInexistentes.Add(mGenderBox.ClientID);
                }
            }

            // Guardar em ViewState
            pTitularDadosInexistentes = _lstTitularDadosInexistentes;
        }

        #endregion

        #region ----- Verificar se pode alterar campo -----

        /// <summary>
        /// Verificar se pode alterar campo
        /// </summary>
        /// <param name="prmControlo">Controlo</param>
        /// <returns>Indica se pode alterar o campo</returns>
        private Boolean verificarPodeAlterarCampo(WebControl prmControlo)
        {
            // Verificar se não está a editar dados de Titular pesquisado
            // ou se o campo está incluido nos de dados Pessoais (mínimos) obrigatórios
            return ((!HasSearchResult) || (pTitularDadosInexistentes.Contains(prmControlo.ClientID)));
        }

        #endregion

        #region ----- Validar -----

        #region ----- Validar dados para Pesquisar -----

        /// <summary>
        /// Validar dados para Pesquisar
        /// </summary>
        /// <param name="prmErros">Erros</param>
        /// <returns>Indica se os dados são válidos</returns>
        private Boolean validarPesquisar(ref StringBuilder prmErros)
        {
            Boolean _blnValido = true;
            String _strErro;

            // Trim
            ClControlos.trimTextBoxes(mSearchPanel.Controls);

            // Actualizar painéis
            actualizarPaineis();

            // Verificar se preencheu algum critério de pesquisa
            if ((mClientNumberSearchBox.Text != String.Empty) ||
                (mNifSearchBox.Text != String.Empty))
            {
                // N.º Cliente
                if (!ClNumericos.isNumeric(mClientNumberSearchBox,
                                            false,
                                            ClNumericos.TipoValorNumerico.Positivo,
                                            String.Empty,
                                            out _strErro))
                {
                    // Erro
                    ClErros.registarErro(ref prmErros,
                                            String.Empty,
                                            "O Número de Cliente a Pesquisar não é válido.",
                                            Environment.NewLine);
                    _blnValido = false;
                }

                // NIF
                if ((mNifSearchBox.Text != String.Empty) &&
                    (!BusinessHelper.IsValidNif(mNifSearchBox.Text)))
                {
                    // Erro
                    ClErros.registarErro(ref prmErros,
                                            String.Empty,
                                            "O Número de Identificação Fiscal a pesquisar não é válido.",
                                            Environment.NewLine);
                    _blnValido = false;
                }
            }
            else
            {
                // Erro
                ClErros.registarErro(ref prmErros,
                                        String.Empty,
                                        "Defina um critério de Pesquisa.",
                                        Environment.NewLine);
                _blnValido = false;
            }

            return _blnValido;
        }

        #endregion

        #region ----- Validar dados para Gravar -----


        public void validar(ref string prmErros)
        {

            // Trim
            ClControlos.trimTextBoxes(this.Controls);

            // Dados Pessoais
            validarDadosPessoais(ref prmErros);

            // Contactos Pessoais
            validarContactosPessoais(ref prmErros);

        }

        #region ----- Validar dados Pessoais -----

        private void validarDadosPessoais(ref string _strErro)
        {

            // Verificar se os dados Pessoais estão vísiveis
            if (mPersonalInfoPanel.Visible)
            {
                // NIF
                if ((mNifBox.Enabled) &&
                    ((IsRequireNifByAge) || (verificarPodeAlterarCampo(mNifBox)) || (mNifBox.Text != String.Empty)))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, validarNIF(mNifBox.Text));
                }

                // BI
                if (mIdCardNumberBox.Enabled)
                {
                    string _dummy = string.Empty;
                    if (!ClNumericos.isNumeric(mIdCardNumberBox,
                                                false,
                                                ClNumericos.TipoValorNumerico.Positivo,
                                                String.Empty,
                                                out _dummy))
                    {
                        BusinessHelper.AddErroAtList(ref _strErro, "O Número de Bilhete de Identidade não é válido.");

                    }
                }

                // Data de Nascimento
                if (mDateOfBirthBox.Enabled)
                {
                    string _ErroMsgTemp = string.Empty;

                    if (mDateOfBirthBox.Text == String.Empty)
                    {
                        _ErroMsgTemp = "Preencha a Data de Nascimento.";
                    }
                    DateTime? _data = TryConvert.ToDateTime(mDateOfBirthBox.Text);
                    if (string.IsNullOrEmpty(_ErroMsgTemp) && !_data.HasValue)
                    {
                        _ErroMsgTemp = "A Data de Nascimento não é válida.";
                    }

                    if (string.IsNullOrEmpty(_ErroMsgTemp) && _data.Value > ClDatas.getDiaActual())
                    {
                        _ErroMsgTemp = "A Data de Nascimento não pode ser superior à Data actual.";
                    }

                    if (string.IsNullOrEmpty(_ErroMsgTemp) && BusinessHelper.getAge(_data.Value) < 18 && pPersonType == "1")
                    {
                        _ErroMsgTemp = "O Tomador não pode ser menor de idade.";
                    }


                    BusinessHelper.AddErroAtList(ref _strErro, _ErroMsgTemp);
                }

                // Sexo
                if (mGenderBox.Enabled && string.IsNullOrEmpty(mGenderBox.SelectedValue))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Seleccione o Sexo.");
                }

                // Estado Civil
                if (mMaritalStatusBox.Enabled && string.IsNullOrEmpty(mMaritalStatusBox.SelectedValue))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Seleccione o Estado Civil.");
                }

                // Profissão
                if (mSearchProfessionButton.Enabled && mProfessionBox.SelectedIndex <= 0 && pPersonType == "1")//Restrição para ignorar a validação quando a pessoa é um beneficiário
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Seleccione a Profissão.");
                }

                // Nome
                if (mNameBox.Enabled && string.IsNullOrEmpty(mNameBox.Text))
                {
                    string _tempErroMsg = string.Empty;

                    if (string.IsNullOrEmpty(mNameBox.Text.Trim()))
                    {
                        _tempErroMsg = "Preencha o Nome.";
                    }
                    if (string.IsNullOrEmpty(_tempErroMsg) && !mNameBox.Text.Contains(" "))
                    {
                        _tempErroMsg = "A Pessoa Segura tem de ter Nome e Apelido.";
                    }

                    BusinessHelper.AddErroAtList(ref _strErro, _tempErroMsg);
                }

                // Nacionalidade
                if (mNationalityBox.Enabled && mNationalityBox.SelectedIndex <= 0)
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Seleccione a Nacionalidade.");
                }

            }
        }
        #endregion

        #region ----- Validar contactos Pessoais -----

        private void validarContactosPessoais(ref string _strErro)
        {
            /******************* Verificar se tem contactos *********************/
            // Código Postal de 4
            if (mZipCode4Box.Enabled && string.IsNullOrEmpty(mZipCode4Box.Text))
            {
                BusinessHelper.AddErroAtList(ref _strErro, "Preencha o Código Postal (4) da Morada.");
            }

            // Código Postal de 3
            if (mZipCode3Box.Enabled && string.IsNullOrEmpty(mZipCode3Box.Text))
            {
                BusinessHelper.AddErroAtList(ref _strErro, "Preencha o Código Postal (3) da Morada.");
            }

            // Localidade
            if (mLocalityBox.Enabled && string.IsNullOrEmpty(mLocalityBox.Text))
            {
                BusinessHelper.AddErroAtList(ref _strErro, "Preencha a Localidade da Morada.");
            }

            //Telefone
            if (!string.IsNullOrEmpty(mPhoneNumberBox.Text))
            {
                if (mPhoneNumberBox.Text.Length < 9 || !mPhoneNumberBox.Text.StartsWith("2"))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Telefone inválido.");
                }                
            }            

            //Telemóvel
            if (!string.IsNullOrEmpty(mMobilePhoneNumberBox.Text))
            {
                if (mMobilePhoneNumberBox.Text.Length < 9 || !mMobilePhoneNumberBox.Text.StartsWith("9"))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Telemóvel inválido.");
                }
            }            

            // E-mail
            if (!string.IsNullOrEmpty(mEMailBox.Text) && (!ClEmail.emailValido(mEMailBox.Text)))
            {
                BusinessHelper.AddErroAtList(ref _strErro, "Email inválido.");
            }


            /******************* Verificar se tem contactos extra *********************/
            if ((pPermitirContactoSecundario) && (HasExtraContact))
            {

                // Código Postal de 4
                if (string.IsNullOrEmpty(mExtraZipCode4Box.Text))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Preencha o Código Postal (4) da Morada de Correspondência.");
                }

                // Código Postal de 3
                if (string.IsNullOrEmpty(mExtraZipCode3Box.Text))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Preencha o Código Postal (3) da Morada de Correspondência.");
                }

                // E-mail
                if (!string.IsNullOrEmpty(mExtraEMailBox.Text) && (!ClEmail.emailValido(mExtraEMailBox.Text)))
                {
                    BusinessHelper.AddErroAtList(ref _strErro, "Email da Morada de Correspondência inválido.");
                }

            }
        }

        #endregion

        #region ----- Validar NIF -----

        private string validarNIF(string nif)
        {
            string _Erro = string.Empty;

            if (string.IsNullOrEmpty(nif))
            {
                _Erro = "Preencha o Número de Identificação Fiscal.";
            }

            if (!string.IsNullOrEmpty(nif) && !BusinessHelper.IsValidNif(nif))
            {
                _Erro = "O Número de Identificação Fiscal não é válido.";
            }

            if (!string.IsNullOrEmpty(nif) && BusinessHelper.IsEnterpriseNif(nif))
            {
                _Erro = "Não é permitido NIF de Empresa.";
            }

            return _Erro;
        }

        #endregion

        #endregion

        #endregion

        #region ----- Get Data -----

        public Pessoa getPerson()
        {
            return new Pessoa()
            {
                TIPOPESSOA = pPersonType,
                PCODIGO = mClientNumberBox.Text,
                PCONTRIB = mNifBox.Text,
                PBI = mIdCardNumberBox.Text,
                DT_NASCIMENTO = mDateOfBirthBox.Text,
                PTIPO = mGenderBox.SelectedValue,
                ESTADO_CIVIL = mMaritalStatusBox.SelectedValue,
                PPROFISS = mProfessionBox.SelectedValue,
                PTITULO = mTitleBox.SelectedValue,
                PNOME = mNameBox.Text,
                NACIONALIDADE = mNationalityBox.SelectedValue,
                TELEMOVEL = mMobilePhoneNumberBox.Text == "" ? TelemovelTomador : mMobilePhoneNumberBox.Text,

                Contacto01 = new Endereco()
                {
                    AO_CUIDADO = mNameBox.Text,
                    MORADA_TIPO = mAddressTypeBox.Text,
                    TITULO_ARTERIA = mAddressTitleBox.Text,
                    MORADA_DESC = mAddressStreetNameBox.Text,
                    MORADA_NUMERO = mAddressStreetNumberBox.Text,
                    MORADA_ANDAR = mAddressFloorNumberBox.Text,
                    CODIGO_POSTAL = mZipCode4Box.Text,
                    SUFIXO_POSTAL = mZipCode3Box.Text,
                    LOCALIDADE = mLocalityBox.Text,
                    PAIS = mCountryBox.SelectedValue,
                    TELEFONE = mPhoneNumberBox.Text,
                    E_MAIL = mEMailBox.Text == "" ? EmailTomador : mEMailBox.Text,
                    TELEX_FAX = mFaxBox.Text,
                    LOCAL = mLocalBox.Text,
                    CPALF = mCPALFBox.Text
                },

                Contacto02 = new Endereco()
                {
                    AO_CUIDADO = mAttentionOfBox.Text,
                    MORADA_TIPO = mExtraAddressTypeBox.Text,
                    TITULO_ARTERIA = mExtraAddressTitleBox.Text,
                    MORADA_DESC = mExtraAddressStreetNameBox.Text,
                    MORADA_NUMERO = mExtraAddressStreetNumberBox.Text,
                    MORADA_ANDAR = mExtraAddressFloorNumberBox.Text,
                    CODIGO_POSTAL = mExtraZipCode4Box.Text,
                    SUFIXO_POSTAL = mExtraZipCode3Box.Text,
                    LOCALIDADE = mExtraLocalityBox.Text,
                    PAIS = mExtraCountryBox.SelectedValue,
                    TELEFONE = mPhoneNumberBox.Text,
                    E_MAIL = mExtraEMailBox.Text,
                    TELEX_FAX = mExtraFaxBox.Text,
                    LOCAL = mExtraLocalBox.Text,
                    CPALF = mExtraCPALFBox.Text
                }
            };
        }

        #endregion

        #region ----- Actualizar painéis -----

        /// <summary>
        /// Actualizar painéis
        /// </summary>
        private void actualizarPaineis()
        {
            List<UpdatePanel> _lstUpdatePanels = ClControlos.findControlsByType<UpdatePanel>(this.Controls);

            // Percorrer a lista e forçar o Update
            foreach (UpdatePanel _upUpdatePanel in _lstUpdatePanels)
            {
                if (_upUpdatePanel.UpdateMode == UpdatePanelUpdateMode.Conditional)
                {
                    _upUpdatePanel.Update();
                }
            }
        }

        #endregion

        #region ----- LoadUserData -----


        //public void loadData(XmlDocument propUserXMLData, string equalToHolder)
        //{
        //    pOpsIgualATomador = equalToHolder;
        //    if (equalToHolder.Equals("S"))
        //    {
        //        cpeDadosPessoais.Collapsed = true;
        //        cpeDadosPessoais.ClientState = "true";
        //        cpeDadosContacto.Collapsed = true;
        //        cpeDadosContacto.ClientState = "true";
        //        //cpeParentesco.Collapsed = true;
        //        //cpeParentesco.ClientState = "true";
        //    }

        //    XmlNode personalDataNode = propUserXMLData.SelectSingleNode("//PROPOSAL_SCREEN_DATA/INSURED_PERSON_LIST/INSURED_PERSON[@OPSNum='" + pNumeroPessoaSegura.ToString() + "']/PERSONAL_DATA");
        //    ClientNumber = readInt32FromXMLNode(personalDataNode, "CODE");
        //    pNIF = readStringFromXMLNode(personalDataNode, "NIF");
        //    IdCardNumber = readStringFromXMLNode(personalDataNode, "BI");
        //    pDataNascimento = readDateTimeFromXMLNode(personalDataNode, "BIRTH_DATE", "dd-MM-yyyy");
        //    //DriverLicense = readStringFromXMLNode(personalDataNode, "LICENSE_NUMBER");
        //    PersonName = readStringFromXMLNode(personalDataNode, "NAME");
        //    GenderID = readStringFromXMLNode(personalDataNode, "GENDER");
        //    MaritalStatusID = readStringFromXMLNode(personalDataNode, "CIVIL_STATUS");
        //    PersonTitleID = readStringFromXMLNode(personalDataNode, "DESCRIPTION");
        //    //pConfidencialidade = readStringFromXMLNode(personalDataNode, "CONFIDENTIALITY");

        //    //ObjectDataSource1.Select();
        //    //ParentDrop.DataBind();


        //    //pParentesco = readStringFromXMLNode(personalDataNode, "KINSHIP");

        //    XmlNode contactNode = propUserXMLData.SelectSingleNode("//PROPOSAL_SCREEN_DATA/INSURED_PERSON_LIST/INSURED_PERSON[@OPSNum='" + pNumeroPessoaSegura.ToString() + "']/CONTACT_LIST/CONTACT[@CONTACTNum='1']");
        //    AddressTypeID = readStringFromXMLNode(contactNode, "TYPE");
        //    AddressStreetName = readStringFromXMLNode(contactNode, "ADRESS");
        //    AddressStreetNumber = readStringFromXMLNode(contactNode, "ADRESS_NUMBER");
        //    AddressFloorNumber = readStringFromXMLNode(contactNode, "ADRESS_FLOOR");
        //    ZipCode4 = readStringFromXMLNode(contactNode, "POSTAL_CODE");
        //    ZipCode3 = readStringFromXMLNode(contactNode, "SUBPOSTAL_CODE");
        //    Locality = readStringFromXMLNode(contactNode, "LOCALITY");
        //    CountryID = readStringFromXMLNode(contactNode, "COUNTRY");
        //    PhoneNumber = readStringFromXMLNode(contactNode, "PHONE");
        //    FaxNumber = readStringFromXMLNode(contactNode, "FAX");
        //    EMail = readStringFromXMLNode(contactNode, "EMAIL");

        //}



        #endregion

        #region >>>> Elements Events <<<<

        /*** BUTTONS ***/
        protected void mClearClientButton_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        /*protected void mSearchProfessionButton_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(mGenderBox.SelectedValue))
            {
                ScriptHelper.Focus(mProfessionBox);

                mProfessionSearchPanel.Visible = true;
                mProfessionSearchPopup.Show();
            }
            else
            {
                ScriptHelper.ShowMessageBox("Seleciona o Sexo para conseguir selecionar a profissão", "Proposta", ScriptHelper.AlertType.ALERT);
            }

        }*/

        protected void mSearchClientValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !(mClientNumberSearchBox.Text.Trim().Length > 0 && mNifSearchBox.Text.Trim().Length > 0);
        }

        protected void mSearchClientButton_Click(object sender, EventArgs e)
        {
            //Valida Nif na Lista Negra            


            if (!String.IsNullOrEmpty(mNifSearchBox.Text))
            {
                string validationMessage = string.Empty;
                int nif = Convert.ToInt32(mNifSearchBox.Text);

                validationMessage = WsHelper.ValidateNifAgainstBlackList(nif,
                ConfigurationManager.AppSettings["NetworkCredential_user"],
                ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                if (!String.IsNullOrEmpty(validationMessage))
                {
                    ScriptHelper.ShowMessageBox(validationMessage, "Proposta", ScriptHelper.AlertType.ERROR);
                    ShowBlankPerson();
                    mNifBox.Text = string.Empty;
                }
                else
                {
                    RunSearch();
                }
            }
            else if (!String.IsNullOrEmpty(mClientNumberSearchBox.Text))
            {
                RunSearch();
            }



            if (userChanged != null)
            {
                userChanged(sender, e);
            }
        }

        /*** Cliente DDL ***/
        protected void mPersonClientBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPersonDetails(PessoaDS.getClient(mPersonClientBox.SelectedValue, !string.IsNullOrEmpty(mPersonClientBox.SelectedValue) ? mNifSearchBox.Text : string.Empty));
        }

        /*** PopUps ***/
        protected void mContactAddressSearch_AddressChosen(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(mContactAddressSearch.SelectedRowID))
            {
                List<EnderecosCTTPesq> xTable = OSBConnector.Shared.DataServices.SaberEnderecosCTTROWID(mContactAddressSearch.SelectedRowID);
                EnderecosCTTPesq xAddress = (xTable != null && xTable.Count > 0 ? xTable[0] : new EnderecosCTTPesq());

                mAddressTypeBox.Text = xAddress.TIPO_ART;
                mAddressTitleBox.Text = xAddress.TITULO_ART;
                mAddressStreetNameBox.Text = xAddress.ARTERIA;
                mZipCode4Box.Text = xAddress.CP4;
                mZipCode3Box.Text = xAddress.CP3;

                if (!string.IsNullOrEmpty(xAddress.LUGAR))
                {
                    mLocalityBox.Text = xAddress.LUGAR;
                    mCPALFBox.Visible = false;
                    mLocalityBox.Visible = true;
                }
                else
                {
                    mLocalityBox.Text = string.Empty;
                    mCPALFBox.Visible = true;
                    mLocalityBox.Visible = false;
                }

                if (string.IsNullOrEmpty(xAddress.LOCAL))
                {
                    mLocalBox.Text = string.Empty;
                }
                else
                {
                    mLocalBox.Text = xAddress.LOCAL;
                }
                if (string.IsNullOrEmpty(xAddress.CPALF))
                {
                    mCPALFBox.Text = string.Empty;
                }
                else
                {
                    mCPALFBox.Text = xAddress.CPALF;
                }
            }

            // Trim aos controlos
            ClControlos.trimTextBoxes(mContactPanel.Controls);
        }

        /*
        protected void mContactAddressSearch_AddressChosen_Old(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(mContactAddressSearch.SelectedRowID))
            {
                GlobalDS.SABER_ENDERECOS_CTT_ROWIDDataTable xTable = (new SABER_ENDERECOS_CTT_ROWIDTableAdapter()).GetData(mContactAddressSearch.SelectedRowID, out mDummy);
                GlobalDS.SABER_ENDERECOS_CTT_ROWIDRow xAddress = (xTable.Count > 0 ? xTable[0] : xTable.NewSABER_ENDERECOS_CTT_ROWIDRow());

                mAddressTypeBox.Text = xAddress[xTable.TIPO_ARTColumn].ToString();
                mAddressTitleBox.Text = xAddress[xTable.TITULO_ARTColumn].ToString();
                mAddressStreetNameBox.Text = xAddress[xTable.ARTERIAColumn].ToString();
                mZipCode4Box.Text = xAddress[xTable.CP4Column].ToString();
                mZipCode3Box.Text = xAddress[xTable.CP3Column].ToString();

                if (!xAddress.IsLUGARNull() && xAddress.LUGAR.Trim().Length > 0)
                {
                    mLocalityBox.Text = xAddress[xTable.LUGARColumn].ToString();
                    mCPALFBox.Visible = false;
                    mLocalityBox.Visible = true;
                }
                else
                {
                    mLocalityBox.Text = string.Empty;
                    mCPALFBox.Visible = true;
                    mLocalityBox.Visible = false;
                }

                if (xAddress.IsLOCALNull())
                {
                    mLocalBox.Text = string.Empty;
                }
                else
                {
                    mLocalBox.Text = xAddress[xTable.LOCALColumn].ToString();
                }
                if (xAddress.IsCPALFNull())
                {
                    mCPALFBox.Text = string.Empty;
                }
                else
                {
                    mCPALFBox.Text = xAddress[xTable.CPALFColumn].ToString();
                }
            }

            // Trim aos controlos
            ClControlos.trimTextBoxes(mContactPanel.Controls);
        }
        */

        protected void mContactAddressSearch_AddressSearch(object sender, EventArgs e)
        {
            mContactSearchPanelExtender.Show();
        }

        protected void mExtraContactAddressSearch_AddressChosen(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(mExtraContactAddressSearch.SelectedRowID))
            {
                List<EnderecosCTTPesq> xTable = OSBConnector.Shared.DataServices.SaberEnderecosCTTROWID(mContactAddressSearch.SelectedRowID);
                EnderecosCTTPesq xAddress = (xTable != null && xTable.Count > 0 ? xTable[0] : new EnderecosCTTPesq());

                mExtraAddressTypeBox.Text = xAddress.TIPO_ART;
                mExtraAddressTitleBox.Text = xAddress.TITULO_ART;
                mExtraAddressStreetNameBox.Text = xAddress.ARTERIA;
                mExtraZipCode4Box.Text = xAddress.CP4;
                mExtraZipCode3Box.Text = xAddress.CP3;

                if (!string.IsNullOrEmpty(xAddress.LUGAR))
                {
                    mExtraLocalityBox.Text = xAddress.LUGAR;
                    mExtraCPALFBox.Visible = false;
                    mExtraLocalityBox.Visible = true;
                }
                else
                {
                    mExtraLocalityBox.Text = string.Empty;
                    mExtraCPALFBox.Visible = true;
                    mExtraLocalityBox.Visible = false;
                }

                if (string.IsNullOrEmpty(xAddress.LOCAL))
                {
                    mExtraLocalBox.Text = string.Empty;
                }
                else
                {
                    mExtraLocalBox.Text = xAddress.LOCAL;
                }
                if (string.IsNullOrEmpty(xAddress.CPALF))
                {
                    mExtraCPALFBox.Text = string.Empty;
                }
                else
                {
                    mExtraCPALFBox.Text = xAddress.CPALF;
                }
            }

            // Trim aos controlos
            ClControlos.trimTextBoxes(mExtraContactPanel.Controls);

        }

        /*
        protected void mExtraContactAddressSearch_AddressChosen_Old(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(mExtraContactAddressSearch.SelectedRowID))
            {
                GlobalDS.SABER_ENDERECOS_CTT_ROWIDDataTable xTable = (new SABER_ENDERECOS_CTT_ROWIDTableAdapter()).GetData(mExtraContactAddressSearch.SelectedRowID, out mDummy);
                GlobalDS.SABER_ENDERECOS_CTT_ROWIDRow xAddress = (xTable.Count > 0 ? xTable[0] : xTable.NewSABER_ENDERECOS_CTT_ROWIDRow());

                mExtraAddressTypeBox.Text = xAddress[xTable.TIPO_ARTColumn].ToString();
                mExtraAddressTitleBox.Text = xAddress[xTable.TITULO_ARTColumn].ToString();
                mExtraAddressStreetNameBox.Text = xAddress[xTable.ARTERIAColumn].ToString();
                mExtraZipCode4Box.Text = xAddress[xTable.CP4Column].ToString();
                mExtraZipCode3Box.Text = xAddress[xTable.CP3Column].ToString();

                if (!xAddress.IsLUGARNull() && xAddress.LUGAR.Trim().Length > 0)
                {
                    mExtraLocalityBox.Text = xAddress[xTable.LUGARColumn].ToString();
                    mExtraCPALFBox.Visible = false;
                    mExtraLocalityBox.Visible = true;
                }
                else
                {
                    mExtraLocalityBox.Text = string.Empty;
                    mExtraCPALFBox.Visible = true;
                    mExtraLocalityBox.Visible = false;
                }

                if (xAddress.IsLOCALNull())
                {
                    mExtraLocalBox.Text = string.Empty;
                }
                else
                {
                    mExtraLocalBox.Text = xAddress[xTable.LOCALColumn].ToString();
                }
                if (xAddress.IsCPALFNull())
                {
                    mExtraCPALFBox.Text = string.Empty;
                }
                else
                {
                    mExtraCPALFBox.Text = xAddress[xTable.CPALFColumn].ToString();
                }
            }

            // Trim aos controlos
            ClControlos.trimTextBoxes(mExtraContactPanel.Controls);
        }
        */

        protected void mExtraContactAddressSearch_AddressSearch(object sender, EventArgs e)
        {
            mExtraContactAddressSearchExtender.Show();
        }

        /*** SEXO ***/
        protected void mGenderBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTitleBox();
            BindProfessionsBox();
        }

        /*** Country ***/
        protected void mCountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCanEditAddress();
            ClearContactFields();
        }

        protected void mExtraCountryBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCanEditExtraAddress();
            ClearExtraContactFields();
        }

        #endregion

        #region >>>> Bind Methods <<<<

        private void LoadPersonClientBox(string nif, int selectedIndex)
        {
            mPersonClientBox.DataSource = PessoaDS.getClientList(selectedIndex == 0 ? "" : selectedIndex.ToString(), nif);
            mPersonClientBox.DataTextField = "PNOME";
            mPersonClientBox.DataValueField = "PCODIGO";
            mPersonClientBox.DataBind();
            if (mPersonClientBox.Items.Count > 1)
            {
                mPersonClientBox.Items.Insert(0, new ListItem(Strings.PersonInfo_SelectPersonClient, String.Empty));
            }
            mPersonClientBox.Visible = mPersonClientPanel.Visible = mPersonClientBox.Items.Count > 1;

        }

        public void BindLists()
        {
            UtilityG.BindList(mGenderBox, UtilityG.GetValue(GenericLists.SEXO), false);
            UtilityG.BindList(mMaritalStatusBox, UtilityG.GetValue(GenericLists.ESTADO_CIVIL), false);
            UtilityG.BindList(mNationalityBox, UtilityG.GetValue(GenericLists.NACIONALIDADE), false);            
            UtilityG.BindList(mCountryBox, UtilityG.GetValue(GenericLists.PAIS), false);
            UtilityG.BindList(mExtraCountryBox, UtilityG.GetValue(GenericLists.PAIS), false);

            BindTitleBox();
            BindProfessionsBox();
        }

        private void BindTitleBox()
        {
            //string sexo = string.IsNullOrEmpty(mGenderBox.SelectedValue) ? DEFAULT_GENDER_CODE : mGenderBox.SelectedValue;

            UtilityG.BindList(mTitleBox, mGenderBox.SelectedValue, UtilityG.GetValue(GenericLists.TITLO_ONORIFICO), true);
        }

        private void BindProfessionsBox()
        {
            UtilityG.BindList(mProfessionBox, mGenderBox.SelectedValue, UtilityG.GetValue(GenericLists.PROFISSAO), false);
        }

        #endregion

        #region >>>> Helper Methods <<<<

        public void SetSearchPanelVisible(bool? visible)
        {
            mSearchPanel.Visible = visible.HasValue? visible.Value: false;
        }

        public void RunSearch()
        {
            StringBuilder _stbErros = new StringBuilder();
            string v_msg_erro;

            ContactUnChecked = true;

            // Validar dados para Pesquisar
            if (validarPesquisar(ref _stbErros))
            {
                if (!String.IsNullOrEmpty(mNifSearchBox.Text) && !BusinessHelper.IsValidNif(mNifSearchBox.Text) || (IsEnterpriseClient && !pPermitirNIFEmpresa))
                {
                    ScriptHelper.ShowMessageBox(Strings.FiscalNumberIsNotValid, "Proposta", ScriptHelper.AlertType.ERROR);
                }
                else
                {

                    if (BusinessHelper.VALIDA_NIF_TEM_CCP_FE(mNifSearchBox.Text))
                    {
                        v_msg_erro = "O Tomador de Seguro é um Cliente de Contratação Pública. O presente contrato tem de ser emitido centralmente. Contacte o seu técnico comercial.";
                        ScriptHelper.ShowMessageBox(v_msg_erro);
                    }

                    List<Pessoa> _clients = PessoaDS.getClientList(mClientNumberSearchBox.Text, mNifSearchBox.Text);

                    mNifBox.Enabled = false;
                    if (_clients.Count == 0)
                    {
                        ScriptHelper.ShowMessageBox(Strings.PersonSearch_NoPersonFound, "Proposta", ScriptHelper.AlertType.ALERT);
                        ShowBlankPerson();
                        HasSearchResult = false;
                        if (!string.IsNullOrEmpty(mNifSearchBox.Text))
                        {
                            mNifBox.Text = mNifSearchBox.Text;
                        }
                        else
                        {
                            mNifBox.Enabled = true;
                        }
                    }
                    else
                    {

                        HasSearchResult = true;
                        ShowPerson(_clients);
                    }

                    LoadPersonClientBox();
                    UpdateCanEditAddress();
                    UpdateCanEditExtraAddress();
                }
            }
            else
            {
                ScriptHelper.ShowMessageBox(_stbErros.ToString(), "Proposta", ScriptHelper.AlertType.ERROR);
            }
        }

        private void LoadPersonClientBox()
        {
            LoadPersonClientBox(mNifSearchBox.Text, TryConvert.ToInt32(mClientNumberSearchBox.Text) ?? 0);
        }



        public void ClearAllFields()
        {
            HasSearchResult = false;
            ClearSearchFields();
            ShowBlankPerson();
            LoadPersonClientBox();
        }

        private void ClearSearchFields()
        {
            mClientNumberSearchBox.Text = String.Empty;
            //mNifSearchBox.Text = String.Empty;
        }

        private void ClearContactFields()
        {
            mAddressTypeBox.Text = String.Empty;
            mAddressTitleBox.Text = String.Empty;
            mAddressStreetNameBox.Text = String.Empty;
            mAddressStreetNumberBox.Text = String.Empty;
            mAddressFloorNumberBox.Text = String.Empty;
            mZipCode4Box.Text = String.Empty;
            mZipCode3Box.Text = String.Empty;
            mLocalityBox.Text = String.Empty;
        }

        private void ClearExtraContactFields()
        {
            mExtraAddressTypeBox.Text = String.Empty;
            mExtraAddressTitleBox.Text = String.Empty;
            mExtraAddressStreetNameBox.Text = String.Empty;
            mExtraAddressStreetNumberBox.Text = String.Empty;
            mExtraAddressFloorNumberBox.Text = String.Empty;
            mExtraZipCode4Box.Text = String.Empty;
            mExtraZipCode3Box.Text = String.Empty;
            mExtraLocalityBox.Text = String.Empty;
        }

        protected bool IsRequireNifByAge
        {
            get
            {
                DateTime? _data = TryConvert.ToDateTime(mDateOfBirthBox.Text);
                return _data.HasValue && BusinessHelper.getAge(_data.Value) >= 18;
            }
        }


        private void DisableSelectedPersonInfoPanel()
        {
            mClientNumberBox.Enabled = false;
            mNifBox.Enabled = false;

            WebUtility.DisableTextBoxIfNotFilled(mIdCardNumberBox);
            WebUtility.DisableTextBoxIfNotFilled(mDateOfBirthBox);
            //WebUtility.DisableTextBoxIfNotFilled(mDriverLicenseBox);
            mGenderBox.Enabled = false;

            //mMaritalStatusBox
            if (string.IsNullOrEmpty(mMaritalStatusBox.SelectedValue))
            {
                mMaritalStatusBox.Enabled = true;
            }
            else
            {
                mMaritalStatusBox.Enabled = false;
            }

            //mProfessionBox
            if (string.IsNullOrEmpty(mProfessionShowBox.Text))
            {
                mSearchProfessionButton.Enabled = true;
            }
            else
            {
                mSearchProfessionButton.Enabled = false;
            }

            //mTitleBox
            if (string.IsNullOrEmpty(mTitleBox.SelectedValue))
            {
                mTitleBox.Enabled = true;
            }
            else
            {
                mTitleBox.Enabled = false;
            }

            WebUtility.DisableTextBoxIfNotFilled(mNameBox);

            mNationalityBox.Enabled = false;
            //if (string.IsNullOrEmpty(mNationalityBox.SelectedValue))
            //{
            //    mNationalityBox.Enabled = true;
            //}
            //else
            //{
            //    mNationalityBox.Enabled = false;
            //}
        }

        private void EnablePersonInfoPanel()
        {
            mSelectedPersonInfoPanel.Enabled = true; //Enable all controls;
            mMaritalStatusBox.Enabled = true;
            mGenderBox.Enabled = true;
            mSearchProfessionButton.Enabled = true;
            mTitleBox.Enabled = true;
            mNationalityBox.Enabled = true;
            mNameBox.Enabled = true;
        }


        private void FillPersonWithDefaultData(Pessoa person)
        {
            // set default country if needed
            if (String.IsNullOrEmpty(person.Contacto01.PAIS))
            {
                person.Contacto01.PAIS = Settings.DefaultCountryCode;
            }

            // set default contact country if needed
            if (String.IsNullOrEmpty(person.Contacto02.PAIS))
            {
                person.Contacto02.PAIS = Settings.DefaultCountryCode;
            }

            // set default nif if needed
            if (String.IsNullOrEmpty(person.PCONTRIB))
            {
                person.PCONTRIB = mNifSearchBox.Text;
            }

            // set default nationality if needed
            if (String.IsNullOrEmpty(person.NACIONALIDADE))
            {
                person.NACIONALIDADE = Settings.DefaultNationalityCode;
            }

            // set default sex if needed
            if (String.IsNullOrEmpty(person.PTIPO))
            {
                //person.PTIPO = DEFAULT_GENDER_CODE;
                person.PTIPO = "";
            }
        }


        private void ShowPerson(List<Pessoa> pessoas)
        {            
            if (pessoas.Count == 0)
            {
                ShowBlankPerson();
            }
            else if (pessoas.Count == 1)
            {
                ShowChosenPerson(pessoas[0]);
            }
            else
            {
                LoadPersonClientBox();
                ShowBlankPerson();

            }
            // Guardar dados Pessoais (mínimos) obrigatórios não preenchidos
            guardarDadosPessoaisObrigatorios();
        }

        private void ShowPersonByPlanID()
        {
            ClearAllFields();

            Pessoa _pp = (new PessoaDS()).Saber_PessoaPlano(pIdPlano);
            if (_pp != null)
            {
                ShowPersonDetails(_pp);
            }
            // Guardar dados Pessoais (mínimos) obrigatórios não preenchidos
            guardarDadosPessoaisObrigatorios();

        }

        private void ShowBlankPerson()
        {
            mGenderBox.SelectedIndex = -1;
            ShowChosenPerson(new Pessoa());
            mNifBox.Text = string.Empty;                        
            UpdateCanEditAddress();
            UpdateCanEditExtraAddress();
        }

        private void ShowPersonDetails(Pessoa pessoa)
        {
            ShowBlankPerson();

            if (pessoa != null)
            {
                ShowChosenPerson(pessoa);
                DisableSelectedPersonInfoPanel();
            }
            else
            {
                BindTitleBox(); //Default person gender is "Masculino"
                BindProfessionsBox();
                EnablePersonInfoPanel();
            }
        }

        private void ShowAddress(Pessoa pessoa)
        {
            mAddressTypeBox.Text = pessoa.Contacto01.MORADA_TIPO;
            mAddressTitleBox.Text = pessoa.Contacto01.TITULO_ARTERIA;
            mAddressStreetNameBox.Text = pessoa.Contacto01.MORADA_DESC;
            mAddressStreetNumberBox.Text = pessoa.Contacto01.MORADA_NUMERO;
            mAddressFloorNumberBox.Text = pessoa.Contacto01.MORADA_ANDAR;
            mZipCode4Box.Text = pessoa.Contacto01.CODIGO_POSTAL;
            mZipCode3Box.Text = pessoa.Contacto01.SUFIXO_POSTAL;
            mLocalityBox.Text = pessoa.Contacto01.LOCALIDADE;

            mCountryBox.DataBind();
            mCountryBox.SelectedValue = pessoa.Contacto01.PAIS;

            mPhoneNumberBox.Text = pessoa.Contacto01.TELEFONE;
            mMobilePhoneNumberBox.Text = pessoa.TELEMOVEL == "" ? TelemovelTomador : pessoa.TELEMOVEL;
            mEMailBox.Text = pessoa.Contacto01.E_MAIL == "" ? EmailTomador : pessoa.Contacto01.E_MAIL;
            mFaxBox.Text = pessoa.Contacto01.TELEX_FAX;

            mAttentionOfBox.Text = pessoa.Contacto02.AO_CUIDADO;
            mExtraAddressTypeBox.Text = pessoa.Contacto02.MORADA_TIPO;
            mExtraAddressTitleBox.Text = pessoa.Contacto02.TITULO_ARTERIA;
            mExtraAddressStreetNameBox.Text = pessoa.Contacto02.MORADA_DESC;
            mExtraAddressStreetNumberBox.Text = pessoa.Contacto02.MORADA_NUMERO;
            mExtraAddressFloorNumberBox.Text = pessoa.Contacto02.MORADA_ANDAR;
            mExtraZipCode4Box.Text = pessoa.Contacto02.CODIGO_POSTAL;
            mExtraZipCode3Box.Text = pessoa.Contacto02.SUFIXO_POSTAL;
            mExtraLocalityBox.Text = pessoa.Contacto02.LOCALIDADE;
            mExtraCountryBox.SelectedValue = pessoa.Contacto02.PAIS;
            mExtraPhoneNumberBox.Text = pessoa.Contacto02.TELEFONE;
            mExtraEMailBox.Text = pessoa.Contacto02.E_MAIL;
            mExtraFaxBox.Text = pessoa.Contacto02.TELEX_FAX;

            // show the extra info panel if needed
            if (!string.IsNullOrEmpty(pessoa.Contacto02.AO_CUIDADO) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.MORADA_TIPO) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.TITULO_ARTERIA) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.MORADA_DESC) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.MORADA_NUMERO) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.MORADA_ANDAR) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.CODIGO_POSTAL) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.SUFIXO_POSTAL) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.LOCALIDADE) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.TELEFONE) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.E_MAIL) ||
                !string.IsNullOrEmpty(pessoa.Contacto02.TELEX_FAX)
                )
            {
                HasExtraContact = true;
            }
            else
            {

                HasExtraContact = false;
            }

            UpdateCanEditExtraAddress();
            UpdateCanEditAddress();
        }

        private void ShowChosenPerson(Pessoa pessoa)
        {
            FillPersonWithDefaultData(pessoa);

            mClientNumberBox.Text = pessoa.PCODIGO;
            mNifBox.Text = pessoa.PCONTRIB;
            mNifBox.Enabled = false;
            mIdCardNumberBox.Text = pessoa.PBI;
            DateTime? _data = TryConvert.ToDateTime(pessoa.DT_NASCIMENTO);
            mDateOfBirthBox.Text = _data.HasValue ? _data.Value.ToShortDateString() : String.Empty;
            mGenderBox.SelectedValue = pessoa.PTIPO;
            mMaritalStatusBox.SelectedValue = pessoa.ESTADO_CIVIL;
            mMaritalStatusBox.Enabled = pessoa.ESTADO_CIVIL == "0";
                        
            BindProfessionsBox();
            mProfessionBox.SelectedValue = pessoa.PPROFISS;
            if (!string.IsNullOrEmpty(pessoa.PPROFISS))
            {
                ListItem li = mProfessionBox.Items.FindByValue(pessoa.PPROFISS);
                mProfessionShowBox.Text = li != null ? li.Text : string.Empty;
            }
            
            BindTitleBox();
            mTitleBox.SelectedValue = pessoa.PTITULO;
            mNameBox.Text = pessoa.PNOME;
            mNationalityBox.SelectedValue = pessoa.NACIONALIDADE;

            ShowAddress(pessoa);
        }



        private void UpdateCanEditExtraAddress()
        {
            CanEditExtraAddress = BusinessHelper.CanEditHolderAddressByCountry(mExtraCountryBox.SelectedValue);
        }

        private void UpdateCanEditAddress()
        {
            CanEditAddress = BusinessHelper.CanEditHolderAddressByCountry(mCountryBox.SelectedValue);
        }



        #endregion
    }
}