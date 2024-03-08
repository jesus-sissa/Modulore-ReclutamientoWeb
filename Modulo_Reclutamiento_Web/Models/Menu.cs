namespace Modulo_Reclutamiento_Web.Models
{
    /// <summary>
    /// Clase que reprecenta un Menu
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Id identificador de Menu
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Menu<br></br>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Estatus del Menu
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Orden del Menu
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Lista de subMenus
        /// </summary>
        public List<subMenu> subMenus { get; set; }
    }

    /// <summary>
    /// Clase que reprecenta un submenu
    /// </summary>
    public class subMenu 
    {
        /// <summary>
        /// Id Identificador de submenu
        /// </summary>
        public  int MenuId { get; set; }
        /// <summary>
        /// Nombre del submenu
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ruta del submenu
        /// </summary>
        public string Link { get; set; }

    }

}
