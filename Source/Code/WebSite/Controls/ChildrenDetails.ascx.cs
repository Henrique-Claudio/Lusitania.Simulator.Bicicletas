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
using System.Text;

// New
using Lusitania.Model.Simuladores.Negocio;
using Lusitania.Utilities;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class ChildrenDetails : System.Web.UI.UserControl
    {

		#region ----- Propriedades -----

		#region ----- pNumeroPessoaSegura -----

		/// <summary>
		/// N.º da Pessoa Segura
		/// </summary>
		public Int32 pNumeroPessoaSegura
		{
			get
			{
				return uctDetalhesPessoaSegura.pNumeroPessoaSegura;
			}
			set
			{
				uctDetalhesPessoaSegura.pNumeroPessoaSegura = value;
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
				return uctDetalhesPessoaSegura.pTituloErro;
			}
		}

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
				return uctDetalhesPessoaSegura.pNIF;
			}
		}

		#endregion

		#endregion

		#endregion

		#region ----- Evento Page_Load -----

		/// <summary>
		/// Evento Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// Configurar o controlo
				configurar();
			}
		}

		#endregion

		#region ----- Inicialização e Configuração -----

		/// <summary>
		/// Configurar o controlo
		/// </summary>
		private void configurar()
		{
			// Pessoa Segura
			// Propriedades
			uctDetalhesPessoaSegura.pTipoSimulador = ClEnumeration.TipoSimulador.ProteccaoCrianca;
			uctDetalhesPessoaSegura.pDadosPessoaisObrigatorios = true;
			uctDetalhesPessoaSegura.pMostrarConfidencialidade = true;
			uctDetalhesPessoaSegura.pPermitirNIFEmpresa = false;
			uctDetalhesPessoaSegura.pPermitirContactoSecundario = false;
			// Inicializar
			uctDetalhesPessoaSegura.inicializar();
		}

		#endregion

		#region ----- Validar dados para Gravar -----

		/// <summary>
		/// Validar dados para Gravar
		/// </summary>
		/// <param name="prmErros">Erros</param>
		/// <returns>Indica se os dados são válidos</returns>
		public Boolean validar(ref StringBuilder prmErros)
		{
			DateTime _datDataNascimento;
			Boolean _blnValido = true;

			// Validar dados da Pessoa Segura
			_blnValido &= uctDetalhesPessoaSegura.validar(ref prmErros);

			// Obter Data de Nascimento
			_datDataNascimento = uctDetalhesPessoaSegura.pDataNascimento;

			// Validar Data de Nascimento
			if(_datDataNascimento != new DateTime())
			{
				// Verificar se a diferença para a Data actual é superior a 17 anos
				if(ClDatas.getDiferencaAnosDatas(_datDataNascimento, ClDatas.getDiaActual()) > 17)
				{
					// Erro
					ClErros.registarErro(ref prmErros,
											uctDetalhesPessoaSegura.pTituloErro,
											"A idade não pode ser superior a 17 anos.",
											Environment.NewLine);
					_blnValido = false;
				}
			}

			// Parentesco c/ Tomador
			if(ParentDrop.SelectedIndex < 0)
			{
				// Erro
				ClErros.registarErro(ref prmErros,
										uctDetalhesPessoaSegura.pTituloErro,
										"Seleccione o Parentesco c/ Tomador.",
										Environment.NewLine);
				_blnValido = false;
			}

			return _blnValido;
		}

		#endregion

		#region ----- Save -----

		/// <summary>
		/// Gravar os dados do controlo
		/// </summary>
		/// <param name="prmMlPessoaSegura">Model de Pessoa Segura (output)</param>
		/// <param name="prmParentescoComTomador">Parentesco com o Tomador (output)</param>
		public void save(out MlPessoaSegura prmMlPessoaSegura,
							out String prmParentescoComTomador)
		{
			// Dados de Pessoa Segura
			prmMlPessoaSegura = uctDetalhesPessoaSegura.save();

			// Parentesco com o Tomador
			prmParentescoComTomador = ParentDrop.SelectedValue;
		}

		#endregion

	}
    
}
