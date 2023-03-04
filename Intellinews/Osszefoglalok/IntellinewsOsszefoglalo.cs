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
using Intellinews.Cikkek;
using System.IO;
using SegedFunkciok;
using System.Linq;

namespace Intellinews.Osszefoglalok
{
    public class IntellinewsOsszefoglalo: CikkOsszefoglalo<IntellinewsOsszefoglaloLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Intellinews", "Osszefoglalok");

        public IntellinewsOsszefoglalo(HtmlNode node) : base(node)
        {
        }

        public IntellinewsOsszefoglalo(int id) : base(id)
        {
        }

        public IntellinewsOsszefoglalo(MySqlDataReader reader) : base(reader)
        {
        }

        public IntellinewsOsszefoglalo() : base()
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
            string kepLink = "https:" + HttpUtility.HtmlDecode(node.QuerySelector("img.img-full").Attributes["src"].Value);
            while (kepLink.First() == '/')
            {
                kepLink = kepLink.Substring(1);
            }
            Kep = new IntellinewsOsszefoglaloLetoltottKep(kepLink);
        }

        protected override void cimBeallitasa(HtmlNode node)
        {
            Cim = HttpUtility.HtmlDecode(node.QuerySelector("h3").QuerySelector("a").InnerText);
        }

        protected override void szovegBeallitasa(HtmlNode node)
        {
            HtmlNode szovegNode = node.QuerySelector("p");
            Szoveg = HttpUtility.HtmlDecode(szovegNode.InnerText);
        }

        protected override void linkBeallitasa(HtmlNode node)
        {
            Link = "www.intellinews.com" + node.QuerySelector("h3").QuerySelector("a").Attributes["href"].Value;
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("span.date").InnerText);
            string[] datumSzovegReszek = datumSzoveg.Trim().Replace(",", "").Split(' ');
            if (datumSzovegReszek.Length == 1)
            {
                Datum = DateTime.ParseExact(datumSzoveg.Trim(), "H:m", null);
            }
            else
            {
                string honapSzoveg = datumSzovegReszek[0];
                int ev = Int32.Parse(datumSzovegReszek[2]);
                int honap = Honapok().FindIndex(aktHonap => aktHonap == honapSzoveg);
                int nap = Int32.Parse(datumSzovegReszek[1]);
                Datum = new DateTime(ev, honap, nap);
            }
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };
        }

        public override void Export(Document szerkesztendoDokumentum)
        {
            Paragraph cimParagrafus = Jelentes<IntellinewsOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Cim, "cikkcim");
            cimParagrafus.AddBookmark(Konyvjelzo);
            Jelentes<IntellinewsOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Datum.ToString(), "cikkalcim");
            ExportUjSor(szerkesztendoDokumentum);
            Jelentes<IntellinewsOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Szoveg, "leiras");
            //Paragraph kepParagrafus = szerkesztendoDokumentum.Sections[0].AddParagraph();
            //kepParagrafus.Format.Alignment = ParagraphAlignment.Center;
            //Image kep = kepParagrafus.AddImage(Kep.EleresiUtvonal);
            ExportUjSor(szerkesztendoDokumentum);
            ExportUjSor(szerkesztendoDokumentum);
        }
    }
}
