using System;
using System.Web.UI;

namespace Lusitania.Utilities.Interfaces
{
	/// <summary>
	/// Interface a implementar pela p�gina Base
	/// </summary>
	public interface IPaginaBase
	{

		#region ----- Propriedades -----

		#region ----- pControlToFocus -----

		/// <summary>
		/// Control to Focus
		/// </summary>
		Control pControlToFocus
		{
			get;
			set;
		}

		#endregion

		#endregion

	}
}
