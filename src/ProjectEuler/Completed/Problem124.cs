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
using System.IO;
using System.Collections;
using System.Data;
using System.Common.Extensions;

namespace ProjectEuler
{
  public class Problem124 : EulerProblem
  {
    public override int Number { get { return 124; } }

    public override object Solve()
    {
      // fill the list with the rad(n) values
      var list = new List<int[]>();
      for (int n = 1; n <= 100000; ++n)
      {
        if (n.IsPrime())
        {
          list.Add(new int[] { n, n });
          continue;
        }

        var factors = n.PrimeFactors();
        list.Add(new int[] { n, factors.Distinct().Multiply() });
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
