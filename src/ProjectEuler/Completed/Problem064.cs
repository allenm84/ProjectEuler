using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem064 : EulerProblem
  {
    public override int Number
    {
      get { return 64; }
    }

    public override object Solve()
    {
      var squares = new Dictionary<int, bool>();
      for (var i = 1; i <= 100; ++i)
      {
        squares[(i * i)] = true;
      }

      var count = 4;
      for (var N = 14; N <= 10000; ++N)
      {
        // if this is a perfect square, then continue
        if (squares.ContainsKey(N)) { continue; }
        var period = GetSqrt(N).Count() - 1;
        if ((period % 2) == 1) { ++count; }
      }

      return count;
    }

    private int FMn(int d, int a, int m)
    {
      return ((d * a) - m);
    }

    private int FDn(int S, int m, int d)
    {
      return ((S - (m * m)) / d);
    }

    private int FAn(double a, double m, double d)
    {
      return (int)Math.Floor((a + m) / d);
    }

    protected IEnumerable<int> GetSqrt(int S)
    {
      var a0 = (int)Math.Floor(Math.Sqrt(S));
      yield return a0;

      var m = 0;
      var d = 1;
      var a = a0;

      Tuple<int, int, int> original = null;
      while (true)
      {
        // calculate the next number
        var mn = FMn(d, a, m);
        var dn = FDn(S, mn, d);
        var an = FAn(a0, mn, dn);

        // if the original is null, then set it
        if (original == null)
        {
          original = new Tuple<int, int, int>(mn, dn, an);
        }
        else
        {
          if (original.Item1 == mn &&
            original.Item2 == dn &&
            original.Item3 == an)
          {
            break;
          }
        }

        // return the expansion
        yield return an;

        // update the variables
        m = mn;
        d = dn;
        a = an;
      }
    }
  }
}