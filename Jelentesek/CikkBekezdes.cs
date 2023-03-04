using AdatbazisFunkciok;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    public class CikkBekezdes : CikkResz
    {
        public CikkBekezdes(string szoveg, int pozicio, TarolhatoAdat cikk): base(szoveg, pozicio, cikk)
        {
        }

        public CikkBekezdes(int id) : base(id)
        {
        }

        public CikkBekezdes(MySqlDataReader reader) : base(reader)
        {
        }

        public override string TablaNev
        {
            get
            {
                return "article_paragraphs";
            }
        }
    }
}
