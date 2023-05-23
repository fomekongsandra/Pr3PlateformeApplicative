using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api3il.Models
{
    public class Emargement
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateEmargement { get; set; }
        public enum Statuts { present, absent }
        [ForeignKey("Etudiant")]
        public int EtudiantId { get; set; }
        
    }
}
