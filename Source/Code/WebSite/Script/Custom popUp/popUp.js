/***
* Author : Dumitru Chirutac
*/
// dependencias jquery 1.7.2.

(function ($) {

    /********************************
    *           InfoBox             *
    ********************************/

    $.fn.InfoBox = function () {
        //validate if exist more open infobox
        if ($('#InfoBox_div').length) {
            // A PopUp is already shown on the page:
            return false;
        }
        //create markup
        var _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body'),
            _markup = [
            '<div id="InfoBox_div" class="ui-infoBox">',
	            '<div class="ui-infoBox-overlay"></div>',
	            '<div class="ui-infoBox-Wrapper">',
		            '<div class="ui-infoBox-Header">',
			            '<a class="ui-infoBox-close" title="Fechar Janela">X</a>',
                        $(this).attr('infoBoxTitle').length > 0 ? '<h4>' + $(this).attr('infoBoxTitle') + '</h4>' : '',
		            '</div>',
		            '<div class="ui-infoBox-Body">',
                    $('#' + $(this).attr('infoBox')).clone().html(),
		            '</div>',
	            '</div>',
            '</div>'
		].join('');

        //add click event
        $(this).unbind().click(function (e) {
            e.preventDefault();
            //append infobox to body
            $(_markup).hide().appendTo(_body).fadeIn(0, function () {

                // if body is scrollind add to top shadow
                var _popUpBody = _body.find('#InfoBox_div > .ui-infoBox-Wrapper > .ui-infoBox-Body'); // wrap a div around the contents
                var _contents = _popUpBody.wrapInner('<div>').children();
                var _contentsHeight = _contents.outerHeight(); // read the inner divs height
                var _contentsWidth = _contents.outerWidth(); // read the inner divs width                        

                if (_body.height() < _contentsHeight) {
                    _body.find('#InfoBox_div > .ui-infoBox-Wrapper')
                        .height(parseInt(_body.height() * 0.7))
                        .width(_contentsWidth + 50);
                } else {

                    _body.find('#InfoBox_div > .ui-infoBox-Wrapper')
                        .height(_contentsHeight + 50)
                        .width(_contentsWidth + 50);
                }

                _body.find('#InfoBox_div > .ui-infoBox-Wrapper').css({
                    'top': '50%',
                    'left': '50%',
                    'margin-top': '-' + _body.find('#InfoBox_div > .ui-infoBox-Wrapper').height() / 2 + 'px',
                    'margin-left': '-' + _body.find('#InfoBox_div > .ui-infoBox-Wrapper').width() / 2 - 5 + 'px'
                });


                if (_popUpBody.height() < _contentsHeight) {
                    _popUpBody.css({ 'overflow-y': 'scroll' })
                    _popUpBody.scroll(function () {
                        if (_popUpBody.scrollTop() > 5 && !_popUpBody.hasClass('topShadow')) {
                            _popUpBody.addClass('topShadow');
                        }
                        if (_popUpBody.scrollTop() <= 5 && _popUpBody.hasClass('topShadow')) {
                            _popUpBody.removeClass('topShadow');
                        }
                    });
                }

            });


            _body.find('#InfoBox_div a.ui-infoBox-close').click(function () {
                $.fn.InfoBox.Close();
            });



        });

    }

    $.fn.InfoBox.Close = function () {
        var _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body');

        _body.find('#InfoBox_div').unbind().hide().remove();
    }

    /*********** Loader Box *********************/
    /*
    * Mostra o loader quando se trata dos pedidos Back End 
    */

    $.fn.LoaderShow = function () {
        var _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body');

        if (_body.find('#loader_tmp').length > 0) {
            return;
        }

        var markup = [
                '<div id="loader_tmp" class="ui-infoBox">',
                    '<div class="ui-infoBox-overlay"></div>',
                    '<div class="ui-infoBox-loader">',
                        '<img src="Script/Custom popUp/img/loader.gif" class="ui-infoBox-loader-img">',
                        '<span class="ui-infoBox-loader-span">Carregamento em curso ...</span>',
                        '<div style="clear:both;"></div>',
                    '</div>',
                '</div>'
		].join('');

        $(markup).hide().appendTo(_body).fadeIn();
    }

    $.fn.LoaderClose = function () {
        var _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body');
        var loader = _body.find('#loader_tmp');
        if (loader.length == 0) {
            return;
        }
        loader.fadeOut(function () {
            $(this).remove();
        });
    }

    /*********** Alert Box *********************/
    /**
    * Tipos de Alert { "1" - info
    *                  "2" - erro
    *                  "3" - alert
    *                }
    */


    $.CustomAlert = function (params) {

        var 
            _windows = (window.location != window.parent.location) ? $(window.parent) : $(window),
            _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body'),
            _spanTipoAlert = params.type == "1" ? "i" : params.type == "2" ? "?" : params.type == "3" ? "!" : "i",
            _headerClass = params.type == "1" ? "infoHeader" : params.type == "2" ? "erroHeader" : params.type == "3" ? "alertHeader" : "infoHeader",
            _buttonHTML = '',
		    _i = 0;

        // A PopUp is already shown on the page:
        if (_body.find('#CustomPopUp').length) {
            return false;
        }

        $.each(params.buttons, function (name, obj) {
            // Generating the markup for the buttons:
            var _classButton = "ui-infoBox-button";
            if (obj.CssClass != undefined) {
                _classButton += " " + obj.CssClass;
            }
            _buttonHTML += '<a class="' + _classButton + '" ' + (_i > 0 ? ' style="margin-right:10px;"' : '') + '>' + name + '<span></span></a>';

            if (!obj.action) {
                obj.action = function () { };
            }
            _i++;
        });

        params.message = params.message.replace(/\t/gi, "&nbsp;").replace(/\n/gi, "<br />");

        var markup = [
                '<div id="CustomPopUp" class="ui-infoBox">',
	                '<div class="ui-infoBox-overlay"></div>',
	                '<div class="ui-ShowBox-Alert">',
		                '<div class="ui-infoBox-alert-header">',
			                '<div class="' + _headerClass + '">',
				                '<span>', _spanTipoAlert, '</span>',
			                '</div>',
			                '<h3 class="' + _headerClass + ' titleHeader">', params.title, '</h3>',
			                '<div style="clear:both;"></div>',
		                '</div>',
		                '<div class="ui-infoBox-alert-body">', params.message, '</div>',
		                '<div class="ui-infoBox-alert-footer">',
			                _buttonHTML,
                            '<div style="clear:both"></div>',
		                '</div>',
	                '</div>',
                '</div>',
		].join('');

        //workaround to verify if loader is visible;
        var _loader = _body.find('#loader_tmp');
        if (_loader != null) {
            _loader.remove();
        }


        $(markup).hide().appendTo(_body).fadeIn();

        var buttons = _body.find('#CustomPopUp .ui-infoBox-button');
        _i = 0;

        $.each(params.buttons, function (name, obj) {
            buttons.eq(_i++).click(function () {
                $.CustomAlert.Close(obj.NotCloseEffect);
                obj.action();
                return false;
            });
        });
        //set dinamic top position
        var popupTemp = _body.find('#CustomPopUp .ui-ShowBox-Alert');
        popupTemp.attr('style', 'top: ' + ((_windows.height() - popupTemp.height()) / 2) + 'px; left: ' + ((_windows.width() - popupTemp.width()) / 2) + 'px;');

    };

    $.CustomAlert.Close = function (IsfadeOutEffect) {
        var _body = (window.location != window.parent.location) ? $($(window.parent.document).find('body')) : $('body');
        var PopUp = _body.find('#CustomPopUp');
        if (PopUp == null) {
            return;
        }

        if (IsfadeOutEffect) {
            PopUp.fadeOut(function () {
                $(this).remove();
            });
        } else {
            PopUp.remove();
        }

    };

    /**
    * Custom Alert Box
    **/
    $.AlertBox = function (message, title, tipoAlert) {
        Sys.Application.remove_load($.AlertBox);

        $.CustomAlert({
            title: title,
            message: message,
            type: tipoAlert,
            buttons: {
                'OK': {
            }
        }
    });
};
/**
* Custom Confirm Box
**/
$.ConfirmBox = function (message, title, type, name) {
    $.CustomAlert({
        title: title,
        message: message,
        type: type,
        buttons: {
            'N&Atilde;O': {
                CssClass: 'link'
            },
            /*'Cancel': { },*/
            'SIM': {
                action: function () {
                    __doPostBack(name, '');
                }
            }
        }
    });
};
})(jQuery);