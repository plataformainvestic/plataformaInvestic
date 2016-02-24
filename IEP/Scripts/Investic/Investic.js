$(document).ready(function () {
    $('html, body').bind('mousewheel DOMMouseScroll MozMousePixelScroll', function (event) {
        this.scrollLeft -= (event.deltaY * 15);
        if (this.scrollLeft > 0) {
            $(".carAnimate").css({ 'left': ((this.scrollLeft) * 1.66666666666667) }, function () { });
        }
        event.preventDefault();
    });

});