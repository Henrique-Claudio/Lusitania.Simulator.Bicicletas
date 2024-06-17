using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Common;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;
using System.Xml;
using Oracle.DataAccess.Types;


namespace Lusitania.Simuladores.DataLayer.Simulation
{
    //public delegate void DecimalAndString2RefHandler(decimal arg1, string arg2, ref string arg3);
    //public delegate Simulacao SimulacaoAndStringOutHandler(string arg1, ref string arg2);
    //public delegate Elaborador StringOutHandler(string arg1);
    //public delegate Simulacao DecimalAndStringRefHandler(decimal arg1, ref string arg2);
        
    public class SimulacaoDS
    {
        public Simulacao Simular(Simulacao simulacao, ref string msgErro)
        {
            //object[] parameters = new object[] { XmlSerialization.ObjectToXml<Simulacao>(simulacao), msgErro };
            //Simulacao _sim = Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperationByReference<Simulacao>("web_pprod_rc_pkg.GRAVAR_SIMULACAO", (SimulacaoAndStringOutHandler)PerformanceMonitor_Simular, false, ref parameters);
            //msgErro = (string)parameters[1];

            Simulacao _sim = PerformanceMonitor_Simular(XmlSerialization.ObjectToXml<Simulacao>(simulacao), ref msgErro);
            return _sim;
        }

        private Simulacao PerformanceMonitor_Simular(String simulacaoAsXml, ref string msgErro)
        {
            msgErro = string.Empty;
            OracleParameter param = new OracleParameter("p_xml", OracleDbType.XmlType, ParameterDirection.InputOutput)
            {
                Value = simulacaoAsXml
            };

            try
            {
                XmlDocument outXml = (new DatabaseAccess()).ExecuteQueryIO_XML(param, "web_pprod_rc_pkg.GRAVAR_SIMULACAO");
                return XmlSerialization.XMLToObject<Simulacao>(outXml.OuterXml);
            }
            catch (Exception ex)
            {
                msgErro = ex.Message;
                return null;
            }
        }

        public static Simulacao ObterSimulacao(decimal simulacaoId, ref string msgErro) 
        {
            //object[] parameters = new object[] { simulacaoId, msgErro };
            //Simulacao _obtsim = Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperationByReference<Simulacao>("web_pprod_rc_pkg.OBTER_SIMULACAO", (DecimalAndStringRefHandler)PerformanceMonitor_ObterSimulacao, false, ref parameters);
            //msgErro = (string)parameters[1];

            Simulacao _obtsim = PerformanceMonitor_ObterSimulacao(simulacaoId, ref msgErro);

            return _obtsim;
        }

        private static Simulacao PerformanceMonitor_ObterSimulacao(decimal simulacaoId, ref string msgErro)
        {
            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("p_id_simulacao", OracleDbType.Int32, ParameterDirection.Input){
                    Value = simulacaoId
                },
                new OracleParameter("p_msg_error", OracleDbType.Varchar2, ParameterDirection.Output),
                new OracleParameter("p_xml", OracleDbType.XmlType, ParameterDirection.Output)
            };

            try
            {
                List<Object> listOut = (new DatabaseAccess()).ExecuteQueryIO(listIn, "web_pprod_rc_pkg.OBTER_SIMULACAO");
                if (!((OracleString)listOut[0]).IsNull)
                {
                    msgErro = ((OracleString)listOut[0]).Value;
                    return null;
                }
                else
                {
                    return XmlSerialization.XMLToObject<Simulacao>((string)listOut[1]);
                }
                
            }
            catch (Exception ex)
            {
                msgErro = "Occureu um erro na obtenção dos dados.";
                return null;
            }
        }

        public static void GravarXML_Cotacao(decimal idSimulacao, ref string msgErro) {

            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("p_idsimulacao", OracleDbType.Int32, ParameterDirection.Input){
                    Value = idSimulacao
                }
            };

