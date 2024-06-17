using System;

namespace Lusitania.Utilities.Interfaces
{
	/// <summary>
	/// Interface que indica que é um Controlo Base
	/// </summary>
	public interface IControloBase
	{

		#region ----- Propriedades -----

		#region ----- pReadOnly -----

		/// <summary>
		/// Indica se o controlo é ReadOnly (default False)
		/// </summary>
		Boolean pReadOnly
		{
			get;
			set;
		}

		#endregion

		#endregion

	}
}
