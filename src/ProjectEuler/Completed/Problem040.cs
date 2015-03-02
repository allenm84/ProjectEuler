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
  public class Problem040 : EulerProblem
  {
    public override int Number { get { return 40; } }

    public override object Solve()
    {
      var indices = new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 };
      var capacity = indices.Last() + 1;

      var sb = new StringBuilder(capacity);
      var i = 1;
      while (sb.Length < capacity)
      {
        sb.Append(i++);
      }

      int result = 1;
      foreach (var index in indices)
      {
        int j = index - 1;
        char c = sb[j];
        int d = ((int)c) - 48;
        result *= d;
      }

      return result;
    }
  }
}
