using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    /// <summary>
    /// Cadena de conexión que se verifica en tiempo de ejecución, buena
    /// práctica de seguridad.
    /// </summary>
    public static class Utilities
    {
        public static string ConnectionString = "";
    }

    /// <summary>
    /// Codificación de los errores de excepciones del sistema.
    /// </summary>
    public static class ErrorCodes
    {
        public static string ErrorCodeToString(int code)
        {
            switch (code)
            {
                case 10:
                    return "Datos ingresados satisfactoriamente";
                case 15:
                    return "Datos modificados satisfactoriamente";
                case 20:
                    return "El usuario ya se encuentra registrado";
                case 25:
                    return "Se presentó un problema al modificar los datos";
                case 100:
                    return "Grupo resgistrado satisfactoriamente.";
                case 105:
                    return "No se pudo registrar el grupo, intente mas tarde.";
                case 110:
                    return "Bitacora para el maestro actualizda satisfactoriamente.";
                case 120:
                    return "Bitacora para el grupo actualizda satisfactoriamente.";
                case 130:
                    return "Archivo cargado satisfactoriamente.";
                case 135:
                    return "Error al cargar el archivo.";
                case 150:
                    return "Solo puede existir un grupo de investigación por usuario.";
                case 160:
                    return "Error al agregar maestro coinvestigador, contacte con el administrador de la plataforma.";
                case 200:
                    return "Datos ingresados satisfactoriamente.";
                case 300:
                    return "Miembro agregado satisfactoriamente.";
                case 400:
                    return "Presupuesto actualizado satisfactoriamente.";
                case 410:
                    return "Presupuesto eliminado satisfactoriamente.";
                case 411:
                    return "Aún no ha registrado presupuesto.";
                case 500:
                    return "Foro creado satisfactoriamente.";
                case 510:
                    return "Respuesta a foro creada satisfactoriamente.";
                case 600:
                    return "Estado del arte agregado satisfactoriamente";
                case 601:
                    return "Concepto agregado satisfactoriamente";
                case 602:
                    return "Información agregado satisfactoriamente";
                case 999:
                    return "No tiene privilegios para esta operación.";
                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// Codigos para algunos objetos del sistema.
    /// </summary>
    public static class Codigos
    {
        /// <summary>
        /// Mapa Conceptual
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string CMap(string groupName, string ext)
        {
            var name = string.Format("MapaC-{0}{1}", groupName.Replace(" ", ""), ext.ToUpper());
            return name;
        }

        /// <summary>
        /// Evidencia informacion
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string Evidence(string groupName, int index, string ext)
        {
            var name = string.Format("EvIn-{2}-{0}{1}", groupName.Replace(" ", ""), ext.ToUpper(), index.ToString());
            return name;
        }

        /// <summary>
        /// Carta de Aceptacion
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string Acceptance(string groupName, string ext)
        {
            var name = string.Format("Aceptación-{0}{1}", groupName.Replace(" ", ""), ext.ToUpper());
            return name;
        }

        /// <summary>
        /// Carta de compromiso
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string Commitment(string groupName, string ext)
        {
            var name = string.Format("Compromiso-{0}{1}", groupName.Replace(" ", ""), ext.ToUpper());
            return name;
        }

        public static string ImgInstitucional(string groupName, string ext, int index)
        {
            var name = string.Format("ImgInst-{2}-{0}{1}", groupName.Replace(" ", ""), ext.ToUpper(), index);
            return name;
        }

        /// <summary>
        /// Código brupo de investigación
        /// </summary>
        /// <returns></returns>
        public static string ResearchGroupCode()
        {
            string cod = string.Format("GI-{0}", Codigo());
            return cod;
        }

        /// <summary>
        /// Código aleatorio
        /// </summary>
        /// <returns></returns>
        public static string Codigo()
        {
            StringBuilder cod = new StringBuilder();
            string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                cod.Append(letras[rnd.Next(0, 35)]);
            }
            return cod.ToString();
        }
    }
}
