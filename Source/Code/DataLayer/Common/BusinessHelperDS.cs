using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;

namespace Lusitania.Simuladores.DataLayer.Common
{
    public class BusinessHelperDS
    {
        public static bool VALIDA_CONTRATO_EMISSAO_DOC(string userName, int simulatorCode, decimal simulationID)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("VALIDA_WEB_PKG.VALIDA_CONTRATO_EMISSAO_DOC", (Func<string, int, decimal, bool>)PerformanceMonitor_VALIDA_CONTRATO_EMISSAO_DOC, userName, simulatorCode, simulationID);
            return PerformanceMonitor_VALIDA_CONTRATO_EMISSAO_DOC(userName, simulatorCode, simulationID);
        }
        private static bool PerformanceMonitor_VALIDA_CONTRATO_EMISSAO_DOC(string userName, int simulatorCode, decimal simulationID)
        {

            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("a_login", OracleDbType.Varchar2, ParameterDirection.Input){
                    Value = userName
                },
                new OracleParameter("a_idsimulador", OracleDbType.Int32, ParameterDirection.Input){
                    Value = simulatorCode
                },
                new OracleParameter("a_idsimulacao", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = simulationID
                },

                new OracleParameter("a_emissao_doc", OracleDbType.Int32, ParameterDirection.Output)
            };


            try
            {
                List<OracleParameter> listOut = (new DatabaseAccess()).ExecuteQuery(listIn, "VALIDA_WEB_PKG.VALIDA_CONTRATO_EMISSAO_DOC");

                return listOut[0].Value.ToString() == "1";


            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool PODE_EFECTUAR_PAGAM_POSTERIOR(int simulatorCode, decimal simulationID, string codPlano)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("VALIDA_WEB_PKG.PODE_EFECTUAR_PAGAM_POSTERIOR", (Func<int, decimal, string, bool>)PerformanceMonitor_PODE_EFECTUAR_PAGAM_POSTERIOR, simulatorCode, simulationID, codPlano);
            return PerformanceMonitor_PODE_EFECTUAR_PAGAM_POSTERIOR(simulatorCode, simulationID, codPlano);
        }
        private static bool PerformanceMonitor_PODE_EFECTUAR_PAGAM_POSTERIOR(int simulatorCode, decimal simulationID, string codPlano)
        {

            List<OracleParameter> listIn = new List<OracleParameter>(){
               
                new OracleParameter("a_idsimulador", OracleDbType.Int32, ParameterDirection.Input){
                    Value = simulatorCode
                },
                new OracleParameter("a_idsimulacao", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = simulationID
                },
                new OracleParameter("a_cod_plano", OracleDbType.Varchar2, ParameterDirection.Input){
                    Value = codPlano
                },

                new OracleParameter("a_pag_posterior", OracleDbType.Int32, ParameterDirection.Output)
            };


            try
            {
                List<OracleParameter> listOut = (new DatabaseAccess()).ExecuteQuery(listIn, "VALIDA_WEB_PKG.PODE_EFECTUAR_PAGAM_POSTERIOR");

                return listOut[0].Value.ToString() == "1";


            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
