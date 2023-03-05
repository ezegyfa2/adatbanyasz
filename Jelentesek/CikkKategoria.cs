using AdatbazisFunkciok;
using MySql.Data.MySqlClient;
using SegedFunkciok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    public class CikkKategoria : TarolhatoAdat
    {
        public string Nev;
        public CikkKategoria FoKategoria;

        public override string TablaNev
        {
            get
            {
                return "article_categories";
            }
        }

        public override List<string> AdatbazisOszlopNevek
        {
            get
            {
                return new List<string>()
                {
                    "name",
                    "parent_category_id"
                };
            }
        }

        public override List<string> AdatbazisOszlopErtekek
        {
            get
            {
                return new List<string>()
                {
                    Nev,
                    FokategoriaID
                };
            }
        }

        public string FokategoriaID
        {
            get
            {
                if (FoKategoria == null)
                {
                    return "NULL";
                }
                else
                {
                    return FoKategoria.ID.ToString();
                }
            }
        }

        public CikkKategoria(string nev)
        {
            Nev = nev;
        }

        public CikkKategoria(int id) : base(id)
        {
        }

        public CikkKategoria(MySqlDataReader beolvaso) : base(beolvaso)
        {
        }

        public override void AdatokBeallitasaReaderbol(MySqlDataReader beolvaso)
        {
            Nev = beolvaso.GetString("name");
            if (!beolvaso.IsDBNull(1))
            {
                FoKategoria = new CikkKategoria(beolvaso.GetInt32("parent_category_id"));
            }
        }

        protected override List<TarolhatoAdat> eloreTarolandoAdatok
        {
            get
            {
                if (FoKategoria == null)
                {
                    return new List<TarolhatoAdat>();
                }
                else
                {
                    return new List<TarolhatoAdat>() { FoKategoria };
                }
            }
        }

        public string QueryFeltetel
        {
            get
            {
                List<CikkKategoria> feltetelKategoriak = Adatbazis.ListaLekeres<CikkKategoria>("SELECT * FROM article_categories WHERE parent_category_id = " + ID);
                feltetelKategoriak.Add(this);
                List<string> queryFeltetelek = feltetelKategoriak.Select(alkategoria => alkategoria.SajatQueryFeltetel).ToList();
                return "(" + StringFuggvenyek.Osszefuzes(queryFeltetelek, " OR ") + ")";
            }
        }

        public string SajatQueryFeltetel
        {
            get
            {
                return " article_category_id = " + ID;
            }
        }

        public static List<CikkKategoria> LekeresAdatbazisbol()
        {
            string lekeresQuery = "SELECT name, parent_category_id FROM article_categories";
            MySqlConnection adatbazisKapcsolat = Adatbazis.KapcsolatKeszites();
            MySqlDataReader beolvaso = (new MySqlCommand(lekeresQuery, adatbazisKapcsolat)).ExecuteReader();
            List<CikkKategoria> kategoriak = new List<CikkKategoria>();
            while (beolvaso.Read())
                kategoriak.Add((CikkKategoria)Activator.CreateInstance(typeof(CikkKategoria), beolvaso));
            beolvaso.Close();
            adatbazisKapcsolat.Close();
            return kategoriak;
        }
    }
}
