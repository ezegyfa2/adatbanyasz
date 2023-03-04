using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Euractiv.Cikkek
{
    public class EuractivCikkJelentes : CikkJelentes<EuractivCikk>
    {
        public CikkKategoria Kategoria;

        public EuractivCikkJelentes(string kategoriaNev) : this(new CikkKategoria(kategoriaNev))
        {
        }

        public EuractivCikkJelentes(CikkKategoria kategoria)
        {
            Kategoria = kategoria;
            ExprotalandoFajlEleresiUtvonal = System.IO.Path.Combine(EuractivCikk.MAPPA_ELERESI_UTVONAL, "Sajto jelentes.pdf");
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(EuractivCikk.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }

        protected override string query(string tablaNev, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            return "SELECT * FROM " + tablaNev + " WHERE " + Kategoria.QueryFeltetel + " AND date BETWEEN \"" + kezdetiDatum.ToString(datumFormatum) + "\" AND \"" + vegsoDatum.ToString(datumFormatum) + "\"";
        }
    }
}
