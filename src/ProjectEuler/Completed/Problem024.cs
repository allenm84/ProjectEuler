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
  public class Problem024 : EulerProblem
  {
    public override int Number { get { return 24; } }

    public override object Solve()
    {
      string digits = "0123456789";
      Permutations<char> perm = new Permutations<char>(digits.ToList());
      var values = perm
        .Select(p => Convert.ToInt64(string.Join("", p)))
        .OrderBy(i => i)
        .ToArray();
      return values[1000000 - 1];
    }
  }
}
