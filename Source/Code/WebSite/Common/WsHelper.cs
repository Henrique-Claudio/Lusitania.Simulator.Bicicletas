using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using Lusitania.Simuladores.WebSite.WsPlanos;
using System.Net;
using System.Xml;
using System.IO;
using Lusitania.Simuladores.WebSite.Ws_auxiliar;
using Lusitania.Simuladores.WebSite.WsDocumentos;

namespace Lusitania.Simuladores.WebSite.Common
{
    //public delegate void PerformanceAnularValidacoesHandler(Hashtable arg1, ref int arg2, ref string arg3, string arg4, string arg5);
    //public delegate Pagamento PerformanceObtemDadosPagamentoHandler(decimal? arg1, string arg2, string arg3);
    //public delegate void PerformanceValidaIbanHandler(string arg1, ref string arg2, ref decimal? arg3, string arg4, string arg5);

    /// Common Webservice Helper class, this class should contain all Ws invocations
    /// </summary>
    public class WsHelper
    {
        /// <summary>
        /// Get the plan URL, of the given GenericPlanID
        /// </summary>        
        /// <param name="planId">FamilyPlanId or GenericPlanId</param>
        /// <param name="networkUser">Client credencial username Ex: ConfigurationManager.AppSettings["NetworkCredential_user"]</param>
        /// <param name="networkPass">Client credencial password Ex:  ConfigurationManager.AppSettings["NetworkCredential_pass"]</param>
        /// <returns>String containing the webpage plus the id (Ex: Http://{URL_Plan_Simulator}?PlanId={GenericPlan:ID}) </returns>
        public static string GetGenericPlanUrl(decimal? planId, string networkUser, string networkPass)
        {
            string url, codigoPlano, descricaoPlano;

            descricaoPlano = string.Empty;
            codigoPlano = string.Empty;
            descricaoPlano = string.Empty;

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((s, certificate, chain, sslPolicyErrors) => true);

            using (WSPlanosClient wsPlanos = new WSPlanosClient())
            {
                wsPlanos.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);

                //Obtem url do Plano
                url = wsPlanos.ObtemURLPlano(out codigoPlano, out descricaoPlano, planId.ToString());

                if (wsPlanos.State != System.ServiceModel.CommunicationState.Closed)
                    wsPlanos.Close();
            }

            return url;
        }

        /// <summary>
        /// Get key value pair month list
        /// </summary>
        /// <param name="networkUser">Client credencial username Ex: ConfigurationManager.AppSettings["NetworkCredential_user"]</param>
        /// <param name="networkPass">Client credencial password Ex:  ConfigurationManager.AppSettings["NetworkCredential_pass"]</param>
        /// <returns>Key value pair month list</returns>
        public static List<Mes> GetMonthList(string networkUser, string networkPass)
        {
            //return Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ListOperation<Mes>("ws_auxiliar.SaberMeses", (Func<string, string, List<Mes>>)PerformanceMonitor_GetMonthList, networkUser, networkPass);
            return PerformanceMonitor_GetMonthList(networkUser, networkPass);
        }

        private static List<Mes> PerformanceMonitor_GetMonthList(string networkUser, string networkPass)
        {
            List<Mes> meses = new List<Mes>();

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            using (WSAuxiliarClient ws_auxiliar = new WSAuxiliarClient())
            {
                ws_auxiliar.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);

                meses = ws_auxiliar.SaberMeses().ToList<Mes>();

                if (ws_auxiliar.State != System.ServiceModel.CommunicationState.Closed)
                    ws_auxiliar.Close();
            }

