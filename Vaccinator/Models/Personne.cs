using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vaccinator.Models
{

    public enum Sexe
    {
        Homme,
        Femme
    }

    public enum Statut
    {
        Résident,
        Personnel
    }

    public class Personne
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required]
        [EnumDataType(typeof(Sexe))]
        public Sexe Sexe { get; set; }

        [Required]
        [Display(Name = "Date de naissance")]
        public DateTime DateNaissance { get; set; }

        [Required]
        [EnumDataType(typeof(Statut))]
        public Statut Statut { get; set; }

        public virtual ICollection<Injection> Injections { get; set; }
    }
}
