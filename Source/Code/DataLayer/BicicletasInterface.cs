using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Lusitania.Simuladores.DataLayer.Properties;

namespace Lusitania.Simuladores.DataLayer
{
    public static class BicicletasInterface
    {
        public static int GetContractPermissionLevel(string user)
        {
            int levelOfPermission = 0;

            /* levels are:
             * 0 - Undefined, works as before (can do anything)
             * 1 - Can't save contract
             * 2 - Cant' charge for contract
             * 3 - Can charge for contract
             */

            #region constants
            string propertyid = "lusitania.util.nivel_gestao_simuladores";
            string applicationId = "001";
            string profileId = "001";
            #endregion

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "lus_gdc.dm.getpropertylov";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.BindByName = true;
            //Parametro return
            OracleParameter resultValue =
            cmd.Parameters.Add("result", OracleDbType.Varchar2, ParameterDirection.ReturnValue);

            resultValue.Size = 1000;

            //Parametros IN
            cmd.Parameters.Add("applicationID", OracleDbType.Varchar2, ParameterDirection.Input).Value = applicationId;
            cmd.Parameters.Add("propertyID", OracleDbType.Varchar2, ParameterDirection.Input).Value = propertyid;
            cmd.Parameters.Add("login", OracleDbType.Varchar2, ParameterDirection.Input).Value = user;
            cmd.Parameters.Add("profileID", OracleDbType.Varchar2, ParameterDirection.Input).Value = profileId;

            try
            {
                //cmd.ExecuteNonQuery();

                using (OracleConnection connection = new OracleConnection(Settings.Default.ConnectionString))
                {
                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(cmd.Parameters["result"].Value.ToString()))
                {
                    if (!int.TryParse(cmd.Parameters["result"].Value.ToString(), out levelOfPermission))
                    {
                        levelOfPermission = 0;
                    }
                }
            }
            catch
            { }
            finally
            {
                cmd.Connection.Close();
            }


            return levelOfPermission;
        }

        /// <summary>
        /// GetEmailApolice
        /// </summary>
        /// <returns>Devolve o email definido na BD para a pessoa</returns>
        public static string GetEmailApolice(string idPessoa)
        {
            string emailToReturn = "";
            int inputIdPessoa = 0;

            if (idPessoa != string.Empty)
            {
                try
                {
                    inputIdPessoa = int.Parse(idPessoa);
                }
                catch
                {
                    inputIdPessoa = -1;
                }
            }

            if (inputIdPessoa > 0)
            {
                OracleCommand cmd = new OracleCommand();
                OracleConnection connection = new OracleConnection(Settings.Default.ConnectionString);
                cmd.Connection = connection;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "WEB_PPROD_SAUDE_PKG.GetEmailApolice";
                    cmd.Parameters.Add("p_idPessoa", inputIdPessoa);
                    cmd.Parameters.Add("p_email", emailToReturn);
                    cmd.Parameters["p_email"].OracleDbType = OracleDbType.Varchar2;
                    cmd.Parameters["p_email"].Size = 100;
                    cmd.Parameters["p_email"].Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["p_email"].Value.ToString() != "")
                        emailToReturn = cmd.Parameters["p_email"].Value.ToString();
                }
                catch
                {
                    emailToReturn = "";
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return emailToReturn;
        }
    }
}
