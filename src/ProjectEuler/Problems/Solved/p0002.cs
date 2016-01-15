using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Advanced;

namespace ProjectEuler
{
  public class p0002 : euler
  {
    const int FourMillion = 4000000;

    public override void Run()
    {
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

      Console.WriteLine(sum);
    }
  }
}
