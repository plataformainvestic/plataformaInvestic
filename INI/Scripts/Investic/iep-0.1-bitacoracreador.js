$(function () {
    var wizard = $("#wizard").steps({
        stepsOrientation: $.fn.steps.stepsOrientation.vertical,
        onStepChanging: function (event, currentIndex, newIndex) {
            var id = $('#id').val();
            if (id == '') {
                var not = $.Notify({
                    caption: "No puedes ir al siguiente paso",
                    content: "Antes debes registrar primero tu grupo de investigación!!!",
                    timeout: 5000,
                    shadow: true,
                    style: { background: '#FF4000', color: '#FFFFFF' }
                });
                return false;
            }
            else {
                var tGroup = $('#TipoGrupo').val();
                if (tGroup == 2) {
                    return true;
                }
                else {
                    var not = $.Notify({
                        caption: "Grupo Pre-Estructurado",
                        content: "Es un grupo preestructurado no hay mas pasos.",
                        timeout: 5000,
                        shadow: true,
                        style: { background: '#FF4000', color: '#FFFFFF' }
                    });
                    return false;
                }
            }
        },
        labels: {
            cancel: "Cancel",
            current: "Paso actual:",
            pagination: "Paginación",
            finish: "Final",
            next: "Siguiente",
            previous: "Anterior",
            loading: "Cargando ..."
        }
    });

    $('input[name=TipoGrupo]').click(function () {
        var option = $('input[type="radio"]:checked').val();
        $.getJSON('/Utilidades/LineaInvestigacion', { id: option }, function (data, textstatus, jqXHR) {
            $('#idLineaInvestigacion').empty();
            $('#idLineaInvestigacion').append('<option value>Linea de Investigación</option>');
            $.each(data.ResearchLineItems, function (i, item) {
                $('#idLineaInvestigacion').append('<option value="' + item.id + '">' + item.Name + '</option>');
            });
        });
    });
    
    $('#ingresar-presupuesto').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Presupuesto",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $('#ingresar-estado-arte').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Estado del Arte",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $('#ingresar-conceptos-principales').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Conceptos",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $('#ingresar-informacion').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Información",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $('#ingresar-feria-institucional').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Imagen",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    //Propagacion
    $('#pr-insertar').dialog({
        autoOpen: false,
        height: 400,
        width: 980,
        modal: true,
        title: "Agregar Imagen",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#lnkPresupuesto").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#ingresar-presupuesto").dialog('open');
        return false;
    });
    $("#lnkCreate").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#register-research-group").dialog('open');
        return false;
    });
    $("#lnkEstadoArte").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#ingresar-estado-arte").dialog('open');
        return false;
    });
    $("#lnkConcepto").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#ingresar-conceptos-principales").dialog('open');
        return false;
    });
    $("#lnkInformacion").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#ingresar-informacion").dialog('open');
        return false;
    });
    $("#lnkFerInstitucional").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#ingresar-feria-institucional").dialog('open');
        return false;
    });

    //Propagacion
    
    $("#lnkNuevaPropagacion").on("click", function (e) {
        //e.preventDefault(); //use this or return false                
        $("#pr-insertar").dialog('open');
        return false;
    });

    //Panel desplegable
    $('a#abre_tab').on('click', function (event) {
        event.preventDefault();
        var $panel = $('.panel_lateral');
        var $tab = $('#tab_interna');
        $tab.toggleClass(function (index, className) {
            $panel.toggle();
            if (className) {
                $panel.animate({ "right": "-=300px" }, "slow");
            }
            else {
                $panel.animate({ "right": "+=300px" }, "slow");
            }
            return "expandida";
        });
    });

    $('#ValorRubro').change(function () {
        var fc = $('#ValorUnitario').val();
        var cm = $(this).val() * fc;
        $('#Total').val(cm);
    });

    $('#ValorUnitario').change(function () {
        var fc = $('#ValorRubro').val();
        var cm = $(this).val() * fc;
        $('#Total').val(cm);
    });
});

function DeletePresupuesto(id) {
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
                '<button class="button primary" onclick="javascript:ConfirmDelete(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}

function ConfirmDelete(id) {
    $.Dialog.close();
    window.location.href = "/AsistenteGruposInvestigacion/EliminarPresupuesto/".concat(id);
}