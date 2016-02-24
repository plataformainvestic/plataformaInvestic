/*
Slider vertical
autor: coyr
Sitio: www.xoyaz.com
*/
velocidad = 1500;
tiempoEspera = 3000;
verificar = 1;
dif = 0;
timer = 0

function moverSlider() {
    sliderAltura = $(".bloque-slider").height();
    moduloAltura = $(".modulo-slider").height() + parseFloat($(".modulo-slider").css("padding-top")) + parseFloat($(".modulo-slider").css("padding-bottom"));
    sliderTop = parseFloat($(".bloque-slider").css("top"));
    dif = sliderAltura + sliderTop;

    if (verificar == 1) {
        if (dif > moduloAltura) {
            $(".bloque-slider").animate({ top: "-=" + moduloAltura }, velocidad);
            timer = setTimeout('moverSlider()', tiempoEspera);
        }
        else {
            clearTimeout(timer);
            $(".bloque-slider").css({ top: 0 });
            timer = setTimeout('moverSlider()', 0);
        }
    }
    else {
        timer = setTimeout('moverSlider()', 1000);
    }
}
function bajarSlider() {
    if (dif >= moduloAltura * 2) {
        $(".bloque-slider").animate({ top: "-=" + moduloAltura }, velocidad);
    }
    else {
        $(".bloque-slider").css({ top: 0 });
        $(".bloque-slider").animate({ top: "-=" + moduloAltura }, velocidad);
    }
}
function subirSlider() {
    if (sliderTop <= -moduloAltura) {
        $(".bloque-slider").animate({ top: "+=" + moduloAltura }, velocidad);
    }
    else {
        $(".bloque-slider").css({ top: -sliderAltura + moduloAltura });
        $(".bloque-slider").animate({ top: "+=" + moduloAltura }, velocidad);
    }
}