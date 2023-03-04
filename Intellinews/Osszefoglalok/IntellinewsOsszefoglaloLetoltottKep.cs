using HtmlAdatKigyujtes;

using System.IO;

namespace Intellinews.Osszefoglalok
{
    public class IntellinewsOsszefoglaloLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(IntellinewsOsszefoglalo.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public IntellinewsOsszefoglaloLetoltottKep(int id) : base(id)
        {
        }

        public IntellinewsOsszefoglaloLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public IntellinewsOsszefoglaloLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }
    }
}
