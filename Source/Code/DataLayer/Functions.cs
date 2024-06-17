using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Client;
using Lusitania.Simuladores.DataLayer.Properties;
using System.Data;
using Oracle.DataAccess.Types;

namespace Lusitania.Simuladores.DataLayer
{
    public static class Functions
    {
        public static bool IsNifValid(string iNif)
        {
            //ALTERAR A LINHA ABAIXO QUANDO A V2 DO SERVIÇO IsValidNif chegar a PROD!
            //return OSBConnector.BDLeg.Clientes.BusinessActivity.Nif.Validar_V2_0(iNif, String.Empty);

            //object[] parameters = new object[] { iNif };
            //bool _response = (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("F_CKDIGITO_NIF", (Func<string, bool>)Functions.PerformanceMonitor_IsNifValid, parameters);
            //return _response;

            return PerformanceMonitor_IsNifValid(iNif);
        }

        private static bool PerformanceMonitor_IsNifValid(string iNif)
        {
            using (OracleConnection xConnection = new OracleConnection(Settings.Default.ConnectionString))
            {
                xConnection.Open();
                using (OracleCommand xCommand = xConnection.CreateCommand())
                {
                    xCommand.CommandType = CommandType.Text;
                    xCommand.CommandText = "SELECT F_CKDIGITO_NIF(:AIN_NIF) FROM DUAL";
                    xCommand.Parameters.Add("AIN_NIF", OracleDbType.Varchar2, iNif, ParameterDirection.Input);

                    return Convert.ToInt32(xCommand.ExecuteScalar()) == 0;
                }
            }
        }

        public static bool IsProfileDefined(string login, string profile)
        {
            var userAttributes = OSBConnector.Shared.DataServices.GetUserAttributes(login, true);
            return userAttributes != null ? userAttributes.IsProfileDefined(profile) : false;
        }

        /*
        public static bool IsProfileDefined_Old(string login, string profile)
        {
            object[] parameters = new object[] { login, profile};
            bool _response = (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("VALIDA_WEB_PKG.TEM_PERFIL_DEFINIDO", (Func<string, string, bool>)Functions.PerformanceMonitor_IsProfileDefined_Old, parameters);
            return _response;
        }
        
        private static bool PerformanceMonitor_IsProfileDefined_Old(string login, string profile)
        {
            decimal? result;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).TEM_PERFIL_DEFINIDO(login, profile, out result);
            return result.GetValueOrDefault(0) == 1;
        }
        */

        public static int Validar_IBAN_BIC(string pIBAN, string pBIC)
        {
            //return (int)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("VALIDA_WEB_PKG.VALIDAR_IBAN_BIC", (Func<string, string, int>)PerformanceMonitor_Validar_IBAN_BIC, pIBAN, pBIC);
            return PerformanceMonitor_Validar_IBAN_BIC(pIBAN, pBIC);
        }

        private static int PerformanceMonitor_Validar_IBAN_BIC(string pIBAN, string pBIC)
        {
            using (OracleConnection xConnection = new OracleConnection(Settings.Default.ConnectionString))
            {
                xConnection.Open();
                using (OracleCommand xCommand = xConnection.CreateCommand())
                {
                    int valido = 0;
                    xCommand.CommandType = CommandType.StoredProcedure;
                    xCommand.CommandText = "VALIDA_WEB_PKG.VALIDAR_IBAN_BIC";
                    xCommand.Parameters.Add("p_IBAN", OracleDbType.Varchar2, pIBAN, ParameterDirection.Input);
                    xCommand.Parameters.Add("p_BIC", OracleDbType.Varchar2, pBIC, ParameterDirection.Input);
                    xCommand.Parameters.Add("p_Valido", OracleDbType.Int32, ParameterDirection.Output);

                    xCommand.ExecuteNonQuery();

                    xConnection.Close();

                    return Convert.ToInt32(xCommand.Parameters["p_Valido"].Value.ToString());
                }
            }
        }

        public static string ValidaCertificado(string ramo, string apolice, string recibo)
        {
            string retXML = string.Empty;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "FATURA_RECIBO_PKG.valida_certificado";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("p_ramo", OracleDbType.Varchar2, 1000).Value = ramo;
            cmd.Parameters.Add("p_apolice", OracleDbType.Varchar2, 1000).Value = apolice;
            cmd.Parameters.Add("p_recibo", OracleDbType.Varchar2, 1000).Value = recibo;

            //Parametro OUT
            cmd.Parameters.Add("p_tem_cert", OracleDbType.Varchar2,100).Direction = ParameterDirection.Output;

            try
            {
                DataHelper.ExecuteQueryOpenConnection(cmd);
                retXML = ((Oracle.DataAccess.Types.OracleString)(cmd.Parameters["p_tem_cert"].Value)).Value;
                DataHelper.CloseStaticConnection();
            }
            catch (Exception ex)
            {
                DataHelper.CloseStaticConnection();
            }

            return retXML;
        }

        public static string VerificaConstante(string contexto, string chave)
        {
            string retXML = string.Empty;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "LN_APPCONSTANTES_PKG.uspObterValorConstante";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("p_IDCOMPANHIA", OracleDbType.Int32, 1000).Value = 0;
            cmd.Parameters.Add("p_IDCONTEXTO", OracleDbType.Varchar2, 1000).Value = contexto;
            cmd.Parameters.Add("p_CHAVE", OracleDbType.Varchar2, 1000).Value = chave;

            //Parametro OUT
            cmd.Parameters.Add("p_VALOR", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output;

            try
            {
                DataHelper.ExecuteQueryOpenConnection(cmd);
                retXML = ((Oracle.DataAccess.Types.OracleString)(cmd.Parameters["p_VALOR"].Value)).Value;
                DataHelper.CloseStaticConnection();
            }
            catch (Exception ex)
            {
                DataHelper.CloseStaticConnection();
            }

            return retXML;
        }
    }
}
