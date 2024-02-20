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
       if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
       if(!esAdmin()) return RedirectToRoute(new {controller = "Tarea", action = "Index"});
             return View(new LUViewModel(repo.GetAll()));
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
   [HttpGet]
    public IActionResult Add(string error= null){
        try{ 
        if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
        if(!esAdmin()) return RedirectToAction("Index");
        var usuario = new AUViewModel() {
            Error = error
        };
        return View(usuario);
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
    [HttpPost]
    public IActionResult Add(AUViewModel usuario) {
        try{ 
        if(!esAdmin()) return RedirectToAction("Index");
        if(!ModelState.IsValid) return View(usuario);
        var Nuevo = new Usuario() {
            nombre_De_Usuario = usuario.Nombre,
            contrasena = usuario.Contrasena,
            rol = usuario.Rol
        };
        if (repo.YaExiste(Nuevo)){
            usuario.Error = "El usuario Ya existe, elija otro";
            return RedirectToAction("Add", usuario);
        }
        repo.Crear(Nuevo);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return RedirectToAction("Add");
        };
    }

  [HttpGet]
    public IActionResult Update(int id) {
        try { 
         if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
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
        if(!ModelState.IsValid) return View(usuarioNuevo);
         var Nuevo = new Usuario() {
            id_usuario = usuarioNuevo.Id,
            nombre_De_Usuario = usuarioNuevo.Nombre,
            contrasena = usuarioNuevo.Contrasena,
            rol = usuarioNuevo.Rol,
        };
    
        repo.Modificar(Nuevo.id_usuario, Nuevo);
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
         return false;
        
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


