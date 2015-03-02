using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem002 : EulerProblem
  {
    private const int FourMillion = 4000000;

    public override int Number
    {
      get { return 2; }
    }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=2
      var fib1 = 1;
      var fib2 = 2;

      var sum = 2;
      var keepGoing = true;

      while (keepGoing)
      {
        var next = fib1 + fib2;
        fib1 = fib2;
        fib2 = next;

        keepGoing = next < FourMillion;
        if (next % 2 == 0)
        {
          sum += next;
        }
      }

      return sum;
    }
  }
}