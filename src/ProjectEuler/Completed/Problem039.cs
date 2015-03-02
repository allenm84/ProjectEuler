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

namespace ProjectEuler
{
  public class Problem039 : EulerProblem
  {
    public override int Number { get { return 39; } }

    public override object Solve()
    {
      int maxSolutionCount = 0;
      int maxN = 0;

      for (int N = 1; N <= 1000; ++N)
      {
        int NHalf = N / 2;
        int solutionCount = 0;

        for (int a = 1; a < NHalf; ++a)
        {
          for (int b = a + 1; b < NHalf; ++b)
          {
            int c = N - (a + b);
            int p = a + b + c;

            if (p == N)
            {
              int left = (a * a) + (b * b);
              int right = (c * c);
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
