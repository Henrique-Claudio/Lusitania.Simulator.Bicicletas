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
using Lusitania.Simuladores.DataLayer;
using Lusitania.Simuladores.DataLayer.FamilyRentDSTableAdapters;

public partial class FamilyRentHeirListControl : UserControl
{
    #region Constants

    private const string cItemsVSK = "Items";
    private const string cKinshipTableVSK = "{3E33B349-01F8-42be-A797-91C9A1254ABE}";

    #endregion

    #region Fields

    #endregion

    #region Properties

    public string ValidationGroup
    {
        get { return ViewState["ValidationGroup"] as string; }
        set { ViewState["ValidationGroup"] = value; }
    }

    public decimal? SimulationID
    {
        get { return HeirPersonalInfo.SimulationID; }
        set { HeirPersonalInfo.SimulationID = value; }
    }

    public MyHeirTarget HeirTarget
    {
        get
        {
            return (ViewState["HeirTarget"] as MyHeirTarget?).GetValueOrDefault(MyHeirTarget.Holder);
        }
        set
        {
            ViewState["HeirTarget"] = value;
        }
    }

    public IList<Item> Items
    {
        get
        {
            List<Item> xList = (List<Item>)ViewState[cItemsVSK];
            if (xList == null)
            {
                xList = new List<Item>();
                ViewState[cItemsVSK] = xList;
            }
            return xList;
        }
    }

    #endregion

    #region Types

    public enum MyHeirTarget
    {
        Holder,
        Spouse
    }

    [Serializable]
    public class Item
    {
        private string _ID = Guid.NewGuid().ToString();
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _PersonID;
        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _KinshipID;
        public string KinshipID
        {
            get { return _KinshipID; }
            set { _KinshipID = value; }
        }

        private int _Number;
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
    }

    #endregion

    #region Methods

    protected override void OnPreRender(EventArgs e)
    {
        HeirPersonalInfo.ValidationGroup = ValidationGroup;
        SaveHeirButton.ValidationGroup = ValidationGroup;
        HeirValidationSummary.ValidationGroup = ValidationGroup;

        base.OnPreRender(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            mUserNameField.Value = UserHelper.UserName;

            BindGridView();
        }

        base.OnLoad(e);
    }

    protected FamilyRentDS.LISTA_VALORES_PARENTESCODataTable QueryKinshipList()
    {
        FamilyRentDS.LISTA_VALORES_PARENTESCODataTable xTable = Context.Items[cKinshipTableVSK] as FamilyRentDS.LISTA_VALORES_PARENTESCODataTable;
        if (xTable == null)
        {
            object dummy;
            HttpContext.Current.Items[cKinshipTableVSK] = xTable = (new LISTA_VALORES_PARENTESCOTableAdapter()).GetData(UserHelper.UserName, out dummy);
        }
        return xTable;
    }

    private void BindGridView()
    {
        for (int xIndex = 0; xIndex < Items.Count; ++xIndex)
        {
            Items[xIndex].Number = xIndex + 1;
        }
        ItemsGridView.DataSource = Items;
        ItemsGridView.DataBind();
    }

    private void SaveGridView()
    {
        foreach (GridViewRow xRow in ItemsGridView.Rows)
        {
            DropDownList xKinshipBox = (DropDownList)(xRow.FindControl("KinshipBox"));

            Item xItem = Items[xRow.RowIndex];
            xItem.KinshipID = xKinshipBox.SelectedValue;
        }
    }

    public void EnsureItemsUpdated()
    {
        SaveGridView();
    }

    #endregion

    protected void ItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "AddItem":
                {
                    HeirPersonalInfoPopup.Show();
                }
                break;

            case "RemoveItem":
                {
                    SaveGridView();
                    foreach (Item xItem in Items)
                    {
                        if (xItem.Number == Convert.ToInt32(e.CommandArgument))
                        {
                            // remove the associated person
                            RemovePerson(xItem);

                            // remove it from the list
                            Items.Remove(xItem);

                            break;
                        }
                    }
                    BindGridView();
                }
                break;
        }
    }

    private void RemovePerson(Item xItem)
    {
        // remove it from the database
        (new QueriesTableAdapterFix()).REMOVE_PESSOA(
            Convert.ToDecimal(Request.Params.Get("SimulationID")),
            Convert.ToDecimal(xItem.PersonID));
    }

    public void RemoveAllHeirs()
    {
        SaveGridView();
        foreach (Item xItem in Items)
        {
            // remove the associated person
            RemovePerson(xItem);
        }
        Items.Clear();
        BindGridView();
    }

    protected void SaveHeirButton_Click(object sender, EventArgs e)
    {
        string xPersonID;
        string xErrorMessage;
        string xPersonName;

        bool xSuccess = false;
        switch (HeirTarget)
        {
            case MyHeirTarget.Holder:
                xSuccess = HeirPersonalInfo.TrySaveToHolder(out xPersonID, out xErrorMessage, out xPersonName);
                break;

            case MyHeirTarget.Spouse:
                xSuccess = HeirPersonalInfo.TrySaveToSpouse(out xPersonID, out xErrorMessage, out xPersonName);
                break;

            default:
                throw new InvalidOperationException();
        }

        if (xSuccess)
        {
            SaveGridView();
            Item xItem = new Item();
            xItem.PersonID = xPersonID;
            xItem.Name = xPersonName;
            Items.Add(xItem);
            BindGridView();

            HeirPersonalInfoPopup.Hide();
        }
        else
        {
            ScriptHelper.ShowMessageBox(xErrorMessage, Page);
        }
    }
}
