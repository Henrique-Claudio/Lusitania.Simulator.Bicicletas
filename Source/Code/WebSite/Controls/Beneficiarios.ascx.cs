using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simuladores.DataLayer.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class Beneficiarios : System.Web.UI.UserControl
    {
        #region Properties

        public bool HasLegalHeirs
        {
            get { return mHasLegalHeirsBox.SelectedValue == "S"; }
            set { mHasLegalHeirsBox.SelectedValue = value ? "S" : "N"; }
        }

        public bool HasDiscriminate
        {
            get { return mHasDiscriminate.SelectedValue == "1"; }
            set { mHasDiscriminate.SelectedValue = value ? "1" : "2"; }
        }

        public string Product
        {
            get { return (ViewState["Product"] != null) ? (string)ViewState["Product"] : string.Empty; }
            set { ViewState["Product"] = value; }
        }

        #endregion

        #region >>>> Page Events <<<<

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                mBicicletasHeirList.Product = Product;
                
                if (Product == UtilityG.GetValue(Products.LIGHT))
                {
                    mHasLegalHeirsBox.SelectedValue = "S";
                }
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {

            conDiscriminatePH.Visible = !string.IsNullOrEmpty(mHasLegalHeirsBox.SelectedValue) && !HasLegalHeirs;
            conClausulaPH.Visible = !string.IsNullOrEmpty(mHasDiscriminate.SelectedValue) && !HasDiscriminate && conDiscriminatePH.Visible;
            conHeirdeiros.Visible = !string.IsNullOrEmpty(mHasDiscriminate.SelectedValue) && HasDiscriminate && conDiscriminatePH.Visible;
            if (!conDiscriminatePH.Visible)
            {
                mHasDiscriminate.SelectedIndex = -1;
            }
            if (!conHeirdeiros.Visible)
            {
                mBicicletasHeirList.RemoveAllHeirs();
            }
            if (!conClausulaPH.Visible)
            {
                mClausulaGenerica.Text = string.Empty;
            }

            base.OnPreRender(e);
        }
        #endregion

        #region >>>> Helper Methods <<<<

        public void GetData(ref Proposta proposta)
        {
            proposta.HERDEIROS_LEGAIS = mHasLegalHeirsBox.SelectedValue;
            proposta.TIPO_BENEFICIARIO = mHasDiscriminate.SelectedValue;
            proposta.CLAUSULA_GENERICA = mClausulaGenerica.Text;
            mBicicletasHeirList.GetData(ref proposta);

        }

        #endregion

        #region >>>> Validação <<<<

        public void vlaidar(ref string msgErro)
        {

            if (string.IsNullOrEmpty(mHasLegalHeirsBox.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Seleccione se tem Herdeiros Legais.");
            }
            else if (mHasLegalHeirsBox.SelectedValue == "N")
            {
                if (string.IsNullOrEmpty(mHasDiscriminate.SelectedValue))
                {
                    BusinessHelper.AddErroAtList(ref msgErro, "Seleccione o Tipo Beneficiário.");
                }
                else if (mHasDiscriminate.SelectedValue == "1")
                {
                    mBicicletasHeirList.validar(ref msgErro);
                }
                else if (mHasDiscriminate.SelectedValue == "2")
                {
                    if (string.IsNullOrEmpty(mClausulaGenerica.Text))
                    {
                        BusinessHelper.AddErroAtList(ref msgErro, "Cláusula Genérica é Obrigatoria.");
                    }
                }
            }
        }

        #endregion

    }
}