using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class OficalIdentificationData
    {
        /*
            * Datos de Identificacion del Prospecto
            * **/

        [Display(Name = "Clave Elector")]
        public string VoterKey { get; set; }
        [Display(Name = "RFC")]
        public string RFC { get; set; }
        [Display(Name = "CURP")]
        public string CURP { get; set; }
        //datos de seguro medico
        [Display(Name = "No IMSS")]
        public string IMSS { get; set; }
        [Display(Name = "No Cartilla")]
        public string BookletNumber { get; set; }
        [Display(Name = "Unidad Medico Familiar")]
        public int UMF { get; set; }
        public List<SelectListItem> UMFOp { get; set; }
        [Display(Name = "Pasaporte")]
        public string Passport { get; set; }

        [Display(Name = "Exp Infonavit")]
        public string ExpInfonavit { get; set; }

        [Display(Name = "No Tributario")]
        public string TaxId { get; set; }

        /*
         * Datos de Licencia de Conducir local y federal
         */
        [Display(Name = "Número de Licencia")]
        public string LicenseNumber { get; set; }
        [Display(Name = "Tipo Licencia")]
        public int LicenseType { get; set; }
        public List<SelectListItem> LicenseTypeOp { get; set; }
        [Display(Name = "Fecha de Expiración")]
        public string LincenseExpirationDate { get; set; }
        [Display(Name = "Número Licencia Federal")]
        public string FederalLicenseNumber { get; set; }
        [Display(Name = "Fecha Expiración")]
        public string FederalLicenseExpirationDate { get; set; }

    }
}
