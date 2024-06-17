using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using Lusitania.Simuladores.Web;

[assembly: WebResource(WebResourceKeys.GroupedRadioButtonRequiredFieldValidator_JavaScript, "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.WebControls
{
    public class GroupedRadioButtonRequiredFieldValidator : CustomValidator
    {
        #region Constants

        private const string cGroupName = "GroupName";

        #endregion

        #region Properties

        public string GroupName
        {
            get { return ViewState[cGroupName] as string; }
            set { ViewState[cGroupName] = value; }
        }
        
        #endregion

        #region Methods

        protected override bool ControlPropertiesValid()
        {
            ControlToValidate = Parent.ID;
            return true;
        }

        protected override bool EvaluateIsValid()
        {
            return true;
            /*
            foreach (RadioButton radio in WebHelper.FindControls<RadioButton>(FindControl(ControlToValidate)))
            {
                if (radio.GroupName == GroupName && radio.Checked)
                {
                    return true;
                }
            }

            return false;
             */
        }

        protected override void OnInit(EventArgs e)
        {
            ControlToValidate = Parent.ID;
            ClientValidationFunction = "GroupedRadioButtonRequiredFieldValidator_EvaluateIsValid";

            base.OnInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (EnableClientScript)
            {
                RegisterClientScript();
            }

            base.OnPreRender(e);
        }

        private void RegisterClientScript()
        {
            ScriptManager.RegisterExpandoAttribute(this, ClientID, "GroupName", GroupName, true);
            ScriptManager.RegisterClientScriptResource(this, GetType(), WebResourceKeys.GroupedRadioButtonRequiredFieldValidator_JavaScript);
        }

        #endregion
    }
}
