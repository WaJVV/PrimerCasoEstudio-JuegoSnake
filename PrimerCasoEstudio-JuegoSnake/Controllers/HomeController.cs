using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerCasoEstudio_JuegoSnake.Models;
using System.Diagnostics;

namespace PrimerCasoEstudio_JuegoSnake.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User loginUser)
        {
           
                using (var context = new DemoContext())
                {
                    var user = await context.Users.SingleOrDefaultAsync(u => u.user == loginUser.user);
                    if (user != null && loginUser.password == user.password)
                    {

                        HttpContext.Session.SetString("user", user.user);
                        HttpContext.Session.Set("photo", user.photo);

                        
                        // Login correcto
                        return RedirectToAction("Main");
                    }

                    ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                }
            
            return View(loginUser);
        }

        public IActionResult Main()
        {
            var userName = HttpContext.Session.GetString("user");
            var userPhoto = HttpContext.Session.Get("photo");

            if (userName != null && userPhoto != null)
            {
                var userModel = new User
                {
                    user = userName,
                    photo = userPhoto
                };

                return View(userModel);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitForm(User usuario, IFormFile photo)
        {
            if (usuario != null)
            {
                if (photo != null && photo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        usuario.photo = memoryStream.ToArray();
                    }
                }

                using (var context = new DemoContext())
                {
                    context.Add(usuario);
                    await context.SaveChangesAsync();
                    return View("Login");
                }
            }
            return Content("<a>SALIO MAL</a>");
        }
        




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
