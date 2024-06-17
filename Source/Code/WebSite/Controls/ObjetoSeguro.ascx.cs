using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simulador.WebCommon.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class ObjetoSeguro : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        #region >>> Helper Methods <<<

        public void GetData(ref Proposta proposta)
        {
            proposta.InsuranceObject = new InsuranceObject();
            //proposta.InsuranceObject = new InsuranceObject(mAnoAquisicao.Text, mMarca.Text, mModelo.Text, mVersao.Text, mNrQuadro.Text);
        }

        #endregion

        #region >>> Validação <<<

        //public void validar(ref string msgErro)
        //{

        //    if (string.IsNullOrEmpty(mAnoAquisicao.Text))
        //    {
        //        BusinessHelper.AddErroAtList(ref msgErro, "O campo Ano Construção é de preenchimento obrigatório");
        //    }

        //    if (string.IsNullOrEmpty(mMarca.Text))
        //    {
        //        BusinessHelper.AddErroAtList(ref msgErro, "O campo Marca é de preenchimento obrigatório");
        //    }
        //    if (string.IsNullOrEmpty(mModelo.Text))
        //    {
        //        BusinessHelper.AddErroAtList(ref msgErro, "O campo Modelo é de preenchimento obrigatório");
        //    }
        //    if (string.IsNullOrEmpty(mVersao.Text))
        //    {
        //        BusinessHelper.AddErroAtList(ref msgErro, "O campo Versão é de preenchimento obrigatório");
        //    }
            
        //}

        #endregion
    }
}