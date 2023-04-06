using HtmlAdatKigyujtes;

using System.IO;

namespace Financialintelligence.Osszefoglalok
{
    public class FinancialintelligenceOsszefoglaloLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FinancialintelligenceOsszefoglalo.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public FinancialintelligenceOsszefoglaloLetoltottKep(int id) : base(id)
        {
        }

        public FinancialintelligenceOsszefoglaloLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public FinancialintelligenceOsszefoglaloLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }
    }
}
