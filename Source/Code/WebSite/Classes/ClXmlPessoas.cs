using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Lusitania.LusnetBase;
using Lusitania.Model.Simuladores.Negocio;
using Lusitania.Utilities;

namespace Lusitania.Web.Simuladores.Base.Classes
{
	public class ClXmlPessoas
	{
        #region Constants
        public const string cNumOPSDefault = "##NUM_OPS##";
        public const string cNumHeirDefault = "##NUM_HEIR##";
        public const string cKinshpDefault = "##KINSHIP_KEY##";
        #endregion

		#region Enums

		public enum TypeContact
		{
			Unknown = 0,
			Phone = 1,
			Mobile = 2,
			Fax = 3,
			Email = 4
		}

		public enum TypeDocument
		{
			Unknown = 0,
			TaxCard = 1,
			IdentityCard = 2,
			DrivingLicenceCard = 3,
			PassportCard = 4,
			SocialSecurityCard = 5,
			MembershipCard = 6,
			CreditCard = 7,
			BankCard = 8,
			NIB = 9
		}

		#endregion

		public static string GetPerson(string personType, string internalCode)
		{
			return string.Format(@"<person type='{0}' code='{1}' />", personType, internalCode);
		}
        public static string GetOPSExtraNode(string numOPS, string tipoBenef, string clausGen)
        {
            return string.Format(@"
    <numOPS>{0}</numOPS>
    <tipoBeneficiarios>{1}</tipoBeneficiarios>
    <clausulaGenerica>{2}</clausulaGenerica>", numOPS, tipoBenef, clausGen);
        }
        public static string GetHerExtraNodes(string numOPS, string numHer)
        {
            return string.Format(@"
        <numOPS>{0}</numOPS>
        <numHer>{1}</numHer>", numOPS, numHer);
        }
        public static string UpdateHeirKinship(string xmlHeir, string kinship) {
            return xmlHeir.Replace(cKinshpDefault, kinship);
        }
        public static string UpdateHeirNum(string xmlHeir, string numHeir)
        {
            return xmlHeir.Replace(cNumHeirDefault, numHeir);
        }
        public static string UpdateOPSNum(string xmlHeir, string numOPS)
        {
            return xmlHeir.Replace(cNumOPSDefault, numOPS);
        }
		public static string GetPerson(string personType,
										string internalCode,
										String prmNome,
										string title,
										string birthDate,
										string gender,
										string maritalStatus,
										string occupation,
										String prmNacionalidade,
										string addressList_content,
										string contactList_content,
										string documentList_content,
										string questionList_content,
										String prmConteudoOpcional)
		{
			object[] parameters = {personType,
									internalCode,
                                    prmNome,
									title,
                                    birthDate,
									gender,
									maritalStatus,
                                    occupation,
									prmNacionalidade,
									addressList_content,
									contactList_content,
                                    documentList_content,
									questionList_content,
									prmConteudoOpcional};

			string person = string.Format(@"
	<person type='{0}' code='{1}'>
		<name>{2}</name>
		<title>{3}</title>
		<birthDate>{4}</birthDate>
		<gender>{5}</gender>
		<maritalStatus>{6}</maritalStatus>
		<occupation>{7}</occupation>
		<nacionality>{8}</nacionality>
		<addressList>{9}</addressList>
		<contactList>{10}</contactList>
		<documentList>{11}</documentList>
		<questionList>{12}</questionList>
		{13}
	</person>", parameters);

			return person;
		}

		public static string GetPerson(string personType,
								string internalCode,
								String prmNome,
								string title,
								string birthDate,
								string gender,
								string maritalStatus,
								string occupation,
								String prmNacionalidade,
								string addressList_content,
								string contactList_content,
								string documentList_content,
								string questionList_content)
		{
			return GetPerson(personType,
								internalCode,
								prmNome,
								title,
								birthDate,
								gender,
								maritalStatus,
								occupation,
								prmNacionalidade,
								addressList_content,
								contactList_content,
								documentList_content,
								questionList_content,
								String.Empty);
		}

		public static string GetAddress(string addressType,
										string streetType,
										string streetName,
										string streetNumber,
										string floor,
										string door,
										string postalCode,
										string local,
										string country,
										string isPrincipal,
										string isMailingAddress)
		{
			// Verificar se a Rua e o Código Postal não estão preenchidos
			if((string.IsNullOrEmpty(streetName)) &&
				(string.IsNullOrEmpty(postalCode)))
			{
				return string.Empty;
			}

			object[] parameters = {addressType,
									streetType,
									streetName,
                                    streetNumber,
									floor,
									door,
                                    postalCode,
									local,
									country,
                                    isPrincipal,
									isMailingAddress};

			string address = string.Format(@"
		<address type='{0}'>
			<streetType>{1}</streetType>
			<streetName>{2}</streetName>
			<streetNumber>{3}</streetNumber>
			<floor>{4}</floor>
			<door>{5}</door>
			<postalCode>{6}</postalCode>
			<local>{7}</local>
			<country>{8}</country>
			<isPrincipal>{9}</isPrincipal>
			<isMailingAddress>{10}</isMailingAddress>
		</address>", parameters);

			return address;
		}

		public static string GetContact(TypeContact contactType,
										string data,
										string isPrincipal,
										string isMailingAddress)
		{
			if(string.IsNullOrEmpty(data))
				return string.Empty;

			string contact = string.Format(@"
		<contact type='{0}'>
			<data>{1}</data>
			<isPrincipal>{2}</isPrincipal>
			<isMailingAddress>{3}</isMailingAddress>
		</contact>", ((int)contactType).ToString(), data, isPrincipal, isMailingAddress);

			return contact;

		}

		public static string GetDocument(TypeDocument documentType,
											string number,
											string issueDate,
											string issuePlace,
											string expirationDate,
											string extraInfo)
		{
			if(string.IsNullOrEmpty(number))
				return string.Empty;

			object[] parameters = { ((int)documentType).ToString(), number, issueDate,
                                    issuePlace, expirationDate, extraInfo };

			string document = string.Format(@"
		<document type='{0}'>
			<number>{1}</number>
			<issueDate>{2}</issueDate>
			<issuePlace>{3}</issuePlace>
			<expirationDate>{4}</expirationDate>
			<extraInfo>{5}</extraInfo>
		</document>", parameters);

			return document;
		}

		public static string GetQuestion(string query, string response)
		{
			if(string.IsNullOrEmpty(query) || string.IsNullOrEmpty(response))
				return string.Empty;

			string question = string.Format(@"
		<question>
			<query>{0}</query>
			<response>{1}</response>
		</question>", query, response);

			return question;
		}
        
        public static string GetQuestionLong(string query, string response, string responselong)
		{
			if(string.IsNullOrEmpty(query) || (string.IsNullOrEmpty(response)&&string.IsNullOrEmpty(responselong)))
				return string.Empty;

			string question = string.Format(@"
		<question>
			<query>{0}</query>
			<response>{1}</response>
            <responselong>{2}</responselong>
		</question>", query, response, responselong);

			return question;
		}


		#region ----- Obter Xml com Dados de Pessoa Segura -----

		/// <summary>
		/// Obter Xml com Dados de Pessoa Segura
		/// </summary>
		/// <param name="prmMlPessoaSegura">Model de Pessoa Segura</param>
		/// <param name="prmNIB">NIB</param>
		/// <param name="prmMorada">Morada (output)</param>
		/// <param name="prmContactos">Contactos (output)</param>
		/// <param name="prmDocumentos">Documentos (output)</param>
		/// <param name="prmContactoPrincipal">Contacto principal?</param>
		public static void obterXmlDadosPessoaSegura(MlPessoaSegura prmMlPessoaSegura,
														String prmIBAN,
                                                        String prmBIC,
														out String prmMorada,
														out String prmContactos,
														out String prmDocumentos,
														Boolean prmContactoPrincipal)
		{
            bool temMorada2 = prmMlPessoaSegura.pvTemContactoSecundario && !string.IsNullOrEmpty(prmMlPessoaSegura.pContactoSecundario.pCodPostal4);
			bool temTelefone2 = prmMlPessoaSegura.pvTemContactoSecundario && !string.IsNullOrEmpty(prmMlPessoaSegura.pContactoSecundario.pTelefone);
            bool temFax2 = prmMlPessoaSegura.pvTemContactoSecundario && !string.IsNullOrEmpty(prmMlPessoaSegura.pContactoSecundario.pFax);
            // Morada
            
            prmMorada = ClXmlPessoas.GetAddress(String.Empty,
                                                prmMlPessoaSegura.pContactoPrincipal.pMoradaTipo,
                                                prmMlPessoaSegura.pContactoPrincipal.pMorada,
                                                prmMlPessoaSegura.pContactoPrincipal.pMoradaNumero,
                                                prmMlPessoaSegura.pContactoPrincipal.pMoradaAndar,
                                                String.Empty,
                                                String.Format("{0}-{1}", prmMlPessoaSegura.pContactoPrincipal.pCodPostal4,
                                                                            prmMlPessoaSegura.pContactoPrincipal.pCodPostal3),
                                                prmMlPessoaSegura.pContactoPrincipal.pLocalidade,
                                                prmMlPessoaSegura.pContactoPrincipal.pPais,
                                                (!temMorada2 ? ClEnumerationBase.obterSim() : ClEnumerationBase.obterNao()),
                                                String.Empty);
            if (temMorada2)
            {
                prmMorada = String.Concat(prmMorada, ClXmlPessoas.GetAddress(String.Empty,
                                                    prmMlPessoaSegura.pContactoSecundario.pMoradaTipo,
                                                    prmMlPessoaSegura.pContactoSecundario.pMorada,
                                                    prmMlPessoaSegura.pContactoSecundario.pMoradaNumero,
                                                    prmMlPessoaSegura.pContactoSecundario.pMoradaAndar,
                                                    String.Empty,
                                                    String.Format("{0}-{1}", prmMlPessoaSegura.pContactoSecundario.pCodPostal4,
                                                                                prmMlPessoaSegura.pContactoSecundario.pCodPostal3),
                                                    prmMlPessoaSegura.pContactoSecundario.pLocalidade,
                                                    prmMlPessoaSegura.pContactoSecundario.pPais,
                                                    ClEnumerationBase.obterSim(),
                                                    String.Empty));
            }

			// Contactos
			prmContactos = String.Concat(ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Phone,
																	prmMlPessoaSegura.pContactoPrincipal.pTelefone,
                                                                    (!temTelefone2 ? ClEnumerationBase.obterSim() : ClEnumerationBase.obterNao()),
																	String.Empty),
                                            ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Mobile,
                                                                    prmMlPessoaSegura.pContactoPrincipal.pTelemovel,
                                                                    String.Empty,
                                                                    String.Empty),
											ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Email,
																	prmMlPessoaSegura.pContactoPrincipal.pEmail,
																	String.Empty,
																	String.Empty),
											ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Fax,
																	prmMlPessoaSegura.pContactoPrincipal.pFax,
																	String.Empty,
																	String.Empty));
            
            if (temTelefone2) { 
                prmContactos = String.Concat(prmContactos, ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Phone,
																	prmMlPessoaSegura.pContactoSecundario.pTelefone,
                                                                    ClEnumerationBase.obterSim(),
																	String.Empty));
            }
            if (temFax2)
            {
                prmContactos = String.Concat(prmContactos, ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Fax,
                                                                    prmMlPessoaSegura.pContactoSecundario.pFax,
                                                                    String.Empty,
                                                                    String.Empty));
            }
			// Documentos
			prmDocumentos = String.Concat(ClXmlPessoas.GetDocument(ClXmlPessoas.TypeDocument.TaxCard,
																	prmMlPessoaSegura.pNIF,
																	String.Empty,
																	String.Empty,
																	String.Empty,
																	String.Empty),
											ClXmlPessoas.GetDocument(ClXmlPessoas.TypeDocument.IdentityCard,
																		prmMlPessoaSegura.pBI,
																		String.Empty,
																		String.Empty,
																		String.Empty,
																		String.Empty),
											ClXmlPessoas.GetDocument(ClXmlPessoas.TypeDocument.DrivingLicenceCard,
																		prmMlPessoaSegura.pCartaConducao,
																		(prmMlPessoaSegura.pvDataCartaConducao == null ? String.Empty : prmMlPessoaSegura.pDataCartaConducao.getData(ClEnumerationBase.FormatoData.DiaMesAno)),
																		String.Empty,
																		String.Empty,
																		String.Empty),
											ClXmlPessoas.GetDocument(ClXmlPessoas.TypeDocument.NIB,
                                                                        prmIBAN,
																		String.Empty,
																		String.Empty,
																		String.Empty,
																		prmBIC));
		}

		#endregion

		#region ----- Obter Xml do Contacto Secundário -----

		/// <summary>
		/// Obter Xml do Contacto Secundário
		/// </summary>
		/// <param name="prmMlPessoaSegura">Model de Pessoa Segura</param>
		/// <returns>Xml do Contacto Secundário</returns>
		public static String obterXmlContactoSecundario(MlPessoaSegura prmMlPessoaSegura)
		{
			String _strXmlContactoSecundario;
			String _strMorada;
			String _strContactos;

			// Verificar se tem Contacto Secundário
			if(prmMlPessoaSegura.pvTemContactoSecundario)
			{
				// Morada
				_strMorada = ClXmlPessoas.GetAddress(prmMlPessoaSegura.pContactoSecundario.pMoradaTipo,
														String.Empty,
														prmMlPessoaSegura.pContactoSecundario.pMorada,
														prmMlPessoaSegura.pContactoSecundario.pMoradaNumero,
														prmMlPessoaSegura.pContactoSecundario.pMoradaAndar,
														prmMlPessoaSegura.pContactoSecundario.pLocalidade,
														String.Format("{0}-{1}", prmMlPessoaSegura.pContactoSecundario.pCodPostal4,
																					prmMlPessoaSegura.pContactoSecundario.pCodPostal3),
														prmMlPessoaSegura.pContactoSecundario.pLocalidade,
														prmMlPessoaSegura.pContactoSecundario.pPais,
														ClEnumerationBase.obterSim(),
														ClEnumerationBase.obterSim());

				// Contactos
				_strContactos = String.Concat(ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Phone,
																		prmMlPessoaSegura.pContactoSecundario.pTelefone,
																		ClEnumerationBase.obterSim(),
																		ClEnumerationBase.obterNao()),
												ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Email,
																		prmMlPessoaSegura.pContactoSecundario.pEmail,
																		String.Empty,
																		String.Empty),
												ClXmlPessoas.GetContact(ClXmlPessoas.TypeContact.Fax,
																		prmMlPessoaSegura.pContactoSecundario.pFax,
																		String.Empty,
																		String.Empty));

				// Pessoa
				_strXmlContactoSecundario = ClXmlPessoas.GetPerson("tomador_complementar",
																	String.Empty,
																	prmMlPessoaSegura.pContactoSecundario.pAoCuidadoDe,
																	String.Empty,
																	String.Empty,
																	String.Empty,
																	String.Empty,
																	String.Empty,
																	String.Empty,
																	_strMorada,
																	_strContactos,
																	String.Empty,
																	String.Empty);
			}
			else
			{
				// Vazio
				_strXmlContactoSecundario = String.Empty;
			}

			return _strXmlContactoSecundario;
		}

		#endregion

	}
}
