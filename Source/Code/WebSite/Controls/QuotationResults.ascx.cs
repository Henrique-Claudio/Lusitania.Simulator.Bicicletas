using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Simuladores.WebSite.Common;
using System.Collections.Generic;
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.Simulation.POCO;
using Lusitania.Simuladores.DataLayer.Common;

public partial class QuotationResults : UserControl
{


    #region Fields
    private const string SDD = "_SDD";    
    #endregion

    #region Properties

    public decimal? SimulatorID
    {
        get { return ViewState["SimulatorId"] as decimal?; }
        set { ViewState["SimulatorId"] = value; }
    }

    public string SelectedQuoteId
    {
        get { return ViewState["SelectedQuoteId"] as string; }
        set { ViewState["SelectedQuoteId"] = value; }
    }
    
    public Boolean? ShowSdd
    {
        get { return ViewState["ShowSdd"] as Boolean?; }
        set { ViewState["ShowSdd"] = value; }
    }

    //public Boolean? ShowMB
    public Boolean? ShowPremiumEmPlano
    {
        get { return ViewState["ShowPremiumEmPlano"] as Boolean?; }
        set { ViewState["ShowPremiumEmPlano"] = value; }
    }

    private List<Produto> _produtos { get;set; }
    public List<Produto> Produtos { 
        get{
            return _produtos;
        } 

        set{
            _produtos = value;            
            this.CanSelectProduct = this.Produtos.Count > 0 && this.Produtos.FirstOrDefault().premios.Count > 0;
        }
    }
        
    public Dictionary<string, string> ProdutoSimulacaoID
    {
        get { return (Dictionary<string, string>)ViewState["ProdutoSimulacaoID"]; }
        set {
            ViewState["ProdutoSimulacaoID"] = value;
        }
    }

    private Boolean? CanSelectProduct { 
        get { return ViewState["CanSelectProduct"] as Boolean?; }
        set { ViewState["CanSelectProduct"] = value; }
    }
	

