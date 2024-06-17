using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using Resources;
using System.Web.UI.WebControls;
using System.Web.Caching;
using Lusitania.Simuladores.DataLayer;


/// <summary>
/// Summary description for UserInterfaceService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class UserInterfaceService : System.Web.Services.WebService
{
    #region Constants

    private const string cGlobalProfessionListCacheKey = "{7255696D-887E-44b9-B690-46137A4364FB}";
    private const string cPersonalAccidentsOptionListCacheKey = "{71A0DB13-B825-4040-BAEC-E6B74A55E2D9}";
    private const string cPersonalAccidentsProfessionListCacheKey = "{DBD987A7-43E6-4c4d-9FB3-C937265DD76C}";
    private const string cHomeProductListCacheKey = "{F3F84012-56CF-48d3-923C-A8D7AE6BD42D}";
    private const string cHomePurposeListCacheKey = "{3023C1F1-5B80-4795-ADA1-9C367A18BF53}";
    private const string cHomeTypeListCacheKey = "{2071E3EA-B57F-401c-A2DC-83905AAF588C}";
    private const string cHomeBuildingStatusListCacheKey = "{379D1640-ECCC-4f72-B4AA-E4E96DC35BED}";
    private const string cHomeConstructionTypeListCacheKey = "{D18D27DC-62E0-424d-97B7-DB184132D516}";
    private const string cHomeLocationListCacheKey = "{E43B8CB4-FF93-4f03-B27E-AE92436FB592}";
    private const string cHomeZoneListCacheKey = "{C3E392C9-7818-4d32-B3AB-0B25B3496219}";

    #endregion

    #region Fields

    private object mDummy;

    #endregion

    [WebMethod]
    [ScriptMethod]
    public CascadingDropDownNameValue[] GetEnterpriseActivityList(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> xList = new List<CascadingDropDownNameValue>();

        Lusitania.Simuladores.DataLayer.GlobalDS.SABER_ACTIV_PROFISSIONAISDataTable xTable = (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.SABER_ACTIV_PROFISSIONAISTableAdapter()).GetData(out mDummy);
        foreach (Lusitania.Simuladores.DataLayer.GlobalDS.SABER_ACTIV_PROFISSIONAISRow xRow in xTable)
        {
            xList.Add(new CascadingDropDownNameValue(
                xRow[xTable.DESCRITIVO_ABREVColumn].ToString(),
                xRow[xTable.CODIGOColumn].ToString()
            ));
        }

        return xList.ToArray();
    }

    [WebMethod]
    [ScriptMethod]
    public CascadingDropDownNameValue[] GetEnterpriseTitleList(string knownCategoryValues, string category)
    {
        List<CascadingDropDownNameValue> xList = new List<CascadingDropDownNameValue>();

        Lusitania.Simuladores.DataLayer.GlobalDS.SABER_TITULO_NOMEDataTable xTable = (new Lusitania.Simuladores.DataLayer.GlobalDSTableAdapters.SABER_TITULO_NOMETableAdapter()).GetData((3).ToString(), out mDummy);
        foreach (Lusitania.Simuladores.DataLayer.GlobalDS.SABER_TITULO_NOMERow xRow in xTable)
        {
            xList.Add(new CascadingDropDownNameValue(
                xRow[xTable.DESCRITIVOColumn].ToString(),
                xRow[xTable.CODIGOColumn].ToString()
            ));
        }

        return xList.ToArray();
    }
}