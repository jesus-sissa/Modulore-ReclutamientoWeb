using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    /// <summary>
    ///Representacion del apartado "Ingresos y Egresos"<br/>
    /// Tabla BD : Cat_EmpleadosPIngresosEgresos
    /// </summary>
    public class IncomeNExpenses
    {
        /// <summary>
        ///  Objeto que contiene parametros de busqueda de la visita domiciliaria
        /// </summary>
        public ResearchParam ResearchParam { get; set; }
        /// <summary>
        /// Lista con la descripcion y valores para SI y No
        /// </summary>
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "NO", Value = "N" }, new SelectListItem { Text = "SI", Value = "S" } };
        /// <summary>
        /// Id identificador del registro de Ingresos y Egresos<br></br>
        /// Columna BD : Id_IngresosEgresos numeric
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Confirma o niega si tienes planes de endeudamiento a largo plazo<br></br>
        /// Columna BD : PlanEndeudamiento varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name = "¿Tiene planes de endeudamiento a largo plazo?")]
        public string LongTermBorrowingPlan { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si tienes planes de endeudamiento a largo plazo<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> LongTermBorrowingPlanOp { get; } = SiNoItems;
        /// <summary>
        /// Descripcion del plan de endeudamiento<br></br>
        /// Column BD : PlanEndeuDescripcion varchar(300)
        /// </summary>
        [Display(Name ="Descripcion")]
        public string LongTermBorrowingPlanExplication { get; set; }
        /// <summary>
        /// Confirma ó niega si Tienes deudas<br></br>
        /// Column BD : TieneDeuda
        /// </summary>
        [Display(Name = "¿Actualmente tiene deudas?")]
        public string HaveDebts { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si tienes planes de deudas a largo plazo<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> HaveDebtsOp { get; } = SiNoItems;
        /// <summary>
        /// Cantidad de la deuda($)  <br></br>
        /// Column BD : CantidadDeuda
        /// </summary>
        [Display(Name = "¿A cuánto ascienden sus deudas?")]
        public double DebtsAmount { get; set; }
        /// <summary>
        /// Cantidad de Ingresos($) obtenidos<br></br>
        /// Column DB : Ingresos 
        /// </summary>
        [Display(Name = "¿A cuánto ascienden sus ingresos?")]
        public double IncomeAmount { get; set; }
        /// <summary>
        /// Cantidad del dinero que se dedica a ahorrar en porcentaje<br></br>
        /// Column DB : IngresoDedicadoAhorro, Ejemplo : 10
        /// </summary>
        [Display(Name = "De los ingresos que obtiene, ¿cuánto dedica al ahorro(%)?")]
        public string IncomeDedicatedToSavings { get; set; }
        /// <summary>
        /// Cantidad de personas que contribuyen a la economia familiar<br></br>
        /// Column DB : NoPersAportaEconomicamente
        /// </summary>
        [Display(Name = "No. de personas que aportan a la economía familiar:")]
        public int NumberOfPeopleContributeToFamilyEconomy { get; set; }
        /// <summary>
        /// Numero de dependientes econimicos<br></br>
        /// Column DB : NoPerDependienteEconomico
        /// </summary>
        [Display(Name = "No. de dependientes económicos:")]
        public int NumberEconomicDependents { get; set; }
        //[Display(Name = "Sus ingresos mensuales en que forma los distribuye")]
        //public string DistributedMonthlyIncome { get; set; }
        /// <summary>
        /// Confirma ó niega que tenga un familiar que trabaje en alguna corporacion policiaca o de seguridad privada<br></br>
        /// Column DB : FamTrabCorpPoliOPriv
        /// </summary>
        [Display(Name = "¿Tiene algún familiar que trabaje en alguna corporación policíaca o de seguridad privada?")]
        public string FamilyMemberWorksInPoliceOrSecurityCorp { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si tiene un familiar que trabaje en alguna corporacion policiaca o de seguridad privada<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> FamilyMemberWorksInPoliceOrSecurityCorpOp { get; } = SiNoItems;
        /// <summary>
        /// Confirma ó niega que tienes auto propio<br></br>
        /// Column DB : AutoPropio
        /// </summary>
        [Display(Name = "¿Cuenta con auto propio? ")]
        public string HaveOwnCar { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si tienes auto propio<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> HaveOwnCarOp { get; } = SiNoItems;
        /// <summary>
        /// Marca del auto<br></br>
        /// Column DB : Marca, valor : Audi,Ford,Nissa etc...
        /// </summary>
        [Display(Name ="Marca")]
        public string HaveOwnCarBrand { get; set; }
        /// <summary>
        /// Modelo del auto <br></br>
        /// Column DB : Modelo
        /// </summary>
        [Display(Name = "Modelo")]
        public string HaveOwnCarModel { get; set; }
        /// <summary>
        /// Tarjeta de credito que manejas<br></br>
        /// Column DB : ManejaTDC
        /// </summary>
        [Display(Name = "¿Qué tarjetas de crédito maneja?")]
        public string CreditCardHandle { get; set; }
        /// <summary>
        /// Confirma ó niega si tienes alguna propiedad<br></br>
        /// Column DB : TienePropiedad
        /// </summary>
        [Display(Name = "¿Cuenta con alguna propiedad?")]
        public string HaveProperties { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si tienes una propiedad<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> HavePropertiesOp { get; } = SiNoItems;
        /// <summary>
        /// Descripcion de la localizacion de la propiedad<br></br>
        /// Column DB : UbicacionPropiedad
        /// </summary>
        [Display(Name = "Ubicación:")]
        public string PropertiesLocation { get; set; }
        [Display(Name = "¿Quiénes habitan en el domicilio?")]
        public string WhoLivesAtHome { get; set; }
        /// <summary>
        /// Lista de familiares que viven en la misma casa
        /// </summary>
        public List<FamilyList> FamilyMemberLiveAtHome { get; set; }
        /// <summary>
        /// Lista de distribucion de ingresos mensuales
        /// </summary>
        [Display(Name = "Sus ingresos mensuales en que forma los distribuye")]
        public List<MonthlyIncomeDistribution> MonthlyIncomeDistributions { get; set; }

    }
}
