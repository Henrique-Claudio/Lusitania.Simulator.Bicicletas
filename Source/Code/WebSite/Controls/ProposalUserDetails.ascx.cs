using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Resources;
using System.ComponentModel;
using System.Text;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters;
using System.Collections.Generic;
using Resources;

using System.Linq;
using System.Reflection;
using Lusitania.Simulador.WebCommon.Common;

// New
using Lusitania.LusnetBase;
using Lusitania.Web.Simuladores.Base.Classes;
using Lusitania.Model.Simuladores.Negocio;
using Lusitania.Utilities;
using Lusitania.Simuladores.WebSite.Common;
using System.Configuration;
using System.Xml;
using Lusitania.Simuladores.DataLayer.Common.Model;
using Lusitania.Simuladores.DataLayer.Common;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class ProposalUserDetails : ClControloBase
    {
        #region >>>> Properties <<<<


        public bool? IsSearchPanelVisible { get; set; }


        public string StartNif { get; set; }

        public string NIF
        {
            get
            {
                return mTomador.Nif;
            }
        }

        public string pCodigo
        {
            get
            {
                return mTomador.pCodigo;
            }
        }

        public DateTime? DataNascimento {
            get { return mTomador.DateOfBirth; }        
        }

        public Decimal pIdPlano
        {
            get
            {
                if (ViewState["pIdPlano"] == null)
                {
                    return 0;
                }
                return Convert.ToDecimal(ViewState["pIdPlano"]);
            }
            set
            {
                ViewState["pIdPlano"] = value;
            }
        }


        public string Telemovel
        {
            get { return (ViewState["TelemovelTomador"] != null) ? (string)ViewState["TelemovelTomador"] : string.Empty; }
            set { ViewState["TelemovelTomador"] = value; }
        }

        public string Email
        {
            get { return (ViewState["EmailTomador"] != null) ? (string)ViewState["EmailTomador"] : string.Empty; }
            set { ViewState["EmailTomador"] = value; }
        }

        #endregion

        public string pClientNumberBox
        {
            get { return (ViewState["pClientNumberBox"] != null) ? (string)ViewState["pClientNumberBox"] : string.Empty; }
            set { ViewState["pClientNumberBox"] = value; }
        }


        #region >>>> Page Events <<<<

        protected override void OnLoad(EventArgs e)
        {

            mTomador.SetSearchPanelVisible(IsSearchPanelVisible);

            if (!IsPostBack)
            {
                mTomador.StartNif = StartNif;
                mTomador.pPersonType = "1";
                mTomador.TelemovelTomador = this.Telemovel;
                mTomador.EmailTomador = this.Email;
                
                if (pIdPlano > 0)
                {
                    mTomador.pIdPlano = this.pIdPlano;    
                }
            }

            base.OnInit(e);

        }

        #endregion

        #region >>>> Validação <<<

        public void validar(ref string msgErro)
        {
            mTomador.validar(ref msgErro);
        }

        #endregion

        #region >>>> Helper Methods <<<

        public void GetData(ref Proposta proposta)
        {
            Pessoa _p = mTomador.getPerson();
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

                MORADA_TIPO = _p.Contacto01.MORADA_TIPO,
                TITULO_ARTERIA = _p.Contacto01.TITULO_ARTERIA,
                MORADA_DESC = _p.Contacto01.MORADA_DESC,
                MORADA_NUMERO = _p.Contacto01.MORADA_NUMERO,
                MORADA_ANDAR = _p.Contacto01.MORADA_ANDAR,                
                CODIGO_POSTAL = _p.Contacto01.CODIGO_POSTAL,                
                SUFIXO_POSTAL = _p.Contacto01.SUFIXO_POSTAL,                
                LOCALIDADE = _p.Contacto01.LOCALIDADE,
                LOCAL = _p.Contacto01.LOCAL,
                CPALF = _p.Contacto01.CPALF,
                PAIS = _p.Contacto01.PAIS,
                TELEFONE = _p.Contacto01.TELEFONE,                
                TELEX_FAX = _p.Contacto01.TELEX_FAX,
                E_MAIL = _p.Contacto01.E_MAIL,

                AO_CUIDADO = _p.Contacto02.AO_CUIDADO,
                MORADA_TIPO_2 = _p.Contacto02.MORADA_TIPO,
                TITULO_ARTERIA_2 = _p.Contacto02.TITULO_ARTERIA,
                MORADA_DESC_2 = _p.Contacto02.MORADA_DESC,
                MORADA_NUMERO_2 = _p.Contacto02.MORADA_NUMERO,
                MORADA_ANDAR_2 = _p.Contacto02.MORADA_ANDAR,
                CODIGO_POSTAL_2 = _p.Contacto02.CODIGO_POSTAL,
                SUFIXO_POSTAL_2 = _p.Contacto02.SUFIXO_POSTAL,
                LOCALIDADE_2 = _p.Contacto02.LOCALIDADE,
                LOCAL_2 = _p.Contacto02.LOCALIDADE,
                CPALF_2 = _p.Contacto02.CPALF,
                PAIS_2 = _p.Contacto02.PAIS

            });
        }

        #endregion
    }
}
