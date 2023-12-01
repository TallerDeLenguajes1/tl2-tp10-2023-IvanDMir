using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class ATViewModel {
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

    public ATViewModel() {}

}