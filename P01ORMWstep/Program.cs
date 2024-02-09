using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Resolvers;

namespace P01ORMWstep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ORM - Object - Relation - Mapping 
            ModelBazyDataContext db = new ModelBazyDataContext();

            // select * from zawodnicy
            Zawodnik[] zawodnicy = db.Zawodnik.ToArray();

            //foreach (var z in zawodnicy)
            //{
            //    Console.WriteLine(z.imie + " " + z.nazwisko);
            //}
            //Console.ReadKey();


            // to polecenie wykonało się na bazie danych 
            Zawodnik[] wyn1 = db.Zawodnik.Where(x=> x.kraj == "pol").ToArray();

            //to się wykonało loklanie (wielkośc liter ma znazenie)
            Zawodnik[] wyn2 = zawodnicy.Where(x => x.kraj == "POL").ToArray();


            // LINQ loklanie 

            string[] napisy = { "BACHLEDA", "MATEJA", "HERR" };
            string[] wynik = napisy.Where(x => x.Length > 4).ToArray();

            //Console.WriteLine(string.Join(" ",wynik));

            int[] liczby = { 4, 6, 33, 2, 30, 20, 22 };
            int[] wynik2 =liczby.Where(x=>x > 20).OrderByDescending(x=>x).ToArray();


            var wyn3 = db.Zawodnik
                    .Where(x => x.kraj == "pol" || x.kraj.Equals("ger"))
                    .OrderByDescending(x => x.kraj)
                    .ThenBy(x => x.wzrost)
                    .ToArray();


            // znajdz zawodników, których nazwisko konczy się na litere a 
            // oraz wzrost jest ponad 2 razy wiekszy niz waga
            // urodzonych w II polowie roku, i posortouj po dlugosci imienia 

            //select* from zawodnicy
            //where right(nazwisko, 1) = 'a'
            //and wzrost > waga * 2 and month(data_ur) > 6
            //order by len(imie)

            //składnia używająca operatora lambda (składnia skrócona) zalecana 

            var wyn7 = db.Zawodnik
                .Where(x=>x.nazwisko.EndsWith("a") && x.wzrost > x.waga*2 && x.data_ur.Value.Month > 6)
                .OrderBy(x=>x.imie.Length)
                .ToArray();

            // składnia opodobna do SQL 

            var wyn8 = (from x in db.Zawodnik
                        where x.nazwisko.EndsWith("a")
                        && x.wzrost > 2 * x.waga 
                        && x.data_ur.Value.Month > 6
                        orderby x.imie.Length
                        select x
                        ).ToArray();


            //select * from zawodnicy 
            Zawodnik[] wyn9 = db.Zawodnik.ToArray();

            string[] wyn10 = db.Zawodnik.Select(x => x.imie).ToArray();

            //select imie + ' ' + nazwisko from zawodnicy 
            string[] wyn11 = db.Zawodnik.Select(x=>x.imie + " " + x.nazwisko).ToArray();

            //select imie, nazwisko from zawodnicy 

            ZawodnikMini[] wyn12 = db.Zawodnik.Select(x => new ZawodnikMini()
            {
                MojeImie = x.imie,
                MojeNazwisko = x.nazwisko
            }).ToArray();

            // jesli nie chce nam sie tworzyc nowych obiektów 
            var wyn13 = db.Zawodnik.Select(x => new
            {
                MojeImie = x.imie,
                MojaWaga = x.waga,
                x.kraj
            });

            // nie musimy podawac nazw kolun 
            var wyn14 = db.Zawodnik.Select(x=> new { x.imie, x.waga, x.kraj });

            //wypisanie danych 
            foreach (var z in wyn14)
                Console.WriteLine(z.imie + " " + z.waga + " "+ z.kraj);


            // wypisz listę zawodnikow (imie nazwisko i BMI) 
            // i posortuj wyniki po BMI malejąco 
            //bmi = waga[kg]/wzrost[m]^2
            // wynik bmi podaj do dwóch miejsc po przecinku 


            //select imie, nazwisko,
            //   format(waga / power((wzrost / 100.0), 2), '0.00') bmi
            //   from zawodnicy
            // order by bmi desc

            var wyn15 = db.Zawodnik
                .Where(x => x.waga.HasValue && x.wzrost.HasValue) // upewnienie sie ze waga i wzrost maja uzup. wartosci 
                .Select(x => new
                {
                    Imie = x.imie,
                    Nazwisko = x.nazwisko,
                    BMI = Math.Round((double)x.waga / Math.Pow((double)x.wzrost / 100, 2),2, MidpointRounding.AwayFromZero)
                })
                .OrderByDescending(x=>x.BMI).ToList();


            var wyn15b =
                (from x in db.Zawodnik
                 where x.waga.HasValue && x.wzrost.HasValue
                 //order by x.bmi // na tym etapie nie mam jeszcze dostepu do bmi
                 select new
                 {
                     Imie = x.imie,
                     Nazwisko = x.nazwisko,
                     BMI = Math.Round((double)x.waga / Math.Pow((double)x.wzrost / 100, 2), 2, MidpointRounding.AwayFromZero)
                 }
                 ).OrderBy(x => x.BMI).ToList();


           IQueryable<Zawodnik> wynik16 = db.Zawodnik.Where(x => x.kraj == "pol"); // to jeszcze nie zostało wysłane do bazy 

            var wynik17 = wynik16.Where(x => x.wzrost > 170).ToArray(); // dopiero teraz zapytanie poszło do bazy 

            // poznaliśmy: select, from, where, order by , group by 

            // grupowanie 


            // select kraj, avg(convert(decimal, wzrost))
            //from zawodnicy
            //group by kraj

            var wyn18 = db.Zawodnik
                .GroupBy(x => x.kraj)
                .Select(x => new
                {
                    Kraj = x.Key,
                    SredniWzrost = x.Average(y => y.wzrost)
                }).ToArray();

            var wyn19 = db.Zawodnik.GroupBy(x => x.kraj).ToArray(); // to zwraca nam grupy 

            string kraj1 = wyn19[0].Key; // kraj z pierwszej grupy 
            double? sredniWzrostZGrupy1 = wyn19.Select(x => x.Average(y => y.wzrost)).First();

            // wypisz wszystkie wartości długości nazwisk, wraz z informacją ile osób posiada
            // nazwisko o podanej długości 
            //np:
            // nazwisko o długości 5 ma 4 osoby
            // nazwisko o długości 7 ma 6 osoby
            // nazwisko o długości 6 ma 6 osoby
            //.... itd..
            // wyniki posortuj po liczbie osób w grupie rosnąco
            // , a jeżeli liczba osób jest taka sama to po długości nazwiska malejąco

            // * uwzgędnij tylko zawodników, których nazwisko nie zaczyna się na "a"
            // i wypisz tylko te grupy, które zawierają co najmniej 2 osoby 

            //select len(nazwisko) dlugosc, count(*) liczbaOsob
            //from zawodnicy
            //where left(nazwisko, 1) != 'a'
            //group by len(nazwisko)
            //having count(*) > 1
            //order by liczbaOsob, dlugosc desc

            var wyn20 = db.Zawodnik
                .Where(x => !x.nazwisko.StartsWith("a"))
                .GroupBy(x => x.nazwisko.Length)
                .Select(x => new
                {
                    DlugoscNazwiska = x.Key,
                    LiczbaOsob = x.Count(),
                    Srednia = x.Average(y => y.wzrost),
                    Max = x.Max(y => y.wzrost)
                })
                .Where(x => x.LiczbaOsob > 1)
                .OrderBy(x => x.LiczbaOsob)
                .ThenByDescending(x => x.DlugoscNazwiska)
                .ToArray();

            foreach (var g in wyn20)
                Console.WriteLine($"Nazwisko o dlugości {g.DlugoscNazwiska} ma {g.LiczbaOsob} osoby");

            Console.ReadKey();
        }
    }
}
