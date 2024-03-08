using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
    public class AcademicData
    {
        public int Action { get; set; }
        public int Id { get; set; }
        [Display(Name ="Ultimo Grade de Estudio")]
        public int LastDegreeStudy { get; set; }
        public List<SelectListItem> LastDegreeStudyOp { get; set; }
        [Display(Name ="Documentacion Recibida")]
        public int DocumentReceived { get; set; }
        public List<SelectListItem> DocumentsReceivedOp { get; set; }
        [Display(Name ="Nombre Completa Escuela")]
        public string SchoolName { get; set; }
        [Display(Name ="Carrera")]
        public string Career { get; set; }
        [Display(Name = "Cedula Profesional")]
        public string ProfessionalID { get; set; }
        [Display(Name = "Especialidad")]
        public string Speciality { get; set; }
        [Display(Name = "Año Inicio")]
        public string StartYear { get; set; }
        public List<SelectListItem> StartYearsOp { get; set; }
        [Display(Name = "Año Finalizacion")]
        public string FinishYear { get; set; }
        public List<SelectListItem> FinishYearsOp { get; set; }
        [Display(Name = "Folio")]
        public string Folio { get; set; }
        [Display(Name = "Promedio")]
        public double SchoolAverage { get; set; }
        public CoursesReceived Courses { get; set; }
        public List<CoursesReceived> CoursesList { get; set; }
    }
}
