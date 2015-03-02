using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem056 : EulerProblem
  {
    public override int Number
    {
      get { return 56; }
    }

    public override object Solve()
    {
      var maxSum = 0L;

      for (var a = 99; a > 0; --a)
      {
        for (var b = 99; b > 0; --b)
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