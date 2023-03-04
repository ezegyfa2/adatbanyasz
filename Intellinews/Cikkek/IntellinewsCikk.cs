using Jelentesek;
using SegedFunkciok;
using Intellinews.Osszefoglalok;

using HtmlAgilityPack;
using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Intellinews.Cikkek
{
    public class IntellinewsCikk : Cikk<IntellinewsOsszefoglalo, IntellinewsCikkLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Intellinews", "Cikkek");

        public IntellinewsCikk()
        {
        }

        public IntellinewsCikk(HtmlNode node) : base(node)
        {
        }

        public IntellinewsCikk(MySqlDataReader reader) : base(reader)
        {
        }

        protected override void kepBeallitas(HtmlNode node)
        {
            HtmlNode kepNode = node.QuerySelector("img");
            if (kepNode == null)
            {
                Kep = null;
            }
            else
            {
                string kepLink = "https:" + HttpUtility.HtmlDecode(node.QuerySelector("img").Attributes["src"].Value);
                Kep = new IntellinewsCikkLetoltottKep(kepLink);
            }
        }

        protected override void focimBeallitasa(HtmlNode node)
        {
            HtmlNode cimNode = node.QuerySelector("h1.subtitle");
            Focim = cimNode.InnerText;
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
                Datum = new DateTime(ev, honap, nap);
            }
        }

        // A csv kiirasnal fontos hogy ne adattag legyen
        public static List<string> Honapok()
        {
            return new List<string>()
            {
                "Jan",
                "Feb",
                "Marc",
                "Apr",
                "Mai",
                "Jun",
                "Jul",
                "Aug",
                "Sept",
                "Oct",
                "Nov",
                "Dec"
            };
        }

        protected override void cikkReszekBeallitasa(HtmlNode node)
        {
            HtmlNode tagNode = node.QuerySelector("#node-eaopinion-full-group-keywords");
            List<HtmlNode> tagParagrafusok = tagNode.QuerySelectorAll("p, h3, .caseta-background-articol").ToList();
            HtmlNode kommentNode = node.QuerySelector(".comentarii, #comments");
            List<HtmlNode> kommentParagrafusok = kommentNode.QuerySelectorAll("p, h3, .caseta-background-articol").ToList();
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
    }
}
