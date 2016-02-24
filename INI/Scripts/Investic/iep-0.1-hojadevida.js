$(function () {
    $("#Institution").autocomplete({        
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "/Utilidades/AutoCompleteInstitucion",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data.Institutions, function (item) {
                        var valueStr = item.Name + " - " + item.Municipio;
                        return { label: valueStr, value: item.id };
                    }))
                }
            })
        },
        select: function (event, ui) {
            $("#idInstitucion").val(ui.item.value);
            $("#Institution").val(ui.item.label);
            return false;
        },
        messages: {
            noResults: "", results: function (resultsCount) { }
        }
    });
});