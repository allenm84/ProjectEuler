using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectEuler
{
  public class Problem030 : EulerProblem
  {
    public override int Number
    {
      get { return 30; }
    }

    public override object Solve()
    {
      var table = Enumerable
        .Range(0, 10)
        .Select(i => (int)Math.Pow(i, 5))
        .ToArray();

      long total = 0;
      Parallel.For(2, 1000000, n =>
      {
        var t = n.ToString();
        var digits = t.Select(c => ((int)c) - 48).ToArray();
        var sum = digits.Sum(d => table[d]);
        if (sum == n)
        {
          Interlocked.Add(ref total, sum);
        }
      });

      return total;
    }
  }
}