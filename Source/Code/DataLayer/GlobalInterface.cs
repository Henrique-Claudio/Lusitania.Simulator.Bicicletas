using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Client;
using Lusitania.Simuladores.DataLayer.Properties;
using System.Data;
using Oracle.DataAccess.Types;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.Base;
using OSBConnector.Models;

namespace Lusitania.Simuladores.DataLayer
{
    public static class GlobalInterface
    {

        public static string OBTEM_BIC(string iban)
        {
            var codigoBanco = iban.Substring(4, 4);
            var bancos = OSBConnector.BDLeg.Financeira.DataService.Bancos.SaberBancos_V3_0(codigoBanco, String.Empty);
            if (bancos.Count == 1)
            {
                return bancos[0].BIC;
            }

            return String.Empty;

            //return (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("F_OBTEM_BIC", (Func<string, string>)PerformanceMonitor_OBTEM_BIC, iban);
        }


        //private static string PerformanceMonitor_OBTEM_BIC(string iban)
        //{
        //    string retVal = string.Empty;

        //    using (OracleConnection xConnection = new OracleConnection(Settings.Default.ConnectionString))
        //    using (OracleCommand xCommand = xConnection.CreateCommand())
        //    {
        //        try
        //        {
        //            xCommand.CommandType = CommandType.StoredProcedure;
        //            xCommand.CommandText = "F_OBTEM_BIC";

        //            //OUTPUT Função
        //            OracleParameter outParameter = xCommand.Parameters.Add("RESULT", OracleDbType.Varchar2, 11);//34 é o tamanho máximo de um IBAN na altura da criação
        //            outParameter.Direction = ParameterDirection.ReturnValue;

        //            //input
        //            OracleParameter Parameter = xCommand.Parameters.Add("A_NIB", OracleDbType.Varchar2, 34);//34 é o tamanho máximo de um IBAN na altura da criação
        //            Parameter.Value = iban;
        //            Parameter.Direction = ParameterDirection.Input;

        //            // execute
        //            xConnection.Open();
        //            xCommand.ExecuteNonQuery();
        //            if (!((Oracle.DataAccess.Types.OracleString)(outParameter.Value)).IsNull)
        //            {
        //                retVal = outParameter.Value.ToString();
        //            }
        //            xConnection.Close();


        //        }
        //        catch (Exception ex)
        //        {
        //            if (xConnection.State == ConnectionState.Open)
        //            {
        //                xConnection.Close();
        //            }
        //        }
        //    }

        //    return retVal;
        //}

        public static string saber_Bancos_ROWID(string rowID)
        {
            string retObj = string.Empty;
            List<Banco> bancos = OSBConnector.Shared.DataServices.SaberBancosROWID(rowID);
            if (bancos != null && bancos.Count > 0)
            {
                retObj = bancos[0].BIC;
            }
            return retObj;
        }

        /*
        public static string saber_Bancos_ROWID_Old(string rowID)
        {
            return (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("WEB_GERAL_PKG.saber_Bancos_ROWID", (Func<string, string>)PerformanceMonitor_saber_Bancos_ROWID_Old, rowID);
        }

        private static string PerformanceMonitor_saber_Bancos_ROWID_Old(string rowID)
        {

            List<OracleParameter> listIn = new List<OracleParameter>(){
                new OracleParameter("p_ROWID", OracleDbType.Varchar2, ParameterDirection.Input){
                    Value = rowID
                },

                new OracleParameter("p_cursor", OracleDbType.RefCursor, ParameterDirection.Output)
            };

            try
            {
                DataTable dt = (new DatabaseAccess()).CursorToDataTable(listIn, "WEB_GERAL_PKG.saber_Bancos_ROWID");
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["BIC"].ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        */

        public static string ObtemCentroCusto(string login)
        {
            string retVal = string.Empty;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VALIDA_WEB_PKG.OBTEM_CENTRO_CUSTO";

            //input
            cmd.Parameters.Add("A_LOGIN", OracleDbType.Varchar2, 8, login, ParameterDirection.Input);

            //Parametros OUT
            cmd.Parameters.Add("A_CENTRO", OracleDbType.Varchar2, 4).Direction = ParameterDirection.Output;

            try
            {
                DataHelper.GetFunctionValue(cmd);

                retVal = DataHelper.GetStringParameter(cmd.Parameters["A_CENTRO"]);
            }
            catch (Exception e)
            {
            }

            return retVal;
        }
    }
}