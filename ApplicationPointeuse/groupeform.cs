using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationPointeuse
{
    public partial class groupeform : Form
    {
        public groupeform()
        {
            InitializeComponent();
        }

        private void groupeform_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");
            HttpResponseMessage response = client.GetAsync("api/Groupes").Result;
            if (response.IsSuccessStatusCode)
            {
                var groupes = response.Content.ReadAsAsync<IEnumerable<Groupe>>().Result;
                dataGridView1.DataSource = groupes.ToList();
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de la récupération des groupes.");
            }
        }
        public void Clear()
        {
            nomtxt1.Clear();

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");
            Groupe groupe = new Groupe();
            groupe.Nom = nomtxt1.Text;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Groupes", groupe);
            Clear();
            dataGridView1.Refresh();

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                // Accéder aux valeurs des cellules de la ligne sélectionnée
                string id = selectedRow.Cells["id"].Value.ToString();
                string nom = selectedRow.Cells["Nom"].Value.ToString();

                idtxt.Text = id;
                nomtxt1.Text = nom;

            }
        }
        private async void button3_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");
            Groupe groupe2 = new Groupe();
            string Id = idtxt.Text;
            groupe2.Id = Int32.Parse(Id);
            groupe2.Nom = nomtxt1.Text;
            HttpResponseMessage response = await client.PutAsJsonAsync("api/Groupes/" + groupe2.Id, groupe2);
            Clear();
            groupeform groupeform = new groupeform();
            groupeform.Show();
            this.Close();

        }
        private async void button2_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");
            Groupe groupe3 = new Groupe();
            string Id = idtxt.Text;
            groupe3.Id = Int32.Parse(Id);
            groupe3.Nom = nomtxt1.Text;
            HttpResponseMessage response = await client.DeleteAsync("api/Groupes/" + groupe3.Id);
            Clear();
            groupeform groupeform = new groupeform();
            groupeform.Show();
            this.Close();
        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
