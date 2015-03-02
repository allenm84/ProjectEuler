using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Common.Extensions;

namespace ProjectEuler
{
  public static class MathHelper
  {
    const string IntMin = "negative two billion one hundred and forty seven million four hundred and eighty three thousand six hundred and forty eight";
    const string IntMax = "two billion one hundred and forty seven million four hundred and eighty three thousand six hundred and forty seven";
    const string IntZero = "Zero";

    static string[] ones = "one,two,three,four,five,six,seven,eight,nine".Split(',');
    static string[] teens = "ten,eleven,twelve,thirteen,fourteen,fifteen,sixteen,seventeen,eighteen,nineteen".Split(',');
    static string[] tens = "twenty,thirty,forty,fifty,sixty,seventy,eighty,ninety".Split(',');
    static string[] tmbs = ",thousand,million,billion".Split(',');
    static MathHelper() { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nmax"></param>
    /// <returns></returns>
    public static IEnumerable<int> Eratosthenes(int nmax)
    {
      var sieve = new BitArray((nmax / 2) + 1, true);
      var upper = (int)Math.Sqrt(nmax);
      if (nmax > 1) yield return 2;

      var m = 1;
      while (2 * m + 1 <= nmax)
      {
        if (sieve[m])
        {
          var n = 2 * m + 1;
          if (n <= upper)
          {
            var i = m;
            while (2 * i < nmax)
            {
              sieve[i] = false;
              i += n;
            }
          }
          yield return n;
        }
        ++m;
      }
    }

    /// <summary>
    /// For a given value n, returns the pair of squares needed to equal n. For instance, for 1010, this
    /// will return {7,31} and {13,29}
    /// </summary>
    public static IEnumerable<int[]> GetSquares(int n)
    {
      int x = 0, y = 0, sos = 0;
      while (sos < n)
      {
        sos += 2 * x + 1;
        ++x;
      }

      while (x >= y)
      {
        if (sos < n)
        {
          sos += 2 * y + 1;
          ++y;
        }
        else if (sos > n)
        {
          sos -= 2 * x - 1;
          --x;
        }
        else
        {
          yield return new int[] { x, y };
          sos += 2 * (y - x + 1);
          ++y;
          --x;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static uint Sqrt(uint number)
    {
      var op = number;
      var res = 0u;
      var one = 1u << 30; // The second-to-top bit is set: use 1u << 14 for uint16_t type; use 1uL<<30 for uint32_t type

      // "one" starts at the highest power of four <= than the argument.
      while (one > op)
      {
        one >>= 2;
      }

      while (one != 0)
      {
        if (op >= res + one)
        {
          op = op - (res + one);
          res = res + 2 * one;
        }
        res >>= 1;
        one >>= 2;
      }

      /* Do arithmetic rounding to nearest integer */
      if (op > res)
      {
        res++;
      }

      return res;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static ulong Sqrt(ulong number)
    {
      var op = number;
      var res = 0UL;
      var one = 1UL << 62; // The second-to-top bit is set: use 1u << 14 for uint16_t type; use 1uL<<30 for uint32_t type

      // "one" starts at the highest power of four <= than the argument.
      while (one > op)
      {
        one >>= 2;
      }

      while (one != 0)
      {
        if (op >= res + one)
        {
          op = op - (res + one);
          res = res + 2 * one;
        }
        res >>= 1;
        one >>= 2;
      }

      /* Do arithmetic rounding to nearest integer */
      if (op > res)
      {
        res++;
      }

      return res;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static BigInteger Sqrt(BigInteger number)
    {
      // problem with lower numbers to to right bit shift
      int bitLength = number.ToByteArray().Length * 8 + 1;
      BigInteger G = number >> bitLength / 2;
      BigInteger LastG = BigInteger.Zero;
      BigInteger One = new BigInteger(1);
      while (true)
      {
        LastG = G;
        G = (number / G + G) >> 1;
        int i = G.CompareTo(LastG);
        if (i == 0) return G;
        if (i < 0)
        {
          if ((LastG - G).CompareTo(One) == 0)
            if ((G * G).CompareTo(number) < 0 &&
              (LastG * LastG).CompareTo(number) > 0) return G;
        }
        else
        {
          if ((G - LastG).CompareTo(One) == 0)
            if ((LastG * LastG).CompareTo(number) < 0 &&
              ((G * G).CompareTo(number) > 0)) return LastG;
        }
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Tuple<BigInteger, BigInteger> ExtGCD(BigInteger a, BigInteger b)
    {
      BigInteger x = 0;
      BigInteger lastx = 1;
      BigInteger y = 1;
      BigInteger lasty = 0;
      while (b != 0)
      {
        BigInteger quotient = a / b;

        BigInteger[] ab = new BigInteger[2];
        ab[0] = b;
        ab[1] = a % b;
        a = ab[0];
        b = ab[1];

        BigInteger[] lxx = new BigInteger[2];
        lxx[0] = lastx - quotient * x;
        lxx[1] = x;
        x = lxx[0];
        lastx = lxx[1];

        BigInteger[] lyy = new BigInteger[2];
        lyy[0] = lasty - quotient * y;
        lyy[1] = y;
        y = lyy[0];
        lasty = lyy[1];
      }

      return new Tuple<BigInteger, BigInteger>(lastx, lasty);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static bool IsPerfectCube(ulong x)
    {
      ulong r = CubeRoot(x);
      return (r * r * r) == x;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="m2"></param>
    /// <returns></returns>
    public static bool IsPerfectSquare(long x)
    {
      var root = Math.Sqrt(x);
      return root.IsInteger();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static uint CubeRoot(ulong x)
    {
      int s;
      uint y;
      ulong b;

      y = 0;
      for (s = 63; s >= 0; s -= 3)
      {
        y += y;
        b = 3 * y * ((ulong)y + 1) + 1;
        if ((x >> s) >= b)
        {
          x -= b << s;
          y++;
        }
      }
      return y;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="exponent"></param>
    /// <param name="modulus"></param>
    /// <returns></returns>
    public static int ModPow(int value, int exponent, int modulus)
    {
      int result = 1;
      while (exponent > 0)
      {
        if ((exponent & 1) == 1)
        {
          result = (result * value) % modulus;
        }
        exponent >>= 1;
        value = (value * value) % modulus;
      }
      return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static bool GaussianSolve(double[][] matrix, int n, out double[] x)
    {
      int i, j, k, maxrow;
      double tmp;
      x = new double[n];

      for (i = 0; i < n; i++)
      {
        /* Find the row with the largest first value */
        maxrow = i;
        for (j = i + 1; j < n; j++)
        {
          if (Math.Abs(matrix[i][j]) > Math.Abs(matrix[i][maxrow]))
            maxrow = j;
        }

        /* Swap the maxrow and ith row */
        for (k = i; k < n + 1; k++)
        {
          tmp = matrix[k][i];
          matrix[k][i] = matrix[k][maxrow];
          matrix[k][maxrow] = tmp;
        }

        /* Singular matrix? */
        if (matrix[i][i] == 0.0)
          return false;

        /* Eliminate the ith element of the jth row */
        for (j = i + 1; j < n; j++)
        {
          for (k = n; k >= i; k--)
          {
            matrix[k][j] -= matrix[k][i] * matrix[i][j] / matrix[i][i];
          }
        }
      }

      /* Do the back substitution */
      for (j = n - 1; j >= 0; j--)
      {
        tmp = 0;
        for (k = j + 1; k < n; k++)
          tmp += matrix[k][j] * x[k];
        x[j] = (matrix[n][j] - tmp) / matrix[j][j];
      }

      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static double DblDiv(double n, double d)
    {
      return n / d;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static float SngDiv(float n, float d)
    {
      return n / d;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    public static decimal DecDiv(decimal n, decimal d)
    {
      return decimal.Divide(n, d);
    }

    /// <summary>
    /// Euler's totient function
    /// </summary>
    public static int Phi(int n)
    {
      if (n <= 1) return n == 1 ? 1 : 0;
      int phi = n;

      if (n % 2 == 0) { phi /= 2; n /= 2; while (n % 2 == 0) n /= 2; }
      if (n % 3 == 0) { phi -= phi / 3; n /= 3; while (n % 3 == 0) n /= 3; }

      for (int p = 5; p * p <= n; )
      {
        if (n % p == 0) { phi -= phi / p; n /= p; while (n % p == 0) n /= p; }
        p += 2;

        if (p * p > n) break;
        if (n % p == 0) { phi -= phi / p; n /= p; while (n % p == 0) n /= p; }
        p += 4;
      }

      if (n > 1) phi -= phi / n;
      return phi;
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> Primes()
    {
      for (int i = 2; i < int.MaxValue; ++i)
      {
        if (i.IsPrime())
          yield return i;
      }
    }

    /// <summary>
    /// Calculates the triangle number at index n
    /// </summary>
    public static int FTriangle(int n)
    {
      return ((n * (n + 1)) / 2);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> TriangleNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FTriangle(n);
    }

    /// <summary>
    /// Calculates the square number at index n
    /// </summary>
    public static int FSquare(int n)
    {
      return (n * n);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> SquareNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FSquare(n);
    }

    /// <summary>
    /// Calculates the pentagon number at index n
    /// </summary>
    public static int FPentagon(int n)
    {
      return ((n * ((3 * n) - 1)) / 2);
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> PentagonNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FPentagon(n);
    }

    /// <summary>
    /// Calculates the hexagon number at index n
    /// </summary>
    public static int FHexagon(int n)
    {
      return (n * ((2 * n) - 1));
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> HexagonNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FHexagon(n);
    }

    /// <summary>
    /// Calculates the heptagon number at index n
    /// </summary>
    public static int FHeptagon(int n)
    {
      return (n * ((5 * n) - 3)) / 2;
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> HeptagonNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FHeptagon(n);
    }

    /// <summary>
    /// Calculates the optagon number at index n
    /// </summary>
    public static int FOctagon(int n)
    {
      return (n * ((3 * n) - 2));
    }

    /// <summary>
    /// 
    /// </summary>
    public static IEnumerable<int> OctagonNumbers()
    {
      for (int n = 0; n < int.MaxValue; ++n)
        yield return MathHelper.FOctagon(n);
    }

    /// <summary>
    /// Returns true if the values is pentagonal
    /// </summary>
    public static bool IsPentagonal(long x)
    {
      double n = (Math.Sqrt((24.0 * x) + 1.0) + 1.0) / 6.0;
      return (n > 0) && (n == Math.Floor(n));
    }

    /// <summary>
    /// Returns true if the values is hexagonal
    /// </summary>
    public static bool IsHexagonal(long x)
    {
      double n = (Math.Sqrt((8.0 * x) + 1.0) + 1.0) / 4.0;
      return (n > 0) && (n == Math.Floor(n));
    }

    /// <summary>
    /// Returns the greatest common denominator of two numbers.
    /// </summary>
    public static decimal GCD(decimal a, decimal b)
    {
      if (b == 0) return a;
      return GCD(b, a % b);
    }

    /// <summary>
    /// Returns the greatest common denominator of two numbers.
    /// </summary>
    public static int GCD(int a, int b)
    {
      if (b == 0) return a;
      return GCD(b, a % b);
    }

    /// <summary>
    /// 
    /// </summary>
    public static List<BigInteger> AllPartitionsUpTo(int max)
    {
      List<BigInteger> pt = new List<BigInteger> { 1 };
      for (int n = 1; n <= max; ++n)
      {
        BigInteger r = 0;
        int f = -1;

        int i = 0;
        while (true)
        {
          int k = gpent(i);
          if (k > n) break;

          if ((i % 2) == 0) f = -f;
          r += f * pt[n - k];
          ++i;
        }

        pt.Add(r);
      }
      return pt;
    }

    private static int gpent(int n)
    {
      if (n < 0) return 0;
      if ((n % 2) == 0) return FPentagon(n / 2 + 1);
      else return FPentagon(-(n / 2 + 1));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int CountParitions(int n)
    {
      if (n < 0) return 0;
      if (n == 0) return 1;

      int n2 = n >> 1;
      int sum = 1;

      var table = new Dictionary<int, Dictionary<int, int>>();
      for (int k = 1; k <= n2; ++k)
      {
        sum += p(k, n - k, ref table);
      }

      return sum;
    }

    private static int p(int k, int n, ref Dictionary<int, Dictionary<int, int>> table)
    {
      if (k > n) { return 0; }
      if (k == n) { return 1; }

      Dictionary<int, int> ktable;
      if (!table.TryGetValue(k, out ktable))
      {
        ktable = new Dictionary<int, int>();
        table[k] = ktable;
      }

      int cnt;
      if (!ktable.TryGetValue(n, out cnt))
      {
        cnt = p(k + 1, n, ref table) + p(k, n - k, ref table);
        ktable[n] = cnt;
      }

      return cnt;
    }

    /// <summary>
    /// Counts the number of different possible ways to make up a value given
    /// coins.  For example, to find out how to make $1.00 using only
    /// $0.05 and $0.10, you could pass in value="100" and coints="5,10". 100 cents
    /// is the same as $1.00.
    /// </summary>
    /// <param name="value">The value to get to (in the same unit as the coins).</param>
    /// <param name="coins">The coin values.</param>
    /// <returns>The number of ways to get to the value using the coins.</returns>
    public static int CoinChangeCount(int value, int[] coins)
    {
      var table = new Dictionary<int, Dictionary<int, int>>();
      return coinchange(value, coins.Length - 1, ref coins, ref table);
    }

    private static int coinchange(int n, int m, ref int[] S, ref Dictionary<int, Dictionary<int, int>> table)
    {
      if (n == 0) { return 1; }
      if (n < 0) { return 0; }
      if (m < 0 && n >= 1) { return 0; }

      Dictionary<int, int> mtable;
      if (!table.TryGetValue(n, out mtable))
      {
        mtable = new Dictionary<int, int>();
        table[n] = mtable;
      }

      int cnt;
      if (!mtable.TryGetValue(m, out cnt))
      {
        cnt = coinchange(n, m - 1, ref S, ref table) + coinchange(n - S[m], m, ref S, ref table);
        mtable[m] = cnt;
      }

      return cnt;
    }

    private static string nnn2words(int iNum)
    {
      int i = iNum % 10;
      string s = string.Empty;

      if (i > 0)
      {
        s = ones[i - 1];
      }

      int ii = (iNum % 100) / 10;
      if (ii == 1)
      {
        s = teens[i];
      }
      else if (1 < ii && ii < 10)
      {
        s = string.Concat(tens[ii - 2], " ", s);
      }

      i = (iNum / 100) % 10;
      if (i > 0)
      {
        s = string.Concat(ones[i - 1], " hundred and ", s);
      }
      return s;
    }

    /// <summary>
    /// Converts a number to its string representation. Does not use hyphens (-) or commas (,).
    /// </summary>
    /// <param name="number">The number to use.</param>
    /// <returns>The string representation of a number.</returns>
    public static string NumberToText(int number)
    {
      string s = string.Empty;
      long l = Math.Abs((long)number);

      bool negative = number < 0;
      if (l == 0)
      {
        s = IntZero;
      }
      else if (l == int.MinValue)
      {
        s = IntMin;
        negative = false;
      }
      else if (l == int.MaxValue)
      {
        s = IntMax;
      }
      else if (l > int.MaxValue)
      {
        throw new ArgumentOutOfRangeException("number");
      }
      else
      {
        int i = Math.Abs(number);
        for (int j = 0; j < tmbs.Length; ++j)
        {
          var iii = i % 1000;
          i /= 1000;
          if (iii > 0)
          {
            s = string.Concat(nnn2words(iii), " ", tmbs[j], " ", s);
          }
        }
      }

      if (negative)
      {
        s = string.Concat("negative ", s);
      }
      return s.Trim();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public static BigInteger nCr(int n, int r)
    {
      return Fact(n) / (Fact(r) * (Fact(n - r)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static BigInteger Fact(int n)
    {
      if (n < 2) return 1;

      var res = new BigInteger(n);
      while ((--n) > 0)
      {
        res *= n;
      }
      return res;
    }

    /// <summary>
    /// 
    /// </summary>
    public static double[] SolveQuadratic(double a, double b, double c)
    {
      double sqrtPart = Math.Sqrt((b * b) - (4 * a * c));
      double twoA = 2 * a;
      return new double[]
      {
        (b + sqrtPart)/twoA,
        (b - sqrtPart)/twoA,
      };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int SumOfDivisors(int num)
    {
      int sum = 0;
      int r = (int)Math.Sqrt(num);
      if (r * r == num) //case that n is a perfect square
      {
        sum += r;
        r -= 1;
      }
      if (num % 2 != 0)
      {
        //number is odd
        for (int i = 1; i <= r; i += 2)
        {
          if (num % i == 0)
            sum += i + num / i;
        }
      }
      else
      {
        //number is even
        for (int i = 1; i <= r; i += 1)
        {
          if (num % i == 0)
            sum += i + num / i;
        }
      }
      return sum;
    }
  }
}
