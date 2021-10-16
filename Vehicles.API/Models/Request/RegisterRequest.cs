using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class RegisterRequest : UserRequest
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(6, ErrorMessage = "El campo {0} debe tener una longitud mínima de {1} carácteres.")]
        public string Password { get; set; }
    }
}
