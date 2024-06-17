using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;

namespace Lusitania.Simuladores.DataLayer.Common.Model
{
    public class GenericListsDS
    {
        public string Codigo { get; set; }
        public string Descritivo { get; set; }

        public static List<GenericListsDS> Load(string procedure)
        {
            return Load(string.Empty, procedure);
        }

        public static List<GenericListsDS> Load(string tipo, string procedure)
        {
            if (procedure == UtilityG.GetValue(GenericLists.PROFISSAO))
            {
                //workaround para o osb connector não rebentar ao pesquisar sem sexo nas profissões...
                return OSBConnector.Shared.DataServices.SaberProfissoes(string.IsNullOrEmpty(tipo) ? " " : tipo).Select(p => new GenericListsDS() { Codigo = p.Codigo, Descritivo = p.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.NACIONALIDADE))
            {
                return OSBConnector.Shared.DataServices.SaberNacionalidades().Select(n => new GenericListsDS() { Codigo = n.Codigo, Descritivo = n.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.PAIS))
            {
                return OSBConnector.Shared.DataServices.SaberPaises().Select(n => new GenericListsDS() { Codigo = n.Codigo, Descritivo = n.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.TITLO_ONORIFICO))
            {
                return OSBConnector.Shared.DataServices.SaberTituloNome(tipo).Select(n => new GenericListsDS() { Codigo = n.Codigo, Descritivo = n.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.ESTADO_CIVIL))
            {
                return OSBConnector.BDLeg.Clientes.DataService.EstadoCivil.Obter_V1_0().Select(ec => new GenericListsDS() { Codigo = ec.Codigo, Descritivo = ec.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.SEXO))
            {
                //Epocas, 2017/05/04 - SaberSexo está marcado como a alterar na aplicação, por isso em vez de chamar o serviço, chama o enumerado
                //return OSBConnector.Shared.DataServices.SaberSexo().Select(ec => new GenericListsDS() { Codigo = ec.Codigo, Descritivo = ec.Descritivo }).ToList();
                return OSBConnector.Enumerations.Common.Sexo.Select(ec => new GenericListsDS(){Codigo = ec.Codigo, Descritivo = ec.Descritivo}).ToList();
            }

            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<GenericListsDS>(procedure, (Func<string, string, List<GenericListsDS>>)PerformanceMonitor_Load, tipo, procedure);
            return PerformanceMonitor_Load(tipo, procedure);
        }

        /*
        public static List<GenericListsDS> Load_Old(string tipo, string procedure)
        {
            if (procedure == UtilityG.GetValue(GenericLists.PROFISSAO))
            {
                return OSBConnector.Shared.DataServices.SaberProfissoes(tipo).Select(p => new GenericListsDS() { Codigo = p.Codigo, Descritivo = p.Descritivo }).ToList();
            }
            else if (procedure == UtilityG.GetValue(GenericLists.NACIONALIDADE))
            {
                return OSBConnector.Shared.DataServices.SaberNacionalidades().Select(n => new GenericListsDS() { Codigo = n.Codigo, Descritivo = n.Descritivo }).ToList();
            }

            return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<GenericListsDS>(procedure, (Func<string, string, List<GenericListsDS>>)PerformanceMonitor_Load, tipo, procedure);
        }
        */

        private static List<GenericListsDS> PerformanceMonitor_Load(string tipo, string procedure)
        {
            List<OracleParameter> paramsList = new List<OracleParameter>();
            string paramTipoIN = "";


            if (UtilityG.GetValue(GenericLists.TITLO_ONORIFICO) == procedure)
            {
                paramTipoIN = "a_TIPO";
            }

            if (UtilityG.GetValue(GenericLists.PROFISSAO) == procedure)
            {
                paramTipoIN = "a_SEXO";
            }

            if (UtilityG.GetValue(GenericLists.FRACCIONAMENTO) == procedure)
            {
                paramTipoIN = "a_produto";
            }


            //IN PARAMETERS
            if (!string.IsNullOrEmpty(paramTipoIN))
            {
                paramsList.Add(new OracleParameter(paramTipoIN, OracleDbType.Varchar2, ParameterDirection.Input) { Value = tipo });
            }

            //OUT PARAMETERS
            paramsList.Add(new OracleParameter("a_cursor", OracleDbType.RefCursor, ParameterDirection.Output));


            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsList, procedure);

            List<GenericListsDS> _coberturas = new List<GenericListsDS>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _coberturas.Add(new GenericListsDS()
                    {
                        Codigo = dt.Rows[i]["CODIGO"].ToString(),
                        Descritivo = dt.Rows[i]["DESCRITIVO"].ToString()
                    }

                    );
                }

            }

            return _coberturas;
        }


        #region ------ Parentesco ------
              
        public static List<GenericListsDS> ListaParentesco( string produto)
        {
            return ListaParentesco( UtilityG.RMS , produto, "S");
        }

        public static List<GenericListsDS> ListaParentesco(string rms, string produto, string remove_titular)
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<GenericListsDS>("WEB_PPROD_RC_PKG.LISTA_VALORES_PARENT", (Func<string, string, string, List<GenericListsDS>>)PerformanceMonitor_ListaParentesco, rms, produto, remove_titular);
            return PerformanceMonitor_ListaParentesco(rms, produto, remove_titular);
        }
        private static List<GenericListsDS> PerformanceMonitor_ListaParentesco(string rms, string produto, string remove_titular)
        {
            List<OracleParameter> paramsList = new List<OracleParameter>();

            //IN PARAMETERS            
            paramsList.Add(new OracleParameter("p_rms", OracleDbType.Varchar2, ParameterDirection.Input) { Value = rms });
            paramsList.Add(new OracleParameter("p_produto", OracleDbType.Varchar2, ParameterDirection.Input) { Value = produto });
            paramsList.Add(new OracleParameter("p_remove_titular", OracleDbType.Varchar2, ParameterDirection.Input) { Value = remove_titular });

            //OUT PARAMETERS
            paramsList.Add(new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output));


            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsList, "WEB_PPROD_RC_PKG.LISTA_VALORES_PARENT");

            List<GenericListsDS> _parentesco = new List<GenericListsDS>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _parentesco.Add(new GenericListsDS()
                    {
                        Codigo = dt.Rows[i]["CODIGO"].ToString(),
                        Descritivo = dt.Rows[i]["DESCRITIVO"].ToString()
                    }

                    );
                }

            }

            return _parentesco;
        }

        #endregion
    }
}
