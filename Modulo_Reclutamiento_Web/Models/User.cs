namespace Modulo_Reclutamiento_Web.Models
{
    /// <summary>
    /// Clase que reprecenta un usuario 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id identificador de usuario
        /// </summary>
        public int user_Id { get; set; }
        public int user_Branch { get; set; }
        public int user_Key { get; set; }
        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string user_Password { get; set; }

    }
}
