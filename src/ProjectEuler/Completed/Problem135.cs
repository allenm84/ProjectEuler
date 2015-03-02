using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem135 : EulerProblem
  {
    public override int Number
    {
      get { return 135; }
    }

    public override object Solve()
    {
      //return v1RetrieveSolutionCount(1000000, 10);
      return v2RetrieveSolutionCount(1000000, 10);
    }

    /// <summary>
    /// Given the equation x^2 - y^2 - z^2 = n where x, y, and z are positive consecutive integers,
    /// each value of n has S>=0 solutions. This function will return the number of solutions that are
    /// equal to the targetSolutionCount and n is less than maximum;
    /// </summary>
    protected uint v2RetrieveSolutionCount(uint maximumN, uint targetSolutionCount)
    {
      /* Taken from the project euler forums for p135 */
      var z = new int[maximumN + 1];
      for (uint p = 2; p <= maximumN; p++)
      {
        var a = 4 - p % 4;
        uint n = 0;
        for (uint v = 0; v < (3 * p - a) / 4; v++)
        {
          n = p * (4 * v + a);
          if (n <= maximumN) { z[n]++; }
          else
          { v = maximumN; }
        }
      }

      uint x = 0;
      for (uint i = 2; i <= maximumN; i++)
      {
        if (z[i] == targetSolutionCount) { x++; }
      }

      return x;
    }

    /// <summary>
    /// Given the equation x^2 - y^2 - z^2 = n where x, y, and z are positive consecutive integers,
    /// each value of n has S>=0 solutions. This function will return the number of solutions that are
    /// equal to the targetSolutionCount and n is less than maximum;
    /// </summary>
    protected uint v1RetrieveSolutionCount(uint maximumN, uint targetSolutionCount)
    {
      /*
       * This one was quite a doozy. It should be noted that we're really solving the equation: 3c^2 + 2cz - z^2 = n 
       * where "c" is the increment amount and "n" is the iterating n value. Then, it basically becomes solving for z.
       * This can be done using brute force and iterating over a c values. When given a c value, we can solve the equation
       * for z: z = c - Sqrt[4c^2 - n] || z = c + Sqrt[4c^2 - n]. OR, z = c - D || z = c + D where "D" is Sqrt[4c^2 - n].
       * I created an excel spreadsheet to get the two values of z from (4*c*c). I noticed that if (4*c*c) is a perfect square
       * then there will be 1 <= S <= 2 solutions. So, I used wolfram alpha to get an equation for the (4*c*c) values for
       * each iterating value of c. The formula ALWAYS came out to: 4n^2 + 4kn - p. I created a text file to generate all the 
       * formulas for an n value and noticed that each formula progressesd in a linear way. Thus, iterating over an n value, you 
       * can get the coefficients for the quadratic formula. I asked on math stack exchange if there was a way to determine for 
       * what values of "n" the equation 4n^2 + 4kn - p will generate a perfect square. I got a general method of figuring this 
       * out (see below). From there, it was a matter of determining the coefficient for "b" (in order to complete the square),
       * and then using that to find k and m. From there, I can determine what "c" is in the equation 3c^2 + 2cz - z^2 = n and
       * thus solve for z using z = c - D || z = c + D.
       */

      uint b = 0;
      uint c = 1;
      uint cLimit = 4;
      uint retval = 0;
      var nt = new NumberTheory(maximumN);

      for (uint n = 1; n < maximumN; ++n, Update(ref b, ref c, ref cLimit))
      {
        // Here you can complete the square, set the quadratic equal to an unknown m^2, rearrange and factor, 
        // (2n+a+m)(2n+a−m)=b, then look at the prime factorization of b and check when k=(d+b/d−2a)/4 is an 
        // integer for not-necessarily-positive divisors d|b, in which case n=k and m=±(d−b/d)/2.
        uint count = 0;
        if (nt.IsPrime(n)) { continue; }
        var mValues = new HashSet<int>();
        var v = b / 2.0;
        var fs = nt.DivisorsOf(n);
        foreach (var f in fs)
        {
          var ds = new[] {f, -(double)f};
          foreach (var d in ds)
          {
            var k = (d + (n / d) - v) / 4.0;
            if (k.IsInteger())
            {
              var m = (d - (n / d)) / 2.0;
              if (m.IsInteger())
              {
                mValues.Add((int)m);
              }
            }
          }
        }

        // from the m value, we need to determine what k is in the equation:
        // 3k^2 + 2kz - z^2 = n
        foreach (var m in mValues)
        {
          var lm = (long)m;
          var d = Math.Sqrt(((lm * lm) + n) / 4);
          if (d.IsInteger())
          {
            var k = (long)d;
            var z = m + k;
            if (z <= 0) { continue; }
            ++count;
            if (count > targetSolutionCount) { break; }
          }
        }

        if (count == targetSolutionCount)
        {
          ++retval;
        }
      }

      return retval;
    }

    private void Update(ref uint b, ref uint c, ref uint cLimit)
    {
      ++c;
      if (c == cLimit)
      {
        c = 0;
        b += 8;
        cLimit += 8;
      }
    }
  }
}