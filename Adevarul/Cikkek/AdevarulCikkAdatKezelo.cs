using Adevarul.Osszefoglalok;
using FormosWeboldalForraskodKigyujtes;

using HtmlAgilityPack;
using Jelentesek;

using System;
using System.Collections.Generic;

namespace Adevarul.Cikkek
{
    public class AdevarulCikkAdatKezelo : TobbOldalasAdatKezelo<AdevarulCikk>
    {
        public override List<AdevarulCikk> Kigyujtes(string url)
        {
            AdevarulOsszefoglaloAdatKezelo cikkOsszefoglaloAdatKezelo = new AdevarulOsszefoglaloAdatKezelo();
            List<AdevarulOsszefoglalo> cikkOsszefoglalok = cikkOsszefoglaloAdatKezelo.Kigyujtes(url);
            List<AdevarulCikk> cikkek = new List<AdevarulCikk>();
            foreach (AdevarulOsszefoglalo cikkOsszefoglalo in cikkOsszefoglalok)
            {
                AdevarulCikk cikk = EgyszeriKigyujtes(cikkOsszefoglalo.Link);
                cikk.Osszefoglalo = cikkOsszefoglalo;
                cikk.Kategoria = KategoriaKigyujtes(url);
                cikkek.Add(cikk);
            }
            return cikkek;
        }

        public override AdevarulCikk EgyszeriKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url, false).QuerySelector("article");
            return new AdevarulCikk(cikkNode);
        }

        public CikkKategoria KategoriaKigyujtes(string url)
        {
            string[] urlReszek = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            CikkKategoria elozoKategoria = null;
            CikkKategoria kategoria = null;
            for (int i = 1; i < urlReszek.Length; i++)
            {
                if (!urlReszek[i].Contains(".html"))
                {
                    kategoria = new CikkKategoria(urlReszek[i]);
                    if (i > 1)
                    {
                        kategoria.FoKategoria = elozoKategoria;
                    }
                    kategoria.Tarolas();
                    elozoKategoria = kategoria;
                }
            }
            return kategoria;
        }

        public override string UrlOldalSzammal(string url, int oldalSzam)
        {
            return url + "/" + oldalSzam + ".html";
        }

        public override string[] KategoriakLinkReszek
        {
            get
            {
                return new string[]
                {
                    "stiri-interne/evenimente",
                    "stiri-interne/societate",
                    "stiri-interne/sanatate",
                    "stiri-interne/educatie",
                    "stiri-interne/meteo",
                    "stiri-locale/alba-iulia",
                    "stiri-locale/arad",
                    "stiri-locale/pitesti",
                    "stiri-locale/bacau",
                    "stiri-locale/oradea",
                    "stiri-locale/bistrita",
                    "stiri-locale/botosani",
                    "stiri-locale/brasov",
                    "stiri-locale/braila",
                    "stiri-locale/bucuresti",
                    "stiri-locale/buzau",
                    "stiri-locale/resita",
                    "stiri-locale/cluj-napoca",
                    "stiri-locale/constanta",
                    "stiri-locale/calarasi",
                    "stiri-locale/craiova",
                    "stiri-locale/targoviste",
                    "stiri-locale/galati",
                    "stiri-locale/giurgiu",
                    "stiri-locale/targu-jiu",
                    "stiri-locale/hunedoara",
                    "stiri-locale/slobozia",
                    "stiri-locale/iasi",
                    "stiri-locale/baia-mare",
                    "stiri-locale/turnu-severin",
                    "stiri-locale/targu-mures",
                    "stiri-locale/piatra-neamt",
                    "stiri-locale/slatina",
                    "stiri-locale/ploiesti",
                    "stiri-locale/satu-mare",
                    "stiri-locale/sibiu",
                    "stiri-locale/suceava",
                    "stiri-locale/zalau/",
                    "stiri-locale/alexandria",
                    "stiri-locale/timisoara",
                    "stiri-locale/tulcea",
                    "stiri-locale/vaslui",
                    "stiri-locale/focsani",
                    "stiri-locale/ramnicu-valcea",
                    "stiri-externe/republica-moldova",
                    "stiri-externe/europa",
                    "stiri-externe/rusia",
                    "stiri-externe/china",
                    "stiri-externe/sua",
                    "stiri-externe/in-lume",
                    "stiri-externe/lumea-vazuta-din-bucuresti/",
                    "politica",
                    "economie",
                    "sport",
                    "stil-de-viata/viata-sanatoasa",
                    "stil-de-viata/viata-de-cuplu",
                    "stil-de-viata/tehnologie",
                    "stil-de-viata/calatorii",
                    "stil-de-viata/bucatarie",
                    "stil-de-viata/horoscop",
                    "stil-de-viata/cultura",
                    "stil-de-viata/stiinta",
                    "stil-de-viata/magazin",
                    "stil-de-viata/auto",
                    "showbiz/vedete",
                    "showbiz/muzica",
                    "showbiz/film",
                    "showbiz/tv"
                };
            }
        }
    }
}
