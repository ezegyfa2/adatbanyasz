using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jelentesek
{
    internal class SzimplaExportalhatoAdat : ExportalhatoAdat
    {
        public override string Konyvjelzo => throw new NotImplementedException();

        public override string TablaNev => throw new NotImplementedException();

        public override List<string> AdatbazisOszlopNevek => throw new NotImplementedException();

        public override List<string> AdatbazisOszlopErtekek => throw new NotImplementedException();

        public override void AdatokBeallitasaReaderbol(MySqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
