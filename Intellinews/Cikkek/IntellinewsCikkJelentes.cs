using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Intellinews.Cikkek
{
    public class IntellinewsCikkJelentes : CikkJelentes<IntellinewsCikk>
    {
        public CikkKategoria Kategoria;

        public IntellinewsCikkJelentes(string kategoriaNev) : this(new CikkKategoria(kategoriaNev))
        {
        }

        public IntellinewsCikkJelentes(CikkKategoria kategoria)
        {
            Kategoria = kategoria;
            ExprotalandoFajlEleresiUtvonal = System.IO.Path.Combine(IntellinewsCikk.MAPPA_ELERESI_UTVONAL, "Sajto jelentes.pdf");
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(IntellinewsCikk.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }

        protected override string query(string tablaNev, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            return "SELECT * FROM " + tablaNev + " WHERE " + Kategoria.QueryFeltetel + " AND date BETWEEN \"" + kezdetiDatum.ToString(datumFormatum) + "\" AND \"" + vegsoDatum.ToString(datumFormatum) + "\"";
        }
    }
}
