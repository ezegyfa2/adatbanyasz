using Jelentesek;
using SegedFunkciok;
using Financialintelligence.Osszefoglalok;

using HtmlAgilityPack;
using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Financialintelligence.Cikkek
{
    public class FinancialintelligenceCikk : Cikk<FinancialintelligenceOsszefoglalo, FinancialintelligenceCikkLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "Intellinews", "Cikkek");

        public FinancialintelligenceCikk()
        {
        }

        public FinancialintelligenceCikk(HtmlNode node) : base(node)
        {
        }

        public FinancialintelligenceCikk(MySqlDataReader reader) : base(reader)
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
                string kepLink = HttpUtility.HtmlDecode(kepNode.Attributes["src"].Value);
                Kep = new FinancialintelligenceCikkLetoltottKep(kepLink);
            }
        }

        protected override void focimBeallitasa(HtmlNode node)
        {
            HtmlNode cimNode = node.QuerySelector("header > h1");
            Focim = cimNode.InnerText;
            Focim = HttpUtility.HtmlDecode(Focim);
        }

        protected override void datumBeallitasa(HtmlNode node)
        {
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("header").QuerySelector("time").InnerText);
            string[] datumSzovegReszek = datumSzoveg.Trim().Split('.');
            int ev = Int32.Parse(datumSzovegReszek[2]);
            int honap = Int32.Parse(datumSzovegReszek[1]);
            int nap = Int32.Parse(datumSzovegReszek[0]);
            Datum = new DateTime(ev, honap, nap);
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

        protected override void cikkReszekBeallitasa(HtmlNode node)
        {
            HtmlNode contentNode = node.QuerySelector(".entry-content");
            HtmlNode socialPanelNode = node.QuerySelector(".swp_social_panel");
            List<HtmlNode> socialPanelSzovegek = socialPanelNode.QuerySelectorAll("p").ToList();
            List<HtmlNode> cimNodek = contentNode.QuerySelectorAll("strong").ToList();
            List<HtmlNode> cikkReszNodek = contentNode.QuerySelectorAll("p, h3").ToList();
            int pozicio = 0;
            foreach (HtmlNode cikkReszNode in cikkReszNodek)
            {
                if (cikkReszNode.Name == "p")
                {
                    if (cimNode(cikkReszNode, cimNodek))
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
                else
                {
                    throw new Exception("Invalid node type");
                }
                ++pozicio;
            }
        }

        protected bool cimNode(HtmlNode cikkReszNode, List<HtmlNode> cimNodek)
        {
            foreach (HtmlNode cimNode in cimNodek) 
            { 
                if (cimNode.InnerText == cikkReszNode.InnerText) 
                { 
                    return true; 
                }
            }
            return false;
        }
    }
}
