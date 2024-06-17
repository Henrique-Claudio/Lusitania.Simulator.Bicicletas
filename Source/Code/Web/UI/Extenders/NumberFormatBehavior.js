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

Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior = function(element) {
    Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.initializeBase(this, [element]);

    this._FormatString = null;
}
Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.prototype = {
    initialize : function() {
        Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.callBaseMethod(this, 'initialize');

        this.format();
        
        var e = this.get_element();
        $addHandler(e, "change", Function.createDelegate(this, this.format));
    },

    dispose : function()
    {
        var e = this.get_element();
        
        $removeHandler(e, "change", this.format);

        Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.callBaseMethod(this, 'dispose');
    },
    
    format : function()
    {
        var target = this.get_element();
        
        var source = Number.parseLocale(target.value);
        if (!isNaN(source))
        {
            target.value = source.localeFormat(this._FormatString);
        }
    },

    get_FormatString : function()
    {
        return this._FormatString;
    },

    set_FormatString : function(value)
    {
        this._FormatString = value;
    }
}
Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.NumberFormatBehavior', AjaxControlToolkit.BehaviorBase);
