using System;
using System.Collections.Generic;

class Program {
   // ispisuje DIMACS zaglavlje
   static void Zaglavlje(int BrojPromenljivih, int BrojKlauzula) {
      Console.WriteLine("p cnf {0} {1}", BrojPromenljivih, BrojKlauzula);
   }

   // ispisuje klauzulu kao red u formatu DIMACS
   static void Klauzula(int[] promenljive) {
      foreach(int p in promenljive)
         Console.Write(p + " ");
      Console.WriteLine(0);
   }
                

   // kodira se uslov da je tacno jedna od promenljivih iz datog niza tacna
   static void TacnoJedna(int[] promenljive) {
       Klauzula(promenljive);
       int[] kl = new int[2];
       for (int i = 0; i < promenljive.Length; i++)
           for (int j = i + 1; j < promenljive.Length; j++) {
               kl[0] = -promenljive[i];
               kl[1] = -promenljive[j];
               Klauzula(kl);
           }
   }

   // trojke (i, j, v) se kodiraju brojevima između 1 i 729
   static int P(int i, int j, int v) {
       return (i-1) + 9*(j-1) + 81*(v-1) + 1;
   }

   static void Main() {
       // ucitavamo vrednosti na zadatim poljima
       List<int> zadate = new List<int>();
       string linija;
       while ((linija = Console.ReadLine()) != null) {
           string[] delovi = linija.Split();
           int i = int.Parse(delovi[0]);
           int j = int.Parse(delovi[1]);
           int v = int.Parse(delovi[2]);
           zadate.Add(P(i, j, v));
       }

       // stampamo DIMACS zaglavlje
       Zaglavlje(729, 4*81*37 + zadate.Count);
   
       // jedinstvenost vrednosti na svakom polju
       int[] promenljive = new int[9];
       for (int i = 1; i <= 9; i++)
          for (int j = 1; j <= 9; j++) {
              for (int v = 1; v <= 9; v++)
                 promenljive[v-1] = P(i, j, v);
              TacnoJedna(promenljive);
          }
       
       // jedinstvenost vrednosti u svakoj vrsti
       for (int i = 1; i <= 9; i++)
          for (int v = 1; v <= 9; v++) {
              for (int j = 1; j <= 9; j++)
                 promenljive[j-1] = P(i, j, v);
              TacnoJedna(promenljive);
          }

       // jedinstvenost vrednosti u svakoj koloni
       for (int j = 1; j <= 9; j++)
          for (int v = 1; v <= 9; v++) {
              for (int i = 1; i <= 9; i++)
                 promenljive[i-1] = P(i, j, v);
              TacnoJedna(promenljive);
          }
       
       // jedinstvenost vrednosti u svakom kvadratu 3x3
       for (int k = 0; k < 3; k++)
          for (int l = 0; l < 3; l++)
             for (int v = 1; v <= 9; v++) {
                 for (int a = 1; a <= 3; a++)
                    for (int b = 1; b <= 3; b++)
                        promenljive[3*(a-1)+(b-1)] = P(3*k+a, 3*l+b, v);
                 TacnoJedna(promenljive);
             }

       // ispisujemo zadate promenljive
       foreach (int p in zadate) {
           int[] kl = {p};
           Klauzula(kl);
       }
   }
}
