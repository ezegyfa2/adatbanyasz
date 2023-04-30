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
using Euractiv.Cikkek;
using System.IO;
using SegedFunkciok;

namespace Euractiv.Osszefoglalok
{
    public class EuractivOsszefoglalo: CikkOsszefoglalo<EuractivOsszefoglaloLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Euroactiv", "Osszefoglalok");

        public EuractivOsszefoglalo(HtmlNode node) : base(node)
        {
        }

        public EuractivOsszefoglalo(int id) : base(id)
        {
        }

        public EuractivOsszefoglalo(MySqlDataReader reader) : base(reader)
        {
        }

        public EuractivOsszefoglalo() : base()
        {
        }

        public override string TablaNev
        {
            get
            {
                return "article_summaries";
            }
        }

        protected override void kepBeallitas(HtmlNode node)
        {
            HtmlNode kepNode = node.QuerySelector("img");
            if (kepNode != null)
            {
                string kepLink = HttpUtility.HtmlDecode(node.QuerySelector("img").Attributes["data-src"].Value);
                Kep = new EuractivOsszefoglaloLetoltottKep(kepLink);
            }

        }

        protected override void cimBeallitasa(HtmlNode node)
        {
            Cim = HttpUtility.HtmlDecode(node.QuerySelector("h2").QuerySelector("a").InnerText);
        }

        protected override void szovegBeallitasa(HtmlNode node)
        {
            HtmlNode szovegNode = node.QuerySelector("p");
            Szoveg = HttpUtility.HtmlDecode(szovegNode.InnerText);
        }

        protected override void linkBeallitasa(HtmlNode node)
        {
            Link = "www.euractiv.ro/" + node.QuerySelector("h2").QuerySelector("a").Attributes["href"].Value;
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            HtmlNode datumNode = node.QuerySelector("span.teaser-timestamp");
            if (datumNode == null)
            {
                Datum = new DateTime(1000, 1, 1);
            }
            else
            {
                string datumSzoveg = HttpUtility.HtmlDecode(datumNode.InnerText);
                string[] datumSzovegReszek = datumSzoveg.Trim().Split(' ');
                if (datumSzovegReszek.Length == 1)
                {
                    Datum = DateTime.ParseExact(datumSzoveg.Trim(), "H:m", null);
                }
                else
                {
                    string honapSzoveg = datumSzovegReszek[1];
                    int ev = Int32.Parse(datumSzovegReszek[2]);
                    int honap = Honapok().FindIndex(aktHonap => aktHonap == honapSzoveg);
                    int nap = Int32.Parse(datumSzovegReszek[0]);
                    Datum = new DateTime(ev, honap + 1, nap);
                }
            }
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "Ian",
                "Feb",
                "Mar",
                "Apr",
                "Mai",
                "Iun",
                "Iul",
                "Aug",
                "Sept",
                "Oct",
                "Nov",
                "Dec"
            };
        }

        public override void Export(Document szerkesztendoDokumentum)
        {
            Paragraph cimParagrafus = Jelentes<EuractivOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Cim, "cikkcim");
            cimParagrafus.AddBookmark(Konyvjelzo);
            Jelentes<EuractivOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Datum.ToString(), "cikkalcim");
            ExportUjSor(szerkesztendoDokumentum);
            Jelentes<EuractivOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Szoveg, "leiras");
            //Paragraph kepParagrafus = szerkesztendoDokumentum.Sections[0].AddParagraph();
            //kepParagrafus.Format.Alignment = ParagraphAlignment.Center;
            //Image kep = kepParagrafus.AddImage(Kep.EleresiUtvonal);
            ExportUjSor(szerkesztendoDokumentum);
            ExportUjSor(szerkesztendoDokumentum);
        }
    }
}
