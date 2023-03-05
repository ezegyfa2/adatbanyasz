using AdatbazisFunkciok;
using HtmlAgilityPack;
using MigraDoc.DocumentObjectModel;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jelentesek
{
    abstract public class Cikk<OsszefoglaloTipus, LetoltottKepTipus> : ExportalhatoAdat
        where OsszefoglaloTipus : TarolhatoAdat, ILinkesAdat
        where LetoltottKepTipus : TarolhatoAdat
    {
        public override string TablaNev
        {
            get
            {
                return "articles";
            }
        }
        public LetoltottKepTipus Kep;
        public string KepID
        {
            get
            {
                if (Kep == null)
                {
                    return "NULL";
                }
                else
                {
                    return Kep.ID.ToString();
                }
            }
        }
        public string Focim;
        public string Link
        {
            get
            {
                return Osszefoglalo.GetLink;
            }
        }
        public DateTime Datum;
        public OsszefoglaloTipus Osszefoglalo;
        public CikkKategoria Kategoria;
        public List<TarolhatoAdat> Bekezdesek = new List<TarolhatoAdat>();
        public List<TarolhatoAdat> Cimek = new List<TarolhatoAdat>();

        public override List<string> AdatbazisOszlopNevek
        {
            get
            {
                return new List<string>() {
                    "title",
                    "image_id",
                    "date",
                    "article_summary_id",
                    "article_category_id"
                };
            }
        }

        public override List<string> AdatbazisOszlopErtekek
        {
            get
            {
                return new List<string>()
                {
                    Focim,
                    KepID,
                    Datum.ToString("yyyy-MM-dd HH:mm:ss"),
                    Osszefoglalo.ID.ToString(),
                    Kategoria.ID.ToString()
                };
            }
        }

        protected override List<TarolhatoAdat> eloreTarolandoAdatok
        {
            get
            {
                return new List<TarolhatoAdat>()
                {
                    Kep,
                    Osszefoglalo,
                    Kategoria
                };
            }
        }

        public override string Konyvjelzo
        {
            get
            {
                return Focim;
            }
        }

        public Cikk() { }

        public Cikk(HtmlNode node)
        {
            kepBeallitas(node);
            focimBeallitasa(node);
            datumBeallitasa(node);
            cikkReszekBeallitasa(node);
        }

        public Cikk(MySqlDataReader reader) : base(reader)
        {
            string bekezdesQuery = Adatbazis.LekeresQuery(
                 "article_paragraphs",
                new List<string>() { "*" },
                new List<string>() { "article_id" },
                new List<string>() { reader.GetInt32("id").ToString() }
            );
            Bekezdesek = Adatbazis.ListaLekeres<CikkBekezdes>(bekezdesQuery).Select(bekezdes => (TarolhatoAdat)bekezdes).ToList();
            string cimQuery = Adatbazis.LekeresQuery(
                "article_titles",
                new List<string>() { "*" },
                new List<string>() { "article_id" },
                new List<string>() { ID.ToString() }
            );
            Cimek = Adatbazis.ListaLekeres<CikkCim>(cimQuery).Select(cim => (TarolhatoAdat)cim).ToList();
        }

        public override void AdatokBeallitasaReaderbol(MySqlDataReader reader)
        {
            kepBeallitasaReaderbol(reader);
            Focim = reader.GetString("title");
            Datum = DateTime.Parse(reader.GetString("date"));
            osszefoglaloBeallitasaReaderbol(reader);
            kategoriaBeallitasaReaderbol(reader);
        }

        protected void kepBeallitasaReaderbol(MySqlDataReader reader)
        {
            try
            {
                Kep = (LetoltottKepTipus)Activator.CreateInstance(typeof(LetoltottKepTipus), reader.GetInt32("image_id"));
            }
            catch (Exception e)
            {
                Kep = null;
            }
        }

        protected void osszefoglaloBeallitasaReaderbol(MySqlDataReader reader)
        {
            try
            {
                Osszefoglalo = (OsszefoglaloTipus)Activator.CreateInstance(typeof(OsszefoglaloTipus), reader.GetInt32("article_summary_id"));
            }
            catch (Exception e)
            {
                Osszefoglalo = null;
            }
        }

        protected void kategoriaBeallitasaReaderbol(MySqlDataReader reader)
        {
            try
            {
                Kategoria = new CikkKategoria(reader.GetInt32("article_category_id"));
            }
            catch (Exception e)
            {
                Kategoria = null;
            }
        }

        abstract protected void kepBeallitas(HtmlNode node);
        abstract protected void focimBeallitasa(HtmlNode node);
        abstract protected void datumBeallitasa(HtmlNode node);
        abstract protected void cikkReszekBeallitasa(HtmlNode node);

        public override void Export(Document szerkesztendoDokumentum)
        {
            Paragraph cimParagrafus = Jelentes<SzimplaExportalhatoAdat>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Focim, "cikkcim");
            cimParagrafus.AddBookmark(Konyvjelzo);
            Jelentes<SzimplaExportalhatoAdat>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Datum.ToString(), "cikkalcim");
            ExportUjSor(szerkesztendoDokumentum);
            ExportCikkReszek(szerkesztendoDokumentum);

            //Paragraph kepParagrafus = szerkesztendoDokumentum.Sections[0].AddParagraph();
            //kepParagrafus.Format.Alignment = ParagraphAlignment.Center;
            //Image kep = kepParagrafus.AddImage(Kep.EleresiUtvonal);
            ExportUjSor(szerkesztendoDokumentum);
            ExportUjSor(szerkesztendoDokumentum);
        }

        public void ExportCikkReszek(Document szerkesztendoDokumentum)
        {
            List<CikkResz> exportalandoCikkReszek = new List<CikkResz>();
            exportalandoCikkReszek.AddRange(Bekezdesek.Select(bekezdes => (CikkResz)bekezdes));
            exportalandoCikkReszek.AddRange(Cimek.Select(cim => (CikkResz)cim));
            int pozicio = 0;
            while (exportalandoCikkReszek.Count > 0)
            {
                CikkResz aktualisCikkResz = CikkReszPozicioAlapjan(exportalandoCikkReszek, pozicio);
                aktualisCikkResz.Export(szerkesztendoDokumentum);
                ExportUjSor(szerkesztendoDokumentum);
                exportalandoCikkReszek.Remove(aktualisCikkResz);
                ++pozicio;
            }
        }

        public static CikkResz CikkReszPozicioAlapjan(List<CikkResz> cikkReszek, int pozicio)
        {
            foreach (CikkResz cikkResz in cikkReszek)
            {
                if (cikkResz.Pozicio == pozicio)
                {
                    return cikkResz;
                }
            }
            throw new Exception("Hibas cikk reszek");
        }

        public override int Tarolas()
        {
            int cikkId = base.Tarolas();
            if (cikkId == -1)
            {
                throw new Exception("Hibas tarolas");
            }
            else
            {
                foreach (CikkBekezdes bekezdes in Bekezdesek)
                {
                    int bekezdesId = bekezdes.Tarolas();
                    if (bekezdesId == -1)
                    {
                        bekezdes.Szoveg = "";
                        if (bekezdes.Tarolas() == -1)
                        {
                            throw new Exception("Nem lehet feltolteni a bekezdest");
                        }
                    }
                }
                foreach (CikkCim cim in Cimek)
                {
                    int cimId = cim.Tarolas();
                    if (cimId == -1)
                    {
                        cim.Szoveg = "";
                        if (cim.Tarolas() == -1)
                        {
                            throw new Exception("Nem lehet feltolteni a cimet");
                        }
                        
                    }
                }
            }
            return cikkId;
        }
    }
}
