using AdatbazisFunkciok;
using FormosWeboldalForraskodKigyujtes;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Jelentesek
{
    public class CikkOsszefoglaloAdatKezelo<KigyujtendoAdatTipus> : AdatKezelo<KigyujtendoAdatTipus> where KigyujtendoAdatTipus : TarolhatoAdat
    {
        protected string taroloSelector;
        protected string cikkSelector;

        public override KigyujtendoAdatTipus EgyszeriKigyujtes(string url)
        {
            throw new NotImplementedException();
        }

        public override List<KigyujtendoAdatTipus> Kigyujtes(string url)
        {
            List<HtmlNode> cikkNodek = CikkTaroloKigyujtes(url).QuerySelectorAll(cikkSelector).ToList();
            return cikkNodek.Select(kigyujtottNode => (KigyujtendoAdatTipus)Activator.CreateInstance(typeof(KigyujtendoAdatTipus), kigyujtottNode)).ToList();
        }

        public virtual HtmlNode CikkTaroloKigyujtes(string url)
        {
            HtmlNode cikkNode = Kigyujto.StatikusKigyujtes(url);
            List<HtmlNode> taroloNodek = cikkNode.QuerySelectorAll(taroloSelector).ToList();
            return taroloNodek.First();
        }
    }
}
