using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Lusitania.Simuladores.Common.Auto
{

    /// <summary>
    /// Summary description for webGeral
    /// </summary>
    public class webGeral
    {
        public webGeral()
        {

        }

        public static string msgJavaScript(string strMensagem)
        {
            strMensagem = strMensagem.Replace("\n", " ");
            strMensagem = strMensagem.Replace("\"", "");

            return String.Format("<SCRIPT language=javascript>var oldEvt=window.onload;window.onload=function(){{if (oldEvt) oldEvt(); alert(\"{0}\");}}</SCRIPT>", strMensagem);
        }

        /// <summary>
        /// Executa uma ou mais funções de javascript depois de carregar a página
        /// </summary>
        /// <param name="strNomeFuncoes">Passar aqui o nome das funções a executar.Separar por ;</param>
        /// <returns></returns>
        public static string functionJavaScript(string strNomeFuncoes)
        {
            return String.Format("\n<SCRIPT language=javascript>{0}</SCRIPT>", strNomeFuncoes);
        }
    }
}
