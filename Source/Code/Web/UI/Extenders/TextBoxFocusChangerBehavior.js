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

Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior = function(element) {
    Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.initializeBase(this, [element]);

    this._NextControlID = null;
    this._MaxLength = 0;
}
Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.prototype = {
    initialize : function()
    {
        Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.callBaseMethod(this, 'initialize');

        $addHandler(this.get_element(), 'keyup', Function.createDelegate(this, this.tryChangeFocus));
    },

    dispose : function()
    {
        try
        {
            $removeHandler(this.get_element(), 'keyup', Function.createDelegate(this, this.tryChangeFocus));
        }
        catch (exc)
        {
        }

        Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.callBaseMethod(this, 'dispose');
    },
    
    tryChangeFocus : function()
    {
        if (this._MaxLength > 0)
        {
            var box = this.get_element();
            if (box.value.toString().length >= this._MaxLength)
            {
                $get(this._NextControlID).focus();
            }
        }
    },

    // TODO: (Step 2) Add your property accessors here
    get_NextControlID : function()
    {
        return this._NextControlID;
    },

    set_NextControlID : function(value)
    {
        this._NextControlID = value;
    },
    
    get_MaxLength : function()
    {
        return this._MaxLength;
    },
    
    set_MaxLength : function(value)
    {
        this._MaxLength = value;
    }
}

Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.TextBoxFocusChangerBehavior', AjaxControlToolkit.BehaviorBase);
