using Microsoft.AspNetCore.Mvc.Rendering;

namespace Modulo_Reclutamiento_Web.Models
{
    public class Answers
    {
        public static List<SelectListItem> YesNoAnswers = new List<SelectListItem> {
            new SelectListItem { Text ="NO",Value="N" },
            new SelectListItem { Text ="SI",Value="S" }
        };

        public static List<SelectListItem> Genders { get; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "MASCULINO", Value = "M" },
            new SelectListItem { Text = "FEMENINO", Value = "F" }
        };

        #region Cuestionario Medico

        public static List<SelectListItem> ComplexionAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Delgada", Value = "1",Selected=false},
            new SelectListItem{ Text ="Regular", Value = "2",Selected=false},
            new SelectListItem{ Text ="Robusta", Value = "3",Selected=false},
            new SelectListItem{ Text ="Atletica", Value = "4",Selected=false},
            new SelectListItem{ Text ="Obesa", Value = "5",Selected=false},
        
        };

        #region Antecendentes Medicos Personales

        public static List<SelectListItem> IMCAnswers = new List<SelectListItem> {
            new SelectListItem { Text="Seleccione...",Value="1",Selected =false },
            new SelectListItem { Text="Bajo",Value="1",Selected =false },
            new SelectListItem { Text="Normal",Value="2",Selected =false },
            new SelectListItem { Text="Sobrepeso",Value="3",Selected =false },
            new SelectListItem { Text="Obeso G 1",Value="4",Selected =false },
            new SelectListItem { Text="Obeso G 2",Value="5",Selected =false },
            new SelectListItem { Text="Obeso G 3",Value="6",Selected =false },
            new SelectListItem { Text="Obesidad Morbida",Value="7",Selected =false }
        };

        public static List<SelectListItem> FamilyHistoryAnswers = new List<SelectListItem> {
            new SelectListItem { Text="Diabetes", Value="1", Selected =false },
            new SelectListItem { Text="Hipertension", Value="2", Selected =false },
            new SelectListItem{ Text="Cancer", Value ="3", Selected =false },
            new SelectListItem{ Text="Enf. Corazon", Value="4", Selected =false },
            new SelectListItem{ Text="Embolia", Value="5", Selected =false },
            new SelectListItem{  Text="Derrame Cerebral", Value="6", Selected =false},
            new SelectListItem{ Text="Colesterol Alto", Value="7", Selected =false }
        };

        public static List<SelectListItem> WearGlassesOrPupilsAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Solo uso para Leer y Escribir", Value="1", Selected =false },
            new SelectListItem{ Text="Uso todo el dia", Value="2", Selected =false },
            new SelectListItem{ Text="Uso lentes bifocales", Value="3", Selected =false },
            new SelectListItem{ Text="Se me dañaron y no uso", Value="4", Selected =false },
        };

        public static List<SelectListItem> ChildhoodDiseasesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Convulsiones/Epilepcia",Value="1",Selected =false },
            new SelectListItem{ Text="Asma",Value="2",Selected =false },
            new SelectListItem{ Text="Bronquitis/Neumonia",Value="3",Selected =false },
            new SelectListItem{ Text="Meningitis",Value="4",Selected =false },
            new SelectListItem{ Text="Varicela",Value="5",Selected =false },
            new SelectListItem{ Text="Sarampion",Value="6",Selected =false },
            new SelectListItem{ Text="Otras",Value="7",Selected =false },
        };

        public static List<SelectListItem> AllergiesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Nasal",Value ="1",Selected=false },
            new SelectListItem{ Text="Piel/Urticaria",Value ="2",Selected=false },
            new SelectListItem{ Text="Medicamentos",Value ="3",Selected=false },
            new SelectListItem{ Text="Alimentos",Value ="4",Selected=false },
        };

        public static List<SelectListItem> WearGlassesOrContactLensesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Miopia",Value ="1", Selected =false },
            new SelectListItem{ Text="Hipermetropia",Value ="2", Selected =false },
            new SelectListItem{ Text="Astisgmatismo",Value ="3", Selected =false },
            new SelectListItem{ Text="Presbicia",Value ="4", Selected =false },
            new SelectListItem{ Text="Lente Intraocular",Value ="5", Selected =false },
            new SelectListItem{ Text="Enf. de la Retina",Value ="6", Selected =false },
            new SelectListItem{ Text="Ceguera",Value ="7", Selected =false },
            new SelectListItem{ Text="Perdida del Ojo",Value ="8", Selected =false },
            new SelectListItem{ Text="Protesis Ocular",Value ="9", Selected =false },
            new SelectListItem{ Text="Dificultad para ver Colores(daltonismo)",Value ="10", Selected =false },
            new SelectListItem{ Text="Estrabismo",Value ="11", Selected =false },
        };

        public static List<SelectListItem> TabacoAnswers = new List<SelectListItem>
        {
            new SelectListItem{ Text="Nunca he Fumado", Value="1", Selected =false },
            new SelectListItem{ Text="Deje de Fumar hace", Value="2",Selected =false },
            new SelectListItem{ Text="Habitualmente Fumo", Value="3",Selected =false },
            new SelectListItem{ Text="Diariamente Fumo", Value="4",Selected =false },
            new SelectListItem{ Text="Semanalmente Fumo", Value="5",Selected =false },
        };

        public static List<SelectListItem> AlcoholAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Nunca", Value="1",Selected =false},
            new SelectListItem{ Text="Esporadicamente", Value="2",Selected =false },
            new SelectListItem{ Text="Fines de Semana", Value="3",Selected =false },
            new SelectListItem{ Text="Cualquier Dia", Value="4",Selected =false },
            new SelectListItem{ Text="2 o 3 Veces al Año,Solo Navidad/Fin de Año", Value="5",Selected =false },
        };


        public static List<SelectListItem> DrinkTypeAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Seleccione...", Value="0", Selected =false },
            new SelectListItem{ Text ="Cerbeza", Value="1", Selected =false },
            new SelectListItem{ Text ="Vinos y Licores", Value="2", Selected =false },
            new SelectListItem{ Text ="Cualquier Bebida", Value="3", Selected =false }
        };

        public static List<SelectListItem> DrugsAnswers = new List<SelectListItem> { 
            new SelectListItem { Text ="Nunca", Value="1",Selected = false },
            new SelectListItem { Text ="Esporadicamente", Value="2",Selected = false },
            new SelectListItem { Text ="Cualquier Dia", Value="3",Selected = false },
            new SelectListItem { Text ="Deja de consumir hace..", Value="4",Selected = false },
            new SelectListItem { Text ="Solo lo probe hace años atras", Value="5",Selected = false }
        };

        public static List<SelectListItem> PhysicalActivityAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Nunca", Value="1",Selected =false },
            new SelectListItem{ Text ="2 ó 3 Veces por Semana", Value="2",Selected =false },
            new SelectListItem{ Text ="Fines de Semana", Value="3",Selected =false },
            new SelectListItem{ Text ="Diariamente", Value="4",Selected =false }
        };

        public static List<SelectListItem> UseOfMedicationsAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Ocasionalmente", Value="1" },
            new SelectListItem{ Text="En los ultimos 15 dias", Value="2" },
        };

        public static List<SelectListItem> DentalDiseasesAnswers = new List<SelectListItem> {
            new SelectListItem { Text ="Caries",Value ="1", Selected =false },
            new SelectListItem { Text ="Empastes",Value ="2", Selected =false },
            new SelectListItem { Text ="Sangrado de Encias(Gingivitis)",Value ="3", Selected =false },
            new SelectListItem { Text ="Ortodoncia(Frenos)",Value ="4", Selected =false },
            new SelectListItem { Text ="Falta de dientes y/o muelas",Value ="5", Selected =false },
            new SelectListItem { Text ="Uso de puente y corona",Value ="6", Selected =false },
            new SelectListItem { Text ="Uso de placa dental",Value ="7", Selected =false },
        };

        public static List<SelectListItem> HormonalDiseasesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Tiroides", Value="1",Selected=false },
            new SelectListItem{ Text ="Hipófisis", Value="2",Selected=false },
            new SelectListItem{ Text ="Ovarios", Value="3",Selected=false },
            new SelectListItem{ Text ="Testiculos", Value="4",Selected=false },
            new SelectListItem{ Text ="Esterlidad", Value="5",Selected=false },
            new SelectListItem{ Text ="Otro", Value="6",Selected=false },
        };

        public static List<SelectListItem> LungDiseaseAnswers = new List<SelectListItem> { 
            new SelectListItem{ Text ="Asma", Value="1",Selected =false},
            new SelectListItem{ Text ="Bronquios", Value="2",Selected =false},
            new SelectListItem{ Text ="Nuemonia/Pulmonia", Value="3",Selected =false},
            new SelectListItem{ Text ="Tuberculosis", Value="4",Selected =false},
            new SelectListItem{ Text ="Otros", Value="5",Selected =false},
        };

        public static List<SelectListItem> HeartDiseaseAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Arritmia", Value ="1", Selected =false},
            new SelectListItem{ Text="Soplo", Value ="2", Selected =false},
            new SelectListItem{ Text="Valvulas dañadas", Value ="3", Selected =false},
            new SelectListItem{ Text="Angina de pecho", Value ="4", Selected =false},
            new SelectListItem{ Text="Infarto cardiaco", Value ="5", Selected =false},
            new SelectListItem{ Text="Embolia", Value ="6", Selected =false},
            new SelectListItem{ Text="Derrame", Value ="7", Selected =false},
            new SelectListItem{ Text="Insuficiencia Cardiaca", Value ="8", Selected =false},
        };

        public static List<SelectListItem> AlteredBloodPressureAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Alta Presion", Value="1",Selected =false},
            new SelectListItem{ Text="Baja Presion", Value="2",Selected =false},
        };

        public static List<SelectListItem> MedicalControlAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="IMSS",Value="1",Selected =false},
            new SelectListItem{ Text="Centro de Salud",Value="2",Selected =false},
            new SelectListItem{ Text="ISSSTE",Value="3",Selected =false},
            new SelectListItem{ Text="Municipio",Value="4",Selected =false},
            new SelectListItem{ Text="Medico Particular",Value="5",Selected =false},
        };

        public static List<SelectListItem> DiabetesMellitusMedicControlAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="IMSS",Value="1",Selected =false},
            new SelectListItem{ Text="Centro de Salud",Value="2",Selected =false},
            new SelectListItem{ Text="ISSSTE",Value="3",Selected =false},
            new SelectListItem{ Text="Municipio",Value="4",Selected =false},
            new SelectListItem{ Text="Medico Particular",Value="5",Selected =false},
        };

        public static List<SelectListItem> AlteredBloodPressureMedicalControlAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="IMSS",Value="1",Selected =false},
            new SelectListItem{ Text="Centro de Salud",Value="2",Selected =false},
            new SelectListItem{ Text="ISSSTE",Value="3",Selected =false},
            new SelectListItem{ Text="Municipio",Value="4",Selected =false},
            new SelectListItem{ Text="Medico Particular",Value="5",Selected =false},
        };

        


        public static List<SelectListItem> DigestiveDiseaseAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Reflujo", Value="1", Selected =false },
            new SelectListItem{ Text="Gastritis", Value="2", Selected =false },
            new SelectListItem{ Text="Ulcera", Value="3", Selected =false },
            new SelectListItem{ Text="Colitis", Value="4", Selected =false },
            new SelectListItem{ Text="Diarreas", Value="5", Selected =false },
            new SelectListItem{ Text="Estreñimiento", Value="6", Selected =false },
        };

        public static List<SelectListItem> LiverDiseaseAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Hepatites",Value="1",Selected =false },
            new SelectListItem{ Text="Ictericia(Piel Amarilla)",Value="2",Selected =false },
            new SelectListItem{ Text="Higado Graso",Value="3",Selected =false },
            new SelectListItem{ Text="Cirrosis",Value="4",Selected =false },
            new SelectListItem{ Text="Piedras eb Vesicula Biliar",Value="5",Selected =false },
        };

        public static List<SelectListItem> DiabetesMellitusAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Dieta",Value="1",Selected =false},
            new SelectListItem{ Text ="Antidiabeticos Orales",Value="2",Selected =false},
            new SelectListItem{ Text ="Insulina",Value="3",Selected =false},
            new SelectListItem{ Text ="No tengo ningun control",Value="4",Selected =false},
        };

        public static List<SelectListItem> KidneyDiseaseAnswers = new List<SelectListItem>
        {
            new SelectListItem{ Text="Infecciones de Orina",Value="1",Selected=false },
            new SelectListItem{ Text="Sangre al Orinar",Value="2",Selected=false },
            new SelectListItem{ Text="Dolor al Orinar",Value="3",Selected=false },
            new SelectListItem{ Text="Colico o Dolor Renal",Value="4",Selected=false },
            new SelectListItem{ Text="Enf. de la Prostata",Value="5",Selected=false },
            new SelectListItem{ Text="Arenilla o Piedras en el Riñon",Value="6",Selected=false },
        };

        public static List<SelectListItem> NeurologicalDiseasesAnswers = new List<SelectListItem> {
        
            new SelectListItem{ Text="Dolor de Cabeza Frecunte",Value="1",Selected =false },
            new SelectListItem{ Text="Migraña",Value="2",Selected =false },
            new SelectListItem{ Text="Temblores",Value="3",Selected =false },
            new SelectListItem{ Text="Vertigo/Mareos",Value="4",Selected =false },
            new SelectListItem{ Text="Convulciones/Epilepcia",Value="5",Selected =false },
            new SelectListItem{ Text="Paralisis",Value="6",Selected =false },
            new SelectListItem{ Text="Desmayos",Value="7",Selected =false },
            new SelectListItem{ Text="Otros",Value="8",Selected =false },
        };

        public static List<SelectListItem> PsychiatricIllnessesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Ansiedad",Value="1",Selected=false},
            new SelectListItem{ Text="Depresion",Value="2",Selected=false},
            new SelectListItem{ Text="Fabios o Miedos",Value="3",Selected=false},
            new SelectListItem{ Text="Insomio/Dificultad para dormir",Value="4",Selected=false},
            new SelectListItem{ Text="Psicosis",Value="5",Selected=false},
        };

        public static List<SelectListItem> MusculoskeletalDiseasesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Esguince",Value="1",Selected=false },
            new SelectListItem{ Text="Fisura",Value="2",Selected=false },
            new SelectListItem{ Text="Fractura Osea",Value="3",Selected=false },
            new SelectListItem{ Text="Luzacion ó Dislocacion",Value="4",Selected=false },
            new SelectListItem{ Text="Lesion de Tendon",Value="5",Selected=false },
            new SelectListItem{ Text="Artritis",Value="6",Selected=false },
            new SelectListItem{ Text="Enf Reumaticas",Value="7",Selected=false },
            new SelectListItem{ Text="Anputaciones",Value="8",Selected=false },
            new SelectListItem{ Text="Lumbagia",Value="9",Selected=false },
            new SelectListItem{ Text="Lesion del Nervio Ciatico",Value="10",Selected=false },
            new SelectListItem{ Text="Problemas de Columna",Value="11",Selected=false },
            new SelectListItem{ Text="cuello",Value="12",Selected=false }
           
        };

        public static List<SelectListItem> InfectiousDiseasesAnswers = new List<SelectListItem> {
            new SelectListItem{ Text ="Fiebre Tifoidea", Value="1", Selected=false },
            new SelectListItem{ Text ="Tuberculosis", Value="2", Selected=false },
            new SelectListItem{ Text ="Hepatitis", Value="3", Selected=false },
            new SelectListItem{ Text ="Meningitis", Value="4", Selected=false },
            new SelectListItem{ Text ="VIH/Sida", Value="5", Selected=false },
            new SelectListItem{ Text ="Fiebre Malta/Brucelosis", Value="6", Selected=false },
            new SelectListItem{ Text ="Paludismo(Malaria)", Value="7", Selected=false },
            new SelectListItem{ Text ="Dengue", Value="8", Selected=false },
        };

        public static List<SelectListItem> AdmissionToHospitalAnswers = new List<SelectListItem> {
            new SelectListItem{ Text="Para Observacion", Value="1", Selected=false },
            new SelectListItem{ Text="Por Enfermedad", Value="2", Selected=false },
            new SelectListItem{ Text="Por Cirugia", Value="3", Selected=false },
        };

        public static List<SelectListItem> ChronicIllnessAnswers = new List<SelectListItem> { 
            new SelectListItem{ Text="Hipertension Arterial", Value="1", Selected =false },
            new SelectListItem{ Text="Diabetes", Value="2", Selected =false },
            new SelectListItem{ Text="Artritis", Value="3", Selected =false },
            new SelectListItem{ Text="Lupus", Value="4", Selected =false },
            new SelectListItem{ Text="Psoriasis", Value="5", Selected =false },
            new SelectListItem{ Text="Vitiligo", Value="6", Selected =false },
            new SelectListItem{ Text="Alcoholismo", Value="7", Selected =false },
            new SelectListItem{ Text="Depresión", Value="8", Selected =false },
            new SelectListItem{ Text="Ansiedad", Value="9", Selected =false },
            new SelectListItem{ Text="Otras", Value="10", Selected =false }
        };

        #endregion


        #endregion
    }
}
