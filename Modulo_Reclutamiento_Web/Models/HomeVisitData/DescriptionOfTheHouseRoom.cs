using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Modulo_Reclutamiento_Web.Models.HomeVisitData
{
    /// <summary>
    ///Representacion del apartado "Descripcion de la Casa Habitacion"<br/>
    /// Tabla BD : Cat_EmpleadosVisitasInmueble
    /// </summary>
    public class DescriptionOfTheHouseRoom
    {
        /// <summary>
        /// Lista con la descripcion y valores para SI y No
        /// </summary>
        private static List<SelectListItem> SiNoItems = new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "S" }, new SelectListItem { Text = "NO", Value = "N" } };
        /// <summary>
        ///Lista con la Descripcion y valores del Tipo de Habitacion ó Vivienda<br></br>
        ///1-Casa Idependiente, 2-Casa Duplex, 3-Departamento, 4-Vecindad, 5-Otro
        /// </summary>
        private static List<SelectListItem> RoomTypeItems = new List<SelectListItem> { new SelectListItem { Text = "Casa Idependiente", Value = "1" }, new SelectListItem { Text = "Casa Duplex", Value = "2" },
           new SelectListItem{ Text="Departamento", Value="3" }, new SelectListItem{ Text="Vecindad",Value="4" }, new SelectListItem{ Text="Otro", Value="5" } };
        /// <summary>
        /// Lista con la Descripcion y valores de los Tipos de Construccion<br></br>
        /// 1-Block, 2-Tabique o Ladrillo, 3-Concreto, 4-Lamina, 5-Otro
        /// </summary>
        private static List<SelectListItem> TypeOfContructionItems = new List<SelectListItem> { new SelectListItem { Text="Block", Value="1" }, new SelectListItem { Text="Tabique o Ladrillo", Value="2" },
            new SelectListItem{ Text ="Concreto", Value="3" }, new SelectListItem{ Text="Lamina", Value="4" }, new SelectListItem{ Text="Otro", Value="5" } };
        /// <summary>
        /// Lista con la Descripcion y valores del mobiliario del domicilio<br></br>
        /// 1-Muy Completo, 2-Completo, 3-Indispensable, 4-Escaso
        /// </summary>
        private static List<SelectListItem> FurnitureItems = new List<SelectListItem> { new SelectListItem { Text="Muy Completo", Value="1" }, new SelectListItem { Text="Completo", Value="2" },
            new SelectListItem{ Text ="Indispensable", Value="3" }, new SelectListItem{ Text ="Escaso", Value="4" } };
        private static List<SelectListItem> QualityOfFurnatureItems = new List<SelectListItem> { new SelectListItem { Text="Buena", Value="1" }, new SelectListItem { Text="Regular", Value="2" } ,
            new SelectListItem{ Text="Malo", Value="3" }, new SelectListItem{ Text ="Otro", Value="4" } };
        private static List<SelectListItem> TypeOfFurnitureItems = new List<SelectListItem> { new SelectListItem { Text="Lujoso", Value="1" }, new SelectListItem { Text="Modesto", Value="2" },
            new SelectListItem{ Text="Malo", Value="3" }, new SelectListItem{ Text="Otro", Value="4" } };
        /// <summary>
        /// Objeto que contiene parametros de busqueda de la visita domiciliaria
        /// </summary>
        public ResearchParam ResearchParam { get; set; }
        /// <summary>
        /// Id identificador del registro "Descripcion de casa habitacion"<br></br>
        ///Columna BD : Id_VisitanteInmueble numeric
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de Propietario de la vivienda visitada <br/>
        /// Columna BD : Propietario varchar(100)
        /// </summary>
        [Display(Name= "Nombre del propietario del inmueble:")]
        public string NameOwnerOfTheProperty { get; set; }
        /// <summary>
        /// Numero de habitantes viviendo en el domicilio <br/>
        /// Columna BD : Cantidad_Habitantes tinyint
        /// </summary>
        [Display(Name = "No. De habitantes:")]
        public int NumberOfInhabitants { get; set; }
        /// <summary>
        /// Cantidad de pisos del domicilio visitado<br/>
        /// Columna BD : Cantidad_Plantas tinyint
        /// </summary>
        [Display(Name = "¿De cuántos pisos es el inmueble?")]
        public int FloorsInTheProperty { get; set; }
        [Display(Name = "Distribución del inmueble:")]
        public string DistributionOfTheProperty { get; set; }
        /// <summary>
        /// Numero de cuartos con el que cuenta el domicilio <br/>
        /// Columna BD : Cantidad_Recamaras tinyint
        /// </summary>
        [Display(Name = "No. de Cuartos")]
        public int NumberOfRooms { get; set; }
        /// <summary>
        /// Numero de baños con el que cuanta el domicilio<br/>
        /// Columna BD : Cantidad_Banos tinyint
        /// </summary>
        [Display(Name = "No. de Baños")]
        public int NumberOfBathrooms { get; set; }
        /// <summary>
        /// Confirma ó niega que existe una sala en el domicilio<br/>
        /// Columa BD : Tiene_Sala varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name =" ¿Tiene Sala?")]
        public bool hasALivingRoom { get; set; }
        /// <summary>
        /// Confirma ó niega que existe una cocina en el domicilio<br/>
        /// Columa BD : Tiene_Cocina varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name = " ¿Tiene Cocina?")]
        public bool hasAKitchen { get; set; }
        /// <summary>
        /// Confirma ó niega que existe un comedor en el domicilio<br/>
        /// Columa BD : Tiene_Comedor varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name =" ¿Tiene Comedor?")]
        public bool hasDiningRoom { get; set; }
        /// <summary>
        /// Confirma ó niega que existe un patio en el domicilio<br/>
        /// Columa BD : Tiene_PatioDelantero varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name =" ¿Tiene Patio?")]
        public bool hasAPatio { get; set; }
        /// <summary>
        /// Confirma ó niega que existe cochera en el domicilio<br/>
        /// Columa BD : Tiene_Cochera varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name =" ¿Tiene Cochera?")]
        public bool hasAGarage { get; set; }
        /// <summary>
        /// Confirma ó niega que existe cuarto de servicio en el domicilio<br/>
        /// Columa BD : Tiene_PatioTrasero varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name =" ¿Tiene Cuarto de Servicio?")]
        public bool hasMaidsRoom { get; set; }

        /// <summary>
        /// Tipo de Habitacion o Vivienda <br/>
        /// Columna BD : Tipo_Casa tinyint, valor : 1...5 <br></br>
        /// Revisar valores en variable "<b>RoomTypeItems</b>"
        /// </summary>
        [Display(Name = "Tipo de Habitación:")]
        public string RoomType { get; set; }
        /// <summary>
        /// Lista con los tipos de Habitacion ó Vivienda <br></br>
        /// Valores por defecto contenidos en variable "<b>RoomTypeItems</b>"
        /// </summary>
        public List<SelectListItem> RoomTypeOp { get; } = RoomTypeItems;
        /// <summary>
        /// Contiene el valor  agregado cuando se selecciona "Otro" en tipo de habitacion ó vivienda<br></br>
        /// Columna BD : Tipo_CasaOtro varchar(200), valor : el que se ingrese
        /// </summary>
        [Display(Name="Otro:")]
        public string RoomTypeOther { get; set; }
        /// <summary>
        /// Tipo de construccion de la habitacion ó vivienda<br></br>
        /// Columna BD : Tipo_Material tyint, valor : 1...5<br></br>
        /// Revisar valores en variable "<b>TypeOfContructionItems</b>"
        /// </summary>
        [Display(Name = "Tipo de Construcción")]
        public string TypeOfContruction { get; set; }
        /// <summary>
        /// Lista con los tipos de contruaccion<br></br>
        /// VAlores por defecto contenidos en la variable "<b>TypeOfContructionItems</b>"
        /// </summary>
        public List<SelectListItem>? TypeOfContructionOp { get;} = TypeOfContructionItems;
        /// <summary>
        /// Contiene el valor agregado cuando se selecciona "Otro" en tipo de construccion<br></br>
        /// Columna BD : Tipo_MaterialOtro varchar(200), valor : el que se ingrese
        /// </summary>
        [Display(Name ="Otro:")]
        public string TypeOfConstructionOther { get; set; }
        /// <summary>
        /// Descripcion de la facha externa del domicilio<br></br>
        /// Columna BD : Observaciones_Exterior text
        /// </summary>
        [Display(Name = "Describa fachada exterior:")]
        public string DescriptionExteriorFacade { get; set; }
        /// <summary>
        /// Mobiliario del domicilio<br></br>
        /// Columna BD : Cantidad_Mobiliario tinyint valor : 1...4
        /// </summary>
        [Display(Name = "Mobiliario")]
        public string Furniture { get; set; }
        /// <summary>
        /// Lista con las descripciones de mobiliario<br></br>
        /// Valores por defecto contenidos en la variable "<b>FurnitureItems</b>"
        /// </summary>
        public List<SelectListItem> FurnitureOp { get; } = FurnitureItems;
        /// <summary>
        /// Calidad del mobiliario<br></br>
        /// Columna BD : Calidad_Mobiliario tinyint
        /// </summary>
        [Display(Name = "Calidad del mobiliario:")]
        public string QualityOfFurnature { get; set; }
        /// <summary>
        /// Lista con las descripciones de calidad del mobiliario<br></br>
        /// Valores por defecto contenidos en la variable "<b>QualityOfFurnatureItems</b>"
        /// </summary>
        public List<SelectListItem> QualityOfFurnatureOp { get;} = QualityOfFurnatureItems;
        /// <summary>
        /// Contiene el valor agregado cuando se selecciona "Otro" en la calidad de mobiliario<br></br>
        /// Columna BD : Calidad_MobiliarioOtro varchar(200), valor : el que se ingrese
        /// </summary>
        [Display(Name ="Otro:")]
        public string QualityOfFurnatureDescription { get; set; }
        /// <summary>
        /// Tipo de mobiliario<br></br>
        /// Columna BD : Tipo_Mobiliario tinyint
        /// </summary>
        [Display(Name = "Tipo de mobiliario:")]
        public string TypeOfFurniture { get; set; }
        /// <summary>
        /// Lista con las descripciones de los tipo de mobiliarios<br></br>
        /// Valores por defecto contenidos en la variable "<b>TypeOfFurnitureItems</b>"
        /// </summary>
        public List<SelectListItem> TypeOfFurnitureOp { get;} = TypeOfFurnitureItems;
        /// <summary>
        /// Contiene el valor agregado cuando se selecciona "Otro" en el tipo de mobiliario<br></br>
        /// Columna BD : Tipo_MobiliarioOtro varchar(200), valor : el que se ingrese
        /// </summary>
        [Display(Name ="Otro")]
        public string TypeOfFurnitureDescription { get; set; }
        /// <summary>
        /// Limpio y Ordenado<br></br>
        /// Columna BD : Limpio_Ordenado varchar(2), valor : "S" ó "N"
        /// </summary>
        [Display(Name = "Se observó limpio y ordenado")]
        public string ObservedCleanAndTidy { get; set; }
        /// <summary>
        /// Lista con las opciones para confirmar o negar si se observa limpio<br></br>
        /// Valores por defecto contenidos en la variable "<b>SiNoItems</b>"
        /// </summary>
        public List<SelectListItem> ObservedCleanAndTidyOp { get; } = SiNoItems;
        /// <summary>
        /// Comentarios que se queiran agregar al respecto<br></br>
        /// Columna BD : Observaciones_Interior text
        /// </summary>
        [Display(Name = "Comentarios:")]
        public string Comments { get; set; }


    }
}
