using MigraDocCore.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharpCore.Utils;
using MigraDocCore.DocumentObjectModel.Shapes;
using QRCoder;
using Microsoft.Extensions.Localization;

namespace ExpertCities.Data
{
    public class DocumentGenerator
    {
        public static Color AccentColor = new Color(60, 20, 80);
        public MemoryStream CreateBuildingSummary(Building Building, string urlPath, IStringLocalizer Loc)
        {
            var Doc = new Document();

            var Sec = Doc.AddSection();

            DefineStyles(Doc, Sec);
            CreateHeader(Doc, Sec, Building, urlPath);
            CreateDocument(Doc, Sec, Building, Loc);
            //CreateFooter(Doc, Sec);

            var Renderer = new PdfDocumentRenderer();
            Renderer.Document = Doc;
            Renderer.RenderDocument();
            var Mem = new MemoryStream();
            Renderer.Save(Mem, false);
            return Mem;
        }
        void CreateHeader(Document Doc, Section Sec, Building Building, string urlPath)
        {
            var sectionWidth = Sec.PageSetup.PageWidth.Point - Sec.PageSetup.LeftMargin.Point - Sec.PageSetup.RightMargin.Point;
            var T = Sec.Headers.Primary.AddTable();
            T.AddColumn().Width = sectionWidth * 0.9f;
            T.AddColumn().Width = sectionWidth * 0.1f;
            var R = T.AddRow();
            if (Building.Images.Any())
            {
                var Par = R.Cells[0].AddParagraph();
                Par.Format.Alignment = ParagraphAlignment.Center;
                var Im = Par.AddImage(ImageSource.FromBinary("Image", () => Building.Images.First().Image));

                Im.Height = "3cm"; Im.LockAspectRatio = true; Im.Left = ShapePosition.Center;
            }
            var Payload = new PayloadGenerator.Url(urlPath);
            var Data = new PngByteQRCode(QRCodeGenerator.GenerateQrCode(Payload));
            var Raw = Data.GetGraphic(10);
            var Par2 = R.Cells[1].AddParagraph();
            Par2.Format.Alignment = ParagraphAlignment.Center;
            var Im2 = Par2.AddImage(ImageSource.FromBinary("QRCode", () => Raw));
            Im2.Height = "3cm"; Im2.LockAspectRatio = true; Im2.Left = ShapePosition.Center;
        }
        void CreateDocument(Document Doc, Section Sec, Building Building, IStringLocalizer Loc)
        {
            Sec.AddParagraph().AddLineBreak();
            var sectionWidth = Sec.PageSetup.PageWidth.Point - Sec.PageSetup.LeftMargin.Point - Sec.PageSetup.RightMargin.Point;
            var Table = Sec.AddTable();
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            var Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Building general data"]).Format.Font.Bold = true;
            Row.Cells[0].MergeRight = 5;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Category"]);
            Row.Cells[1].AddParagraph(Building.Category.GetLocalizeName());
            Row.Cells[2].AddParagraph(Loc["Name"]);
            Row.Cells[3].AddParagraph(Building.Denomination);
            Row.Cells[4].AddParagraph(Loc["Structure"]);
            Row.Cells[5].AddParagraph(Building.Structure.GetLocalizeName());
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Dates"]).Format.Font.Bold = true;
            Row.Cells[0].MergeRight = 5;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Acquisition Date"]);
            if (Building.Date_Acquire.HasValue)
                Row.Cells[1].AddParagraph(Building.Date_Acquire.Value.ToString("yyyy/MM/dd"));
            Row.Cells[2].AddParagraph(Loc["Commission Date"]);
            if (Building.Date_Commission.HasValue)
                Row.Cells[3].AddParagraph(Building.Date_Commission.Value.ToString("yyyy/MM/dd"));
            Row.Cells[4].AddParagraph(Loc["Warranty Date"]);
            if (Building.Date_Garantee.HasValue)
                Row.Cells[5].AddParagraph(Building.Date_Garantee.Value.ToString("yyyy/MM/dd"));
            SetBorders(Table);

            //Valeurs
            Table = Sec.AddTable();
            Table.AddColumn().Width = sectionWidth * .2f;
            Table.AddColumn().Width = sectionWidth * .3f;
            Table.AddColumn().Width = sectionWidth * .2f;
            Table.AddColumn().Width = sectionWidth * .3f;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Values"]).Format.Font.Bold = true;
            Row.Cells[0].MergeRight = 3;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Acquirement Value"]);
            Row.Cells[1].AddParagraph($"{Building.Val_Acquire / 100f} $");
            Row.Cells[2].AddParagraph(Loc["Actual Value"]);
            Row.Cells[3].AddParagraph("Calculation");
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Remplacement Value"]);
            Row.Cells[1].AddParagraph($"{Building.Val_Remplace / 100f} $");
            Row.Cells[2].AddParagraph(Loc["Works Value"]);
            Row.Cells[3].AddParagraph($"{Building.Val_Work / 100f} $");
            SetBorders(Table);

            Table = Sec.AddTable();
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            Table.AddColumn().Width = sectionWidth * .1333f;
            Table.AddColumn().Width = sectionWidth * .20f;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Geometry"]).Format.Font.Bold = true;
            Row.Cells[0].MergeRight = 5;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Length"]);
            Row.Cells[1].AddParagraph($"{Building.Length} m");
            Row.Cells[2].AddParagraph(Loc["Width"]);
            Row.Cells[3].AddParagraph($"{Building.Width} m");
            Row.Cells[4].AddParagraph(Loc["Surface"]);
            Row.Cells[5].AddParagraph($"{Building.Surface} m²");
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Depth"]);
            Row.Cells[1].AddParagraph($"{Building.Depth} m");
            Row.Cells[2].AddParagraph(Loc["Shape"]);
            Row.Cells[3].AddParagraph($"{Building.Shape.GetLocalizeName()}");
            Row.Cells[4].AddParagraph(Loc["Volume"]);
            Row.Cells[5].AddParagraph($"{Building.Volume} m3");
            SetBorders(Table);

            //Contact
            Table = Sec.AddTable();
            Table.AddColumn().Width = sectionWidth * .2f;
            Table.AddColumn().Width = sectionWidth * .3f;
            Table.AddColumn().Width = sectionWidth * .2f;
            Table.AddColumn().Width = sectionWidth * .3f;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Contact"]).Format.Font.Bold = true;
            Row.Cells[0].MergeRight = 3;
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Country"]);
            if (Building.Country != null)
                Row.Cells[1].AddParagraph(Building.Country);
            Row.Cells[2].AddParagraph(Loc["City"]);
            if (Building.City != null)
                Row.Cells[3].AddParagraph(Building.City);
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Zip code"]);
            if (Building.ZipCode != null)
                Row.Cells[1].AddParagraph(Building.ZipCode);
            Row.Cells[2].AddParagraph(Loc["Civic number"]);
            if (Building.CivicNumber != null)
                Row.Cells[3].AddParagraph(Building.CivicNumber);
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Street name, road ..."]);
            if (Building.Street != null)
                Row.Cells[1].AddParagraph(Building.Street);
            Row.Cells[2].AddParagraph(Loc["Land line, mobile phone"]);
            if (Building.Telephone != null)
                Row.Cells[3].AddParagraph(Building.Telephone);
            Row = Table.AddRow();
            Row.Cells[0].AddParagraph(Loc["Email"]);
            if (Building.Email != null)
                Row.Cells[1].AddParagraph(Building.Email);
            Row.Cells[2].AddParagraph(Loc["Position"]);
            if (Building.Position != null)
                Row.Cells[3].AddParagraph(Building.Position);

            SetBorders(Table);

            if (Building.Description != null)
                Sec.AddParagraph(Building.Description);
            if (Building.Observation != null)
                Sec.AddParagraph(Building.Observation);
        }
        void SetBorders(Table Table)
        {
            Table.Borders.Style = BorderStyle.Single;
            Table.Borders.Color = Colors.Black;
            Table.Borders.Visible = true;
        }
        void DefineStyles(Document Doc, Section Sec)
        {
            ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();
            // Get the predefined style Normal.
            Style style = Doc.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";
            style.Font.Size = 8;

            style = Doc.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Right);


            style = Doc.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.Font.Size = 7;

            // Create a new style called Table based on style Normal
            style = Doc.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Times New Roman";
            style.Font.Size = 9;

            style = Doc.Styles.AddStyle("TableHeader", "Normal");
            style.Font.Size = 12;
            style.Font.Color = Colors.White;
            style.ParagraphFormat.Shading.Color = AccentColor;


            // Create a new style called Reference based on style Normal
            style = Doc.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "5mm";
            style.ParagraphFormat.SpaceAfter = "5mm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);

            PageSetup.GetPageSize(PageFormat.A4, out Unit w, out Unit h);
            Sec.PageSetup.PageHeight = h;
            Sec.PageSetup.PageWidth = w;
            Sec.PageSetup.LeftMargin = "1cm";
            Sec.PageSetup.RightMargin = "1cm";
            Sec.PageSetup.PageFormat = PageFormat.A4;
            Sec.PageSetup.TopMargin = "4cm";
            Sec.PageSetup.BottomMargin = "2cm";
            Sec.PageSetup.FooterDistance = "-2cm";

        }
    }
}
