using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa usługi")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Cena (PLN)")]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Display(Name = "Czas trwania (minuty)")]
        [Range(0, 240)]
        public int DurationMinutes { get; set; }

        [Display(Name = "Zdjęcie (URL lub ścieżka)")]
        public string? PhotoUrl { get; set; }
    }
}
