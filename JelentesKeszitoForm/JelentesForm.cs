using AdatbazisFunkciok;
using Adevarul.Cikkek;
using Euractiv.Cikkek;
using Euractiv.Osszefoglalok;
using Intellinews.Cikkek;
using Jelentesek;
using SegedFunkciok;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JelentesKeszitoForm
{
    public partial class JelentesForm : Form
    {
        public JelentesForm()
        {
            InitializeComponent();
            websiteComboBox.Items.AddRange(new string[]
            {
                "adevarul.ro",
                "euractive.ro",
                "intellinews.com"
            });
            websiteComboBox.SelectedIndex = 0;
            pathTextBox.Text = "D:\\Projektek\\Ceg\\Erno\\Projekt filok";
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            switch (websiteComboBox.SelectedIndex)
            {
                case 0:
                    Adatbazis.beallitas.Adatbazis = "adevarul";
                    AdevarulCikkJelentes adevarulJelentes = new AdevarulCikkJelentes(categoryComboBox.SelectedItem.ToString());
                    adevarulJelentes.Export();
                    break;
                case 1:
                    Adatbazis.beallitas.Adatbazis = "euractive";
                    EuractivCikkJelentes euractiveJelentes = new EuractivCikkJelentes(categoryComboBox.SelectedItem.ToString());
                    euractiveJelentes.Export();
                    break;
                case 2:
                    Adatbazis.beallitas.Adatbazis = "intellinews";
                    IntellinewsCikkJelentes intellinewsJelentes = new IntellinewsCikkJelentes(categoryComboBox.SelectedItem.ToString());
                    intellinewsJelentes.Export();
                    break;
                default:
                    MessageBox.Show("Must select a website.");
                    break;
            }
        }

        private void websiteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (websiteComboBox.SelectedIndex)
            {
                case 0:
                    Adatbazis.beallitas.Adatbazis = "adevarul";
                    break;
                case 1:
                    Adatbazis.beallitas.Adatbazis = "euroactive";
                    break;
                default:
                    MessageBox.Show("Must select a website.");
                    break;
            }
            categoryComboBox.Items.Clear();
            foreach (CikkKategoria kategoria in CikkKategoria.LekeresAdatbazisbol())
            {
                categoryComboBox.Items.Add(kategoria.Nev);
            }
            categoryComboBox.SelectedIndex = 0;
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            FajlKezelo.MAPPA_ELERESI_UTVONAL = pathTextBox.Text;
        }
    }
}
