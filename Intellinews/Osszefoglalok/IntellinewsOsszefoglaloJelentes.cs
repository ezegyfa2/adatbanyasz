using Intellinews.Cikkek;
using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Intellinews.Osszefoglalok
{
    public class IntellinewsOsszefoglaloJelentes : CikkJelentes<IntellinewsOsszefoglalo>
    {
        public IntellinewsOsszefoglaloJelentes()
        {
            ExprotalandoFajlEleresiUtvonal = System.IO.Path.Combine(IntellinewsOsszefoglalo.MAPPA_ELERESI_UTVONAL, "Sajto jelentes.pdf");
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(IntellinewsOsszefoglalo.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }
    }
}
