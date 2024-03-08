using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    /// <summary>
    /// Clase que reprecenta la Visita Domiciliaria
    /// </summary>
    public class HomeVisit
    {
        public HomeVisit()
        {
            NeighborhoodReference = new NeighborhoodReference();
        }

        /// <summary>
        /// Id identificador de la visita domiciliaria<br></br>
        /// Columna : Id_Visita
        /// </summary>
        public int IdHomeVisit { get; set; }
        /// <summary>
        /// Numero de Nomina<br></br>
        /// Columna  : Clave_EmpleadoP
        /// </summary>
        [Display(Name ="Numero de Nomina")]
        public string PayrollNumber { get; set; }
        /// <summary>
        /// Apellido Paterno<br></br>
        /// Columna  : APaterno
        /// </summary>
        [Display(Name = "Apellido Paterno")]
        public string PaternalName { get; set; }
        /// <summary>
        /// Apeliido Materno<br></br>
        /// Columna  : AMaterno
        /// </summary>
        [Display(Name = "Apellido Materno")]
        public string MaternalName { get; set; }
        /// <summary>
        /// Nombre(s) del candidato o empleado<br></br>
        /// Columna  : Nombres
        /// </summary>
        [Display(Name = "Nombre(s)")]
        public string FirstName { get; set; }
        /// <summary>
        /// Edad<br></br>
        /// Columna  : Edad
        /// </summary>
        [Display(Name ="Edad")]
        public int Age { get; set; }
        /// <summary>
        /// Puesto del candidato o empleado<br></br>
        /// Columna  : Puesto
        /// </summary>
        [Display(Name = "Puesto")]
        public string Position { get; set; }
        /// <summary>
        /// Departamento <br></br>
        /// Columna  : Departamento
        /// </summary>
        [Display(Name = "Departamento")]
        public string Department { get; set; }
        /// <summary>
        /// Domicilio <br></br>
        /// Columna  : Domicilio
        /// </summary>
        [Display(Name ="Domicilio Actual (Calle y Numero)")]
        public string Domicile { get; set; }
        /// <summary>
        /// Colonia <br></br>
        /// Columna  : Colonia
        /// </summary>
        [Display(Name ="Colonia")]
        public string Colony { get; set; }
        /// <summary>
        /// Ciudad <br></br>
        /// Columna  : Ciudad
        /// </summary>
        [Display(Name = "Delegacion o Municipio")]
        public string City { get; set; }
        /// <summary>
        /// Estado<br></br>
        /// Columna  : Estado
        /// </summary>
        [Display(Name = "Estado")]
        public string State { get; set; }
        /// <summary>
        /// Telefono<br></br>
        /// Columna  :Telefono
        /// </summary>
        [Display(Name = "Telefono Fijo")]
        public string Phone { get; set; }
        /// <summary>
        /// Celular <br></br>
        /// Columna  : Telefono_Movil
        /// </summary>
        [Display(Name = "Telefono Celular")]
        public string CellPhone { get; set; }
        /// <summary>
        /// Antiguedad <br></br>
        /// Columna  : Antiguedad
        /// </summary>
        [Display(Name = "Antiguedad en la Empresa")]
        public string Seniority { get; set; }
        /// <summary>
        /// Lugar de Nacimiento<br></br>
        /// Columna  : Lugar_Nacimiento
        /// </summary>
        [Display(Name ="Lugar de Nacimiento")]
        public string BornLocation { get; set; }
        /// <summary>
        /// Fecha de Nacimiento<br></br>
        /// Columna  : Fecha_Nacimiento
        /// </summary>
        [Display(Name ="Fecha Nacimiento")]
        public string BornDay { get; set; }
        /// <summary>
        /// Estado Civil<br></br>
        /// Columna  : EstadoCivil
        /// </summary>
        [Display(Name ="Estado Civil")]
        public string MaritalStatus { get; set; }
        /// <summary>
        /// Nombre de Cónyugue<br></br>
        /// Columna  : Conyugue
        /// </summary>
        [Display(Name = "Nombre de Cónyugue")]
        public string Spouse { get; set; }
        /// <summary>
        /// Telefono de Emergencia <br></br>
        ///Columna  : TelefonoEmergencia
        /// </summary>
        [Display(Name = "Telefono de Emergencia")]
        public string EmergencyPhone { get; set; }
       

        /////////////////////////////////////////////
        ///
        [Display(Name ="Tiempo Recidencia (Años)")]
        public int RecidenceTime { get; set; }
        /// <summary>
        /// Dia de Visita <br></br>
        /// Columna  : FechaVisita
        /// </summary>
        [Display(Name = "Fecha de Visita")]
        public string DayOfVisit { get; set; }
        /// <summary>
        /// Hora de visita<br></br>
        /// Columna  : HoraVisita
        /// </summary>
        [Display(Name = "Hora de Visita")]
        public string VisitingTime { get; set; }

        [Display(Name = "Firma de Entrevistado")]
        public string InterviewSignature { get; set; }

        /// <summary>
        /// Nombre de reclutador<br></br>
        /// Columna  : UsuarioVisita
        /// </summary>
        [Display(Name = "Nombre Entrevistador")]
        public string RecruiterName { get; set; }
        [Display(Name = "Puesto Entrevistador")]
        public string RecruiterPosition { get; set; }

        [Display(Name = "Firma Entrevistador")]
        public string RecruiterSignature { get; set; }
        /// <summary>
        /// Motivo de Visita
        /// </summary>
        [Display(Name = "Motivo de Visita")]
        public string ReasonToVisit { get; set; }
        /// <summary>
        /// Identifica si es Prospecto o Empleado<br></br>
        /// P : Prospecto, E : Empleado
        /// </summary>
        public string ProspOrEmpl { get; set; }
        /// <summary>
        /// Id identificador de prospecto o empleado
        /// </summary>
        public int IdProspOrEmpl { get; set; }

       
        /// <summary>
        /// Objeto <b>IncomeNExpenses</b> contiene la informacion de "Ingresos y Egresos"
        /// </summary>
        public IncomeNExpenses IncomeNExpenses { get; set; }

        /// <summary>
        /// Objeto <b>NeighborhoodReference</b> contiene la informacion de "Referencias Vecinales"
        /// </summary>
        public NeighborhoodReference NeighborhoodReference { get; set; }

        /// <summary>
        /// Objeto <b>DescriptionOfTheHouseRoom</b> contiene la informacion de "Descripcion de Casa Habitacion"
        /// </summary>
        public DescriptionOfTheHouseRoom DescriptionOfTheHouseRoom { get; set; }
        


    }
}
