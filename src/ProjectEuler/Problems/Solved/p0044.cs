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
  public class p0044 : euler
  {
    public override void Run()
    {
      // create a list to hold the diff values
      var D = 0;
      var a = 0;
      var b = 0;

      // go through the pentagonal numbers
      for (var j = 1; j < short.MaxValue; ++j)
      {
        var Pj = math.npentagon(j);
        for (var k = 1; k < short.MaxValue; ++k)
        {
          if (k == j) { continue; }
          var Pk = math.npentagon(k);

          var sum = Pj + Pk;
          var diff = math.abs(Pj - Pk);

          if (math.isPentagonal(sum) && math.isPentagonal(diff))
          {
            D = diff;
            a = math.min(Pk, Pj);
            b = math.max(Pk, Pj);

            k = short.MaxValue;
            j = short.MaxValue;
          }
        }
      }

      Console.WriteLine(D);
    }
  }
}
