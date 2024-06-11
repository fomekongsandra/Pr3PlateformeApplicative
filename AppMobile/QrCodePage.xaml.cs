using Microsoft.Maui.Controls;
using AppMobile;
using System;
using ZXing.Net.Maui;
using System.IO;


namespace AppMobile

{
    public partial class QrCodePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly Student _student;

        public QrCodePage(Student student)
        {
            InitializeComponent();
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Pointeuse;integrated security=True;TrustServerCertificate=True;"; 
            _databaseService = new DatabaseService(connectionString);
            _student = student;
            StudentNameLabel.Text = $"{_student.Nom} {_student.Prenom}";
        }

        private async void OnGenerateQrCodeClicked(object sender, EventArgs e)
        {
            string qrCodeData = $"{_student.Nom}_{_student.Prenom}_{DateTime.Now:yyyyMMddHHmmss}";

            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 200,
                    Height = 200
                }
            };

            var qrCode = barcodeWriter.Write(qrCodeData);
            QrCodeImage.Source = qrCode;

            // Enregistrer la date et l'heure de génération du QR code dans la base de données
            _student.DateTimeGenerated = DateTime.Now;
            _student.QrCode = qrCodeData;

            
        }
    }
}
