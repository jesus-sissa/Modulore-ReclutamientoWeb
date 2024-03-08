using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.MedicalQuestionData
{
    public class GynecologicalAntecento
    {
        public int Id { get; set; }
        /// <summary>
        /// Embarazos
        /// </summary>
        [Display(Name="Embarazos")]
        public int Pregnancies { get; set; }
        /// <summary>
        /// Partos
        /// </summary>
        [Display(Name="Partos")]
        public int Births { get; set; }
        /// <summary>
        /// Cesarias
        /// </summary>
        [Display(Name="Cesarias")]
        public int Cesarias { get; set; }
        /// <summary>
        /// Abortos
        /// </summary>
        [Display(Name="Abortos")]
        public int Abortion { get; set; }
        /// <summary>
        /// Fechas de Nacimiento de los Hijos
        /// </summary>
        [Display(Name ="Fechas de Nacimiento de los Hijos")]
        public string ChildrensBirthDates { get; set; }
        //------------------------------------------------
        /// <summary>
        /// Irregularidades Mestruales
        /// </summary>
        [Display(Name ="Irregularidades Mestruales")]
        public string? MenstrualIrregularities { get; set; }
        public List<SelectListItem> MenstrualIrregularitiesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Infecciones
        /// </summary>
        [Display(Name ="Infecciones")]
        public string Infections { get; set; }
        public List<SelectListItem> InfectionsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Quistes/ Enf. de Ovarios
        /// </summary>
        [Display(Name ="Quistes/ Enf. de Ovarios")]
        public string Cysts { get; set; }
        public List<SelectListItem> CystsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Esterilidad
        /// </summary>
        [Display(Name ="Esterilidad")]
        public string Sterility { get; set; }
        public List<SelectListItem> SterilityOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Otros Problemas
        /// </summary>
        [Display(Name ="Otros Problemas")]
        public string OtherProblems { get; set; }
        public List<SelectListItem> OtherProblemsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Cuales?
        /// </summary>
        [Display(Name ="Cuáles?")]
        public string OtherProblemsDesc { get; set; }
        //-----------------------------------------------------------------
        /// <summary>
        /// Bulto, Nódulo, Bolita en Seno
        /// </summary>
        [Display(Name = "Bulto, Nódulo, Bolita en Seno")]
        public string BreastBall { get; set; }
        public List<SelectListItem> BreastBallOp { get;} = Answers.YesNoAnswers;
        /// <summary>
        /// Quistes en Seno
        /// </summary>
        [Display(Name ="Quistes en Seno")]
        public string BreastCysts { get; set; }
        public List<SelectListItem> BreastCystsOp { get; set; } = Answers.YesNoAnswers;
        /// <summary>
        /// Secrecion
        /// </summary>
        [Display(Name ="Secreción")]
        public string Secretion { get; set; }
        public List<SelectListItem> SecretionOp { get; set; } = Answers.YesNoAnswers;
        /// <summary>
        /// Otros Problemas
        /// </summary>
        [Display(Name ="Otros Problemas")]
        public string OtherProblemsBreast { get; set; }
        public List<SelectListItem> OtherProblemsBreastOp { get; set; } = Answers.YesNoAnswers;
        /// <summary>
        /// Cuales? definicion de los problemas en el pecho
        /// </summary>
        [Display(Name ="Cuales")]
        public string OtherProblemsBreastDesc { get; set; }
        /// <summary>
        /// Revisiones Ginecologicas
        /// </summary>
        [Display(Name ="Revisiones Ginecológicas")]
        public string GynecologicalReviews { get; set; }
        public List<SelectListItem> GynecologicalReviewsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Fecha de la ultima revision medica
        /// </summary>
        [Display(Name ="Fecha de la última revisión médica")]
        public string LastDateMedicalReview { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Lugar
        /// </summary>
        [Display(Name ="Lugar")]
        public string place { get; set; }
        public List<SelectListItem> placeAnswers { get; set; } = Answers.MedicalControlAnswers;
        /// <summary>
        /// Fecha del Ultimo examen de deteccion de cancer
        /// </summary>
        [Display(Name ="Fecha del Ultimo examen de detección de cáncer")]
        public string LastDateCancerScreeningTest { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public List<SelectListItem> LastDateCancerScreeningTestAnswers { get; set; } = new List<SelectListItem> { new SelectListItem { Text="Nunca Me lo he Realizado", Value="0", Selected=false } };
        /// <summary>
        /// Usa algun metodo anticonceptivo
        /// </summary>
        [Display(Name ="Usa algun metodo anticonceptivo")]
        public string ContraceptiveMethod { get; set; }
        public List<SelectListItem> ContraceptiveMethodOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Cual? definicion de metodo anticonceptivo
        /// </summary>
        [Display(Name ="Cual?")]
        public string ContraceptiveMethodDesc { get; set; }
        /// <summary>
        /// Ultima Fecha de Mestruacion
        /// </summary>
        [Display(Name ="Última Fecha de Menstruación")]
        public string LastMenstruation { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public List<SelectListItem> LastMenstruationAnswers { get; set; } = new List<SelectListItem> { new SelectListItem { Text="No lo Recuerdo", Value="0", Selected=false } };
        /// <summary>
        /// Esta Usted Embarazada
        /// </summary>
        [Display(Name ="Esta Usted Embarazada")]
        public string ArePregnated { get; set; }
        public List<SelectListItem> ArePregnatedOp { get; } = Answers.YesNoAnswers;
        [Display(Name ="Fecha Probable de Parto")]
        public string DueDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Tiene Sospecha de Estar Embarazada
        /// </summary>
        [Display(Name ="Tiene Sospecha de Estar Embarazada")]
        public string SuspectedPregnancy { get; set; }
        public List<SelectListItem> SuspectedPregnancyOp { get; } = Answers.YesNoAnswers;


    }
}
