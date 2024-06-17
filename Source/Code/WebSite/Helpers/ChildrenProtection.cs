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

namespace Lusitania.Simuladores.WebSite.Helpers.ChildrenProtection
{
    public class Proposal
    {
 
        public static string GetInputXml(string sessionId, string user, string quotationId,
                                         string paymenType, string frequency, string personList_content,
                                         string intermediate, string collector, string simulatorCode,
                                         string notesText, string planoPagamentos)
        {
            if (string.IsNullOrEmpty(quotationId))
                return string.Empty;

            object[] parameters = { sessionId, user, quotationId,
                                    paymenType, frequency, personList_content,
                                    intermediate, collector, simulatorCode, notesText, planoPagamentos };

            string proposal = string.Format(@"<?xml version='1.0' encoding='utf-8'?>
                                                <proposal>
                                                  <sessionId>{0}</sessionId>
                                                  <channel>web</channel>
                                                  <user>{1}</user>
                                                  <simulatorCode>{8}</simulatorCode>
                                                  <input>
                                                    <quotationId>{2}</quotationId>
                                                    <paymenType>{3}</paymenType>
                                                    <frequency>{4}</frequency>
                                                    <personList>{5}</personList>
                                                    <intermediate>{6}</intermediate>
                                                    <collector>{7}</collector>
                                                    <obs>{9}</obs>
                                                    <planoPagamentos>{10}</planoPagamentos>
                                                  </input>
                                                </proposal>", parameters);

            return proposal;
        }

    }
}
