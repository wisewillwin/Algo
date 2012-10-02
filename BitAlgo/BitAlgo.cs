using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BitAlgo
{
    class BitAlgo
    {
        static void Main(string[] args)
        {
            BitAlgos.ToggleBitsTest();

            BitAlgos.SwapBitsTest();

            BitAlgos.TurnOffRightestOneTest();

            BitAlgos.CountBitOneTest();

            BitAlgos.SwapPairsTest();

            BitAlgos.AddTest();

            BitAlgos.ReverseTest();

            BitAlgos.CompareTest();

            BitAlgos.SwapNumTest();

			//TwoUniqueNumber.Test();

            //BitDiff.Test();

            SetBitsSorting.Test();
        }
	}

	public class BitAlgos {

        // Q1: toggle from bit i to bit j
        // XOR trick: X ^ 111...1 can toggle all the bits in X
        public static int ToggleBits(int x, int i, int j)
        {
            int mask = 0;
            while (i < j)
            {
                mask = mask | (1 << i); // generate binary of bit i to j of 1
                i++;
            }
            return x ^ mask;
        }
        public static void ToggleBitsTest()
        {
            int x = 100; // "1100100"
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(ToggleBits(x, 1, 3), 2));
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(ToggleBits(x, 2, 4), 2));
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(ToggleBits(x, 3, 5), 2));
            Console.WriteLine();
        }



        // Q2: swap bit i and j of integer x
        // XOR trick: X ^ 0 = X
		public static int SwapBits(int x, int i, int j)
        {
            // if bit i and bit j is he same, no need to do anything
            if ((x >> i & 1) == (x >> j & 1)) return x;
            // else, toggle bit i and j
            else return x ^ ((1 << i) | (1 << j));
        }
		public static void SwapBitsTest()
        {
            int x = 100; // "1100100"
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(SwapBits(x, 1, 3), 2));
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(SwapBits(x, 2, 4), 2));
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(SwapBits(x, 3, 5), 2));
            Console.WriteLine();
        }

        // Q3: turn off the right-most 1 bit
		public static int TurnOffRightestOne(int x)
        {
            return x & (x - 1);
        }
		public static void TurnOffRightestOneTest()
        {
            int x = 100; // "1100100"
            Console.WriteLine(Convert.ToString(x, 2) + " -> " + Convert.ToString(TurnOffRightestOne(x), 2));
            Console.WriteLine();
        }

        // Q4: count the number of 1 bit in x
		public static int CountBitOne(int x)
        {
            int count = 0;
            while (x != 0)
            {
                x &= x - 1;
                count++;
            }
            return count;
        }
		public static void CountBitOneTest()
        {
            int x = 100;
            Console.WriteLine(Convert.ToString(x, 2) + " count: " + CountBitOne(x));
            Console.WriteLine();
        }

        // Q5: swap every two bits, eg: 10100101 -> 01011010
        // Careercup 5.6
		public static int SwapPairs(int x)
        {
            for (int i = 0; i < 32; i += 2)
            {
                x = SwapBits(x, i, i + 1);
            }
            return x;
        }
        public static Int64 SwapPairs_manually(Int64 x)
        {
            return ((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1);
        }
		public static void SwapPairsTest()
        {
            int x = 165;
            Console.WriteLine(Convert.ToString(x, 2) + " swap every pair of bits: " + 
                Convert.ToString(SwapPairs_manually(x), 2));
            Console.WriteLine();
        }

        // http://www.leetcode.com/2011/08/reverse-bits.html 
        // Swap each pair by XOR tricks
        // Q6: reverse 32-bit integer, eg: 10101111 -> 11110101
		public static int Reverse(int x)
        {
            for (int i = 0; i < 16; i++)
            {
                x = SwapBits(x, i, 31 - i);
            }
            return x;
        }
        public static byte Reverse_byte(byte b)
        {
            int rev = (b >> 4) | ((b & 0xf) << 4);
            // (rev & 11001100) | (rev & 00110011) 
            rev = ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2);
            // (rev & 10101010) | (rev & 01010101)
            rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1);
            return (byte)rev;
        }

		public static void ReverseTest()
        {
            int x = (int)Math.Pow(2, 16) - 1;
            Console.WriteLine(Convert.ToString(x, 2) + " reverse: " + Convert.ToString(Reverse(x), 2));
            Console.WriteLine();
            byte y = 0xcc;
            Console.WriteLine(Convert.ToString(y, 2) + " reverse: " + Convert.ToString(Reverse_byte(y), 2));
            Console.WriteLine();

        }

        // Q8: add operation without operator +-*/, only bitwise operation
		public static int Add(int x, int y)
        {
            if (x == 0) return y;
            if (y == 0) return x;
            int a = x ^ y; // result without carry
            int b = (x & y) << 1; // carry
            return Add(a, b);
        }
		public static void AddTest()
        {
            int x = 100, y = 23;
            Console.WriteLine("{0} + {1} = {2}", x, y, Add(x, y));
            Console.WriteLine();
            x = 95; y = 23;
            Console.WriteLine("{0} + {1} = {2}", x, y, Add(x, y));
            Console.WriteLine();
        }

        // Q9: compare two integer without comparator ">" or "<", return the larger number
        // trick: use the sign of signed interger
		public static int Compare(int x, int y)
        {
            int diff = x - y;
            return (diff >> 31 & 1) == 0 ? x : y;
        }
		public static void CompareTest()
        {
            int x = 100, y = 23;
            int result = Compare(x, y);
            string op;
            if (result > 0) op = ">";
            else if (result == 0) op = "=";
            else op = "<";
            Console.WriteLine("{0} {1} {2}", x, op, y);
            Console.WriteLine();
        }

        // Q10: swap two integer without temperary variable
        static int x = 100, y = 23;
		public static void SwapNum_XOR()
        { // XOR trick
            x = x ^ y;
            y = x ^ y;
            x = x ^ y;
        }
		public static void SwapNum_PlusMinus()
        {
            x = x - y;
            y = x + y;
            x = y - x;
        }
		public static void SwapNum_PlusMinus_2()
        {
            x = x + y;
            y = x - y;
            x = x - y;
        }
		public static void SwapNumTest()
        {
            Console.WriteLine("Before Swap: x = {0}, y = {1}", x, y);
            SwapNum_XOR();
            Console.WriteLine("SwapNum: x = {0}, y = {1}", x, y);
            x = 100;
            y = 23;
            SwapNum_PlusMinus();
            Console.WriteLine("SwapNum2: x = {0}, y = {1}", x, y);
            x = 100;
            y = 23;
            SwapNum_PlusMinus_2();
            Console.WriteLine("SwapNum3: x = {0}, y = {1}", x, y);
            Console.WriteLine();
        }


    }

	/// <summary>
	/// Question 34
	/// Given an array, of which two numbers appear once and other numbers appear exact twice
	/// Find the two number that appear once, return true if they are found
	/// </summary>
	public class TwoUniqueNumber
	{
		// 1. XOR the whole array
		// 2. find the right-most kth bit-one in the XOR result
		// 3. divide the array into two subarray: if the kth bit is 1 put in first subarray, if not put in the second subarray
 		// 4. xor all the element in first subarray, the result is the first unique number
		//    xor all the element in second subarray, the result is the second unique number
		public static bool FindTwoUniqueNumbers(int[] a, out int num1, out int num2)
		{
			int xorAll = 0;
			for (int i = 0; i < a.Length; i++) 
				xorAll ^= a[i];
			int rightBitOne = 0; // find the right-most bit one
			while (true)
			{
				if (((xorAll >> rightBitOne++) & 1) == 1) break;
				if (rightBitOne >= 31) // IMPORTANT!!! checking the boundary case!
				{
					num1 = num2 = 0;
					return false;
				}
			}
            num1 = 0; num2 = 0;
            for (int i = 0; i < a.Length; i++)
			{
                if (((a[i] >> (rightBitOne - 1) & 1) == 1))
                    num1 ^= a[i]; 
                else
                    num2 ^= a[i]; 
			}
            if (num1 == num2) return false;
            else return true;
		}

		public static void Test()
		{
			int[] a = { 1, 2, 1, 2, 3, 4, 5, 6, 6, 5, 4, 7, 8, 8, 9, 9 };
			FindTwoUniqueNumbersTest(a);
			int[] a2 = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
			FindTwoUniqueNumbersTest(a2);
		}

		public static void FindTwoUniqueNumbersTest(int[] a)
		{
			Console.Write("array: ");
			foreach (int i in a) Console.Write(i + " ");
			Console.WriteLine();
			int num1, num2;
			bool found = FindTwoUniqueNumbers(a, out num1, out num2);
			if (found)
				Console.WriteLine("num1 = {0}, num2 = {1}", num1, num2);
			else
				Console.WriteLine("Not Found!");
		}

	}

    /// <summary>
    /// Careercup 5.2
    /// Given a string representing a decimal number, print the binary represetation
    /// </summary>
    public class BinaryConversion
    {
        public static string ToBinary(string s)
        {
            return null;
        }

        public static void Test()
        {
            string s = "0.625";
            Console.WriteLine(s + " -> " + ToBinary(s));   
        }
    }

    /// <summary>
    /// Careercup 5.5
    /// Count how many bits are needed to toggle to convert inteter a into integer b
    /// </summary>
    public class BitDiff
    {
        // count number of 1 of the xor result of the two number
        public static int BitsDiffCount(int a, int b)
        {
            int xor = a ^ b;
            int count = 0;
            while (xor != 0)
            {
                xor &= xor - 1;
                count++;
            }
            return count;
        }

        public static void Test()
        {
            int a = 10;
            int b = 23;
            int c = BitsDiffCount(a, b);
            Console.WriteLine("a = {0}, b = {1}, count = {2}", 
                Convert.ToString(a, 2), Convert.ToString(b, 2), c);
        }
    }

    /// <summary>
    /// Leetcode
    /// Given two binary strings, return their sum (also a binary string). 
    /// For example, a = "11", b = "1", Return "100". 
    /// </summary>
    public class BinaryAddition
    {
        /*
        public static string Add(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1)) return s2;
            if (string.IsNullOrEmpty(s2)) return s1;
            string longer = s1.Length >= s2.Length ? s1 : s2;
            string shorter = s1.Length < s2.Length ? s1 : s2;
            for (int i = 0; i < longer.Length - shorter.Length; i++)
            {
                shorter = "0" + shorter;
            }
            string xor = "";
            for (int i = 0; i < longer.Length; i++)
            {
                if (longer[i] != shorter[i]) xor += "1";
                else xor += "0";
            }
            bool carry = false;
            string result = "";
            for (int i = longer.Length - 1; i >= 0; i--)
            {
                if (i == longer.Length - 1) result += xor[i];
                else {
                    if (longer[i] == '1' && shorter[i] == '1') // carry '1'
                    {
                        if (xor[i] == '1')
                        { result = '1' + result; carry = true; }
                        else
                        { result = '1' + result; carry = false; }
                    }
                    else // no carry
                    { 
                        
                    }
                }
            }

        }


        public static void Test()
        {         
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = r.Next();
                int y = r.Next();
                //int z = x + y;
                Console.WriteLine(x + " + " + y + " = " + Add(x.ToString(), y.ToString()));
                Debug.Assert((x + y).ToString() == Add(x.ToString(), y.ToString())); 
            }
        }
        */
    }

    /// <summary>
    /// You have given n numbers from 1 to n. You have to sort numbers with increasing number of set bits.
    /// If you have two number with equal number of set bits, then number with lowest value come first in the output.
    /// for ex: n=5. output: 1,2,4,3,5
    /// </summary>
    public class SetBitsSorting
    {

        // (1) traverse through the number, for each number, reset the lowest bit using n & (n - 1), 
        // if the number becomes zero, append it to the result list. 
        // (2) repeat (1) until the result list length equals to n
        public static void SortBySetBits(int n)
        {
            int[] nums = new int[n];
            for (int i = 1; i <= nums.Length; i++)
            {
                nums[i - 1] = i;
            }
            int zeros = 0;
            while (zeros < n)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] != 0)
                    {
                        nums[i] = nums[i] & (nums[i] - 1);
                        if (nums[i] == 0)
                        {
                            zeros++;
                            Console.Write((i + 1) + " ");
                        }
                    }
                }
            }
        }
        

        public static void Test()
        {
            SortBySetBits(64);
        }
    
    }












}