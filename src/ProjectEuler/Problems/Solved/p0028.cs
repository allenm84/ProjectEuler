using System;
using System.Advanced;
using System.Advanced.Combinations;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class p0028 : euler
  {
    public override void Run()
    {
      int[] values = { 7, 9, 3, 5 };
      int[] diffs = { 6, 8, 2, 4 };
      var n = 3;

      var sum = 1;
      do
      {
        sum += values.Sum();
        for (var i = 0; i < 4; ++i)
        {
          // calculate the new value
          var value = values[i] + (diffs[i] + 8);

          // calculate the new diff
          var diff = value - values[i];

          // save the new values
          values[i] = value;
          diffs[i] = diff;
        }
        n += 2;
      } while (n <= 1001);

      Console.WriteLine(sum);
    }
  }
}
