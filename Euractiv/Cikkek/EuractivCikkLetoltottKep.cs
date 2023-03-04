using HtmlAdatKigyujtes;

using System.IO;

namespace Euractiv.Cikkek
{
    public class EuractivCikkLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(EuractivCikk.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public EuractivCikkLetoltottKep(int id) : base(id)
        {
        }

        public EuractivCikkLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public EuractivCikkLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
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
