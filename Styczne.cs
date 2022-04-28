using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miejsce_Zerowe_3
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0;
            double y = Math.Atan(Math.Log(Math.Pow(x, 3) + 1) - 3); //wprowadzenie równania
            double a_range = 1.396; //zakres sprawdzania (od 1.396)
            double b_range = 10; //zakres sprawdzania (do 10)
            double x1;
            double y1;
            double x2;
            double y2;
            double a;
            double b;
            double x0 = b_range;
            double ex_x0 = a_range;
            double epsilon = x0 - ex_x0; //epsilon potrzebny do sprawdzania warunku w pętli while
            double epsilon_min = 0.0000000000000001; //dokładność
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

            if (y1 > 0 && y2 < 0 || y1 < 0 && y2 > 0)
            {               
                while (epsilon > epsilon_min)
                {
                    iteracje++;
                    Console.WriteLine("x1= " + x1);
                    Console.WriteLine("y1= " + y1);
                    a = poch_y(x1); //obliczanie współczynnika 'a' funkcji liniowej (stycznej)
                    b = wsp_b(a, x1, y1); //obliczanie współczynnika 'a' funkcji liniowej (stycznej)
                    Console.WriteLine("------------");
                    x0 = msc_0(a, b); //obliczanie wartości x dla której wartość funkcji liniowej (stycznej) wynosi 0
                    x1 = x0;
                    y1 = f(x1); //obliczanie wartości funkcji pierwotnej w punkcie 'x' będącym miejscem zerowym funkcji liniowej (stycznej)
                    epsilon = Math.Abs(x0 - ex_x0); //modyfikacja epsilona
                    ex_x0 = x0;
                    File.AppendAllText(docelowasciezka, iteracje + "-" + x1 + "-" + y1 + " " + "\r\n"); //zapis do pliku
                }
            }
            else
            {
                Console.WriteLine("Dla zakresu od " + a_range + " do " + b_range + " nie można ustalić miejsca zerowego tą metodą.");
            }
            Console.WriteLine("Iteracje: " + iteracje); //wydruk ilości iteracji
            Console.ReadKey();
        }

        public static double f(double x) //zwraca wartość funkcji w punkcie x
        {
            double y;
            y = Math.Atan(Math.Log(Math.Pow(x, 3) + 1) - 3);            
            return y;
        }

        public static double poch_y(double x) //zwraca współczynnik "a" funkcji liniowej będącej styczną do funkcji
        {
            double a = (3*Math.Pow(x,2))/((Math.Pow(x,3) + 1)*(Math.Pow((Math.Log(Math.Pow(x,3) + 1) - 3),2) + 1)); //pochodna funkcji pierwotnej
            return a;
        }

        public static double wsp_b(double a, double x, double y) //zwraca współczynnik "b" funkcji liniowej będącej styczną do funkcji na podstawie współczynnika 'a' oraz jednego punktu należącego do tej stycznej
        {
            double b;
            if (x == 0)
            {
                b = y;
            }
            else
            {
                b = y - (a * x);
            }               
            return b;
        }

        public static double msc_0(double a, double b) //zwraca miejsce zerowe funkcji liniowej (stycznej)
        {
            double x0;
            x0 = -(b / a);
            return x0;
        }
    }
}
