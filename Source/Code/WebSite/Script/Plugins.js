/**
* _collapsiblePanel.js v1.0.0
*
* Licensed under the MIT license.
* http://www.opensource.org/licenses/mit-license.php
* 
* Copyright 2013, Dumitru Chirutac
*
*/


var _collapsiblePanel = function () {
    this.panels = $('.CollapsiblePanel');    
    this.init();
};

_collapsiblePanel.prototype = {


    init: function () {
        var self = this;

        $.each(self.panels, function (index, el) {
            var _btn = $(el).find('.PanelHeader > img'),
            _panel = $(el).find('.PanelBody').addClass('Opened');
            
            _btn.unbind();
            _btn.click(function (event) {
                event.preventDefault();

                if (_panel.hasClass('Opened')) {
                    self.collapse(_panel, _btn);
                } else {
                    self.expande(_panel, _btn);
                }

            });
        });
    },
    expande: function (el, btn) {
        el.animate({ height: el.data('tempHeight') }, 500, function () {
            el.css({ 'height': 'auto' });
            var newImgSrc = btn.attr('src').substring(0, btn.attr('src').lastIndexOf('/')) + '/hide' +
                            btn.attr('src').substring(btn.attr('src').lastIndexOf('.'));
            btn.attr('src', newImgSrc);
        }).addClass('Opened');
    },
    collapse: function (el, btn) {
        el.data('tempHeight', el.height() + 'px').animate({ height: '0px' }, 500, function () {
            var newImgSrc = btn.attr('src').substring(0, btn.attr('src').lastIndexOf('/')) + '/show' +
                            btn.attr('src').substring(btn.attr('src').lastIndexOf('.'));
            btn.attr('src', newImgSrc);
        }).removeClass('Opened');
    }
};




/* Retain element focus after UpdatePanel postback
***************************************************/
var lastFocusedControlId = "";

function focusHandler(e) {
    document.activeElement = e.originalTarget;
}

function appInit() {
    if (typeof (window.addEventListener) !== "undefined") {
        window.addEventListener("focus", focusHandler, true);
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(pageLoadingHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoadedHandler);
}

function pageLoadingHandler(sender, args) {
    lastFocusedControlId = typeof (document.activeElement) === "undefined"
        ? "" : document.activeElement.id;
}

function focusControl(targetControl) {
    if (Sys.Browser.agent === Sys.Browser.InternetExplorer) {
        try {
            var focusTarget = targetControl;
            if (focusTarget && (typeof (focusTarget.contentEditable) !== "undefined")) {
                oldContentEditableSetting = focusTarget.contentEditable;
                focusTarget.contentEditable = false;
            }
            else {
                focusTarget = null;
            }
            targetControl.focus();
            if (focusTarget) {
                focusTarget.contentEditable = oldContentEditableSetting;
            }
        }
        catch (err) {         
        }
        
    }
    else {
        try {
            targetControl.focus();
        }
        catch (err) {
        }
       
    }
}

function pageLoadedHandler(sender, args) {
    if (typeof (lastFocusedControlId) !== "undefined" && lastFocusedControlId != "") {
        var newFocused = $get(lastFocusedControlId);
        if (newFocused) {
            focusControl(newFocused);
        }
    }
}

Sys.Application.add_init(appInit);