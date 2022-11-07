using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Fonts;
using Microsoft.Extensions.Localization;
using System.Reflection;
using QRCoder;

namespace ExpertCities.Data
{
    public class PDFGenerator : IDisposable
    {
        private XFont HeaderFont;
        private XFont BoldFont;
        private XBrush HeaderBrush;
        private XBrush MeasureBrush;
        private XFont ValueFont;
        private XBrush ValueBrush;
        private XBrush ValueBackBrush;
        private XGraphics GFX;
        private PdfDocument Doc;
        private double CurrentY;
        public PDFGenerator()
        {
            InitFonts();
        }
        protected void InitFonts()
        {
            HeaderFont = new XFont("Calibri", 12, XFontStyle.Regular);
            ValueFont = new XFont("Calibri", 8, XFontStyle.Regular);
            BoldFont = new XFont("Calibri", 8, XFontStyle.Bold);
            HeaderBrush = new XSolidBrush(XColors.Black);
        }
        public MemoryStream GetBuilding(Asset Build, string urlPath, IStringLocalizer Loc)
        {
            var m = new MemoryStream();
            Doc = new PdfDocument();
            var p = Doc.AddPage();
            GFX = XGraphics.FromPdfPage(p);
            CurrentY = 10;
            PrintImage(Build, p);
            CurrentY = 10;
            PrintQR(urlPath, p);
            CurrentY = 160f;

            PrintTitle(Build, p, Loc);



            Doc.Save(m);
            m.Position = 0;
            return m;
        }
        public void PrintTitle(Asset Build, PdfPage p, IStringLocalizer Loc)
        {
            var H = GFX.MeasureString(Loc["Building general data"], HeaderFont).Height;
            GFX.DrawString(Loc["Building general data"], HeaderFont, HeaderBrush, 10, CurrentY);
            CurrentY += H;
            GFX.DrawString($"{Loc["Category"]} : {Build.Category}", HeaderFont, HeaderBrush, 10, CurrentY);
            CurrentY += H;
            GFX.DrawString($"{Loc["Name"]} : {Build.Denomination}", HeaderFont, HeaderBrush, 10, CurrentY);
            CurrentY += H;
            GFX.DrawString($"{Loc["Structure"]} : {Build.Structure}", HeaderFont, HeaderBrush, 10, CurrentY);

        }
        public void PrintImage(Asset Build, PdfPage p)
        {
            if (Build.Images.Any())
            {
                var Im = Build.Images.FirstOrDefault();
                var ImageStream = new MemoryStream();
                ImageStream.Write(Im.Image, 0, Im.Image.Length);
                ImageStream.Position = 0;
                var xim = XImage.FromStream(() => ImageStream);
                xim.Interpolate = false;
                var Imy = 136;
                var Imx = xim.PixelWidth * Imy / xim.PixelHeight;
                GFX.DrawImage(xim, (p.Width.Point - 156) / 2 - Imx / 2, CurrentY, Imx, Imy);
            }
        }
        public void PrintQR(string urlPath, PdfPage p)
        {
            var Payload = new PayloadGenerator.Url(urlPath);
            var Data = new PngByteQRCode(QRCodeGenerator.GenerateQrCode(Payload));
            var ImageStream = new MemoryStream();
            var Raw = Data.GetGraphic(10);
            ImageStream.Write(Raw, 0, Raw.Length);
            ImageStream.Position = 0;
            var xim = XImage.FromStream(() => ImageStream);
            xim.Interpolate = false;
            var Imx = 136;
            var Imy = 136;
            GFX.DrawImage(xim, (p.Width - Imx - 10f), CurrentY, Imx, Imy);
            CurrentY += Imy * 2;
        }
        public void Dispose()
        {
            Doc?.Dispose();
            GFX?.Dispose();
        }
    }
}
