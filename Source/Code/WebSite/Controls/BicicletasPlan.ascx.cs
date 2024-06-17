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
using Lusitania.Simuladores.DataLayer;
using System.Collections.Specialized;

using System.Globalization;
using Lusitania.Simuladores.DataLayer.Simulation;


public partial class BicicletasPlan : UserControl
{
   
    #region Methods

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
          
            mGridCoberturas.DataSource = (new CoberturasDS()).GridCoberturas();
            mGridCoberturas.DataBind();
        } 

        base.OnLoad(e);
    }

    #endregion   
    
}