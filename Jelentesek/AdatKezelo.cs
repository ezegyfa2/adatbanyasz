using AdatbazisFunkciok;
using System.Collections.Generic;
//using CsvFunkciok;

namespace Jelentesek
{
    abstract public class AdatKezelo<KigyujtendoAdatTipus> where KigyujtendoAdatTipus : TarolhatoAdat
    {
        public List<KigyujtendoAdatTipus> KigyujtesTarolasAdatbazisba(string url)
        {
            List<KigyujtendoAdatTipus> kigyujtottAdatok = Kigyujtes(url);
            foreach (KigyujtendoAdatTipus kigyujtottAdat in kigyujtottAdatok)
            {
                kigyujtottAdat.Tarolas();
            }
            return kigyujtottAdatok;
        }

        public KigyujtendoAdatTipus EgyszeriKigyujtesTarolasAdatbazisba(string url)
        {
            KigyujtendoAdatTipus kigyujtottAdat = EgyszeriKigyujtes(url);
            kigyujtottAdat.Tarolas();
            return kigyujtottAdat;
        }

        /*public void KigyujtesKiirasCSVbe(string url)
        {
            using (CsvKiiro kiiro = new CsvKiiro(@"D:\Projektek\AdatBanyaszat\kigyujtottAdatok.csv", false, ';'))
            {
                kiiro.KiirObjektumokHeaderSorral(Kigyujtes(url));
            }
        }*/

        abstract public List<KigyujtendoAdatTipus> Kigyujtes(string url);
        abstract public KigyujtendoAdatTipus EgyszeriKigyujtes(string url);
    }
}
