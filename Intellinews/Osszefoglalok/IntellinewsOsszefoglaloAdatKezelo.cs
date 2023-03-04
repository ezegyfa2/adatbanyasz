using FormosWeboldalForraskodKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intellinews.Osszefoglalok
{
    public class IntellinewsOsszefoglaloAdatKezelo : CikkOsszefoglaloAdatKezelo<IntellinewsOsszefoglalo>
    {
        public IntellinewsOsszefoglaloAdatKezelo()
        {
            taroloSelector = ".newsBlock";
            cikkSelector = ".newsArticle";
        }
    }
}
