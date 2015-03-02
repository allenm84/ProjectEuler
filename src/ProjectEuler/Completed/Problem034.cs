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
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem034 : EulerProblem
  {
    public override int Number { get { return 34; } }

    public override object Solve()
    {
      const int Limit = 1000000;

      // a table containing the factorials for 0-9
      var table = new int[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 };

      // go through the limits
      long result = 0;
      for (int i = 3; i <= Limit; ++i)
      {
        var sum = i.GetDigits().Sum(d => table[d]);
        if (sum == i)
        {
          result += sum;
        }
      }

      return result;
    }
  }
}
