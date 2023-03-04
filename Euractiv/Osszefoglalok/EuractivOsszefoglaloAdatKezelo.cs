using FormosWeboldalForraskodKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Euractiv.Osszefoglalok
{
    public class EuractivOsszefoglaloAdatKezelo : CikkOsszefoglaloAdatKezelo<EuractivOsszefoglalo>
    {
        public EuractivOsszefoglaloAdatKezelo()
        {
            taroloSelector = "#section-content";
            cikkSelector = ".teaser";
        }
    }
}
