using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simulador.WebCommon.Common;





namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class RGPD : System.Web.UI.UserControl
    {


        #region ----- Propriedades -----
        /// <summary>
        /// 
        /// </summary>

        public string isRGPD_VALUE()
        {
            if (mRGPD.SelectedIndex < 0)
                return "";
            else
                return mRGPD.SelectedValue;
        }

        #endregion

        #region >>>> Page events <<<<
        
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                div_RGPD.Visible = true;
            }
            catch (Exception)
            {
            }

        }

        #endregion

        #region >>> Validação <<<

        public void validar(ref string msgErro)
        {

            if (string.IsNullOrEmpty(mRGPD.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Indique se autoriza o Tratamento de dados pessoais.");
            }

        }

        #endregion

        #region >>> Helper Methods <<<

        
        #endregion

    }
}