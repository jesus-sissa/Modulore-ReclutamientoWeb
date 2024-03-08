namespace Modulo_Reclutamiento_Web.Models
{
    /// <summary>
    /// Clase que reprecenta la validacion de un usuario
    /// </summary>
    public class Validation
    {
        /// <summary>
        /// Confirma ó niega si existe
        /// </summary>
        public bool IsExist { get; set; }
        /// <summary>
        /// Mensaje de Error
        /// </summary>
        public string Error { get; set; }
    }
   
}
