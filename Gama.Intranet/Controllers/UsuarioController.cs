using Microsoft.AspNetCore.Mvc;

namespace Gama.Intranet.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Ingresar()
        {
            return View();
        }
        public IActionResult CambiarContraseña()
        {
            return View();
        }
    }
}
