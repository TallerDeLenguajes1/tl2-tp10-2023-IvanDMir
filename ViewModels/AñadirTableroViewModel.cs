
using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class ABViewModel {
    
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre de Usuario")] 
    public string Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion del tablero")] 
    public string Descripcion { get; set; }
     [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id usuario Propietario")] 
    public int IdUsuarioPropietario { get; set; }
    public string Error{get;set;}

    public ABViewModel() {}
    public ABViewModel(int IdPropietario){
        IdUsuarioPropietario = IdPropietario;

    }
}
