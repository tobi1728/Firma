using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Intranet.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Instruktor")]
        public int InstructorId { get; set; }

        [ForeignKey("InstructorId")]
        public Instructor? Instructor { get; set; }

        [Required]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Opis (opcjonalny)")]
        public string? Description { get; set; }
    }
}
