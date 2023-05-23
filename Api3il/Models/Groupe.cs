using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api3il.Models
{
    public class Groupe
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }
        
    }
}
