// Models/Rider.cs
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models
{
    public class Rider
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1, 120)]
        [Display(Name = "Wiek")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Poziom zaawansowania")]
        public string SkillLevel { get; set; }

        [Required]
        [Display(Name = "Waga (kg)")]
        [Range(20, 200)]
        public int Weight { get; set; }

        [Display(Name = "Zdjęcie (opcjonalne)")]
        public string? PhotoUrl { get; set; }
    }
}
