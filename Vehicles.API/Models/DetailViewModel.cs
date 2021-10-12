using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Models
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Precio Mano de Obra")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int LaborPrice { get; set; }

        [Display(Name = "Precio Repuestos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int SparePartsPrice { get; set; }

        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public int HistoryId { get; set; }

        [Display(Name = "Procedimiento")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un procedimiento.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProcedureId { get; set; }

        public IEnumerable<SelectListItem> Procedures { get; set; }
    }
}
