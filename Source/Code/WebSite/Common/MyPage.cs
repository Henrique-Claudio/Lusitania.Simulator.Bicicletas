using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Utilities;
//using Oracle.DataAccess.Client;


public class MyPage : Lusitania.Utilities.ClViewStateCompressor//Page
{
    #region Fields

    protected object Dummy;

    public bool MaintainFocusNextTime
    {
        get { return _MaintainFocusNextTime; }
        set { _MaintainFocusNextTime = value; }
    }
    private bool _MaintainFocusNextTime = true;

    #endregion

    #region Properties

    protected override PageStatePersister PageStatePersister
    {
        get
        {
            if (Settings.UseSessionForViewState)
            {
                return new SessionPageStatePersister(this);
            }
            else
            {
                return base.PageStatePersister;
            }
        }
    }

    protected string ServidorDestino
    {
        get { return ViewState["servidorDestino"] as string; }
        set { ViewState["servidorDestino"] = value; }
    }

    #endregion

    /// <summary>
    /// Nova LoadPageStateFromPersistenceMedium
    /// source: http://www.codeproject.com/Articles/101888/ViewState-Various-ways-to-reduce-performance-overh
    /// </summary>
    /// <returns></returns>
    //protected override object LoadPageStateFromPersistenceMedium()
    //{
    //    System.Web.UI.PageStatePersister pageStatePersister1 = this.PageStatePersister;
    //    pageStatePersister1.Load();
    //    String vState = pageStatePersister1.ViewState.ToString();
    //    byte[] pBytes = System.Convert.FromBase64String(vState);
    //    pBytes = Compressor.Decompress(pBytes);
    //    LosFormatter mFormat = new LosFormatter();
    //    Object ViewState = mFormat.Deserialize(System.Convert.ToBase64String(pBytes));
    //    return new Pair(pageStatePersister1.ControlState, ViewState);
    //}

    /// <summary>
    /// Nova SavePageStateToPersistenceMedium
    /// source: http://www.codeproject.com/Articles/101888/ViewState-Various-ways-to-reduce-performance-overh
    /// </summary>
    /// <returns></returns>
    //protected override void SavePageStateToPersistenceMedium(Object pViewState)
    //{
    //    Pair pair1;
    //    System.Web.UI.PageStatePersister pageStatePersister1 = this.PageStatePersister;
    //    Object ViewState;
    //    if (pViewState is Pair)
    //    {
    //        pair1 = ((Pair)pViewState);
    //        pageStatePersister1.ControlState = pair1.First;
    //        ViewState = pair1.Second;
    //    }
    //    else
    //    {
    //        ViewState = pViewState;
    //    }
    //    LosFormatter mFormat = new LosFormatter();
    //    System.IO.StringWriter mWriter = new System.IO.StringWriter();
    //    mFormat.Serialize(mWriter, ViewState);
    //    String mViewStateStr = mWriter.ToString();
    //    byte[] pBytes = System.Convert.FromBase64String(mViewStateStr);
    //    pBytes = Compressor.Compress(pBytes);
    //    String vStateStr = System.Convert.ToBase64String(pBytes);
    //    pageStatePersister1.ViewState = vStateStr;
    //    pageStatePersister1.Save();
    //}
    

