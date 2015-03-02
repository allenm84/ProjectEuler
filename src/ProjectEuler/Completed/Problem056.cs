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
  public class Problem056 : EulerProblem
  {
    public override int Number { get { return 56; } }

    public override object Solve()
    {
      var maxSum = 0L;

      for (int a = 99; a > 0; --a)
      {
        for (int b = 99; b > 0; --b)
        {
          var result = BigInteger.Pow(a, b).ToString();
          var sum = result.Sum(c => ((long)c) - 48L);
          if (sum > maxSum)
          {
            maxSum = sum;
          }
        }
      }

      return maxSum;
    }
  }
}
