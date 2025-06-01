using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Lata doświadczenia")]
        [Range(0, 60)]
        public int YearsOfExperience { get; set; }

        [Display(Name = "Pensja (PLN)")]
        [Range(0, 99999)]
        public decimal? Salary { get; set; }

        [Display(Name = "Zdjęcie (URL lub ścieżka)")]
        public string? PhotoUrl { get; set; }

        [Display(Name = "Typ jazdy")]
        public string? RidingType { get; set; }
    }
}
