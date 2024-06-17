using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.Common.OutputES;
using Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;
using Lusitania.Simuladores.WebSite.Ws_auxiliar;
using Lusitania.Utilities;

using Lusitania.Simuladores.WebSite.WsDocumentos;
using System.Collections;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Configuration;
using System.Xml;
using Lusitania.Simuladores.DataLayer.Simulation;
using Lusitania.Simuladores.DataLayer.Proposal;
using Lusitania.Simuladores.WebSite.Classes;

using Resources;

namespace Lusitania.Simuladores.WebSite.Pages
{
    public partial class OutputES_PDF : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            string simulation_aux = Convert.ToString(Request.QueryString["ID"]);

            string templateName = Request.QueryString["templateName"];	            // "CondicoesP.xdp";
            string recordName = Request.QueryString["recordName"];		            // "CondicoesP";
            int SimulationId;                                                       // SimulationId;

            SimulationId = Convert.ToInt32(simulation_aux.Replace("_SDD", ""));     //Remove SDD

            string RamoId = "";	// RamoId;
            string ApoliceId = "";	// ApoliceId;
            string ReciboId = "";	// ReciboId;

            if (recordName == "AVISOS_RECIBO")
            {
                RamoId = Request.QueryString["Ramo"];	// RamoId;
                ApoliceId = Request.QueryString["Apolice"];	// ApoliceId;
                ReciboId = Request.QueryString["Recibo"];	// ReciboId;
            }

            if (recordName == "PPAGAMENTOS")
            {
                RamoId = Request.QueryString["Ramo"];	// RamoId;
                ApoliceId = Request.QueryString["Apolice"];	// ApoliceId;
            }

            int docType = 0;
            try
            {
                docType = int.Parse(Request.QueryString["docType"]);	// 77;185 
            }
            catch
            {
                docType = 0;
            }

            //const 
            string contentRoot = ConfigurationManager.AppSettings["OutputES.OutputServiceDir"];//@"C:\Adobe\jboss\FORMS\";

            PDFOutputOptionsSpec spec = new PDFOutputOptionsSpec();

            spec.generateManyFiles = false;
            spec.lookAhead = 300;
            spec.recordName = 
                "DOCUMENTO";//Todos os recordnames passaram a DOCUMENTO no novo LyveCycle//
                //recordName;

            RenderOptionsSpec renderOptions = new RenderOptionsSpec();
            renderOptions.cacheEnabled = true;

            String result = String.Empty;
            String url = String.Empty;
            String processMode = String.Empty;
            String filename = String.Empty;
            String rec = String.Empty;

            if (recordName == Strings.RecordSetQuotation)
            {
                simulation_aux = simulation_aux.Replace("_SDD", "");

                bool hasSDD = false;
                if (Convert.ToString(Request.QueryString["ID"]).Length != simulation_aux.Length)
                    hasSDD = true;

                result = OutputsManagement.GetXMLQuotation(SimulationId, hasSDD);
            }
            else if (recordName == "PROP_BIKE")
            {                
                string _msgErro = "";
                result = (new PropostaDS()).GravarXml_Proposta(SimulationId, ref _msgErro);

                try
                {
                    string auxRGPD = "";
                    int startNIF = 0;
                    int endNIF = 0;

                    startNIF = result.IndexOf("<NIF>") + 5;
                    endNIF = result.IndexOf("</NIF>") - result.IndexOf("<NIF>") - 5;
                    auxRGPD = "Não";
                    if (GetRGPD("", result.Substring(startNIF, endNIF)))
                        auxRGPD = "Sim";

                    result = result.Replace("<AUTORIZACAO_USO_DADOS><RGPD>#</RGPD></AUTORIZACAO_USO_DADOS>", "<AUTORIZACAO_USO_DADOS><RGPD>" + auxRGPD + "</RGPD></AUTORIZACAO_USO_DADOS>");
                }
                catch (Exception)
                {
                }

            }
            else if (recordName == "RECIBO")
            {
                RamoId = Request.QueryString["Ramo"];	// RamoId;
                ApoliceId = Request.QueryString["Apolice"];	// ApoliceId;
                ReciboId = Request.QueryString["Recibo"];
                (new DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).LE_XML_RECIBO_ONLINE(RamoId, ApoliceId, ReciboId, UserHelper.UserName, out result);
            }
            else if (recordName == "FATURA_RECIBO")
            {
                RamoId = Request.QueryString["Ramo"];	// RamoId;
                ApoliceId = Request.QueryString["Apolice"];	// ApoliceId;
                ReciboId = Request.QueryString["Recibo"];
                (new DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).LE_XML_FATURA_RECIBO(RamoId, ApoliceId, ReciboId, UserHelper.UserName, out result, out filename, out url, out rec, processMode);
            }
            else if (recordName == "AVISOS_RECIBO")
            {
                (new DataLayer.GlobalDSTableAdapters.QueriesTableAdapter()).LE_XML_AVISO(RamoId, ApoliceId, ReciboId, out result);                
            }            
            else
            {
                (new QueriesTableAdapter()).LE_XML(SimulationId, out result);
            }

