using tl2_tp10_2023_IvanDMir.Models;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UUViewModel {
    public int Id { get; set; }
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre de Usuario")] 
    public string Nombre { get; set; }
     [StringLength(30, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 30 caracteres.")]
    public string Contrasena { get; set; }
    public Roles Rol {get;set;}


    public UUViewModel(){

    }
    public UUViewModel(Usuario user) {
        Id = user.id_usuario;
        Nombre = user.nombre_De_Usuario;
        Contrasena = user.contrasena;
        Rol = user.rol;
    }
}