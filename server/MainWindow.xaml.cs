
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using QRCoder;


namespace Server
{
    public partial class MainWindow
    {
       
        public MainWindow()
        {
            InitializeComponent();
            var server = new Server();
            InitIp();
        }
        
        private void InitIp()
        {
            //获得本地连接IP地址
            var appUrl=@"http://tv.huangyiyang.com?ip=" + IpHelper.GetLocalIp();

            //设置Label
            LabelIp.Content = appUrl;

            //设置二维码图片
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(appUrl, QRCodeGenerator.ECCLevel.Q);
            var qrcode = new QRCode(qrCodeData);
            var qrCodeImage = qrcode.GetGraphic(10, Color.Black, Color.White, null, 15, 6, false);
            var ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Bmp); 
            var image = new BitmapImage();  
            image.BeginInit();  
            image.StreamSource = new MemoryStream(ms.GetBuffer()); 
            image.EndInit();
            ImageQrCode.Source = image;
        }
    }
}
