using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class QuotationPage : MyPage
{
    protected decimal? SimulationID
    {
        get { return ViewState["SimulationID"] as decimal?; }
        set { ViewState["SimulationID"] = value; }
    }

    protected string Format
    {
        get { return ViewState["Format"] as string; }
        set { ViewState["Format"] = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        SimulationID = TryConvert.ToDecimal(Request.Params.Get("SimulationID"));
        Format = Request.Params.Get("Format");

        base.OnInit(e);
    }
}