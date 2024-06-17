using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;
using OSBConnector.Extensions;

namespace Lusitania.Simuladores.DataLayer.Simulation
{
    public class ProtocoloDS
    {
        public int Codigo { get; set; }
        public string Descritivo { get; set; }

        public List<ProtocoloDS> Load(string ramo, string modalidade, string submodalidade, string sLogin, string sConta)
        {
            List<OracleParameter> paramsList = new List<OracleParameter> { 
                //IN PARAMETERS
                new OracleParameter("prmRamo", OracleDbType.Varchar2, 2, ParameterDirection.Input){
                    Value = ramo
                },
                new OracleParameter("prmModalidade", OracleDbType.Varchar2, 2, ParameterDirection.Input){
                    Value = modalidade
                },
                new OracleParameter("prmSubModalidade", OracleDbType.Varchar2, 2, ParameterDirection.Input){
                    Value = submodalidade
                },
                new OracleParameter("prmProduto", OracleDbType.Varchar2, 5, ParameterDirection.Input){
                    Value = "00006"
                },
                new OracleParameter("prmLogin", OracleDbType.Varchar2, 10, ParameterDirection.Input){
                    Value = sLogin
                },
                new OracleParameter("prmConta", OracleDbType.Varchar2, 10, ParameterDirection.Input){
                    Value = sConta
                },
                
                //OUT PARAMETERS
                new OracleParameter("A_CURSOR_PROTOCOLOS", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsList, "PROTOCOLOS_NEGOCIO_PKG.OBTER_PROTOCOLOS_TIPO_PROT_C");

            List<ProtocoloDS> _protocolos = new List<ProtocoloDS>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TIPO_ASSOCIADO"].ToString() == "E")
                    {
                        ProtocoloDS protocolo = new ProtocoloDS()
                        {
                            Codigo = int.Parse(dt.Rows[i]["COD_PROTONEG"].ToString()),
                            Descritivo = dt.Rows[i]["DESC_PROTONEG"].ToString()
                        };

                        _protocolos.Add(protocolo);
                    }
                    else
                    {
                        {
                            ProtocoloDS protocolo = new ProtocoloDS()
                            {
                                Codigo = int.Parse(dt.Rows[i]["COD_PROTONEG"].ToString()),
                                Descritivo = dt.Rows[i]["DESC_PROTONEG"].ToString()
                            };

                            _protocolos.Add(protocolo);
                        }
                    }
                }

            }

            return _protocolos;
        }

        public string getTipoProtocolo(int protocolo)
        {
            List<OracleParameter> InParamsList = new List<OracleParameter> { 
                //IN PARAMETERS
                new OracleParameter("prmCodProtoneg ", OracleDbType.Int32, ParameterDirection.Input){
                    Value = protocolo
                },
                //OUT PARAMETERS
                new OracleParameter("prmTipoCliente", OracleDbType.Varchar2, 10){
                    Direction = ParameterDirection.Output
                }
            };

            try
            {
                List<OracleParameter> OutParamsList = (new DatabaseAccess()).ExecuteQuery(InParamsList, "PROTOCOLOS_NEGOCIO_PKG.obtemTipoCliente");
                return OutParamsList[0].Value.ToString();
            }
            catch (Exception ex)
            {

                return string.Empty;
            }


        }
    }
}
