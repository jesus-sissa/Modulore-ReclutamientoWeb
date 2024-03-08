using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    public class MonthlyIncomeDistribution
    {

        [Display(Name ="Id")]
        public int Id { get; set; }
        [Display(Name = "Ingreso")]
        public string Income { get; set; }
        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

    }
}
