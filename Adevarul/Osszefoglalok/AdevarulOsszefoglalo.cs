using HtmlAdatKigyujtes;
using HtmlAdatKigyujtes.AdatKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using MigraDoc.DocumentObjectModel;

using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel.Shapes;
using AdatbazisFunkciok;
using System.Web;
using Adevarul.Cikkek;
using System.IO;
using SegedFunkciok;

namespace Adevarul.Osszefoglalok
{
    public class AdevarulOsszefoglalo: CikkOsszefoglalo<AdevarulOsszefoglaloLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Adevarul", "Osszefoglalok");

        public override string TablaNev
        {
            get
            {
                return "article_summaries";
            }
        }

        public AdevarulOsszefoglalo(HtmlNode node) : base(node)
        {
        }

        public AdevarulOsszefoglalo(int id) : base(id)
        {
        }

        public AdevarulOsszefoglalo(MySqlDataReader reader) : base(reader)
        {
        }

        public AdevarulOsszefoglalo() : base()
        {
        }

        protected override void kepBeallitas(HtmlNode node)
        {
            string kepLink = node.QuerySelector("img").Attributes["src"].Value;
            Kep = new AdevarulOsszefoglaloLetoltottKep(kepLink);
        }

        protected override void cimBeallitasa(HtmlNode node)
        {
            Cim = HttpUtility.HtmlDecode(node.QuerySelector("a.title").InnerText);
        }

        protected override void szovegBeallitasa(HtmlNode node)
        {
            HtmlNode szovegNode = node.QuerySelectorAll("a")[2];
            Szoveg = HttpUtility.HtmlDecode(szovegNode.InnerText);
        }

        protected override void linkBeallitasa(HtmlNode node)
        {
            Link = node.QuerySelector("a").Attributes["href"].Value;
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector(".date").InnerText);
            string[] datumSzovegReszek = datumSzoveg.Split(' ');
            if (datumSzovegReszek.Length == 1)
            {
                Datum = DateTime.ParseExact(datumSzoveg, "H:m", null);
            }
            else
            {
                string honapSzoveg = datumSzovegReszek[1].Replace(".", "");
                int ev = Int32.Parse(datumSzovegReszek[2]);
                int honap = Honapok().FindIndex(aktHonap => aktHonap == honapSzoveg);
                int nap = Int32.Parse(datumSzovegReszek[0]);
                Datum = new DateTime(ev, honap + 1, nap);
            }
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "ian",
                "feb",
                "mart",
                "apr",
                "mai",
                "iun",
                "iul",
                "aug",
                "sept",
                "oct",
                "nov",
                "dec"
            };
        }

        public override void Export(Document szerkesztendoDokumentum)
        {
            Paragraph cimParagrafus = Jelentes<AdevarulOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Cim, "cikkcim");
            cimParagrafus.AddBookmark(Konyvjelzo);
            Jelentes<AdevarulOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Datum.ToString(), "cikkalcim");
            ExportUjSor(szerkesztendoDokumentum);
            Jelentes<AdevarulOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Szoveg, "leiras");
            //Paragraph kepParagrafus = szerkesztendoDokumentum.Sections[0].AddParagraph();
            //kepParagrafus.Format.Alignment = ParagraphAlignment.Center;
            //Image kep = kepParagrafus.AddImage(Kep.EleresiUtvonal);
            ExportUjSor(szerkesztendoDokumentum);
            ExportUjSor(szerkesztendoDokumentum);
        }
    }
}
