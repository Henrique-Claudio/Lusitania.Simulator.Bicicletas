using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lusitania.Simulador.WebCommon.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class AddressSearch : UserControl
    {
        #region Fields

        private string mSelectedRowID;

        #endregion

        #region Properties

        public string SelectedRowID
        {
            get
            {
                return mSelectedRowID;
            }
        }

        #endregion

        #region Events

        public event EventHandler AddressChosen;
        protected virtual void OnAddressChosen()
        {
            if (AddressChosen != null)
            {
                AddressChosen(this, EventArgs.Empty);
            }
        }

        public event EventHandler AddressLocalSearch;
        protected virtual void OnAddressLocalSearch()
        {
            if (AddressLocalSearch != null)
            {
                AddressLocalSearch(this, EventArgs.Empty);
            }
        }

        #endregion

        protected void mSearchResultsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Choose":
                    mSelectedRowID = mSearchResultsGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                    OnAddressChosen();
                    break;

                default:
                   // OnAddressLocalSearch();
                    break;
            }
        }

        protected void mSearchResultsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(mSearchResultsGridView, String.Format("Choose${0}", e.Row.RowIndex));
            }
        }

        protected void mRunSearchButton_Click(object sender, EventArgs e)
        {
            SearchParamZipCode4.Value = ZipCode4Box.Text.Trim();
            SearchParamZipCode3.Value = ZipCode3Box.Text.Trim();
            SearchParamAddress.Value = MyHelper.IgnorePrepositions(AddressBox.Text.Trim());
            SearchParamLocality.Value = MyHelper.IgnorePrepositions(LocalityBox.Text.Trim());
            mSearchResultsGridView.DataBind();
            OnAddressLocalSearch();
        }

        protected void mAddressTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}