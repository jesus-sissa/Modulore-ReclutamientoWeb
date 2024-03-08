using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class DomicileData
    {
        /*
        * Datos de domicilio del candidato
        */
        [Display(Name = "Código Postal")]
        public int ZipCode { get; set; }
        [Display(Name = "Ciudad")]
        public int City { get; set; }
        public List<SelectListItem> Cities { get; set; }
        [Display(Name = "Colonia")]
        public int Colony { get; set; }
        public List<SelectListItem> Colonies { get; set; }
        [Display(Name = "Calle")]
        public string Street { get; set; }
        [Display(Name = "Entre Calle")]
        public string BetweenStreet1 { get; set; }
        [Display(Name = "Y Calle")]
        public string BetweenStreet2 { get; set; }
        [Display(Name = "No Exterior")]
        public int ExteriorNumber { get; set; }
        [Display(Name = "No Interior")]
        public int InteriorNumber { get; set; }
        [Display(Name = "Zona")]
        public int Zone { get; set; }
        public List<SelectListItem> Zones { get; set; }

    }
}
