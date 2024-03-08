using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Service;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class RepresentativeController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            DateTime Fecha_Inicio = DateTime.Today.AddDays(-14);
            DateTime Fecha_Fin = DateTime.Today;
            var ListaProspectos = RepresentativeService.Instancia.get_ProspectusWithContrats(Convert.ToDateTime(Fecha_Inicio.ToString("yyyy-MM-dd")), Convert.ToDateTime(Fecha_Fin.ToString("yyyy-MM-dd")));
            return View(ListaProspectos);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Index(string Fecha_Inicio, string Fecha_Fin)
        {

            var ListaProspectos = RepresentativeService.Instancia.get_ProspectusWithContrats(Convert.ToDateTime(Fecha_Inicio), Convert.ToDateTime(Fecha_Fin));
            return View(ListaProspectos);
        }

        [Authorize(Roles = "User")]
        public IActionResult Employees(int pto)
        {
            ViewBag.pto = pto;
            var informaction = ProspectusService.Instancia.getInformation(pto);
            var _PropValidado = RepresentativeService.Instancia.IsValidateProspectus(pto);
            ViewBag.PropValidado = _PropValidado;
            ViewBag.Name = informaction.Name;
            ViewBag.IsEmployee = RepresentativeService.Instancia.IsEmployee(pto);
            User_Persistent_Data.ProspectusName = (User_Persistent_Data.ProspectusName != null && ViewBag.Name == null) ? User_Persistent_Data.ProspectusName : ViewBag.Name;
            var _documentos = RecruiterService.Instancia.getDocumentProspectus(pto);
            return View(_documentos);
        }

        [Authorize(Roles = "User")]
        public IActionResult Documents(int pto, int doc, string status)
        {
            ViewBag.pto = pto;
            var informaction = ProspectusService.Instancia.getInformation(pto);
            informaction.Document = doc;
            informaction.Document_Status = status;
            informaction.FingerPrints = RecruiterService.Instancia.getFingerPrintsProspectus(pto);
            informaction.Document_Signatures = (informaction.IsValidatedByRepresentantive) ? RecruiterService.Instancia.getFirmasDocuments(pto) : User_Persistent_Data.Signatures;
            //informaction.Document_Signatures =User_Persistent_Data.Signatures;
            return View(informaction);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool ConfirmProspectus(string password, int pto)
        {
            bool _result = false;
             if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password)
            {
                _result = RepresentativeService.Instancia.ValidatePto(pto, "S");
                if (_result)
                {
                  var mail = new Email().SendEmailCandidateApproval(64,"Candidato ha sido Aprobado", User_Persistent_Data.ProspectusName);
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
                _result = RepresentativeService.Instancia.ValidatePto(pto, "N");
            }

            return _result;
        }





    }
}
