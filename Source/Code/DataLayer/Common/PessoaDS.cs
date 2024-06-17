using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;

namespace Lusitania.Simuladores.DataLayer.Common
{
    public class PessoaDS
    {
        public static List<Pessoa> getClientList(string codigo, string nif)
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<Pessoa>("WEB_GERAL_PKG.SABER_PESSOA", (Func<string, string, List<Pessoa>>)PerformanceMonitor_getClientList, codigo, nif);
            return PerformanceMonitor_getClientList(codigo, nif);
        }
        private static List<Pessoa> PerformanceMonitor_getClientList(string codigo, string nif)
        {


            List<OracleParameter> paramsList = new List<OracleParameter> { 
                //IN PARAMETERS
                new OracleParameter("a_PCodigo", OracleDbType.Varchar2, 20, ParameterDirection.Input){
                    Value = codigo
                },
                  new OracleParameter("a_PContrib", OracleDbType.Varchar2, 20, ParameterDirection.Input){
                    Value = nif
                },
                
                //OUT PARAMETERS
                new OracleParameter("a_cursor", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsList, "WEB_GERAL_PKG.SABER_PESSOA");

            List<Pessoa> _clients = new List<Pessoa>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _clients.Add(
                        new Pessoa()
                        {
                            PCODIGO = dt.Rows[i]["PCODIGO"].ToString(),
                            PCONTRIB = dt.Rows[i]["PCONTRIB"].ToString(),
                            PBI = dt.Rows[i]["PBI"].ToString(),
                            CARTA_CONDUCAO = dt.Rows[i]["CARTA_CONDUCAO"].ToString(),
                            PTITULO = dt.Rows[i]["PTITULO"].ToString(),
                            PNOME = dt.Rows[i]["PNOME"].ToString(),
                            NACIONALIDADE = dt.Rows[i]["NACIONALIDADE"].ToString(),
                            ESTADO_CIVIL = dt.Rows[i]["ESTADO_CIVIL"].ToString(),
                            PPROFISS = dt.Rows[i]["PPROFISS"].ToString(),
                            PTIPO = dt.Rows[i]["PTIPO"].ToString(),
                            TELEMOVEL = dt.Rows[i]["TELEMOVEL"].ToString(),
                            Contacto01 = new Endereco()
                            {
                                TELEFONE = dt.Rows[i]["TELEFONE01"].ToString(),
                                TELEX_FAX = dt.Rows[i]["TELEX_FAX01"].ToString(),
                                MORADA_TIPO = dt.Rows[i]["MORADA_TIPO01"].ToString(),
                                TITULO_ARTERIA = dt.Rows[i]["TITULO_ARTERIA01"].ToString(),
                                LOCAL = dt.Rows[i]["LOCAL01"].ToString(),
                                MORADA_DESC = dt.Rows[i]["MORADA_DESC01"].ToString(),
                                MORADA_NUMERO = dt.Rows[i]["MORADA_NUMERO01"].ToString(),
                                MORADA_ANDAR = dt.Rows[i]["MORADA_ANDAR01"].ToString(),
                                E_MAIL = dt.Rows[i]["E_MAIL01"].ToString(),
                                LOCALIDADE = dt.Rows[i]["LOCALIDADE01"].ToString(),
                                CODIGO_POSTAL = dt.Rows[i]["CODIGO_POSTAL01"].ToString(),
                                SUFIXO_POSTAL = dt.Rows[i]["SUFIXO_POSTAL01"].ToString(),
                                PAIS = dt.Rows[i]["PAIS01"].ToString()
                            },
                            Contacto02 = new Endereco()
                            {
                                AO_CUIDADO = dt.Rows[i]["AO_CUIDADO02"].ToString(),
                                TELEFONE = dt.Rows[i]["TELEFONE02"].ToString(),
                                TELEX_FAX = dt.Rows[i]["TELEX_FAX02"].ToString(),
                                MORADA_TIPO = dt.Rows[i]["MORADA_TIPO02"].ToString(),
                                TITULO_ARTERIA = dt.Rows[i]["TITULO_ARTERIA02"].ToString(),
                                LOCAL = dt.Rows[i]["LOCAL02"].ToString(),
                                MORADA_DESC = dt.Rows[i]["MORADA_DESC02"].ToString(),
                                MORADA_NUMERO = dt.Rows[i]["MORADA_NUMERO02"].ToString(),
                                MORADA_ANDAR = dt.Rows[i]["MORADA_ANDAR02"].ToString(),
                                E_MAIL = dt.Rows[i]["E_MAIL02"].ToString(),
                                LOCALIDADE = dt.Rows[i]["LOCALIDADE02"].ToString(),
                                CODIGO_POSTAL = dt.Rows[i]["CODIGO_POSTAL02"].ToString(),
                                SUFIXO_POSTAL = dt.Rows[i]["SUFIXO_POSTAL02"].ToString(),
                                PAIS = dt.Rows[i]["PAIS02"].ToString()
                            },
                            NUMREGISTOS = dt.Rows[i]["NUMREGISTOS"].ToString(),
                            DT_NASCIMENTO = dt.Rows[i]["DT_NASCIMENTO"].ToString(),
                            DT_CARTA_CONDUCAO = dt.Rows[i]["DT_CARTA_CONDUCAO"].ToString(),
                            CLIENTE_MG = dt.Rows[i]["CLIENTE_MG"].ToString(),
                            CODIGO_EMPREGADO_MG = dt.Rows[i]["CODIGO_EMPREGADO_MG"].ToString(),
                            CODIGO_ASSOCIADO_MG = dt.Rows[i]["CODIGO_ASSOCIADO_MG"].ToString(),
                            BALCAO_TITULAR = dt.Rows[i]["BALCAO_TITULAR"].ToString()
                        }

                    );
                }

            }

