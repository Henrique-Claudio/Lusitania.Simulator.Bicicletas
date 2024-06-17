using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Lusitania.Simuladores.DataLayer;
using System.ComponentModel;
using System.Collections.Generic;
using Lusitania.Simuladores.DataLayer.Common;

namespace Lusitania.Simulador.WebCommon.Common
{
    //public delegate void PerformanceinsereDocumentoHandler(string arg1, string arg2, string arg3, decimal? arg4, byte[] arg5);

    public static class BusinessHelper
    {

        public static int getAge(DateTime bday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age)) age--;

            return age;
        }

        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static void AddErroAtList(ref string list, string erro)
        {
            AddErroAtList(ref list, erro, true);
        }
        public static void AddErroAtList(ref string list, string erro, bool hasTrace)
        {
            if (!string.IsNullOrEmpty(erro))
            {
                list += (!string.IsNullOrEmpty(list) ? "<br />" : "") + (hasTrace ? "- " : "") + erro;
            }
        }

        public static bool VALIDA_NIF_TEM_CCP_FE(string NIF)
        {
            string xResult;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_NIF_TEM_CCP_FE(NIF, out xResult);
            if (xResult == "S")

                return true;
            else
                return false;

        }

        public static bool IsContractStartDateValid(DateTime? iStartDate, out string oErrorMessage)
        {
            string xHasError;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_DATA_INICIO(iStartDate, out xHasError, out oErrorMessage);
            return String.IsNullOrEmpty(oErrorMessage);
        }


        /// <summary>
        /// Verifica se um dado nif é válido.
        /// </summary>
        /// <param name="iNif">O Nif a validar.</param>
        /// <returns>True se o Nif for válido, False se não o for.</returns>
        public static bool IsValidNif(string iNif)
        {
            return Functions.IsNifValid(iNif);
        }


        /// <summary>
        /// Asks the database if the user should be allowed to edit it's address, given it's country of origin
        /// </summary>
        /// <param name="iCountryCode"></param>
        /// <returns></returns>

        public static bool CanEditHolderAddressByCountry(string iCountryCode)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("WEB_PPROD_GERAL_PKG.PODE_ALTERAR_MORADA", (Func<string, string, bool>)PerformanceMonitor_CanEditHolderAddressByCountry, UserHelper.UserName, iCountryCode);
            return PerformanceMonitor_CanEditHolderAddressByCountry(UserHelper.UserName, iCountryCode);
        }

        private static bool PerformanceMonitor_CanEditHolderAddressByCountry(string UserName, string iCountryCode)
        {
            decimal? xResult;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).PODE_ALTERAR_MORADA(UserName, iCountryCode, out xResult);
            return xResult.HasValue && xResult.Value == 1;
        }


        ///// <summary>
        ///// Queries the database for the name of a Mediator, given its user name.
        ///// </summary>
        ///// <param name="iUserName">The UserName of the Mediator.</param>
        ///// <returns>Null if the Mediator was not found. The Name of the Mediator otherwise.</returns>
        //public static string GetMediatorName(string iUserName, out string oErrorMessage)
        //{
        //    //string xMediatorName;
        //    //(new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapterFix()).VALIDA_MEDIADOR(iUserName, out xMediatorName, out oErrorMessage);
        //    //return xMediatorName;
        //    return DataHelper.GetMediatorName(iUserName, out oErrorMessage);
        //}

        ///// <summary>
        ///// Queries the database for the name of a Collector, given its user name.
        ///// </summary>
        ///// <param name="iUserName">The UserName of the Collector.</param>
        ///// <returns>Null if the Collector was not found. The Name of the Collector otherwise.</returns>
        //public static string GetCollectorName(string iUserName, string login, out string oErrorMessage)
        //{
        //    string xCollectorName;
        //    (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapterFix()).VALIDA_COBRADOR(iUserName, login, out xCollectorName, out oErrorMessage);
        //    return xCollectorName;
        //}




        public static bool CanEmitDocuments(string login, int simulatorCode, decimal simulationId)
        {
            return BusinessHelperDS.VALIDA_CONTRATO_EMISSAO_DOC(login, simulatorCode, simulationId);
        }

        public static bool CanPayLaterAnContract(int simulatorCode, decimal simulationId, string planCode)
        {
            return BusinessHelperDS.PODE_EFECTUAR_PAGAM_POSTERIOR(simulatorCode, simulationId, planCode);
        }

        public static bool IsEnterpriseNif(string nif)
        {
            //return (bool)Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperation("WEB_GERAL_PKG.SABER_NIF_EMPRESA", (Func<string, bool>)PerformanceMonitor_IsEnterpriseNif,  nif);
            return PerformanceMonitor_IsEnterpriseNif(nif);
        }

        public static bool PerformanceMonitor_IsEnterpriseNif(string nif)
        {
            decimal? result;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).SABER_NIF_EMPRESA(nif, out result);
            return result.GetValueOrDefault(0) == 1;
        }

        public static decimal? CheckSimPlanIntegration(decimal? simulationId)
        {
            decimal? result;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_INTEGRACAO_COM_PLANO(simulationId, out result);
            return result;
        }

        public static bool IsLoginAgent(string login)
        {
            string errorMsg;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).VALIDA_LOGIN_AGENTE(login, out errorMsg);
            return String.IsNullOrEmpty(errorMsg);
        }

        public static string getDependencyInternalAccount(string login)
        {
            return OSBConnector.Shared.DataServices.GetUserAttributes(login).Attributes.Attribute.Find(x => x.Key == "lusitania.util.conta").Value;
        }

        /*
        public static string getDependencyInternalAccount_Old(string login)
        {
            string errorMsg;
            string account;
            (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).CONTA_INTERNA_DEPENDENCIA(login, out account, out errorMsg);
            return account;
        }
        */

        public static void saveRenderedDocument(string codRamo, string idDoc, string idDocType, decimal? docType, byte[] renderedDoc)
        {
            //object[] parameters = new object[] { codRamo, idDoc, idDocType, docType, renderedDoc };
            //Lusitania.PerformanceMonitor.Modules.PerformanceMonitor.Main.ObjectOperationByReference("WEB_SIM_DOCUMENTOS_PKG.INSERE_DOCUMENTO", (PerformanceinsereDocumentoHandler)PerformanceMonitor_saveRenderedDocument, ref parameters);
            PerformanceMonitor_saveRenderedDocument(codRamo, idDoc, idDocType, docType, renderedDoc);
        }
        private static void PerformanceMonitor_saveRenderedDocument(string codRamo, string idDoc, string idDocType, decimal? docType, byte[] renderedDoc)
        {
            (new Lusitania.Simuladores.DataLayer.DocumentsDSTableAdapters.QueriesTableAdapter()).INSERE_DOCUMENTO(codRamo, idDoc, docType, renderedDoc);
        }

        public static void saveRenderedDocument(int idSimulacao, decimal? docType, byte[] renderedDoc)
        {
            (new Lusitania.Simuladores.DataLayer.DocumentsDSTableAdapters.QueriesTableAdapter()).INSERE_DOCUMENTO_2(idSimulacao, docType, renderedDoc);
        }

        public static void saveDocUpload(string guid_doc, decimal? docType, byte[] renderedDoc)
        {
            (new Lusitania.Simuladores.DataLayer.DocumentsDSTableAdapters.QueriesTableAdapter()).INSERE_OUTPUT_DOCUPLOAD(guid_doc, docType, renderedDoc);
        }
    }

}