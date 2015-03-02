using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem002 : EulerProblem
  {
    const int FourMillion = 4000000;

    public override int Number { get { return 2; } }

    public override object Solve()
    {
      // http://projecteuler.net/index.php?section=problems&id=2
      int fib1 = 1;
      int fib2 = 2;

      int sum = 2;
      bool keepGoing = true;

      while (keepGoing)
      {
        int next = fib1 + fib2;
        fib1 = fib2;
        fib2 = next;

        keepGoing = next < FourMillion;
        if (next % 2 == 0)
          sum += next;
      }

      return sum;
    }
  }
}
