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

using Adevarul.Cikkek;
using AdatbazisFunkciok;
using Euractiv.Cikkek;
using Intellinews.Cikkek;

namespace AdatBanyaszForm
{
    public partial class BanyaszForm : System.Windows.Forms.Form
    {
        public BanyaszForm()
        {
            InitializeComponent();
            websiteComboBox.Items.AddRange(new string[]
            {
                "adevarul.ro",
                "euractive.ro",
                "intellinews.com"
            });
            websiteComboBox.SelectedIndex = 0;
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
            switch (websiteComboBox.SelectedIndex)
            {
                case 0:
                    Adatbazis.beallitas.Adatbazis = "adevarul";
                    AdevarulCikkAdatKezelo adevarulAdatKezelo = new AdevarulCikkAdatKezelo();
                    adevarulAdatKezelo.KigyujtesTarolasKategoriakkalAdatbazisba("adevarul.ro", int.Parse(pageCountTextBox.Text));
                    break;
                case 1:
                    Adatbazis.beallitas.Adatbazis = "euroactive";
                    EuractivCikkAdatKezelo euroactiveAdatKezelo = new EuractivCikkAdatKezelo();
                    euroactiveAdatKezelo.KigyujtesTarolasKategoriakkalAdatbazisba("euroactiv.ro", int.Parse(pageCountTextBox.Text));
                    break;
                case 2:
                    Adatbazis.beallitas.Adatbazis = "intellinews";
                    IntellinewsCikkAdatKezelo intellinewsAdatKezelo = new IntellinewsCikkAdatKezelo();
                    intellinewsAdatKezelo.KigyujtesTarolasKategoriakkalAdatbazisba("intellinews.com", int.Parse(pageCountTextBox.Text));
                    break;
                default:
                    MessageBox.Show("Must select a website.");
                    break;
            }
        }

        private void BanyaszForm_Load(object sender, EventArgs e)
        {

        }
    }
}
