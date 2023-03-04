using Intellinews.Osszefoglalok;
using FormosWeboldalForraskodKigyujtes;

using HtmlAgilityPack;
using Jelentesek;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;

namespace Intellinews.Cikkek
{
    public class IntellinewsCikkAdatKezelo : TobbOldalasAdatKezelo<IntellinewsCikk>
    {
        public override List<IntellinewsCikk> Kigyujtes(string url)
        {
            IntellinewsOsszefoglaloAdatKezelo cikkOsszefoglaloAdatKezelo = new IntellinewsOsszefoglaloAdatKezelo();
            List<IntellinewsOsszefoglalo> cikkOsszefoglalok = cikkOsszefoglaloAdatKezelo.Kigyujtes(url);
            List<IntellinewsCikk> cikkek = new List<IntellinewsCikk>();
            foreach (IntellinewsOsszefoglalo cikkOsszefoglalo in cikkOsszefoglalok)
            {
                IntellinewsCikk cikk = EgyszeriKigyujtes(cikkOsszefoglalo.Link);
                cikk.Osszefoglalo = cikkOsszefoglalo;
                cikk.Kategoria = KategoriaKigyujtes(url);
                cikkek.Add(cikk);
            }
            return cikkek;
        }

        public override IntellinewsCikk EgyszeriKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url, false).QuerySelector(".article");
            return new IntellinewsCikk(cikkNode);
        }

        public CikkKategoria KategoriaKigyujtes(string url)
        {
            string[] urlReszek = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return new CikkKategoria(urlReszek[urlReszek.Length - 2]);
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
                    "romania"
                };
            }
        }
    }
}
