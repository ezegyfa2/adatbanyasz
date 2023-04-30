using AdatbazisFunkciok;
using Adevarul.Cikkek;
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
        List<CikkKategoria> kategoriak;

        public JelentesForm()
        {
            InitializeComponent();
            Adatbazis.beallitas.Adatbazis = "adevarul";
            categoryComboBox.Items.Clear();
            categoryComboBox.Items.Add("All");
            kategoriak = CikkKategoria.LekeresAdatbazisbol();
            foreach (CikkKategoria kategoria in kategoriak)
            {
                categoryComboBox.Items.Add(kategoria.Nev);
            }
            pathTextBox.Text = "D:\\Projects\\Catalyst";
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string kivalasztottKategoria = categoryComboBox.SelectedItem.ToString();
            AdevarulCikkJelentes jelentes;
            if (kivalasztottKategoria == "All")
            {
                /*foreach (CikkKategoria kategoria in kategoriak)
                {
                    jelentes = new AdevarulCikkJelentes(kategoria);
                    jelentes.Export(kategoria.Nev + ".pdf", false);
                }*/
                jelentes = new AdevarulCikkJelentes(kategoriak[categoryComboBox.SelectedIndex]);
                jelentes.Export();
            }
            else
            {
                jelentes = new AdevarulCikkJelentes(kategoriak[categoryComboBox.SelectedIndex]);
                jelentes.Export();
            }
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            FajlKezelo.MAPPA_ELERESI_UTVONAL = pathTextBox.Text;
        }
    }
}
