using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api3il.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
         : base(options)
        {
        }

        public DbSet<Promotion> promotion { get; set; } = null!;
        public DbSet<Etudiant> etudiant { get; set; } = null!;
        public DbSet<Groupe> groupe { get; set; } = null!;
        public DbSet<Emargement> emargement { get; set; } = null!;
        public DbSet<Cordonateur> cordonateur { get; set; } = null!;
        public DbSet<UserAdministration> userAdministration { get; set; } = null!;

        //internal Task SaveChangesAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
