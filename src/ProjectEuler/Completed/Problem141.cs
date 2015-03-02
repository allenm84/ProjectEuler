using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem141 : EulerProblem
  {
    public override int Number
    {
      get { return 141; }
    }

    public override object Solve()
    {
      const long Max = 1000000000000;

      // this took ~ 2 days
      //return v1Solution(Max);

      // solution from sfabriz
      return v2Solution(Max);
    }

    private object v2Solution(long max)
    {
      long sum = 0;
      long stop = 10000;
      for (long a = 2; a < stop; a++)
      {
        for (long b = 1; b < a; b++)
        {
          if (a * a * a * b + b * b >= max) { break; }
          if (MathHelper.GCD(a, b) > 1)
          {
            continue;
          }

          for (long c = 1;; c++)
          {
            var m2 = c * c * a * a * a * b + c * b * b;
            if (m2 >= max) { break; }
            if (MathHelper.IsPerfectSquare(m2))
            {
              sum += m2;
            }
          }
        }
      }
      return sum;
    }

    private object v1Solution(long max)
    {
      var SqrtMax = (long)Math.Sqrt(max);
      var sum = BigInteger.Zero;

      for (long i = 1; i < SqrtMax; ++i)
      {
        var square = i * i;
        if (IsProgressive(square, i))
        {
          sum += square;
        }

        if ((i % 1000) == 0)
        {
          AddMessage("{0}", i);
        }
      }

      return sum;
    }

    private bool IsProgressive(long value, long max)
    {
      for (long d = 2; d <= max; ++d)
      {
        var r = value % d;
        if (r == 0) { continue; }

        var q = value / d;

        var sequence = new double[] {d, q, r};
        Array.Sort(sequence);

        var r1 = sequence[1] / sequence[0];
        var r2 = sequence[2] / sequence[1];
        if (r1 == r2) { return true; }
      }
      return false;
    }
  }
}