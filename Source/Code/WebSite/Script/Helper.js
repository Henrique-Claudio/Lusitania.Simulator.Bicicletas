var printMsg;

function loadHelpersQuotation(printServerMsg) {
    printMsg= printServerMsg;
}
function PrintQuotationAction(obj) {
    if ($("input[name*='nome']").val().length == 0 || $("input[name*='nrContrib']").val().length == 0 || $("input[name*='telefone']").val().length == 0) {
        $.ConfirmBox(printMsg, 'Simulação Bicletas', '3', obj.name);
    }; 
    return false;
}