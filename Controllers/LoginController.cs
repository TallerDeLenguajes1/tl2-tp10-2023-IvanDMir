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
    public IActionResult Login(LoginVM  login){ 
        try {
            var UsuarioLogueado = repo.Existe(login.Nombre, login.Contrasena);
            LoguearUsuario(UsuarioLogueado);
            _logger.LogInformation("User " + UsuarioLogueado.nombre_De_Usuario + " logged successfully");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        } catch (Exception e) {
            _logger.LogError(e.ToString());
            _logger.LogWarning(
                "Invalid user loggin attempt - Username: "+ login.Nombre + "/Password: " + login.Contrasena
            );
            return RedirectToAction("Index");
        }
    }
     

   private void LoguearUsuario(Usuario user) {
        HttpContext.Session.SetString("Id", user.id_usuario.ToString());
        HttpContext.Session.SetString("Usuario", user.nombre_De_Usuario);
        HttpContext.Session.SetString("Contraseña", user.contrasena);
        HttpContext.Session.SetString("Rol", Enum.GetName(user.rol));
    } 
    
}