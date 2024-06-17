using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Simuladores.WebSite.Controls;
using Resources;

namespace Lusitania.Simuladores.WebSite.Common
{
    public class SimulatorPage : MyPage
    {
        protected QuotationDetails quotationDetails;
        protected CommercialAccount commercialAccount;
        protected CreatedBy createdBy;

        private string proposalClickScript;

        protected string ProposalClickScript
        {
            get
            {
                return (proposalClickScript == null ? "Proposta();" : proposalClickScript);
            }

            set
            {
                proposalClickScript = value;
            }
        }
        
        protected virtual void btnProposal_Click(object sender, EventArgs e)
        {
            if (quotationDetails != null)
            {
                if (quotationDetails.HasValidNif().HasValue)
                {
                    if (quotationDetails.HasValidNif().Value)
                    {
                        ScriptManager.RegisterStartupScript(((Control)sender), GetType(), "proposal", ProposalClickScript, true);
                    }
                    else
                    {
                        ScriptHelper.ShowMessageBox(Strings.FiscalNumberIsNotValid);
                    }
                }
                else
                {
                    if (quotationDetails.SimulationNif.Length > 0)
                        ScriptHelper.ShowMessageBox("O NIF introduzido não é válido.");
                    else
                        ScriptHelper.ShowMessageBox("Para iniciar a proposta, deve indicar o NIF do Tomador.");
                }
            }
        }
    }
}
