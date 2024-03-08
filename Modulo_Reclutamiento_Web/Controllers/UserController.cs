using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modulo_Reclutamiento_Web.Models;
using Modulo_Reclutamiento_Web.Service;
using System.Security.Claims;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        private List<SelectListItem> listConexion = new List<SelectListItem>
            {
                new SelectListItem {Text = "MONTERREY", Value = "1"},
                new SelectListItem {Text = "SALTILLO", Value = "2"}
                //new SelectListItem {Text = "PRUEBAS-MTY", Value = "3"},
                //new SelectListItem {Text = "PRUEBAS-SALT", Value = "4"},
                //new SelectListItem {Text = "QA-MTY", Value = "5"}
            };

        [HttpGet]
        public IActionResult Login()
        {

            ViewBag.DrpDwnLisBranch = listConexion;
            return View();
        }
     
        [HttpPost]
        public async Task<IActionResult> Login(User _user)
        {
           
            User_Persistent_Data.Connection = _user.user_Branch;
          
            var user = UserService.Instancia.AuthenticateUser(_user);

            if (user.IsExist && user.Error == "NA")
            {
                var claims = new List<Claim>()
                             {
                               new Claim(ClaimTypes.Name, User_Persistent_Data.Name) ,
                               new Claim(ClaimTypes.Role, "User"),
                              
                             };
                /* Actualmente se manejan roles (User) para todos los usuarios y se tiene la siguiente anotacion siguiente para reestringir 
                 * el acceso a las vistas
                 *  [Authorize(Roles = "User")]
                 *  En caso de agregar politicas se tendra que agregar el Claim ó Claims correspondientes
                 *  Ejemplo
                 *      var claims = new List<Claim>()
                 *           {
                 *              new Claim("Restart","Y")
                 *            };
                 *  para que se cumpla la polica que queremos que se cumpla.
                 *  agregar funcion para que se haga dinamicamante.
                 *  agregar la anotacion siguiente en las funciones del contralador que queremos restringuir segun la/s politicas que tengamos
                 *  [Authorize(Roles = "User", Policy = "RestarProcess")]
                 *  En case de dudas revisar : https://www.youtube.com/watch?v=GbNhnksUS0k para orientacion de la implementacion
                 */


                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties();
                
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties).Wait();
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                ViewBag.DrpDwnLisBranch = listConexion;
                ViewBag.ERR = user.Error;
                return View();
            }
           
          
        }


        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("~/User/Login");
        }



    }
}