            try
            {
                List<OracleParameter> listOut = (new DatabaseAccess()).ExecuteQuery(listIn, "OUT_COTACAO_RC_PKG.IMPRIME_COTACAO_BICICLETA");
                
            }
            catch (Exception ex)
            {
                msgErro = "Occureu um erro na geração do XML.";
            }
        }

        public Elaborador GetElaborador(string userName)
        {
            Elaborador elaborador = new Elaborador();
            var userAttributes = OSBConnector.Shared.DataServices.GetUserAttributes(userName);

            if (userAttributes != null)
            {

                elaborador.Nome = userAttributes.Person.FullName;
                elaborador.Email = userAttributes.Person.Email;
                elaborador.Telefon = userAttributes.IsInternUser() ? String.Empty : userAttributes.Person.Phone;

            }

            return elaborador;

            //object[] parameters = new object[] { userName };
            //Elaborador _elab = (Elaborador)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperationByReference<Elaborador>("WEB_GERAL_PKG.SABER_DADOS_AGENTE", (StringOutHandler)PerformanceMonitor_GetElaborador, false, ref parameters);
            //return _elab;
        }

        //private Elaborador PerformanceMonitor_GetElaborador(string userName)
        //{
        //    List<OracleParameter> paramsListIN = new List<OracleParameter> { 
        //        //IN PARAMETERS
        //        new OracleParameter("A_LOGNOME", OracleDbType.Varchar2, ParameterDirection.Input){
        //            Value = userName
        //        },
        //        //OUT PARAMETERS
        //        new OracleParameter("A_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
        //    };


        //    DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsListIN, "WEB_GERAL_PKG.SABER_DADOS_AGENTE");

        //    if (dt.Rows.Count > 0)
        //    {

        //        return new Elaborador()
        //        {
        //            Email = dt.Rows[0]["EMAIL"].ToString(),
        //            Nome = dt.Rows[0]["NOME"].ToString(),
        //            Telefon = dt.Rows[0]["TELEFONE"].ToString()
        //        };
        //    }


        //    return null;

        //}


        #region Plano E+

        public Tomador Saber_PessoaProv(decimal planoID) {
            List<OracleParameter> paramsListIN = new List<OracleParameter> { 
                //IN PARAMETERS
                new OracleParameter("a_IDPLANO", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = planoID
                },
                //OUT PARAMETERS
                new OracleParameter("A_CURSOR", OracleDbType.RefCursor, ParameterDirection.Output)
            };


            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsListIN, "WEB_PPROD_PLANO_C_EXTRACTO_PKG.Saber_PessoaProv");

            if (dt.Rows.Count > 0)
            {

                return new Tomador()
                {
                    Email = dt.Rows[0]["e_mail01"].ToString(),
                    Nome = dt.Rows[0]["PNome"].ToString(),
                    Telefon = dt.Rows[0]["Telefone01"].ToString(),
                    Telemovel = dt.Rows[0]["Telemovel"].ToString(),
                    NIF = dt.Rows[0]["PContrib"].ToString()
                };
            }


            return null;
        }

        public void ADD_BIKE(decimal planoID, string simualtionID, ref string msgErro)
        {
            //object[] parameters = new object[] { planoID, simualtionID, msgErro };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("WEB_PPROD_PLANO_C_EXTRACTO_PKG.ADD_BIKE", (DecimalAndString2RefHandler)PerformanceMonitor_ADD_BIKE, ref parameters);
            //msgErro = (string)parameters[2];

            PerformanceMonitor_ADD_BIKE(planoID, simualtionID, ref msgErro);
        }

        private void PerformanceMonitor_ADD_BIKE(decimal planoID, string simualtionID, ref string msgErro)
        {
            msgErro = string.Empty;
            List<OracleParameter> listIn = new List<OracleParameter>()
            {
                //IN
                new OracleParameter("a_IDPLANO", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = planoID
                },
                new OracleParameter("a_IDSIMULACAO", OracleDbType.Decimal, ParameterDirection.Input){
                    Value = simualtionID
                },
                //OUT
                new OracleParameter("a_msg_error", OracleDbType.Varchar2, ParameterDirection.Output){
                    Size = 2000
                }
            };

            try
            {
                List<OracleParameter> listOut = (new DatabaseAccess()).ExecuteQuery(listIn, "WEB_PPROD_PLANO_C_EXTRACTO_PKG.ADD_BIKE");
                msgErro = listOut[0].Value.ToString() == "null" ? "" : listOut[0].Value.ToString();
            }
            catch (Exception ex)
            {
                msgErro = "Erro na gravação dos dados.";
            }
        }

        #endregion
    }
}
