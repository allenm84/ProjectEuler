using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using ProjectEuler.Properties;

namespace ProjectEuler
{
  public class Problem013 : EulerProblem
  {
    public override int Number
    {
      get { return 13; }
    }

    public override object Solve()
    {
      var numbers = Resources
        .Problem013Data
        .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => BigInteger.Parse(s.Trim()));

      var sum = new BigInteger(0);
      foreach (var n in numbers)
      {
        sum += n;
      }

      return string.Join("", sum.ToString().Take(10));
    }
  }
}