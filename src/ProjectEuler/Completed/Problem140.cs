using System;
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem140 : EulerProblem
  {
    public override int Number
    {
      get { return 140; }
    }

    public override object Solve()
    {
      // from Mystic
      // I generate the first 16 golden nugget on brute force, and i search a recursive connection between every number (like 137th problem).
      // So, i find that Un:=7*U(n-2)+7-U(n-4).
      // So just add these numbers.

      var nuggets = new List<BigInteger> {2, 5, 21, 42};
      for (var i = nuggets.Count; nuggets.Count < 30; ++i)
      {
        var next = 7 * nuggets[i - 2] + 7 - nuggets[i - 4];
        nuggets.Add(next);
      }
      return string.Format("{0}", nuggets.Sum());
    }
  }
}