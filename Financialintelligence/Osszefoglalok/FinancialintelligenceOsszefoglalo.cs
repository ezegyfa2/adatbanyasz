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
using Financialintelligence.Cikkek;
using System.IO;
using SegedFunkciok;
using System.Linq;

namespace Financialintelligence.Osszefoglalok
{
    public class FinancialintelligenceOsszefoglalo: CikkOsszefoglalo<FinancialintelligenceOsszefoglaloLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Intellinews", "Osszefoglalok");

        public FinancialintelligenceOsszefoglalo(HtmlNode node) : base(node)
        {
        }

        public FinancialintelligenceOsszefoglalo(int id) : base(id)
        {
        }

        public FinancialintelligenceOsszefoglalo(MySqlDataReader reader) : base(reader)
        {
        }

        public FinancialintelligenceOsszefoglalo() : base()
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
                string kepLink = "https:" + HttpUtility.HtmlDecode(kepNode.Attributes["src"].Value);
                while (kepLink.First() == '/')
                {
                    kepLink = kepLink.Substring(1);
                }
                Kep = new FinancialintelligenceOsszefoglaloLetoltottKep(kepLink);
            }
        }

        protected override void cimBeallitasa(HtmlNode node)
        {
            Cim = HttpUtility.HtmlDecode(node.QuerySelector("h1").InnerText);
        }

        protected override void szovegBeallitasa(HtmlNode node)
        {
            HtmlNode szovegNode = node.QuerySelector(".entry-content");
            Szoveg = HttpUtility.HtmlDecode(szovegNode.InnerText);
        }

        protected override void linkBeallitasa(HtmlNode node)
        {
            Link = node.QuerySelector("header").QuerySelector("a").Attributes["href"].Value;
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("footer").QuerySelector("time").InnerText);
            string[] datumSzovegReszek = datumSzoveg.Trim().Split(' ');
            string honapSzoveg = datumSzovegReszek[1];
            int ev = Int32.Parse(datumSzovegReszek[2]);
            int honap = Honapok().FindIndex(aktHonap => aktHonap == honapSzoveg);
            int nap = Int32.Parse(datumSzovegReszek[0]);
            Datum = new DateTime(ev, honap + 1, nap);
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "ianuarie",
                "februarie",
                "martie",
                "aprilie",
                "mai",
                "iunie",
                "iulie",
                "august",
                "septembrie",
                "octombrie",
                "noiembrie",
                "decembrie"
            };
        }

        public override void Export(Document szerkesztendoDokumentum)
        {
            Paragraph cimParagrafus = Jelentes<FinancialintelligenceOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Cim, "cikkcim");
            cimParagrafus.AddBookmark(Konyvjelzo);
            Jelentes<FinancialintelligenceOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Datum.ToString(), "cikkalcim");
            ExportUjSor(szerkesztendoDokumentum);
            Jelentes<FinancialintelligenceOsszefoglalo>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Szoveg, "leiras");
            //Paragraph kepParagrafus = szerkesztendoDokumentum.Sections[0].AddParagraph();
            //kepParagrafus.Format.Alignment = ParagraphAlignment.Center;
            //Image kep = kepParagrafus.AddImage(Kep.EleresiUtvonal);
            ExportUjSor(szerkesztendoDokumentum);
            ExportUjSor(szerkesztendoDokumentum);
        }
    }
}
