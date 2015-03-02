using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem123 : EulerProblem
  {
    public override int Number
    {
      get { return 123; }
    }

    public override object Solve()
    {
      // using a brute force solution, the following values were generated:
      /*
       * n=1 p=2 rem=0
       * n=2 p=3 rem=2
       * n=3 p=5 rem=5
       * n=4 p=7 rem=2
       * n=5 p=11 rem=110
       * n=6 p=13 rem=2
       * n=7 p=17 rem=238
       * n=8 p=19 rem=2
       * n=9 p=23 rem=414
       * n=10 p=29 rem=2
       * n=11 p=31 rem=682
       * n=12 p=37 rem=2
       * n=13 p=41 rem=1066
       * n=14 p=43 rem=2
       * n=15 p=47 rem=1410
       * ... <truncated> ...
       */

      // starting at n=5, we see the following pattern:
      // if n%2 == 0, then rem=2
      // if n%2 == 1, then rem = n*p*2

      var max = BigInteger.Pow(10, 10);
      var n = 5;

      for (long p = 12;; ++p)
      {
        if (p.IsPrime())
        {
          ++n;
          if (n % 2 == 0) { continue; }
          var rem = p * n;
          rem <<= 1;
          if (rem > max)
          {
            return n;
          }
        }
      }
    }

    private static void BruteForce()
    {
      var n = 0;
      long p = 2;

      using (Stream stream = File.Open("prob123.txt", FileMode.Create))
      {
        var writer = new StreamWriter(stream) {AutoFlush = true};
        var max = BigInteger.Pow(10, 5);
        for (;; p++)
        {
          if (p.IsPrime())
          {
            ++n;

            BigInteger pn_m_1 = p - 1;
            BigInteger pn_p_1 = p + 1;

            var pm2 = BigInteger.Pow(pn_m_1, n);
            var pp2 = BigInteger.Pow(pn_p_1, n);

            var sum = pm2 + pp2;
            var pn2 = p * p;

            var rem = sum % pn2;
            writer.WriteLine("n={0} p={1} rem={2}", n, p, rem);

            if (rem > max)
            {
              break;
            }
          }
        }
      }
    }
  }
}