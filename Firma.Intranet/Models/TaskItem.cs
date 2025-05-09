using System;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Opis zadania")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Do zrobienia";

        [Display(Name = "Data utworzenia")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
