using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UTViewModel {
    public int Id { get; set; }
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre de Usuario")] 
    public string Nombre { get; set; }
    public Estados Estado { get; set; }
    public string Descripcion { get; set; }
    public string Color { get; set; }
    public int IdTablero {get;set;}

    public bool  Dueño {get;set;}

    public string rol {get;set;}

    public UTViewModel(){

    }
    public UTViewModel(Tarea tarea,bool dueño,string Rol) {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        IdTablero = tarea.IdTablero;
        Dueño = dueño;
        rol = Rol;
    }
}