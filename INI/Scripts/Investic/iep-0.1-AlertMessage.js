function AlertMessage(msj) {
    if (msj != "") {
        var not = $.Notify({
            style: { background: 'red', color: 'white' },
            caption: "Alerta",
            content: msj,
            timeout: 5000
        });
    }
}