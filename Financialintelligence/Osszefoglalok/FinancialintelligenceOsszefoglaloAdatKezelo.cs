using FormosWeboldalForraskodKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Financialintelligence.Osszefoglalok
{
    public class FinancialintelligenceOsszefoglaloAdatKezelo : CikkOsszefoglaloAdatKezelo<FinancialintelligenceOsszefoglalo>
    {
        public FinancialintelligenceOsszefoglaloAdatKezelo()
        {
            taroloSelector = "#posts";
            cikkSelector = "article.post";
        }
    }
}
