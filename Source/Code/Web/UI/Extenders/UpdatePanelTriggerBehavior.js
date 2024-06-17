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

Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior = function(element)
{
    Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.initializeBase(this, [element]);

    // TODO : (Step 1) Add your property variables here
    this._UpdatePanelControlID = null;
}

Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.prototype =
{
    initialize : function()
    {
        Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.callBaseMethod(this, 'initialize');
        
        $addHandler(this.get_element(), 'click', Function.createDelegate(this, this.doUpdate));
        $addHandler(this.get_element(), 'keypress', Function.createDelegate(this, this.doUpdate));
        
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(this.handlePageLoaded);
    },

    dispose : function()
    {
        Sys.WebForms.PageRequestManager.getInstance().remove_pageLoaded(this.handlePageLoaded);
        
        $removeHandler(this.get_element(), 'click', Function.createDelegate(this, this.doUpdate));
        $removeHandler(this.get_element(), 'keypress', Function.createDelegate(this, this.doUpdate));

        Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.callBaseMethod(this, 'dispose');
    },
    
    doUpdate : function()
    {
        if (Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.CanDoPostBack && !Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
        {
            Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.CanDoPostBack = false;
            //var xCurrentTime = (new Date()).getTime();
            //if (xCurrentTime - Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.LastTriggeredUpdate > 1000)
            //{
            //    try
             //   {
             //       if (!Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
             //       {
                        __doPostBack(this._UpdatePanelControlID, '');
              //      }
              //  }
              //  catch (exc)
              //  {
              //  }
                
                //Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.LastTriggeredUpdate = xCurrentTime;
            //}
        }
    },
    
    handlePageLoaded : function()
    {
        Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.CanDoPostBack = true;
    },

    // TODO: (Step 2) Add your property accessors here
    get_UpdatePanelControlID : function()
    {
        return this._UpdatePanelControlID;
    },
    set_UpdatePanelControlID : function(value)
    {
        this._UpdatePanelControlID = value;
    }
}

Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.LastTriggeredUpdate = (new Date()).getTime();
Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.CanDoPostBack = true;

Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior.registerClass('Lusitania.Simuladores.Web.UI.Extenders.UpdatePanelTriggerBehavior', AjaxControlToolkit.BehaviorBase);
