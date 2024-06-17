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
using System.Collections.Generic;
using AjaxControlToolkit;
using System.Diagnostics;
using System.Xml;
using Lusitania.Simuladores.DataLayer;

using Lusitania.Web.Simuladores.Base.Classes;
using Lusitania.LusnetBase;
using Lusitania.Utilities;
using Lusitania.Model.Simuladores.Negocio;
using Lusitania.Simuladores.DataLayer.Base.MODEL;
using Lusitania.Simuladores.DataLayer.Common.Model;
using Lusitania.Simulador.WebCommon.Common;
using Lusitania.Simuladores.DataLayer.Proposal.POCO;

public partial class BicicletasHeirListControl : UserControl
{

    #region Properties

    public List<Pessoa> Pessoas
    {
        get
        {
            return ((List<Pessoa>)ViewState["Pessoas"] != null) ? (List<Pessoa>)ViewState["Pessoas"] : new List<Pessoa>();
        }
        set
        {

            ViewState["Pessoas"] = value;
        }
    }

    public string Product
    {
        get { return (ViewState["Product"] != null) ? (string)ViewState["Product"] : string.Empty; }
        set { ViewState["Product"] = value; }
    }

    #endregion

    #region >>>> Page Events <<<<

    protected override void OnPreRender(EventArgs e)
    {
        conAddButtons.Visible = !conPerson.Visible;

        base.OnPreRender(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
            mPersonPH.pPersonType = "3";
        }

        base.OnLoad(e);
    }

    private void BindGridView()
    {
        ItemsGridView.DataSource = Pessoas;
        ItemsGridView.DataBind();
    }

    #endregion

    #region >>>> Elements Events

    protected void ItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "RemoveItem":
                {
                    RemovePerson(Convert.ToInt32(e.CommandArgument));
                    BindGridView();
                }
                break;
        }
    }

    protected void mParentesco_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Pessoa> _ps = Pessoas;
        foreach (var item in _ps)
        {
            item.PARENTESCO = ((DropDownList)ItemsGridView.Rows[item.ORDEM - 1].FindControl("mParentesco")).SelectedValue;
        }

        Pessoas = _ps;
    }

    protected void SaveHeirButton_Click(object sender, EventArgs e)
    {
        AddHeir();
    }

    protected void CancelHeirButton_Click(object sender, EventArgs e)
    {
        CancelAddHeir();
    }

    protected void AddHeirButton_Click(object sender, EventArgs e)
    {
        conPerson.Visible = true;
        mPersonPH.ClearAllFields();
    }

    #endregion

    #region >>>> Helper Methods <<<<

    public void RemoveAllHeirs()
    {
        Pessoas.Clear();
        mPersonPH.ClearAllFields();
        BindGridView();
    }

    private void RemovePerson(int index)
    {
        foreach (Pessoa item in Pessoas)
        {
            if (item.ORDEM == index)
            {
                Pessoas.Remove(item);
                break;
            }
        }

        OrdenarPersons();
    }


    private void OrdenarPersons()
    {
        int i = 1;
        foreach (Pessoa item in Pessoas)
        {
            item.ORDEM = i;
            i++;
        }
    }

    private void AddHeir()
    {
        string _msgErro = string.Empty;
        mPersonPH.validar(ref _msgErro);

        if (string.IsNullOrEmpty(_msgErro))
        {
            bool existHeir = false;
            Pessoa _p = mPersonPH.getPerson();
            foreach (Pessoa item in Pessoas)
            {
                if (item.PCODIGO == _p.PCODIGO)
                {
                    existHeir = true;
                    break;
                }
            }
            if (!existHeir)
            {
                List<Pessoa> _ps = Pessoas;
                _ps.Add(_p);
                Pessoas = _ps;

                OrdenarPersons();

                BindGridView();

                CancelAddHeir();
            }
            else
            {
                ScriptHelper.ShowMessageBox("O Herdeiro que quer introduzir já se encontre na lista.", "Dados Herdeiro", ScriptHelper.AlertType.ALERT);
            }
        }
        else
        {
            ScriptHelper.ShowMessageBox(_msgErro, "Dados Herdeiro", ScriptHelper.AlertType.ERROR);
        }

    }

    private void CancelAddHeir()
    {
        conPerson.Visible = false;
    }

    protected DataTable ListaParentesco()
    {
        List<GenericListsDS> _parentesco = GenericListsDS.ListaParentesco(Product);
        _parentesco.Insert(0, new GenericListsDS() { Codigo = "", Descritivo = "Seleccione o Parentesco" });
        return BusinessHelper.ToDataTable<GenericListsDS>(_parentesco);
    }

    public void GetData(ref Proposta proposta)
    {
        foreach (Pessoa _p in Pessoas)
        {
            proposta.PESSOAS.Add(new ProposalPerson()
            {
                TIPO = _p.TIPOPESSOA,
                SEQUENCIA = _p.ORDEM.ToString(),
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
                PARENTESCO = _p.PARENTESCO,

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
    }

    #endregion

    #region >>> Validação <<<

    public void validar(ref string msgErro)
    {
        if (Pessoas.Count == 0)
        {
            BusinessHelper.AddErroAtList(ref msgErro, "Deve descriminar pelo menos um herdeiro.");
        }
        else
        {
            foreach (var item in Pessoas)
            {
                if (string.IsNullOrEmpty(item.PARENTESCO))
                {
                    BusinessHelper.AddErroAtList(ref msgErro, "O herdeiro número " + item.ORDEM + " não tem parentesco selecionado");
                }

            }
        }
    }

    #endregion

}
