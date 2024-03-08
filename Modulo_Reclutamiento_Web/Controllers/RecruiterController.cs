using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.GeneralData;
using Modulo_Reclutamiento_Web.Models.HomeVisitData;
using Modulo_Reclutamiento_Web.Service;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class RecruiterController : Controller
    {
        private List<SelectListItem> _homeVisitOp = new List<SelectListItem>() { new SelectListItem { Text = "PROSPECTOS", Value = "1" }, new SelectListItem { Text = "EMPLEADOS", Value = "2" } };

        #region Views
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Index()
        {
            DateTime Fecha_Inicio = DateTime.Today.AddDays(-14);
            DateTime Fecha_Fin = DateTime.Today;
            var ListaProspectos = RecruiterService.Instancia.get_Prospectus(Convert.ToDateTime(Fecha_Inicio.ToString("yyyy-MM-dd")), Convert.ToDateTime(Fecha_Fin.ToString("yyyy-MM-dd")));
            return View(ListaProspectos);

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Index(string Fecha_Inicio, string Fecha_Fin)
        {
            var ListaProspectos = RecruiterService.Instancia.get_Prospectus(Convert.ToDateTime(Fecha_Inicio), Convert.ToDateTime(Fecha_Fin));
            return View(ListaProspectos);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult NewProspectus()
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData();

            return View(prospectus);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult NewProspectus(ProspectusData generalData)
        {
            var data = RecruiterService.Instancia.ValidationData(generalData);
            var saved = RecruiterService.Instancia.SaveData(generalData);
            if (saved)
            {
                ViewBag.MSG = 1;
            }
            else
            {
                ViewBag.MSG = 0;
            }
            return View(data);
        }

        [Authorize(Roles = "User")]
       
        public IActionResult Message()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult MedicalQuestionnarie() 
        {
            ViewBag.VisitOp = _homeVisitOp;
            var Departs = RecruiterService.Instancia.getDepartments();
            Departs.Insert(0, new SelectListItem { Text = "Seleccione un Departamento", Value = "0" });
            ViewBag.Departs = Departs;
            var position = RecruiterService.Instancia.getPositions(Convert.ToInt32(Departs.First().Value));
            position.Insert(0, new SelectListItem { Text = "Seleccione un Puesto", Value = "0" });
            ViewBag.Positions = position;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult MedicalQuestionnarie(int homevisitop, int? departop, int? positop, string Fecha_Inicio, string Fecha_Fin)
        {
            var Departs = RecruiterService.Instancia.getDepartments();
            Departs.Insert(0, new SelectListItem { Text = "Seleccione un Departamento", Value = "0" });
            var positions = RecruiterService.Instancia.getPositions((int)departop);
            positions.Insert(0, new SelectListItem { Text = "Seleccione un Puesto", Value = "0" });
            SelectListItem selectitemdepart = new SelectListItem();
            SelectListItem selectitemp = new SelectListItem();

            List<Prospectus> resultado = null;
            var typeList = "N";
            if (homevisitop == 1)//1 prospectos || 2 empleados
            {
                ViewBag.VisitOp = _homeVisitOp.OrderBy(x => x.Value);
                resultado = RecruiterService.Instancia.get_Prospectus(Convert.ToDateTime(Fecha_Inicio), Convert.ToDateTime(Fecha_Fin));
                typeList = "P";
            }
            else
            {
                //orden con empreados al principio
                ViewBag.VisitOp = _homeVisitOp.OrderByDescending(x => x.Value);
                //recorremos los departamentos encontramos el enviado, se elimina y despues 
                //se agrega al principio
                if ((int)departop != 0)
                {
                    foreach (var item in Departs)
                    {
                        if (item.Value == departop.ToString())
                        {

                            selectitemdepart.Text = item.Text;
                            selectitemdepart.Value = item.Value;
                            Departs.Remove(item);

                            break;
                        }
                    }
                    Departs.Insert(0, selectitemdepart);
                }

                //recorremos los puestos encontramos el enviado, se elimina y despues 
                //se agrega al principio
                if ((int)positop != 0)
                {
                    foreach (var itemp in positions)
                    {
                        if (itemp.Value == positop.ToString())
                        {

                            selectitemp.Text = itemp.Text;
                            selectitemp.Value = itemp.Value;
                            positions.Remove(itemp);
                            break;
                        }
                    }
                    positions.Insert(0, selectitemp);
                }

                // se regreson los empleados del departamento y puesto enviado
                resultado = HomeVisitServices.Instancia.get_Employees((int)departop, (int)positop);
                typeList = "E";
            }

            ViewBag.TypeList = typeList;
            ViewBag.Departs = Departs;
            ViewBag.Positions = positions;

            return View(resultado);
        }
        [Authorize(Roles = "User")]
        public IActionResult ListOfMedicalQuestionnarie(int pto,int filter)
        { //regresar a la vista id de prospecto, filtro y status
            ViewBag.pto = pto;
            ViewBag.Filter = filter;
            ViewBag.TypeList = (filter==1?"P":"E");
            //ViewBag.Stat = stat;
            //lista de visitas domiciliadas hechas al prospecto ó empleado
            var list = RecruiterService.Instancia.GetCuestionaries(pto, filter);
            //retorno lista en la vista
            return View(list);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool AddCuestionarie(int Id,int filter) 
        {
            bool _Add = RecruiterService.Instancia.AddCuestionarie(Id,filter);
            return _Add;
        }

        #endregion

        #region PartialView selects
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectPosition(int department)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(1, null, null, department);
            return PartialView("../Recruiter/partialViews/selectViews/_selectPositions", prospectus);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectPositionHomeVisit(int department)
        {
            var positions = RecruiterService.Instancia.getPositions(department);
            positions.Insert(0, new SelectListItem { Text = "Seleccione un Puesto", Value = "0" });
            return PartialView("../Recruiter/partialViews/selectViews/_selectPositionHomeVisit", positions);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectState(int country)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(country);
            return PartialView("../Recruiter/partialViews/selectViews/_selectState", prospectus);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectCity(int country, int state)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(country, state);
            return PartialView("../Recruiter/partialViews/selectViews/_selectCity", prospectus);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectColonies(int city)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(1, null, city);
            return PartialView("../Recruiter/partialViews/selectViews/_selectColony", prospectus);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectZones(int city)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(1, null, city);
            return PartialView("../Recruiter/partialViews/selectViews/_selectZones", prospectus);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult selectContactD(int contactMode)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(1, null, null, null, contactMode);
            return PartialView("../Recruiter/partialViews/selectViews/_selectContacModeD", prospectus);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult updateListFamily([FromBody] FamilyData family)
        {
            var prospectus = RecruiterService.Instancia.NewProspectusData(1, null, null, null, null, family);
            return PartialView("../Recruiter/partialViews/selectViews/_listFamily", prospectus);
        }

        #endregion


        [Authorize(Roles = "User")]
        [HttpPost]
        public bool Confirm_Reset(string password, int pto)
        {
            bool resp = false;
            if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password)
            {
                resp = RecruiterService.Instancia.Reset_Prospectus(pto);
            }

            return resp;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool Confirm_Validate(string password, int pto)
        {
            bool resp = false;
            if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password)
            {
                 resp = RecruiterService.Instancia.Validate_Prospectus(pto);
                if (resp)
                {
                    resp = true;
                }
            }

            return resp;
        }

    }
}
