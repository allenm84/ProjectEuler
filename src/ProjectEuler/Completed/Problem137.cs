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

namespace ProjectEuler
{
  public class Problem137 : EulerProblem
  {
    public override int Number { get { return 137; } }

    public override object Solve()
    {
      // from Youjaes
      // I brute forced the first 10 nuggets and noticed that...
      // Nugget(n) = Nugget(n-1) * 7 - Nugget(n-2) + 1
      // for n > 2, so my code became:

      var nuggets = new List<long> { 2L, 15L };
      for (int i = 2; nuggets.Count < 15; ++i)
      {
        var nugget = nuggets[i - 1] * 7L - nuggets[i - 2] + 1L;
        nuggets.Add(nugget);
      }
      return string.Format("{0}", nuggets.Last());
    }
  }
}
