using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer.Simulation;
using Lusitania.Simuladores.WebSite.Common;
using Lusitania.Simuladores.Common.OutputES;
using Resources;

namespace Lusitania.Simuladores.WebSite.Classes
{
    public class OutputsManagement
    {
        public static string GetXMLQuotation(decimal simulationID, bool sdd)
        {
            string recordName = Strings.RecordSetQuotation;
            /*if (sdd) {
                recordName = recordName + "_SDD";
            }*/
            string msgErro = string.Empty;
            SimulacaoDS.GravarXML_Cotacao(simulationID, ref msgErro);
            if (string.IsNullOrEmpty(msgErro))
            {
                return WsHelper.GetXMLQuotation(simulationID.ToString(), recordName);
            }
            else return null;
        }
        public static string GetXMLDocument(string recordName, List<String> parameters)
        {
            string xmlDocument = string.Empty;

            if (recordName.Equals(Strings.RecordSetQuotation))
            {
                int idSim;
                Int32.TryParse(parameters[0], out idSim);
                string hasSDD = parameters[1];
                xmlDocument = GetXMLQuotation(idSim, hasSDD.Equals("S"));
            }

            return xmlDocument;
        }

        public static BLOB GetLiveCycleResp(string xmlDocument, string recordName, string templateName, string contentRoot)
        {

            PDFOutputOptionsSpec spec = new PDFOutputOptionsSpec();
            RenderOptionsSpec renderOptions = new RenderOptionsSpec();
            renderOptions.cacheEnabled = true;

            spec.generateManyFiles = false;
            spec.lookAhead = 300;
            spec.recordName = "DOCUMENTO";

            byte[] xmlByteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(xmlDocument);

            //xml com os dados para produção do output
            BLOB inDataDoc = new BLOB();
            inDataDoc.binaryData = xmlByteArray;

            BLOB generatePDFmetaDataDoc = new BLOB();
            BLOB generatePDFresultDoc = new BLOB();

            OutputResult generatePDFresult = new OutputResult();

            OutputServiceService service = new OutputServiceService();
            service.Url = Settings.OutputES_Url.AbsoluteUri;

            BLOB outPDF = service.generatePDFOutput(TransformationFormat.PDF, templateName, contentRoot,
                spec, renderOptions, inDataDoc, out generatePDFmetaDataDoc, out generatePDFresultDoc, out generatePDFresult);

            return outPDF;
        }

        public static byte[] GetPDFBynaryData(string recordName, List<String> parameters
                                       , string templateName, string contentRoot)
        {
            string xmlDocument = GetXMLDocument(recordName, parameters);
            BLOB outPDF = GetLiveCycleResp(xmlDocument, recordName, templateName, contentRoot);
            return outPDF.binaryData;
        }

        public static Stream GetPDFStream(string recordName, List<String> parameters
                                       , string templateName, string contentRoot)
        {
            return new MemoryStream(GetPDFBynaryData(recordName, parameters, templateName, contentRoot));
        }
    }
}