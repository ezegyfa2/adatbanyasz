using HtmlAdatKigyujtes;

using System.IO;

namespace Adevarul.Cikkek
{
    public class AdevarulCikkLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(AdevarulCikk.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public AdevarulCikkLetoltottKep(int id) : base(id)
        {
        }

        public AdevarulCikkLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public AdevarulCikkLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }
    }
}
