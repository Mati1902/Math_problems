using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miejsce_Zerowe_2
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0;            
            double y = Math.Atan(Math.Log(Math.Pow(x, 3) + 1) - 3); //wprowadzenie równania
            double a_range = 0; //zakres sprawdzania (od 0)
            double b_range = 10; //zakres sprawdzania (do 10)
            double epsilon = Math.Abs(a_range - b_range); //epsilon potrzebny do sprawdzania warunku w pętli while
            double epsilon_min = 0.000000000000001; //dokładność
            double x1;
            double x2;
            double y1;
            double y2;
            double a;
            double b;
            double x0;
            double f1;
            double ex_x0 = b_range;
            int iteracje = 0;
            //ścieżka do pliku txt
            string execPath = Assembly.GetEntryAssembly().Location;
            int iloscznakowwsciezce = execPath.Length;
            string nowasciezka = execPath.Remove(iloscznakowwsciezce - 20);
            string docelowasciezka = nowasciezka + @"wyniki.txt";
            File.WriteAllText(docelowasciezka, String.Empty);
            //plik zapisywany do lokalizacji w której uruchamiany był plik exe. domyślnie jest to Miejsce_zerowe_1/bin/Debug

            x1 = a_range;
            y1 = f(x1); //wartość funkcji w punkcie x1 (który jest początkiem zakresu)
            x2 = b_range;
            y2 = f(x2); //wartość funkcji w punkcie x2 (który jest końcem zakresu)

            if (f(a_range) > 0 && f(b_range) < 0 || f(a_range) < 0 && f(b_range) > 0) //warunek rozpoczęcia pętli. rozpocznie się jeśli wartość funkcji dla końca zakresu jest odwrotna niż dla początku (jedna musi być ujemna, druga dodatnia)
            {
                while(epsilon > epsilon_min)
                {
                    iteracje++;
                    a = wsp_a(x1,y1,x2,y2); //obliczanie współczynnika 'a' dla prostej zawierającej punkty P1(x1,y1) oraz P2(x2,y2)
                    b = wsp_b(x1, y1, x2, y2); //obliczanie współczynnika 'b' dla prostej zawierającej punkty P1(x1,y1) oraz P2(x2,y2)
                    x0 = msc_0(a, b); //miejsce zerowe dla prostej o współczynnikach 'a' oraz 'b'
                    f1 = f(x0); //wartość funkcji w punkcie x0, a więc w x które jest miejscem zerowym funkcji liniowej
                    Console.WriteLine(iteracje); //wydruk wyników
                    Console.WriteLine("x= " + x2);
                    Console.WriteLine("y= " + y2);
                    Console.WriteLine("------------");
                    if (f1 < 0 && y2 < 0 || f1 > 0 && y2 > 0)
                    {
                        //punkt x0,f1 staje sie nowym punktem końcowym funkcji
                        x2 = x0;
                        y2 = f1;
                    }
                    else if (f1 < 0 && y2 > 0 || f1 > 0 && y2 < 0)
                    {
                        //punkt x0,f1 staje się nowym punktem początkowym funkcji
                        x1 = x0;
                        y1 = f1;
                    }                   
                    epsilon = Math.Abs(ex_x0 - x0); //modyfikacja epsilona
                    ex_x0 = x0;
                    File.AppendAllText(docelowasciezka, iteracje + "-" + x0 + "-"  + f1 + " " + "\r\n"); //zapis do pliku
                }
            }
            else
            {
                Console.WriteLine("Dla zakresu od " + a_range + " do " + b_range + " nie można ustalić miejsca zerowego tą metodą.");
            }
            Console.WriteLine("Ilość iteracji: " + iteracje); //wydruk ilości iteracji
            Console.ReadKey();
        }

        public static double f(double x) //metoda zwracająca wartość funkcji w danym punkcie
        {
            double y;
            y = Math.Atan(Math.Log(Math.Pow(x, 3) + 1) - 3);
            return y;
        }

        public static double wsp_a(double x1, double y1, double x2, double y2) //metoda obliczająca współczynnik 'a' funkcji liniowej na podstawie współrzędnych dwóch punktów
        {
            double a;
            a = (y2 - y1) / (x2 - x1);
            return a;
        }

        public static double wsp_b(double x1, double y1, double x2, double y2) //metoda obliczająca współczynnik 'b' funkcji liniowej na podstawie współrzędnych dwóch punktów
        {
            double b;
            b = (y2*x1 - y1*x2) / (x1 - x2);
            return b;
        }

        public static double msc_0(double a, double b) //metoda obliczająca miejsce zerowe funkcji liniowej na podstawie współczynników 'a' i 'b'
        {
            double x0;
            x0 = -(b/a);
            return x0;
        }
    }
}
