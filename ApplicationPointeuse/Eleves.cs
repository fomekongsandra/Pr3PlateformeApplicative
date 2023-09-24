using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ApplicationPointeuse
{
    public partial class Eleves : Form
    {
       
        public Eleves()
        {
            InitializeComponent();
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        
        private  void Eleves_Load(object sender, EventArgs e)
        {
            
        }

        private void Init()
        {
            dataGridView1.Rows[0].Selected = true;
            this.Refresh();
        }
        private void Eleves_Load_1(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");

            HttpResponseMessage response = client.GetAsync("api/Etudiants").Result;

            if (response.IsSuccessStatusCode)
            {
                var etudiants = response.Content.ReadAsAsync<IEnumerable<Etudiant>>().Result;

                // Récupérer les groupes disponibles
                HttpResponseMessage groupesResponse = client.GetAsync("api/Groupes").Result;
                if (groupesResponse.IsSuccessStatusCode)
                {
                    var groupes = groupesResponse.Content.ReadAsAsync<IEnumerable<Groupe>>().Result;

                    // Récupérer les promotions disponibles
                    HttpResponseMessage promotionsResponse = client.GetAsync("api/Promotions").Result;
                    if (promotionsResponse.IsSuccessStatusCode)
                    {
                        var promotions = promotionsResponse.Content.ReadAsAsync<IEnumerable<Promotion>>().Result;

                        // Parcourir les étudiants et récupérer les noms du groupe et de la promotion
                        var data = etudiants.Select(etudiant =>
                        {
                            var groupe = groupes.FirstOrDefault(g => g.Id == etudiant.GroupeId);
                            var promotion = promotions.FirstOrDefault(p => p.Id == etudiant.PromotionId);
                            var nomGroupe = (groupe != null) ? groupe.Nom : string.Empty;
                            var nomPromotion = (promotion != null) ? promotion.Annee : string.Empty;
                            return new
                            {
                                id = etudiant.Id,
                                Nom = etudiant.Nom,
                                Prenom = etudiant.Prenom,
                                Groupe = nomGroupe,
                                Promotion = nomPromotion
                            };
                        }).ToList();

                        dataGridView1.DataSource = data;
                    }
                    else
                    {
                        // Gérer les erreurs de requête pour les promotions
                    }
                }
                else
                {
                    // Gérer les erreurs de requête pour les groupes
                }

            }
        }


        public void Clear()
        {
            Nomtxt.Clear();
            prenomtxt.Clear();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");

            Etudiant etudiant = new Etudiant();
            etudiant.Nom = Nomtxt.Text;
            etudiant.Prenom = prenomtxt.Text;
            string Groupes = cmbgroupes.Text;
            if (Groupes != null)
            {
                if (Groupes == "MS2D-FE")
                {
                    Groupes = "1";
                }
                else
                {
                    Groupes = "2";
                }
            }
            etudiant.GroupeId = Int32.Parse(Groupes);
            string Promotions = cmbgroupes.Text;
            if (Promotions != null)
            {
                //l'id correspondant a la promotion MS2D-Fa = 4
                if (Promotions == "2022-2023")
                {
                    Promotions = "1";
                }
                else
                {
                    // l'id 1 correspond a MS2D-FE
                    Promotions = "1";
                }
            }
            etudiant.PromotionId = Int32.Parse(Promotions);

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Etudiants", etudiant);
            Clear();
            dataGridView1.Refresh();
        }
        public Etudiant etudiant1;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                // Accéder aux valeurs des cellules de la ligne sélectionnée
                string id = selectedRow.Cells["id"].Value.ToString();
                string nom = selectedRow.Cells["Nom"].Value.ToString();
                string prenom = selectedRow.Cells["Prenom"].Value.ToString();
                string groupe = selectedRow.Cells["Groupe"].Value.ToString();
                string promotion = selectedRow.Cells["Promotion"].Value.ToString();

                Idtxt.Text = id;
                Nomtxt.Text = nom;
                prenomtxt.Text = prenom;
                cmbgroupes.Text = groupe;
                cmbpromotion.Text = promotion;
            }
        }





        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    if (dataGridView1.SelectedRows[0].DataBoundItem is Etudiant etudiant)
            //    {
            //        etudiant1 = etudiant;
            //        Idtxt.Text = etudiant1.Id.ToString();
            //        Nomtxt.Text = etudiant1.Nom;
            //        prenomtxt.Text = etudiant1.Prenom;
            //        cmbgroupes.Text = etudiant1.GroupeId.ToString();
            //        cmbpromotion.Text = etudiant1.PromotionId.ToString();
            //        Refresh();
            //    }
            //}
        }



        private async void button2_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");

            Etudiant etudiant2 = new Etudiant();
            string Id =Idtxt.Text;
            etudiant2.Id = Int32.Parse(Id);
            etudiant2.Nom = Nomtxt.Text;
            etudiant2.Prenom = prenomtxt.Text;
            string Groupes = cmbgroupes.Text;
            if (Groupes != null)
            {
                if (Groupes == "MS2D-FE")
                {
                    Groupes = "1";
                }
                else
                {
                    Groupes = "2";
                }
            }
            etudiant2.GroupeId = Int32.Parse(Groupes);
            string Promotions = cmbgroupes.Text;
            if (Promotions != null)
            {
                //l'id correspondant a la promotion MS2D-Fa = 4
                if (Promotions == "2022-2023")
                {
                    Promotions = "1";
                }
                else
                {
                    // l'id 7 correspond a MS2D-FE
                    Promotions = "1";
                }
            }
            etudiant2.PromotionId = Int32.Parse(Promotions);

            HttpResponseMessage response = await client.PutAsJsonAsync("api/Etudiants/" + etudiant2.Id ,etudiant2);
            Clear();
            Eleves eleves = new Eleves();
            eleves.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");

            Etudiant etudiant3 = new Etudiant();
            string Id = Idtxt.Text;
            etudiant3.Id = Int32.Parse(Id);
            etudiant3.Nom = Nomtxt.Text;
            etudiant3.Prenom = prenomtxt.Text;
            string Groupes = cmbgroupes.Text;
            if (Groupes != null)
            {
                if (Groupes == "MS2D-FA")
                {
                    Groupes = "1";
                }
                else
                {
                    Groupes = "2";
                }
            }
            etudiant3.GroupeId = Int32.Parse(Groupes);
            string Promotions = cmbgroupes.Text;
            if (Promotions != null)
            {
                //l'id correspondant a la promotion 1
                if (Promotions == "2022-2023")
                {
                    Promotions = "1";
                }
                else
                {

                    Promotions = "1";
                }
            }
            etudiant3.PromotionId = Int32.Parse(Promotions);

            HttpResponseMessage response = await client.DeleteAsync("api/Etudiants/" + etudiant3.Id);
            Clear();
            Eleves eleves = new Eleves();
            eleves.Show();
            this.Close();
        }

        private void Rechercher()
        {
            // Récupérer la valeur du critère de recherche
            string recherche = recherchetxt.Text;

            // Effacer la sélection précédente
            dataGridView1.ClearSelection();

            // Parcourir toutes les lignes du DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Récupérer les valeurs des colonnes "Nom", "Groupe" et "Promotion"
                string nomCell = row.Cells["Nom"].Value.ToString();
                string groupeCell = row.Cells["Groupe"].Value.ToString();
                string promotionCell = row.Cells["Promotion"].Value.ToString();

                // Vérifier si la ligne correspond aux critères de recherche
                bool filtreNom = string.IsNullOrEmpty(recherche) || nomCell.Contains(recherche);
                bool filtreGroupe = string.IsNullOrEmpty(recherche) || groupeCell.Contains(recherche);
                bool filtrePromotion = string.IsNullOrEmpty(recherche) || promotionCell.Contains(recherche);

                // Vérifier si la ligne correspond aux critères de recherche
                if (filtreNom || filtreGroupe || filtrePromotion)
                {
                    // Mettre en évidence la ligne correspondante
                    row.Selected = true;
                    row.Visible = true; // Rendre la ligne visible
                }
                else
                {
                    // Cacher la ligne qui ne correspond pas au critère de recherche
                    row.Visible = false;
                }
            }
        }
        private void recherchetxt_TextChanged(object sender, EventArgs e)
        {
            Rechercher();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "data.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<string> columnNames = dataGridView1.Columns.Cast<DataGridViewColumn>().Select(column => column.HeaderText);
                sb.AppendLine(string.Join(",", columnNames));
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    IEnumerable<string> fields = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString());
                    sb.AppendLine(string.Join(",", fields));
                }
                File.WriteAllText(sfd.FileName, sb.ToString());
                MessageBox.Show("Données exportées avec succès.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV (*.csv)|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;

                try
                {
                    // Lire le contenu du fichier CSV
                    DataTable dataTable = ReadCsvFile(filePath);

                    // Afficher les données dans le DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'importation du fichier : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dataTable = new DataTable();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string[] headers = reader.ReadLine().Split(',');

                foreach (string header in headers)
                {
                    dataTable.Columns.Add(header);
                }

                while (!reader.EndOfStream)
                {
                    string[] rows = reader.ReadLine().Split(',');
                    dataTable.Rows.Add(rows);
                }
            }

            return dataTable;
        }
    }
}
