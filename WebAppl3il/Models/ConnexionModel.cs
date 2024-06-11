using System.ComponentModel.DataAnnotations;

namespace WebAppl3il.Models
{
    public class ConnexionModel
    {
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [EmailAddress(ErrorMessage = "Veuillez entrer une adresse email valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}



