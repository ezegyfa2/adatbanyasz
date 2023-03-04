using AdatbazisFunkciok;
using Adevarul.Cikkek;
using Adevarul.Osszefoglalok;
using Jelentesek;
using System;
using System.Web;

namespace Adevarul
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
            //adevarulCikkKigyujtes();
            adevarulCikkJelentes();
            //adevarulOsszefoglaloKigyujtes();
            //adevarulOssefoglaloJelentes();
        }

        static void adevarulCikkKigyujtes()
        {
            AdevarulCikkAdatKezelo adatKezelo = new AdevarulCikkAdatKezelo();
            adatKezelo.TobbOldalasKigyujtesTarolasAdatbazisba("adevarul.ro/economie", 3);
        }

        static void adevarulCikkJelentes()
        {
            AdevarulCikkJelentes jelentes = new AdevarulCikkJelentes("economie");
            jelentes.Export();
        }

        static void adevarulOsszefoglaloKigyujtes()
        {
            AdevarulOsszefoglaloAdatKezelo adatKezelo = new AdevarulOsszefoglaloAdatKezelo();
            adatKezelo.KigyujtesTarolasAdatbazisba("adevarul.ro/economie");
        }

        static void adevarulOssefoglaloJelentes()
        {
            AdevarulOsszefoglaloJelentes jelentes = new AdevarulOsszefoglaloJelentes();
            jelentes.Export();
        }
    }
}