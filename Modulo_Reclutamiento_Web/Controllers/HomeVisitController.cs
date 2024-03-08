using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Models.HomeVisitData;
using Modulo_Reclutamiento_Web.Service;
using Newtonsoft.Json;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class HomeVisitController : Controller
    {
        private List<SelectListItem> _homeVisitOp = new List<SelectListItem>() { new SelectListItem { Text = "PROSPECTOS", Value = "1" }, new SelectListItem { Text = "EMPLEADOS", Value = "2" } };

       
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Index()
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
        public IActionResult Index(int homevisitop, int? departop, int? positop, string? Fecha_Inicio, string? Fecha_Fin)
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
        public IActionResult ListOfVisits(int pto, int filter, string? stat)
        {
            //regresar a la vista id de prospecto, filtro y status
            ViewBag.pto = pto;
            ViewBag.Filter = filter;
            ViewBag.Stat = stat;
            //lista de visitas domiciliadas hechas al prospecto ó empleado
            var list = HomeVisitServices.Instancia.GetVisits(pto, filter);
            //retorno lista en la vista
            return View(list);
        }

        [Authorize(Roles = "User")]
        public IActionResult Research(int pto,int idvisit, int filter,string stat,int? MSG)
        {
            ViewBag.pto = pto;
            ViewBag.Filter = filter;
            ViewBag.Stat = stat;
            ViewBag.MSG = MSG;
            var homeVisit = HomeVisitServices.Instancia.NewHomeVisit(pto, idvisit,(filter == 1 ? "P" : "E"));
            return View(homeVisit);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult NewVisit(int pto, int filter) 
        {
            ViewBag.pto = pto;
            ViewBag.Filter = filter;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult NewVisit(HomeVisit home)
        {
            var homeVisit = HomeVisitServices.Instancia.AddNewVisit(home);
            if (homeVisit)
            {
                return RedirectToAction("ListOfVisits", new { pto = home.IdProspOrEmpl,filter = (home.ProspOrEmpl=="P"?1:2),stat="R" });
            }
            else 
            {
                ViewBag.ERR = "ERRINS";
                return View();
            }
         
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public String GetNeighborhoodReference(int Id) 
        {
            NeighborhoodReference neighborn = HomeVisitServices.Instancia.getNeighbornhoodRerefences(Id);
            return JsonConvert.SerializeObject(neighborn);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool SaveNeighborhoodReference(NeighborhoodReference neightbornhoodReference)
        {
            bool _save = HomeVisitServices.Instancia.AddNeighbornhoodRerefences(neightbornhoodReference);
            return _save;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool UpdateNeighborhoodReference(NeighborhoodReference neightbornhoodReference)
        {
            bool _update = HomeVisitServices.Instancia.UpdateNeighbornhoodRerefences(neightbornhoodReference);
            return _update;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool DeleteNeighborhoodReference(int id)
        {
            bool _delete = HomeVisitServices.Instancia.DeleteNeighbornhoodRerefences(id);
            return _delete;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult saveUpdIncomeNExpenses(IncomeNExpenses incomeNExpenses) 
        {
           
            int MSG = 0;
            if (incomeNExpenses.ResearchParam.stat == "SV")
            {
                bool _save = HomeVisitServices.Instancia.SaveIncomeNExpenses(incomeNExpenses);
                if (_save)
                {
                    MSG = 1;
                }
            }
            else 
            {
                bool _save = HomeVisitServices.Instancia.UpdateIncomeNExpenses(incomeNExpenses);
                if (_save)
                {
                    MSG = 2;
                }
            }

            return RedirectToAction("Research", new { pto = incomeNExpenses.ResearchParam.prosp, idvisit = incomeNExpenses.ResearchParam.idvisit, filter = incomeNExpenses.ResearchParam.filter, stat = incomeNExpenses.ResearchParam.stat, MSG = MSG });
            
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult saveUpdDescriptionOfTheHouseRoom(DescriptionOfTheHouseRoom houseRoom) 
        {
            
            int MSG = 3;
            if (houseRoom.ResearchParam.stat == "SV")
            {
             bool _save = HomeVisitServices.Instancia.SaveDescriptionOfTheHouseRoom(houseRoom);
                if (_save)
                {
                    MSG = 4;
                }
            }
            else 
            {
                bool _save = HomeVisitServices.Instancia.UpdateDescriptionOfTheHouseRoom(houseRoom);
                if (_save)
                {
                    MSG = 5;
                }
            }

            return RedirectToAction("Research", new { pto = houseRoom.ResearchParam.prosp, idvisit = houseRoom.ResearchParam.idvisit, filter = houseRoom.ResearchParam.filter, stat = "R", MSG = MSG });

        }



    }
}
