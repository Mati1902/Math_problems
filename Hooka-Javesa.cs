using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooka_Jeevesa_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double[] punkt = { x, y, z };
            double dlugosc_kroku = 0.1;            
            double epsilon = 0.00001;
            double[] kierunek = { 1, -1 };
            double F0 = 0;
            double F_nowy = 0;
            int iteracja = 0;
            double x_temp = 0;
            double y_temp = 0;
            double z_temp = 0;
            double[] punkt_temp = { x_temp, y_temp, z_temp };
            bool czy_przerwac = false;
            double poprzeni_F = 0;
            while (f(punkt[0], punkt[1], punkt[2]) > epsilon)
            {
                poprzeni_F = F0;
                iteracja++;
                for (int q = 0; q < punkt_temp.Length; q++)
                {
                    punkt_temp[q] = punkt[q];
                }
                F0 = f(punkt[0], punkt[1], punkt[2]);

                for (int j = 0; j < punkt.Length; j++)
                {
                    czy_przerwac = false;
                    for (int p = 0; p < kierunek.Length; p++)
                    {
                        punkt_temp[j] = punkt[j] + kierunek[p] * dlugosc_kroku;
                        F_nowy = f(punkt_temp[0], punkt_temp[1], punkt_temp[2]);
                        if (F_nowy < F0)
                        {
                            punkt[0] = punkt_temp[0];
                            punkt[1] = punkt_temp[1];
                            punkt[2] = punkt_temp[2];
                            czy_przerwac = true;
                            break;
                        }
                    }
                    if (czy_przerwac)
                    {
                        break;
                    }
                }
                Console.WriteLine("x: " + punkt[0]);
                Console.WriteLine("y: " + punkt[1]);
                Console.WriteLine("z: " + punkt[2]);
                Console.WriteLine("F: " + F_nowy);
                Console.WriteLine("--------");
                if (poprzeni_F == F0)
                {
                    dlugosc_kroku = dlugosc_kroku / 2;
                    if (dlugosc_kroku <= 0)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Iteracji: " + iteracja);
            Console.ReadKey();
        }

        static double f(double x, double y, double z)
        {
            double n = 13;
            double a = 1;
            double b = 2 * n;
            double v = (Math.Pow((a - x), 2) + b * Math.Pow((y - Math.Pow(x, 2)), 2)) + (Math.Pow((a - y), 2) + b * Math.Pow((z - Math.Pow(y, 2)), 2));
            return v;
        }
    }
}
