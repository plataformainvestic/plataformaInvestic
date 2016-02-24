$(function () {
    $("#informeProyectoDialog").dialog({
        autoOpen: false,
        height: 800,
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


function InformeProyectoFinal(id) {
    $("#informeProyectoDialog").load("/ProyectosInvestigacion/InformeProyecto/?id=".concat(id));
    $("#informeProyectoDialog").dialog("open");
}