    protected override void OnLoad(EventArgs e)
    {
        //sets onfocus event to all apropriate controls on the page.
        if (!IsPostBack)
        {
            HookOnFocus(this.Page as Control);
            ServidorDestino = Request.QueryString["destino"];
        }

        //HookDataSourcesToSuppresOracleNoDataFoundBug(this);

        // done
        base.OnLoad(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        
       /* if (MaintainFocusNextTime)
        {
            //replaces REQUEST_LASTFOCUS in SCRIPT_DOFOCUS with the posted value from 
            //Request ["__LASTFOCUS"]
            //and registers the script to start after Update panel was rendered
            ScriptManager.RegisterStartupScript(
                this,
                typeof(MyPage),
                "ScriptDoFocus",
                cScriptDoFocus.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]),
                true);
        }
        

        // this hack is need to solve the slowness problem of the updatepanel + very big dropdownlist
        // this hack will also disable some functions of the clientscriptmanager, which are never used anyway
        ScriptManager.RegisterStartupScript(this, GetType(), "RemoveWebForm_InitCallback", @"WebForm_InitCallback=function() {};", true);
        */
        // done
        base.OnPreRender(e);
    }

    private void DisableDataSourcesCore(Control parent)
    {
        ObjectDataSource ods = parent as ObjectDataSource;
        if (ods != null)
        {
            ods.TypeName = typeof(DummyDataObject).FullName;
            ods.SelectMethod = "GetEmptyDataTable";
            ods.SelectParameters.Clear();

            /*
            ods.Selecting += new ObjectDataSourceSelectingEventHandler(delegate(object pSender, ObjectDataSourceSelectingEventArgs pArgs)
            {
                pArgs.Cancel = true;
            });
            ods.Selected += new ObjectDataSourceStatusEventHandler(delegate(object pSender, ObjectDataSourceStatusEventArgs pArgs)
            {
                pArgs.ReturnValue
            });
             */
        }

        foreach (Control child in parent.Controls)
        {
            DisableDataSourcesCore(child);
        }
    }

    protected T GetFromViewState<T>(string key, T _default)
    {
        try
        {
            return (T)ViewState[key];
        }
        catch
        {
            return _default;
        }
    }


    /// <summary>
    /// This function goes recursively all child controls and sets 
    /// onfocus attribute if the control has one of defined types.
    /// </summary>
    /// <param name="CurrentControl">the control to hook.</param>
    private void HookOnFocus(Control CurrentControl)
    {
        //checks if control is one of TextBox, DropDownList, ListBox or Button
      /*  if ((CurrentControl is TextBox) ||
            (CurrentControl is DropDownList) ||
            (CurrentControl is ListBox) ||
            (CurrentControl is Button))
        {
            //adds a script which saves active control on receiving focus 
            //in the hidden field __LASTFOCUS.
            (CurrentControl as WebControl).Attributes.Add(
                "onfocus",
                "try { $get('__LASTFOCUS').value = this.id } catch(e) {}");
        }

        //checks if the control has children
        if (CurrentControl.HasControls())
        {
            //if yes do them all recursively
            foreach (Control CurrentChildControl in CurrentControl.Controls)
            {
                HookOnFocus(CurrentChildControl);
            }
        }*/
    }

    /// <summary>
    /// This script sets a focus to the control with a name to which
    /// REQUEST_LASTFOCUS was replaced. Setting focus heppens after the page
    /// (or update panel) was rendered. To delay setting focus the function
    /// window.setTimeout() will be used.
    /// </summary>
    private const string cScriptDoFocus =
        @"setTimeout('DoFocus()', 1); " +
        "function DoFocus() { try { $get('REQUEST_LASTFOCUS').focus(); }  catch (ex) {} }";

    //private void HookDataSourcesToSuppresOracleNoDataFoundBug(Control parent)
    //{
    //    foreach (Control child in parent.Controls)
    //    {
    //        ObjectDataSource xODS = child as ObjectDataSource;
    //        if (xODS != null)
    //        {
    //            xODS.Selected += new ObjectDataSourceStatusEventHandler(delegate(object pSender, ObjectDataSourceStatusEventArgs pArgs)
    //            {
    //                if (pArgs.Exception != null)
    //                {
    //                    OracleException xOracleException = DiscoverException<OracleException>(pArgs.Exception);
    //                    if (xOracleException != null)
    //                    {
    //                        if (xOracleException.Number == 1403)
    //                        {
    //                            pArgs.ExceptionHandled = true;
    //                        }
    //                    }
    //                }
    //            });
    //        }

    //        HookDataSourcesToSuppresOracleNoDataFoundBug(child);
    //    }
    //}

    private T DiscoverException<T>(Exception exc) where T : Exception
    {
        if (exc == null) throw new ArgumentNullException("exc");

        do
        {
            T xSearch = exc as T;
            if (xSearch != null)
            {
                return xSearch;
            }

        } while ((exc = exc.InnerException) != null);

        return null;
    }
}

//public static class Compressor
//{

//    public static byte[] Compress(byte[] data)
//    {
//        System.IO.MemoryStream output = new System.IO.MemoryStream();
//        System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(output,
//                          System.IO.Compression.CompressionMode.Compress, true);
//        gzip.Write(data, 0, data.Length);
//        gzip.Close();
//        return output.ToArray();
//    }

//    public static byte[] Decompress(byte[] data)
//    {
//        System.IO.MemoryStream input = new System.IO.MemoryStream();

//        input.Write(data, 0, data.Length);

//        input.Position = 0;

//        System.IO.Compression.GZipStream gzip = new System.IO.Compression.GZipStream(input,
//                          System.IO.Compression.CompressionMode.Decompress, true);

//        System.IO.MemoryStream output = new System.IO.MemoryStream();

//        //byte[] buff = new byte[64];
//        byte[] buff = new byte[4096];
//        int read = -1;

//        read = gzip.Read(buff, 0, buff.Length);
//        while (read > 0)
//        {
//            output.Write(buff, 0, read);
//            read = gzip.Read(buff, 0, buff.Length);
//        }
//        gzip.Close();
//        return output.ToArray();
//    }
//}
