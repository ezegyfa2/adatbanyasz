using AdatbazisFunkciok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    abstract public class TobbOldalasAdatKezelo<KigyujtendoAdatTipus> : AdatKezelo<KigyujtendoAdatTipus> where KigyujtendoAdatTipus : TarolhatoAdat
    {
        public List<KigyujtendoAdatTipus> KigyujtesTarolasKategoriakkalAdatbazisba(string url, int oldalSzam = 1)
        {
            List<KigyujtendoAdatTipus> kigyujtottAdatok = new List<KigyujtendoAdatTipus>();
            foreach (string kategoriaLinkResz in KategoriakLinkReszek)
            {
                kigyujtottAdatok.AddRange(TobbOldalasKigyujtesTarolasAdatbazisba(url + '/' + kategoriaLinkResz));
            }
            return kigyujtottAdatok;
        }

        public List<KigyujtendoAdatTipus> TobbOldalasKigyujtesTarolasAdatbazisba(string url, int oldalSzam = 1)
        {
            List<KigyujtendoAdatTipus> kigyujtottAdatok = new List<KigyujtendoAdatTipus>();
            kigyujtottAdatok.AddRange(KigyujtesTarolasAdatbazisba(url));
            for (int i = 2; i <= oldalSzam; ++i)
            {
                kigyujtottAdatok.AddRange(KigyujtesTarolasAdatbazisba(UrlOldalSzammal(url, i)));
            }
            return kigyujtottAdatok;
        }

        abstract public string UrlOldalSzammal(string url, int oldalSzam);
        abstract public string[] KategoriakLinkReszek { get; }
    }
}
