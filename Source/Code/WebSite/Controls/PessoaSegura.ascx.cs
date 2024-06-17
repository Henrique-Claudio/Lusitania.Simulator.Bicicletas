using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lusitania.Simuladores.DataLayer.Common.Model;

using Lusitania.Simuladores.DataLayer.Proposal.POCO;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer.Common;


namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class PessoaSegura : System.Web.UI.UserControl
    {
        #region >>>> Properties <<<<
        public string Product
        {
            get { return (ViewState["Product"] != null) ? (string)ViewState["Product"] : string.Empty; }
            set { ViewState["Product"] = value; }
        }

        

        public bool IsPSIgualTomador
        {
            get { return mPS_Is_Tomador.SelectedValue == "S"; }
            
        }

        public DateTime? DataNascimento {
            get { return mPessoaSegura.DateOfBirth; }
        }

        public string NIF
        {
            get { return mPessoaSegura.Nif; }
        }

        public int Age
        {
            get { return (ViewState["Age"] as int?).GetValueOrDefault(0); }
            set { ViewState["Age"] = value; }
        }
        #endregion

        #region >>>> Page Events <<<<
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                conBeneficiarios.Visible = Product != UtilityG.GetValue(Products.LIGHT); 
                mBeneficiarios.Product = Product;

                mPessoaSegura.pPersonType = "2";
                if (Age < 18)
                {
                    mPS_Is_Tomador.SelectedValue = "N";
                    mPS_Is_Tomador.Enabled = false;
                    conPessoaSegura.Visible = true;
                }
                
                UtilityG.BindList(mParentesco, GenericListsDS.ListaParentesco(Product), true, "Seleccione o Parentesco");
            }
        }
        #endregion

        #region >>>> Elements Events <<<<
        protected void mPS_Is_Tomador_SelectedIndexChanged(object sender, EventArgs e)
        {
            conPessoaSegura.Visible = mPS_Is_Tomador.SelectedValue == "N";
            if (!conPessoaSegura.Visible)
            {
                mPessoaSegura.ClearAllFields();
            }
        }

        #endregion

        #region >>>> Validação <<<<

        public void validar(ref string msgErro)
        {
            if (string.IsNullOrEmpty(mPS_Is_Tomador.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Seleccione se Pessoa Segura é o Tomador.");
            }
            else if (mPS_Is_Tomador.SelectedValue == "N")
            {
                mPessoaSegura.validar(ref msgErro);

                if (string.IsNullOrEmpty(mParentesco.SelectedValue))
                {
                    BusinessHelper.AddErroAtList(ref msgErro, "Seleccione o Parentesco.");
                }
            }

            if (string.IsNullOrEmpty(mDadosConfideciais.SelectedValue))
            {
                BusinessHelper.AddErroAtList(ref msgErro, "Seleccione se Pretende a confidencialidade da Pessoa Segura.");
            }

            mBeneficiarios.vlaidar(ref msgErro);


        }

        #endregion

        #region >>> Helper Methods <<<

        public void GetData(ref Proposta proposta)
        {
            proposta.PS_E_TOMADOR = mPS_Is_Tomador.SelectedValue;

            if (mPS_Is_Tomador.SelectedValue == "N")
            {
                Pessoa _p = mPessoaSegura.getPerson();
                proposta.PESSOAS.Add(new ProposalPerson()
                {
                    TIPO = _p.TIPOPESSOA,
                    PCODIGO = _p.PCODIGO,
                    NUMERO_CONTRIBUINTE = _p.PCONTRIB,
                    BI = _p.PBI,
                    DATA_NASCIMENTO = _p.DT_NASCIMENTO,
                    SEXO = _p.PTIPO,
                    ESTADO_CIVIL = _p.ESTADO_CIVIL,
                    PROFISSAO = _p.PPROFISS,
                    TITULO_HONORIFICO = _p.PTITULO,
                    NOME = _p.PNOME,
                    NACIONALIDADE = _p.NACIONALIDADE,
                    TELEMOVEL = _p.TELEMOVEL,
                    PARENTESCO = mParentesco.SelectedValue,

                    MORADA_TIPO = _p.Contacto01.MORADA_TIPO,
                    MORADA_DESC = _p.Contacto01.MORADA_DESC,
                    MORADA_NUMERO = _p.Contacto01.MORADA_NUMERO,
                    MORADA_ANDAR = _p.Contacto01.MORADA_ANDAR,
                    CODIGO_POSTAL = _p.Contacto01.CODIGO_POSTAL,
                    SUFIXO_POSTAL = _p.Contacto01.SUFIXO_POSTAL,
                    LOCALIDADE = _p.Contacto01.LOCALIDADE,
                    PAIS = _p.Contacto01.PAIS,
                    TELEFONE = _p.Contacto01.TELEFONE,
                    TELEX_FAX = _p.Contacto01.TELEX_FAX,
                    E_MAIL = _p.Contacto01.E_MAIL,

                    AO_CUIDADO = _p.Contacto02.AO_CUIDADO,
                    MORADA_TIPO_2 = _p.Contacto02.MORADA_TIPO,
                    MORADA_DESC_2 = _p.Contacto02.MORADA_DESC,
                    MORADA_NUMERO_2 = _p.Contacto02.MORADA_NUMERO,
                    MORADA_ANDAR_2 = _p.Contacto02.MORADA_ANDAR,
                    CODIGO_POSTAL_2 = _p.Contacto02.CODIGO_POSTAL,
                    SUFIXO_POSTAL_2 = _p.Contacto02.SUFIXO_POSTAL,
                    LOCALIDADE_2 = _p.Contacto02.LOCALIDADE,
                    PAIS_2 = _p.Contacto02.PAIS

                });
            }

            proposta.CONFIDENCIALIDADE_PS = mDadosConfideciais.SelectedValue;

            mBeneficiarios.GetData(ref proposta);
        }

        #endregion
    }
}