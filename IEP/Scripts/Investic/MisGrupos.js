$(function () {
    $('#misGrupos').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });
});

function DisableButton(option) {    
    if (option) {        
        $('#CrearGrupoLink').attr("disabled", "disabled");
        $('#CrearGrupoLink').addClass("disabled");
    }
}

$('a').click(function () {
    return ($(this).attr('disabled')) ? false : true;
});