using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Adevarul.Cikkek
{
    public class AdevarulCikkJelentes : Jelentes<AdevarulCikk>
    {
        public CikkKategoria Kategoria;

        public AdevarulCikkJelentes(string kategoriaNev)
        {
            Kategoria = new CikkKategoria(kategoriaNev);
        }

        public AdevarulCikkJelentes(CikkKategoria kategoria)
        {
            Kategoria = kategoria;
        }

        public override string ExportFejlecKepEleresiUtvonal
        {
            get
            {
                return System.IO.Path.Combine(AdevarulCikk.MAPPA_ELERESI_UTVONAL, "JelentesKepek", "Logo.png");
            }
        }

        public override void Export(List<AdevarulCikk> exportalandoAdatok, DateTime kezdetiDatum, DateTime vegsoDatum, bool megnyitasExportUtan = true)
        {
            string exprotalandoFajlEleresiUtvonal = System.IO.Path.Combine(AdevarulCikk.MAPPA_ELERESI_UTVONAL, "Sajto jelentes.pdf");
            Document exportalandoDokumentum = new Document();
            exportalandoDokumentum.AddSection();
            exportalandoDokumentum.Sections[0].PageSetup.HeaderDistance = 10;
            exportalandoDokumentum.Sections[0].PageSetup.TopMargin = 120;
            exportStilusokBeallitasa(exportalandoDokumentum);
            exportFejlecBeallitasa(exportalandoDokumentum);
            exportCimBeallitasa(exportalandoDokumentum, kezdetiDatum, vegsoDatum);
            ExportLinkekBeallitasa(exportalandoDokumentum, exportalandoAdatok.Select(adat => (ExportalhatoAdat)adat).ToList());
            ExportalhatoAdat.ExportUjSor(exportalandoDokumentum);
            ExportalhatoAdat.ExportUjSor(exportalandoDokumentum);
            HirOsszefoglalokExportalasa(exportalandoDokumentum, exportalandoAdatok);
            MentesPDFkent(exportalandoDokumentum, exprotalandoFajlEleresiUtvonal, megnyitasExportUtan);
        }

        private void exportCimBeallitasa(Document exportalandoDokumentum, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            for (int i = 0; i < 4; ++i)
            {
                Paragraph ujSor = ExportalhatoAdat.ExportUjSor(exportalandoDokumentum);
                ujSor.Style = "focim";
            }
            FormazottSzovegExportalasa(exportalandoDokumentum, "HETI GAZDASÁG", "focim");
            FormazottSzovegExportalasa(exportalandoDokumentum, kezdetiDatum.ToShortDateString() + " - " + vegsoDatum.ToShortDateString(), "alcim");
            for (int i = 0; i < 4; ++i)
                ExportalhatoAdat.ExportUjSor(exportalandoDokumentum);
        }

        public void HirOsszefoglalokExportalasa(Document exportalandoDokumentum, List<AdevarulCikk> exportalandoAdatok)
        {
            foreach (AdevarulCikk exportalandoAdat in exportalandoAdatok)
            {
                exportalandoAdat.Export(exportalandoDokumentum);
            }
        }

        protected override string query(string tablaNev, DateTime kezdetiDatum, DateTime vegsoDatum)
        {
            return "SELECT * FROM " + tablaNev + " WHERE " + Kategoria.QueryFeltetel + " AND date BETWEEN \"" + kezdetiDatum.ToString(datumFormatum) + "\" AND \"" + vegsoDatum.ToString(datumFormatum) + "\"";
        }
    }
}
