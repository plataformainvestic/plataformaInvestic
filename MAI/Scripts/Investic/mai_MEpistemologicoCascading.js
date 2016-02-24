$(function () {
    $('#tblParadigmaEpistemologico_ID').change(function () {
        if ($('#tblParadigmaEpistemologico_ID').val() == 0) {
            $('#tblTipoEstudioProy_ID').html('');
            $('#tblDiseniosProy_ID').html('');
        }
        if ($('#tblParadigmaEpistemologico_ID').val() == 1 ||
            $('#tblParadigmaEpistemologico_ID').val() == 2 ||
            $('#tblParadigmaEpistemologico_ID').val() == 3 ) {
            $.getJSON('/MetodoProy/TipoEstudioList/?tblParadigmaEpistemologico_ID=' + $('#tblParadigmaEpistemologico_ID').val(), function (data) {
                var items = '<option>Seleccione tipo de estudio</option>';
                $.each(data, function (i, tEstudio) {
                    items += "<option value='" + tEstudio.Value + "'>" + tEstudio.Text + "</option>";
                });
                $('#tblTipoEstudioProy_ID').html(items);
            });
        }
    });
});

$(function () {
    $('#tblTipoEstudioProy_ID').change(function () {
        if ($('#tblTipoEstudioProy_ID').val() == 4 && $('#tblParadigmaEpistemologico_ID').val() == 1) {
            $.getJSON('/MetodoProy/DisenioList/?tblTipoEstudioProy_ID=' + $('#tblTipoEstudioProy_ID').val(), function (data) {
                var items = '<option>Seleccione el Diseño del Estudio</option>';
                $.each(data, function (i, tDisenio) {
                    items += "<option value='" + tDisenio.Value + "'>" + tDisenio.Text + "</option>";
                });
                $('#tblDiseniosProy_ID').html(items);
            });
        } else {
            $('#tblDiseniosProy_ID').html('');
        }
    });
});