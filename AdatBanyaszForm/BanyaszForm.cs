using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using AdatbazisFunkciok;
using Intellinews.Cikkek;
using SegedFunkciok;
using System.IO;
using Adevarul.Cikkek;

namespace AdatBanyaszForm
{
    public partial class BanyaszForm : System.Windows.Forms.Form
    {
        public BanyaszForm()
        {
            InitializeComponent();
            FajlKezelo.MAPPA_ELERESI_UTVONAL = "D:\\Projektek\\Ceg\\Erno\\Projekt filok";
        }

        private void pageCountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (pageCountTextBox.Text.Length != validPageCountText.Length)
            {
                pageCountTextBox.Text = validPageCountText;
            }
        }

        protected string validPageCountText
        {
            get
            {
                string pageCountText = "";
                foreach (char c in pageCountTextBox.Text)
                {
                    if (char.IsDigit(c))
                    {
                        pageCountText += c;
                    }
                }
                return pageCountText;
            }
        }

        private void collectButton_Click(object sender, EventArgs e)
        {
            Adatbazis.beallitas.Adatbazis = "adevarul";
            AdevarulCikkAdatKezelo aAdatKezelo = new AdevarulCikkAdatKezelo();
            aAdatKezelo.KigyujtesTarolasKategoriakkalAdatbazisba("adevarul.ro", int.Parse(pageCountTextBox.Text));
            //IntellinewsCikkAdatKezelo intellinewsAdatKezelo = new IntellinewsCikkAdatKezelo();
            //intellinewsAdatKezelo.KigyujtesTarolasKategoriakkalAdatbazisba("intellinews.com", int.Parse(pageCountTextBox.Text));
        }

        private void exportPathTextBox_TextChanged(object sender, EventArgs e)
        {
            FajlKezelo.MAPPA_ELERESI_UTVONAL = exportPathTextBox.Text;
        }
    }
}
