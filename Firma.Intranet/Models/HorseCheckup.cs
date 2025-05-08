// Models/HorseCheckup.cs
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace Firma.Intranet.Models
{
    public class HorseCheckup
    {
        public int Id { get; set; }

        [Required]
        public int HorseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckupDate { get; set; }
        [Required]
        public string Type { get; set; }

        public string? Notes { get; set; }

        [ValidateNever]
        public Horse Horse { get; set; }
    }
}
