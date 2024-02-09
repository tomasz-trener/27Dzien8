
using P03Zawodnicy.Shared.Domain;
using P03Zawodnicy.Shared.Services;
using P06Zawodnicy.Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P02ZawodnicyNoweOkna
{
    public partial class FrmWyszukiwarka : Form
    {
        IManagerZawodnikow mz;
        public FrmWyszukiwarka(IManagerZawodnikow mz)

        {
            this.mz = mz;
            InitializeComponent();
        }

        private void btnSzukaj_Click(object sender, EventArgs e)
        {
            List<Osoba> wynik = mz.WyszukajOsoby(txtSzukaj.Text);

            generujKontrolkiDlaOsob(wynik);
        }

        private void generujKontrolkiDlaOsob(List<Osoba> wynik)
        {
            pnlWyniki.Controls.Clear();
            int yOffset = 0;

            foreach (Osoba osoba in wynik)
            {
                Label lblImie = new Label()
                {
                    Text = $"Imie: {osoba.Imie}",
                    Location = new Point(10, yOffset)
                };
                pnlWyniki.Controls.Add(lblImie);

                Label lblNazwisko = new Label()
                {
                    Text = $"Nazwisko: {osoba.Nazwisko}",
                    Location = new Point(10, yOffset + 20)
                };
                pnlWyniki.Controls.Add(lblNazwisko);

                Label lbldataUr = new Label()
                {
                    Text = $"Data ur: {osoba.DataUrodzenia}",
                    Location = new Point(10, yOffset + 40)
                };

                pnlWyniki.Controls.Add(lbldataUr);
                yOffset += 70;
            }
        }
    }
}
