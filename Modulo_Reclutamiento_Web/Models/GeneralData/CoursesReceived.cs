using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class CoursesReceived
    {
        public int Id { get; set; }
        [Display(Name = "Tipo de Curso")]
        public int? TypeCourse { get; set; }
        public List<SelectListItem> TypeCourseOp { get; } = new List<SelectListItem> { new SelectListItem { Text = "INTERNO", Value = "I" }, new SelectListItem { Text = "EXTERNO", Value = "E" } };
        [Display(Name = "Nombre Curso")]
        public string? Name { get; set; }
        [Display(Name = "Fecha Inicio")]
        public string? StartDate { get; set; }
        [Display(Name = "Fecha Fin")]
        public string? FinishDate { get; set; }
       
        [Display(Name="Finalizado")]
        public string? Finished { get; set; }
        public List<SelectListItem> FinishedOp { get; } = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "S" }, new SelectListItem { Text = "N", Value = "N" } };
        [Display(Name = "Instructor")]
        public string? Instructor { get; set; }
        [Display(Name = "Documento Recibido")]
        public int? DocucumentReceived { get; set; } 
        public List<SelectListItem> DocucumentReceivedOp { get; set; }
        [Display(Name = "Comentarios")]
        public string? Comments { get; set; }

    }
}
