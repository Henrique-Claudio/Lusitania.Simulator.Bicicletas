using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.ComponentModel.Design;
using AjaxControlToolkit;


[assembly: System.Web.UI.WebResource("Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.js", "text/javascript")]

namespace Lusitania.Simuladores.Web.UI.Extenders
{
	[Designer(typeof(ValidatorEnablerDesigner))]
	[ClientScriptResource("Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior", "Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.js")]
	[TargetControlType(typeof(Control))]
	public class ValidatorEnablerExtender : ExtenderControlBase
	{
		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(RadioButton))]
		[DefaultValue("")]
		public string RadioButtonControlID
		{
			get { return GetPropertyValue("RadioButtonControlID", ""); }
			set { SetPropertyValue("RadioButtonControlID", value); }
		}

		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(DropDownList))]
		[DefaultValue("")]
		public string DropDownListControlID
		{
			get { return GetPropertyValue("DropDownListControlID", ""); }
			set { SetPropertyValue("DropDownListControlID", value); }
		}

		[ExtenderControlProperty]
		[IDReferenceProperty(typeof(CheckBox))]
		[DefaultValue("")]
		public string CheckBoxControlID
		{
			get { return GetPropertyValue("CheckBoxControlID", ""); }
			set { SetPropertyValue("CheckBoxControlID", value); }
		}
	}
}
