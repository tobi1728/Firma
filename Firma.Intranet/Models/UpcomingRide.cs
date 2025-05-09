using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models
{
    public class UpcomingRide
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data i godzina jazdy")]
        public DateTime RideDate { get; set; }

        [Required]
        [Display(Name = "Instruktor")]
        public int InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor? Instructor { get; set; }

        [Required]
        [Display(Name = "Jeździec")]
        public int RiderId { get; set; }

        [ForeignKey("RiderId")]
        public Rider? Rider { get; set; }

        [Required]
        [Display(Name = "Koń")]
        public int HorseId { get; set; }

        [ForeignKey("HorseId")]
        public Horse? Horse { get; set; }

        [Display(Name = "Komentarz (opcjonalny)")]
        public string? Notes { get; set; }

        [Display(Name = "Potwierdzona")]
        public bool IsConfirmed { get; set; }
    }
}
