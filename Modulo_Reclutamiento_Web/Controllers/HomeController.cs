using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Modulo_Reclutamiento_Web.Models;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            //if (User_Persistent_Data.menus[0].subMenus !=null)
            //{
            //    ViewBag.allowed = true;
            //}
            //else 
            //{
            //    ViewBag.allowed = false;
            //}
            return View();
        }

       
    }
}