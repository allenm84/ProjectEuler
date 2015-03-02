using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectEuler.Properties;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;
using Facet.Combinatorics;

namespace ProjectEuler
{
  public class Problem015 : EulerProblem
  {
    public override int Number { get { return 15; } }

    public override object Solve()
    {
      const int Size = 20;

      // for the example listed, we have 6 routes through a 2x2 grid without backtracking.
      // We basically have a binary tree, but more simply, we the following routes through
      // the grid:
      //
      // 0011, 0101, 0110, 1001, 1010, 1100.
      //
      // The starting route is the base route.  If we do the default (0), but then switch to
      // doing the non-default (1). The default is right while the non-default is down. We
      // can simplify this further by getting the number of permutations of the base route.

      var zeroes = Enumerable.Repeat('0', Size);
      var ones = Enumerable.Repeat('1', Size);
      var values = zeroes.Concat(ones).ToList();

      Permutations<char> perm = new Permutations<char>(values);
      return perm.Count;
    }
  }
}
