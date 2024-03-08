using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.MedicalQuestionData
{
    public class MedicalQuestion
    {
        private static List<SelectListItem> _list = new List<SelectListItem> {
            new SelectListItem { Text = "APROBADO", Value = "AP" },
            new SelectListItem { Text = "NO CONCLUYENTE", Value = "NC" } ,
            new SelectListItem {  Text ="NO APROBADO" ,Value="NA"}
        };
        public int Id { get; set; }
        public int ProspectusId { get; set; }
        public int EmployeerId { get; set; }
        public string stat { get; set; }
        public int filter { get; set; }
        //-------------------------------------
        public string Name { get; set; }
        [Display(Name = "Peso")]
        public string Weight { get; set; }
        [Display(Name = "Altura")]
        public string height { get; set; }
      
        //public int PersonalMedical { get; set; }
        public MedicalQuestionPerson MedicalQuestionPerson { get; set; }
        public int personalMedicalHistoryId { get; set; }
        public PersonalMedicalHistory personalMedicalHistory { get; set; }
        public int gynecologicalAntecentoId { get; set; }
        public GynecologicalAntecento  gynecologicalAntecento{ get; set; }
        [Display(Name = "Antidoping")]
        public string Antidoping { get; set; }
        public bool IsSigned { get; set; }
        [Display(Name = "Firma")]
        public string Signature { get; set; }
        [Display(Name = "Servicio Medico")]
        public string MedicalServices { get; set; }
        
        public string MedicalServiceApproval { get; set; }
        public List<SelectListItem> MedicalServiceApprovalOp { get; set; } = _list;
        [Display(Name = "Observaciones")]
        public string Remarks { get; set; }
        [Display(Name ="Fecha")]
        public string Date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        //public MedicalQuestion() 
        //{
        //    this.personalMedicalHistory = new PersonalMedicalHistory();
        //    this.gynecologicalAntecento = new GynecologicalAntecento();
        //}
    }


}