            return meses;
        }

        /// <summary>
        /// Get the next due date for the simulation
        /// </summary>        
        /// <param name="planCode">Code that identifies the plan type (Ex: Particular or Empresa)</param>
        /// <param name="startDate">Date when the policy is scheduled to begin</param>
        /// <param name="networkUser">Client credencial username Ex: ConfigurationManager.AppSettings["NetworkCredential_user"]</param>
        /// <param name="networkPass">Client credencial password Ex:  ConfigurationManager.AppSettings["NetworkCredential_pass"]</param>
        /// <returns>Due date string</returns>
        /// 
        public static string GetDueDate(string planCode, DateTime startDate, string networkUser, string networkPass)
        {
            //return (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("wsPlanos.GetProxVencimentoPlanos2", (Func<string, DateTime, string, string, string>)PerformanceMonitor_GetDueDate, planCode, startDate, networkUser, networkPass);
            return PerformanceMonitor_GetDueDate(planCode, startDate, networkUser, networkPass);
        }

        private static string PerformanceMonitor_GetDueDate(string planCode, DateTime startDate, string networkUser, string networkPass)
        {
            string dataVencimento = null;

            using (WSPlanosClient wsPlanos = new WSPlanosClient())
            {
                wsPlanos.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);


                dataVencimento = wsPlanos.GetProxVencimentoPlanos2(startDate, planCode);


                if (wsPlanos.State != System.ServiceModel.CommunicationState.Closed)
                    wsPlanos.Close();
            }
            return dataVencimento;
        }

        /// <summary>
        /// Verifies if the given Nif is in the blacklist, if so, a message is returned
        /// </summary>
        /// <param name="nif">The nif that will be verified</param>
        /// <param name="networkUser">Client credencial username Ex: ConfigurationManager.AppSettings["NetworkCredential_user"]</param>
        /// <param name="networkPass">Client credencial password Ex:  ConfigurationManager.AppSettings["NetworkCredential_pass"]</param>
        /// <returns>Validation message if nif in the black list, empty otherwise</returns>
        public static string ValidateNifAgainstBlackList(int nif, string networkUser, string networkPass)
        {
            //return (string)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("ws_auxiliar.Valida_ListaNegra_Nif", (Func<int, string, string, string>)PerformanceMonitor_ValidateNifAgainstBlackList, nif, networkUser, networkPass);
            return PerformanceMonitor_ValidateNifAgainstBlackList(nif, networkUser, networkPass);
        }
        private static string PerformanceMonitor_ValidateNifAgainstBlackList(int nif, string networkUser, string networkPass)
        {
            string validationMessage = null;

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((senderCallback, certificate, chain, sslPolicyErrors) => true);

            using (WSAuxiliarClient ws_auxiliar = new WSAuxiliarClient())
            {
                ws_auxiliar.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);

                validationMessage = ws_auxiliar.Valida_ListaNegra_Nif(nif);

                if (ws_auxiliar.State != System.ServiceModel.CommunicationState.Closed)
                    ws_auxiliar.Close();
            }

            return validationMessage;
        }

        /// <summary>
        /// Get the payment method currently selected by the user for the plan
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="networkUser">Client credencial username Ex: ConfigurationManager.AppSettings["NetworkCredential_user"]</param>
        /// <param name="networkPass">Client credencial password Ex:  ConfigurationManager.AppSettings["NetworkCredential_pass"]</param>
        /// <returns>Payment method</returns>
        /// 
        public static Pagamento GetPlanPaymentMethod(decimal? planId, string networkUser, string networkPass)
        {
            //object[] parameters = new object[] { planId, networkUser, networkPass };
            //Pagamento _pagam = Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.GenericObjectOperationByReference<Pagamento>("wsPlanos.ObtemDadosPagamento", (PerformanceObtemDadosPagamentoHandler)PerformanceMonitor_GetPlanPaymentMethod, false, ref parameters);
            //return _pagam;

            Pagamento _pagam = PerformanceMonitor_GetPlanPaymentMethod(planId, networkUser, networkPass);
            return _pagam;
        }

        private static Pagamento PerformanceMonitor_GetPlanPaymentMethod(decimal? planId, string networkUser, string networkPass)
        {
            Pagamento pagamento = null;

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((s, certificate, chain, sslPolicyErrors) => true);

            using (WSPlanosClient wsPlanos = new WSPlanosClient())
            {
                wsPlanos.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);

                //Obtem url do Plano
                pagamento = wsPlanos.ObtemDadosPagamento(planId.ToString());

                if (wsPlanos.State != System.ServiceModel.CommunicationState.Closed)
                    wsPlanos.Close();
            }
            return pagamento;
        }

        #region ----- validarAnulacoesRecentes -----
        /// <summary>
        /// Validar Anulacoes Recentes
        /// </summary>
        /// <param name="paramsXML">Parâmetros para o XML</param>
        /// <returns>XML com parâmetros para chamada na BD</returns>
        /// 

        public static void validarAnulacoesRecentes(Hashtable paramsXML, ref int error, ref string errorMessage
                                            , string networkUser, string networkPass)
        {
            //object[] parameters = new object[] { paramsXML, error, errorMessage, networkUser, networkPass };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("ws_auxiliar.validarAnulacoesRecentes", (PerformanceAnularValidacoesHandler)PerformanceMonitor_validarAnulacoesRecentes, ref parameters);

            PerformanceMonitor_validarAnulacoesRecentes(paramsXML, ref error, ref errorMessage, networkUser, networkPass);
        }
        public static void PerformanceMonitor_validarAnulacoesRecentes(Hashtable paramsXML, ref int error, ref string errorMessage
                                            , string networkUser, string networkPass)
        {

            string xml = string.Format(@"
<VALIDATIONS type='0'>
   <USER_DATA>
      <LOGIN>{0}</LOGIN>
   </USER_DATA>
   <CONTRACT_DATA>
      <SIM_ID>{1}</SIM_ID>
      <SIM_NUM>{2}</SIM_NUM>
      <PLAN_ID>{3}</PLAN_ID>
   </CONTRACT_DATA>
   <PERSONS>
      <PERSON type='T'>
         <CODE>{4}</CODE>
         <DOCUMENTS>
            <DOCUMENT type='NIF'>
               <CODE>{5}</CODE>
            </DOCUMENT>
         </DOCUMENTS>
      </PERSON>
   </PERSONS>
</VALIDATIONS>
", paramsXML["LOGIN"] as string
 , paramsXML["SIM_ID"] as string
 , paramsXML["SIM_NUM"] as string
 , paramsXML["PLAN_ID"] as string
 , paramsXML["HOLDER_CODE"] as string
 , paramsXML["HOLDER_NIF"] as string);

            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            using (WSAuxiliarClient ws_auxiliar = new WSAuxiliarClient())
            {
                ws_auxiliar.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);
                xml = ws_auxiliar.validarAnulacoesRecentes(xml);

                if (ws_auxiliar.State != System.ServiceModel.CommunicationState.Closed)
                    ws_auxiliar.Close();
            }

            if (!string.IsNullOrEmpty(xml))
            {
                readErrorsXML(xml, ref error, ref errorMessage);
            }
        }
        #endregion

        /// <summary>
        /// Lê Int32 de Outbound do XML
        /// </summary>
        /// <param name="pXMLNode">pXMLNode</param>
        public static Int32 readInt32FromXMLNode(XmlNode pXMLNode, string nodeName)
        {
            if (pXMLNode[nodeName] == null)
            {
                return new Int32();
            }
            else
            {
                return Int32.Parse(pXMLNode[nodeName].InnerText);
            }
        }

        /// <summary>
        /// Lê String de Outbound do XML
        /// </summary>
        /// <param name="pXMLNode">pXMLNode</param>
        public static string readStringFromXMLNode(XmlNode pXMLNode, string nodeName)
        {
            if (pXMLNode[nodeName] == null)
            {
                return string.Empty;
            }
            else
            {
                return pXMLNode[nodeName].InnerText;
            }
        }

        public static void readErrorsXML(string pXML, ref int pErrorCod, ref string pErrorDes)
        {
            XmlDocument doc;

            doc = new XmlDocument();
            doc.Load(new StringReader(pXML));

            XmlNode errorNode = doc.SelectSingleNode("//ERROS/ERRO");

            if (errorNode != null)
            {
                pErrorCod = readInt32FromXMLNode(errorNode, "COD");
                pErrorDes = readStringFromXMLNode(errorNode, "MSG");
            }
        }

        #region ----- ValidaIBAN_ObtemBIC -----
        /// <summary>
        /// Valida IBAN e Obtem BIC
        /// </summary>
        /// <param name="paramsXML">Parâmetros para o XML</param>
        /// <param name="paramsXML">Parâmetros para o XML</param>
        /// <param name="paramsXML">Parâmetros para o XML</param>
        /// 
        public static void ValidaIBAN_ObtemBIC(string iban, ref string bic, ref decimal? valido, string networkUser, string networkPass)
        { 
            //object[] parameters = new object[] {  iban, bic,  valido,  networkUser,  networkPass };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("ws_auxiliar.validarIBANObterBIC", (PerformanceValidaIbanHandler)PerformanceMonitor_ValidaIBAN_ObtemBIC, ref parameters);
            //bic = parameters[1] as string;
            //valido = parameters[2] as decimal?;

            PerformanceMonitor_ValidaIBAN_ObtemBIC(iban, ref bic, ref valido, networkUser, networkPass);
        }

        private static void PerformanceMonitor_ValidaIBAN_ObtemBIC(string iban, ref string bic, ref decimal? valido
                                             , string networkUser, string networkPass)
        {
            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            using (WSAuxiliarClient ws_auxiliar = new WSAuxiliarClient())
            {
                ws_auxiliar.ClientCredentials.Windows.ClientCredential = new NetworkCredential(networkUser, networkPass);
                ws_auxiliar.validarIBANObterBIC(iban, ref bic, ref valido);

                if (ws_auxiliar.State != System.ServiceModel.CommunicationState.Closed)
                    ws_auxiliar.Close();
            }
        }
        #endregion

        public static void insereOutputDocUploadOnlineXML(string xml, decimal tipoDoc, byte[] ficheiro, short companyCode)
        {
            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((senderCallback, certificate, chain, sslPolicyErrors) => true);

            using (WSDocumentosClient wsDocumentos = new WSDocumentosClient())
            {
                wsDocumentos.ClientCredentials.Windows.ClientCredential = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_user"]
                                                                                              , ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                wsDocumentos.insereOutputDocUploadOnlineXML(companyCode, xml, tipoDoc, ficheiro);

                if (wsDocumentos.State != System.ServiceModel.CommunicationState.Closed)
                    wsDocumentos.Close();
            }
        }

        public static void insereOutputDocUploadOnline(string guidDoc, decimal tipoDoc, byte[] ficheiro, short companyCode)
        {
            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((senderCallback, certificate, chain, sslPolicyErrors) => true);

            using (WSDocumentosClient wsDocumentos = new WSDocumentosClient())
            {
                wsDocumentos.ClientCredentials.Windows.ClientCredential = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_user"]
                                                                                              , ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                wsDocumentos.insereOutputDocUploadOnlineGuidDoc(companyCode, guidDoc, tipoDoc, ficheiro);

                if (wsDocumentos.State != System.ServiceModel.CommunicationState.Closed)
                    wsDocumentos.Close();
            }
        }

        public static string GetXMLQuotation(string simulationID, string recordName) {
            string result;
            //Aceita qualquer "certificate" (Para DEV e QUA)
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((s, certificate, chain, sslPolicyErrors) => true);

            using (WSAuxiliarClient ws_auxiliar = new WSAuxiliarClient())
            {
                ws_auxiliar.ClientCredentials.Windows.ClientCredential = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_user"], ConfigurationManager.AppSettings["NetworkCredential_pass"]);

                result = ws_auxiliar.Get_Xml(simulationID, recordName);

                if (ws_auxiliar.State != System.ServiceModel.CommunicationState.Closed)
                    ws_auxiliar.Close();
            }

            return result;
        }
    }
}