$(function () {
    $('#misGrupos').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });

    $("#lnkCreate").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        if ($(this).attr('disabled')) {
            return false
        }
        $("#register-research-group").dialog('open');
        return false;
    });

    $("#lnkEdit").on("click", function (e) {
        //e.preventDefault(); //use this or return false                        
        $("#edit-research-group").dialog('open');
        return false;
    });

    $('#register-research-group').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Registrar Grupo de Investigación",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $('#edit-research-group').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Modificar Grupo Investigación",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });
});

function DisableButton(option) {
    console.log(option);
    if (option) {        
        $('#lnkCreate').attr("disabled", "disabled");
        $('#lnkCreate').addClass("disabled");
        $('#lnkEdit').show();
        $('#lnkCreate').hide();
    }
    else {
        $('#lnkEdit').hide();
        $('#lnkCreate').show();
    }
}