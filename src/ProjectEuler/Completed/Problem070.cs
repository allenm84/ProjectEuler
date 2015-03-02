using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;
using System.Diagnostics;
using System.IO;

namespace ProjectEuler
{
  public class Problem070 : EulerProblem
  {
    public override int Number { get { return 70; } }

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
      decimal minRatio = decimal.MaxValue;
      int minN = 0;

      // now, we need to choose pairs
      var generator = new Combinations<int>(primes, 2);
      foreach (var pair in generator)
      {
        // don't consider invalid N values
        int N = (pair[0] * pair[1]);
        if (N >= 10000000) continue;

        int phi = (pair[0] - 1) * (pair[1] - 1);
        var a = N.ToString();
        var b = phi.ToString();

        if ((a.Length == b.Length))
        {
          var m = new string(a.OrderBy(c => c).ToArray());
          var n = new string(b.OrderBy(c => c).ToArray());
          if (m.Equals(n))
          {
            decimal ratio = decimal.Divide(N, phi);
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
