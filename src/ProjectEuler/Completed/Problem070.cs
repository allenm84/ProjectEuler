using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem070 : EulerProblem
  {
    public override int Number
    {
      get { return 70; }
    }

    public override object Solve()
    {
      // the minimum comes euler (in the forums)
      const int Minimum = 1009;

      // the maximum comes from trial and error.
      const int Maximum = 3560;

      // generate a list of primes
      var primes = MathHelper.Primes()
        .SkipWhile(i => i < Minimum)
        .TakeWhile(i => i < Maximum).ToList();

      // create a variable to store the minimum phi and the N associated with it
      var minRatio = decimal.MaxValue;
      var minN = 0;

      // now, we need to choose pairs
      var generator = new Combinations<int>(primes, 2);
      foreach (var pair in generator)
      {
        // don't consider invalid N values
        var N = (pair[0] * pair[1]);
        if (N >= 10000000) { continue; }

        var phi = (pair[0] - 1) * (pair[1] - 1);
        var a = N.ToString();
        var b = phi.ToString();

        if ((a.Length == b.Length))
        {
          var m = new string(a.OrderBy(c => c).ToArray());
          var n = new string(b.OrderBy(c => c).ToArray());
          if (m.Equals(n))
          {
            var ratio = decimal.Divide(N, phi);
            if (ratio < minRatio)
            {
              minRatio = ratio;
              minN = N;
            }
          }
        }
      }

      // return the N value
      return minN;
    }
  }
}