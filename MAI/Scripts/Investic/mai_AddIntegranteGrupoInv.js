$(function () {
    $("#addMemberDialog").dialog({
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Crear Nuevo Grupo de Investigación",
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

$("#newIntegrante").on("click", function (e) {
    $("#addMemberDialog").dialog('open');
});


function AddMember(idUsuario, idGrupo) {
    console.log(idUsuario + " - " + idGrupo)
    window.location.href = "/IntegrantesGrupoInv/AddIntegrante?idUsuario=".concat(idUsuario) + "?idGrupo=".concat(idGrupo);
}

//function AgregarIntegranteGrupoInv(id, id) {
//    $("#addMemberDialog").load("/IntegrantesGrupoInv/AgregarIntegrantes/?id=".concat(id));
//    $("#addMemberDialog").dialog("open");
//}