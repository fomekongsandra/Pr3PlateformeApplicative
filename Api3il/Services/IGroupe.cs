using Api3il.Models;

namespace Api3il.Services
{
    public interface IGroupe
    {
        public Groupe GetGroupeById(int id);
        public List<Groupe> GetGroupes();
        public Groupe CreateGoupe(Groupe groupe);
        public Groupe UpdateGroupe(int id, Groupe groupe);
        public void DeleteGroupe(int id);
    }
}
