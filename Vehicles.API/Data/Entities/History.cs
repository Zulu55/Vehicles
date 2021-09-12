using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace Vehicles.API.Data.Entities
{
    public class History
    {
        public int Id { get; set; }

        [Display(Name = "Vehículo")]
        [JsonIgnore]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Kilometraje")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Mileage { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [JsonIgnore]
        [Display(Name = "Mecánico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public User User { get; set; }

        public ICollection<Detail> Details { get; set; }

        [Display(Name = "# Detalles")]
        public int DetailsCount => Details == null ? 0 : Details.Count;

        [Display(Name = "Total Mano de Obra")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalLabor => Details == null ? 0 : Details.Sum(x => x.LaborPrice);

        [Display(Name = "Total Repuestos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalSpareParts => Details == null ? 0 : Details.Sum(x => x.SparePartsPrice);

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total => Details == null ? 0 : Details.Sum(x => x.TotalPrice);
    }
}
