using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models.Request
{
    public class HistoryRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Remarks { get; set; }
    }
}
