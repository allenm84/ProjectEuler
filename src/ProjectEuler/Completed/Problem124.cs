using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem124 : EulerProblem
  {
    public override int Number
    {
      get { return 124; }
    }

    public override object Solve()
    {
      // fill the list with the rad(n) values
      var list = new List<int[]>();
      for (var n = 1; n <= 100000; ++n)
      {
        if (n.IsPrime())
        {
          list.Add(new[] {n, n});
          continue;
        }

        var factors = n.PrimeFactors();
        list.Add(new[] {n, factors.Distinct().Multiply()});
      }

      // sort on the rad value
      list.Sort((a, b) => a[1].CompareTo(b[1]));

      // return the proper value
      return string.Format("n = {0} rad(n) = {1}",
        list[10000 - 1]
          .Cast<object>()
          .ToArray());
    }
  }
}