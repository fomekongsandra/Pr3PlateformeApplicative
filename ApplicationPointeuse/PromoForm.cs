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
    public partial class PromoForm : Form
    {
        public PromoForm()
        {
            InitializeComponent();
        }

        private void PromoForm_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7166");
            HttpResponseMessage response = client.GetAsync("api/Promtions").Result;
            if (response.IsSuccessStatusCode)
            {
                var promotions = response.Content.ReadAsAsync<IEnumerable<Promotion>>().Result;
                dataGridView1.DataSource = promotions.ToList();
            }
            else
            {
                MessageBox.Show("Une erreur s'est produite lors de la récupération des promotions.");
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
            Promotion promotion = new Promotion();
            promotion.Annee = nomtxt1.Text;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Promotions", promotion);
            Clear();
            dataGridView1.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
