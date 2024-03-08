using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using Modulo_Reclutamiento_Web.Models.HomeVisitData;
using Modulo_Reclutamiento_Web.Models.MedicalQuestionData;
using Modulo_Reclutamiento_Web.Service;
using Newtonsoft.Json;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class ProspectusController : Controller
    {
        #region Views
        [Authorize(Roles = "User")]
        public IActionResult Index(int pto, string name)
        {
            ViewBag.pto = pto;
            var _propValidadoRepres = RepresentativeService.Instancia.IsValidateProspectus(pto);
            ViewBag.ValRepre = _propValidadoRepres;
            var _PropValidado = ProspectusService.Instancia.IsValidatePto(pto);
            ViewBag.PropValidado = _PropValidado;
            var _examResolv = ExamsService.Instancia.ExamBarsitResolve(pto);
            ViewBag.ExamResolv = _examResolv;

            var _RefLaboral = ProspectusService.Instancia.getEmploymentRef(pto);
            ViewBag.RefLaboral = _RefLaboral;
            var _RefPers = ProspectusService.Instancia.getPersonalRef(pto);
            ViewBag.RefPers = _RefPers;
            var idhomeVisit = ProspectusService.Instancia.getHomeVisitId(pto,"P");
            ViewBag.HV = idhomeVisit;
          
            User_Persistent_Data.ProspectusName = (User_Persistent_Data.ProspectusName != null && name == null) ? User_Persistent_Data.ProspectusName : name;
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Information(int pto)
        {
            ViewBag.pto = pto;
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult Agreements(int pto)
        {
            ViewBag.pto = pto;
            //var IsSigned = ProspectusService.Instancia.Check_Sign_AvisoPrivDatos(prosp);
            ViewBag.IsSigned = ProspectusService.Instancia.Check_Sign_AvisoPrivDatos(pto);
            User_Persistent_Data.Prospectus = pto;
            var informaction = ProspectusService.Instancia.getInformation(pto);
            //ViewBag.Key = informaction.Key_Prospectus;
            ViewBag.Name = informaction.Name;
            var _documentos = RecruiterService.Instancia.getDocumentProspectus(pto);
            ViewBag.IndexDoc = _documentos.Sum(x => x.Index);
            return View(_documentos);
        }
        [Authorize(Roles = "User")]
        public IActionResult AdditionalInformation(int pto)
        {
            ViewBag.pto = pto;
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult AcademicData(int pto, string? MSG)
        {
            ViewBag.pto = pto;
            ViewBag.MSG = MSG;
             var Academic = ProspectusService.Instancia.NewAcademic(pto);
            return View(Academic);
        }
        [Authorize(Roles = "User")]
        public IActionResult FamilyData(int pto, int? MSG)
        {
            ViewBag.pto = pto;
            ViewBag.MSG = MSG;
            var _family = ProspectusService.Instancia.NewFamily(pto);
            return View(_family);
        }
        [Authorize(Roles = "User")]
        public IActionResult EmploymentData(int pto, int? MSG) 
        {
            ViewBag.pto = pto;
            ViewBag.MSG = MSG;
            var employment = ProspectusService.Instancia.NewEmployment(pto, null);
            return View(employment);
        }
        [Authorize(Roles = "User")]
        public IActionResult PersonalReferencesData(int pto, int? MSG)
        {
            ViewBag.pto = pto;
            ViewBag.MSG = MSG;
            var references = ProspectusService.Instancia.NewPersonalOrEmploymentReferences(pto, 1,1);
            return View(references);
        }
        [Authorize(Roles = "User")]
        public IActionResult Document(int pto, int doc, string status)
        {
            ViewBag.pto = pto;
            //var IsSigned = ProspectusService.Instancia.Check_Sign_AvisoPrivDatos(prosp);
            //ViewBag.IsSigned = (IsSigned) ? "S" : "N";
            var informaction = ProspectusService.Instancia.getInformation(pto);
            informaction.Document = doc;
            informaction.Document_Status = status;
            informaction.FingerPrints = RecruiterService.Instancia.getFingerPrintsProspectus(pto);
            informaction.Document_Signatures = (informaction.IsValidatedByRepresentantive) ? RecruiterService.Instancia.getFirmasDocuments(pto) : User_Persistent_Data.Signatures;
            return View(informaction);
        }
        /// <summary>
        /// Vista Cuestionario Medico
        /// </summary>
        /// <param name="pto">Id de Prospecto</param>
        /// <returns>Retorna Vsta con Cuestionario Medico vacio o lleno</returns>
        [Authorize(Roles = "User")]
        public IActionResult MedicalQuestionnaire(int pto, int filter, int? msg,string? stat)
        {
            ViewBag.pto = pto;
            ViewBag.msg = msg;
            ViewBag.filter =(filter==0)?1:filter;
            ViewBag.stat = stat;
            
            var medicalQuestion = ProspectusService.Instancia.NewMedicalQuestion(pto, (filter == 0) ? 1 : filter);
            var FirmadoCuestionario = ProspectusService.Instancia.CuestionarioFirmado(medicalQuestion.Id);
            ViewBag.CuestionnaireSigned = FirmadoCuestionario;
            return View(medicalQuestion);
        }

        #endregion

        #region AcademicData and CoursesData
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult AcademicDataSave(AcademicData academic)
        {
            bool success = false;
            int msg = 0;
            int action = 0;
            //insertar informacion academica
            if (academic.Action == 1)
            {
                success = ProspectusService.Instancia.SaveAcademicData(academic);
                if (success)
                {
                    action = 1;
                }

            }
            //actualizar informacion academica
            if (academic.Action == 2)
            {
                success = ProspectusService.Instancia.UpdateAcademicData(academic);
                if (success)
                {
                    action = 2;
                }
            }
            if (success && action == 1)
            {
                msg = 1;
            }
            if (success && action == 2)
            {
                msg = 2;
            }

            return RedirectToAction("AcademicData", "Prospectus", new { pto = academic.Id, MSG = msg });
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public Boolean DeleteADataCourse(int key)
        {
            var delete = ProspectusService.Instancia.DeleteAcademicData(key);

            return delete;
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public bool SaveDataCourse(CoursesReceived courses)
        {
            var save = ProspectusService.Instancia.CoursesDataSave(courses);
            return save;
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public bool DeleteDataCourse(int id)
        {
            var save = ProspectusService.Instancia.CoursesDataDelete(id);
            return save;
        }
        #endregion

        #region FamilyData
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveFamilyData(FamilyData familyData)
        {
            int _MSG = 0;
            var _save = ProspectusService.Instancia.SaveFamilyData(familyData);
            if (_save)
            {
                _MSG = 1;
            }

            return RedirectToAction("FamilyData", new { pto = familyData.Id, MSG = _MSG });
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public bool DeleteFamilyData(int id)
        {
            var save = ProspectusService.Instancia.DeleteFamilyData(id);
            return save;
        }
        #endregion

        #region EmploymentData
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveEmploymentData(EmploymentData employment) 
        {
            int _MSG = 0;
            var save = ProspectusService.Instancia.SaveEmploymentData(employment);
            if (save)
            {
                _MSG = 1;
            }

            return RedirectToAction("EmploymentData", new { pto = employment.Id,MSG= _MSG });
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public bool DeleteDataEmployment(int id)
        {
            var _delete = ProspectusService.Instancia.DeleteDataEmployment(id);
            return _delete;
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectCities(int country)
        {
            var employment = ProspectusService.Instancia.NewEmployment(0,country);
            return PartialView("../Prospectus/partialViews/selectViews/_selectCitiesOfContry", employment);
        }

        #endregion

        #region References laboral y personal
         //referencia personal
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SavePersonalReferencesData(References references)
        {
            int _MSG = 0;
            //references.PersonalOrLabor = "P";
            var _save = ProspectusService.Instancia.SaveReferencesData(references);
            if (_save)
            {
                _MSG = 1;
            }

            return RedirectToAction("PersonalReferencesData", new { pto = references.Id, MSG = _MSG });
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        //eliminar referencia
        public bool DeleteReferencesData(int id)
        {
            var deleted = ProspectusService.Instancia.DeleteDataReferences(id);
            return deleted;
        }

        /// referencia laboral

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveLaborReferencesData(References references)
        {
            int _MSG = 0;
            references.TypeReferences = 0;
            //references.PersonalOrLabor = "L";
            var _save = ProspectusService.Instancia.SaveReferencesData(references);
            if (_save)
            {
                _MSG = 1;
            }

            return RedirectToAction("LaborReferencesData", new { pto = references.Id, MSG = _MSG });
        }
        //selects

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectCitiesRef(int country)
        {
            var references = ProspectusService.Instancia.NewPersonalOrEmploymentReferences(0, country);
            return PartialView("../Prospectus/partialViews/selectViews/_selectCitiesofCountry_Ref", references);
        }
        #endregion

        #region CuestionarioMedico

       
        /// <summary>
        /// Guarda el Cuestionario Medico
        /// </summary>
        /// <param name="medical">Objeto Cuestionario Medico</param>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SaveMedicalQuestionnaire(MedicalQuestion medical) 
        {
            bool _save = false;
            int _msg = 00;
            if (medical.personalMedicalHistoryId == 0 && medical.gynecologicalAntecentoId ==0 )
            {
                _save = ProspectusService.Instancia.SaveMedicalQuestion(medical);
                if (_save)
                {
                    _msg = 1;
                }
            }
            else 
            {
                _save = ProspectusService.Instancia.UpdateMedicalQuestion(medical);
                if (_save)
                {
                    _msg = 2;
                }
            }

            if (medical.stat == "R")
            {
                
                return RedirectToAction("MedicalQuestionnaire","Recruiter", new { pto = ((medical.filter == 1) ? medical.ProspectusId : medical.EmployeerId), filter = medical.filter, stat = "R" });
            }
            else 
            {
                return RedirectToAction("MedicalQuestionnaire", new { pto = medical.ProspectusId, medical.filter, msg = _msg });
            }

           
        }

        #endregion

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool ProspectusSignature(string signature, int pto)
        {

            var _result = ProspectusService.Instancia.Add_ProspectusSignature(signature, pto);
            if (_result)
            {
                User_Persistent_Data.Prospectus = pto;
            }
            return _result;

        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public bool ConfirmDocument(int doc, string beneficiarios, int pto)
        {
            var _result = ProspectusService.Instancia.Confirm_Document(pto, doc, beneficiarios);
            return _result;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool ConfirmMedicalQuestionnaireDocument(int medical) 
        {
            var _result = ProspectusService.Instancia.Confirm_MedicalQuestionnaire(medical);
            return _result;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool ConfirmProspectus(string password,int pto)
        {
            bool _result = false;
            if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password) 
            {
                _result = ProspectusService.Instancia.ValidatePto(pto, "S");
                if (_result)
                {
                  var mail =  new Email().SendingLeadApprovalMail(63, "Nuevo Prospecto En Espera de Aprobacion", User_Persistent_Data.ProspectusName);
                }
            }
               
            return _result;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool DesConfirmProspectus(string password, int pto)
        {
            bool _result = false;
            if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password)
            {
                _result = ProspectusService.Instancia.ValidatePto(pto, "N");
            }

            return _result;
        }



    }
}
