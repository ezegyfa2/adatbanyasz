using HtmlAdatKigyujtes;

using System.IO;

namespace Euractiv.Osszefoglalok
{
    public class EuractivOsszefoglaloLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(EuractivOsszefoglalo.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public EuractivOsszefoglaloLetoltottKep(int id) : base(id)
        {
        }

        public EuractivOsszefoglaloLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public EuractivOsszefoglaloLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }

        protected override string getExtension(string link)
        {
            string[] linkReszek = link.Split('&');
            foreach (string linkResz in linkReszek)
            {
                if (linkResz.Contains("f="))
                {
                    return linkResz.Replace("amp;", "").Replace("f=", "");
                }
            }
            throw new System.Exception("Invalid link");
        }
    }
}
