$(function () {

    $("#AddMember").dialog({
        autoOpen: false,
        height: 480,
        width: 980,
        modal: true,
        title: "Agregar miembro",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "fade",
            duration: 1000
        }
    });

    $('#CoresearchTeachersTable').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });
});

function DeleteMember(id) {
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
                '<button class="button primary" onclick="javascript:ConfirmDeleteMember(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}

function ConfirmDeleteMember(id) {
    $.Dialog.close();
    window.location.href = "/ResearchGroupWizard/DeleteGroupMember?id=".concat(id);
}