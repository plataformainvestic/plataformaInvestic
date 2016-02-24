$(function () {    
    $("#txtInstitution").autocomplete({
        autoFocus: true,
        source: function (request, response) {
            $.ajax({
                url: "/ResearchGroup/AutoCompleteInstitution",
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
});