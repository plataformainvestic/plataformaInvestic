//$("#tblUsuarioPlataforma_ID").autocomplete({
//    appendTo: "#register-research-group",
//    autoFocus: true,
//    source: function (request, response) {
//        $.ajax({
//            url: "/IntegrantesGrupoInvController/AutoCompleteIntegrantesGrupoInv",
//            type: "POST",
//            dataType: "json",
//            data: { term: request.term },
//            success: function (data) {
//                response($.map(data.Integrantes, function (item) {
//                    var valueStr = item.Identificacion + " - " + item.Name;
//                    return { label: valueStr, value: item.id };
//                }))
//            }
//        })
//    },
//    select: function (event, ui) {
//        $("#idIntegrantes").val(ui.item.value);
//        $("#Integrante").val(ui.item.label);
//        return false;
//    },
//    messages: {
//        noResults: "", results: function (resultsCount) { }
//    }
//});


$("#tblUsuarioPlataforma_ID").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/IntegrantesGrupoInvController/AutoCompleteIntegrantesGrupoInv",
            type: "POST",
            datatype: "json",
            data: { searchText: request.term, maxResults: 10 },
            success: function (data) {
                response($.map(data, function (item) {
                    return { label: item.Name, value: item.Name, id: item.Id }
                }))
            }
        })
    },
    select: function (event, ui) {
        $("#Id").val(ui.item.id);
    }

});