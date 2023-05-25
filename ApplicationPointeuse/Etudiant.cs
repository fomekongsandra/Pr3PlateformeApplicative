using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPointeuse
{
    public class Etudiant
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public int  GroupeId { get; set;}
        public int PromotionId { get; set; }
        public Etudiant() { }
    }
}
