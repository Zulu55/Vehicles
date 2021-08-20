using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes introducir un email válido.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener una longitud mínima de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    }
}
