using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Lusitania.Simuladores.DataLayer;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Lusitania.Simuladores.DataLayer.Base;
using Lusitania.Simuladores.DataLayer.Properties;

namespace Lusitania.Simuladores.DataLayer
{
    //public delegate void DecimalAndString2RefHandler(decimal? arg1, ref string arg2, ref string arg3);
    //public delegate string StringAndStringOutHandler(string arg1, out string arg2);
    //public delegate string String2AndStringOutHandler(string arg1, string arg2, out string arg3);

    public static class DataHelper
    {
        private static OracleConnection staticConnection = new OracleConnection(Settings.Default.ConnectionString);

        public static void FixOracleParameterBug(IEnumerable<IDbCommand> iCommands)
        {
            foreach (IDbCommand xCommand in iCommands)
            {
                foreach (IDbDataParameter xParameter in xCommand.Parameters)
                {
                    DbType xDbType = xParameter.DbType;
                    xParameter.DbType = DbType.Object;
                    xParameter.DbType = xDbType;
                }
            }
        }

        private static bool validUser(string userName, string source)
        {

            List<OracleParameter> paramIn = new List<OracleParameter>{
                new OracleParameter("a_login", OracleDbType.Varchar2, ParameterDirection.Input)
                {
                    Size= 100,
                    Value = userName
                },
                new OracleParameter("a_log", OracleDbType.Int32, ParameterDirection.Output)

            };

            try
            {
                List<OracleParameter> paramOut = (new DatabaseAccess()).ExecuteQuery(paramIn, source);
                return paramOut[0].Value.ToString() == "1";
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsInternUser(string userName)
        {
            var userAttributes = OSBConnector.Shared.DataServices.GetUserAttributes(userName);
            //return userAttributes != null ? userAttributes.IsInternUser() : false;

            //object[] parameters = new object[] { userName, "WEB_GERAL_PKG.SABER_SE_UTILIZADOR_INTERNO" };
            //bool _response = (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("WEB_GERAL_PKG.SABER_SE_UTILIZADOR_INTERNO", (Func<string, string, bool>)DataHelper.validUser, parameters);
            //return validUser(userName, "WEB_GERAL_PKG.SABER_SE_UTILIZADOR_INTERNO");
            //return _response;

            return userAttributes != null ? userAttributes.IsInternUser() : false;
        }

        public static bool CanLogAsAgent(string userName)
        {
            var userAttributes = OSBConnector.Shared.DataServices.GetUserAttributes(userName);
            return userAttributes != null ? userAttributes.CanLogAsAgente() : false;
        }

        /*
        public static bool CanLogAsAgent_Old(string userName)
        {
            object[] parameters = new object[] { userName, "WEB_PPROD_RC_PKG.LOG_AS_AGENTE" };
            bool _response = (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("WEB_PPROD_RC_PKG.LOG_AS_AGENTE", (Func<string, string, bool>)DataHelper.validUser, parameters);
            //return validUser(userName, "WEB_PPROD_RC_PKG.LOG_AS_AGENTE");
            return _response;
        }
        */

        public static void PreSelectCommercialAccount(decimal? genericPlanID, ref string mediator, ref string collector)
        {
            //object[] parameters = new object[] { genericPlanID, mediator, collector };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("WEB_PPROD_PLANO_C_EXTRACTO_PKG.OBTEM_MEDIADOR_COBRADOR", (DecimalAndString2RefHandler)DataHelper.PerformanceMonitor_PreSelectCommercialAccount, ref parameters);
            //mediator = (string)parameters[1];
            //collector = (string)parameters[2];

            PerformanceMonitor_PreSelectCommercialAccount(genericPlanID, ref mediator, ref collector);
        }

        private static void PerformanceMonitor_PreSelectCommercialAccount(decimal? genericPlanID, ref string mediator, ref string collector)
        {
            if (genericPlanID.HasValue)
            {
                List<OracleParameter> paramIn = new List<OracleParameter>{
                    new OracleParameter("a_IDPLANO", OracleDbType.Decimal, ParameterDirection.Input){
                        Value = genericPlanID.Value
                    },
                    new OracleParameter("a_mediador", OracleDbType.Varchar2, ParameterDirection.Output),
                    new OracleParameter("a_cobrador", OracleDbType.Varchar2, ParameterDirection.Output)
                };

                try
                {
                    List<OracleParameter> paramOut = (new DatabaseAccess()).ExecuteQuery(paramIn, "WEB_PPROD_PLANO_C_EXTRACTO_PKG.OBTEM_MEDIADOR_COBRADOR");
                    mediator = paramOut[0].Value.ToString();
                    collector = paramOut[1].Value.ToString();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static string GetMediatorName(string iUserName, out string oErrorMessage)
        {
            if (string.IsNullOrEmpty(iUserName))
            {
                oErrorMessage = "false#:#Deverá preencher o Nº de conta do mediador a pesquisar.";
                return "";
            }

            string a_nome_mediador;
            OSBConnector.Shared.BusinessActivities.ValidarMediador(iUserName, out a_nome_mediador, out oErrorMessage);
            return a_nome_mediador;
        }

        /*
        public static string GetMediatorName_Old(string iUserName, out string oErrorMessage)
        {
            oErrorMessage = string.Empty;
            object[] parameters = new object[] { iUserName, oErrorMessage };
            string _response = (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("VALIDA_WEB_PKG.VALIDA_MEDIADOR", (StringAndStringOutHandler)DataHelper.PerformanceMonitor_GetMediatorName_Old, ref parameters);
            oErrorMessage = (string)parameters[1];

            return _response;
        }

        //public static string GetMediatorName(string iUserName, out string oErrorMessage)
        private static string PerformanceMonitor_GetMediatorName_Old(string iUserName, out string oErrorMessage)
        {
            string xMediatorName;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_MEDIADOR(iUserName, out xMediatorName, out oErrorMessage);
            return xMediatorName;
        }
        */

        public static string GetCollectorName(string iUserName, string login, out string oErrorMessage)
        {
            string xCollectorName;
            OSBConnector.Shared.BusinessActivities.ValidarCobrador(iUserName, login, String.Empty, out xCollectorName, out oErrorMessage);
            return xCollectorName;
        }

        /*
        public static string GetCollectorName_Old(string iUserName, string login, out string oErrorMessage)
        {
            oErrorMessage = string.Empty;
            object[] parameters = new object[] { iUserName, login, oErrorMessage };
            string _response = (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("VALIDA_WEB_PKG.VALIDA_COBRADOR", (String2AndStringOutHandler)DataHelper.PerformanceMonitor_GetCollectorName_Old, ref parameters);
            oErrorMessage = (string)parameters[2];

            return _response;
        }

        //public static string GetCollectorName(string iUserName, string login, out string oErrorMessage)
        private static string PerformanceMonitor_GetCollectorName_Old(string iUserName, string login, out string oErrorMessage)
        {
            string xCollectorName;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_COBRADOR(iUserName, login, out xCollectorName, out oErrorMessage);
            return xCollectorName;
        }
        */

        public static string getDependencyInternalAccount(string login)
        {
            var attributes = OSBConnector.Shared.DataServices.GetUserAttributes(login).Attributes; 
            var attribute = attributes != null ? attributes.Attribute.Find(x => x.Key == "lusitania.util.conta") : null;
            return attribute != null ? attribute.Value : null;            
        }

        /*
        public static string getDependencyInternalAccount_Old(string login)
        {
            object[] parameters = new object[] { login };
            string _response = (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("VALIDA_WEB_PKG.CONTA_INTERNA_DEPENDENCIA", (Func<string, string>)DataHelper.PerformanceMonitor_getDependencyInternalAccount_Old, parameters);

            return _response;
        }

        private static string PerformanceMonitor_getDependencyInternalAccount_Old(string login)
        {
            string errorMsg;
            string account;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).CONTA_INTERNA_DEPENDENCIA(login, out account, out errorMsg);
            return account;
        }
        */
        
        public static void GetFunctionValue(OracleCommand cmd)
        {
            using (OracleConnection connection = new OracleConnection(Settings.Default.ConnectionString))
            {
                cmd.Connection = connection;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                cmd.Connection.Close();
            }
        }

        public static void ExecuteQueryOpenConnection(OracleCommand cmd)
        {
            cmd.Connection = staticConnection;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
        }

        public static void CloseStaticConnection()
        {
            if (staticConnection.State == ConnectionState.Open)
                staticConnection.Close();
        }

        public static string GetStringParameter(OracleParameter oParam)
        {
            string retVal;
            if (((OracleString)(oParam.Value)).IsNull)
            {
                retVal = string.Empty;
            }
            else
            {
                retVal = ((OracleString)(oParam.Value)).Value;
            }

            return retVal;
        }

        public static decimal? GetDecimalParameter(OracleParameter oParam)
        {
            decimal? retVal;
            if (((OracleDecimal)(oParam.Value)).IsNull)
            {
                retVal = null;
            }
            else
            {
                retVal = Convert.ToDecimal(oParam.Value.ToString());
            }

            return retVal;
        }
    }
}


   
