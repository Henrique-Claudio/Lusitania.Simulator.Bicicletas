using System.ComponentModel;
using System;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace Lusitania.Simuladores.Web.UI.WebControls
{
    /// <summary>
    /// Requires a user to check at least one check box and/or limits the number of checkboxes that may be checked in the associated CheckBoxList control.
    /// </summary>
    /// <remarks></remarks>
    public class CheckBoxListRequiredFieldValidator : BaseValidator
    {
        /// <summary>
        /// The minimum number of checkboxes allowed to be checked.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [Description("The minimum number of checkboxes allowed to be checked.")]
        public int MinimumCheckBoxesRequired
        {
            get
            {
                return (ViewState["MinimumCheckBoxesRequired"] as int?).GetValueOrDefault(1);
            }
            set
            {
                ViewState["MinimumCheckBoxesRequired"] = value;
            }
        }

        /// <summary>
        /// The maximum number of checkboxes allowed to be checked. Set this to zero to allow all.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [Description("The maximum number of checkboxes allowed to be checked. Set this to zero to allow all.")]
        public int MaximumCheckBoxesAllowed
        {
            get
            {
                return (ViewState["MaximumCheckBoxesAllowed"] as int?).GetValueOrDefault(0);
            }
            set
            {
                ViewState["MaximumCheckBoxesAllowed"] = value;
            }
        }
        /*
        /// <summary>
        /// Gets or sets the CheckBoxList to validate.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [Description("Gets or sets the CheckBoxList to validate."), TypeConverter(typeof(CheckBoxListConverter))]
        public new string ControlToValidate
        {
            get
            {
                string target = ViewState["ControlToValidate"] as string;
                return (target == null ? String.Empty : target);
            }
            set
            {
                ViewState["ControlToValidate"] = value;
            }
        }
        */
        protected override bool ControlPropertiesValid()
        {
            return true;
        }

        protected override bool EvaluateIsValid()
        {
            return EvaluateIsChecked();
        }

        /// <summary>
        /// Server side code to check that the required number of checkboxes are checked.
        /// </summary>
        /// <returns>A boolean value </returns>
        /// <remarks></remarks>
        protected bool EvaluateIsChecked()
        {
            // Get the checkboxlist to check
            CheckBoxList target = FindControl(ControlToValidate) as CheckBoxList;

            // Set the items checked count to zero
            int CountChecked = 0;

            // Count each item that is checked in the list
            foreach (ListItem item in target.Items)
            {
                if (item.Selected)
                {
                    CountChecked += 1;
                }
            }

            // After all checkboxes have been counted see if the number of items checked is within the min and max allowed
            if (CountChecked >= MinimumCheckBoxesRequired && (CountChecked <= MaximumCheckBoxesAllowed || MaximumCheckBoxesAllowed == 0))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Injects the client side validation script.
        /// </summary>
        /// <param name="e"></param>
        /// <remarks></remarks>
        protected override void OnPreRender(System.EventArgs e)
        {
            if (EnableClientScript)
            {
                ClientScript();
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Client side script to check that the required number of checkboxes are checked.
        /// </summary>
        /// <remarks></remarks>
        protected void ClientScript()
        {
            string xStartVerificationScriptName = "StartVerification" + ClientID;
            if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), xStartVerificationScriptName))
            {
                Attributes.Add("evaluationfunction", "start_verification_" + ClientID);

                string xScript = (
                    "function start_verification_###CLIENTID###(val)" +
                    "{" +
                        "return checkboxlist_verification('###CLIENTID###');" +
                    "}")
                .Replace("###CLIENTID###", ClientID);

                if (ScriptManager.GetCurrent(Page) == null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), xStartVerificationScriptName, xScript, true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), xStartVerificationScriptName, xScript, true);
                }
            }

            string xCheckBoxListVerificationScriptName = "CheckBoxListVerification" + ClientID;
            if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), xCheckBoxListVerificationScriptName))
            {
                string xScript = (
                    "function checkboxlist_verification(clientID)" +
                    "{" +
                        "var val = document.all[document.all[clientID].controltovalidate];" +
                        "var col = val.all;" +
                        "if (col != null)" +
                        "{" +
                            "var checked = 0;" +
                            "var checkboxcount = 0;" +
                            "for (i = 0; i < col.length; i++)" +
                            "{" +
                                "if (col.item(i).tagName == 'INPUT')" +
                                "{" +
                                    "checkboxcount += 1;" +
                                    "if (col.item(i).checked)" +
                                    "{" +
                                        "checked += 1;" +
                                    "}" +
                                "}" +
                            "}" +
                            "if (checked >= ###MinReq### && (checked <= ###MaxAllowed### || ###MaxAllowed### == 0))" +
                            "{" +
                                "return true;" +
                            "}" +
                            "else" +
                            "{" +
                                "return false;" +
                            "}" +
                        "}" +
                    "}")
                .Replace("###MinReq###", MinimumCheckBoxesRequired.ToString())
                .Replace("###MaxAllowed###", MaximumCheckBoxesAllowed.ToString());

                if (ScriptManager.GetCurrent(Page) == null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), xCheckBoxListVerificationScriptName, xScript, true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), xCheckBoxListVerificationScriptName, xScript, true);
                }
            }
        }
    }
}