    #endregion
    
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!SimulatorID.HasValue)
            {
                if (!(bool)ShowPremiumEmPlano)
                {
                    //trPlanPremium.Visible = false;
                }
                Clear();
            }                
            
        }
    }
    
    protected void verDetalhe_SelectedIndexChanged(object sender, EventArgs e) {
        if (this.CanSelectProduct.HasValue && this.CanSelectProduct.Value)
        {
            string _selectedProduct = mVerDetalhe_1.Checked ? UtilityG.GetValue(Products.LIGHT) : UtilityG.GetValue(Products.SUPER);

            this.Page.GetType().InvokeMember("SelectProduct", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { _selectedProduct });

           _SelectedIndexChanged(getSelectedQuotationID(_selectedProduct));
                
        }
        else
        {
            mVerDetalhe_1.Checked = mVerDetalhe_2.Checked = false;
            mRefSim.Text = string.Empty;
        }
        
        
    }

    #endregion

    #region Methods

    public void Clear()
    {
        mPremio_Produto_1.Text =
        //mPremio_Plano_Produto_1.Text =
        mPremio_Produto_2.Text =
        //mPremio_Plano_Produto_2.Text = "N/A";

        mRefSim.Text = 
        SelectedQuoteId = string.Empty;

        CanSelectProduct = 
        mVerDetalhe_1.Checked =
        mVerDetalhe_2.Checked = false;
        simRadiosEnable(false);

        mLabelDisable_1.Visible = !mVerDetalhe_1.Checked;
        mLabelEnable_1.Visible = !mLabelDisable_1.Visible;

        mLabelDisable_2.Visible = !mVerDetalhe_2.Checked;
        mLabelEnable_2.Visible = !mLabelDisable_2.Visible;
        
        this.ProdutoSimulacaoID =  new Dictionary<string,string>();
    }

    public void BindPremios() {

        if (this.Produtos != null)
        {
            this.ProdutoSimulacaoID = new Dictionary<string, string>();
            foreach (Produto item in this.Produtos)
            {
                if (item.ProductID == UtilityG.GetValue(Products.LIGHT))
                {
                    //obter os premios anuais 
                    Premio _premiosAnual =  item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    mPremio_Produto_1.Text = UtilityG.GetFormatDoubleValue(_premiosAnual.PremioTotal);
                    //mPremio_Plano_Produto_1.Text = UtilityG.GetFormatDoubleValue(_premiosAnual.PremioPlano);
                    //se existe id de simulacao guardamos
                    if (!string.IsNullOrEmpty(item.SimulalationID))
                    {
                        this.ProdutoSimulacaoID.Add(UtilityG.GetValue(Products.LIGHT), item.SimulalationID);    
                    }

                    var _premioAnual = item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    
                    //caso tem desconto adicionar um tooltip
                    if (_premioAnual != null && _premioAnual.Desconto > 0)
                    {
                        //mDescontoComrec_1.Attributes.Add("ToolTipInfo", "A simulação contem um desconto de " + _premioAnual.Desconto + " %");                        
                    }
                    //mDescontoComrec_1.Visible = _premioAnual != null && _premioAnual.Desconto > 0;

                }

                if (item.ProductID == UtilityG.GetValue(Products.SUPER))
                {
                    //obter os premios anuais 
                    Premio _premiosAnual = item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    mPremio_Produto_2.Text = UtilityG.GetFormatDoubleValue(_premiosAnual.PremioTotal);
                    //mPremio_Plano_Produto_2.Text = UtilityG.GetFormatDoubleValue(_premiosAnual.PremioPlano);
                    //se existe id de simulacao guardamos
                    if (!string.IsNullOrEmpty(item.SimulalationID))
                    {
                        this.ProdutoSimulacaoID.Add(UtilityG.GetValue(Products.SUPER), item.SimulalationID);
                    }

                    var _premioAnual = item.premios.Where(x => x.Fraccionamento == UtilityG.GetValue(Fractionations.ANUAL)).FirstOrDefault();
                    //caso tem desconto adicionar um tooltip
                    //if (_premioAnual != null && _premioAnual.Desconto > 0)
                    //{
                    //    mDescontoComrec_2.Attributes.Add("ToolTipInfo", "A simulação contem um desconto de " + _premioAnual.Desconto + " %");
                    //}
                    //mDescontoComrec_1.Visible = _premioAnual != null && _premioAnual.Desconto > 0;
                }
            }
            
        }
    }    

    public void LoadSimulacao(Simulacao sim)
    {
        this.Produtos = sim.Outputs.Produtos;

        mVerDetalhe_1.Checked = this.Produtos.FirstOrDefault().ProductID == UtilityG.GetValue(Products.LIGHT);
        mVerDetalhe_2.Checked = !mVerDetalhe_1.Checked;

        this.ProdutoSimulacaoID.Add(this.Produtos.FirstOrDefault().ProductID, this.Produtos.FirstOrDefault().SimulalationID);

       _SelectedIndexChanged( this.Produtos.FirstOrDefault().SimulalationID);

    }

    private void _SelectedIndexChanged( string  SimulalationID ) {
        mLabelDisable_1.Visible = !mVerDetalhe_1.Checked;
        mLabelEnable_1.Visible = !mLabelDisable_1.Visible;

        mLabelDisable_2.Visible = !mVerDetalhe_2.Checked;
        mLabelEnable_2.Visible = !mLabelDisable_2.Visible;

        mRefSim.Text = SelectedQuoteId = SimulalationID;

    }


    public string getSelectedQuotationID(string selectedProduct)
    {

        if (this.ProdutoSimulacaoID.Count > 0)
        {
            var _produto = this.ProdutoSimulacaoID.Where(x => x.Key == selectedProduct).FirstOrDefault();
            if (!_produto.Equals(null) && _produto.Value != null)
            {
                return _produto.Value;

            }
        }

        return string.Empty;
    }

    public void simRadiosEnable(bool status) 
    {
        mVerDetalhe_1.Enabled = status;
        mVerDetalhe_2.Enabled = status;
    }

    #endregion
}
