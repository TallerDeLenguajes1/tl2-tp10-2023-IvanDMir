using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using  tl2_tp10_2023_IvanDMir.ViewModels;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    IUsuarioRepositorio repo;

    public UsuarioController(ILogger<UsuarioController> logger,IUsuarioRepositorio repositorio)
    {
        _logger = logger;
        repo = repositorio;
    }
      public IActionResult Index()
    {
        try{ 
       if(!isLogin() || !esAdmin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
         return View(new LUViewModel(repo.GetAll()));
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
   [HttpGet]
    public IActionResult Add(){
        try{ 
        if(!esAdmin()) return RedirectToAction("Index");
        return View(new AUViewModel());
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult Add(AUViewModel user) {
        try{ 
        if(!esAdmin()) return RedirectToAction("Index");
        var Nuevo = new Usuario() {
            nombre_De_Usuario = user.Nombre,
            contrasena = user.Contrasena,
            rol = user.Rol
        };
        repo.Crear(Nuevo);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

  [HttpGet]
    public IActionResult Update(int id) {
        try { 
        if(!esAdmin()) return RedirectToAction("Index");
        return View(new UUViewModel(repo.GetById(id)));
    }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
 [HttpPost]
    public IActionResult Update(UUViewModel usuarioNuevo) {
        try { 
        if(!ModelState.IsValid) return RedirectToAction("Index");
         var Nuevo = new Usuario() {
            nombre_De_Usuario = usuarioNuevo.Nombre,
            contrasena = usuarioNuevo.Contrasena,
            rol = usuarioNuevo.Rol,
        };
        
        repo.Modificar(usuarioNuevo.Id, Nuevo);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
   [HttpGet]
    public IActionResult Delete(int id) {
        try { 
        if(!esAdmin()) return RedirectToAction("Index");
        if(!ModelState.IsValid) return RedirectToAction("Index");
        repo.eliminar(id);
        return RedirectToAction("Index");
    }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
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


