using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class EmploymentData
    {
        public int Id { get; set; }
        [Display(Name = "Nombre de Compañia")]
        public string CompanyName { get; set; }
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
        [Display(Name = "Entre calle")]
        public string StreetBetween1 { get; set; }
        [Display(Name = "Y calle")]
        public string StreetBetween2 { get; set; }
        [Display(Name ="Teléfono")]
        public string Phone { get; set; }
        [Display(Name = "Número Exterior")]
        public int ExternalNumber { get; set; }
        [Display(Name = "Número Interior")]
        public int InternalNumber { get; set; }
        [Display(Name = "Fecha Ingreso")]
        public string StartDate { get; set; }
        [Display(Name = "Fecha Baja")]
        public string FinishDate { get; set; }
        [Display(Name = "Empresa Seguridad")]
        public string SecurityCompany { get; set; }
        public List<SelectListItem> SecurityCompanyOp { get; } = new List<SelectListItem>() { new SelectListItem { Text = "NO", Value = "NO" }, new SelectListItem { Text = "SI", Value = "SI" } };
        [Display(Name = "Porta Armas?")]
        public string Guns { get; set; }
        public List<SelectListItem> GunsOp { get; } = new List<SelectListItem>() { new SelectListItem { Text = "NO", Value = "NO" }, new SelectListItem { Text = "SI", Value = "SI" } };
        public string Position { get; set; }

        [Display(Name = "Nombre Jefe Inmediato")]
        public string BossName { get; set; }

        [Display(Name = "Puesto Jefe Inmediato")]
        public string BossPosition { get; set; }

        [Display(Name = "Sueldo Inicial Mensual")]
        public decimal StartSalary { get; set; }

        [Display(Name = "Sueldo Final Mensual")]
        public decimal FinishSalary { get; set; }

        [Display(Name = "Motivo de Separacion")]
        public int ReasonSeparation { get; set; }
        public List<SelectListItem> ReasonSeparationOp { get; set; }

        [Display(Name = "Otro Motivo")]
        public string OtherMotive { get; set; }
        public List<EmploymentList> EmploymentLists { get; set; }

    }

    public class EmploymentList
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Domicile { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string Position { get; set; }
        public string SecurityCompany { get; set; }
        public string Guns { get; set; }
        public string BossName { get; set; }
        public string Phone { get; set; }
    }
}
