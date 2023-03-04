
using Google.Protobuf.WellKnownTypes;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Org.BouncyCastle.Asn1.Pkcs;
using PdfSharp.Drawing;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Jelentesek
{
    public class JelentesOldal
    {
        public string Cim;
        public string Szoveg;
        public string KepEleresiUtvonal;

        public JelentesOldal(string cim, string szoveg, string kepEleresiUtvonal)
        {
            Cim = cim;
            Szoveg = szoveg;
            KepEleresiUtvonal = kepEleresiUtvonal;
        }

        public void Export(Document szerkesztendoDokumentum)
        {
            
        }

        public void CimExportalas()
        {
            
        }
    }
}
