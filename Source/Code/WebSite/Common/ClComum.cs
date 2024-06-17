using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lusitania.Simuladores.WebSite.WSLUSNETConfig;

namespace Lusitania.Simulador.WebCommon.Common
{
    public class ClComum
    {

        #region ----- Construtor -----

        /// <summary>
        /// Construtor
        /// </summary>
        public ClComum()
        {
        }

        #endregion

        #region ----- Constantes -----

        // Código da Lusitania
        public static readonly Int16 _cstCodigoLusitania = 1026;

        #endregion

        #region ----- Variáveis de instância -----

        // Objectos
        private static Config _objConfig;

        // Variáveis locais
        private static String _strSmtpServer = String.Empty;

        #endregion

        #region ----- Propriedades -----

        #region ----- pObjConfig -----

        /// <summary>
        /// Objecto Config
        /// </summary>
        private static Config pObjConfig
        {
            get
            {
                if (_objConfig == null)
                {
                    _objConfig = new Config();
                }
                return _objConfig;
            }
        }

        #endregion

        #endregion

        #region ----- Smtp Server -----

        /// <summary>
        /// Obter Smtp Server
        /// </summary>
        /// <returns>Url da Css de Print</returns>
        public static String obterSmtpServer()
        {
            if (_strSmtpServer == String.Empty)
            {
                // Obter valor constante

                _strSmtpServer = pObjConfig.obterValorSetting(_cstCodigoLusitania,
                                                                "EMAIL",
                                                                "SMTPServer");
            }

            return _strSmtpServer;
        }

        #endregion

    }
}