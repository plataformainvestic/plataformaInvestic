//$(function () {
//    var wizard = $("#wizard").steps({
//        headerTag: "h1",
//        bodyTag: "div",
//        contentMode: 2,
//        transitionEffect: "slideLeft",
//        stepsOrientation: "vertical",
//        enableAllSteps: true,
//        labels: {
//            cancel: "Cancel",
//            current: "Paso actual:",
//            pagination: "Paginación",
//            finish: "Final",
//            next: "Siguiente",
//            previous: "Anterior",
//            loading: "Cargando ..."
//        },
//        stepsOrientation: $.fn.steps.stepsOrientation.vertical,
//        onStepChanging: function (event, currentIndex, newIndex) {
//            if (currentIndex == 0 && newIndex == 1) {
//                console.log("Formulario");
//                console.log(this.attributes)
//                var form = $('#PresentacionProyecto');
//                $('#PresentacionProyecto').submit(
//                    $.ajax({
//                        type: "POST",
//                        url: form.attr('action'),
//                        data: form.serialize(),
//                    })
//                    );
//                return true;
//            }
//        }
//    });
//});


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
        var form1 = $('#ProblemaInvestigacionProy')
        var form2 = $('#MarcoReferenciaProy')
        var form3 = $('#MetodoProy') //Falta implementar Cascading
        var form4 = $('#CaracteristicasProy')

        //Post presentacion proyecto
        if (currentIndex == 0 && newIndex == 1) {
            $('#PresentacionProyecto').submit(
                $.ajax({
                    type: "POST",
                    url: form0.attr('action'),
                    data: form0.serialize(),
                })
            );
            return true;
        }
        //Post ProblemaInvestigacionProy
        if (currentIndex == 1 && newIndex == 2) {
            $('#ProblemaInvestigacionProy').submit(
                $.ajax({
                    type: "POST",
                    url: form1.attr('action'),
                    data: form1.serialize(),
                })
            );
            return true;
        }
        //Post MarcoReferenciaProy
        if (currentIndex == 2 && newIndex == 3) {
            $('#MarcoReferenciaProy').submit(
                $.ajax({
                    type: "POST",
                    url: form2.attr('action'),
                    data: form2.serialize(),
                })
            );
            return true;
        }
        //Post MetodoProy
        if (currentIndex == 3 && newIndex == 4) {
                $('#MetodoProy').submit(
                    $.ajax({
                        type: "POST",
                        url: form3.attr('action'),
                        data: form3.serialize(),
                    })
                );
            return true;
        }
        //Post CaracteristicasProy
        if (currentIndex == 4 && newIndex == 5) {
            $('#CaracteristicasProy').submit(
                $.ajax({
                    type: "POST",
                    url: form4.attr('action'),
                    data: form4.serialize(),
                })
            );
            return true;
        }
        //Post Cronograma
        if (currentIndex == 5 && newIndex == 6) {
            return true;
        }
        //Post Presupuesto
        if (currentIndex == 6 && newIndex == 7) {
            return true;
        }


    },

    onFinished: function (event, currentIndex) {
        var form5 = $('#ReferenciasProy')
        //Post ReferenciasProy
        $('#ReferenciasProy').submit(
                $.ajax({
                    type: "POST",
                    url: form5.attr('action'),
                    data: form5.serialize(),
                })
            );


        var id = $(this).attr('data-id')

        FinalizarProyecto(id)
        //alert("Su Proyecto fue completado en su totalidad\n Puede realizar cambios o modificaciones en cualquier momento ");
        //$("#informacion-proyecto").dialog('open');
        return false;
    }

});


$('#informacion-proyecto').dialog({
    scroll: top,
    autoOpen: false,
    height: 400,
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

$("#lnkInformeProyecto").on("click", function (e) {
    //e.preventDefault(); //use this or return false                
    $("#informacion-proyecto").dialog('open');
    return false;
});

$(function () {
    $("#informeProyectoDialog").dialog({
        scroll: top,
        autoOpen: false,
        height: 400,
        width: 680,
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

$(function () {
    $('#miTablaCronograma').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
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

//function FinalizarProyecto(id) {
//    console.log(id + 'Entrando a la funcion')
//    $.Dialog({
//        overlay: true,
//        shadow: true,
//        flat: true,
//        icon: '<span class="icon-remove"></span>',
//        title: 'Confirmación',
//        width: 400,
//        content: '',
//        onShow: function (_dialog) {
//            var content =
//                '<div class="confirmbox">' +
//                '<p>¿Desea Finalizar la edición del proyecto?</p>' +
//                '<p>El proyecto pasara a Revision por parte de los Evaluadores Investic</p>' +
//                '<button class="button primary" onclick="javascript:ConfirmFinalizarProyecto(' + id +
//                ');">Finalizar Proyecto</button> ' +
//                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
//                '</div>';
//            $.Dialog.content(content);
//        }
//    });
//}
//function ConfirmFinalizarProyecto(id) {
//    $.Dialog.close();
//    window.location.href = "".concat(id);
//}


