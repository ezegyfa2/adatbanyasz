using Euractiv.Osszefoglalok;
using FormosWeboldalForraskodKigyujtes;

using HtmlAgilityPack;
using Jelentesek;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace Euractiv.Cikkek
{
    public class EuractivCikkAdatKezelo : TobbOldalasAdatKezelo<EuractivCikk>
    {
        public override List<EuractivCikk> Kigyujtes(string url)
        {
            EuractivOsszefoglaloAdatKezelo cikkOsszefoglaloAdatKezelo = new EuractivOsszefoglaloAdatKezelo();
            List<EuractivOsszefoglalo> cikkOsszefoglalok = cikkOsszefoglaloAdatKezelo.Kigyujtes(url);
            List<EuractivCikk> cikkek = new List<EuractivCikk>();
            foreach (EuractivOsszefoglalo cikkOsszefoglalo in cikkOsszefoglalok)
            {
                EuractivCikk cikk = EgyszeriKigyujtes(cikkOsszefoglalo.Link);
                cikk.Osszefoglalo = cikkOsszefoglalo;
                cikk.Kategoria = KategoriaKigyujtes(url);
                cikkek.Add(cikk);
            }
            return cikkek;
        }

        public override EuractivCikk EgyszeriKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url, false).QuerySelector(".article");
            return new EuractivCikk(cikkNode);
        }

        public CikkKategoria KategoriaKigyujtes(string url)
        {
            string[] urlReszek = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return new CikkKategoria(urlReszek[urlReszek.Length - 2]);
        }

        public override string UrlOldalSzammal(string url, int oldalSzam)
        {
            return url + "/pagina-" + oldalSzam;
        }

        public override string[] KategoriakLinkReszek
        {
            get
            {
                return new string[]
                {
                    "news",
                    "politic-intern",
                    "justitie",

                };
            }
        }
    }
}
