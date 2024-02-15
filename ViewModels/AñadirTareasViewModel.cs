using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_IvanDMir.Models;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class ATViewModel {
    public List<Tablero> TablerosAUsar {get;set;}
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "IdTablero")] 
    public int IdTablero { get; set; }

     [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre")] 
    public string Nombre { get; set; }
     [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")] 
    public string Descripcion { get; set; }
     [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Color")] 
    public string Color { get; set; }

   public ATViewModel() {
        
    }
    public ATViewModel(List<Tablero> tableros) {
        TablerosAUsar = tableros;
    }

}