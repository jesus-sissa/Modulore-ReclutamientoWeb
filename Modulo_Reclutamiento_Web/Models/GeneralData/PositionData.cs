using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class PositionData
    {
        /*
                * Puesto solicitado por el candidato 
                */
        [Display(Name = "Departamento")]
        public int Department { get; set; }
        public List<SelectListItem> Departments { get; set; }
        [Display(Name = "Puesto")]
        public int Position { get; set; }
        public List<SelectListItem> Positions { get; set; }

        /*
         *  Datos de Contacto con la Empresa
         * */
        [Display(Name = "Mode de Contacto")]
        public int ContactMode { get; set; }
        public List<SelectListItem> ContactModeOp { get; set; }
        [Display(Name = "Especifique")]
        public int Specify { get; set; }
        public List<SelectListItem> SpecifyOp { get; set; }
    }
}
