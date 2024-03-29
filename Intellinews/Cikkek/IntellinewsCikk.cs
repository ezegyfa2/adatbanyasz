﻿using Jelentesek;
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
            string datumSzoveg = HttpUtility.HtmlDecode(datumNode(node).InnerText);
            string[] datumSzovegReszek = datumSzoveg.Trim().Split(',');
            if (datumSzovegReszek.Length == 1)
            {
                Datum = DateTime.ParseExact(datumSzoveg.Trim(), "H:m", null);
            }
            else
            {
                string honapSzoveg = datumSzovegReszek[0].Split(' ')[0];
                int ev = Int32.Parse(datumSzovegReszek[1]);
                int honap = Honapok().FindIndex(aktHonap => aktHonap == honapSzoveg);
                int nap = Int32.Parse(datumSzovegReszek[0].Split(' ')[1]);
                Datum = new DateTime(ev, honap + 1, nap);
            }
        }

        protected HtmlNode datumNode(HtmlNode node)
        {
            List<HtmlNode> kivalasztottNodek = node.QuerySelectorAll("span.date").ToList();
            foreach (HtmlNode kivalasztottNode in kivalasztottNodek)
            {
                if (kivalasztottNode.ParentNode.QuerySelectorAll("span").Count() == 2)
                {
                    return kivalasztottNode;
                }
            }
            throw new Exception("Nincs datum node");
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
            HtmlNode contentNode = node.QuerySelector(".searchHighlight");
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
