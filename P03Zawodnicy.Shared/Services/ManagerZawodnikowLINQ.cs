using P03Zawodnicy.Shared.Domain;
using P03Zawodnicy.Shared.Services;
using P04Zawodnicy.Shared.Data;
using P06Zawodnicy.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04Zawodnicy.Shared.Services
{
    internal class ManagerZawodnikowLINQ : IManagerZawodnikow
    {
        public void Dodaj(Zawodnik z)
        {
            throw new NotImplementedException();
        }

        public void Edytuj(Zawodnik edytowany)
        {
            throw new NotImplementedException();
        }

        public string[] PodajKraje()
        {
            throw new NotImplementedException();
        }

        public int PodajSredniWiekZawodnikow(string kraj)
        {
            throw new NotImplementedException();
        }

        public double PodajSredniWzrost(string kraj)
        {
            throw new NotImplementedException();
        }

        public Trener[] PodajTrenerow()
        {
            throw new NotImplementedException();
        }

        public Zawodnik[] PodajZawodnikow(string kraj)
        {
            throw new NotImplementedException();
        }

        public void PosorotujZawodnikowPoNazwisku(Zawodnik[] posortowaniZawodnicy)
        {
            throw new NotImplementedException();
        }

        public void Usun(int id)
        {
            throw new NotImplementedException();
        }

        public Zawodnik[] WczytajZawodnikow()
        {
            var zawodnicyDb = new ModelBazyDataContext().ZawodnikDb.ToArray();
            return mapujZawodnikow(zawodnicyDb);
        }

        public List<Osoba> WyszukajOsoby(string fragmentNazwy)
        {
            throw new NotImplementedException();
        }

        private Zawodnik[] mapujZawodnikow(params ZawodnikDb[] dane)
        {
            Zawodnik[] tab = new Zawodnik[dane.Length];
            for (int i = 0; i < dane.Length; i++)
            {
                tab[i] = new Zawodnik()
                {
                    Id_zawodnika = dane[i].id_zawodnika,
                    Id_trenera = dane[i].id_trenera,
                    Imie = dane[i].imie,
                    Kraj = dane[i].kraj,
                    DataUrodzenia = dane[i].data_ur,
                    Wzrost = (int)dane[i].wzrost,
                    Waga = (int)dane[i].waga,
                };
            }
            return tab;
        }
    }
}
