using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0039 : euler
  {
    public override void Run()
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

      Console.WriteLine(maxN);
    }
  }
}
