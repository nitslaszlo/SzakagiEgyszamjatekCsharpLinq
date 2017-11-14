using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SzakagiEgyszamjatekCsharpLinq
{
    class Játékos
    {
        public string Név { get; private set; }
        public List<int> Tippek { get; private set; }

        public Játékos(string[] m)
        {
            Név = m[m.Length-1];
            Tippek = new List<int>();
            foreach (var i in m.Take(m.Length-1)) Tippek.Add(int.Parse(i));
        }
    }

    class egyszamjatek
    {
        static void Main(string[] args)
        {
            List<Játékos> t = new List<Játékos>();
            foreach (var i in File.ReadAllLines("egyszamjatek.txt")) t.Add(new Játékos(i.Split()));

            Console.WriteLine($"3. feladat: Játékosok száma: {t.Count}");

            Console.WriteLine($"4. feladat: Fordulók száma: {t[0].Tippek.Count}");

            Console.WriteLine($"5. feladat: Az első fordulóban {(t.Count(i => i.Tippek[0] == 1) > 0 ? "" : "nem ")}volt egyes tipp!");

            Console.WriteLine($"6. feladat: A legnagyobb tipp a fordulók során: {t.Max(i => i.Tippek.Max())}");

            Console.Write($"7. feladat: Kérem a forduló sorszámát [1-{t[0].Tippek.Count}]: ");
            int fordulóSorszáma = int.Parse(Console.ReadLine());
            if (fordulóSorszáma < 1 || fordulóSorszáma > t[0].Tippek.Count) fordulóSorszáma = 1;

            var egyediTipp = t.GroupBy(g => g.Tippek[fordulóSorszáma - 1]).Where(i => i.Count() == 1).OrderBy(i => i.Key).FirstOrDefault();
            if (egyediTipp != null) Console.WriteLine($"8. feladat: A nyertes tipp a megadott fordulóban: {egyediTipp.Key}");
            else Console.WriteLine($"8. feladat: Nem volt egyedi tipp a megadott fordulóban!");

            string fordulóNyertese = " ";
            if (egyediTipp != null)
            {
                fordulóNyertese = t.Where(i => i.Tippek[fordulóSorszáma - 1] == egyediTipp.Key).First().Név;
                Console.WriteLine($"9. feladat: A megadott forduló nyertese: {fordulóNyertese}");
            }
            else Console.WriteLine($"9. feladat: Nem volt nyertes a megadott fordulóban!");

            if (egyediTipp != null) //10. feladat
            {
                List<string> ki = new List<string>();
                ki.Add($"Forduló sorszáma: {fordulóSorszáma}.");
                ki.Add($"Nyertes tipp: {egyediTipp.Key}");
                ki.Add($"Nyertes játékos: {fordulóNyertese}");
                File.WriteAllLines("nyertes.txt", ki);
            }
            Console.ReadKey();
        }
    }
}