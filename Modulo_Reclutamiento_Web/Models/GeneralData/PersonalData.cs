using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class PersonalData
    {
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "S" }, new SelectListItem { Text = "NO", Value = "N" } };
        /*
         * Datos personales de candidato
         */
        public string Key { get; set; }
        [Display(Name = "Nombre(s)")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string PaternalName { get; set; }
        [Display(Name = "Apellido Materno")]
        public string MaternalName { get; set; }
        [Display(Name = "Género")]
        public string Gender { get; set; }
        public List<SelectListItem> Genders { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "MASCULINO", Value = "M" },
            new SelectListItem { Text = "FEMENINO", Value = "F" }
        };
        [Display(Name = "Estado Civil")]
        public int MaritalStatus { get; set; }
        public List<SelectListItem> MaritalStatusOp { get; set; }
        [Display(Name = "Vive con Familia")]
        public string LiveWithFamily { get; set; }
        public List<SelectListItem> LiveWithFamilyOp { get; } = SiNoItems;
        [Display(Name = "Número de hijos")]
        public int NumberOfChildren { get; set; }
        //dia de nacimiento, edad , pais y ciudad de nacimiento
        [Display(Name = "Dia de Nacimiento")]
        public string DateOfBirth { get; set; }
        [Display(Name = "Edad")]
        public int Age { get; set; }
        [Display(Name = "País de Nacimiento")]
        public int Country { get; set; }
        public List<SelectListItem> CountriesOp { get; set; }
        [Display(Name = "Estado")]
        public int State { get; set; }
        public List<SelectListItem> StatesOp { get; set; }
        [Display(Name = "Ciudad")]
        public int CityOfState { get; set; }
        public List<SelectListItem> CityOfStateOp { get; set; }
        [Display(Name = "Nacionalidad")]
        public int NationalityMode { get; set; }
        public List<SelectListItem> NationalityModeOp { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "NACIMIENTO", Value = "1", Selected=true },
            new SelectListItem { Text = "NATURALIZADO", Value = "2" },
            new SelectListItem { Text = "EXTRANJERO", Value = "3" }
        };
        [Display(Name = "Dia Naturalizacion")]
        public string Naturalizationday { get; set; }

    }
}
