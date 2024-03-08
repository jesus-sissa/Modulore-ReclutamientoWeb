using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models
{
    public class ProspectusData
    {
        //[Display(Name = "")]
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "S" }, new SelectListItem { Text = "NO", Value = "N" } };

        public ProspectusData()
        {
            this.PersonalData = new PersonalData();
            this.PositionData = new PositionData();
            this.DomicileData = new DomicileData();
            this.ContactData = new ContactData();
            this.OfIdentificationData = new OficalIdentificationData();
            this.InfonaCredit = new InfonavitCreditData();
        }

        public PersonalData PersonalData { get; private set; }
        public PositionData PositionData { get; private set; }
       
        public DomicileData DomicileData { get; private set; }

        public ContactData ContactData { get; private set; }
       
        public OficalIdentificationData OfIdentificationData { get; private set; }

        public InfonavitCreditData InfonaCredit { get; private set; }

        //[Display(Name = "Certificado Academico")]
        //public string AcademicCertification { get; set; }
        //public List<SelectListItem> AcademicCertificationOp { get; } = SiNoItems;
        //[Display(Name = "En catalogo de Firmas")]
        //public string InCatalogSignatures { get; set; }
        //public List<SelectListItem> InCatalogSignaturesOp { get; } = SiNoItems;
        //[Display(Name = "Fecha Ingreso")]
        //public string DayOfAdmission { get; set; }
        //[Display(Name = "Fecha de Baja")]
        //public string DismissalDay { get; set; }

        /*
         * datos de referencia de empleado
         */
        [Display(Name = "Empleado de Referencia")]
        public int EmployeeReference { get; set; }
        public List<SelectListItem> EmployeeReferenceOp { get; set; }
        [Display(Name = "¿Jefe de Area?")]
        public string AreaManager { get; set; }
        public List<SelectListItem> AreaManagerOp { get; } = SiNoItems;
        [Display(Name = "¿Sale a Ruta?")]
        public string GoinOnTheRoad { get; set; }
        public List<SelectListItem> GoinOnTheRoadOp { get; } = SiNoItems;
        [Display(Name = "¿Verifica Depositos?")]
        public string VerifyDeposits { get; set; }
        public List<SelectListItem> VerifyDepositsOp { get; } = SiNoItems;
    }

}
