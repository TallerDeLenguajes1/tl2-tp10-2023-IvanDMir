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
    public IActionResult Index( string error = null)

    {
         var usuario = new LoginVM() {
                Error = error
            };
        return View(usuario);
    }

     [HttpPost]
    public IActionResult Login(LoginVM  login){ 
        try {
            var UsuarioLogueado = repo.Existe(login.Nombre, login.Contrasena);
            LoguearUsuario(UsuarioLogueado);
            _logger.LogInformation("Usuario " + UsuarioLogueado.nombre_De_Usuario + " Logueado correctamente");
            return RedirectToRoute(new { controller = "Usuario", action = "Index" });
        } catch (Exception e) {
            login.Error = "Usuario o Contraseña Incorrectos";
            _logger.LogError(e.ToString());
            _logger.LogWarning(
                "Intento de loguear Invalido - usuario: "+ login.Nombre + "/Contraseña: " + login.Contrasena
            );
            return View("Index",login);
        }
    }
      [HttpGet]
    public IActionResult Desloguear() {
        try {
             if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
             var idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            var usuarioActual = repo.GetById(idUsuario);
            DesloguearUsuario();
            _logger.LogInformation("usuario " + usuarioActual.nombre_De_Usuario + " Deslogueado");
            return RedirectToAction("Index");
        } catch (Exception e) {
            _logger.LogError(e.ToString());
            _logger.LogWarning("No se pudo desloguear");
            return RedirectToRoute(new { controller = "Login", action = "Index"});
        }
    }
 
     

   private void LoguearUsuario(Usuario user) {
        HttpContext.Session.SetString("Id", user.id_usuario.ToString());
        HttpContext.Session.SetString("Usuario", user.nombre_De_Usuario);
        HttpContext.Session.SetString("Contraseña", user.contrasena);
        HttpContext.Session.SetString("Rol", Enum.GetName(user.rol));
    } 
     private void DesloguearUsuario() {
        HttpContext.Session.SetString("Id", string.Empty);
        HttpContext.Session.SetString("Usuario", string.Empty);
        HttpContext.Session.SetString("Contraseña", string.Empty);
        HttpContext.Session.SetString("Rol", string.Empty);
    }  
          private bool isLogin()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Rol"))){
            return false;
        }else{
            return true;
        }
    }
   
}
    
