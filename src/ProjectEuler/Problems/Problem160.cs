using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem160 : EulerProblem
  {
    public override int Number
    {
      get { return 160; }
    }

    public override object Solve()
    {
      // When n is a positive multiple of 2500, f(n) = f(n * 5^x) for all x >= 0
      const long N = 2560000;

      BigInteger value = 1;
      var max = BigInteger.Pow(10, 5);

      for (long i = 1; i <= N; ++i)
      {
        value *= i;
        if (value > max)
        {
          if (value % 10 == 0)
          {
            value /= 10;
          }
          else
          {
            value %= max;
          }
        }
      }

      while (value % 10 == 0)
      {
        value /= 10;
      }
      return value % max;
    }
  }
}