using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Financialintelligence.Cikkek
{
    public class FinancialintelligenceCikkJelentes : CikkJelentes<FinancialintelligenceCikk>
    {
        public CikkKategoria Kategoria;

        public FinancialintelligenceCikkJelentes(string kategoriaNev) : this(new CikkKategoria(kategoriaNev))
        {
        }

        public FinancialintelligenceCikkJelentes(CikkKategoria kategoria)
        {
            Kategoria = kategoria;
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(FinancialintelligenceCikk.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }

        protected override string query(string tablaNev, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            return "SELECT * FROM " + tablaNev + " WHERE " + Kategoria.QueryFeltetel + " AND date BETWEEN \"" + kezdetiDatum.ToString(datumFormatum) + "\" AND \"" + vegsoDatum.ToString(datumFormatum) + "\"";
        }
    }
}
