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
function vet(id) {
    switch (id) {
        case 1:
            $('#coordinacion').show();
            $('#administrativo').hide();
            $('#seguimiento').hide();
            $('#iep').hide();
            $('#fortic').hide();
            $('#infraestructura').hide();
            $('#plataforma').hide();
            break;
        case 2:
            $('#coordinacion').hide();
            $('#administrativo').show();
            $('#seguimiento').hide();
            $('#iep').hide();
            $('#fortic').hide();
            $('#infraestructura').hide();
            $('#plataforma').hide();
            break;
        case 3:
            $('#coordinacion').hide();
            $('#administrativo').hide();
            $('#seguimiento').show();
            $('#iep').hide();
            $('#fortic').hide();
            $('#infraestructura').hide();
            $('#plataforma').hide();
            break;
        case 4:
            $('#coordinacion').hide();
            $('#administrativo').hide();
            $('#seguimiento').hide();
            $('#iep').show();
            $('#fortic').hide();
            $('#infraestructura').hide();
            $('#plataforma').hide();
            break;
        case 5:
            $('#coordinacion').hide();
            $('#administrativo').hide();
            $('#seguimiento').hide();
            $('#iep').hide();
            $('#fortic').show();
            $('#infraestructura').hide();
            $('#plataforma').hide();
            break;
        case 6:
            $('#coordinacion').hide();
            $('#administrativo').hide();
            $('#seguimiento').hide();
            $('#iep').hide();
            $('#fortic').hide();
            $('#infraestructura').show();
            $('#plataforma').hide();
            break;
        case 7:
            $('#coordinacion').hide();
            $('#administrativo').hide();
            $('#seguimiento').hide();
            $('#iep').hide();
            $('#fortic').hide();
            $('#infraestructura').hide();
            $('#plataforma').show();
            break;
    }
}
function vegeneral(id) {
    switch (id) {
        case 1:
            $('#contenido_izquierda').css('background-image', 'url()');
            $('#contenido_izquierda').css('background-image', 'url(../../images/conocenos/general/bg_objetivo.png) repeat-y;');
            $('#titulo1').html("Objetivo General <br> <div style='color: #ffffff;' id=\'texto_objetivos\'>Desarrollar capacidades y habilidades para el fomento de competencias científicas y tecnológicas que impulsen la apropiación del conocimiento y la productividad en la comunidad y establecimientos educativos del Departamento de Nariño.</div>");

            $('#contenido_centro').css('background-image', 'url(/images/conocenos/general/img_objetivosespecificos.png)');
            $('#titulo2').html("Objetivo específicos");

            $('#contenido_derecha').css('background-image', 'url(../../images/conocenos/general/img_componentes.png)');
            $('#titulo3').html("Componentes Acompañamiento");
            break;
        case 2:
            $('#contenido_izquierda').css('background-image', 'url(../../images/conocenos/general/img_objetivogeneral.png)');
            $('#titulo1').html("Objetivo General");

            $('#contenido_centro').css('background-image', 'url()');
            $('#contenido_centro').css('background-image', 'url(../../images/conocenos/general/bg_objetivo.png) repeat-y;');
            $('#titulo2').html("Objetivos específicos <br><div style='color: #ffffff;' id=\"texto_especificos\"><br>Formar el espíritu científico y de apropiación de las TIC en la escuela y en diferentes instancias de socialización.<br><br> Apropiar mecanismos de CT+I que garanticen la comprensión del conocimiento por diversos actores de la sociedad apoyado en TIC.<br><br>Desarrollar mecanismos de evaluación de los procesos de apropiación y de impacto de los proyectos de CT+I.</div>");

            $('#contenido_derecha').css('background-image', 'url(../../images/conocenos/general/img_componentes.png)');
            $('#titulo3').html("Componentes Acompañamiento");
            break;
        case 3:
            $('#contenido_izquierda').css('background-image', 'url(../../images/conocenos/general/img_objetivogeneral.png)');
            $('#titulo1').html("Objetivo General");
            $('#contenido_centro').css('background-image', 'url(/images/conocenos/general/img_objetivosespecificos.png)');
            $('#titulo2').html("Objetivos específicos");

            $('#contenido_derecha').css('background-image', 'url()');
            $('#contenido_derecha').css('background-image', 'url(../../images/conocenos/general/bg_objetivo.png) repeat-y;');
            $('#titulo3').html("Componentes Acompañamiento<br><div style='color: #ffffff;' id= \"texto_componentes\">Seguimiento y formación de los grupos de investigación infantiles y juveniles y los proyectos de aula siguiendo la ruta metodológica de la IEP, apoyada en TIC para la transformación de la Institucionalidad Educativa.<br /><br />Formación a estudiantes y comunidad en general para la producción del conocimiento a través del uso y apropiación de las TIC. .<br /><br/>Apoyo a grupos de investigación. .<br /><br/>Adquisición de recursos tecnológicos que se requieren para participar en el Proyecto.  .<br /><br/> Desarrollo de una Plataforma Tecnológica para el apoyo a proyectos de investigación, proyectos productivos y al componente curricular a través de OVAS y AVAS virtuales.    .<br /><br/>Desarrollo de ferias de investigación.    .<br /><br/>Desarrollo de un componente de evaluación y seguimiento a través de la Plataforma Tecnológica y contratación de Interventorías.</div>");
            break;
    }
}