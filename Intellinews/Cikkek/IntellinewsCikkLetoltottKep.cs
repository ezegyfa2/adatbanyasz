using HtmlAdatKigyujtes;

using System.IO;

namespace Intellinews.Cikkek
{
    public class IntellinewsCikkLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(IntellinewsCikk.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public IntellinewsCikkLetoltottKep(int id) : base(id)
        {
        }

        public IntellinewsCikkLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public IntellinewsCikkLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
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
