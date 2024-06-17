using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;
using Lusitania.Simuladores.DataLayer.Simulation.Info;
using System.Web;

namespace Lusitania.Simuladores.DataLayer.Simulation
{
    public class CONDICAO_ESPECIAL_030
    {
        public List<CondicaoEspecial> Portugal {
            get {

                return getCE("Portugal");
            }
        }

        public List<CondicaoEspecial> Espanha
        {
            get
            {

                return getCE("Espanha");
            }
        }

        public DataTable dt_Portugal
        {
            get
            {

                return ReadDataFromExcel("Portugal");
            }
        }

        public DataTable dt_Espanha
        {
            get
            {

                return ReadDataFromExcel("Espanha");
            }
        }


        private List<CondicaoEspecial> getCE(string workSheetName) {

            List<CondicaoEspecial> listCE = ReadDataFromExcel(workSheetName).AsEnumerable()
               .Select(x =>
                   new CondicaoEspecial()
                   {
                       Ordem = x.Field<string>("Ordem"),
                       Nivel = x.Field<string>("Nivel"),
                       Nome = x.Field<string>("Nome"),
                       LimitMax = x.Field<string>("Limit Max")
                   }
           ).ToList();

            return listCE;

        }

        private DataTable ReadDataFromExcel(string workSheetName)
        {
            string fileName = HttpContext.Current.Server.MapPath("~/Documents/CONDICAO_ESPECIAL_030.xlsx");
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", fileName);
            
            OleDbConnection con = new OleDbConnection(connectionString);

            try
            {
                con.Open();
                var adapter = new OleDbDataAdapter("SELECT * FROM [" + workSheetName + "$]", con);
                var ds = new DataSet();

                adapter.Fill(ds, workSheetName);

                return ds.Tables[workSheetName];
               

            }
            catch (Exception ex)
            {

                throw ex;                

            }finally
            {
                con.Dispose();
            }
        }
    }
}
