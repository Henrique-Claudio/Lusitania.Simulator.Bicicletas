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
	/// Classe			: MlPessoaSegura.cs
	/// Tabela			: N/A
	/// Autor			: Nuno Silva
	/// Data de geração : 28-02-2011
	/// </summary>
	[Serializable]
	public class MlPessoaSegura : MlBase
	{

		#region ----- Variáveis de instância -----

		private MlContacto _objMlContactoPrincipal = null;
		private MlContacto _objMlContactoSecundario = null;

		#endregion

		#region ----- Propriedades -----

		#region ----- pCodigoPessoa -----

		/// <summary>
		/// Código Pessoa
		/// </summary>
		public Int64 pCodigoPessoa
		{
			get;
			set;
		}

		#endregion

		#region ----- pNIF -----

		/// <summary>
		/// NIF
		/// </summary>
		public String pNIF
		{
			get;
			set;
		}

		#endregion

		#region ----- pBI -----

		/// <summary>
		/// BI
		/// </summary>
		public String pBI
		{
			get;
			set;
		}

		#endregion

		#region ----- pDataNascimento -----

		/// <summary>
		/// Data de Nascimento
		/// </summary>
		public DateTime pDataNascimento
		{
			get;
			set;
		}

		#endregion

		#region ----- pCartaConducao -----

		/// <summary>
		/// Carta de Condução
		/// </summary>
		public String pCartaConducao
		{
			get;
			set;
		}

		#endregion

		#region ----- pDataCartaConducao -----

		/// <summary>
		/// Data da Carta de Condução
		/// </summary>
		public DateTime pDataCartaConducao
		{
			get;
			set;
		}

		#endregion

		#region ----- pTipoPessoa -----

		/// <summary>
		/// Tipo de Pessoa
		/// </summary>
		public String pTipoPessoa
		{
			get;
			set;
		}

		#endregion

		#region ----- pEstadoCivil -----

		/// <summary>
		/// Estado Civil
		/// </summary>
		public String pEstadoCivil
		{
			get;
			set;
		}

		#endregion

		#region ----- pProfissao -----

		/// <summary>
		/// Profissão
		/// </summary>
		public String pProfissao
		{
			get;
			set;
		}

		#endregion

		#region ----- pTitulo -----

		/// <summary>
		/// Título
		/// </summary>
		public String pTitulo
		{
			get;
			set;
		}

		#endregion

		#region ----- pNome -----

		/// <summary>
		/// Nome
		/// </summary>
		public String pNome
		{
			get;
			set;
		}

		#endregion

		#region ----- pNacionalidade -----

		/// <summary>
		/// Nacionalidade
		/// </summary>
		public String pNacionalidade
		{
			get;
			set;
		}

		#endregion

		#region ----- pEmpresaId -----

		/// <summary>
		/// Empresa - Id
		/// </summary>
		public Int64 pEmpresaId
		{
			get;
			set;
		}

		#endregion

		#region ----- pEmpresaActividade -----

		/// <summary>
		/// Empresa - Actividade
		/// </summary>
		public String pEmpresaActividade
		{
			get;
			set;
		}

		#endregion

		#region ----- pConfidencialidade -----

		/// <summary>
		/// Confidencialidade
		/// </summary>
		public String pConfidencialidade
		{
			get;
			set;
		}

		#endregion

		#region ----- pContactoPrincipal -----

		/// <summary>
		/// Contacto Principal
		/// </summary>
		public MlContacto pContactoPrincipal
		{
			get
			{
				return (_objMlContactoPrincipal == null ? new MlContacto() : _objMlContactoPrincipal);
			}
			set
			{
				_objMlContactoPrincipal = value;
			}
		}

		#endregion

		#region ----- pContactoSecundario -----

		/// <summary>
		/// Contacto Secundário
		/// </summary>
		public MlContacto pContactoSecundario
		{
			get
			{
				return (_objMlContactoSecundario == null ? new MlContacto() : _objMlContactoSecundario);
			}
			set
			{
				_objMlContactoSecundario = value;
			}
		}

		#endregion

		#endregion

		#region ----- Propriedades Virtuais -----

		#region ----- pvPrimeiroNome -----

		/// <summary>
		/// Primeiro Nome
		/// </summary>
		public String pvPrimeiroNome
		{
			get
			{
				String _strPrimeiroNome = pNome;
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

		#region ----- pvSobrenome -----

		/// <summary>
		/// Sobrenome
		/// </summary>
		public String pvSobrenome
		{
			get
			{
				String _strSobrenome = pNome;
				Int32 _intIndiceEspaco;

				// Obter o índice do último espaço " "
				_intIndiceEspaco = _strSobrenome.LastIndexOf(" ");

				// Verificar se encontrou
				if(_intIndiceEspaco > 0)
				{
					// Obter o primeiro Nome
					_strSobrenome = _strSobrenome.substring(_intIndiceEspaco + 1);
				}

				return _strSobrenome;
			}
		}

		#endregion

		#region ----- pvDataNascimento -----

		/// <summary>
		/// Data de Nascimento
		/// </summary>
		public DateTime? pvDataNascimento
		{
			get
			{
				DateTime? _datDataNascimento = null;

				if(pDataNascimento != new DateTime())
				{
					_datDataNascimento = pDataNascimento;
				}

				return _datDataNascimento;
			}
		}

		#endregion

		#region ----- pvDataCartaConducao -----

		/// <summary>
		/// Data da Carta de Condução
		/// </summary>
		public DateTime? pvDataCartaConducao
		{
			get
			{
				DateTime? _datDataCartaConducao = null;

				if(pDataCartaConducao != new DateTime())
				{
					_datDataCartaConducao = pDataCartaConducao;
				}

				return _datDataCartaConducao;
			}
		}

		#endregion

		#region ----- pvTemContactoSecundario -----

		/// <summary>
		/// Tem Contacto Secundário?
		/// </summary>
		public Boolean pvTemContactoSecundario
		{
			get
			{
				return (_objMlContactoSecundario != null);
			}
		}

		#endregion

		#endregion

		#region ----- Construtores -----

		/// <summary>
		/// Construtor padrão
		/// </summary>
		public MlPessoaSegura()
		{
		}

		#endregion

		#region ----- Métodos Estáticos -----

		#region ----- dataTableToModel -----

		/// <summary>
		/// Converter uma DataTable para um Array de instâncias do modelo
		/// </summary>
		/// <param name="prmDataTable">DataTable com os dados</param>
		public static MlPessoaSegura[] dataTableToModel(DataTable prmDataTable)
		{
			return ClDataTables.convertDataTableToArray<MlPessoaSegura>(prmDataTable);
		}

		#endregion

		#region ----- dataRowToModel -----

		/// <summary>
		/// Converter uma DataRow para uma instância do modelo
		/// </summary>
		/// <param name="prmDataRow">DataRow com os dados</param>
		public static MlPessoaSegura dataRowToModel(DataRow prmDataRow)
		{
			return ClDataTables.convertDataRowToObject<MlPessoaSegura>(prmDataRow);
		}

		#endregion

		#endregion

	}
}
