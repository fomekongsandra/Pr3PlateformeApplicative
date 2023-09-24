using System.Text.RegularExpressions;

namespace Borne3il
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ScanBorne ScanBorne = new ScanBorne();
            ScanBorne.Show();
            this.Close();
        }
    }
}