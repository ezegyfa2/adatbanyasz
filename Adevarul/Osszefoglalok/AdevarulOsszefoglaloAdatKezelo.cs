using FormosWeboldalForraskodKigyujtes;
using Jelentesek;

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adevarul.Osszefoglalok
{
    public class AdevarulOsszefoglaloAdatKezelo : CikkOsszefoglaloAdatKezelo<AdevarulOsszefoglalo>
    {
        public AdevarulOsszefoglaloAdatKezelo()
        {
            taroloSelector = ".flex-row.flex-grow0";
            cikkSelector = ".borderRight";
        }

        public override HtmlNode CikkTaroloKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url);
            string ultimeleStiriContainerNodeSelector = ".flex-row.flex-grow0";
            List<HtmlNode> taroloNodek = cikkNode.QuerySelectorAll(ultimeleStiriContainerNodeSelector).ToList();
            if (taroloNodek.Count == 4)
            {
                return taroloNodek[2];
            }
            else
            {
                return taroloNodek.Last();
            }
        }
    }
}
