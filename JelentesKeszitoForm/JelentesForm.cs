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
using System.IO;
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
            pathTextBox.Text = "D:\\Projects\\Catalyst";
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string kivalasztottKategoria = categoryComboBox.SelectedItem.ToString();
            switch (websiteComboBox.SelectedIndex)
            {
                case 0:
                    Adatbazis.beallitas.Adatbazis = "adevarul";
                    AdevarulCikkJelentes adevarulJelentes;
                    if (kivalasztottKategoria == "All")
                    {
                        adevarulJelentes = new AdevarulCikkJelentes();
                    }
                    else
                    {
                        adevarulJelentes = new AdevarulCikkJelentes(categoryComboBox.SelectedItem.ToString());
                    }
                    //adevarulJelentes.Export();


                    List<CikkKategoria> kategoriak = CikkKategoria.LekeresAdatbazisbol();
                    foreach (CikkKategoria kategoria in kategoriak) {
                        adevarulJelentes = new AdevarulCikkJelentes(kategoria);
                        adevarulJelentes.Export(kategoria.Nev + ".pdf", false);
                    }
                    break;
                case 1:
                    Adatbazis.beallitas.Adatbazis = "euroactive";
                    EuractivCikkJelentes euroactiveJelentes;
                    if (kivalasztottKategoria == "All")
                    {
                        euroactiveJelentes = new EuractivCikkJelentes();
                    }
                    else
                    {
                        euroactiveJelentes = new EuractivCikkJelentes(categoryComboBox.SelectedItem.ToString());
                    }
                    //adevarulJelentes.Export();

                    List<CikkKategoria> euroactiveKategoriak = CikkKategoria.LekeresAdatbazisbol();
                    foreach (CikkKategoria kategoria in euroactiveKategoriak)
                    {
                        euroactiveJelentes = new EuractivCikkJelentes(kategoria);
                        euroactiveJelentes.Export(Path.Combine(pathTextBox.Text, kategoria.Nev + ".pdf"), false);
                    }
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
            categoryComboBox.Items.Add("All");
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
