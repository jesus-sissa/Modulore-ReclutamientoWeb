using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    public class NeighborhoodReference
    {
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        public string NeiborhoodName { get; set; }
        [Display(Name = "Cómo definiría a la familia en General:")]
        public string DefinitionOfTheFamily { get; set; }
        [Display(Name ="Cuantos años tienen de conocer a la familia:")]
        public double YearsOfGettingToKnowFamily { get; set; }
        [Display(Name = "Sabe a qué se dedica el candidato ")]
        public string KnowWhatTheCandidateDoes { get; set; }
        [Display(Name = "Cómo definiría a la persona:")]
        public string HowYouWouldDefineThePerson { get; set; }
        public List<SelectListItem> HowYouWouldDefineThePersonOp { get; } = definitions;
        //Agresiva,Seria,Tranquila,Enojona,Impulsiva,Amigable
        public List<NeighborhoodReference> NeighborhoodReferenceList { get; set; }

        private static List<SelectListItem> definitions = new List<SelectListItem>() {
            new SelectListItem { Text="Seleccione..",Value="0" },
            new SelectListItem { Text="AGRESIVA",Value="1" },
            new SelectListItem { Text="IMPULSIVA",Value="2" },
            new SelectListItem { Text="ENOJONA",Value="3" },
            new SelectListItem { Text="TRANQUILA",Value="4" },
            new SelectListItem { Text="SERIO",Value="5" },
            new SelectListItem { Text="AMIGABLE",Value="6" },
            new SelectListItem { Text="OTRO",Value="7" },
           
        };


      
    }
}
