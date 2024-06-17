using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.Common;

namespace Lusitania.Simuladores.WebSite.Controls
{
    public partial class BancSearch : UserControl
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

        public event EventHandler BancChosen;
        protected virtual void OnBancChosen()
        {
            if (BancChosen != null)
            {
                BancChosen(this, EventArgs.Empty);
            }
        }

        public event EventHandler BancLocalSearch;
        protected virtual void OnBancLocalSearch()
        {
            if (BancLocalSearch != null)
            {
                BancLocalSearch(this, EventArgs.Empty);
            }
        }

        public event EventHandler BancPageIndexChanging;
        protected virtual void OnBancPageIndexChanging()
        {
            if (BancPageIndexChanging != null)
            {
                BancPageIndexChanging(this, EventArgs.Empty);
            }
        }


        #endregion

        protected void mSearchResultsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Choose":
                    mSelectedRowID = mSearchResultsGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                    OnBancChosen();
                    break;
                    
                default:
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

            SearchParamDescritivoBanco.Value = mNomeBanco.Text.Trim();            
            mSearchResultsGridView.DataBind();

            OnBancLocalSearch();
        }

        protected void mSearchResultsGridView_PageIndexChanging(object sender, EventArgs e)
        {
            OnBancPageIndexChanging();
        }

        protected void mAddressTypeDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
    }
}