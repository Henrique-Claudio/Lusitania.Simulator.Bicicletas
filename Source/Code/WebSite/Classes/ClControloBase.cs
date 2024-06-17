using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Lusitania.Utilities;

namespace Lusitania.Web.Simuladores.Base.Classes
{
	/// <summary>
	/// Summary description for ClControloBase.
	/// </summary>
	public class ClControloBase : UserControl
	{

		#region ----- Propriedades -----

		#region ----- IsUserControlPostBack -----

		/// <summary>
		/// Indica se é um PostBack dentro no User Control,
		/// ou se já inicializou o User Control
		/// </summary>
		protected Boolean IsUserControlPostBack
		{
			get
			{
				return ViewState["IsUserControlPostBack"] != null;
			}
		}

		#endregion

		#region ----- pControloInicializado -----

		/// <summary>
		/// Flag que controla se o controlo está inicializado
		/// </summary>
		public bool pControloInicializado
		{
			get
			{
				if(ViewState["pControloInicializado"] == null)
				{
					return false;
				}
				return (bool)ViewState["pControloInicializado"];
			}

			set
			{
				ViewState["pControloInicializado"] = value;
			}
		}

		#endregion

		#region ----- pReadOnly -----

		/// <summary>
		/// Indica se o controlo é ReadOnly (default False)
		/// </summary>
		public virtual Boolean pReadOnly
		{
			get
			{
				if(ViewState["pReadOnly"] == null)
				{
					return false;
				}
				return Convert.ToBoolean(ViewState["pReadOnly"]);
			}
			set
			{
				ViewState["pReadOnly"] = value;
			}
		}

		#endregion

		#endregion

		#region ----- Construtor -----

		/// <summary>
		/// Construtor por defeito
		/// </summary>
		public ClControloBase()
			: base()
		{
		}

		#endregion

		#region ----- Inicialização e Configuração -----

		/// <summary>
		/// Inicializar o Controlo
		/// </summary>
		public virtual void inicializar()
		{
			// Verificar se o controlo ainda não foi inicializado
			if(!pControloInicializado)
			{
				// Marcar o controlo como inicializado
				pControloInicializado = true;
			}
		}

		#endregion

		#region ----- Eventos -----

		/// <summary>
		/// Override do Evento OnLoad do User Control
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);

			if(!IsUserControlPostBack)
			{
				ViewState.Add("IsUserControlPostBack", true);
			}
		}

		#endregion

	}

}
