using System;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;


namespace Lusitania.Simuladores.Common.Auto
{
    /// <summary>
    /// Summary description for lusOracleDB
    /// </summary>
    public static class LusOracleDB
    {
        
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["DSN_LUS_SIM"];
            }
        }


       // private static DataTable PreencheDataBoundControl(string textoComando)
       // {}

        public static void PreencherListaSimples(ListControl oLista, string textoComando, string valorSeleccionado, bool valorInicialVazio)
        {   
            OracleConnection cn = new OracleConnection(ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = textoComando;
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros OUT
            cmd.Parameters.Add("a_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    oLista.DataSource = dr;
                    oLista.DataValueField = dr.GetName(0).ToString();
                    oLista.DataTextField = dr.GetName(1).ToString();
                    oLista.DataBind();
                    
                    //Para seleccionar um valor na Lista
                    if (valorSeleccionado!=null && valorSeleccionado.Length > 0)
                    {
                        oLista.SelectedValue = valorSeleccionado;
                    }
                    // Para adicionar um valor inicial vazio
                    if (valorInicialVazio)
                    {
                        oLista.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }
                }

                dr.Close();
                cn.Close();
              
            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

        }

		public static void PreencherLista(ListControl oLista, OracleCommand cmd, string valorSeleccionado, bool valorInicialVazio)
		{
			OracleConnection cn = new OracleConnection(ConnectionString);

			cmd.Connection = cn;

            try
            {
                cn.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dr.HasRows)
                {
                    oLista.Items.Clear();
                    oLista.DataSource = dr;
                    oLista.DataValueField = dr.GetName(0).ToString();
                    oLista.DataTextField = dr.GetName(1).ToString();
                    oLista.DataBind();

                    //Para seleccionar um valor na Lista
                    if (valorSeleccionado != null && valorSeleccionado.Length > 0)
                    {
                        oLista.SelectedValue = valorSeleccionado;
                    }
                    // Para adicionar um valor inicial vazio
                    if (valorInicialVazio)
                    {
                        oLista.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }
                }

                dr.Close();
                

            }
            catch
            {
                throw;
            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

		}


		public static bool MostraDescontoComercial(int idSimulador)
		{
			bool bRetorno = false;

			OracleConnection cn = new OracleConnection(ConnectionString);

			OracleCommand cmd = new OracleCommand();
			cmd.Connection = cn;
			cmd.CommandText = "valida_web_pkg.VALIDA_CAMPO_DESC_COMERCIAL";
			cmd.CommandType = CommandType.StoredProcedure;

			//Parametros IN

            cmd.Parameters.Add("a_login", OracleDbType.Varchar2, 8).Value = Lusitania.Simuladores.Common.Auto.Redes.SaberLogin(Lusitania.Simuladores.Common.Auto.TipoSaberLogin.UserName);
			cmd.Parameters.Add("a_id_simulador", OracleDbType.Int32).Value = idSimulador;

			//Parametros OUT			
			cmd.Parameters.Add("a_campo_visivel", OracleDbType.Int32).Direction = ParameterDirection.Output;

			try
			{
				cn.Open();
				cmd.ExecuteNonQuery();
				if (cmd.Parameters["a_campo_visivel"].Value.ToString() == "1")
				{
					bRetorno = true;
				}
			}
			catch
			{
				bRetorno = false;
			}
			finally
			{
				cn.Close();
				cn.Dispose();
			}

			return bRetorno;			
		}

		
    }
}