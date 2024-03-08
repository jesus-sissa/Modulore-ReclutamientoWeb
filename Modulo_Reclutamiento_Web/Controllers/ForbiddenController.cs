using Microsoft.AspNetCore.Mvc;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class ForbiddenController : Controller
    {
        
        public IActionResult Illegal()
        {
            return View();
        }
    }
}
