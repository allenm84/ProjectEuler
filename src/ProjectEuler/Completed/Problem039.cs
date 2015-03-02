using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem039 : EulerProblem
  {
    public override int Number
    {
      get { return 39; }
    }

    public override object Solve()
    {
      var maxSolutionCount = 0;
      var maxN = 0;

      for (var N = 1; N <= 1000; ++N)
      {
        var NHalf = N / 2;
        var solutionCount = 0;

        for (var a = 1; a < NHalf; ++a)
        {
          for (var b = a + 1; b < NHalf; ++b)
          {
            var c = N - (a + b);
            var p = a + b + c;

            if (p == N)
            {
              var left = (a * a) + (b * b);
              var right = (c * c);
              if (left == right)
              {
                ++solutionCount;
              }
            }
          }
        }

        if (solutionCount > maxSolutionCount)
        {
          maxN = N;
          maxSolutionCount = solutionCount;
        }
      }

      return maxN;
    }
  }
}