using System;
using System.Collections.Generic;
using System.Web;

namespace Lusitania.Utilities
{
	/// <summary>
	/// Summary description for ClEnumeration.
	/// </summary>
	public class ClEnumeration
	{

		#region ----- Construtor -----

		/// <summary>
		/// Construtor
		/// </summary>
		public ClEnumeration()
		{
		}
		
		#endregion

		#region ----- Tipo de Simulador -----

		/// <summary>
		/// Tipo de Simulador
		/// </summary>
		public enum TipoSimulador
		{
			NaoDefinido,
			ProteccaoCrianca,
			RendaFamiliar,
			TrabalhadorIndependente,
			Viagem
		}

		#endregion

		#region ----- Tipo de NIF -----

		/// <summary>
		/// Tipo de NIF
		/// </summary>
		public enum TipoNIF
		{
			Qualquer,
			Pessoal,
			Empresa
		}

		#endregion

	}
}
