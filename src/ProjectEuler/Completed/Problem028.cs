using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem028 : EulerProblem
  {
    public override int Number
    {
      get { return 28; }
    }

    public override object Solve()
    {
      int[] values = {7, 9, 3, 5};
      int[] diffs = {6, 8, 2, 4};
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

      // return the sum
      return sum;
    }
  }
}