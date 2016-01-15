using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0037 : euler
  {
    static int[] sPow = { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 };

    public override void Run()
    {
      int sum = Candidates()
        .Where(IsTruncatable)
        .Take(11)
        .Sum();

      Console.WriteLine(sum);
    }

    public static IEnumerable<int> Candidates()
    {
      // 2, 3, 5, and 7 are not valid
      for (int i = 8; i < int.MaxValue; ++i)
      {
        yield return i;
      }
    }

    public static bool IsTruncatable(int n)
    {
      if (!math.isPrime(n)) return false;

      // go through the lengths of the numbers truncating the digits
      int p, t, pow, len = math.numDigits(n);
      for (p = len - 1; p > 0; --p)
      {
        // load the power of 10
        pow = sPow[p];

        // truncate the first digit
        t = n % pow;
        if (!math.isPrime(t)) return false;

        // truncate the last digit
        t = n / pow;
        if (!math.isPrime(t)) return false;
      }

      return true;
    }
  }
}
