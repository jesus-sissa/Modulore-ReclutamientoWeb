using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Modulo_Reclutamiento_Web.Models.MedicalQuestionData
{
    public class PersonalMedicalHistory
    {
        public int? Id { get; set; }
        /// <summary>
        /// Valor de Presion Arterial <br></br>
        /// Columna : PresionArterial
        /// </summary>
        [Display(Name = "Presión Arterial")]
        public string? BloodPressure { get; set; }
        /// <summary>
        /// Valor de Glucosa <br></br>
        /// Columna : Glucosa
        /// </summary>
        [Display(Name = "Glucosa")]
        public string? Glucose { get; set; }
        /// <summary>
        /// Valor de IMC <br></br>
        /// Columna : IMC
        /// </summary>
        [Display(Name = "IMC")]
        public string? IMC { get; set; }
        public List<SelectListItem> IMCAnswers { get; set; } = Answers.IMCAnswers;
        /// <summary>
        /// Observaciones del IMC<br></br>
        /// Columna : Observaciones_IMC
        /// </summary>
        [Display(Name = "Observaciones")]
        public string? Remarks { get; set; }
        //--------------------------------------------------------------------
        /// <summary>
        /// Uso Lentes ó Pupilentes
        /// Columna : UsoLentesORPup
        /// </summary>
        [Display(Name = "Uso Lentes ó Pupilentes")]
        public string? WearGlassesOrPupils { get; set; }
        /// <summary>
        /// Afirma o niega el uso de lentes o pupilentes<br></br>
        /// Columna : UsoLentesORPup
        /// </summary>
        public List<SelectListItem> WearGlassesOrPupilsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Uso Lentes ó Pupilentes Desde
        /// Columna : ULP_Desde
        /// </summary>
        [Display(Name = "Desde")]
        public string? WearGlassesOrPupilsSince { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Respuesta complemento si afirmas usar lentes o pupilentes <br></br>
        /// Columna : ULP_Respuesta
        /// </summary>
        public string WearGlassesOrPupilsAnswer { get; set; }
        /// <summary>
        /// Respuestas complemento al afirmar usar lentes o pupilentes
        /// </summary>
        public List<SelectListItem> WearGlassesOrPupilsAnswers { get; set; } = Answers.WearGlassesOrPupilsAnswers;
      
        [Display(Name = "Antecedentes Familiares(Abuelos, Padre, Madre Hermanos/as)")]
        public string? FamyHistory { get; set; }
        /// <summary>
        /// Respuesta de AntecedentesFamiliares <br></br>
        /// Columna : AntecedentesFamiliares
        /// </summary>
        public List<SelectListItem> FamilyHistoryAnswers { get; set; } = Answers.FamilyHistoryAnswers;
        //----------------------------------------------------------------------------
        /// <summary>
        /// Enfermedad Congénita o Hereditaria <br></br>
        /// Columna : EnfermedadCongenORHered
        /// </summary>
        [Display(Name = "Enfermedad Congénita o Hereditaria")]
        public string? CongenitalOrInheritedDisease { get; set; }
        public List<SelectListItem> CongenitalOrInheritedDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Deformidad Congénita
        /// </summary>
        [Display(Name = "Deformidad Congénita")]
        public string? CongenitalDeformity { get; set; }
        public List<SelectListItem> CongenitalDeformityOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Enfermedades Infantiles Importantes
        /// </summary>
        [Display(Name = "Enfermedades Infantiles Importantes?")]
        public string? ChildhoodDiseases { get; set; }
        public List<SelectListItem> ChildhoodDiseasesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        ///lista de Enfermedades Infantiles Importantes
        /// </summary>
        public List<SelectListItem> ChildhoodDiseasesAnswers { get; set; } = Answers.ChildhoodDiseasesAnswers;
        /// <summary>
        /// Otra/s enfermedades infantiles importantes
        /// </summary>
        [Display(Name = "Otra")]
        public string? ChildhoodDiseasesOther { get; set; }
        /// <summary>
        /// Alergias
        /// </summary>
        [Display(Name = "Alergias?")]
        public string? Allergies { get; set; }
        public List<SelectListItem> AllergiesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de cosas que puede ser alergico
        /// </summary>
        public List<SelectListItem> AllergiesAnswers { get; set; } = Answers.AllergiesAnswers;
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Ve Usted Bien
        /// </summary>
        [Display(Name = "Ve Usted Bien")]
        public string? SeeYourselfWell { get; set; }
        public List<SelectListItem> SeeYourselfWellOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Ojo Derecho
        /// </summary>
        [Display(Name = "Ojo Derecho")]
        public string? RightEye { get; set; }
        public List<SelectListItem> RightEyeOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Ojo Izquierdo
        /// </summary>
        [Display(Name = "Ojo Izquierdo")]
        public string? LeftEye { get; set; }
        public List<SelectListItem> LeftEyeOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Cirugía Ocular
        /// </summary>
        [Display(Name = "Cirugía Ocular")]
        public string? EyeSurgery { get; set; }
        public List<SelectListItem> EyeSurgeryOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Por, razon por la que ocurrio la cirugia
        /// </summary>
        [Display(Name = "Por")]
        public string? EyeSurgeryBy { get; set; }
        /// <summary>
        /// Fecha de cirugia
        /// </summary>
        [Display(Name = "Fecha")]
        public string? dateSurgery { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Usa lentes ó lentes de contacto
        /// </summary>
        [Display(Name = "Usa lentes ó lentes de contacto?")]
        public string? WearGlassesOrContactLenses { get; set; }
        public List<SelectListItem> WearGlassesOrContactLensesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Razones por las que usa lentes o lentes de contacto
        /// </summary>
        public List<SelectListItem> WearGlassesOrContactLensesAnswers { get; set; } = Answers.WearGlassesOrContactLensesAnswers;

        //------------------------------------------------------------------------------------
        /// <summary>
        /// Enfermedades de los oido
        /// </summary>
        [Display(Name = "Enfermedades de los oido?")]
        public string? EarDisease { get; set; }
        public List<SelectListItem> EarDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Cual, enfermedad de oido que posee
        /// </summary>
        [Display(Name = "Cual?")]
        public string? EarDiseaseAnswer { get; set; }
        /// <summary>
        /// Escucha usted bien
        /// </summary>
        [Display(Name = "Escucha usted bien?")]
        public string? ListenWell { get; set; }
        public List<SelectListItem> ListenWellOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Usa aparato auditivo
        /// </summary>
        [Display(Name = "Usa aparato auditivo?")]
        public string? UseHearingAid { get; set; }
        public List<SelectListItem> UseHearingAidOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Desde cuando?, utiliza el aparato auditivo
        /// </summary>
        [Display(Name = "Desde cuando?")]
        public string? UseHearingAidsince { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        /// <summary>
        /// Enfermedades Dentales
        /// </summary>
        [Display(Name = "Enfermedades Dentales")]
        public string? DentalDiseases { get; set; }
        public List<SelectListItem> DentalDiseasesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de enfermedades dentales
        /// </summary>
        public List<SelectListItem> DentalDiseasesAnswers { get; set; } = Answers.DentalDiseasesAnswers;

        /// <summary>
        /// Enfermedades Hormonales
        /// </summary>
        [Display(Name ="Enfermedades Hormonales")]
        public string? HormonalDiseases { get; set; }
        /// <summary>
        /// lista de enfermedades hormonales
        /// </summary>
        public List<SelectListItem> HormonalDiseasesAnswers { get; set; } = Answers.HormonalDiseasesAnswers;
        /// <summary>
        /// otra enfermedad hormonal que no se incluya en la lista
        /// </summary>
        [Display(Name ="Otras")]
        public string? HormonalDiseasesOther { get; set; }

        /// <summary>
        /// Enfermedad de los Pulmones
        /// </summary>
        [Display(Name ="Enfermedad de los Pulmones")]
        public string? LungDisease { get; set; }
        public List<SelectListItem> LungDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Lista de enfermedades pulmonares
        /// </summary>
        public List<SelectListItem> LungDiseaseAnswers { get; set; } = Answers.LungDiseaseAnswers;
        /// <summary>
        /// otras enfermedades pulmonares que no se incluyan en la lista
        /// </summary>
        [Display(Name = "Otra")]
        public string? LungDiseaseOther { get; set; }
        /// <summary>
        /// Enfermedades del Corazon"
        /// </summary>
        [Display(Name ="Enfermedades del Corazón")]
        public string? HeartDisease { get; set; }
        public List<SelectListItem> HeartDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de enfermedades del corazon
        /// </summary>
        public List<SelectListItem> HeartDiseaseAnswers { get; set; } = Answers.HeartDiseaseAnswers;
        /// <summary>
        /// Alteracion de la Presion Arterial
        /// </summary>
        [Display(Name ="Alteración de la Presión Arterial")]
        public string? AlteredBloodPressure { get; set; }
        public List<SelectListItem> AlteredBloodPressureOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de tipos de alteracion de la presion arterial
        /// </summary>
        public List<SelectListItem> AlteredBloodPressureAnswers { get; set; } = Answers.AlteredBloodPressureAnswers;
        /// <summary>
        /// Medicamentos, para alteracion de la presion arterial
        /// </summary>
        [Display(Name ="Medicamentos:")]
        public string? AlteredBloodPressureMedicaments { get; set; }
        /// <summary>
        /// Estoy en Control Medico, para la alteracion de la presion arterial
        /// </summary>
        [Display(Name ="Estoy en Control Medico")]
        public string? AlteredBloodPressureMedicalControl { get; set; }
        public List<SelectListItem> AlteredBloodPressureMedicalControlOp { get; } = Answers.YesNoAnswers; 
        /// <summary>
        /// lista de lugares donde llevo mi control medico de la alteracion de la presion arterial
        /// </summary>
        public List<SelectListItem> AlteredBloodPressureMedicalControlAnswers { get; set; } = Answers.AlteredBloodPressureMedicalControlAnswers;
        /// <summary>
        /// Enfermedades Digestivas
        /// </summary>
        [Display(Name ="Enfermedades Digestivas")]
        public string? DigestiveDisease { get; set; }
        public List<SelectListItem> DigestiveDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de enfermedades digestivas
        /// </summary>
        public List<SelectListItem> DigestiveDiseaseAnswers { get; set; } = Answers.DigestiveDiseaseAnswers;
        /// <summary>
        /// Enfermedad del Higado Y/o Vias Biliares
        /// </summary>
        [Display(Name ="Enfermedad del Higado Y/o Vias Biliares")]
        public string? LiverDisease { get; set; }
        public List<SelectListItem> LiverDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedad del Higado Y/o Vias Biliares
        /// </summary>
        public List<SelectListItem> LiverDiseaseAnswers { get; set; } = Answers.LiverDiseaseAnswers;
        /// <summary>
        /// Diabetes Mellitus/Azucar
        /// </summary>
        [Display(Name ="Diabetes Mellitus/Azúcar")]
        public string? DiabetesMellitus { get; set; }
        public List<SelectListItem> DiabetesMellitusOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Diabetes Mellitus/Azucar
        /// </summary>
        public List<SelectListItem> DiabetesMellitusAnswers { get; set; } = Answers.DiabetesMellitusAnswers;
        /// <summary>
        /// Fecha de ultimo Examen de Sangre
        /// </summary>
        [Display(Name ="Fecha de último Examen de Sangre")]
        public string? DateOfLastExamination { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Control Medico de la Diabetes Mellitus/Azucar
        /// </summary>
        [Display(Name ="Control Medico")]
        public string? DiabetesMellitusMedicControl { get; set; }
        public List<SelectListItem> DiabetesMellitusMedicControlOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de lugares donde llevo mi control medico de la Diabetes Mellitus/Azucar
        /// </summary>
        public List<SelectListItem> DiabetesMellitusMedicControlAnswers { get; set; } = Answers.DiabetesMellitusMedicControlAnswers;

        /// <summary>
        /// Enfermedades del Colesterol y/o Trigliceridos
        /// </summary>
        [Display(Name ="Enfermedades del Colesterol y/o Trigliceridos")]
        public string? CholesterolDisease { get; set; }
        public List<SelectListItem> CholesterolDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Medicamentos para controlar las Enfermedades del Colesterol y/o Trigliceridos
        /// </summary>
        [Display(Name ="Medicamentos:")]
        public string? CholesterolDiseaseMedications { get; set; }
        /// <summary>
        /// Enfermedad del Acido Urico
        /// </summary>
        [Display(Name ="Enfermedad del Acido Úrico")]
        public string? UricAcidDisease { get; set; }
        public List<SelectListItem> UricAcidDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Medicamentos para controlar las Enfermedad del Acido Urico
        /// </summary>
        [Display(Name ="Medicamentos:")]
        public string? UricAcidDiseaseMedication { get; set; }
        /// <summary>
        /// Enfermedad del Riñon y/o Urologicas
        /// </summary>
        [Display(Name ="Enfermedad del Riñón y/o Urologicas")]
        public string? KidneyDisease { get; set; }
        public List<SelectListItem> KidneyDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de enfermedades del Riñon y/o Urologicas
        /// </summary>
        public List<SelectListItem> KidneyDiseaseAnswers { get; set; } = Answers.KidneyDiseaseAnswers;

        /// <summary>
        /// Enfermedades Neurologicas
        /// </summary>
        [Display(Name ="Enfermedades Neurologicas")]
        public string? NeurologicalDiseases { get; set; }
        public List<SelectListItem> NeurologicalDiseasesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedades Neurologicas
        /// </summary>
        public List<SelectListItem> NeurologicalDiseasesAnswers { get; set; } = Answers.NeurologicalDiseasesAnswers;

        /// <summary>
        /// Enfermedades Psiquiatricas
        /// </summary>
        [Display(Name = "Enfermedades Psiquiatricas")]
        public string? PsychiatricIllnesses { get; set; }
        public List<SelectListItem> PsychiatricIllnessesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedades Psiquiatricas
        /// </summary>
        public List<SelectListItem> PsychiatricIllnessesAnswers { get; set; } = Answers.PsychiatricIllnessesAnswers;

        /// <summary>
        /// Enfermedades Osteo Musculares Huesos y/o Musculo
        /// </summary>
        [Display(Name ="Enfermedades Osteo Musculares Huesos y/o Musculo")]
        public string? MusculoskeletalDiseases { get; set; }
        public List<SelectListItem> MusculoskeletalDiseasesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedades Osteo Musculares Huesos y/o Musculo
        /// </summary>
        public List<SelectListItem> MusculoskeletalDiseasesAnswers { get; set; } = Answers.MusculoskeletalDiseasesAnswers;

        /// <summary>
        /// Enfermedades de la Piel
        /// </summary>
        [Display(Name ="Enfermedades de la Piel")]
        public string? SkinDisease { get; set; }
        public List<SelectListItem> SkinDiseaseOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Enfermedad de las Uñas
        /// </summary>
        [Display(Name ="Enfermedad de las Uñas")]
        public string? NailDisease { get; set; }
        public List<SelectListItem> NailDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Enfermedad del Cabello
        /// </summary>
        [Display(Name ="Enfermedad del Cabello")]
        public string? HairDisease { get; set; }
        public List<SelectListItem> HairDiseaseOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Enfermedades Infecciosas Relevantes
        /// </summary>
        [Display(Name ="Enfermedades Infecciosas Relevantes")]
        public string? InfectiousDiseases { get; set; }
        public List<SelectListItem> InfectiousDiseasesOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedades Infecciosas Relevantes
        /// </summary>
        public List<SelectListItem> InfectiousDiseasesAnswers { get; set; } = Answers.InfectiousDiseasesAnswers;

        /// <summary>
        /// Accidentes de Importancia(Requirieron Atencion Medica y/o Hospital)
        /// </summary>
        [Display(Name ="Accidentes de Importancia(Requirieron Atencion Medica y/o Hospital)")]
        public string? MajorAccidents { get; set; }
        public List<SelectListItem> MajorAccidentsOp { get;} = Answers.YesNoAnswers;
        /// <summary>
        /// Has Recibido en alguna Ocasión una Trasfusión de Sangre y/o plaquetas
        /// </summary>
        [Display(Name ="Has Recibido en alguna Ocasión una Trasfusión de Sangre y/o plaquetas")]
        public string? BloodTransfusion { get; set; }
        public List<SelectListItem> BloodTransfusionOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Ingreso al Hospital
        /// </summary>
        [Display(Name ="Ingreso al Hospital")]
        public string? AdmissionToHospital { get; set; }
        public List<SelectListItem> AdmissionToHospitalOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de razones por la cual Ingreso al Hospital
        /// </summary>
        public List<SelectListItem> AdmissionToHospitalAnswers { get; set; } = Answers.AdmissionToHospitalAnswers;
        /// <summary>
        /// Cirugias Realizadas
        /// </summary>
        [Display(Name ="Cirugias Realizadas")]
        public string? SurgeriesPerformed { get; set; }

        /// <summary>
        /// Tiene Usted Alguna Secuela
        /// </summary>
        [Display(Name ="Tiene Usted Alguna Secuela")]
        public string? SomeSequel { get; set; }
        public List<SelectListItem> SomeSequelOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Tiene Usted Algun Impedimento Fisico y/o Psicologico Emocional
        /// </summary>
        [Display(Name ="Tiene Usted Algun Impedimento Fisico y/o Psicologico Emocional")]
        public string? PhysicalOrPsychologicalImpairment { get; set; }
        public List<SelectListItem> PhysicalOrPsychologicalImpairmentOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Tiene usted alguna Enfermedad Cronica
        /// </summary>
        [Display(Name ="Tiene usted alguna Enfermedad Cronica")]
        public string? ChronicIllness { get; set; }
        public List<SelectListItem> ChronicIllnessOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de Enfermedades Cronicas
        /// </summary>
        public List<SelectListItem> ChronicIllnessAnswers { get; set; } = Answers.ChronicIllnessAnswers;
        /// <summary>
        /// otras Enfermedades Cronicas que posea
        /// </summary>
        [Display(Name ="Otra")]
        public string? ChronicIllnessOther { get; set; }

        /// <summary>
        /// Ha Padecido Cancer y/o Algun Tumor Maligno
        /// </summary>
        [Display(Name ="Ha Padecido Cáncer y/o Algun Tumor Maligno")]
        public string? SufferedFromCancerOrMalignantTumor { get; set; }
        public List<SelectListItem> SufferedFromCancerOrMalignantTumorOp { get; } = Answers.YesNoAnswers;

        /// <summary>
        /// Tiene Usted Varices
        /// </summary>
        [Display(Name ="Tiene Usted Varices")]
        public string? HaveVaricoseVeins { get; set; }
        public List<SelectListItem> HaveVaricoseVeinsOp { get; } = Answers.YesNoAnswers;

        //habitos
        //tabaco
        /// <summary>
        /// Tabaco
        /// </summary>
        [Display(Name = "Tabaco")]
        public string Tabaco { get; set; }
        public List<SelectListItem> TabacoOp { get; set; } = Answers.TabacoAnswers;
        /// <summary>
        /// Cantidad de cigarros que consume
        /// </summary>
        public string? TabacoQuantity { get; set; }
        /// <summary>
        /// Fecha en la cual deje de fumar
        /// </summary>
        [Display(Name ="Fecha en la cual deje de fumar")]
        public string? TabacoDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");

        //alcohol
        /// <summary>
        /// Alcohol
        /// </summary>
        [Display(Name = "Alcohol")]
        public string? Alcohol { get; set; }
        public List<SelectListItem> AlcoholOp { get; } = Answers.AlcoholAnswers;
        /// <summary>
        /// Tipo de Bebida
        /// </summary>
        [Display(Name ="Tipo de Bebida")]
        public string Drink { get; set; }
        /// <summary>
        /// lista de tipod de bebidas
        /// </summary>
        public List<SelectListItem> Drinktype { get; set; } = Answers.DrinkTypeAnswers;

        // Drogas
        /// <summary>
        /// Drogas
        /// </summary>
        [Display(Name ="Drogas")]
        public string Drugs { get; set; }
        public List<SelectListItem> DrugsOp { get; } = Answers.DrugsAnswers;
        /// <summary>
        /// Fecha en la que dejo de consumir
        /// </summary>
        [Display(Name ="Fecha en la que dejo de consumir")]
        public string DrugsDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Tipo de Droga
        /// </summary>
        [Display(Name = "Tipo de Droga")]
        public string? DrugType { get; set; }

        //deportes / actividad fisica
        /// <summary>
        /// Deportes/Actividad Fisica
        /// </summary>
        [Display(Name ="Deportes/Actividad Fisica")]
        public string? PhysicalActivity { get; set; }
        /// <summary>
        /// lista de veces de Deportes/Actividad Fisica
        /// </summary>
        public List<SelectListItem> PhysicalActivityOp { get; } = Answers.PhysicalActivityAnswers;
        /// <summary>
        /// Tipo de Ejercicio
        /// </summary>
        [Display(Name ="Tipo de Ejercicio")]
        public string? PhysicalActivityType { get; set; }
        /// <summary>
        /// Tiempo dedicado
        /// </summary>
        [Display(Name = "Tiempo dedicado")]
        public string? PhysicalActivityTimeSpent { get; set; }

        /// <summary>
        /// Uso de Medicamentos
        /// </summary>
        [Display(Name = "Uso de Medicamentos")]
        public string? UseOfMedications { get; set; }
        public List<SelectListItem> UseOfMedicationsOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// lista de veces que Uso Medicamentos
        /// </summary>
        public List<SelectListItem> UseOfMedicationsAnswers { get; set; } = Answers.UseOfMedicationsAnswers;
        /// <summary>
        /// Nombre de Medicamento que usa
        /// </summary>
        [Display(Name = "Nombre de Medicamento")]
        public string? MedicationName { get; set; }
        /// <summary>
        /// Para que es usado el medicamento
        /// </summary>
        [Display(Name = "Usado para:")]
        public string? UsedFor { get; set; }


        /// <summary>
        /// Horas de Sueño al dia
        /// </summary>
        [Display(Name = "Horas de Sueño al dia")]
        public decimal? TimeOfsleeping { get; set; }
        /// <summary>
        /// Otros trabajos a desempeñar
        /// </summary>
        [Display(Name = "Otros trabajos a desempeñar")]
        public string? OtherJobsToBePerformed { get; set; }
        public List<SelectListItem> OtherJobsToBePerformedOp { get; } = Answers.YesNoAnswers;
        /// <summary>
        /// Donde realiza Otros trabajos
        /// </summary>
        [Display(Name = "En Donde:")]
        public string? OtherJobsWhere { get; set; }
        /// <summary>
        /// Que hace en el Otro trabajos
        /// </summary>
        [Display(Name = "Que hace:")]
        public string? OtherJobsWhatsHeDoing { get; set; }
        /// <summary>
        /// Desde cuando (Fecha) realiza el otro trabajo
        /// </summary>
        [Display(Name = "Desde:")]
        public string? OtherJobsSince { get; set; } =DateTime.Now.ToString("yyyy-MM-dd");
        /// <summary>
        /// Realiza Tareas Domesticas
        /// </summary>
        [Display(Name = "Realiza Tareas Domesticas")]
        public string? PerformHouseholdChores { get; set; }
        public List<SelectListItem> PerformHouseholdChoresOp { get; } = Answers.YesNoAnswers;


    }
}
