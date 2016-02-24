$(function () {
    $("#crearProyectoDialog").dialog({
        autoOpen: false,
        height: 400,
        width: 680,
        modal: true,
        title: "Crear Nuevo Proyecto de Investigación",
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

$("#AddNuevoProyecto").on("click", function (e) {
    $("#crearProyectoDialog").dialog('open');
});


//function AgregarNuevoProyecto(id) {
//    $("#crearProyectoDialog").load("/ProyectosInvestigacion/CrearProyecto/?id=".concat(id));
//    $("#crearProyectoDialog").dialog("open");
//}