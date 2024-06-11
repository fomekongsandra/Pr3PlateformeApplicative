
using System;

namespace AppMobile
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public LoginPage()
        {
            InitializeComponent();
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Pointeuse;integrated security=True;TrustServerCertificate=True;";
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string nom = NomEntry.Text;
            string prenom = PrenomEntry.Text;

            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom et le prénom.", "OK");
                return;
            }

            var student = await _databaseService.GetStudentByNameAsync(nom, prenom);

            if (student == null)
            {
                await DisplayAlert("Erreur", "Étudiant non trouvé.", "OK");
                return;
            }

            await Navigation.PushAsync(new QrCodePage(student));
        }
    }
}
