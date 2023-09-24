using Microsoft.Maui.Controls;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using com.google.zxing.qrcode;
using SkiaSharp;


namespace MobileApp
{
    public partial class GenerateQRCodePage : ContentPage
    {
        private ScanData scan; // Assurez-vous d'avoir un objet ScanData

        public GenerateQRCodePage(ScanData scanData)
        {
            InitializeComponent();
            scan = scanData;

            // G�n�rer le contenu du QR Code � partir des informations stock�es dans scan
            string qrCodeContent = $"Nom: {scan.StudentName}, Pr�nom: {scan.StudentFirstName}, Groupe: {scan.StudentGroup}, Promotion: {scan.StudentPromotion}, Date/Heure: {scan.ScanDateTime}";

            // G�n�rer le QR Code
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 400,
                    Height = 400
                }
            };

            var qrCodeBitmap = writer.Write(qrCodeContent);

            // Convertir le SKBitmap en tableau d'octets au format PNG
            using (var image = SKImage.FromBitmap(qrCodeBitmap))
            {
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    byte[] qrCodeBytes = data.ToArray();

                    // Afficher le QR Code dans l'�l�ment Image
                    qrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                }
            }
        }

        private void ReturnButton_Clicked(object sender, EventArgs e)
        {
            // Retourner � la page pr�c�dente
            Navigation.PopAsync();
        }
    }
}
