using AdatbazisFunkciok;
using Jelentesek;
using MigraDoc.DocumentObjectModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    abstract public class CikkResz: TarolhatoAdat
    {
        public string Szoveg;
        public int Pozicio;
        public TarolhatoAdat Cikk;

        public CikkResz(string szoveg, int pozicio, TarolhatoAdat cikk)
        {
            Szoveg = szoveg;
            Pozicio = pozicio;
            Cikk = cikk;
        }

        public CikkResz(int id) : base(id)
        {
        }

        public CikkResz(MySqlDataReader reader) : base(reader)
        {
        }

        public override void AdatokBeallitasaReaderbol(MySqlDataReader reader)
        {
            Szoveg = reader.GetString("text");
            Pozicio = reader.GetInt32("position");
        }

        public override List<string> AdatbazisOszlopNevek
        {
            get
            {
                return new List<string>()
                {
                    "text",
                    "position",
                    "article_id"
                };
            }
        }

        public override List<string> AdatbazisOszlopErtekek
        {
            get
            {
                return new List<string>()
                {
                    Szoveg,
                    Pozicio.ToString(),
                    Cikk.ID.ToString()
                };
            }
        }

        public Paragraph Export(Document szerkesztendoDokumentum)
        {
            return Jelentes<SzimplaExportalhatoAdat>.FormazottSzovegExportalasa(szerkesztendoDokumentum, Szoveg, StilusNev);
        }

        public virtual string StilusNev
        {
            get
            {
                return "leiras";
            }
        }
    }
}
