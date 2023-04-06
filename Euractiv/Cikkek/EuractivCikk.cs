using Jelentesek;

using HtmlAgilityPack;
using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using Euractiv.Osszefoglalok;
using System.Linq;
using System.Web;
using System.IO;
using SegedFunkciok;

namespace Euractiv.Cikkek
{
    public class EuractivCikk: Cikk<EuractivOsszefoglalo, EuractivCikkLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Euroactiv", "Cikkek");

        public EuractivCikk()
        {
        }

        public EuractivCikk(HtmlNode node) : base(node)
        {
        }

        public EuractivCikk(MySqlDataReader reader) : base(reader)
        {
        }

        protected override void kepBeallitas(HtmlNode node)
        {
            HtmlNode kepNode = node.QuerySelector("img");
            if (kepNode == null || kepNode.Attributes["src"] == null)
            {
                Kep = null;
            }
            else
            {
                string kepLink = HttpUtility.HtmlDecode(kepNode.Attributes["src"].Value);
                Kep = new EuractivCikkLetoltottKep(kepLink);
            }
        }

        protected override void focimBeallitasa(HtmlNode node)
        {
            HtmlNode cimNode = node.QuerySelector("h2.page-header");
            Focim = cimNode.InnerText;
            HtmlNode cimKategoriaNode = cimNode.QuerySelector("span.tag-titlu");
            if (cimKategoriaNode != null)
            {
                string kategoriaSzoveg = cimKategoriaNode.InnerText;
                Focim.Replace(kategoriaSzoveg, "");
            }
            Focim = HttpUtility.HtmlDecode(Focim);
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("span.format-timestamp").InnerText);
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

        protected override void cikkReszekBeallitasa(HtmlNode node)
        {
            List<HtmlNode> tagParagrafusok = tagParagrafusokLekerese(node);
            List<HtmlNode> kommentParagrafusok = kommentNodek(node);
            List<HtmlNode> cikkReszNodek = node.QuerySelectorAll("p, h3, .caseta-background-articol").ToList();
            int pozicio = 0;
            foreach (HtmlNode cikkReszNode in cikkReszNodek)
            {
                if (!tagParagrafusok.Contains(cikkReszNode) && !kommentParagrafusok.Contains(cikkReszNode) && cikkReszNode.InnerText != "")
                {
                    if (cikkReszNode.Name == "p")
                    {
                        if (cikkReszNode.ParentNode.Name == "blockquote"
                            || (cikkReszNode.ParentNode.Attributes["class"] != null
                            && cikkReszNode.ParentNode.Attributes["class"].Value == "field-item even"))
                        {
                            Cimek.Add(new CikkCim(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                        }
                        else
                        {
                            Bekezdesek.Add(new CikkBekezdes(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                        }
                    }
                    else if (cikkReszNode.Name == "h2" || cikkReszNode.Name == "h3")
                    {
                        Cimek.Add(new CikkCim(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                    }
                    else if (cikkReszNode.Attributes["class"] != null
                            && cikkReszNode.Attributes["class"].Value == "caseta-background-articol")
                    {
                        Bekezdesek.Add(new CikkBekezdes(HttpUtility.HtmlDecode(cikkReszNode.InnerText), pozicio, this));
                    }
                    else
                    {
                        throw new Exception("Invalid node type");
                    }
                    ++pozicio;
                }
            }
        }

        protected List<HtmlNode> kommentNodek(HtmlNode node)
        {
            HtmlNode kommentNode = node.QuerySelector(".comentarii, #comments");
            if (kommentNode == null)
            {
                return new List<HtmlNode>();
            }
            else
            {
                var kommentParagrafusok = kommentNode.QuerySelectorAll("p, h3, .caseta-background-articol");
                if (kommentParagrafusok == null)
                {
                    return new List<HtmlNode>();
                }
                else
                {
                    return kommentParagrafusok.ToList();
                }
            }
        }

        protected List<HtmlNode> tagParagrafusokLekerese(HtmlNode node)
        {
            HtmlNode tagNode = node.QuerySelector("#node-eaopinion-full-group-keywords");
            if (tagNode == null)
            {
                return new List<HtmlNode>();
            }
            else
            {
                return tagNode.QuerySelectorAll("p, h3, .caseta-background-articol").ToList();
            }
        }
    }
}
