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
  public class Problem028 : EulerProblem
  {
    public override int Number { get { return 28; } }

    public override object Solve()
    {
      int[] values = new int[] { 7, 9, 3, 5 };
      int[] diffs = new int[] { 6, 8, 2, 4 };
      int n = 3;

      int sum = 1;
      do
      {
        sum += values.Sum();
        for (int i = 0; i < 4; ++i)
        {
          // calculate the new value
          int value = values[i] + (diffs[i] + 8);

          // calculate the new diff
          int diff = value - values[i];

          // save the new values
          values[i] = value;
          diffs[i] = diff;
        }
        n += 2;
      }
      while (n <= 1001);

      // return the sum
      return sum;
    }
  }
}
