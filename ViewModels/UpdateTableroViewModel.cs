using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UBViewModel {
    public int Id { get; set; }
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre de Tablero")] 
    public string Nombre { get; set; }
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion de tablero")] 
    public string Descripcion { get; set; }
    public int IdUsuarioPropietario{get;set;}
    public UBViewModel(){

    }
    public UBViewModel(Tablero tablero) {
        Id = tablero.IdTablero;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
    }
}