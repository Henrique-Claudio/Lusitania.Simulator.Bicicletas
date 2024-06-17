using System;
using System.Configuration;

namespace Lusitania.Simuladores.Common.Auto
{

    /// <summary>
    /// Summary description for dbGeral
    /// </summary>
    public class dbGeral
    {

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["DSN_LUS_SIM"];
            }
        }
    }
}
