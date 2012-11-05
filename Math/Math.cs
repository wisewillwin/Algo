using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math
{
    class Math
    {
        static void Main(string[] args)
        {
            SQRT.Test();
        }
    }

    /// <summary>
    /// square root function
    /// </summary>
    public class SQRT
    {
        public static readonly double eps = 1e-10; 
        
        // newton's method, very efficient
        // f(y) = y^2 - x and f'(y) = 2y
        public static double SqrtNewtonMethod(double x)
        {
            if (x < 0) return -1;
            double val = x;
            double temp;
            do
            {
                temp = val;
                val = (val + x / val) / 2;
            } while (System.Math.Abs(val - temp) > eps);
            return val;
        }

        // binary search, not efficient
        // pitfalls: (1) error handling for x < 0
        //           (2) pick different (low, high) initial range for 0<x<1 and x>1
        public static double SqrtBinarySearch(double x)
        {
            if (x < 0) return -1; // error handling
            double low;
            double high;
            if (x < 1)
            { // pitfall
                low = 0;
                high = 1;
            }
            else
            {
                low = 1;
                high = x;
            }
            double mid = (low + high) / 2;
            double temp;
            do
            {
                if (mid * mid > x)
                    high = mid;
                else
                    low = mid;
                temp = mid;
                mid = (low + high) / 2;
            } while (System.Math.Abs(mid - temp) > eps);
            return mid;
        }

        public static void Test()
        {
            for (int i = -2; i < 5; i++)
            {
                Console.WriteLine("X = {0} Sqrt by Newton       {1}", 
                    i, SqrtNewtonMethod(i));
                Console.WriteLine("X = {0} Sqrt by BinarySearch {1}",
                    i, SqrtBinarySearch(i));
                Console.WriteLine("X = {0} Sqrt by System.Math  {1}\n",
                    i, System.Math.Sqrt(i));
            }

            for (double i = 0; i < 0.6; i += 0.1)
            {
                Console.WriteLine("X = {0} Sqrt by Newton       {1}",
                    i, SqrtNewtonMethod(i));
                Console.WriteLine("X = {0} Sqrt by BinarySearch {1}",
                    i, SqrtBinarySearch(i));
                Console.WriteLine("X = {0} Sqrt by System.Math  {1}\n",
                    i, System.Math.Sqrt(i));
            }
        }
    
    
    
    }


}
