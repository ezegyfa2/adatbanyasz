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
using System.Text.RegularExpressions;

namespace Financialintelligence.Cikkek
{
    public class FinancialintelligenceCikk : Cikk<FinancialintelligenceOsszefoglalo, FinancialintelligenceCikkLetoltottKep>
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FajlKezelo.MAPPA_ELERESI_UTVONAL, "FinancialIntelligence", "Cikkek");

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
            string datumSzoveg = HttpUtility.HtmlDecode(node.QuerySelector("header").QuerySelector("time").Attributes["datetime"].Value);
            string[] datumSzovegReszek = datumSzoveg.Trim().Split('T')[0].Split('-');
            int ev = Int32.Parse(datumSzovegReszek[0]);
            int honap = Int32.Parse(datumSzovegReszek[1]);
            int nap = Int32.Parse(datumSzovegReszek[2]);
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
            HtmlNode cikkNode = node.QuerySelector("article > .entry-content");
            HtmlNode socialPanelNode = cikkNode.QuerySelector(".swp_social_panel");
            string cikkSzoveg = cikkNode.InnerText;
            cikkSzoveg = cikkSzoveg.Replace(socialPanelNode.InnerText, "");
            cikkSzoveg = Regex.Replace(cikkSzoveg, "<!--[^<]*-->", "");

            List<string> cimek = cikkNode.QuerySelectorAll("strong, h3").Select(cimNode => cimNode.InnerText).ToList();
            if (cimek.Count == 0)
            {
                string bekezdesSzoveg = HttpUtility.HtmlDecode(cikkSzoveg);
                Bekezdesek.Add(new CikkBekezdes(bekezdesSzoveg, 0, this));
            }
            else
            {
                int pozicio = 0;
                foreach (string cim in cimek)
                {
                    int cimPozicio = cikkSzoveg.IndexOf(cim);
                    if (cimPozicio == -1)
                    {
                        throw new Exception("Hibas cim");
                    }
                    else
                    {
                        if (cimPozicio > 0)
                        {
                            string bekezdesSzoveg = HttpUtility.HtmlDecode(cikkSzoveg.Substring(0, cimPozicio));
                            Bekezdesek.Add(new CikkBekezdes(bekezdesSzoveg, pozicio, this));
                            ++pozicio;
                        }
                        Cimek.Add(new CikkCim(HttpUtility.HtmlDecode(cim), pozicio, this));
                        ++pozicio;
                        cikkSzoveg = cikkSzoveg.Substring(cimPozicio + cim.Length);
                    }
                }
            }
        }
    }
}
