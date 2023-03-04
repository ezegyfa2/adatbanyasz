using AdatbazisFunkciok;
using CsvFunkciok;

namespace CikkKigyujtes
{
    abstract public class AdatKezelo<KigyujtendoAdatTipus> where KigyujtendoAdatTipus : TarolhatoAdat
    {
        public void KigyujtesTarolasAdatbazisba(string url)
        {
            foreach (KigyujtendoAdatTipus kigyujtottAdat in Kigyujtes(url))
            {
                kigyujtottAdat.Tarolas();
            }
        }

        public void KigyujtesKiirasCSVbe(string url)
        {
            using (CsvKiiro kiiro = new CsvKiiro(@"D:\Projektek\AdatBanyaszat\kigyujtottAdatok.csv", false, ';'))
            {
                kiiro.KiirObjektumokHeaderSorral(Kigyujtes(url));
            }
        }

        abstract public List<KigyujtendoAdatTipus> Kigyujtes(string url);
    }
}