            if (string.IsNullOrEmpty(result))
            {
                ScriptHelper.ShowMessageBox(Strings.SapIsNOK, "Simulador", ScriptHelper.AlertType.ERROR);
                return;
            }  

            byte[] docLiveCycle = ChamaLiveCycle(templateName, result);

            //  77 - Apólice
            //  130 - Aviso de Cobrança
            //  131 - Recibo
            // 338 - Fatura Recibo
            if (docType == 77 || docType == 131 || docType == 130 || docType == 167 || docType == 338)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(result);
                string GUID_DOC = string.Empty;

                if (docType == 131)
                {//Para já estamos a colocar o recibo do Cliente mais o Recibo com Comissões no document type RECIBO(S) DE PRÉMIO OU ESTORNO e o Recibo do Cliente no document type RECIBO(S) DE PRÉMIO OU ESTORNO COM COMISSÕES
                    string doc131 = RemoveDocumentNodeIdx(result, 2);
                    GUID_DOC = GetGuidDoc(doc131);
                    byte[] doc131LiveCycle = ChamaLiveCycle(templateName, result);
                    //indexDocument(doc131, docType, docLiveCycle);
                    indexDocumentGuiDoc(GUID_DOC, docType, doc131LiveCycle);

                    //Reset do GuidDoc
                    GUID_DOC = string.Empty;
                    string doc347 = RemoveDocumentNodeIdx(result, 1);
                    GUID_DOC = GetGuidDoc(doc347);
                    byte[] doc347LiveCycle = ChamaLiveCycle(templateName, doc131);
                    indexDocumentGuiDoc(GUID_DOC, 347, doc347LiveCycle);

                    string reciboSimpl = Request.QueryString["reciboSimpl"];
                    if (!string.IsNullOrEmpty(reciboSimpl) && reciboSimpl.Equals("S"))//coloca para impressão de ecrã o documento 347--131
                        docLiveCycle = doc347LiveCycle;
                }
                else
                {
                    GUID_DOC = GetGuidDoc(result);
                    indexDocument(result, docType, docLiveCycle);
                }
            }

			// Enviar para o Browser
            SendToScreen(docLiveCycle);
        }

        private string GetGuidDoc(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            return doc.SelectSingleNode("JOB/DOCUMENTO/METADADOS/GUID_DOC").InnerText;
        }

        private void indexDocument(string xmlDoc, decimal docType, byte[] docLiveCycle)
        {
            try
            {
                Lusitania.Simuladores.WebSite.Common.WsHelper.insereOutputDocUploadOnlineXML(xmlDoc, docType, docLiveCycle, 1026);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        private void indexDocumentGuiDoc(string guiDoc, decimal docType, byte[] docLiveCycle)
        {
            try
            {
                Lusitania.Simuladores.WebSite.Common.WsHelper.insereOutputDocUploadOnline(guiDoc, docType, docLiveCycle, 1026);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

        private string RemoveDocumentNodeIdx(string xmlTotal, int idxDocument)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlTotal);

            XmlNodeList auxListNode = xmlDoc.SelectNodes("//JOB/DOCUMENTO[" + idxDocument + "]");
            if (auxListNode.Count > 0)
            {
                auxListNode[0].ParentNode.RemoveChild(auxListNode[0]);
            }

            return xmlDoc.OuterXml;
        }

        private byte[] ChamaLiveCycle(string templateName, string docXML)
        {
            byte[] xmlByteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(docXML);

            string contentRoot = ConfigurationManager.AppSettings["OutputES.OutputServiceDir"];//@"C:\Adobe\jboss\FORMS\";

            PDFOutputOptionsSpec spec = new PDFOutputOptionsSpec();

            spec.generateManyFiles = false;
            spec.lookAhead = 300;
            spec.recordName = "DOCUMENTO";//Todos os recordnames passaram a DOCUMENTO no novo LyveCycle//recordName;

            RenderOptionsSpec renderOptions = new RenderOptionsSpec();
            renderOptions.cacheEnabled = true;

            BLOB inDataDoc = new BLOB();
            inDataDoc.binaryData = xmlByteArray;

            BLOB generatePDFmetaDataDoc = new BLOB();
            BLOB generatePDFresultDoc = new BLOB();

            OutputResult generatePDFresult = new OutputResult();

            OutputServiceService service = new OutputServiceService();
            service.Url = Settings.OutputES_Url.AbsoluteUri;

            BLOB outPDF = service.generatePDFOutput(TransformationFormat.PDF, templateName, contentRoot,
                spec, renderOptions, inDataDoc, out generatePDFmetaDataDoc, out generatePDFresultDoc, out generatePDFresult);

            return outPDF.binaryData;
        }

        private void SendToScreen(byte[] docBytes)
        {
            ClDownload.enviarFicheiroBrowser(Page,
                                             docBytes,
                                             @"application/pdf",
                                             "Documento.pdf",
                                             ClDownload.EnviarFicheiroBrowserTipoFicheiro.Inline);
        }

        private static BLOB GetXML(string xmlfilename)
        {
            BLOB inDataDoc = new BLOB();

            FileStream stmRead = new FileStream(xmlfilename, FileMode.Open);
            BinaryReader bnr = new BinaryReader(stmRead);
            inDataDoc.binaryData = bnr.ReadBytes((int)stmRead.Length);

            bnr.Close();
            stmRead.Close();

            return inDataDoc;
        }

        private bool GetRGPD(string PESSOA, string NIF)
        {
            bool response = false;

            string URLAuth = "";
            const string contentType = "application/json";

            try
            {

                URLAuth = ConfigurationManager.AppSettings["RGPD_url"] + "GetPessoaRGPD?Pessoa=" + PESSOA + "&NIF=" + NIF;
                System.Net.ServicePointManager.Expect100Continue = false;

                HttpWebRequest webRequest = WebRequest.Create(URLAuth) as HttpWebRequest;
                //webRequest.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["NetworkCredential_user"], ConfigurationManager.AppSettings["NetworkCredential_pass"]);
                SetBasicAuthHeader(webRequest, ConfigurationManager.AppSettings["NetworkCredential_user_RGPD"], ConfigurationManager.AppSettings["NetworkCredential_pass_RGPD"]);
                webRequest.Method = "GET";
                webRequest.ContentType = contentType;

                using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                {
                    string responseData = responseReader.ReadToEnd();

                    if (responseData != "[]")
                    {

                        string CONSENTIMENTO = "";
                        int indexCONSENTIMENTO = 0;

                        indexCONSENTIMENTO = responseData.IndexOf("CONSENTIMENTO\":");

                        CONSENTIMENTO = responseData.Substring(indexCONSENTIMENTO + 16, 1);

                        if (CONSENTIMENTO == "N")
                            response = false;
                        else if (CONSENTIMENTO == "A")
                            response = true;

                    }

                    webRequest.GetResponse().Close();
                }

                return response;
            }
            catch
            {
                return response;
            }
        }

        public void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }


    }
}
