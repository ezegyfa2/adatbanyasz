using HtmlAdatKigyujtes;

using System.IO;

namespace Financialintelligence.Cikkek
{
    public class FinancialintelligenceCikkLetoltottKep : LetoltottKep
    {
        public static readonly string MAPPA_ELERESI_UTVONAL = Path.Combine(FinancialintelligenceCikk.MAPPA_ELERESI_UTVONAL, "LetoltottKepek");

        public FinancialintelligenceCikkLetoltottKep(int id) : base(id)
        {
        }

        public FinancialintelligenceCikkLetoltottKep(string link): base(MAPPA_ELERESI_UTVONAL, link)
        {
        }

        public FinancialintelligenceCikkLetoltottKep(string fileNev, string link) : base(MAPPA_ELERESI_UTVONAL, fileNev, link)
        {
        }
    }
}
