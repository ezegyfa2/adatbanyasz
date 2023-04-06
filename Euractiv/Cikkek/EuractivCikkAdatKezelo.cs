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
                if (cikkOsszefoglalo.Datum.Year == 1000)
                {
                    cikkOsszefoglalo.Datum = cikk.Datum;
                }
                cikk.Osszefoglalo = cikkOsszefoglalo;
                cikk.Kategoria = KategoriaKigyujtes(url);
                cikkek.Add(cikk);
            }
            return cikkek;
        }

        public override EuractivCikk EgyszeriKigyujtes(string url)
        {
            //www.euractiv.ro/justitie/gabriel-oprea-achitat-in-prima-instanta-in-dosarul-privind-decesul-politistului-gigina-32964
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url, false).QuerySelector(".article");
            return new EuractivCikk(cikkNode);
        }

        public CikkKategoria KategoriaKigyujtes(string url)
        {
            string[] urlReszek = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return new CikkKategoria(urlReszek[urlReszek.Length - 1]);
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
                    "economic",
                    "social",
                    "eu-elections-2019",
                    "extern",
                    "virtuality",
                    "espresso",
                    "coronavirus-covid19",
                    "opinie",
                    "prezidentiale"
                };
            }
        }
    }
}
