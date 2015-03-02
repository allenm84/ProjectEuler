using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem006 : EulerProblem
  {
    public override int Number
    {
      get { return 6; }
    }

    public override object Solve()
    {
      long sumSquares = 0;
      long sum = 0;

      for (long i = 1; i < 101; ++i)
      {
        var square = (i * i);
        sumSquares += square;
        sum += i;
      }

      var squareSum = (sum * sum);
      return Math.Abs(sumSquares - squareSum);
    }
  }
}