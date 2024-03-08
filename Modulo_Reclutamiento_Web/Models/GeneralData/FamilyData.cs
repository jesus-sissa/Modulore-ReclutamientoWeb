using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class FamilyData
    {
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "SI" }, new SelectListItem { Text = "NO", Value = "NO" } };
        public int Id { get; set; }
        [Display(Name ="Nombre Completo")]
        public string FullName { get; set; }
        [Display(Name ="Parentesco")]
        public int Kinship { get; set; }
        public List<SelectListItem> KinshipOp { get; set; }
        [Display(Name ="Fecha de Nacimiento")]
        public string BirthDate { get; set; }
        [Display(Name ="Vive en el mismo Domicilio")]
        public string LiveIntheSameAddress { get; set; }
        public List<SelectListItem> LiveIntheSameAddressOp { get; } = SiNoItems;
        [Display(Name ="Domicilio")]
        public string Domicile { get; set; }
        [Display(Name ="Ciudad")]
        public int City { get; set; }
        public List<SelectListItem> CitiesOp { get; set; }
        [Display(Name ="Telefono")]
        public string Phone { get; set; }
        [Display(Name ="Vive")]
        public string Lives { get; set; }
        public List<SelectListItem> LivesOp { get; } = SiNoItems;
        [Display(Name = "Dependiente Economico")]
        public string EconomicDependent { get; set; }
        public List<SelectListItem> EconomicDependentOp { get; } = SiNoItems;

        public List<FamilyList> FamilyList { get; set; }
    }

    public class FamilyList 
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Kinship { get; set; }
        public string BirthDate { get; set; }
        public string Lives { get; set; }
        public string EconomicDependent { get; set; }
    }
}
