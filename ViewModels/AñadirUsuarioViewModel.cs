using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AUViewModel {

 [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre")] 
    public string Nombre { get; set; }
    [StringLength(30, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres.")]
    public string Contrasena { get; set; }
    public Roles Rol { get; set; }

   
    public AUViewModel() {}
}