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

Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior = function(element) {
    Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.initializeBase(this, [element]);

    // TODO : (Step 1) Add your property variables here
    this._SelectableControlID = null;
}
Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.prototype =
{
    initialize : function()
    {
        Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.callBaseMethod(this, 'initialize');
        
        this.apply();        
        $addHandler($get(this._SelectableControlID), "propertychange", Function.createDelegate(this, this.apply));
    },

    dispose : function()
    {
        $removeHandler($get(this._SelectableControlID), "propertychange", Function.createDelegate(this, this.apply));
        
        Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.callBaseMethod(this, 'dispose');
    },
    
    apply : function()
    {
        var target = this.get_element();
        var trigger = $get(this._SelectableControlID);
        
        if (trigger.checked)
        {
            target.style.display = '';
        }
        else
        {
            target.style.display = 'none';
        }
    },

    get_SelectableControlID : function()
    {
        return this._SelectableControlID;
    },

    set_SelectableControlID : function(value)
    {
        this._SelectableControlID = value;
    }
}
Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.HiddenPanelBehavior', AjaxControlToolkit.BehaviorBase);
