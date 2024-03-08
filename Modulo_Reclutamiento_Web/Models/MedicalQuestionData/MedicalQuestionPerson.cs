using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.MedicalQuestionData
{
    public class MedicalQuestionPerson
    {
        public int Id { get; set; } 
        [Display(Name ="Puesto a Desempeñar")]
        public string Position { get; set; }
        [Display(Name ="Nombre")]
        public string Name { get; set; }
        [Display(Name = "Edad")]
        public int Age { get; set; }
        [Display(Name = "Escolaridad")]
        public int LastDegreeStudy { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public string DateOfBirth { get; set; }
        [Display(Name = "Género")]
        public string Gender { get; set; }
        public List<SelectListItem> GenderOp { get; } = Answers.Genders;
        [Display(Name = "Estado Civil")]
        public string MaritalStatus { get; set; }
        [Display(Name = "Lugar de Nacimiento")]
        public string PlaceOfBirth  { get; set; }
       
        [Display(Name = "Estado")]
        public string State { get; set; }
        //domicilio actual
        [Display(Name ="Calle")]
        public string StreetOfDomicilie { get; set; }
        [Display(Name = "Numero")]
        public string StreetNumberOfDomicilie { get; set; }
        [Display(Name = "Colonia")]
        public string ColonyOfDomicilie { get; set; }
        [Display(Name = "Ciudad")]
        public string CityOfDomicilie { get; set; }
        [Display(Name = "Código Postal")]
        public string PostalCodeOfDomicilie { get; set; }
        [Display(Name = "Estado")]
        public string StateOfDomicilie { get; set; }
        [Display(Name = "Teléfono Fijo")]
        public string Phone { get; set; }
        [Display(Name = "Teléfono Celular")]
        public string CellPhone { get; set; }
        [Display(Name = "No de Hijos")]
        public string NumberOfChilds { get; set; }
        [Display(Name = "Edades de los Hijos")]
        public string AgeOfTheChildrens { get; set; }
        [Display(Name = "Estan Sanos?")]
        public string AreHealthy { get; set; }
        [Display(Name = "Complexión")]
        public string Complexion { get; set; }
        public List<SelectListItem> ComplexionOp { get; } = Answers.ComplexionAnswers;




    }
}
