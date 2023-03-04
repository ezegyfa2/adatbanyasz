using AdatbazisFunkciok;
using Euractiv.Cikkek;
using Euractiv.Osszefoglalok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euractiv
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Adatbazis.Beallitas();
            cikkKigyujtes();
            //cikkJelentes();
            //osszefoglaloKigyujtes();
            //ossefoglaloJelentes();
        }

        static void cikkKigyujtes()
        {
            EuractivCikkAdatKezelo adatKezelo = new EuractivCikkAdatKezelo();
            adatKezelo.TobbOldalasKigyujtesTarolasAdatbazisba("euractiv.ro/news", 3);
        }

        static void osszefoglaloKigyujtes()
        {
            EuractivOsszefoglaloAdatKezelo adatKezelo = new EuractivOsszefoglaloAdatKezelo();
            adatKezelo.KigyujtesTarolasAdatbazisba("euractiv.ro/economic");
        }

        static void ossefoglaloJelentes()
        {
            EuractivOsszefoglaloJelentes jelentes = new EuractivOsszefoglaloJelentes();
            jelentes.Export();
        }

        static void cikkJelentes()
        {
            EuractivCikkJelentes jelentes = new EuractivCikkJelentes("news");
            jelentes.Export();
        }
    }
}
