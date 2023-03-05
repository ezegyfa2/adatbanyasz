using Euractiv.Cikkek;
using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Euractiv.Osszefoglalok
{
    public class EuractivOsszefoglaloJelentes : CikkJelentes<EuractivOsszefoglalo>
    {
        public EuractivOsszefoglaloJelentes()
        {
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(EuractivOsszefoglalo.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }
    }
}
