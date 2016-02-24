$(function () {
    $('.btn-tmp').on('click', function () {
        var not = $.Notify({
            caption: "Investic",
            content: "Funcionalidad en proceso de impalntación",
            timeout: 5000,
            shadow: true,
            style: { background: '#FF4000', color: '#FFFFFF' }
        });
    });
});