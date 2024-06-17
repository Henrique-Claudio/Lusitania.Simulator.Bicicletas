using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Xml;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simuladores.DataLayer.Base;

namespace Lusitania.Simuladores.DataLayer.Proposal
{
    //public delegate Proposta PerformanceMonitorGravarPropostaHandler(String propostaAsXml);
    public class PropostaDS
    {
        DatabaseAccess _db = new DatabaseAccess();


        public Proposta Gravar(Proposta proposta)
        {
            //object[] parameters = new object[] { XmlSerialization.ObjectToXml<Proposta>(proposta) };
            //Proposta _prop = Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperationByReference<Proposta>("web_pprod_rc_pkg.GRAVA_PROPOSTA", (PerformanceMonitorGravarPropostaHandler)PerformanceMonitor_Gravar, false, ref parameters);
            Proposta _prop = PerformanceMonitor_Gravar(XmlSerialization.ObjectToXml<Proposta>(proposta));
            return _prop;
        }

        private Proposta PerformanceMonitor_Gravar(String propostaAsXml)
        {
            OracleParameter param = new OracleParameter("p_xml", OracleDbType.XmlType, ParameterDirection.InputOutput)
            {
                Value = propostaAsXml
            };

            try
            {
                XmlDocument outXml = _db.ExecuteQueryIO_XML(param, "web_pprod_rc_pkg.GRAVA_PROPOSTA");
                return XmlSerialization.XMLToObject<Proposta>(outXml.OuterXml);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GravarXml_Proposta(decimal idSimulacao, ref string msgErro)
        {

            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("p_idsimulacao", OracleDbType.Int32, ParameterDirection.Input){
                    Value = idSimulacao
                },
                 new OracleParameter("a_xml", OracleDbType.Clob, ParameterDirection.Output)

            };

            try
            {
                List<Object> listOut = _db.ExecuteQueryIO(listIn, "LUS_PLSQL.OUT_PROPOSTA_RC_PKG.IMPRIME_PROPOSTA_RC");
                return listOut[0].ToString();
            }
            catch (Exception ex)
            {
                msgErro = "Occureu um erro na geração do XML.";
                return string.Empty;
            }

        }



        #region Buttons

        public void AtualizaImprimiuProposta(decimal idSimulacao) {
            List<OracleParameter> param = new List<OracleParameter>(){
                new OracleParameter("a_idsimulacao", OracleDbType.Decimal, ParameterDirection.Input)
                {
                    Value = idSimulacao
                }
            };

            try
            {
                _db.ExecuteQuery(param, "web_pprod_rc_pkg.ATUALIZA_IMPRIMIU_PROPOSTA");                
            }
            catch (Exception ex)
            {                
            }
        }


        private bool CanSeeButton(decimal idSimulacao, string UserName, string source)
        {
            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("a_idsimulacao", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = idSimulacao
                },
                new OracleParameter("a_login", OracleDbType.Varchar2, ParameterDirection.Input){
                    Size = 100,
                    Value = UserName
                },

                new OracleParameter("a_botao_visivel", OracleDbType.Int32, ParameterDirection.Output)
            };


            try
            {
                List<OracleParameter> listOut = _db.ExecuteQuery(listIn, source);

                return listOut[0].Value.ToString() == "1";


            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool MostraBotaoContrato(decimal idSimulacao, string UserName)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("web_pprod_rc_pkg.MOSTRA_BOTAO_CONTRATO", (Func<decimal, string, bool>)PerformanceMonitor_MostraBotaoContrato, idSimulacao, UserName);
            return PerformanceMonitor_MostraBotaoContrato(idSimulacao, UserName);
        }

        private bool PerformanceMonitor_MostraBotaoContrato(decimal idSimulacao, string UserName)
        {

            return CanSeeButton(idSimulacao, UserName, "web_pprod_rc_pkg.MOSTRA_BOTAO_CONTRATO");
            
        }

        public bool MostraBotaoImpRecibo(decimal idSimulacao, string UserName)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("web_pprod_rc_pkg.MOSTRA_BOTAO_IMP_RECIBO", (Func<decimal, string, bool>)PerformanceMonitor_MostraBotaoImpRecibo, idSimulacao, UserName);
            return PerformanceMonitor_MostraBotaoImpRecibo(idSimulacao, UserName);
        }

        private bool PerformanceMonitor_MostraBotaoImpRecibo(decimal idSimulacao, string UserName)
        {
            return CanSeeButton(idSimulacao, UserName, "web_pprod_rc_pkg.MOSTRA_BOTAO_IMP_RECIBO");
        }
        #endregion

       


    }
}
