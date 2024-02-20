using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AUViewModel {

 [Required(ErrorMessage = "Elija un usuario valido")]
    [Display(Name = "Nombre")] 
    public string Nombre { get; set; }
    [Required(ErrorMessage = "Elija una contraseña valido.")]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres.")]
    public string Contrasena { get; set; }

     [Required(ErrorMessage = "Elija un rol valido.")]
    public Roles Rol { get; set; }
    public String Error { get; set; }

   
    public AUViewModel() {}
}