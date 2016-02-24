$('#crearGrupoDialog').dialog({
    scroll: top,
    autoOpen: false,
    height: 400,
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

$("#newGroup").on("click", function (e) {
    //e.preventDefault(); //use this or return false                
    $("#crearGrupoDialog").dialog('open');
    return false;
});


//$(function () {
//    $("#crearGrupoDialog").dialog({
//        autoOpen: false,
//        //height: 600,
//        //width: 980,
//        modal: true,
//        title: "Crear Nuevo Grupo de Investigación",
//        show: {
//            effect: "blind",
//            duration: 1000
//        },
//        hide: {
//            effect: "blind",
//            duration: 1000
//        }
//    });
//});


//function CrearGrupo() {
//    $("#crearGrupoDialog").load("/GruposInvestigacion/CrearGrupoInv");
//    $("#crearGrupoDialog").dialog("open");
//}

//function EliminarIntegrante(id) {
//    var url = "/Controllers/MAI/IntegrantesGrupoInvController";
//    $.getJSON(url, function (result) {
//        if (result != "Eliminado") {
//            alert("error al eliminar el registro");
//        }
//    })
//}

