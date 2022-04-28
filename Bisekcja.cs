using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miejsce_Zerowe_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0;
            //double y = Math.Atan(Math.Log(Math.Pow(x,3)+1)-3); //wprowadzenie równania
            double y = Math.Cosh(x) - Math.Log(19 * Math.Sqrt(x));
            double a_range = 0; //zakres sprawdzania (od 0)
            double b_range = 0.46; //zakres sprawdzania (do 10)
            double epsilon = Math.Abs(a_range-b_range) / 2; //epsilon potrzebny do sprawdzania warunku w pętli while
            double epsilon_min = 0.000000000000001; //dokładność
            double y1 = Math.Pow(Math.E, (-Math.Atan(a_range))) - a_range;
            double y2 = Math.Pow(Math.E, (-Math.Atan(b_range))) - b_range;
            int iteracje = 0;
            //ścieżka do pliku txt
            string execPath = Assembly.GetEntryAssembly().Location;
            int iloscznakowwsciezce = execPath.Length;
            string nowasciezka = execPath.Remove(iloscznakowwsciezce - 20);
            string docelowasciezka = nowasciezka + @"wyniki.txt";           
            File.WriteAllText(docelowasciezka, String.Empty);
            //plik zapisywany do lokalizacji w której uruchamiany był plik exe. domyślnie jest to Miejsce_zerowe_1/bin/Debug

            //if (y1 < 0 && y > 0 || y1 > 0 && y < 0) //warunek rozpoczęcia pętli. rozpocznie się jeśli wartość funkcji dla końca zakresu jest odwrotna niż dla początku (jedna musi być ujemna, druga dodatnia)
            //{
                while (epsilon > epsilon_min)
                {
                    iteracje++;

                    //y1 = Math.Atan(Math.Log(Math.Pow(a_range, 3) + 1) - 3); //wartość funkcji dla x - początek zakresu
                    //y2 = Math.Atan(Math.Log(Math.Pow(b_range, 3) + 1) - 3); //wartość funkcji dla x - koniec zakresu
                    y1 = Math.Cosh(a_range) - Math.Log(18.9 * Math.Sqrt(a_range));
                    y2 = Math.Cosh(b_range) - Math.Log(18.9 * Math.Sqrt(b_range));
                    x = (b_range + a_range) / 2; //nowy x będący po środku, pomiędzy początkiem i końcem zakresu
                    //y = Math.Atan(Math.Log(Math.Pow(x, 3) + 1) - 3);  //od (b+a)/2
                    y = Math.Cosh(x) - Math.Log(18.9 * Math.Sqrt(x));
                    if (y1 < 0 && y > 0 || y1 > 0 && y < 0) //warunek podmieniający granice zakresu w zależności od wartości funkcji w punkcje (b+a)/2
                    {
                        b_range = (b_range + a_range) / 2; //jeśli początek zakresu (f(x)) jest ujemny a wartość dla f((b+a)/2) jest dodatnia to punkt środka zastępuje punkt końca zakresu
                        epsilon = Math.Abs(b_range - a_range); //epsilon przyjmuje wartość bezwzględnej różnicy pomiędzy początkiem i końcem zakresu
                    }
                    else if (y2 < 0 && y > 0 || y2 > 0 && y < 0)
                    {
                        a_range = (b_range + a_range) / 2; //jeśli koniec zakresu (f(x)) jest ujemny a wartość dla f((b+a)/2) jest dodatnia to punkt środka zastępuje punkt początku zakresu
                        epsilon = Math.Abs(b_range - a_range);
                    }
                    Console.WriteLine(iteracje+" ");
                    Console.WriteLine("x= " + a_range + " ");
                    Console.WriteLine("y= " + y1 + " ");
                    Console.WriteLine("------------"); //wydruk wartości x i y w konsoli
                    File.AppendAllText(docelowasciezka, iteracje + "-" + a_range + "-" + y1 + " " + "\r\n"); //zapis do pliku
                }

            //}
            //else
            //{
            //    Console.WriteLine("Brak miejsc zerowych");
            //}

            Console.WriteLine("Ilość iteracji: " + iteracje); //wydruk ilości iteracji
            ///double alfa =


            Console.ReadLine();
        }
    }
}
