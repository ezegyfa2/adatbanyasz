using AdatbazisFunkciok;
using HtmlAdatKigyujtes;
using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Jelentesek
{
    abstract public class CikkOsszefoglalo<LetoltottKepTipus> : ExportalhatoAdat, ILinkesAdat where LetoltottKepTipus : TarolhatoAdat
    {

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
        public string Cim;
        public string Szoveg;
        public string Link;
        public DateTime Datum;

        public override List<string> AdatbazisOszlopNevek
        {
            get
            {
                return new List<string>() {
                    "title",
                    "text",
                    "link",
                    "image_id",
                    "date"
                };
            }
        }

        public override List<string> AdatbazisOszlopErtekek
        {
            get
            {
                return new List<string>()
                {
                    Cim,
                    Szoveg,
                    Link,
                    null,
                    Datum.ToString("yyyy-MM-dd HH:mm:ss"),
                };
            }
        }

        protected override List<TarolhatoAdat> eloreTarolandoAdatok
        {
            get
            {
                return new List<TarolhatoAdat>()
                {
                    Kep
                };
            }
        }

        public override string Konyvjelzo
        {
            get
            {
                return Cim;
            }
        }

        // A Cikk-ben van ra szukseg
        public string GetLink
        {
            get { return Link; }
        }

        public CikkOsszefoglalo(HtmlNode node)
        {
            kepBeallitas(node);
            cimBeallitasa(node);
            szovegBeallitasa(node);
            linkBeallitasa(node);
            datumBeallitasa(node);
        }

        public CikkOsszefoglalo(int id) : base(id)
        {
        }

        public CikkOsszefoglalo() : base() 
        {
        }

        public CikkOsszefoglalo(MySqlDataReader reader) : base(reader)
        {
        }

        public override void AdatokBeallitasaReaderbol(MySqlDataReader reader)
        {
            try
            {
                Kep = (LetoltottKepTipus)Activator.CreateInstance(typeof(LetoltottKepTipus), reader.GetInt32("image_id"));
            }
            catch (Exception ex)
            {
                Kep = null;
            }
            Cim = reader.GetString("title");
            Szoveg = reader.GetString("text");
            Link = reader.GetString("link");
            Datum = DateTime.Parse(reader.GetString("date"));
        }

        abstract protected void kepBeallitas(HtmlNode node);
        abstract protected void cimBeallitasa(HtmlNode node);
        abstract protected void szovegBeallitasa(HtmlNode node);
        abstract protected void linkBeallitasa(HtmlNode node);
        abstract protected void datumBeallitasa(HtmlNode node);
    }
}
