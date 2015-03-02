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
  public class Problem140 : EulerProblem
  {
    public override int Number { get { return 140; } }

    public override object Solve()
    {
      // from Mystic
      // I generate the first 16 golden nugget on brute force, and i search a recursive connection between every number (like 137th problem).
      // So, i find that Un:=7*U(n-2)+7-U(n-4).
      // So just add these numbers.

      var nuggets = new List<BigInteger> { 2, 5, 21, 42 };
      for (int i = nuggets.Count; nuggets.Count < 30; ++i)
      {
        BigInteger next = 7 * nuggets[i - 2] + 7 - nuggets[i - 4];
        nuggets.Add(next);
      }
      return string.Format("{0}", nuggets.Sum());
    }
  }
}
