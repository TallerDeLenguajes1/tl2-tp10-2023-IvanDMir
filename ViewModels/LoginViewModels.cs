using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_IvanDMir.ViewModels;

    public class LoginVM
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Contrasena {get;set;}
    }