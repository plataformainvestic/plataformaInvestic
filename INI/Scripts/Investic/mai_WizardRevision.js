$("#wizard").steps({
    headerTag: "h1",
    bodyTag: "div",
    contentMode: 2,
    transitionEffect: "slideLeft",
    stepsOrientation: "vertical",
    enableAllSteps: true,
    labels: {
        cancel: "Cancelar",
        current: "Paso actual:",
        pagination: "Pagination",
        finish: "Terminar",
        next: "Siguiente",
        previous: "Anterior",
        loading: "Loading ..."
    }

});