using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using  tl2_tp10_2023_IvanDMir.ViewModels;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    UsuarioRepositorio repo;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repo = new UsuarioRepositorio();
    }
      public IActionResult Index()
    {
       if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
         return View(new LUViewModel(repo.GetAll()));
    
    }
   [HttpGet]
    public IActionResult Add(){
        if(!esAdmin()) return RedirectToAction("Index");
        return View(new AUViewModel());
    }
    [HttpPost]
    public IActionResult Add(AUViewModel user) {
        if(!esAdmin()) return RedirectToAction("Index");
        var Nuevo = new Usuario() {
            nombre_De_Usuario = user.Nombre,
            contrasena = user.Contrasena,
            rol = user.Rol
        };
        repo.Crear(Nuevo);
        return RedirectToAction("Index");
    }

  [HttpGet]
    public IActionResult Update(int id) {
        if(!esAdmin()) return RedirectToAction("Index");
        return View(new UUViewModel(repo.GetById(id)));
    }
 [HttpPost]
    public IActionResult Update(UUViewModel usuarioNuevo) {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var usuario = repo.GetAll().FirstOrDefault(u => u.id_usuario == usuarioNuevo.Id);
        repo.Modificar(usuario.id_usuario, usuario);
        return RedirectToAction("Index");
    }
   [HttpGet]
    public IActionResult Delete(int id) {
        if(!esAdmin()) return RedirectToAction("Index");
        if(!ModelState.IsValid) return RedirectToAction("Index");
        repo.eliminar(id);
        return RedirectToAction("Index");
    }

     private bool isLogin()
    {
        if (HttpContext.Session != null ){
            return true;
        }else{
            return false;
        }
    }

    private bool esAdmin(){
         if (HttpContext.Session.GetString("Rol") == Enum.GetName(Roles.admin)){
            return true;
         }
         var colo = HttpContext.Session.GetString("Rol");
         var malo =  Enum.GetName(Roles.admin);
         return false;
        
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


