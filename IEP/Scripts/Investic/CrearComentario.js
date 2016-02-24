$(function () {
    $('#ComentariosProyecto').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });

    $('#nuevo-foro').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Nuevo Foro",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });    

    $("#lnkCrearForo").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#nuevo-foro").dialog('open');
        return false;
    });
});

function DeleteComentario(id) {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: '<span class="icon-remove"></span>',
        title: 'Confirmación',
        width: 400,
        content: '',
        onShow: function (_dialog) {
            var content =
                '<div class="confirmbox">' +
                '<p>Desea continuar con la eliminación del registro?</p>' +
                '<button class="button primary" onclick="javascript:ConfirmDelete(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}

function ConfirmDelete(id) {
    $.Dialog.close();
    window.location.href = "/AsistenteGruposInvestigacion/EliminarComentario/".concat(id);
}