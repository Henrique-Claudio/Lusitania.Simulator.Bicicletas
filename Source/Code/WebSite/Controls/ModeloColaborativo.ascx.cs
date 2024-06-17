using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Globalization;
using System.Net.Mail;
using System.Xml;
using System.Net;
using System.IO;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class ModeloColaborativo : System.Web.UI.UserControl
    {
        public Button ButtonEnviarMediador;
        public Button ButtonQueroContratar;
        public Button ButtonTenhoDuvidas;
        public Button ButtonSubmitCallback;
        public Button ButtonCancelCallback;
        public HtmlTable divCallBack;


        public ModeloColaborativo() 
        {
            
        }
        /*
        public Label labelInfo = this.lblInfo;
        public ListBox ListaAgentes = this.cboAgenteCallBack;
        */
        /*
        public TextBox txtMoradaAgente = this.txtMoradaAgente;
        public TextBox txtNumeroMoradaAgente = this.txtNumeroMoradaAgente;
        public TextBox txtAndarMoradaAgente = this.txtAndarMoradaAgente;
        public TextBox txtCP4MoradaAgente = this.txtCP4MoradaAgente;
        public TextBox txtCP3MoradaAgente = this.txtCP3MoradaAgente;
        public TextBox txtLocalidadeMoradaAgente = this.txtLocalidadeMoradaAgente;
        public TextBox txtTelefoneAgente = this.txtTelefoneAgente;
        public TextBox txtTelemovelAgente = this.txtTelemovelAgente;
        public TextBox txtTelefoneTrabAgente = this.txtTelefoneTrabAgente;
        public TextBox txtEmailAgente = this.txtEmailAgente;*/

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonEnviarMediador = BtnEnviarMediador;
            ButtonQueroContratar = BtnQueroContratar;
            ButtonTenhoDuvidas = BtnTenhoDuvidas;
            ButtonSubmitCallback = BtnSubmitCallback;
            ButtonCancelCallback = BtnCancelCallback;
            divCallBack = dvCallBack;

            ButtonQueroContratar.Attributes.Add("onmouseover", "window.status='';this.style.fontWeight='bold'; return true");
            ButtonQueroContratar.Attributes.Add("onmouseout", "window.status='';this.style.fontWeight='normal'; return true");

            ButtonTenhoDuvidas.Attributes.Add("onmouseover", "window.status='';this.style.fontWeight='bold'; return true");
            ButtonTenhoDuvidas.Attributes.Add("onmouseout", "window.status='';this.style.fontWeight='normal'; return true");

            ButtonEnviarMediador.Attributes.Add("onmouseover", "window.status='';this.style.fontWeight='bold'; return true");
            ButtonEnviarMediador.Attributes.Add("onmouseout", "window.status='';this.style.fontWeight='normal'; return true");

        }

        public event EventHandler QueroContratarClick;
        public event EventHandler TenhoDuvidasClick;
        public event EventHandler EnviarMediadorClick;
        public event EventHandler SubmitCallBackClick;
        public event EventHandler CancelCallBackClick;
        public event EventHandler PreencherCallBackDetalhe;

        protected void ButtonQueroContratar_Click(object sender, EventArgs e)
        {
            if (QueroContratarClick != null)
            {
                QueroContratarClick(this,null);
            }
        }

        protected void ButtonTenhoDuvidas_Click(object sender, EventArgs e)
        {
            if (TenhoDuvidasClick != null)
            {
                TenhoDuvidasClick(this, null);
            }
        }

        protected void ButtonEnviarMediador_Click(object sender, EventArgs e)
        {
            if (EnviarMediadorClick != null)
            {
                EnviarMediadorClick(this, null);
            }
        }

        protected void ButtonSubmitCallback_Click(object sender, EventArgs e)
        {
            if (SubmitCallBackClick != null)
            {
                SubmitCallBackClick(this, null);
            }
        }

        protected void ButtonCancelCallback_Click(object sender, EventArgs e)
        {
            if (CancelCallBackClick != null)
            {
                CancelCallBackClick(this, null);
            }
         }

        protected void PreencherAgenteCallBackDetalhe(object sender, EventArgs e)
        {
            if (PreencherCallBackDetalhe != null)
            {
                PreencherCallBackDetalhe(this, null);
            }
         }

    }
}