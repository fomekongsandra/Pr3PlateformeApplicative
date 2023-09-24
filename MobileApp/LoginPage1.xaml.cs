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
        // R�cup�rez l'ID saisi par l'utilisateur
        string studentId = etudiantIdEntry.Text;

        // V�rifiez si l'ID appartient � un �tudiant dans la base de donn�es
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
                    // L'ID correspond � un �tudiant, r�cup�rez les informations et l'heure/la date actuelle
                    string studentName = reader["nom"].ToString();
                    string studentFirstName = reader["prenom"].ToString();
                    string studentGroup = reader["groupe"].ToString();
                    string studentPromotion = reader["promotion"].ToString();
                    DateTime currentDateTime = DateTime.Now;

                    // Stockez ces informations dans la variable "scan" (que vous devrez d�finir)
                    ScanData scan = new ScanData
                    {
                        StudentName = studentName,
                        StudentFirstName = studentFirstName,
                        StudentGroup = studentGroup,
                        StudentPromotion = studentPromotion,
                        ScanDateTime = currentDateTime
                    };

                    // Naviguez vers la page de g�n�ration de QR Code et passez les informations
                    Navigation.PushAsync(new GenerateQRCodePage(scan));
                }
                else
                {
                    // L'ID n'appartient pas � un �tudiant, affichez un message d'erreur
                    DisplayAlert("Erreur", "ID invalide", "OK");
                }
            }
        }
    }
}