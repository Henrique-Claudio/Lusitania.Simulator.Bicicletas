using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.Common;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;


namespace Lusitania.Simulador.WebCommon.Common
{
    public static class MyHelper
    {
        public static DataTable ToTable(IDataReader iReader)
        {
            DataTable xTable = new DataTable();
            xTable.Locale = Thread.CurrentThread.CurrentCulture;
            xTable.Load(iReader);
            return xTable;
        }

        public static void FillTable(OracleCommand iCommand, DataTable iTable, params DataColumnMapping[] iMappings)
        {
            using (OracleDataReader xReader = iCommand.ExecuteReader())
            {
                while (xReader.Read())
                {
                    DataRow xRow = iTable.NewRow();

                    foreach (DataColumnMapping xMapping in iMappings)
                    {
                        xRow[xMapping.DataSetColumn] = xReader[xMapping.SourceColumn];
                    }

                    iTable.Rows.Add(xRow);
                }
            }

            iTable.AcceptChanges();
        }

        /// <summary>
        /// Tests if a string is null or trimmed-empty and returns it if not or a dbnull if it is.
        /// </summary>
        /// <param name="iSource">the string to test.</param>
        /// <returns>The string, if it has something, or a dbnull if not.</returns>
        public static object ValueOrDbNull(string iSource)
        {
            if (iSource == null || iSource.Trim().Length == 0)
            {
                return DBNull.Value;
            }
            else
            {
                return iSource.Trim();
            }
        }

        /// <summary>
        /// Tests if a given nullable type has a value and returns its value if it does, or a dbnull if not.
        /// </summary>
        /// <typeparam name="T">The underlaying type of the target nullable type.</typeparam>
        /// <param name="iSource">The nullable value to be tested.</param>
        /// <returns>The source's value if possible, or a dbnull if not.</returns>
        public static object ValueOrDbNull<T>(T? iSource) where T : struct
        {
            if (iSource.HasValue)
            {
                return iSource.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// Tests if a given nullable boolean has a value and returns its corresponding typed value, or dbnull if not.
        /// </summary>
        /// <param name="iValue">The nullable boolean to be tested.</param>
        /// <param name="iTrueValue">The corresponding true value.</param>
        /// <param name="iFalseValue">The corresponding false value.</param>
        /// <returns>The corresponding typed value if possible, otherwise DBNull.</returns>
        public static object ValueOrDbNull<T>(bool? iValue, T iTrueValue, T iFalseValue)
        {
            if (iValue.HasValue)
            {
                return (iValue.Value ? iTrueValue : iFalseValue);
            }
            else
            {
                return DBNull.Value;
            }
        }

        public static void RemoveRows(DataTable iTable, Comparison<DataRow> iComparison)
        {
            List<DataRow> xRowsToDelete = new List<DataRow>();

            for (int xRowIndex = 1; xRowIndex < iTable.Rows.Count - 1; ++xRowIndex)
            {
                for (int xSubRowIndex = xRowIndex + 1; xSubRowIndex < iTable.Rows.Count; ++xSubRowIndex)
                {
                    DataRow xRow = iTable.Rows[xRowIndex];
                    DataRow xSubRow = iTable.Rows[xSubRowIndex];

                    if (iComparison(xRow, xSubRow) == 0)
                    {
                        xRowsToDelete.Add(xSubRow);
                        break;
                    }
                }
            }

            foreach (DataRow xRowToDelete in xRowsToDelete)
            {
                iTable.Rows.Remove(xRowToDelete);
            }
        }

        public static string LoginNameOnly(string iLogin)
        {
            if (iLogin != null && iLogin.Contains("\\"))
            {
                string[] xParts = iLogin.Split('\\');
                iLogin = xParts[xParts.Length - 1];
            }
            
            //martelada para COMMON
            if (iLogin == "ncms" || iLogin == "casimoes" || iLogin =="alicemartins")
                iLogin = "rfaccb";
            
            return iLogin;
        }

        public static void CompensateOracleParameterBug(IEnumerable<IDbCommand> iCommands)
        {
            foreach (IDbCommand xCommand in iCommands)
            {
                foreach (IDbDataParameter xParameter in xCommand.Parameters)
                {
                    DbType xDbType = xParameter.DbType;
                    xParameter.DbType = DbType.Object;
                    xParameter.DbType = xDbType;
                }
            }
        }

        public static string IgnorePrepositions(string str)
        { 
            str = str.Replace(" de ","%")
                        .Replace(" da ","%")
                        .Replace(" do ","%")
                        .Replace(" das ", "%")
                        .Replace(" dos ", "%")
                        .Replace(" em ","%")
                        .Replace(" a ","%")
                        .Replace(" e ","%")
                        .Replace(" o ","%");

            if (str.StartsWith("a ") || str.StartsWith("e ") || str.StartsWith("o "))
                str = str.Substring(2);
            else if (str.StartsWith("de ") || str.StartsWith("da ") || str.StartsWith("do ") || str.StartsWith("em "))
                str = str.Substring(3);
            else if (str.StartsWith("das ") || str.StartsWith("dos "))
                str = str.Substring(4);

            if (str.EndsWith(" a") || str.EndsWith(" e") || str.EndsWith(" o"))
                str = str.Remove(str.Length - 2);
            else if (str.EndsWith(" de") || str.EndsWith(" da") || str.EndsWith(" do") || str.EndsWith(" em"))
                str = str.Remove(str.Length - 3);
            else if (str.EndsWith(" das") || str.EndsWith(" dos"))
                str = str.Remove(str.Length - 4);

            return str.Replace(" ", "%");
        }

    }
}
