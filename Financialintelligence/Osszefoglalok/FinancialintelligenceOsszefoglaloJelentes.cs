using Financialintelligence.Cikkek;
using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Financialintelligence.Osszefoglalok
{
    public class FinancialintelligenceOsszefoglaloJelentes : CikkJelentes<FinancialintelligenceOsszefoglalo>
    {
        public FinancialintelligenceOsszefoglaloJelentes()
        {
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(FinancialintelligenceOsszefoglalo.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }
    }
}
