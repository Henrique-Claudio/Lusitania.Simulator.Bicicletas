using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using Lusitania.Simuladores.DataLayer.Base;
using Lusitania.Simuladores.DataLayer.Simulation.Info;
using Lusitania.Simuladores.DataLayer.Common;

namespace Lusitania.Simuladores.DataLayer.Simulation
{
    public class CoberturasDS
    {
        public string Produto { get; set; }
        public string CodCobertura { get; set; }
        public string DescCobertura { get; set; }
        public string Capital { get; set; }

        public List<CoberturasDS> Load()
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<CoberturasDS>("WEB_PPROD_RC_PKG.LISTA_VALORES_COBERTURAS", (Func<List<CoberturasDS>>)PerformanceMonitor_Load, null);
            return PerformanceMonitor_Load();
        }

        //public List<CoberturasDS> Load()
        private List<CoberturasDS> PerformanceMonitor_Load()
        {
            List<OracleParameter> paramsList = new List<OracleParameter> { 
                
                //OUT PARAMETERS
                new OracleParameter("a_cursor", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            DataTable dt = (new DatabaseAccess()).CursorToDataTable(paramsList, "WEB_PPROD_RC_PKG.LISTA_VALORES_COBERTURAS");

            List<CoberturasDS> _coberturas = new List<CoberturasDS>();

            

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _coberturas.Add(new CoberturasDS()
                        {
                            Produto = dt.Rows[i]["CODPRODUTO"].ToString(),
                            CodCobertura = dt.Rows[i]["CODCOBERTURA"].ToString(),
                            DescCobertura = dt.Rows[i]["DESCCOBERTURA"].ToString(),
                            Capital = dt.Rows[i]["CAPITAL"].ToString()
                        }

                    );
                }

            }

            return _coberturas;
        }

        #region Helpeer Methods

        public DataTable GridCoberturas()
        {

            DataTable dt = new DataTable();
            List<CoberturasDS> _coberturas = Load().OrderBy(x=> x.CodCobertura).ToList();
            var  _produtos = _coberturas.Select(x => new { produto = x.Produto }).GroupBy(x => x.produto).ToList();


            //definir header
            DataColumn _colCobertura = new DataColumn("Cobertura");
            dt.Columns.Add(_colCobertura);
            DataColumn _colCodCobertura = new DataColumn("CodCobertura");
            dt.Columns.Add(_colCodCobertura);
            foreach (var item in _produtos)
            {
                dt.Columns.Add(item.Key);    
            }
            
            
            
            //construir o body
            if (_coberturas.Count > 0)
            { 
                DataRow row = dt.NewRow();
                string cobertura =  _coberturas[0].CodCobertura;
                row[_colCobertura] = _coberturas[0].DescCobertura;
                row[_colCodCobertura] = _coberturas[0].CodCobertura;

                for (int i = 0; i < _coberturas.Count; i++)
                {

                    if (cobertura == _coberturas[i].CodCobertura)
                    {
                        foreach (var _prod in _produtos)
                        {
                            if (_prod.Key == _coberturas[i].Produto)
                            {


                                row[_prod.Key] = UtilityG.GetFormatDoubleValue(_coberturas[i].Capital);        
                            }                            
                        }

                        //gravar a ultima linha
                        if (i == (_coberturas.Count - 1))
                        {
                             dt.Rows.Add(row);
                        }
                    }
                    else
                    {
                        
                        dt.Rows.Add(row);

                        cobertura = _coberturas[i].CodCobertura;
                        row = dt.NewRow();
                        row[_colCobertura] = _coberturas[i].DescCobertura;
                        row[_colCodCobertura] = _coberturas[i].CodCobertura;
                        
                        i--;
                    }
                }
            }

            return dt;
        }



        

        #endregion




    }
}
