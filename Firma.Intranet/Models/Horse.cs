using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Data ostatniego przeglądu")]
        [DataType(DataType.Date)]
        public DateTime? LastCheckup { get; set; }
    }
}
