$(function () {
    $("#addFechaDialog").dialog({
        autoOpen: false,
        height: 200,
        width: 480,
        modal: true,
        title: "Agregar Fecha",
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



$("#agregarFechaCronograma").on("click", function (e) {
    $("#addFechaDialog").dialog('open');
    return false;
});