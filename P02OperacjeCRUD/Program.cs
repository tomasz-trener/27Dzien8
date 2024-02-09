using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02OperacjeCRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModelBazyDataContext db = new ModelBazyDataContext();

            // Dodanie danych do bazy 

            Zawodnik nowy = new Zawodnik()
            {
                imie = "adam",
                nazwisko = "nowak",
                waga = 70,
                wzrost = 180,
                kraj = "pol",
                data_ur = new DateTime(2000, 2, 4)
            };


            db.Zawodnik.InsertOnSubmit(nowy); // przygtowanie do wyslania do bazy 

            Zawodnik nowy2 = new Zawodnik()
            {
                imie = "Jakub",
                nazwisko = "nowak",
                waga = 70,
                wzrost = 180,
                kraj = "pol",
                data_ur = new DateTime(2000, 2, 4)
            };

            db.Zawodnik.InsertOnSubmit(nowy2); // przygtowanie do wyslania do bazy 

            //db.Zawodnik.InsertAllOnSubmit(); // tutaj moge przekazac kolekcje zawodnikow na raz 

            db.SubmitChanges(); // w tym momnecie zawodnicy sa dodani do bazy 

            // UWAGA! Żeby działało dodawanie zawodników używajac linq konieznie mus byc zdefiniowany klucz głowny w bazie 


        }
    }
}
