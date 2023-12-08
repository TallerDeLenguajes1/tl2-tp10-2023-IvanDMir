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
       try
        { 
             if(ModelState.IsValid){
                try {
                        var usuarioLogeado = repo.GetAll().FirstOrDefault(u => u.nombre_De_Usuario == login.Nombre && u.contrasena == login.Contrasena);

                        if (usuarioLogeado == null){
                             _logger.LogWarning("Intento de acceso invalido - Usuario: "+login.Nombre+" Clave ingresada: " + login.Contrasena);
                            return RedirectToRoute(new { controller = "Home", action = "Index" });
                        } 
                         _logger.LogInformation("El usuario: "+login.Nombre+" ingreso correctamente");
                        LoguearUser(usuarioLogeado);
                        
                        return RedirectToRoute(new { controller = "Home", action = "IndexLogueado" });
            }  
            catch(Exception ex){
                    _logger.LogError(ex.ToString());
                    return BadRequest();
                }
             }
             return RedirectToRoute(new { controller = "Home", action = "Index" });
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
     

   private void LoguearUser(Usuario user) {
        HttpContext.Session.SetString("Id", user.id_usuario.ToString());
        HttpContext.Session.SetString("Usuario", user.nombre_De_Usuario);
        HttpContext.Session.SetString("Contraseña", user.contrasena);
        HttpContext.Session.SetString("Rol", Enum.GetName(user.rol));
    } 
    
}