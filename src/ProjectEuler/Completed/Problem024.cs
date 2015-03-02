using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem024 : EulerProblem
  {
    public override int Number
    {
      get { return 24; }
    }

    public override object Solve()
    {
      var digits = "0123456789";
      var perm = new Permutations<char>(digits.ToList());
      var values = perm
        .Select(p => Convert.ToInt64(string.Join("", p)))
        .OrderBy(i => i)
        .ToArray();
      return values[1000000 - 1];
    }
  }
}