using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DynamicProgramming
{
    class DynamicProgramming
    {
        static void Main(string[] args)
        {
            //MaxRectangleOf1s.Test();

            PartitionProblem.Test();

            //SubsequenceMaxSum.Test();

            //MatrixMaxPath.Test();

            //LongestIncresingSubsequence.Test();

            //StringEditDistance.Test();

            //LongestCommonSubsequence.Test();

            //BinomialCoefficient.Test();


            //MaxStockProfitProblem.Test();


            //ArrayMaxHistgram.Test();
        
        
        
        
        
        }
    }

    /// <summary>
    /// Given a matrix of 0s and 1s, find size of the max rectangle of 1s
    /// </summary>
    public class MaxRectangleOf1s
    {

        public static int FindMaxSize(int[,] matrix)
        {
            int[,] continous1sMatrix = matrix;
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                        continous1sMatrix[i, j] = Math.Min(continous1sMatrix[i - 1, j - 1],
                            Math.Min(continous1sMatrix[i - 1, j], continous1sMatrix[i, j - 1]));
                    else
                        continous1sMatrix[i, j] = 0;
                }
            }
            return 0;
        }
        public static void Test()
        {
            int[,] mr = { 
            {0, 1, 1, 0, 1},
            {1, 1, 0, 1, 0},
            {0, 1, 1, 1, 0},
            {1, 1, 1, 1, 0}, 
            {1, 1, 1, 1, 1},
            {0, 0, 0, 0, 0}};
            Console.WriteLine(FindMaxSize(mr));
        }
    }

    /// <summary>
    /// Decide if an array can be partitioned into two parts of the same sum
    /// i.e. {3, 1, 1, 2, 2, 1} can be partitioned into {1, 2, 2} and {3, 1, 1}
    /// O(Sum * N) time, psedou-poly time
    /// </summary>
    public class PartitionProblem
    {
        // builds a table of size (sum/2, array.Length), O(N*Sum) pseduo-polynomial time, O(N*Sum) space
        // table[sum, index] = table[sum, index - 1] 
        //                     OR table[sum - (a[index] - 1), index - 1]

        /*    index =====>       
         *sum 0   1   2   3   4   5   6
         * || 1   F   T   T   T   T   T
         * || 2   F   F   T   T   T   T
         * \/ 3   T   T   T   T   T   T
         *    4   F   T   T   T   T   T
         *    5   F   F   T   T   T   T
         */
        public static bool CanPartition(int[] a)
        {
            int sum = 0;
            foreach (int i in a) sum += i;
            if (sum % 2 == 1) return false;
            int row = sum / 2;
            int col = a.Length;
            bool[,] table = new bool[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (j == 0)
                    {
                        if (i + 1 == a[j]) 
                            table[i, j] = true;
                    }
                    else
                    {
                        table[i, j] = table[i, j - 1] 
                            || (i + 1 >= a[j] && table[i + 1 - a[j], j - 1]);                            
                    }
                }
            }
            return table[row - 1, col - 1];
        }

        // slightly different implementation, builds a table of size (sum/2+1, array.Length+1)
        public static bool CanPartition_version2(int[] a)
        {
            int sum = 0;
            foreach (int i in a) sum += i;
            if (sum % 2 == 1) return false;
            int row = sum / 2 + 1;
            int col = a.Length + 1;
            bool[,] table = new bool[row, col];
            for (int j = 0; j < col; j++)
                table[0, j] = true;
            for (int i = 1; i < row; i++)
                table[i, 0] = false;
            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < col; j++)
                {
                    table[i, j] = table[i, j - 1]
                        || (i + 1 >= a[j] && table[i + 1 - a[j], j - 1]);    
                }
            }
            return table[row - 1, col - 1];        
        }

        public static void Test()
        {
            int[] a1 = {1, 2, 2, 3, 1, 1}; // true
            int[] a2 = {1, 2, 2, 3, 1, 2}; // false
            int[] a3 = {1, 2, 2, 3, 3, 13}; // false
            Debug.Assert(CanPartition(a1) && (CanPartition(a1) == CanPartition_version2(a1)));
            Debug.Assert(!CanPartition(a2) && (CanPartition(a2) == CanPartition_version2(a2)));
            Debug.Assert(!CanPartition(a3) && (CanPartition(a3) == CanPartition_version2(a3))); 
        }
    
    }

    /// <summary>
    /// Beauty 2.14
    /// Find max sum of subsequence of a given array
    /// O(N) time
    /// </summary>
    public class SubsequenceMaxSum
    {

        public static int MaxSum(int[] a)
        {
            if (a.Length == 1)
                return a[0];
            int sum = a[a.Length - 1];
            int temp = a[a.Length - 1];
            for (int i = a.Length - 2; i > 0; i--)
            { 
                // 1. update temp
                // 2. update global sum
                temp = Math.Max(a[i], a[i] + temp);
                sum = Math.Max(sum, temp);
            }
            return sum;
        }


        public static void Test()
        {
            int[] a1 = {1, -2, 3, 5, -3, 2 }; // 8
            int[] a2 = {0, -2, 3, 5, -1, 2 }; // 9
            int[] a3 = {-9, -2, -3, -5, -3 }; // -2
            Debug.Assert(MaxSum(a1) == 8);
            Debug.Assert(MaxSum(a2) == 9);
            Debug.Assert(MaxSum(a3) == -2);
        }

    
    }

    /// <summary>
    /// Beauty 2.15
    /// Find max sum of subsequence of a 2D array
    /// </summary>
    public class SubsequenceMaxSum2DArray
    {
        public static int MaxSum(int[,] a)
        {

            return 0;
        }


        public static void Test()
        {
            int[,] a = { 
                        {0, -1, -1, 0},
                        {-10, 10, 10, -10},
                        {-9, 9, 9, -9},
                        {-8, 8, 8, -8},
                        { 26, 1, 1, 27}
                      };
            Console.WriteLine(MaxSum(a));
        
        
        }
    
    }



    /// <summary>
    /// Beauty 2.17
    /// Find the longest ascending subsequence of a given array
    /// </summary>
    public class LongestIncresingSubsequence
    {
        // DP implementation, (N^2) time and O(N) space
        public static int LIS_DP(int[] a)
        {
            int[] lis = new int[a.Length];
            for (int i = 0; i < lis.Length; i++) lis[i] = 1;
            for (int i = 1; i < a.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (a[j] < a[i] && lis[j] >= lis[i])
                        lis[i] = lis[j] + 1;
                }
            }
            int max = 1;
            for (int i = 0; i < lis.Length; i++)
            {
                if (lis[i] > max) max = lis[i];
            }
            return max;
        }

        // binary search/arrayList implementation, O(N lg N) time and O(N) space
        public static int LIS_BinarySearch(int[] a)
        {
            List<int> lis = new List<int>();
            lis.Add(a[0]);
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > lis[lis.Count - 1])
                    lis.Add(a[i]);
                else
                {
                    int low = 0;
                    int high = lis.Count - 1;
                    int mid;
                    while (low <= high)
                    {
                        mid = (low + high) / 2;
                        if (a[i] > lis[mid])
                            low = mid + 1;
                        else high = mid - 1;
                    }
                    lis[low] = a[i];
                }
            }
            return lis.Count;
        }

        public static void Test()
        {
            int[] a1 = {1, 5, 8, 3, 7};
            int[] a2 = {1, 5, 8, 9, 3, 4, 6};
            int[] a3 = {5, 4, 3, 2, 1};

            Debug.Assert(3 == LIS_DP(a1) && LIS_DP(a1) == LIS_BinarySearch(a1));
            Debug.Assert(4 == LIS_DP(a2) && LIS_DP(a2) == LIS_BinarySearch(a2));
            Debug.Assert(1 == LIS_DP(a3) && LIS_DP(a3) == LIS_BinarySearch(a3));
        }

    }

    /// <summary>
    /// Beauty 2.18
    /// Partition an array of size 2N into two halves of size N, the difference 
    /// of sum should be as small as possible
    /// </summary>
    public class ClosestPartitionSum
    {
    
    
    }


    /// <summary>
    /// Beauty 3.3
    /// Calculate the insert/delete/update (each operation is at expense of 1) distance of two strings
    /// </summary>
    public class StringEditDistance
    {
        // O(NM) time and O(NM) space
        // table[i, j] = min(table[i-1,j]+1, table[i,j-1]+1, table[i-1,j-1] if s1[i] == s2[j])
        public static int EditDistance(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                return -1; // null string's edit distance is -1
            if (s1.Length == 0)
                return s2.Length; // empty string's edit distance is the length of the other string
            if (s2.Length == 0)
                return s1.Length;
            int[,] table = new int[s1.Length, s2.Length];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        if (s1[i] == s2[j]) table[i, j] = 0;
                        else table[i, j] = 1;
                    }
                    else if (i == 0)
                    {
                        table[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        table[i, j] = i;
                    }
                    else
                    {
                        if (s1[i] == s2[j])
                            table[i, j] = Math.Min(table[i - 1, j - 1], Math.Min(table[i - 1, j], table[i, j - 1]) + 1);
                        else
                            table[i, j] = Math.Min(table[i - 1, j - 1], Math.Min(table[i - 1, j], table[i, j - 1])) + 1;
                    }
                }
            }
            return table[table.GetLength(0) - 1, table.GetLength(1) - 1];        
        }

        public static void Test()
        {
            string s1 = "sunny";
            string s2 = "snowy";
            Debug.Assert(3 == EditDistance(s1, s2));
            Debug.Assert(5 == EditDistance(s1, ""));
            Debug.Assert(0 == EditDistance(s1, s1));
            Debug.Assert(-1 == EditDistance(s1, null));     
        }
    
    }

    /// <summary>
    /// Find the longest common subsequence (not necessarily continuous) of two strings
    /// (Similar problem to string Edit Distance problem)
    /// </summary>
    public class LongestCommonSubsequence
    {
        // build a N*M table, O(NM) time and O(NM) space
        // if s1[i]==s2[j]: 
        //      table[i][j] = table[i-1][j-1]+1
        // else: 
        //      since table[i-1][j-1] <= max(table[i-1][j-1], table[i-1][j])
        //      table[i][j] = max(table[i-1][j], table[i][j-1])
        public static int LCS(string s1, string s2)
        {
            if (s1 == null || s2 == null)
                return -1;
            if (s1.Length == 0 || s2.Length == 0)
                return 0;
            int[,] table = new int[s1.Length, s2.Length];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == 0 || j == 0)
                    {
                        if (s1[i] == s2[j]) table[i, j] = 1;
                        else table[i, j] = 0;
                    }
                    else
                    { 
                        if (s1[i] == s2[j]) 
                            table[i, j] = table[i - 1, j - 1] + 1;
                        else  
                            table[i, j] = Math.Max(table[i - 1, j], table[i, j - 1]);
                    }
                }
            }
            return table[table.GetLength(0) - 1, table.GetLength(1) - 1];        
        }

        public static void Test()
        {
            string s1 = "12abcdefg";
            string s2 = "a1b2d345g";
            string s3 = "abcabbbdg";
            Debug.Assert(4 == LCS(s1, s2));
            Debug.Assert(5 == LCS(s1, s3));
            Debug.Assert(9 == LCS(s1, s1));
            Debug.Assert(0 == LCS(s1, ""));
            Debug.Assert(-1 == LCS(s1, null));
        }
    
    }

    /// <summary>
    /// 
    /// </summary>
    public class MatrixMaxPath
    {
        // build a lookup table for DP, O(NM) time and O(NM) space
        public static int MaxPathSum(int[,] a)
        {
            int[,] table = new int[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                        table[i, j] = a[0, 0];
                    else if (i == 0 && j != 0)
                        table[i, j] = table[i, j - 1] + a[i, j];
                    else if (i != 0 && j == 0)
                        table[i, j] = table[i - 1, j] + a[i, j];
                    else
                        table[i, j] = Math.Max(table[i - 1, j], table[i, j - 1]) + a[i, j];
                }
            }
            return table[table.GetLength(0) - 1, table.GetLength(1) - 1];
        }

        // optimized for space, only use O(NM) space
        public static int MaxPathSum_optimized_for_space(int[,] a)
        {
            // use int[] lastRow to hold upper row for query
            int[] lastRow = new int[a.GetLength(1)];
            lastRow[0] = a[0, 0];
            for (int i = 1; i < a.GetLength(1); i++)
                lastRow[i] = lastRow[i - 1] + a[0, i];
            // pre-compute the first column of the table
            int[] col = new int[a.GetLength(0)];
            col[0] = a[0, 0];
            for (int i = 1; i < a.GetLength(0); i++)
                col[i] = col[i - 1] + a[i, 0];
            for (int i = 1; i < a.GetLength(0); i++)
            {
                for (int j = 1; j < a.GetLength(1); j++)
                {
                    if (j == 1) lastRow[0] = col[i]; // set boundary
                    lastRow[j] = Math.Max(lastRow[j], lastRow[j - 1]) + a[i, j];
                }
            }
            return lastRow[lastRow.Length - 1];
        }


        public static void FindMaxPath(int[,] a)
        {

        }

        public static void Test()
        {
            int[,] a = { {2, 3, 4}, 
                       {7, 8, 5}, 
                       {6, 9, 10}};
            Console.WriteLine("max path sum = " + MaxPathSum(a));
            Console.WriteLine("max path sum = " + MaxPathSum_optimized_for_space(a));
            //FindMaxPath(a);
            
        
        }

    
    
    }

    /// <summary>
    /// Find the length of longest substring with no repeating characters
    /// </summary>
    public class LongestNonrepeatingSubstring
    {

        public static int LongestSubstring(string s)
        {


            return 0;
        }


        public static void Test()
        {
            string s1 = "hello world";
            string s2 = "One World One Dream";
            Console.WriteLine(LongestSubstring(s1));
            Console.WriteLine(LongestSubstring(s2));
            //Debug.Assert(6 == LongestSubstring(s1));
            //Debug.Assert(9 == LongestSubstring(s2));
        }
    
    }


    /// <summary>
    /// Compute binomial coefficient (n, k)
    /// i.e. C(10, 3) = 120, C(10, 4) = 210
    /// </summary>
    public class BinomialCoefficient
    {
        // use table[][] in DP
        // O(NK) time and O(NK) space
        public static int BiCoefficient_DP(int n, int k)
        {
            if (n < 1 || k < 0 || n < k) return -1; // check for illegal inputs
            if (k > n / 2)
                k = n - k;
            int[,] table = new int[n + 1, k + 1];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= Math.Min(i, k); j++)
                {
                    if (i == 1)
                        table[i, j] = 1;
                    else if (j == 0)
                        table[i, j] = 1;
                    else
                        table[i, j] = table[i - 1, j - 1] + table[i - 1, j];
                }
            }
            return table[n, k];
        }


        // use array[] instead of table[][] in DP, optmized for space
        // O(NK) time and O(K) space
        public static int BiCoefficient_DP_2(int n, int k)
        {
            if (n < 1 || k < 0 || n < k) return -1; // check for illegal inputs
            if (k > n / 2) 
                k = n - k;
            int[] array = new int[k + 1];
            array[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                for (int j = Math.Min(i, k); j >= 1; j--) // tricky
                {
                    array[j] += array[j - 1];
                }
            }
            return array[k];            
        }

        

        // directly compute, O(K) time and O(1) space
        public static int BiCoefficient(int n, int k)
        {
            if (n < 1 || k < 0 || n < k) return -1; // check for illegal inputs
            double result = 1;
            if (k > n / 2)
                k = n - k;
            for (int i = 0; i < k; i++)
            {
                result *= (n - i) / (double)(i + 1);
            }
            return (int)(Math.Round(result)); 
        }

        public static void Test()
        {
            Debug.Assert(BiCoefficient(10, 3) == BiCoefficient_DP(10, 3) && BiCoefficient(10, 3) == 120);
            Debug.Assert(BiCoefficient(10, 8) == BiCoefficient_DP(10, 8) && BiCoefficient(10, 8) == 45);
            Debug.Assert(BiCoefficient(10, 0) == BiCoefficient_DP(10, 0) && BiCoefficient(10, 0) == 1);
            Debug.Assert(BiCoefficient(10, 12) == BiCoefficient_DP(10, 12) && BiCoefficient(10, 12) == -1);
            Debug.Assert(BiCoefficient(10, -3) == BiCoefficient_DP(10, -3) && BiCoefficient(10, -3) == -1);
        }
    }


    /// <summary>
    /// Given an array representing stock prices, find the point to buy and sell 
    /// so as maximum your profit, return the max profit
    /// i.e. array =  { 10, 11, 20, 13, 5, 8, 17, 11 }, buy at 5 and sell at 17
    /// </summary>
    public class MaxStockProfitProblem
    {
        // (p, q) as min-max globally, (pp, qq) as min-max locally
        // iterate through a for-loop, (1) if local min-max exceeds global min-max, update global min-max
        // (2) else if a bigger local max is seen, update local max
        // (3) else if a smaller local min is seen, reset local min and local max
        public static int MaxStockProfit(int[] a)
        {
            if (a.Length <= 1) return 0;          
            if (a.Length == 2) 
                return Math.Abs(a[0] - a[1]);
            int p = a[0] < a[1] ? 0 : 1; // min index so far
            int q = 1 - p; // max index so far
            int pp = p; //  min pointer temperally
            int qq = q; // max pointer termperally
            for (int i = 2; i < a.Length; i++)
            {
                if (a[i] - a[pp] > a[q] - a[p])
                {
                    p = pp;
                    q = i;
                }
                else if (a[i] > a[qq]) 
                {
                    qq = i;
                }
                else if (a[i] < a[pp]) 
                {
                    pp = qq = i;
                }
            }
            return a[q] - a[p];
        }


        public static void Test()
        {
            int[] a = { 10, 11, 20, 13, 5, 8, 17, 11 }; 
            int profit = MaxStockProfit(a);
            Debug.Assert(profit == 12);
            profit = 0;
            int[] a2 = { 10, 20, 30, 10, 20 };
            MaxStockProfit(a2);
            Debug.Assert(profit == 20);
        }


    }



    /// <summary>
    /// Given an array, use the array to generate a histgram, find the max rectangle of the histgram
    /// eg: { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 6, 6, 13 } max: 13
    /// </summary>
    public class ArrayMaxHistgram
    {
        // this is the first version I wrote, not efficient for space
        // O(N) time and space
        // result[i] is for Opt(i), temp is for sum contains current a[i]  
        public static int MaxRectangle_inefficient(int[] a)
        {
            int n = a.Length;
            if (n <= 1) return a[0];
            int[] result = new int[n];
            result[0] = a[0];
            int temp = a[0];
            for (int i = 1; i < n; i++)
            {
                if (a[i] != a[i - 1])
                {
                    temp = a[i];
                    result[i] = Math.Max(result[i - 1], a[i]);
                }
                else
                {
                    temp += a[i];
                    result[i] = Math.Max(result[i - 1], temp);
                }
            }
            return result[n - 1];
        }

        // O(N) time and O(1) space
        // two variables one for previous max sum, one for current sum
        public static int MaxRectangle(int[] a)
        {
            if (a.Length <= 1) return a[0];
            int max = a[0];
            int temp = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] == a[i - 1]) // 1. continue to build previous rectangle
                    temp += a[i];
                else // 2. build a new rectangle
                    temp = a[i];
                max = Math.Max(max, temp); // 3. update
            }
            return max;
        }

        public static void Test()
        {
            int[] array = new int[] { 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 6, 6, 13 };
            Console.WriteLine(MaxRectangle(array)); // 13
        }
    }





















































}
