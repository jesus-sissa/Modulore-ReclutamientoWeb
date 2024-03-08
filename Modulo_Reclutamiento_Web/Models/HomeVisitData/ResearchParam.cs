namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    /// <summary>
    /// Clase que contiene parametros de busqueda de la visita domiciliaria
    /// </summary>
    public class ResearchParam
    {
       /// <summary>
       ///identifica al Prospecto
       /// </summary>
       public int prosp { get; set; }
        /// <summary>
        /// Id identificador de la visita domiciliaria
        /// </summary>
        public int idvisit { get; set; }
        /// <summary>
        /// Filtro
        /// </summary>
        public int filter { get; set; }
        /// <summary>
        /// Estatus
        /// </summary>
        public string stat { get; set; }
    }
}
