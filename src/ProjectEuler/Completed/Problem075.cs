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

namespace ProjectEuler
{
  public class Problem075 : EulerProblem
  {
    public override int Number { get { return 75; } }

    public override object Solve()
    {
      const int Maximum = 1500000;

      // first, generate all of the valid primitive triples
      var triples = new List<int[]>();
      int limit = (int)Math.Round(Math.Sqrt(Maximum));
      for (int m = 2; m <= limit; ++m)
      {
        for (int n = 1; n < m; ++n)
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
      int count = triples.Count;
      for (int i = 0; i < count; ++i)
      {
        var triple = triples[i];
        int k = 2;
        bool keepGoing = false;
        do
        {
          var newTriple = triple.Select(t => t * k).ToArray();
          keepGoing = (newTriple.Sum() <= Maximum);
          if (keepGoing)
          {
            triples.Add(newTriple);
            ++k;
          }
        }
        while (keepGoing);
      }

      // finally, group the triples by their sum and count the number of sums that are 1
      var groups = (from t in triples
                    group t by t.Sum() into tg
                    select tg.Count()).ToArray();
      return groups.Count(i => i == 1);
    }

    protected int[] EuclidTriple(int m, int n)
    {
      int m2 = (m * m);
      int n2 = (n * n);

      int a = m2 - n2;
      int b = 2 * m * n;
      int c = m2 + n2;

      return new int[] { a, b, c };
    }
  }
}
