using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace ApplicationPointeuse
{
    
    public partial class Connexion : Form
    {
        

        public Connexion()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost\\SQLEXPRESS;Initial Catalog=Pointeuse;integrated security=True;TrustServerCertificate=True");
        private async void guna2Button1_Click(object sender, EventArgs e)
        {
           

            String login, password;

            login = "ruchaud@3il.fr";
            password = "12345@";
            // Vérifier si les champs de login et de mot de passe sont vides
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                // Afficher un message d'erreur et empêcher la navigation vers la page du menu
                MessageBox.Show("Veuillez saisir un nom d'utilisateur et un mot de passe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Les champs sont remplis, vous pouvez passer à la page du menu
                Menu menu = new Menu();
                menu.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
