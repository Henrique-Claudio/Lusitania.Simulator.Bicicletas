using System;
using System.Web.UI;

namespace Lusitania.Utilities.Interfaces
{
	/// <summary>
	/// Interface a implementar pela página Base
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
