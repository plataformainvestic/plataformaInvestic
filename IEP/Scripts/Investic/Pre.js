$(function () {
    function UploadFileCommitment(id) {
        $('#UploadFile').load("/DetalleSuministros/CargarArchivo/".concat(id));
        $('#UploadFile').dialog('open');
    }

    function UploadFileAceptance(id) {
        $('#UploadFile').load("/DetalleSuministros/CargarArchivo/".concat(id));
        $('#UploadFile').dialog('open');
    }
});