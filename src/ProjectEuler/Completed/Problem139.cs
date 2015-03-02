using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem139 : EulerProblem
  {
    public override int Number
    {
      get { return 139; }
    }

    public override object Solve()
    {
      const int MaxPerimeter = 100000000;

      // calculate the maximum limit
      var limit = (int)Math.Sqrt(MaxPerimeter / 2);

      // pre-generate the m values
      var values = new int[limit];
      for (var i = 1; i < limit; ++i)
      {
        values[i] = i * i;
      }

      // create a variable to store the count
      BigInteger count = 0;

      // generate the valid pythagorean triples
      for (var n = 1; n < limit; ++n)
      {
        // calculate n*n
        var nn = values[n];

        for (var m = n + 1; m < limit; ++m)
        {
          // if not odd, then keep going
          if ((n + m) % 2 == 0) { continue; }

          // if not coprime, then keep going
          if (MathHelper.GCD(m, n) != 1) { continue; }

          // calculate m*m
          var mm = values[m];

          // calculate a,b and c
          var a = mm - nn;
          var b = 2 * m * n;
          var c = mm + nn;

          // now, cycle through the k values
          for (var k = 1;; ++k)
          {
            // calculate a,b and c
            var ak = a * k;
            var bk = b * k;
            var ck = c * k;

            // determine the perimeter
            if ((ak + bk + ck) >= MaxPerimeter) { break; }

            // subtract the two
            var d = Math.Abs(bk - ak);

            // is this valid?
            if ((ck % d) == 0) { ++count; }
          }
        }
      }

      // return the count
      return count;
    }
  }
}