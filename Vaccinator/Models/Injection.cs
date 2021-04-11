using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vaccinator.Models
{
    public class Injection
    {
        public int Id { get; set; }

        [Required]
        public virtual Vaccin Vaccin { get; set; }

        [Required]
        [MaxLength(50)]
        public string Marque { get; set; }

        [Required]
        public int Lot { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Date de rappel")]
        public DateTime DateRappel { get; set; }

        [Required]
        public virtual Personne Personne { get; set; }
    }
}
