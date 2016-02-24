function valt(id) {

    switch (id) {

        case 1:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa1.1.png'>");
            break;
        case 2:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa2.1.png'>");
            break;
        case 3:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa3.1.png'>");
            break;
        case 4:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa4.1.png'>");
            break;
        case 5:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa5.1.png'>");
            break;
        case 6:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa6.1.png'>");
            break;
        case 7:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa7.1.png'>");
            break;
        case 8:
            $('#viewalternativas').html("<img src='../../images/alternativas/Alternativa8.1.png'>");
            break;
    }
    
}

function vconocenos(id) {
    switch (id)
    {
        case 1:
            //conocenos
            $('#conocenos').show();
            $('#general').hide();
            $('#equipotrabajo').hide();
            $('#alternativas').hide();
            $('#asesoreszona').hide();
            $('#tutorestic').hide();
            $('body').css('background-image', 'url(../../images/conocenos/img_fondo.png)');
            break;

        case 2:
            //general
            $('#conocenos').hide();
            $('#equipotrabajo').hide();
            $('#alternativas').hide();
            $('#asesoreszona').hide();
            $('#tutorestic').hide();
            $('#general').show();
            break;
            
        case 3:
            //Equipotrabajo
            $('#conocenos').hide();
            $('#general').hide();
            $('#alternativas').hide();
            $('#asesoreszona').hide();
            $('#tutorestic').hide();
            $('#equipotrabajo').show();
            $('body').css('background-image', 'url(../../images/conocenos/equipotrabajo/equipodetrabajo_fondo.png)');
            break;
        case 4:
            //alternativas
            $('#conocenos').hide();
            $('#general').hide();
            $('#alternativas').show();
            $('#asesoreszona').hide();
            $('#tutorestic').hide();
            $('#equipotrabajo').hide();
            $('body').css('background-image', 'url(../../images/conocenos/alternativas/Fondo.png)');
            break;
        case 5:
            //Asesores Zona
            $('#conocenos').hide();
            $('#general').hide();
            $('#alternativas').hide();
            $('#asesoreszona').show();
            $('#tutorestic').hide();
            $('#equipotrabajo').hide();
            $('body').css('background-image', 'url(../../images/conocenos/AsesoresZona/bg_fondo.png)');
            break;
        case 6:
            //tutores TIC
            $('#conocenos').hide();
            $('#general').hide();
            $('#alternativas').hide();
            $('#asesoreszona').hide();
            $('#tutorestic').show();
            $('#equipotrabajo').hide();
            $('body').css('background-image', 'url(../../images/conocenos/TutoresTIC/bg_fondo.png)');
            break;
    }

}

