// README
//
// There are two steps to adding a property:
//
// 1. Create a member variable to store your property
// 2. Add the get_ and set_ accessors for your property
//
// Remember that both are case sensitive!


/// <reference name="MicrosoftAjaxTimer.debug.js" />
/// <reference name="MicrosoftAjaxWebForms.debug.js" />
/// <reference name="AjaxControlToolkit.ExtenderBase.BaseScripts.js" assembly="AjaxControlToolkit" />


Type.registerNamespace('Lusitania.Simuladores.Web.UI.Extenders');

Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior = function(element) {
    Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.initializeBase(this, [element]);

    this._RadioButtonControlID = null;
    this._DropDownListControlID = null;
    this._CheckBoxControlID = null;
}

Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.prototype =
{
    initialize: function() {
        Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.callBaseMethod(this, 'initialize');

        this.apply();

        var xRadioButton = $get(this._RadioButtonControlID);
        if (xRadioButton != null) {
            $addHandler(xRadioButton, 'propertychange', Function.createDelegate(this, this.apply));
        }

        var xDropDownList = $get(this._DropDownListControlID);
        if (xDropDownList != null) {
            $addHandler(xDropDownList, 'propertychange', Function.createDelegate(this, this.apply));
        }

        var xCheckBox = $get(this._CheckBoxControlID);
        if (xCheckBox != null) {
            $addHandler(xCheckBox, 'propertychange', Function.createDelegate(this, this.apply));
        }
    },

    dispose: function() {
        var xRadioButton = $get(this._RadioButtonControlID);
        if (xRadioButton != null) {
            $removeHandler(xRadioButton, 'propertychange', Function.createDelegate(this, this.apply));
        }

        var xDropDownList = $get(this._DropDownListControlID);
        if (xDropDownList != null) {
            $removeHandler(xDropDownList, 'propertychange', Function.createDelegate(this, this.apply));
        }

        var xCheckBox = $get(this._CheckBoxControlID);
        if (xCheckBox != null) {
            $removeHandler(xCheckBox, 'propertychange', Function.createDelegate(this, this.apply));
        }

        Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.callBaseMethod(this, 'dispose');
    },

    apply: function() {
        var xTarget = this.get_element();
        if (xTarget != null) {
            var xEnable = false;

            var xRadioButton = $get(this._RadioButtonControlID);
            if (xRadioButton != null && xRadioButton.checked) {
                xEnable = true;
            }

            var xDropDownList = $get(this._DropDownListControlID);
            if (xDropDownList != null && xDropDownList.value.length > 0) {
                xEnable = true;
            }

            var xCheckBox = $get(this._CheckBoxControlID);
            if (xCheckBox != null && xCheckBox.checked) {
                xEnable = true;
            }

            ValidatorEnable(xTarget, xEnable);
        }
    },

    get_RadioButtonControlID: function() {
        return this._RadioButtonControlID;
    },
    set_RadioButtonControlID: function(value) {
        this._RadioButtonControlID = value;
    },

    get_DropDownListControlID: function() {
        return this._DropDownListControlID;
    },
    set_DropDownListControlID: function(value) {
        this._DropDownListControlID = value;
    },

    get_CheckBoxControlID: function() {
        return this._CheckBoxControlID;
    },
    set_CheckBoxControlID: function(value) {
        this._CheckBoxControlID = value;
    }
}

Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.ValidatorEnablerBehavior', AjaxControlToolkit.BehaviorBase);
