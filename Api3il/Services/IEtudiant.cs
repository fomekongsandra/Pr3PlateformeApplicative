using Api3il.Models;

namespace Api3il.Services
{
    public interface IEtudiant
    {
        public Etudiant GetEtudiantById(int id);
        public Etudiant CreateEtudiant(Etudiant e);
        public Etudiant UpdateEtudiant(int id, Etudiant e);
        public void DeleteEtudiant(int id);
    }
}