            return _clients;
        }

        public static Pessoa getClient(string codigo, string nif)
        {
            List<Pessoa> _clientes = getClientList(codigo, nif);

            if (_clientes.Count > 0)
            {
                return _clientes[0];
            }

            return null;
        }

        #region Plano E+

        public Pessoa Saber_PessoaPlano(decimal planoID)
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperation<Pessoa>("WEB_PPROD_PLANO_C_EXTRACTO_PKG.Saber_PessoaProv", (Func<decimal, Pessoa>)PerformanceMonitor_Saber_PessoaPlano, false, planoID);
            return PerformanceMonitor_Saber_PessoaPlano(planoID);
        }

        private Pessoa PerformanceMonitor_Saber_PessoaPlano(decimal planoID)
        {
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

                return new Pessoa()
                        {
                            PCODIGO = dt.Rows[0]["PCodigo"].ToString(),
                            PCONTRIB = dt.Rows[0]["PContrib"].ToString(),
                            PBI = dt.Rows[0]["PBI"].ToString(),
                            CARTA_CONDUCAO = dt.Rows[0]["CARTA_CONDUCAO"].ToString(),
                            PTITULO = dt.Rows[0]["PTitulo"].ToString(),
                            PNOME = dt.Rows[0]["PNome"].ToString(),
                            NACIONALIDADE = dt.Rows[0]["Nacionalidade"].ToString(),
                            ESTADO_CIVIL = dt.Rows[0]["Estado_Civil"].ToString(),
                            PPROFISS = dt.Rows[0]["PProfiss"].ToString(),
                            PTIPO = dt.Rows[0]["PTipo"].ToString(),
                            TELEMOVEL = dt.Rows[0]["Telemovel"].ToString(),
                            DT_NASCIMENTO = DateTime.ParseExact(dt.Rows[0]["Dt_Nascimento"].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString(),

                            Contacto01 = new Endereco()
                            {
                                TELEFONE = dt.Rows[0]["Telefone01"].ToString(),
                                TELEX_FAX = dt.Rows[0]["Telex_Fax01"].ToString(),
                                MORADA_TIPO = dt.Rows[0]["Morada_Tipo01"].ToString(),
                                LOCAL = dt.Rows[0]["Local01"].ToString(),
                                MORADA_DESC = dt.Rows[0]["Morada_Desc01"].ToString(),
                                MORADA_NUMERO = dt.Rows[0]["Morada_Numero01"].ToString(),
                                MORADA_ANDAR = dt.Rows[0]["Morada_Andar01"].ToString(),
                                E_MAIL = dt.Rows[0]["e_mail01"].ToString(),
                                LOCALIDADE = dt.Rows[0]["Localidade01"].ToString(),
                                CODIGO_POSTAL = dt.Rows[0]["Codigo_Postal01"].ToString(),
                                SUFIXO_POSTAL = dt.Rows[0]["Sufixo_Postal01"].ToString(),
                                PAIS = dt.Rows[0]["Pais01"].ToString()
                            },
                            Contacto02 = new Endereco()
                            {
                                AO_CUIDADO = dt.Rows[0]["Ao_Cuidado02"].ToString(),
                                MORADA_TIPO = dt.Rows[0]["Morada_Tipo02"].ToString(),
                                LOCAL = dt.Rows[0]["Local02"].ToString(),
                                MORADA_DESC = dt.Rows[0]["Morada_Desc02"].ToString(),
                                MORADA_NUMERO = dt.Rows[0]["Morada_Numero02"].ToString(),
                                MORADA_ANDAR = dt.Rows[0]["Morada_Andar02"].ToString(),
                                LOCALIDADE = dt.Rows[0]["Localidade02"].ToString(),
                                CODIGO_POSTAL = dt.Rows[0]["Codigo_Postal02"].ToString(),
                                SUFIXO_POSTAL = dt.Rows[0]["Sufixo_Postal02"].ToString(),
                                PAIS = dt.Rows[0]["Pais02"].ToString()
                            }

                        };

            }


            return null;
        }




        #endregion
    }
}
