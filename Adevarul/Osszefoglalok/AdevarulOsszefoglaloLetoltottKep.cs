using HtmlAdatKigyujtes;

using System.IO;

namespace Adevarul.Osszefoglalok
{
    public class AdevarulOsszefoglaloLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(AdevarulOsszefoglalo.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public AdevarulOsszefoglaloLetoltottKep(int id) : base(id)
        {
        }

        public AdevarulOsszefoglaloLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public AdevarulOsszefoglaloLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }
    }
}
