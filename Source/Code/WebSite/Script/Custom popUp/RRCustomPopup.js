//http://stackoverflow.com/questions/326069/how-to-identify-if-a-webpage-is-being-loaded-inside-an-iframe-or-directly-into-t
function checkIfIframe() {
    if (self == top) {
        return false;
    }
    else {
        return true;
    }
}

function loadPopup(popup_trigger, overlay_div, overlay, popup_text, widthCtrlDiv, event) {

    if (overlay != null) {
        overlay.fadeIn("slow");
    }
    overlay_div.fadeIn("slow");

    var posOD = popup_trigger.position();
    var x = overlay_div.width();
    var y = overlay_div.height();

    /////////////////////
    //Saber qual o tamanho da popup
    var H_popup = popup_text.height();
    var W_popup = popup_text.width();

    //Saber Posição do trigger
    var pos_trigger = popup_trigger.position();
    var T_trigger = pos_trigger.top;
    var L_trigger = pos_trigger.left;

    //Saber Tamanho da innerHeight()
    var H_window, W_window;
    if (checkIfIframe()) {
        H_window = window.parent.document.documentElement.clientHeight;
        W_window = window.parent.document.documentElement.clientWidth;
    }
    else {
        H_window = $(window).innerHeight();
        W_window = $(window).innerWidth();
    }

    /////////////////////

    var newHeight = (H_window / 2) - (popup_text.height() / 2);
    if (newHeight < $(window).scrollTop()) {
        newHeight = $(window).scrollTop();
    }
    var newWidth = (W_window / 2) - (popup_text.width() / 2);
    if (newWidth < $(window).scrollLeft()) {
        newWidth = $(window).scrollLeft();
    }

    /*var newHeight = ($(window).innerHeight() / 2) - (popup_text.height() / 2);
    if (newHeight < $(window).scrollTop()) {
    newHeight = $(window).scrollTop();
    }
    var newWidth = ($(window).innerWidth() / 2) - (popup_text.width()/2);
    if (newWidth < $(window).scrollLeft()) {
    newWidth = $(window).scrollLeft();
    }*/

    var _windows = (window.location != window.parent.location) ? $(window.parent) : $(window),
            _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body');

    //$("#" + popup_trigger.get(0).id).css({ top: '100px' });

    //$("#" + popup_trigger.get(0).id).css({ top: 'px' });

    //newHeight = event.pageY - 3;//   (H_popup / 2);
    //newWidth = (W_window / 2) - (W_popup / 2); //event.pageX - (W_popup / 2);

    //popupTemp.attr('style', 'top: ' + ((_windows.height() - popupTemp.height()) / 2) + 'px;');
    /*overlay_div.offset({
    //top: $(window).height() / 2 - overlay_div.height() / 2,
    //left: $(window).width() / 2 - overlay_div.width() / 2
    top: (newHeight)//(event.pageY - popup_trigger.height() - 10)
    , left: (newWidth)//(event.pageX - popup_trigger.width() - 100)
    });*/
    var my_window;
    if (checkIfIframe()) {
        my_window = $(parent);
    }
    else {
        my_window = $(parent);
    }
    H_window = my_window.height();
    W_window = my_window.width();

    //$(".popup")/*$('#' + popup_trigger.get(0).id)*/.css('top', ((H_window - document.getElementById('popup_label').offsetHeight) / 2));
    //$(".popup")/*$('#'+popup_trigger.get(0).id)*/.css('left', ((W_window - document.getElementById('popup_label').offsetWidth) / 2));
    //$('#' + popup_text.get(0).id).css('top', ((H_window - document.getElementById('popup_label').offsetHeight) / 2));
    //$('#' + popup_text.get(0).id).css('left', ((W_window - document.getElementById('popup_label').offsetWidth) / 2));

    //var t = $(window).innerWidth()-40;
    overlay_div.width(widthCtrlDiv.width() - 40);
    

    var olPos = overlay_div.offset();

    overlay_div.css({ top: widthCtrlDiv.position().top })

    popup_text.css({
        "max-height": H_window + "px"
            ,
        "max-width": W_window + "px"
    });

}

function closePopup(overlay, overlay_div) {
    overlay.fadeOut("slow");
    overlay_div.fadeOut("slow");
}

function popup_Manager(popup_trigger, close_btn, overlay_div, overlay, popup_text, widthCtrlDiv) {
    popup_trigger.click(function (event) {
        loadPopup(popup_trigger, overlay_div, overlay, popup_text, widthCtrlDiv, event);
    });

    close_btn.click(function () {
        closePopup(overlay, overlay_div);
    });
}
