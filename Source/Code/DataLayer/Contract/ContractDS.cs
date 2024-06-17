using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lusitania.Simuladores.DataLayer.Base;
using Oracle.DataAccess.Client;
using System.Data;

namespace Lusitania.Simuladores.DataLayer.Contract
{
    //public delegate void PerformanceMonitorGravarHandler(decimal idSimulacao, string userName, int emissaoDoc, out string apolice, out string recibo, out string msgErro);

    public class ContractDS
    {

        DatabaseAccess _db = new DatabaseAccess();

        public void Gravar(decimal idSimulacao, string userName, int emissaoDoc, out string apolice, out string recibo, out string msgErro)
        {
            //object[] parameters = new object[] { idSimulacao, userName, emissaoDoc, string.Empty, string.Empty, string.Empty };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("web_pprod_rc_pkg.GRAVA_CONTRATO", (PerformanceMonitorGravarHandler)PerformanceMonitor_Gravar, ref parameters);
            //apolice = (string)parameters[3];
            //recibo = (string)parameters[4];
            //msgErro = (string)parameters[5];
            PerformanceMonitor_Gravar(idSimulacao, userName, emissaoDoc, out apolice, out recibo, out msgErro);
        }
        private void PerformanceMonitor_Gravar(decimal idSimulacao, string userName, int emissaoDoc, out string apolice, out string recibo, out string msgErro)
        {
            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("a_idsimulacao", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = idSimulacao
                },
                new OracleParameter("a_lognome", OracleDbType.Varchar2, ParameterDirection.Input){
                    Size = 100,
                    Value = userName
                },
                new OracleParameter("a_emissao_doc", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = emissaoDoc
                },

                new OracleParameter("a_apolice", OracleDbType.Varchar2, ParameterDirection.Output){
                    Size = 100
                },
                new OracleParameter("a_Recibo", OracleDbType.Varchar2,  ParameterDirection.Output){
                    Size = 100
                },
                new OracleParameter("a_msg_erro", OracleDbType.Varchar2,  ParameterDirection.Output){
                    Size = 2000
                }
            };


            try
            {
                List<OracleParameter> listOut = _db.ExecuteQuery(listIn, "web_pprod_rc_pkg.GRAVA_CONTRATO");

                apolice = listOut[0].Value.ToString();
                recibo = listOut[1].Value.ToString();
                msgErro = listOut[2].Value.ToString() == "null" ? "" : listOut[2].Value.ToString();
            }
            catch (Exception ex)
            {
                apolice =
                recibo = string.Empty;
                msgErro = "Erro na gravação de contrato.";
            }

        }

        public ContractModel IMP_RECIBO(string simulationID)
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperation<ContractModel>("WEB_PPROD_RC_PKG.IMP_RECIBO_PARAM", (Func<string, ContractModel>)PerformanceMonitor_IMP_RECIBO, false, simulationID);
            return PerformanceMonitor_IMP_RECIBO(simulationID);
        }

        private ContractModel PerformanceMonitor_IMP_RECIBO(string simulationID)
        {
            List<OracleParameter> paramsList = new List<OracleParameter>(){
                
                //IN PARAMETERS
                new OracleParameter("a_IDSimulacao", OracleDbType.Varchar2, ParameterDirection.Input){
                    Value = simulationID
                },
                 //OUT PARAMETERS
                new OracleParameter("a_cursor", OracleDbType.RefCursor, ParameterDirection.Output)

            };

            try
            {
                DataTable _dt = _db.CursorToDataTable(paramsList, "WEB_PPROD_RC_PKG.IMP_RECIBO_PARAM");
                if (_dt.Rows.Count > 0)
	            {
                    return new ContractModel()
                    {
                        RAMO = _dt.Rows[0]["RAMO"].ToString(),
                        MODALIDADE = _dt.Rows[0]["MODALIDADE"].ToString(),
                        SUBMODALIDADE = _dt.Rows[0]["SUBMODALIDADE"].ToString(),
                        APOLICE = _dt.Rows[0]["APOLICE"].ToString(),
                        RECIBO = _dt.Rows[0]["RECIBO"].ToString()
                    };
	            }
                return null;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

    }
}
