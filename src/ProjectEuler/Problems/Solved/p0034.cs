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
  public class p0034 : euler
  {
    public override void Run()
    {
      const uint Limit = 1000000;

      // a table containing the factorials for 0-9
      var table = new uint[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

      // create the iteration variables
      uint i;

      // go through the limits
      ulong result = 0, sum;
      for (i = 3; i <= Limit; ++i)
      {
        sum = i.digits()
          .Select(d => table[d])
          .Aggregate((x, y) => x + y);

        if (sum == i)
        {
          result += sum;
        }
      }

      Console.WriteLine(result);
    }
  }
}
