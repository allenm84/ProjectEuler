using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
  public class Problem075 : EulerProblem
  {
    public override int Number
    {
      get { return 75; }
    }

    public override object Solve()
    {
      const int Maximum = 1500000;

      // first, generate all of the valid primitive triples
      var triples = new List<int[]>();
      var limit = (int)Math.Round(Math.Sqrt(Maximum));
      for (var m = 2; m <= limit; ++m)
      {
        for (var n = 1; n < m; ++n)
        {
          if (MathHelper.GCD(m, n) == 1 && ((m + n) % 2 == 1))
          {
            var triple = EuclidTriple(m, n);
            if (triple.Sum() <= Maximum)
            {
              triples.Add(triple);
            }
          }
        }
      }

      // second, go through each primitive triple and generate triples
      // that are less than or equal to the maximum
      var count = triples.Count;
      for (var i = 0; i < count; ++i)
      {
        var triple = triples[i];
        var k = 2;
        var keepGoing = false;
        do
        {
          var newTriple = triple.Select(t => t * k).ToArray();
          keepGoing = (newTriple.Sum() <= Maximum);
          if (keepGoing)
          {
            triples.Add(newTriple);
            ++k;
          }
        } while (keepGoing);
      }

      // finally, group the triples by their sum and count the number of sums that are 1
      var groups = (from t in triples
        group t by t.Sum()
        into tg
        select tg.Count()).ToArray();
      return groups.Count(i => i == 1);
    }

    protected int[] EuclidTriple(int m, int n)
    {
      var m2 = (m * m);
      var n2 = (n * n);

      var a = m2 - n2;
      var b = 2 * m * n;
      var c = m2 + n2;

      return new[] {a, b, c};
    }
  }
}