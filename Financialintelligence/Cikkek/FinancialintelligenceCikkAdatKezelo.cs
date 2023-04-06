using Financialintelligence.Osszefoglalok;
using FormosWeboldalForraskodKigyujtes;

using HtmlAgilityPack;
using Jelentesek;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace Financialintelligence.Cikkek
{
    public class FinancialintelligenceCikkAdatKezelo : TobbOldalasAdatKezelo<FinancialintelligenceCikk>
    {
        public override List<FinancialintelligenceCikk> Kigyujtes(string url)
        {
            FinancialintelligenceOsszefoglaloAdatKezelo cikkOsszefoglaloAdatKezelo = new FinancialintelligenceOsszefoglaloAdatKezelo();
            List<FinancialintelligenceOsszefoglalo> cikkOsszefoglalok = cikkOsszefoglaloAdatKezelo.Kigyujtes(url);
            List<FinancialintelligenceCikk> cikkek = new List<FinancialintelligenceCikk>();
            foreach (FinancialintelligenceOsszefoglalo cikkOsszefoglalo in cikkOsszefoglalok)
            {
                FinancialintelligenceCikk cikk = EgyszeriKigyujtes(cikkOsszefoglalo.Link);
                cikk.Osszefoglalo = cikkOsszefoglalo;
                cikk.Kategoria = KategoriaKigyujtes(url);
                cikkek.Add(cikk);
            }
            return cikkek;
        }

        public override FinancialintelligenceCikk EgyszeriKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url, false).QuerySelector("article");
            return new FinancialintelligenceCikk(cikkNode);
        }

        public CikkKategoria KategoriaKigyujtes(string url)
        {
            string[] urlReszek = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return new CikkKategoria(urlReszek[urlReszek.Length - 1]);
        }

        public override string UrlOldalSzammal(string url, int oldalSzam)
        {
            return url + "/?offset=" + oldalSzam * 20;
        }

        public override string[] KategoriakLinkReszek
        {
            get
            {
                return new string[]
                {
                    "category/industrie"
                };
            }
        }
    }
}
