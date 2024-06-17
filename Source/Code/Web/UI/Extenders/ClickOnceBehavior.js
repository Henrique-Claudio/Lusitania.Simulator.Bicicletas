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

Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior = function(element)
{
    Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.initializeBase(this, [element]);

    // TODO : (Step 1) Add your property variables here
    //this._myPropertyValue = null;
}

Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.prototype =
{
    initialize : function()
    {
        Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.callBaseMethod(this, 'initialize');

        $addHandler(this.get_element(), 'click', Function.createDelegate(this, this.disable));
    },

    dispose : function()
    {
        $removeHandler(this.get_element(), 'click', Function.createDelegate(this, this.disable));

        Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.callBaseMethod(this, 'dispose');
    },
    
    disable : function()
    {
        this.get_element().disabled = true;
        return true;
    }

    /*
    // TODO: (Step 2) Add your property accessors here
    get_MyProperty : function() {
        return this._myPropertyValue;
    },

    set_MyProperty : function(value) {
        this._myPropertyValue = value;
    }
    */
}

Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.ClickOnceBehavior', AjaxControlToolkit.BehaviorBase);
