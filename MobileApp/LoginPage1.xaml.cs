using System.Data.SqlClient;

namespace MobileApp;

public partial class LoginPage1 : ContentPage
{
	public LoginPage1()
	{
		InitializeComponent();
	}
    private void OnConnectClicked(object sender, EventArgs e)
    {
        // Récupérez l'ID saisi par l'utilisateur
        string studentId = etudiantIdEntry.Text;

        // Vérifiez si l'ID appartient à un étudiant dans la base de données
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Pointeuse;Integrated Security=True;TrustServerCertificate=True";
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = "SELECT * FROM etudiant WHERE id = @Id";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", studentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // L'ID correspond à un étudiant, récupérez les informations et l'heure/la date actuelle
                    string studentName = reader["nom"].ToString();
                    string studentFirstName = reader["prenom"].ToString();
                    string studentGroup = reader["groupe"].ToString();
                    string studentPromotion = reader["promotion"].ToString();
                    DateTime currentDateTime = DateTime.Now;

                    // Stockez ces informations dans la variable "scan" (que vous devrez définir)
                    ScanData scan = new ScanData
                    {
                        StudentName = studentName,
                        StudentFirstName = studentFirstName,
                        StudentGroup = studentGroup,
                        StudentPromotion = studentPromotion,
                        ScanDateTime = currentDateTime
                    };

                    // Naviguez vers la page de génération de QR Code et passez les informations
                    Navigation.PushAsync(new GenerateQRCodePage(scan));
                }
                else
                {
                    // L'ID n'appartient pas à un étudiant, affichez un message d'erreur
                    DisplayAlert("Erreur", "ID invalide", "OK");
                }
            }
        }
    }
}