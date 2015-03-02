using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem094 : EulerProblem
  {
    public override int Number
    {
      get { return 94; }
    }

    public override object Solve()
    {
      // basically an ALMOST equilateral triangle with integral length
      // is a primitive pythagorean triple. So, the almost equilateral
      // triangle is 5-5-6. The area is 12. So, the two sides of the triangle
      // are 5, and the bottom is 6. 6/2 is 3. This gives us the 3 as a, and 5
      // as c. 3^2 + b^2 = 5^2, telling us that b is 4. So, if b is 4, that means
      // the height is 4. 1/2base*height gives us (6*4)/2 = 12. Which is the area.
      // Therefore, we're looking for all pythagorean triples that can be transformed
      // into an almost equilateral triangle. Using the triple {3,4,5}, we take a*2 (6)
      // to get the triangle 5-5-6 (we ignore 4, the height).
      const int MaxPerimeter = 1000000000;

      // calculate the maximum limit
      var limit = (int)Math.Sqrt(MaxPerimeter / 2) / 2;

      // pre-generate the m values
      var values = new int[limit];
      for (var i = 1; i < limit; ++i)
      {
        values[i] = i * i;
      }

      // create a variable to hold the sum
      var sum = 0;

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

          // calculate the primitive triple
          var x = 2 * n * m;
          var y = mm - nn;
          var sameSide = mm + nn;

          // calculate the sides of the triangle
          var a = (x < y) ? x : y;
          var oneSide = a << 1;
          if (Math.Abs(oneSide - sameSide) != 1) { continue; }

          // calculate the perimeter
          var perimeter = oneSide + (sameSide << 1);
          if (perimeter > MaxPerimeter) { break; }

          // add the perimeter to the sum
          sum += perimeter;
        }
      }

      return sum;
    }
  }
}