using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem130 : Problem129
  {
    public override int Number
    {
      get { return 130; }
    }

    public override object Solve()
    {
      const int limit = int.MaxValue - 1;

      var sum = 0;
      var count = 0;

      for (var n = 2; n < limit; ++n)
      {
        if (n.IsPrime()) { continue; }
        if (MathHelper.GCD(n, 10) != 1) { continue; }

        var n1 = n - 1;
        var an = A(n);

        if ((n1 % an) != 0) { continue; }
        sum += n;

        ++count;
        if (count == 25) { break; }
      }

      return sum;
    }
  }
}