using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace MethodOfPotentials_C_
{
    internal class CellPotentials
    {
        static int[] Suppl(int n)
        {
            int[] Suppliers = new int[n];
            string[] s = Console.ReadLine().Split(" ");

            int i = 0;
            foreach (string c in s)
            {
                Suppliers[i++] = int.Parse(c);
            }
            return Suppliers;
        }
        static int[] Cust(int m)
        {
            int[] Customers = new int[m];
            string[] s = Console.ReadLine().Split(" ");

            int i = 0;
            foreach (string c in s)
            {
                Customers[i++] = int.Parse(c);
            }
            return Customers;
        }
        static int[,] _Cost(int n,int m)
        {
            int[,] Cost = new int[n,m];
            string[] s = Console.ReadLine().Split(" ");

            int i = 0;
            int j = 0;
            foreach (string c in s)
            {
                Cost[i,j++] = int.Parse(c);
                if (j == m)
                {
                    j = 0;
                    i++;
                }
            }
            return Cost;
        }
        static int[,] _Glavmass(int n, int m)
        {
            int[,] Cost = new int[n, m];
            string[] s = Console.ReadLine().Split(" ");

            int i = 0;
            int j = 0;
            foreach (string c in s)
            {
                Cost[i, j++] = int.Parse(c);
                if (j == m)
                {
                    j = 0;
                    i++;
                }
            }
            return Cost;
        }
        /////////////////////////////////////////////////////////////////////////////
        static void WorkMassUV(ref int[] U, ref int[] V, int[,] Cost, int[,] GlavMass)
        {
            for (int i = 0; i < V.Length; i++)
            {
                if (GlavMass[0,i] != 0) {
                    V[i] = Cost[0,i] - 0;
                }
            }
            for (int IJ = 0; IJ < V.Length; IJ++)
            {
                for (int i = 0; i < V.Length; i++)
                {
                    for (int j = 0; j < U.Length; j++) 
                    {
                        if (V[i] != 0 && GlavMass[j,i] != 0)
                        {
                            U[j] = Cost[j,i] - V[i];
                        }
                    }                    
                }
                for (int i = 0; i < U.Length; i++)
                {
                    for (int j = 0; j < V.Length; j++)
                    {
                        if (U[i] != 0 && GlavMass[i, j] != 0)
                        {
                            V[j] = Cost[i, j] - U[i];
                        }
                    }
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////
        static int _Degeneracy(int[,] GlavMass)
        {
            int count = 0;
            foreach (int i in GlavMass) 
            { 
                if(i > 0)
                {
                    count++;
                }
            }
            return count;
        }
        static int[] _triangle(int Degeneracy, int[,] Glavmass, int[,] Cost, int[] U, int[] V)
        {
            int[] triangle = new int[Degeneracy];
            int triangle_count = 0;
            for(int i = 0; i < U.Length; i++)
            {
                for(int j = 0; j < V.Length; j++)
                {
                    if(Glavmass[i,j] == 0 && triangle_count < 7)
                    {
                        triangle[triangle_count++] = U[i] + V[j] - Cost[i,j];
                    }
                }
            }
            return triangle;
        }
        public void CellPot(int n, int m)
        {
            int InCount = int.MaxValue;
            while(InCount == int.MaxValue)
            {
                int[] Suppliers = Suppl(n);
                int[] Customers = Cust(m);

                int[,] Cost = _Cost(n, m);
                int[,] Glavmass = _Glavmass(n, m);

                int[] U = new int[n];
                int[] V = new int[m];

                int Degeneracy = _Degeneracy(Glavmass);

                WorkMassUV(ref U, ref V, Cost, Glavmass);

                int[] triangle = _triangle(Degeneracy, Glavmass, Cost, U, V);

                int MaxTriangle = triangle.Max();

                //////////////////////////ИНТЕРФЕЙС/////////////////////////////

                string _TR = "Решение оптимальное";

                if (Suppliers.Sum() == Customers.Sum())
                {
                    Console.WriteLine("Задача закрытая");
                }
                else { Console.WriteLine("Задача открытая"); }

                if (Degeneracy == m+n-1)
                {
                    Console.WriteLine("Задача невырожденная");
                }
                else { Console.WriteLine("Задача вырожденная"); }

                Console.Write("U: ");
                foreach (int i in U)
                {
                    Console.Write($"{i} ");
                }

                Console.Write("\nV: ");
                foreach (int i in V)
                {
                    Console.Write($"{i} ");
                }

                Console.Write("\nTR: ");
                foreach (int i in triangle)
                {
                    Console.Write($"{i} ");
                    if (i > 0) { _TR = "Решение неоптимальное"; }
                }
                
                Console.WriteLine($"\n{_TR}");

                if (_TR == "Решение неоптимальное")
                {
                    Console.WriteLine("Решение неоптимальное, попробуйте исправить опорный план\n\n");
                }
                else
                {
                    int Lx = 0;
                    for(int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            Lx += Glavmass[i, j] * Cost[i, j];
                        }
                    }
                    Console.WriteLine($"LX: {Lx}");
                }
            }
        }
    }
}
