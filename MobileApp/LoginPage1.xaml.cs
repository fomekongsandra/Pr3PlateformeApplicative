
using System;
using ZXing.QrCode;


using com.google.zxing.qrcode;

namespace MobileApp
{
    public partial class LoginPage1 : ContentPage
    {
        public LoginPage1()
        {
            InitializeComponent();
        }

        private void GenerateBtn_Clicked(object sender, EventArgs e)
        {
            QRCoder.QRCodeGenerator qRcodeGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qRCodeData = qRcodeGenerator.CreateQrCode(InputText.Text, QRCoder.QRCodeGenerator.ECCLevel.L);
            QRCoder.PngByteQRCode qRCode = new QRCoder.PngByteQRCode(qRCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));

        }
    }
}
