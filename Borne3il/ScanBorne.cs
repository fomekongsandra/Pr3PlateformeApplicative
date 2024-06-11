using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ZXing.QrCode;

namespace Borne3il
{
    public partial class ScanBorne : Form
    {
        private FilterInfoCollection CaptureDivice;
        private VideoCaptureDevice FinalFrame;
        public ScanBorne()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDivice[comboBox1.SelectedIndex].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();
        }

        private void ScanBorne_Load(object sender, EventArgs e)
        {
            CaptureDivice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDivice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }
        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader reader = new BarcodeReader();
            pictureBox1.Save(new MemoryStream(), pictureBox1.RawFormat);
            Result result = reader.Decode(pictureBox1.Image);
            try
            {
                if (result != null)
                {

                    var decode = result.ToString().Trim();
                    if (decode != "")
                    {
                        textBox1.Text = decode;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la manipulation du résultat du décodage du code-barres : " + ex.Message);

            }
        }
    }
}
