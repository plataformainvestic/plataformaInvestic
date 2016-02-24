$("#wizard").steps({
    headerTag: "h1",
    bodyTag: "div",
    contentMode: 2,
    transitionEffect: "slideLeft",
    stepsOrientation: "vertical",
    enableAllSteps: true,
    labels: {
        cancel: "Cancelar",
        current: "Paso actual:",
        pagination: "Pagination",
        finish: "Terminar",
        next: "Siguiente",
        previous: "Anterior",
        loading: "Loading ..."
    },

    stepsOrientation: $.fn.steps.stepsOrientation.vertical,

    onStepChanging: function (event, currentIndex, newIndex) {

        var form0 = $('#PresentacionProyecto');
        var form1 = $('#ProblemaInvestigacionProy');
        var form2 = $('#MarcoReferenciaProy');
        var form3 = $('#MetodoProy');
        var form4 = $('#CaracteristicasProy');
        

        //Post presentacion proyecto
        if (currentIndex == 0 && newIndex == 1) {
            $('#PresentacionProyecto').submit(
                $.ajax({
                    type: "POST",
                    url: form0.attr('action'),
                    data: form0.serialize()
                })
            );
        }
        //return true;
        //Post ProblemaInvestigacionProy
        if (currentIndex == 1 && newIndex == 2) {
            $('#ProblemaInvestigacionProy').submit(
                $.ajax({
                    type: "POST",
                    url: form1.attr('action'),
                    data: form1.serialize()
                })
            );
        }
        //return true;
        //Post MarcoReferenciaProy
        if (currentIndex == 2 && newIndex == 3) {
            $('#MarcoReferenciaProy').submit(
                $.ajax({
                    type: "POST",
                    url: form2.attr('action'),
                    data: form2.serialize()
                })
            );
        }
        //return true;
        //Post MetodoProy
        if (currentIndex == 3 && newIndex == 4) {
            $('#MetodoProy').submit(
                $.ajax({
                    type: "POST",
                    url: form3.attr('action'),
                    data: form3.serialize()
                })
            );
        }
        //return true;
        //Post CaracteristicasProy
        if (currentIndex == 5 && newIndex == 6) {
            $('#CaracteristicasProy').submit(
                $.ajax({
                    type: "POST",
                    url: form4.attr('action'),
                    data: form4.serialize()
                })
            );
        }
        return true;
    },


    onFinished: function (event, currentIndex) {
        //var form5 = $('#ReferenciasProy');
        $('#ReferenciasProy').submit(
                $.ajax({
                    type: "POST",
                    url: $('#ReferenciasProy').attr('action'),
                    data: $('#ReferenciasProy').serialize()
                })
            );

        var id = $(this).attr('data-id');
        FinalizarProyecto(id);
        return true;
    }

    


    //onFinished: function (event, currentIndex) {
    //    var form5 = $('#ReferenciasProy');
    //    //Post ReferenciasProy
    //    $('#ReferenciasProy').submit(
    //        $.ajax({
    //            type: "POST",
    //            url: form5.attr('action'),
    //            data: form5.serialize()
    //        })
    //        );

    //    var id = $(this).attr('data-id');
    //    FinalizarProyecto(id);
    //    return false;
    //}
});

$(function () {
    $("#informeProyectoDialog").dialog({
        scroll: top,
        autoOpen: false,
        height: 600,
        width: 980,
        modal: true,
        title: "Presentación de la Propuesta del Proyecto",
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


function FinalizarProyecto(id) {
    $("#informeProyectoDialog").load("/ProyectosInvestigacion/InformeProyecto/?id=".concat(id));
    $("#informeProyectoDialog").dialog("open");
}

//Cronograma
$(function () {
    $("#addFechaDialog").dialog({
        autoOpen: false,
        height: 500,
        width: 600,
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

$("#agregarFechaCronograma").on("click", function (e) {
    //e.preventDefault(); //use this or return false                
    $("#addFechaDialog").dialog('open');
    return false;
});

function BorrarFechaCronograma(id) {
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
                '<button class="button primary" onclick="javascript:ConfirmBorrarFechaCronograma(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}
function ConfirmBorrarFechaCronograma(id) {
    $.Dialog.close();
    window.location.href = "/FechasCronograma/BorrarFechaCronograma?id=".concat(id);
}

//Presupuesto
$(function () {
    $("#addRubroDialog").dialog({
        autoOpen: false,
        height: 500,
        width: 600,
        modal: true,
        title: "Agregar Rubro al Presupuesto",
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

$("#agregarPresupuestoProy").on("click", function (e) {
    //e.preventDefault(); //use this or return false                
    $("#addRubroDialog").dialog('open');
    return false;
});

function BorrarRubroPresupuesto(id) {
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
                '<button class="button primary" onclick="javascript:ConfirmBorrarRubroPresupuesto(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}
function ConfirmBorrarRubroPresupuesto(id) {
    $.Dialog.close();
    window.location.href = "/RubrosPresupuesto/BorrarRubroPresupuesto?id=".concat(id);
}




$(function () {
    $('#miTablaCronograma').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });
});

$(function () {
    
    $('#miTablaPresupuesto').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });
});
