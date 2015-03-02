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
  public class Problem030 : EulerProblem
  {
    public override int Number { get { return 30; } }

    public override object Solve()
    {
      int[] table = Enumerable
        .Range(0, 10)
        .Select(i => (int)Math.Pow(i, 5))
        .ToArray();

      long total = 0;
      Parallel.For(2, 1000000, n =>
        {
          string t = n.ToString();
          int[] digits = t.Select(c => ((int)c) - 48).ToArray();
          int sum = digits.Sum(d => table[d]);
          if (sum == n)
          {
            Interlocked.Add(ref total, sum);
          }
        });

      return total;
    }
  }
}
