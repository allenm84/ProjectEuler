using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem066 : Problem064
  {
    public override int Number
    {
      get { return 66; }
    }

    public override object Solve()
    {
      // http://en.wikipedia.org/wiki/Pell's_equation
      // http://en.wikipedia.org/wiki/Fundamental_recurrence_formulas

      // keep a table of perfect squares
      var squares = new Dictionary<int, bool>();
      for (var i = 1; i <= 100; ++i)
      {
        squares[(i * i)] = true;
      }

      // we store the maximum A value and the D value associated with it
      BigInteger maxA = 0;
      var maxD = 0;

      // go through the D values
      for (var D = 1; D <= 1000; ++D)
      {
        // if this is a perfect square, then continue
        if (squares.ContainsKey(D)) { continue; }
        var sqrt = GetSqrt(D).ToArray();

        // k is odd, continue
        if (sqrt.Length % 2 == 1) { continue; }

        var Av = new BigInteger[2];
        var Bv = new BigInteger[2];

        // use the fundamental recurrence formula to find the (x,y) values that
        // satisfy the equation. Here, an is always 1 since we're taking the square root.
        for (var n = 0; n < sqrt.Length; ++n)
        {
          var b = sqrt[n];

          BigInteger A, B;
          if (n == 0)
          {
            A = b;
            B = 1;
          }
          else if (n == 1)
          {
            A = (b * sqrt[0]) + 1;
            B = b;
          }
          else
          {
            A = (b * Av[1]) + (Av[0]);
            B = (b * Bv[1]) + (Bv[0]);
          }

          if (IsSolution(D, A, B))
          {
            if (A > maxA)
            {
              maxA = A;
              maxD = D;
            }
            break;
          }

          Av[0] = Av[1];
          Bv[0] = Bv[1];

          Av[1] = A;
          Bv[1] = B;
        }
      }

      return maxD;
    }

    private bool IsSolution(BigInteger D, BigInteger x, BigInteger y)
    {
      var x2 = (x * x);
      var Dy2 = D * y * y;
      return BigInteger.Abs((x2 - Dy2)) == BigInteger.One;
    }
  }
}