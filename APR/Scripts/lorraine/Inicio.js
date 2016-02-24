function ponerConsulta() {

    $("#Beto1").hide();
    $("#Beto3").hide();
    $("#Beto2").show();

    $("#msjInvestiga").hide();
    $("#msjConsulta").show();
    $("#msjAprende").hide();

    $("#imgConsultaG").show();
    $("#imgInvestigaG").hide();
    $("#imgAprendeG").hide();

    $("#imgConsulta").hide();
    $("#imgInvestiga").show();
    $("#imgAprende").show();
}

function ponerRegistro() {

    $("#BetoRegistro").show();
    $("#BetoSeguimiento").hide();
    $("#BetoComunidad").hide();
    $("#BetoDocente").hide();
}

function ponerSeguimiento() {

    $("#BetoRegistro").hide();
    $("#BetoSeguimiento").show();
    $("#BetoComunidad").hide();
    $("#BetoDocente").hide();
}

function ponerComunidad() {

    $("#BetoRegistro").hide();
    $("#BetoSeguimiento").hide();
    $("#BetoComunidad").show();
    $("#BetoDocente").hide();
}

function ponerDocente() {

    $("#BetoRegistro").hide();
    $("#BetoSeguimiento").hide();
    $("#BetoComunidad").hide();
    $("#BetoDocente").show();
}

function ponerInvestiga() {

    $("#Beto2").hide();
    $("#Beto3").hide();
    $("#Beto1").show();

    $("#msjInvestiga").show();
    $("#msjConsulta").hide();
    $("#msjAprende").hide();

    $("#imgConsultaG").hide();
    $("#imgInvestigaG").show();
    $("#imgAprendeG").hide();

    $("#imgConsulta").show();
    $("#imgInvestiga").hide();
    $("#imgAprende").show();

}

function ponerAprende() {

    $("#Beto1").hide();
    $("#Beto2").hide();
    $("#Beto3").show();

    $("#msjInvestiga").hide();
    $("#msjConsulta").hide();
    $("#msjAprende").show();

    $("#imgConsultaG").hide();
    $("#imgInvestigaG").hide();
    $("#imgAprendeG").show();

    $("#imgConsulta").show();
    $("#imgInvestiga").show();
    $("#imgAprende").hide();

}

function set() {

    w = window.innerWidth;
    h = window.innerHeight;
    w2 = $("#con").width();
    h2 = $("#con").height();
    $("#con").css({ 'top': (h - h2) / 2, 'left': (w - w2) / 2 }, function () { });
    $("#con").show();
    $("#botonRegistro").css({ 'top': "20px", 'right': "20px" });
    $("#botonRegistro").show();
    $("#cargando").hide();

}
