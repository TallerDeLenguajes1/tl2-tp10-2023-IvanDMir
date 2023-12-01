using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AUViewModel {
    public string Nombre { get; set; }
    [StringLength(30, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)+$",
     ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un numero.")]
    public string Contrasena { get; set; }
    public Roles Rol { get; set; }

   
    public AUViewModel() {}
}