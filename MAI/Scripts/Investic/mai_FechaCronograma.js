$(function () {
    $("#addFechaDialog").dialog({
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Agregar Fecha al Cronograma",
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


//function agregarFechaCronograma(id) {
//    //$("#addFechaDialog").load("/FechasCronograma/Create/?id=".concat(id));
//    $("#addFechaDialog").dialog("open");
//}

$("#agregarFechaCronograma").on("click", function (e) {
    console.log("Boton Presionado");
    //e.preventDefault(); //use this or return false                
    $("#addFechaDialog").dialog('open');
    return false;
});