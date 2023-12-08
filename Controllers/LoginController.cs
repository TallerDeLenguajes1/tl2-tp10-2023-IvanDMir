using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.ViewModels;
using tl2_tp10_2023_IvanDMir.repositorios;
namespace tl2_tp10_2023_IvanDMir.Controllers;

public class LoginController : Controller
{
   IUsuarioRepositorio repo;

    private readonly ILogger<LoginController> _logger;

 public LoginController(ILogger<LoginController> logger,IUsuarioRepositorio usuarioRepositorio)
    {
        _logger = logger;
    repo = usuarioRepositorio;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginVM());
    }

     [HttpPost]
    public IActionResult Login(LoginVM  login)
    {
        var usuarioLogeado = repo.GetAll().FirstOrDefault(u => u.nombre_De_Usuario == login.Nombre && u.contrasena == login.Contrasena);

        if (usuarioLogeado == null){
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        } 
        
        LoguearUser(usuarioLogeado);
        
        return RedirectToRoute(new { controller = "Home", action = "IndexLogueado" });
    }

   private void LoguearUser(Usuario user) {
        HttpContext.Session.SetString("Id", user.id_usuario.ToString());
        HttpContext.Session.SetString("Usuario", user.nombre_De_Usuario);
        HttpContext.Session.SetString("Contraseña", user.contrasena);
        HttpContext.Session.SetString("Rol", Enum.GetName(user.rol));
    } 
    
}