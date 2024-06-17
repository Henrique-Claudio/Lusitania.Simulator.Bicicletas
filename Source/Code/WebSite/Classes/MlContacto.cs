//------------------------------------------------------------------------------------------------------------------------
// MODEL
//------------------------------------------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Web.UI.WebControls;

using Lusitania.LusnetBase;
using Lusitania.Model.Framework.Interfaces;
using Lusitania.Utilities;

namespace Lusitania.Model.Simuladores.Negocio
{
	/// <summary>
	/// Classe			: MlContacto.cs
	/// Tabela			: N/A
	/// Autor			: Nuno Silva
	/// Data de geração : 28-02-2011
	/// </summary>
	[Serializable]
	public class MlContacto : MlBase
	{

		#region ----- Variáveis de instância -----

		private String _strMoradaTipo = String.Empty;

		#endregion

		#region ----- Propriedades -----

		#region ----- pAoCuidadoDe -----

		/// <summary>
		/// Ao Cuidado De
		/// </summary>
		public String pAoCuidadoDe
		{
			get;
			set;
		}

		#endregion

		#region ----- pMoradaTipo -----

		/// <summary>
		/// Morada - Tipo
		/// </summary>
		public String pMoradaTipo
		{
			get
			{
				return (_strMoradaTipo == String.Empty ? " " : _strMoradaTipo);
			}
			set
			{
				_strMoradaTipo = value;
			}
		}

		#endregion

		#region ----- pMoradaTitulo -----

		/// <summary>
		/// Morada - Título
		/// </summary>
		public String pMoradaTitulo
		{
			get;
			set;
		}

		#endregion

		#region ----- pMorada -----

		/// <summary>
		/// Morada
		/// </summary>
		public String pMorada
		{
			get;
			set;
		}

		#endregion

		#region ----- pMoradaNumero -----

		/// <summary>
		/// Morada - Número
		/// </summary>
		public String pMoradaNumero
		{
			get;
			set;
		}

		#endregion

		#region ----- pMoradaAndar -----

		/// <summary>
		/// Morada - Andar
		/// </summary>
		public String pMoradaAndar
		{
			get;
			set;
		}

		#endregion

		#region ----- pCodPostal4 -----

		/// <summary>
		/// Código Postal 4
		/// </summary>
		public String pCodPostal4
		{
			get;
			set;
		}

		#endregion

		#region ----- pCodPostal3 -----

		/// <summary>
		/// Código Postal 3
		/// </summary>
		public String pCodPostal3
		{
			get;
			set;
		}

		#endregion

		#region ----- pLocalidade -----

		/// <summary>
		/// Localidade
		/// </summary>
		public String pLocalidade
		{
			get;
			set;
		}

		#endregion

		#region ----- pPais -----

		/// <summary>
		/// País
		/// </summary>
		public String pPais
		{
			get;
			set;
		}

		#endregion

		#region ----- pTelefone -----

		/// <summary>
		/// Telefone
		/// </summary>
		public String pTelefone
		{
			get;
			set;
		}

		#endregion

        #region ----- pTelemovel -----

        /// <summary>
        /// Telemovel
        /// </summary>
        public String pTelemovel
        {
            get;
            set;
        }

        #endregion

		#region ----- pEmail -----

		/// <summary>
		/// Email
		/// </summary>
		public String pEmail
		{
			get;
			set;
		}

		#endregion

		#region ----- pFax -----

		/// <summary>
		/// Fax
		/// </summary>
		public String pFax
		{
			get;
			set;
		}

		#endregion

		#endregion

		#region ----- Propriedades Virtuais -----

		#region ----- pvAoCuidadoDePrimeiroNome -----

		/// <summary>
		/// Ao Cuidado De - Primeiro Nome
		/// </summary>
		public String pvAoCuidadoDePrimeiroNome
		{
			get
			{
				String _strPrimeiroNome = pAoCuidadoDe;
				Int32 _intIndiceEspaco;

				// Obter o índice do 1.º espaço " "
				_intIndiceEspaco = _strPrimeiroNome.IndexOf(" ");

				// Verificar se encontrou
				if(_intIndiceEspaco > 0)
				{
					// Obter o primeiro Nome
					_strPrimeiroNome = _strPrimeiroNome.substring(0, _intIndiceEspaco);
				}

				return _strPrimeiroNome;
			}
		}

		#endregion

		#region ----- pvAoCuidadoDeSobrenome -----

		/// <summary>
		/// Ao Cuidado De - Sobrenome
		/// </summary>
		public String pvAoCuidadoDeSobrenome
		{
			get
			{
				String _strSobrenome = pAoCuidadoDe;
				Int32 _intIndiceEspaco;

				// Obter o índice do último espaço " "
				_intIndiceEspaco = _strSobrenome.LastIndexOf(" ");

				// Verificar se encontrou
				if(_intIndiceEspaco > 0)
				{
					// Obter o primeiro Nome
					_strSobrenome = _strSobrenome.substring(_intIndiceEspaco);
				}

				return _strSobrenome;
			}
		}

		#endregion

		#endregion

		#region ----- Construtores -----

		/// <summary>
		/// Construtor padrão
		/// </summary>
		public MlContacto()
		{
		}

		#endregion

		#region ----- Métodos Estáticos -----

		#region ----- dataTableToModel -----

		/// <summary>
		/// Converter uma DataTable para um Array de instâncias do modelo
		/// </summary>
		/// <param name="prmDataTable">DataTable com os dados</param>
		public static MlContacto[] dataTableToModel(DataTable prmDataTable)
		{
			return ClDataTables.convertDataTableToArray<MlContacto>(prmDataTable);
		}

		#endregion

		#region ----- dataRowToModel -----

		/// <summary>
		/// Converter uma DataRow para uma instância do modelo
		/// </summary>
		/// <param name="prmDataRow">DataRow com os dados</param>
		public static MlContacto dataRowToModel(DataRow prmDataRow)
		{
			return ClDataTables.convertDataRowToObject<MlContacto>(prmDataRow);
		}

		#endregion

		#endregion

	}
}
