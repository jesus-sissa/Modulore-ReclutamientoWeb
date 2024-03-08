using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.GeneralData
{
   
    public class InfonavitCreditData
    {
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "S" }, new SelectListItem { Text = "NO", Value = "N" } };
        /*
           * datos de credito infonavit
           */

        [Display(Name = "Crédito Infonavit")]
        public string CreditInfonavit { get; set; }
        public List<SelectListItem> CreditInfonavitOp { get; } = SiNoItems;
        [Display(Name = "Número de Credito")]
        public int CreditNumber { get; set; }
        [Display(Name = "Tipo de Descuento")]
        public string DiscountType { get; set; }
        public List<SelectListItem> DiscountTypeOp { get; } = new List<SelectListItem> { new SelectListItem { Text = "IMPORTE", Value = "I" }, new SelectListItem { Text = "PORCENTAJE", Value = "P" }, new SelectListItem { Text = "VSM", Value = "V" } };
        [Display(Name = "Cantidad Descontada")]
        public decimal DiscountedAmount { get; set; }
    }
}
