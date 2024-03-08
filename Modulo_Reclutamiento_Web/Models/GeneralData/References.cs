using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class References
    {

        public int Id { get; set; }
        [Display(Name = "Tipo Referencia")]
        public int TypeReferences { get; set; }
        public List<SelectListItem> TypeReferencesOp { get; set; }
        [Display(Name = "Nombre Completo")]
        public string Name { get; set; }
        [Display(Name="Apellido Paterno")]
        public string PaternalName { get; set; }
        [Display(Name="Apellido Materno")]
        public string MaternalName { get; set; }
        [Display(Name = "Ocupación")]
        public string Ocupation { get; set; }
        public List<SelectListItem> OcupationOp { get; set; }
        [Display(Name = "Género")]
        public string Gender { get; set; }
        public List<SelectListItem> Genders { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "MASCULINO", Value = "M" },
            new SelectListItem { Text = "FEMENINO", Value = "F" }
        };
        [Display(Name = "País")]
        public int Country { get; set; }
        public List<SelectListItem> CountryOp { get; set; }
        [Display(Name = "Ciudad")]
        public int City { get; set; }
        public List<SelectListItem> CityOp { get; set; }
        [Display(Name = "Colonia")]
        public string Colony { get; set; }
        [Display(Name = "Código Postal")]
        public int PostalCode { get; set; }
        [Display(Name = "Calle")]
        public string Street { get; set; }
        [Display(Name = "Entre Calle")]
        public string StreetBetween1 { get; set; }
        [Display(Name = "Y la Calle")]
        public string StreetBetween2 { get; set; }
        [Display(Name = "Número Interior")]
        public int InteriorNumber { get; set; }
        [Display(Name = "Número Exterior")]
        public int ExternalNumber { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        public List<ReferencesList> ReferencesLists { get; set; }

    }

    public class ReferencesList {
    
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Ocupation { get; set; }
        public string Domicile { get; set; }
        public string Phone { get; set; }


    }

}
