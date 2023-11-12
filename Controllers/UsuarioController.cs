using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;

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
        List<Usuario> usuarios = repo.GetAll();
        return View(usuarios);
    
    }
   [HttpGet]
    public IActionResult Add(){
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult Add(Usuario user){
        repo.Crear(user);
         return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id){
        return View(repo.GetById(id));
    }
    [HttpPost]
    public IActionResult Update(Usuario user){
        repo.Modificar(user.id_usuario,user);
        return RedirectToAction("Index");
        

    }
   [HttpGet]
   public IActionResult Delete(int id){
        return View(repo.GetById(id));
   }
   
    [HttpPost]
    public IActionResult Delete(Usuario user)
    {   
        repo.eliminar(user.id_usuario);
      return RedirectToAction("Index");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

