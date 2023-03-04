using AdatbazisFunkciok;

using PdfSharp.Pdf;
using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using PdfSharp.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MigraDoc.DocumentObjectModel;

namespace Jelentesek
{
    abstract public class ExportalhatoAdat : TarolhatoAdat
    {
        public ExportalhatoAdat(int id) : base(id)
        {
        }

        public ExportalhatoAdat(MySqlDataReader reader): base(reader)
        {
        }

        public ExportalhatoAdat() { }

        public void TorlesAdatbazisbol()
        {
            Adatbazis.Torles(TablaNev, AdatbazisOszlopNevek, AdatbazisOszlopErtekek);
        }

        abstract public string Konyvjelzo { get; }
        public virtual void Export(Document szerkesztendoDokumentum) { }

        public static Paragraph ExportUjSor(Document szerkesztendoDokumentum)
        {
            return szerkesztendoDokumentum.Sections[0].AddParagraph();
        }
    }
}
