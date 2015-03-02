using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace ProjectEuler
{
  public class Problem065 : EulerProblem
  {
    public override int Number
    {
      get { return 65; }
    }

    public override object Solve()
    {
      var hv = new BigInteger[] {0, 1};
      var kv = new BigInteger[] {1, 0};

      var c = 0;
      var sum = 0;

      foreach (var a in e_cf())
      {
        var h = (a * hv[1]) + hv[0];
        var k = (a * kv[1]) + kv[0];

        hv[0] = hv[1];
        kv[0] = kv[1];

        hv[1] = h;
        kv[1] = k;

        ++c;
        if (c == 100)
        {
          sum = h.ToString().Sum(d => ((int)d) - 48);
          break;
        }
      }

      return sum;
    }

    /// <summary>
    /// Retrieves the a values for the continued fraction of e
    /// </summary>
    private IEnumerable<int> e_cf()
    {
      yield return 2;
      var num = 2;
      while (num > 0)
      {
        yield return 1;

        yield return num;
        num += 2;

        yield return 1;
      }
    }
  }
}