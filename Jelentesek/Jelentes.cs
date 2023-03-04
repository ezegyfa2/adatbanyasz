using AdatbazisFunkciok;

using MySql.Data.MySqlClient;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using PdfSharp.Drawing;
using System.Xml.Linq;

namespace Jelentesek
{
    abstract public class Jelentes<HirTipus> where HirTipus : ExportalhatoAdat, new()
    {
        protected static string datumFormatum = "yyyy-MM-dd HH:mm:ss";

        public void Export(string[] args)
        {
            try
            {
                Adatbazis.Beallitas("config.txt");
                bool megnyitasExportUtan = true;
                if (args.Length > 0 && (args.Last() == "megnyitas-nelkul" || args.Last() == "-m"))
                {
                    megnyitasExportUtan = false;
                    args = args.ToList().GetRange(0, args.Length - 1).ToArray();
                }
                switch (args.Length)
                {
                    case 0:
                        Export(megnyitasExportUtan);
                        break;
                    case 1:
                        Export(DateTime.Parse(args[0]), megnyitasExportUtan);
                        break;
                    case 2:
                        Export(DateTime.Parse(args[0]), DateTime.Parse(args[1]), megnyitasExportUtan);
                        break;
                    default:
                        throw new Exception("Hibas parameterek. Parameterkent megadhato a kezdeti es a veg datum tovabba, hogy az exportalt fajl megnyitodjon-e a muvelet befejeztevel");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " (A teljes uzenet a Hibajelentes.txt-ben talalhato)", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hibaJelentesKeszites(e);
            }
        }

        private static void hibaJelentesKeszites(Exception e)
        {
            try
            {
                using (StreamWriter hibaJelentesKiiro = new StreamWriter("Hibajelentes.txt", true))
                {

                    hibaJelentesKiiro.WriteLine(DateTime.Now);
                    hibaJelentesKiiro.WriteLine(e.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        public void Export(bool megnyitasExportUtan = true)
        {
            Export(legkorabbiDatum, legkesobbiDatum, megnyitasExportUtan);
        }

        protected DateTime legkorabbiDatum
        {
            get
            {
                HirTipus hir = new HirTipus();
                string lekeresQuery = "SELECT date FROM " + hir.TablaNev + " ORDER BY date LIMIT 1";
                MySqlConnection adatbazisKapcsolat = Adatbazis.KapcsolatKeszites();
                MySqlDataReader beolvaso = (new MySqlCommand(lekeresQuery, adatbazisKapcsolat)).ExecuteReader();
                if (beolvaso.HasRows)
                {
                    beolvaso.Read();
                    DateTime lekertDatum = beolvaso.GetDateTime("date");
                    adatbazisKapcsolat.Close();
                    return lekertDatum;
                }
                else
                    return new DateTime(1000, 1, 1);
            }
        }

        public void Export(DateTime kezdetiDatum, bool megnyitasExportUtan = true)
        {
            Export(kezdetiDatum, legkesobbiDatum, megnyitasExportUtan);
        }

        protected DateTime legkesobbiDatum
        {
            get
            {
                HirTipus hir = new HirTipus();
                string lekeresQuery = "SELECT date FROM " + hir.TablaNev + " ORDER BY date DESC LIMIT 1";
                MySqlConnection adatbazisKapcsolat = Adatbazis.KapcsolatKeszites();
                MySqlDataReader beolvaso = (new MySqlCommand(lekeresQuery, adatbazisKapcsolat)).ExecuteReader();
                if (beolvaso.HasRows)
                {
                    beolvaso.Read();
                    DateTime lekertDatum = DateTime.Parse(beolvaso.GetString("date"));
                    adatbazisKapcsolat.Close();
                    return lekertDatum;
                }
                else
                    return DateTime.Now;
            }
        }

        public void Export(DateTime kezdetiDatum, DateTime vegsoDatum, bool megnyitasExportUtan = true)
        {
            HirTipus hir = new HirTipus();
            string lekeresQuery = query(hir.TablaNev, kezdetiDatum, vegsoDatum);
            MySqlConnection adatbazisKapcsolat = Adatbazis.KapcsolatKeszites();
            MySqlDataReader beolvaso = (new MySqlCommand(lekeresQuery, adatbazisKapcsolat)).ExecuteReader();
            List<HirTipus> hirOsszefoglalok = new List<HirTipus>();
            while (beolvaso.Read())
                hirOsszefoglalok.Add((HirTipus)Activator.CreateInstance(typeof(HirTipus), beolvaso));
            adatbazisKapcsolat.Close();
            Export(hirOsszefoglalok, kezdetiDatum, vegsoDatum, megnyitasExportUtan);
        }

        protected void exportFejlecBeallitasa(Document exportalandoDokumentum)
        {
            if (ExportFejlecKepEleresiUtvonal != null)
            {
                HeaderFooter fejlec = exportalandoDokumentum.Sections[0].Headers.Primary;
                Paragraph fejlecParagrafus = fejlec.AddParagraph();
                fejlecParagrafus.Format.Alignment = ParagraphAlignment.Center;
                fejlecParagrafus.AddImage(ExportFejlecKepEleresiUtvonal);
                //fejlec.AddParagraph();
            }
        }

        public static Paragraph FormazottSzovegExportalasa(Document exportalandoDokumentum, string szoveg, string stilusNev)
        {
            Paragraph szovegParagrafus = exportalandoDokumentum.Sections[0].AddParagraph();
            szovegParagrafus.AddText(szoveg);
            szovegParagrafus.Style = stilusNev;
            return szovegParagrafus;
        }

        public static void ExportLinkekBeallitasa(Document exportalandoDokumentum, List<ExportalhatoAdat> exportalandoAdatok)
        {
            foreach (ExportalhatoAdat exportalandoAdat in exportalandoAdatok)
            {
                Paragraph linkParagrafus = exportalandoDokumentum.Sections[0].AddParagraph();
                linkParagrafus.Style = "link";
                Hyperlink link = linkParagrafus.AddHyperlink(exportalandoAdat.Konyvjelzo);
                link.AddFormattedText(" - " + exportalandoAdat.Konyvjelzo);
                //link.AddPageRefField(exportalandoAdat.Konyvjelzo);
            }
            ExportalhatoAdat.ExportUjSor(exportalandoDokumentum);
        }

        protected void exportStilusokBeallitasa(Document beallitandoDokumentum)
        {
            Style foCimStilus = new Style("focim", "Normal");
            foCimStilus.Font.Size = 24;
            foCimStilus.Font.Bold = true;
            foCimStilus.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            beallitandoDokumentum.Styles.Add(foCimStilus);

            Style alcimStilus = new Style("alcim", "Normal");
            alcimStilus.Font.Size = 11;
            alcimStilus.Font.Italic = true;
            alcimStilus.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            beallitandoDokumentum.Styles.Add(alcimStilus);

            Style cikkCimStilus = new Style("cikkcim", "Normal");
            cikkCimStilus.Font.Size = 14;
            cikkCimStilus.Font.Name = "Calibri";
            cikkCimStilus.Font.Bold = true;
            beallitandoDokumentum.Styles.Add(cikkCimStilus);

            Style cikkAlcimStilus = new Style("cikkalcim", "Normal");
            cikkAlcimStilus.Font.Size = 11;
            cikkAlcimStilus.Font.Name = "Calibri";
            cikkAlcimStilus.Font.Italic = true;
            beallitandoDokumentum.Styles.Add(cikkAlcimStilus);

            Style leirasStilus = new Style("leiras", "Normal");
            leirasStilus.Font.Size = 11;
            leirasStilus.Font.Name = "Calibri";
            beallitandoDokumentum.Styles.Add(leirasStilus);

            Style linkStilus = new Style("link", "Normal");
            linkStilus.Font.Size = 11;
            linkStilus.Font.Name = "Calibri";
            linkStilus.Font.Underline = Underline.Single;
            beallitandoDokumentum.Styles.Add(linkStilus);
        }

        public static void MentesPDFkent(Document exportalandoDokumentum, string exprotalandoFajlEleresiUtvonal, bool megnyitasExportUtan = true)
        {
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
            pdfRenderer.Document = exportalandoDokumentum;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(exprotalandoFajlEleresiUtvonal);
            if (megnyitasExportUtan)
                System.Diagnostics.Process.Start(exprotalandoFajlEleresiUtvonal);
        }

        /*private static DocumentObject kepKereses(Document doc)
        {
            foreach (Paragraph p in doc.Sections[0].Paragraphs)
                foreach (DocumentObject d in p.ChildObjects)
                    if (d.DocumentObjectType == DocumentObjectType.Picture)
                        return d;
            return null;
        }*/

        protected virtual string query(string tablaNev, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            return "SELECT * FROM " + tablaNev + " WHERE date BETWEEN \"" + kezdetiDatum.ToString(datumFormatum) + "\" AND \"" + vegsoDatum.ToString(datumFormatum) + "\"";
        }

        abstract public string ExportFejlecKepEleresiUtvonal { get; }
        abstract public void Export(List<HirTipus> exportalandoHirOsszefoglalok, DateTime kezdetiDatum, DateTime vegsoDatum, bool megnyitasExportUtan = true);
    }
}
