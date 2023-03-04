using AdatbazisFunkciok;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    public class CikkCim: CikkResz
    {
        public CikkCim(string szoveg, int pozicio, TarolhatoAdat cikk): base(szoveg, pozicio, cikk)
        {
        }

        public CikkCim(int id) : base(id)
        {
        }

        public CikkCim(MySqlDataReader reader) : base(reader)
        {
        }

        public override string TablaNev
        {
            get
            {
                return "article_titles";
            }
        }

        public override string StilusNev
        {
            get
            {
                return "cikkalcim";
            }
        }
    }
}
