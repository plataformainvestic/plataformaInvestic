$(function () {
    $('#BinnacleTwoTable').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });

    $("#AddMainResearchQuestion").dialog({
        autoOpen: false,
        height: 350,
        width: 980,
        modal: true,
        title: "Agregar pregunta",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#AddBegininResearchQuestions").dialog({
        autoOpen: false,
        height: 350,
        width: 980,
        modal: true,
        title: "Agregar pregunta",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#AddAditionalsResearchQuestions").dialog({
        autoOpen: false,
        height: 350,
        width: 980,
        modal: true,
        title: "Agregar pregunta",
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#dialog-edit").dialog({
        title: 'Modificar',
        autoOpen: false,
        resizable: false,
        height: 350,
        width: 980,
        show: { effect: 'drop', direction: "up" },
        modal: true,
        draggable: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();
            $(this).load(url);
        }
    });

    $("#dialog-confirm").dialog({
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 400,
        show: { effect: 'drop', direction: "up" },
        modal: true,
        draggable: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide();

        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
                window.location.href = url;
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $(".lnkEdit").on("click", function (e) {
        //e.preventDefault();        
        url = $(this).attr('href');
        $(".ui-dialog-title").html("Modificar pregunta");
        $("#dialog-edit").dialog('open');

        return false;
    });

    $(".lnkDelete").on("click", function (e) {
        // e.preventDefault(); use this or return false
        url = $(this).attr('href');
        $("#dialog-confirm").dialog('open');

        return false;
    });

    $('#MemberGroupTable').dataTable({
        "sPaginationType": "full_numbers",
        "oLanguage": {
            "sUrl": "../../Lang/es-CO.txt"
        }
    });
});