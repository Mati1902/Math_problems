using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neldera_Meada_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x1 = 10;
            double y1 = 10;
            double x2 = 5;
            double y2 = 10;
            double x3 = 10;
            double y3 = 5;

            double[] punkt1 = new double[3] { f(x1,y1), x1, y1 };
            double[] punkt2 = new double[3] { f(x2,y2), x2, y2 };
            double[] punkt3 = new double[3] { f(x3,y3), x3, y3 };


            double[][] tablica = { punkt1, punkt2, punkt3 };
            //Drukowanie tablicy punktów
            for (int i = 0; i < 3; i++)
            {
                int q = 1;
                foreach (double x in tablica[i])
                {
                                       
                    Console.Write(x + " ");
                    if (q == 3)
                    {
                        Console.WriteLine();
                        q = 1;
                    }
                    q++;
                }
            }
            double[][] nowa_tablica = new double[3][];
            for (int i = 0; i <3; i++)
            {
                for(int j = 0; j< 3; j++)
                {
                    nowa_tablica[i] = tablica[i];
                }
            }

            double[][] tablica_posortowana = sortowanie(nowa_tablica);
            Console.WriteLine("-------");
            //Drukowanie posortowanej tablicy
            for (int i = 0; i < 3; i++)
            {
                int q = 1;
                foreach (double x in tablica_posortowana[i])
                {

                    Console.Write(x + " ");
                    if (q == 3)
                    {
                        Console.WriteLine();
                        q = 1;
                    }
                    q++;
                }
            }
            Console.WriteLine("-------");

            //współrzędne centroidu
            double[] centroid_wsp_x_y = centroid_wsp(tablica_posortowana);

            //współrzędne centroidu wraz z wartością Z
            double[] punkt_centroid_z_x_y = {f(centroid_wsp_x_y[0],centroid_wsp_x_y[1]), centroid_wsp_x_y[0], centroid_wsp_x_y[1] };

            //współrzędnie x oraz y punktu x0           
            Console.WriteLine("Centroid: (Z),(X),(Y)");
            foreach (double x in punkt_centroid_z_x_y)
            {
                Console.WriteLine(x);
            }
            //współrzędnie x oraz y punktu xr (metoda reflection)
            double[] xr_Reflection = wspolrzedne_xr_Reflection(tablica_posortowana, punkt_centroid_z_x_y);
            Console.WriteLine("Xr_Refl: (Z),(X),(Y)");
            Console.WriteLine(f(xr_Reflection[0], xr_Reflection[1]));
            foreach (double x in xr_Reflection)
            {
                Console.WriteLine(x);
            }
            //współrzędnie x oraz y punktu xr (metoda expension)
            double[] xe_Expansion = wspolrzedne_xe_Expansion(tablica_posortowana, punkt_centroid_z_x_y);
            Console.WriteLine("Xe_Exp: (Z),(X),(Y)");
            Console.WriteLine(f(xe_Expansion[0], xe_Expansion[1]));
            foreach (double x in xe_Expansion)
            {
                Console.WriteLine(x);
            }

            double[] xc_Contraction_in = contraction_xc_contraction_inside(punkt_centroid_z_x_y, tablica_posortowana);
            Console.WriteLine("Xc_in: (Z),(X),(Y)");
            Console.WriteLine(f(xc_Contraction_in[0], xc_Contraction_in[1]));
            foreach (double x in xc_Contraction_in)
            {
                Console.WriteLine(x);
            }



            double[] xc_Contraction_out = contraction_xc_contraction_outside(punkt_centroid_z_x_y, xr_Reflection);
            Console.WriteLine("Xc_out: (Z),(X),(Y)");
            Console.WriteLine(f(xc_Contraction_out[0], xc_Contraction_out[1]));
            foreach (double x in xc_Contraction_out)
            {
                Console.WriteLine(x);
            }

            double[,] shrink = shrinking(tablica_posortowana);
            Console.WriteLine("Shrink: (X2),(Y2),(X3),(Y3)");
            

                foreach (double x in shrink)
                {
                    Console.WriteLine(x);
                }


            double epsilon = 0.0000000000000001;
            centroid_wsp_x_y = centroid_wsp(tablica_posortowana);
            double[] xr_z_x_y = new double[3];
            

            Console.WriteLine("---------------------");
            Console.WriteLine("--Zaczyna się pętla--");
            Console.WriteLine("---------------------");
            int iteracje = 0;
            while (obliczkryterium(tablica_posortowana, punkt_centroid_z_x_y) > epsilon)
            {
                iteracje++;
                //sortowanie
                tablica_posortowana = sortowanie(tablica_posortowana);
                //obliczanie x i y centroidu
                centroid_wsp_x_y = centroid_wsp(tablica_posortowana);
                //obliczanie z centroidu i dodawanie do nowej tablicy
                punkt_centroid_z_x_y[0] = f(centroid_wsp_x_y[0], centroid_wsp_x_y[1]);
                punkt_centroid_z_x_y[1] = centroid_wsp_x_y[0];
                punkt_centroid_z_x_y[2] = centroid_wsp_x_y[1];
                //obliczanie punktu xr
                xr_Reflection = wspolrzedne_xr_Reflection(tablica_posortowana, punkt_centroid_z_x_y);
                xr_z_x_y[0] = f(xr_Reflection[0],xr_Reflection[1]);
                xr_z_x_y[1] = xr_Reflection[0];
                xr_z_x_y[2] = xr_Reflection[1];
                if (xr_z_x_y[0] < tablica_posortowana[0][0])
                {
                    double[] xe = wspolrzedne_xe_Expansion(tablica_posortowana,punkt_centroid_z_x_y); //xe_x , xe_y
                    if (f(xe[0],xe[1]) < tablica_posortowana[0][0])
                    {
                        // x3 = xe
                        tablica_posortowana[2][0] = f(xe[0], xe[1]);
                        tablica_posortowana[2][1] = xe[0];
                        tablica_posortowana[2][2] = xe[1];
                    }
                    else if (f(xe[0], xe[1]) >= tablica_posortowana[0][0])
                    {
                        //x3 = xr
                        tablica_posortowana[2][0] = xr_z_x_y[0];
                        tablica_posortowana[2][1] = xr_z_x_y[1];
                        tablica_posortowana[2][2] = xr_z_x_y[2];
                    }
                }
                //znacznik
                else if (tablica_posortowana[1][0] > xr_z_x_y[0] && xr_z_x_y[0] > tablica_posortowana[0][0])
                {
                    //x3 = xr
                    tablica_posortowana[2][0] = xr_z_x_y[0];
                    tablica_posortowana[2][1] = xr_z_x_y[1];
                    tablica_posortowana[2][2] = xr_z_x_y[2];
                }
                else if (tablica_posortowana[2][0] > xr_z_x_y[0] && xr_z_x_y[0] > tablica_posortowana[1][0])
                {
                    //x3 = xr
                    tablica_posortowana[2][0] = xr_z_x_y[0];
                    tablica_posortowana[2][1] = xr_z_x_y[1];
                    tablica_posortowana[2][2] = xr_z_x_y[2];
                    //kontrakcja
                    centroid_wsp_x_y = centroid_wsp(tablica_posortowana);
                    punkt_centroid_z_x_y[0] = f(centroid_wsp_x_y[0], centroid_wsp_x_y[1]);
                    punkt_centroid_z_x_y[1] = centroid_wsp_x_y[0];
                    punkt_centroid_z_x_y[2] = centroid_wsp_x_y[1];
                    xc_Contraction_in = contraction_xc_contraction_inside(punkt_centroid_z_x_y, tablica_posortowana);
                    if(f(xc_Contraction_in[0],xc_Contraction_in[1]) < tablica_posortowana[2][0])
                    {
                        //x3 = xc
                        tablica_posortowana[2][0] = f(xc_Contraction_in[0], xc_Contraction_in[1]);
                        tablica_posortowana[2][1] = xc_Contraction_in[0];
                        tablica_posortowana[2][2] = xc_Contraction_in[1];
                    }
                    else
                    {
                        //redukcja
                        shrink = shrinking(tablica_posortowana);
                        tablica_posortowana[1][1] = shrink[0, 0];
                        tablica_posortowana[1][2] = shrink[0, 1];
                        tablica_posortowana[1][0] = f(shrink[0, 0], shrink[0, 1]);
                        tablica_posortowana[2][1] = shrink[1, 0];
                        tablica_posortowana[2][2] = shrink[1, 1];
                        tablica_posortowana[2][0] = f(shrink[1, 0], shrink[1, 1]);
                    }
                }
                //znacznik
                else if (xr_z_x_y[0] > tablica_posortowana[2][0])
                {
                    xc_Contraction_in = contraction_xc_contraction_inside(punkt_centroid_z_x_y, tablica_posortowana);
                    if (f(xc_Contraction_in[0], xc_Contraction_in[1]) < tablica_posortowana[2][0])
                    {
                        //x3 = xc
                        tablica_posortowana[2][1] = xc_Contraction_in[0];
                        tablica_posortowana[2][2] = xc_Contraction_in[1];
                        tablica_posortowana[2][0] = f(xc_Contraction_in[0], xc_Contraction_in[1]);
                    }
                    else
                    {
                        shrink = shrinking(tablica_posortowana);
                        tablica_posortowana[1][1] = shrink[0, 0];
                        tablica_posortowana[1][2] = shrink[0, 1];
                        tablica_posortowana[1][0] = f(shrink[0, 0], shrink[0, 1]);
                        tablica_posortowana[2][1] = shrink[1, 0];
                        tablica_posortowana[2][2] = shrink[1, 1];
                        tablica_posortowana[2][0] = f(shrink[1, 0], shrink[1, 1]);
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int q = 1;
                foreach (double x in tablica_posortowana[i])
                {

                    Console.Write(x + " ");
                    if (q == 3)
                    {
                        Console.WriteLine();
                        q = 1;
                    }
                    q++;
                }
            }
            Console.WriteLine("Iteracji: " + iteracje);
            Console.ReadKey();
        }

        static double obliczkryterium(double[][] tablica_posortowana, double[] punkt_centroid_z_x_y)
        {
            double suma = 0;
            for (int i = 0; i < 3; i++)
            {
                suma = suma + Math.Pow((f(tablica_posortowana[i][1], tablica_posortowana[i][2]) - f(punkt_centroid_z_x_y[1], punkt_centroid_z_x_y[2])), 2);
            }
            suma = suma / 3;
            double sqrt_suma = Math.Sqrt(suma);
            return sqrt_suma;
        }

        static double[,] shrinking(double[][] tablica_posortowana)
        {
            double x1 = tablica_posortowana[0][1];
            double y1 = tablica_posortowana[0][2];
            double x2 = tablica_posortowana[1][1];
            double y2 = tablica_posortowana[1][2];
            double x3 = tablica_posortowana[2][1];
            double y3 = tablica_posortowana[2][2];

            double new_x2 = (x1 + x2) / 2;
            double new_y2 = (y1 + y2) / 2;

            double new_x3 = (x1 + x3) / 2;
            double new_y3 = (y1 + y3) / 2;

            double[,] tablica = new double[2,2];
            tablica[0,0] = new_x2;
            tablica[0,1] = new_y2;
            tablica[1,0] = new_x3;
            tablica[1,1] = new_y3;

            return tablica;
        }

        static double[] contraction_xc_contraction_inside(double[] punkt_centroid_z_x_y, double[][] tablica_posortowana)
        {
            double x_centroid = punkt_centroid_z_x_y[1];
            double y_centroid = punkt_centroid_z_x_y[2];
            double x3 = tablica_posortowana[2][1];
            double y3 = tablica_posortowana[2][2];

            double xc_x = (x_centroid + x3) / 2;
            double xc_y = (y_centroid + y3) / 2;

            double[] xc_Contraction_inside = { xc_x, xc_y };
            return xc_Contraction_inside;
        }

        static double[] contraction_xc_contraction_outside(double[] punkt_centroid_z_x_y, double[] xr_Reflection)
        {
            double x_centroid = punkt_centroid_z_x_y[1];
            double y_centroid = punkt_centroid_z_x_y[2];
            double xr = xr_Reflection[0];
            double yr = xr_Reflection[1];

            double xc_x = (x_centroid + xr) / 2;
            double xc_y = (y_centroid + yr) / 2;

            double[] xc_Contraction_outside = { xc_x, xc_y };
            return xc_Contraction_outside;
        }

        static double[] wspolrzedne_xr_Reflection(double[][] tablica_posortowana, double[] punkt_centroid_z_x_y)
        {
            //wektor przesunięcia
            double wektor_przesuniecia_x = punkt_centroid_z_x_y[1] - tablica_posortowana[2][1];
            double wektor_przesuniecia_y = punkt_centroid_z_x_y[2] - tablica_posortowana[2][2];
            //współrzędne x i y punktu xr
            double xr_x = punkt_centroid_z_x_y[1] + wektor_przesuniecia_x;
            double xr_y = punkt_centroid_z_x_y[2] + wektor_przesuniecia_y;
            double[] wsp_xr = { xr_x, xr_y };
            return wsp_xr;
        }

        static double[] wspolrzedne_xe_Expansion(double[][] tablica_posortowana, double[] punkt_centroid_z_x_y)
        {
            //wektor przesunięcia
            double wektor_przesuniecia_x = 2 * (punkt_centroid_z_x_y[1] - tablica_posortowana[2][1]);
            double wektor_przesuniecia_y = 2 * (punkt_centroid_z_x_y[2] - tablica_posortowana[2][2]);
            //współrzędne x i y punktu xr
            double xr_x = punkt_centroid_z_x_y[1] + wektor_przesuniecia_x;
            double xr_y = punkt_centroid_z_x_y[2] + wektor_przesuniecia_y;
            double[] wsp_xr = { xr_x, xr_y };
            return wsp_xr;
        }

        static double f(double x, double y)
        {
            double a = 2;
            double n = 13;
            double b = n + 5;
            double z = Math.Pow((a - x), 2) + b * Math.Pow(y - Math.Pow(x, 2), 2);
            return z;
        }

        static double[][] sortowanie(double[][] tablica)
        {
            int n = tablica.Length;
            bool zamien = false;
            double[] stara1;
            double[] stara2;
            while (n > 1)
            {
                zamien = false;
                for (int i = 0; i < n-1; i++)
                {
                    if (tablica[i][0] > tablica[i+1][0])
                    {
                        stara1 = tablica[i];
                        stara2 = tablica[i + 1];
                        tablica[i + 1] = stara1;
                        tablica[i] = stara2;
                        zamien = true;
                    }
                }
                n--;
                if(zamien == false)
                {
                    break;
                }
            }
            return tablica;
        }

        static double[] centroid_wsp(double[][] tablica)
        {
            double x1 = tablica[0][1];
            double x2 = tablica[1][1];           
            double x_centroid = (x1 + x2) / 2;
            double y1 = tablica[0][2];
            double y2 = tablica[1][2];
            double y_centroid = (y1 + y2) / 2;
            double[] centroid_wsp = { x_centroid, y_centroid };
            return centroid_wsp;
        }

    }
}
