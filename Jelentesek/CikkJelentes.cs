using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    abstract public class CikkJelentes<HirTipus> : Jelentes<HirTipus> where HirTipus : ExportalhatoAdat, new()
    {
        public string ExprotalandoFajlEleresiUtvonal;

        public override void Export(List<HirTipus> exportalandoAdatok, DateTime kezdetiDatum, DateTime vegsoDatum, bool megnyitasExportUtan = true)
        {
            string exprotalandoFajlEleresiUtvonal = ExprotalandoFajlEleresiUtvonal;
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

        public void HirOsszefoglalokExportalasa(Document exportalandoDokumentum, List<HirTipus> exportalandoAdatok)
        {
            foreach (HirTipus exportalandoAdat in exportalandoAdatok)
            {
                exportalandoAdat.Export(exportalandoDokumentum);
            }
        }
    }
}
