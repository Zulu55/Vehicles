using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class HistoryViewModel
    {
        public int VehicleId { get; set; }

        [Display(Name = "Kilometraje")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Mileage { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Remarks { get; set; }
    }
}
