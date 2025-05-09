using System;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa przedmiotu")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Kwota")]
        [Range(0.01, 10000)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Data zamówienia")]
        public DateTime OrderDate { get; set; }
    }
}
