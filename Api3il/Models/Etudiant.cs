using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api3il.Models
{
    public class Etudiant
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [ForeignKey("Groupe")]
        public int GroupeId { get; set; }
        

        [ForeignKey("Promotion")]

        public int PromotionId { get; set; }
       

    }
}
