using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class ContactData
    {

        /*
         * Datos para contactar al prospecto
         */

        [Display(Name = "Correo electrónico")]
        public string PersonalEmail { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Celular")]
        public string MobilePhone { get; set; }
    }
}
