$(function () {

    $("#demoDialog").dialog({
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Agregar miembro",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#demoDialog2").dialog({
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Agregar vista",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });
    $("#demoDialog3").dialog({
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Agregar vista",
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

function LlamarDialog3() {
    $("#demoDialog2").load("/Prueba/Vista2")
    $("#demoDialog2").dialog("open");
}

function LlamarDialog()
{
    $("#demoDialog3").load("/Prueba/Vista3/4")
    $("#demoDialog3").dialog("open");
}

