using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api3il.Models
{
    public class Promotion
    {
        [Key]
        public int Id { get; set; }
        public string Annee { get; set; }

        
    }
}
