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

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
           Menu menu = new Menu();
            menu.Show();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
