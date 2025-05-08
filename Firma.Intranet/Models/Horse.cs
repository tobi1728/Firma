using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models
{
    public class Horse
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię konia")]
        public string Name { get; set; }

        [Display(Name = "Rasa")]
        public string Breed { get; set; }

        [Display(Name = "Wiek")]
        public int Age { get; set; }

        [Column("WeightKg")]
        [Display(Name = "Waga konia (kg)")]
        [Range(100, 1000)]
        public int? WeightKg { get; set; }

        [Column("HeightCm")]
        [Display(Name = "Wzrost konia (cm)")]
        [Range(100, 250)]
        public int? HeightCm { get; set; }

        [Column("MaxRiderLevel")]
        [Display(Name = "Maks. poziom jeźdźca")]
        public string? MaxRiderLevel { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Data ostatniego przeglądu")]
        [DataType(DataType.Date)]
        public DateTime? LastCheckup { get; set; }

        [Display(Name = "Zdjęcie (URL lub ścieżka)")]
        public string? PhotoUrl { get; set; }


    }
}
