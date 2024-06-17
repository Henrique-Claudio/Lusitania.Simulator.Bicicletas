using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;

namespace Lusitania.Simuladores.WebSite.Common
{
    public class BaseMasterPage : System.Web.UI.MasterPage
    {


        public BaseMasterPage()
        {
            this.SetCultureSettings();
        }

        protected void SetCultureSettings()
        {
            try
            {
                string CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;

                CultureInfo myCulture = new System.Globalization.CultureInfo(CurrentCulture, false);

                myCulture.DateTimeFormat.DateSeparator = "-";
                myCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                myCulture.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy";


                myCulture.NumberFormat.NumberDecimalDigits = 2;
                myCulture.NumberFormat.NumberGroupSeparator = ".";
                myCulture.NumberFormat.NumberDecimalSeparator = ",";
                myCulture.NumberFormat.CurrencyDecimalDigits = 2;
                myCulture.NumberFormat.CurrencyGroupSeparator = ".";
                myCulture.NumberFormat.CurrencyDecimalSeparator = ",";
                myCulture.NumberFormat.CurrencySymbol = "€";


                Thread.CurrentThread.CurrentCulture = myCulture;
                Thread.CurrentThread.CurrentUICulture = myCulture;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }

    }



}
