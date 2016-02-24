$(document).ready(function () {   

    $('input[name=txtTypeGroup]').click(function () {
        var option = $('input[type="radio"]:checked').val();        
        $.getJSON('/ResearchGroupWizard/ResearchLineOptions/', { id: option }, function (data, textstatus, jqXHR) {            
            $('#txtResearchLineId').empty();
            $('#txtResearchLineId').append('<option value>Linea de Investigación</option>');
            $.each(data.ResearchLineItems, function (i, item) {
                $('#txtResearchLineId').append('<option value="' + item.txtId + '">' + item.txtName + '</option>');
            });
        });
    });

    $("#txtInstitution").autocomplete({
        appendTo: "#register-research-group",
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "/ResearchGroupWizard/AutoCompleteInstitution",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data.Institutions, function (item) {
                        var valueStr = item.Name + " - " + item.Municipio;
                        return { label: valueStr, value: item.ID };
                    }))
                }
            })
        },
        select: function (event, ui) {
            $("#txtInstitutionId").val(ui.item.value);
            $("#txtInstitution").val(ui.item.label);
            return false;
        },
        messages: {
            noResults: "", results: function (resultsCount) { }
        }
    });

    $('#register-research-group').dialog({
        autoOpen: false,
        height: 420,
        width: 980,
        modal: true,
        title: "Registrar Grupo",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#lnkCreate").on("click", function (e) {
        //e.preventDefault(); //use this or return false        
        $("#register-research-group").dialog('open');

        return false;
    });
});