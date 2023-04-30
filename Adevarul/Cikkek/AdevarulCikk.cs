using HtmlAdatKigyujtes;
using HtmlAdatKigyujtes.AdatKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using MigraDoc.DocumentObjectModel;

using System;
using System.Collections.Generic;
using MigraDoc.DocumentObjectModel.Shapes;
using Adevarul.Osszefoglalok;
using AdatbazisFunkciok;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.IO;
using SegedFunkciok;

namespace Adevarul.Cikkek
{
    public class AdevarulCikk: Cikk<AdevarulOsszefoglalo, AdevarulCikkLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Adevarul", "Cikkek");
        
        public AdevarulCikk() 
        {
        }

        public AdevarulCikk(HtmlNode node) : base(node)
        {
        }

        public AdevarulCikk(MySqlDataReader reader): base(reader)
        {
        }

        protected override void kepBeallitas(HtmlNode node)
        {
            if (node.QuerySelector("img") != null && node.QuerySelector("img").Attributes.Contains("src"))
            {
                string kepLink = node.QuerySelector("img").Attributes["src"].Value;
                Kep = new AdevarulCikkLetoltottKep(kepLink);
            }
        }

        protected override void focimBeallitasa(HtmlNode node)
        {
            Focim = HttpUtility.HtmlDecode(node.QuerySelector("h1").InnerText);
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("time").InnerText);
            string[] datumSzovegReszek = datumSzoveg.Replace("\t", "").Replace("\n", "").Split(' ');
            if (datumSzovegReszek.Length == 1)
            {
                Datum = DateTime.ParseExact(datumSzoveg, "H:m", null);
            }
            else
            {
                string[] idoSzovegReszek = datumSzovegReszek[1].Split(':');
                int ora = Int32.Parse(idoSzovegReszek[0]);
                int perc = Int32.Parse(idoSzovegReszek[1]);

                datumSzovegReszek = datumSzovegReszek[0].Split('.');
                int ev = Int32.Parse(datumSzovegReszek[2]);
                int honap = Int32.Parse(datumSzovegReszek[1]);
                int nap = Int32.Parse(datumSzovegReszek[0]);
                Datum = new DateTime(ev, honap, nap, ora, perc, 0);
            }
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "ian.",
                "feb.",
                "mart.",
                "apr.",
                "mai.",
                "iun.",
                "iul.",
                "aug.",
                "sept.",
                "okt.",
                "nov.",
                "dec."
            };
        }

        protected override void cikkReszekBeallitasa(HtmlNode node)
        {
            List<HtmlNode> cikkReszNodek = node.QuerySelectorAll("main > p, main > h2").OrderBy(cikkReszNode => cikkReszNode.StreamPosition).ToList();
            int pozicio = 0;
            foreach (HtmlNode cikkReszNode in cikkReszNodek)
            {
                if (cikkReszNode.Name == "p")
                {
                    Bekezdesek.Add(new CikkBekezdes(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                }
                else if (cikkReszNode.Name == "h2")
                {
                    Cimek.Add(new CikkCim(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                }
                else
                {
                    throw new Exception("Invalid node type");
                }
                ++pozicio;
            }
        }
    }
}
