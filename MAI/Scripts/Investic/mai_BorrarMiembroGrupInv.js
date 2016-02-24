function DeleteMember(id) {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: '<span class="icon-remove"></span>',
        title: 'Confirmación',
        width: 400,
        content: '',
        onShow: function (_dialog) {
            var content =
                '<div class="confirmbox">' +
                '<p>Desea continuar con la eliminación del registro?</p>' +
                '<button class="button primary" onclick="javascript:ConfirmDeleteMember(' + id +
                ');">Continuar</button> ' +
                '<button class="button" type="button" onclick="$.Dialog.close()">Cancel</button> ' +
                '</div>';
            $.Dialog.content(content);
        }
    });
}
function ConfirmDeleteMember(id) {
    $.Dialog.close();
    window.location.href = "/IntegrantesGrupoInv/Delete?id=".concat(id);
}